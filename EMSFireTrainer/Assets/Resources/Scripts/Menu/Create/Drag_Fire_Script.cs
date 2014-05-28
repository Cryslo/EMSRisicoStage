using UnityEngine;
using System.Collections;
using System.IO;



public class Drag_Fire_Script : MonoBehaviour
{
    Camera camera;
    Transform obj;
    Vector3 offset;
    Vector3 prefPoint;
    GameObject FireIcon;
    GameObject selectedFire;
    GameObject IconHolder;
    TopBar_Script CBFscript;

    GameObject fireObject;
    Sprite fireSprite;
    SpriteRenderer fireRenderer;
    Bounds fireBounds;
    BoxCollider2D fireCollider;
    Vector3 position;

    float tempx;
    float tempy;

    Vector2 mPos;
    GameObject fireHolder;

    void Start()
    {
        camera = GameObject.Find("Main Camera").camera;
        fireHolder = GameObject.Find("createdSceneObjectHolder");
        CBFscript = GameObject.Find("CreateMenuScriptHolder").GetComponent<TopBar_Script>();
    }
    private void Update()
    {
        Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.transform == this.transform && obj == null)
            {
                addFire(fireHolder.transform);
                obj = fireObject.transform;
                //offset = new Vector3(mousePos.x - hit.transform.position.x, mousePos.y - hit.transform.position.y, obj.position.z);
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (obj)
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                obj.position = new Vector3(ray.origin.x - offset.x, ray.origin.y - offset.y, obj.position.z);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (obj != null && !obj.GetComponent<Fire_Propertie_Menu_Script>())
            {
                obj.gameObject.AddComponent<Fire_Propertie_Menu_Script>();
            }
            obj = null;
            IconHolder = null;
            CBFscript.deleteTopBar();
        }
    }

    private void addFire(Transform parent)
    {
        fireObject = new GameObject("FireIcon_" + fireHolder.transform.childCount);
        fireSprite = Resources.Load<Sprite>("Sprites/Fire/SmallFire_Object");
        fireRenderer = fireObject.AddComponent<SpriteRenderer>();
        fireRenderer.sprite = fireSprite;
        fireCollider = fireObject.AddComponent<BoxCollider2D>();
        fireRenderer.sortingOrder = 1;
        fireObject.transform.parent = fireHolder.transform;

        float screenDPI = Screen.dpi / 160;

        float xSize = fireSprite.bounds.size.x;
        float ySize = fireSprite.bounds.size.y;

        float width;
        float height;

        if (screenDPI > 0)
        {
            width = 204 * screenDPI;
            height = 330 * screenDPI;
        }
        else
        {
            width = 204 * parent.transform.localScale.x / 2;
            height = 280 * parent.transform.localScale.y / 2;
        }

        float worldwidth = (camera.orthographicSize * 2 / Screen.height * width) / xSize;
        float worldHeight = (camera.orthographicSize * 2 / Screen.height * height) / ySize;

        fireObject.transform.localScale = new Vector3(worldwidth, worldHeight, 1);
    }

}
