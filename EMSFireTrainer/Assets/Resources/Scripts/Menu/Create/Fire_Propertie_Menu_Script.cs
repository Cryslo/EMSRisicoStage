using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fire_Propertie_Menu_Script : MonoBehaviour
{
    GameObject propertyMenuObject;
    Sprite propertyMenu;
    SpriteRenderer propertyMenuRenderer;

    GameObject option_Object;
    GameObject bottom_Bar_Object_Left;
    GameObject bottom_Bar_Object_Right;

    Sprite option_Sprite;
    Sprite[] bottom_Bar_Left;
    Sprite[] bottom_Bar_Right;

    SpriteRenderer option_Renderer;
    SpriteRenderer bottom_Bar_Renderer_Left;
    SpriteRenderer bottom_Bar_Renderer_Right;

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
    Rect menuRec;

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
                    removeMenu();
                    obj = hit.transform;
                    if (havePos == false)
                    {
                        prefPoint = obj.position;
                        havePos = true;
                    }
                    offset = obj.position - mousePos;
                }
            }
            if (menuOn && propertyMenuObject != null)
            {
                print("ShouldRemove 1");
                //menuRec = new Rect(propertyMenuObject.transform.position.x, propertyMenuObject.transform.position.y, propertyMenuRenderer.sprite.bounds.size.x, propertyMenuRenderer.sprite.bounds.size.y);
                //if (!menuRec.Contains(Input.mousePosition))
                //{
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                    if (!hit.transform == propertyMenuObject.transform)
                    {
                        print("ShouldRemove 2");
                        removeMenu();
                        menuOn = false;
                    }
                //}
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (obj)
            {
                    if (prefPoint == obj.position)
                    {
                        if (this.gameObject.name == obj.transform.name)
                        {
                            removeMenu();
                            screenPos = camera.WorldToScreenPoint(obj.position);
                            menuOn = true;
                            oldX = obj.position.x;
                            oldY = obj.position.y;
                            print(oldX);
                            print(oldY);
                            lastObject = obj.gameObject;

                            //addMenu(obj.position);

                            if (oldX < 0 && oldY > 0)
                            {
                                print("Upper Left");
                                addMenu(new Vector3(obj.position.x + 2, obj.position.y - 2, 0));
                            }
                            if (oldX > 0 && oldY > 0)
                            {
                                print("Upper Right");
                                addMenu(new Vector3(obj.position.x - 4, obj.position.y - 2, 0));
                            }
                            if (oldX < 0 && oldY < 0)
                            {
                                print("Lower Left");
                                addMenu(new Vector3(obj.position.x + 2, obj.position.y, 0));
                            }
                            if (oldX > 0 && oldY < 0)
                            {
                                print("Lower Right");
                                addMenu(new Vector3(obj.position.x - 4, obj.position.y, 0));
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

    void removeMenu()
    {
        if (GameObject.Find("propertyMenuObject"))
        {

            Destroy(GameObject.Find("propertyMenuObject"));
        }
    }

    void addMenu(Vector3 position)
    {
        propertyMenuObject = new GameObject("propertyMenuObject");
        propertyMenu = Resources.Load<Sprite>("Sprites/Create_Menu/Property_Menu/Background");
        propertyMenuRenderer = propertyMenuObject.AddComponent<SpriteRenderer>();
        propertyMenuRenderer.sprite = propertyMenu;
        propertyMenuObject.transform.position = position;
        propertyMenuObject.AddComponent<BoxCollider2D>();

        bottom_Bar_Object_Left = new GameObject("bottom_Bar_Object_Left");
        bottom_Bar_Left = Resources.LoadAll<Sprite>("Sprites/Create_Menu/Property_Menu/Button_Bottom");
        bottom_Bar_Renderer_Left = bottom_Bar_Object_Left.AddComponent<SpriteRenderer>();
        bottom_Bar_Renderer_Left.sprite = bottom_Bar_Left[0];
        bottom_Bar_Object_Left.transform.position = new Vector3(position.x, position.y - propertyMenuRenderer.sprite.bounds.size.y, position.z);
        bottom_Bar_Renderer_Left.sortingOrder = 1;
        bottom_Bar_Object_Left.transform.parent = propertyMenuObject.transform;
        bottom_Bar_Object_Left.AddComponent<BoxCollider2D>();

        bottom_Bar_Object_Right = new GameObject("bottom_Bar_Object_Right");
        bottom_Bar_Right = Resources.LoadAll<Sprite>("Sprites/Create_Menu/Property_Menu/Button_Bottom");
        bottom_Bar_Renderer_Right = bottom_Bar_Object_Right.AddComponent<SpriteRenderer>();
        bottom_Bar_Renderer_Right.sprite = bottom_Bar_Right[1];
        bottom_Bar_Object_Right.transform.position = new Vector3(position.x + bottom_Bar_Renderer_Right.sprite.bounds.size.x, position.y - propertyMenuRenderer.sprite.bounds.size.y, position.z);
        bottom_Bar_Renderer_Right.sortingOrder = 1;
        bottom_Bar_Object_Right.transform.parent = propertyMenuObject.transform;
        bottom_Bar_Object_Right.AddComponent<BoxCollider2D>();

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
        obj.transform.parent = propertyMenuObject.transform;
        spriteRen.sprite = sprite;
        spriteRen.sortingOrder = 1;
        if (i < 3)
        {
            obj.transform.position = new Vector3(propertyMenuObject.transform.position.x + i * propertyMenuRenderer.sprite.bounds.size.x / 3, propertyMenuObject.transform.position.y, 0);
        }
        else
        {
            obj.transform.position = new Vector3(propertyMenuObject.transform.position.x + (i - (i / 3) * 3) * propertyMenuRenderer.sprite.bounds.size.x / 3, propertyMenuObject.transform.position.y - spriteRen.sprite.bounds.size.y, 0);
        }
    }
}
