using UnityEngine;
using System.Collections;
using pumpkin.display;
using pumpkin.events;
using System.IO;



namespace BlusSimulator
{
    public class Create_Object_Drag_Script : MonoBehaviour
    {
        Camera camera;
        Transform obj;
        Vector3 offset;
        Vector3 prefPoint;
        GameObject FireIcon;
        GameObject selectedFire;
        GameObject IconHolder;

        float tempx;
        float tempy;

        Vector2 mPos;

        public void Create_ObjectMenu()
        {
            //camera = GameObject.Find("Main Camera").camera;
            FireIcon = Resources.Load("Prefabs/Scene_1_Prefabs/fireSprite_03") as GameObject;
        }

        private void Update()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Infinity));
            RaycastHit2D hit = Physics2D.Raycast(mousePos, mousePos - Camera.main.ScreenToWorldPoint(mousePos));
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.tag == "FireIcon")
                {
                    print(hit.transform.name);
                    IconHolder = (GameObject)Instantiate(FireIcon, new Vector3(0, 0, 0), Quaternion.identity);
                    IconHolder.transform.parent = GameObject.Find("FlameHolder").transform;
                    IconHolder.name = "Fire";
                    obj = IconHolder.transform;
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (obj)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    obj.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, obj.position.z);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {

            }
        }
    }
}
