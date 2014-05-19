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
        }

        private void Update()
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
                if (hit.transform == this.transform && obj == null)
                {
                    print(hit.transform.name);
                    FireIcon = Resources.Load("Prefabs/Scene_1_Prefabs/fireSprite_03") as GameObject;
                    IconHolder = (GameObject)Instantiate(FireIcon, new Vector3(0, 0, 0), Quaternion.identity);
                    IconHolder.name = "FireIcon";
                    obj = IconHolder.transform;
                    offset = new Vector3(mousePos.x - hit.transform.position.x, mousePos.y - hit.transform.position.y, obj.position.z);
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (obj)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    obj.position = new Vector3(ray.origin.x - offset.x, ray.origin.y - offset.y, obj.position.z);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                obj = null;
                IconHolder = null;
            }
        }
    }
}
