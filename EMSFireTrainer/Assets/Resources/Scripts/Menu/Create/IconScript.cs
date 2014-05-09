using UnityEngine;
using System.Collections;

namespace BlusSimulator
{
    public class IconScript : MonoBehaviour
    {
        private Camera camera;
        GameObject Flame;
        GameObject FlameHolder;
        // Use this for initialization
        void Start()
        {
            camera = GameObject.Find("Main Camera").camera;
            
            Flame = Resources.Load("Prefabs/Vuurtje") as GameObject;
            FlameHolder = GameObject.Find("GameManager");
        }

        // Update is called once per frame
        void Update()
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            this.transform.position = new Vector3(ray.origin.x, ray.origin.y, 1);

            if (Input.GetMouseButtonUp(0))
            {
                if (this.gameObject.tag == "FireIcon")
                {
                    FlameHolder = (GameObject)Instantiate(Flame, new Vector3(ray.origin.x, ray.origin.y, -1), Quaternion.identity);
                    FlameHolder.transform.parent = GameObject.Find("FireHolder").transform;
                    FlameHolder.name = "Fire" + FlameHolder.transform.parent.childCount;
                }
                if (this.gameObject.tag == "SmokeIcon")
                {

                }
                if (this.gameObject.tag == "OtherIcon")
                {

                }
                Destroy(this.gameObject);
            }
            
            
        }
    }
}
