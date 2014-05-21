using UnityEngine;
using System.Collections;
using pumpkin.display;
using pumpkin.events;
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

    float tempx;
    float tempy;

    Vector2 mPos;
    GameObject fireHolder;

    public void Create_ObjectMenu()
    {

    }
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
                print(hit.transform.name);
                FireIcon = Resources.Load("Prefabs/Scene_1_Prefabs/fireSprite_03") as GameObject;
                IconHolder = (GameObject)Instantiate(FireIcon, new Vector3(0, 0, 0), Quaternion.identity);
                IconHolder.transform.parent = fireHolder.transform;
                IconHolder.name = "FireIcon_" + IconHolder.transform.parent.childCount;
                obj = IconHolder.transform;
                offset = new Vector3(mousePos.x - hit.transform.position.x, mousePos.y - hit.transform.position.y, obj.position.z);
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
            obj = null;
            IconHolder = null;
            CBFscript.deleteTopBar();
        }
    }

}
