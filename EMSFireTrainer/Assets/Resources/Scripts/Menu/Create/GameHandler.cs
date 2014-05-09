using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BlusSimulator
{
    public class GameHandler : MonoBehaviour
    {
        public static int audio;
        public static bool timerOn;
        public static bool timerVisible;
        public static int sceneLength;
        public static float agentAmount;
        public static string bgName;

        public static string SceneName;
        public static string folderPath;
        public static string name;
        public static List<GameObject> ObjectList;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update() 
        {

	    }
        /*static void saveAll()
        {
            ObjectList = new List<GameObject>();
            for (int i = 0; i < GameObject.Find("FireHolder").transform.childCount; i++)
            {
                GameObject a = GameObject.Find("FireHolder").transform.GetChild(i).gameObject;
                ObjectList.Add(a);;
                XmlBehaviour.SaveObject(a, a.GetComponent<FlameScript>().canGrow, a.GetComponent<FlameScript>().RequiredFireAgent);
            }

            XmlBehaviour.SaveScene(folderPath, name, timerOn, sceneLength, timerVisible, audio, bgName);
        }*/
    }
}
