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
        private Vector3 prefPoint = new Vector3(0, 0, 0);
        Camera camera;
        Vector3 screenPos;
        List<MovieClip> SliderHolder;
        Rect guiRect;
        Rect guiRectB1;
        Rect guiRectB2;
        Rect guiRectT1;
        Rect guiRectT2;
        Rect guiRectT3;
        Rect guiRectT4;
        Rect guiRectCh;
        Rect sliderRectx;
        Rect sliderRecty;
        Rect sliderRectz;


        MovieClip Delete_Btn;
        MovieClip Dupe_Btn;
        MovieClip Width_Increase_Btn;
        MovieClip Width_Decrease_Btn;
        MovieClip Height_Increase_Btn;
        MovieClip Height_Decrease_Btn;
        MovieClip Expansion_Toggle;

        Rect Width_Increase_BtnR;
        Rect Width_Decrease_BtnR;
        Rect Height_Increase_BtnR;
        Rect Height_Decrease_BtnR;

        Rect Rotate_L_Btn;
        Rect Rotate_R_BtnR;

        MovieClip RotateL;
        MovieClip RotateR;
        MovieClip Agent01;
        MovieClip Agent02;
        MovieClip Agent03;
        MovieClip Agent04;
        MovieClip Agent05;
        MovieClip Agent06;

        bool expandToggled = false;
        Rect rec;
        Stage stage;
        GameObject lastObject;
        GameObject newObject;
        MovieClip Property_Menu;
        bool menuOn = false;
        GameObject selectedFire;
        float oldX;
        float oldY;

        public bool canGrow = false;
        public string RequiredFireAgent = "None";
        // Use this for initialization
        void Start()
        {
            SliderHolder = new List<MovieClip>();
            camera = GameObject.Find("Main Camera").camera;
            stage = MovieClipOverlayCameraBehaviour.instance.stage;
            Property_Menu = new MovieClip("SWFs/Menu/PropertyScreen.swf:Holder");
            Width_Increase_Btn = Property_Menu.getChildByName("Width_Increase_Btn") as MovieClip;
            Width_Decrease_Btn = Property_Menu.getChildByName("Width_Decrease_Btn") as MovieClip;
            Height_Increase_Btn = Property_Menu.getChildByName("Height_Increase_Btn") as MovieClip;
            Height_Decrease_Btn = Property_Menu.getChildByName("Height_Decrease_Btn") as MovieClip;
            Expansion_Toggle = Property_Menu.getChildByName("Expansion_Toggle") as MovieClip;
            Delete_Btn = Property_Menu.getChildByName("Delete_Btn") as MovieClip;
            Dupe_Btn = Property_Menu.getChildByName("Dupe_Btn") as MovieClip;

            RotateL = Property_Menu.getChildByName("Rotate_Left") as MovieClip;
            RotateR = Property_Menu.getChildByName("Rotate_Right") as MovieClip;
            Agent01 = Property_Menu.getChildByName("Agent01") as MovieClip;
            Agent02 = Property_Menu.getChildByName("Agent02") as MovieClip;
            Agent03 = Property_Menu.getChildByName("Agent03") as MovieClip;
            Agent04 = Property_Menu.getChildByName("Agent04") as MovieClip;
            Agent05 = Property_Menu.getChildByName("Agent05") as MovieClip;
            Agent06 = Property_Menu.getChildByName("Agent06") as MovieClip;

            SliderHolder.AddMany(Width_Increase_Btn, Width_Decrease_Btn, Height_Increase_Btn, Height_Decrease_Btn, Expansion_Toggle, Delete_Btn);
            //, RotateL, RotateR, Agent01, Agent02, Agent03, Agent04, Agent05, Agent06
            foreach (MovieClip name in SliderHolder)
            {
                addGotoAndStop(name);
            }

            Width_Increase_Btn.addEventListener(MouseEvent.CLICK, wInc);
            Width_Decrease_Btn.addEventListener(MouseEvent.CLICK, wdec);
            Height_Increase_Btn.addEventListener(MouseEvent.CLICK, hInc);
            Height_Decrease_Btn.addEventListener(MouseEvent.CLICK, hDec);
            RotateL.addEventListener(MouseEvent.CLICK, RotateLHandler);
            RotateR.addEventListener(MouseEvent.CLICK, RotateRHandler);
            Agent01.addEventListener(MouseEvent.CLICK, Agent01Handler);
            Agent02.addEventListener(MouseEvent.CLICK, Agent02Handler);
            Agent03.addEventListener(MouseEvent.CLICK, Agent03Handler);
            Agent04.addEventListener(MouseEvent.CLICK, Agent04Handler);
            Agent05.addEventListener(MouseEvent.CLICK, Agent05Handler);
            Agent06.addEventListener(MouseEvent.CLICK, Agent06Handler);
            Expansion_Toggle.addEventListener(MouseEvent.CLICK, ExpansionHandler);
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
                    if (Input.mousePosition.x > Screen.width)
                    {
                        print("NOPE");
                        obj.position = prefPoint;
                    }
                    if (Input.mousePosition.x < 0)
                    {
                        print("NOPE");
                        obj.position = prefPoint;
                    }
                    if (Input.mousePosition.y > Screen.height)
                    {
                        print("NOPE");
                        obj.position = prefPoint;
                    }
                    if (Input.mousePosition.y < 0)
                    {
                        print("NOPE");
                        obj.position = prefPoint;
                    }

                    if (prefPoint == obj.position)
                    {
                        if (this.gameObject.name == obj.transform.name)
                            {
                                screenPos = camera.WorldToScreenPoint(obj.position);
                                menuOn = true;
                                oldX = obj.position.x;
                                oldY = obj.position.y;

                                //Text
                                guiRect = new Rect(screenPos.x, Screen.height - screenPos.y, 200, 230);
                                guiRectT1 = new Rect(screenPos.x, Screen.height - screenPos.y, 100, 20);
                                guiRectT2 = new Rect(screenPos.x + 50, Screen.height - screenPos.y, 100, 20);
                                guiRectT3 = new Rect(screenPos.x, Screen.height - screenPos.y + 20, 100, 20);
                                guiRectCh = new Rect(screenPos.x + 50, Screen.height - screenPos.y + 20, 15, 20);

                                //Buttons
                                guiRectB1 = new Rect(screenPos.x, Screen.height - screenPos.y + 40, 100, 20);
                                guiRectB2 = new Rect(screenPos.x, Screen.height - screenPos.y + 60, 100, 20);

                                //Sliders
                                sliderRectx = new Rect(screenPos.x, Screen.height - screenPos.y + 80, 100, 15);
                                sliderRecty = new Rect(screenPos.x, Screen.height - screenPos.y + 95, 200, 15);
                                sliderRectz = new Rect(screenPos.x, Screen.height - screenPos.y + 110, 200, 15);
                                lastObject = obj.gameObject;
                            }
                    }
                    //obj.position = prefPoint;
                }
                havePos = false;
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
                Expansion_Toggle.gotoAndStop(2);
                print(expandToggled);
            }
            else if (expandToggled == true)
            {
                expandToggled = false;
                Expansion_Toggle.gotoAndStop(1);
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
        private void Agent06Handler(CEvent e)
        {
            if (lastObject != null)
            {
                RequiredFireAgent = "06";
                print(RequiredFireAgent);
            }
        }

        private void Agent05Handler(CEvent e)
        {
            if (lastObject != null)
            {
                RequiredFireAgent = "05";
                print(RequiredFireAgent);
            }
        }

        private void Agent04Handler(CEvent e)
        {
            if (lastObject != null)
            {
                RequiredFireAgent = "04";
                print(RequiredFireAgent);
            }
        }

        private void Agent03Handler(CEvent e)
        {
            if (lastObject != null)
            {
                RequiredFireAgent = "03";
                print(RequiredFireAgent);
            }
        }

        private void Agent02Handler(CEvent e)
        {
            if (lastObject != null)
            {
                RequiredFireAgent = "02";
                print(RequiredFireAgent);
            }
        }

        private void Agent01Handler(CEvent e)
        {
            if (lastObject != null)
            {
                RequiredFireAgent = "01";
                print(RequiredFireAgent);
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
        void OnGUI()
        {
            Event e = Event.current;
            if (menuOn)
            {
                if(oldX < 0 && oldY > 0)
                {
                    if (Property_Menu.parent != stage)
                    {
                        print("Upper Left");
                        stage.addChild(Property_Menu);
                        Property_Menu.x = Screen.width - (Screen.width / 2 / 2);
                        Property_Menu.y = Screen.height - (Screen.height / 2 / 2);
                        Property_Menu.gotoAndStop(1);
                        rec = new Rect(Screen.height - (Screen.height / 2 / 2) - 300, Screen.height - (Screen.height / 2 / 2) - 160f, 600, 320);
                    }
                }
                if (oldX > 0 && oldY > 0)
                {
                    if (Property_Menu.parent != stage)
                    {
                        print("Upper Right");
                        stage.addChild(Property_Menu);
                        Property_Menu.x = Screen.width / 2 / 2;
                        Property_Menu.y = Screen.height - (Screen.height / 2 / 2);
                        Property_Menu.gotoAndStop(1);
                        rec = new Rect(Screen.width / 2 / 2 - 300, Screen.height - (Screen.height / 2 / 2) - 160, 640, 320);
                    }
                }
                if (oldX < 0 && oldY < 0)
                {
                    if (Property_Menu.parent != stage)
                    {
                        print("Lower Left");
                        stage.addChild(Property_Menu);
                        Property_Menu.x = Screen.width - (Screen.width / 2 / 2);
                        Property_Menu.y = Screen.height / 2 / 2;
                        Property_Menu.gotoAndStop(1);
                        rec = new Rect(Screen.width - (Screen.width / 2 / 2) - 300, Screen.height / 2 / 2 - 160, 640, 320);
                    }
                }
                if (oldX > 0 && oldY < 0)
                {
                    if (Property_Menu.parent != stage)
                    {
                        print("Lower Right");
                        stage.addChild(Property_Menu);
                        Property_Menu.x = Screen.width / 2 / 2;
                        Property_Menu.y = Screen.height / 2 / 2;
                        Property_Menu.gotoAndStop(1);
                        rec = new Rect(Screen.width / 2 / 2 - 300, Screen.height / 2 / 2 - 160, 640, 320);
                    }
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!rec.Contains(e.mousePosition))
                {
                    menuOn = false;
                    stage.removeChild(Property_Menu);
                }
            }
        }
        void addGotoAndStop(MovieClip a)
        {
            a.gotoAndStop(1);
        }
    }
}
