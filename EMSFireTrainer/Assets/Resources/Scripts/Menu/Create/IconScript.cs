using UnityEngine;
using System.Collections;

namespace BlusSimulator
{
    public class IconScript : MonoBehaviour
    {
        private Camera camera;
        GameObject Flame;
        GameObject FlameHolder;
        GameObject flameSpawner;
        // Use this for initialization
        void Start()
        {
            camera = GameObject.Find("Main Camera").camera;

            FlameHolder = new GameObject("FlameHolder");
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider.tag == "FireIcon")
                {
                    Flame = Resources.Load("Prefabs/Scene_1_Prefabs/fireSprite_02") as GameObject;
                    flameSpawner = (GameObject)Instantiate(Flame, new Vector3(ray.origin.x, ray.origin.y, -1), Quaternion.identity);
                    flameSpawner.transform.parent = FlameHolder.transform;
                    flameSpawner.name = "Fire" + FlameHolder.transform.parent.childCount;
                }
            }
            
            
        }
    }
}
