using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fire_Propertie_Menu_Script : MonoBehaviour
{
    GameObject propertyMenuObject;
    Sprite propertyMenu;
    SpriteRenderer propertyMenuRenderer;

    GameObject option_Object;
    GameObject bottom_Bar_Object;

    Sprite option_Sprite;
    Sprite bottom_Bar;

    SpriteRenderer option_Renderer;
    SpriteRenderer bottom_Bar_Renderer;

    private List<string> spawnList = new List<string>();

    Transform obj;
    Vector3 prefPoint = new Vector3(0, 0, 0);
    Camera camera;
    Vector3 screenPos;
    Vector3 offset;
    GameObject lastObject;
    GameObject newObject;
    float oldX;
    float oldY;
    bool havePos = false;
    bool menuOn = false;

    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("Main Camera").camera;

    }

    void Update()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (!obj)
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                    if (this.transform == hit.transform)
                    {
                        obj = hit.transform;
                        if (havePos == false)
                        {
                            prefPoint = obj.position;
                            havePos = true;
                        }
                        offset = obj.position - mousePos;
                    }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (obj)
            {
                print("Ohai");
                if (prefPoint == obj.position)
                {
                    if (this.gameObject.name == obj.transform.name)
                    {
                        screenPos = camera.WorldToScreenPoint(obj.position);
                        menuOn = true;
                        oldX = obj.position.x;
                        oldY = obj.position.y;
                        lastObject = obj.gameObject;

                        addMenu(obj.position);

                        if (oldX < 0 && oldY > 0)
                        {
                                print("Upper Left");
                                addMenu(new Vector3(-50, 50,0));
                        }
                        if (oldX > 0 && oldY > 0)
                        {
                            print("Upper Right");
                            addMenu(new Vector3(50, -50, 0));
                        }
                        if (oldX < 0 && oldY < 0)
                        {
                            print("Lower Left");
                            addMenu(new Vector3(-50, 50, 0));
                        }
                        if (oldX > 0 && oldY < 0)
                        {
                            print("Lower Right");
                            addMenu(new Vector3(50, -50, 0));
                        }
                    }
                }
            }
            obj = null;
            havePos = false;
        }
        if (obj)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            obj.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, obj.position.z);

        }
    }

    void addMenu(Vector3 position)
    {
        propertyMenuObject = new GameObject("propertyMenuObject");
        propertyMenu = Resources.Load<Sprite>("Sprites/Create_Menu/Property_Menu/Background");
        propertyMenuRenderer = propertyMenuObject.AddComponent<SpriteRenderer>();
        propertyMenuRenderer.sprite = propertyMenu;
        propertyMenuObject.transform.position = position;

        //propertyMenuObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

        for (int i = 0; i < 6; i++)
        {
            addContent(option_Object, option_Sprite, option_Renderer, "Button_Extinguisher_" + i, "Sprites/Create_Menu/Property_Menu/Button_Extinguisher", i);
        }
    }

    void addContent(GameObject obj, Sprite sprite, SpriteRenderer spriteRen, string name, string location, int i)
    {
        obj = new GameObject(name);
        sprite = Resources.Load<Sprite>(location);
        spriteRen = obj.AddComponent<SpriteRenderer>();
        spriteRen.sprite = sprite;
        spriteRen.sortingOrder = 1;
        if (i < 3)
        {
            obj.transform.position = new Vector3(i * propertyMenuRenderer.sprite.bounds.size.x / 3, 0, 0);
        }
        else
        {
            obj.transform.position = new Vector3((i - (i / 3) * 3) * propertyMenuRenderer.sprite.bounds.size.x / 3, -spriteRen.sprite.bounds.size.y, 0);
        }
        obj.transform.parent = propertyMenuObject.transform;
    }
}
