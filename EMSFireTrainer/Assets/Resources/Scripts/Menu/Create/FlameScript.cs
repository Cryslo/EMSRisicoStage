using UnityEngine;
using System.Collections;
using pumpkin.display;
using System.Collections.Generic;
using pumpkin.events;

namespace BlusSimulator
{
    public class FlameScript : MonoBehaviour
    {
        Transform obj;
        bool havePos = false;
        Vector3 offset;
        Vector3 prefPoint = new Vector3(0, 0, 0);
        Camera camera;
        Vector3 screenPos;

        bool expandToggled = false;
        GameObject lastObject;
        GameObject newObject;
        bool menuOn = false;
        GameObject selectedFire;
        float oldX;
        float oldY;

        Rect rec;
        public bool canGrow = false;
        public string RequiredFireAgent = "None";
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            /*if (Menu_Script.menuState == "Play")
            {
                if (expandToggled == true)
                {
                    this.transform.localScale = new Vector3(this.transform.localScale.x + 0.01f, this.transform.localScale.y + 0.01f, this.transform.localScale.z);
                }
            }
            if (Menu_Script.menuState != "CreateMenu")
            {
                print(Menu_Script.menuState);
                    Destroy(this.gameObject);
            }*/

            if (Input.GetMouseButtonDown(0))
            {
                if (!obj)
                {
                    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                    {
                        if (this.gameObject.name == hit.transform.name)
                        {
                            obj = hit.transform;
                            if (havePos == false)
                            {
                                prefPoint = obj.position;
                                havePos = true;
                            }
                            offset = obj.position - ray.origin;
                        }
                    }
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
                            screenPos = camera.WorldToScreenPoint(obj.position);
                            menuOn = true;
                            oldX = obj.position.x;
                            oldY = obj.position.y;
                            lastObject = obj.gameObject;


                        }
                    }
                }
                obj = null;
            }
            if (obj)
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                obj.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, obj.position.z);

            }
        }


        private void ExpansionHandler(CEvent e)
        {
            if (expandToggled == false)
            {
                expandToggled = true;
                print(expandToggled);
            }
            else if (expandToggled == true)
            {
                expandToggled = false;
                print(expandToggled);
            }
        }
        private void hDec(CEvent e)
        {
            if (lastObject != null)
            {
                lastObject.transform.localScale = new Vector3(lastObject.transform.localScale.x, lastObject.transform.localScale.y - 0.1f, lastObject.transform.localScale.z);
                print("Height Scaled Down" + lastObject.transform.localScale.y);
            }
        }

        private void hInc(CEvent e)
        {
            if (lastObject != null)
            {
                lastObject.transform.localScale = new Vector3(lastObject.transform.localScale.x, lastObject.transform.localScale.y + 0.1f, lastObject.transform.localScale.z);
                print("Height Scaled Up" + lastObject.transform.localScale.y);
            }
        }

        private void wdec(CEvent e)
        {
            if (lastObject != null)
            {
                lastObject.transform.localScale = new Vector3(lastObject.transform.localScale.x - 0.1f, lastObject.transform.localScale.y, lastObject.transform.localScale.z);
                print("Width Scaled Down" + lastObject.transform.localScale.x);
            }
        }

        private void wInc(CEvent e)
        {
            if (lastObject != null)
            {
                lastObject.transform.localScale = new Vector3(lastObject.transform.localScale.x + 0.1f, lastObject.transform.localScale.y, lastObject.transform.localScale.z);
                print("Width Scaled Up" + lastObject.transform.localScale.x);
            }
        }

        private void RotateRHandler(CEvent e)
        {
            if (lastObject != null)
            {
                lastObject.transform.Rotate(new Vector3(lastObject.transform.localRotation.x, lastObject.transform.localRotation.y, lastObject.transform.localRotation.z - 1.5f));
                print("Rotated Right: " + lastObject.transform.localRotation.z);
            }
        }

        private void RotateLHandler(CEvent e)
        {
            if (lastObject != null)
            {
                lastObject.transform.Rotate(new Vector3(lastObject.transform.localRotation.x, lastObject.transform.localRotation.y, lastObject.transform.localRotation.z + 1.5f));
                print("Rotated Left: " + lastObject.transform.localRotation.z);
            }
        }
        /*void OnGUI()
        {
            Event e = Event.current;
            if (menuOn)
            {
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!rec.Contains(e.mousePosition))
                {
                    menuOn = false;
                }
            }
        }*/
    }
}
