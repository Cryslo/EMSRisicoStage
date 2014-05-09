using UnityEngine;
using System.Collections;
using pumpkin.display;
using pumpkin.events;
using System.IO;



namespace BlusSimulator
{
    public class Create_Object_Drag_Script : MonoBehaviour
    {
        Stage stage;
        MovieClip ObjectHolder;
        MovieClip Fire_Icon;
        MovieClip Smoke_Icon;
        MovieClip Other_Icon;
        MovieClip Fire01;
        MovieClip Fire02;
        MovieClip Fire03;
        MovieClip Fire04;
        MovieClip Fire05;
        MovieClip Smoke01;
        MovieClip Smoke02;
        MovieClip Smoke03;
        MovieClip Smoke04;
        MovieClip Smoke05;
        MovieClip Other01;
        MovieClip Other02;
        MovieClip Other03;
        MovieClip Other04;
        MovieClip Other05;
        Camera camera;
        Transform obj;
        Vector3 offset;
        Vector3 prefPoint;
        GameObject FireIcon;
        GameObject SmokeIcon;
        GameObject OtherIcon;
        GameObject IconHolder;

        GameObject selectedFire;

        float tempx;
        float tempy;

        Vector2 mPos;

        // Use this for initialization
        private bool isStepOne;
        public bool IsStepOne
        {
            get
            {
                return isStepOne;
            }
            set
            {
                isStepOne = value;
            }
        }
        private void Awake()
        {
        }
        public void Create_ObjectMenu()
        {
            camera = GameObject.Find("Main Camera").camera;
            stage = MovieClipOverlayCameraBehaviour.instance.stage;
                print("Creating Menu");
                ObjectHolder = new MovieClip("Resources/SWFs/Menu/Object_Scene_Holder.swf:Object_Scene_Holder2");
                ObjectHolder.x = UnityEngine.Screen.width - (ObjectHolder.width / 2);
                ObjectHolder.y = UnityEngine.Screen.height - (ObjectHolder.height / 2) - 20;
                ObjectHolder.gotoAndStop(1);
                SmokeIcon = Resources.Load("Prefabs/SmokeIconPref") as GameObject;
                FireIcon = Resources.Load("Prefabs/FireIconPref") as GameObject;
                OtherIcon = Resources.Load("Prefabs/OtherIconPref") as GameObject;
                stage.addChild(ObjectHolder);
                AddButtons();
        }
        public void Remove_ObjectMenu()
        {
            stage.removeChild(ObjectHolder);
            print("Removed CreateMenu");
        }

        private void AddButtons()
        {
            Fire01 = ObjectHolder.getChildByName("Fire01") as MovieClip;
            Fire02 = ObjectHolder.getChildByName("Fire02") as MovieClip;
            Fire03 = ObjectHolder.getChildByName("Fire03") as MovieClip;
            Fire04 = ObjectHolder.getChildByName("Fire04") as MovieClip;
            Fire05 = ObjectHolder.getChildByName("Fire05") as MovieClip;
            Fire01.addEventListener(MouseEvent.MOUSE_DOWN, Fire01Handler);
            Fire02.addEventListener(MouseEvent.MOUSE_DOWN, Fire02Handler);
            Fire03.addEventListener(MouseEvent.MOUSE_DOWN, Fire03Handler);
            Fire04.addEventListener(MouseEvent.MOUSE_DOWN, Fire04Handler);
            Fire05.addEventListener(MouseEvent.MOUSE_DOWN, Fire05Handler);
            Fire01.addEventListener(MouseEvent.MOUSE_UP, Fire01HandlerUp);
            Fire02.addEventListener(MouseEvent.MOUSE_UP, Fire02HandlerUp);
            Fire03.addEventListener(MouseEvent.MOUSE_UP, Fire03HandlerUp);
            Fire04.addEventListener(MouseEvent.MOUSE_UP, Fire04HandlerUp);
            Fire05.addEventListener(MouseEvent.MOUSE_UP, Fire05HandlerUp);
            print("FireX: " + Fire01.x);
            print("FireY: " + Fire01.y);

        }

        private void Fire05HandlerUp(CEvent e)
        {
        }

        private void Fire04HandlerUp(CEvent e)
        {
        }

        private void Fire03HandlerUp(CEvent e)
        {
        }

        private void Fire02HandlerUp(CEvent e)
        {
        }

        private void Fire01HandlerUp(CEvent e)
        {
            Destroy(IconHolder);
        }

        private void Fire05Handler(CEvent MouseEvent)
        {
            IconHolder = (GameObject)Instantiate(FireIcon, new Vector3(0, 0, 0), Quaternion.identity);
            IconHolder.transform.parent = GameObject.Find("FireHolder").transform;
            IconHolder.name = "Fire" + IconHolder.transform.parent.childCount;
        }

        private void Fire04Handler(CEvent MouseEvent)
        {
            IconHolder = (GameObject)Instantiate(FireIcon, new Vector3(0, 0, 0), Quaternion.identity);
        }

        private void Fire03Handler(CEvent MouseEvent)
        {
            IconHolder = (GameObject)Instantiate(FireIcon, new Vector3(0, 0, 0), Quaternion.identity);
        }

        private void Fire02Handler(CEvent MouseEvent)
        {
            IconHolder = (GameObject)Instantiate(FireIcon, new Vector3(0, 0, 0), Quaternion.identity);
        }

        private void Fire01Handler(CEvent MouseEvent)
        {
            IconHolder = (GameObject)Instantiate(FireIcon, new Vector3(0, 0, 0), Quaternion.identity);
        }

        private void Dragging(Transform obj)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            
            if (obj)
            {
                obj.position = new Vector3(ray.origin.x + offset.x, ray.origin.y + offset.y, obj.position.z);
            }
        }

        void Update()
        {
        }


    }
}
