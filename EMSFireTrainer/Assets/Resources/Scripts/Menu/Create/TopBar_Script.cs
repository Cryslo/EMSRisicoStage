using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBar_Script : MonoBehaviour
{
    private GameObject topBar_Holder;
    private Create_Bars_fire_Script topBar_Script;
    private Camera camera;
    private Rect scrollArea;
    private int currentPage = 0;
    private float startpointX;
    private float offset;

    private float startX;

    private float absoluteMinX;
    private float absoluteMaxX;
    // Use this for initialization
    void Start()
    {
        camera = GameObject.Find("Main Camera").camera;
    }

    void Update()
    {
        //Topbar raycast to check if you're clicking it
        if (topBar_Script != null)
        {
            Vector3 hP = topBar_Holder.transform.position;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (scrollArea.Contains(ray.origin))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startpointX = ray.origin.x;
                    startX = topBar_Holder.transform.position.x;
                    offset = startX - startpointX;
                }
                if (Input.GetMouseButton(0))
                {
                    Vector3 v = new Vector3(startX - (startpointX - ray.origin.x), topBar_Holder.transform.position.y, topBar_Holder.transform.position.z);
                    if (v.x >= absoluteMinX && v.x <= absoluteMaxX)
                    {
                        topBar_Holder.transform.position = v;
                    }
                    else if (v.x <= absoluteMinX && Input.GetAxis("Mouse X") < 0)
                    {
                        v = new Vector3(absoluteMinX, hP.y, hP.z);
                    }
                    else if (v.x >= absoluteMaxX && Input.GetAxis("Mouse X") > 0)
                    {
                        v = new Vector3(absoluteMaxX, hP.y, hP.z);
                    }
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (hP.x < absoluteMinX)
                    {
                        topBar_Holder.transform.position = new Vector3(absoluteMinX, hP.y, hP.z);
                    }
                    else if (hP.x > absoluteMaxX)
                    {
                        topBar_Holder.transform.position = new Vector3(absoluteMaxX, hP.y, hP.z);
                    }
                }
            }
        }
    }

    public void createTopBar()
    {
        if (topBar_Script == null)
        {
            topBar_Holder = new GameObject("TopBar_Holder");
            int number = 8;
            for (int i = 0; i < number; i++)
            {
                topBar_Script = new Create_Bars_fire_Script();
                topBar_Script.AddBar(camera.ScreenToWorldPoint(new Vector3(i * Screen.width / 4, camera.pixelHeight, 10)), i, topBar_Holder, "NEIN");
            }
            float pixelRatio = (camera.orthographicSize * 2) / camera.pixelHeight;
            SpriteRenderer a = GameObject.Find("TopBar_0").GetComponent<SpriteRenderer>();
            scrollArea = new Rect(0, 0, Screen.width, a.sprite.bounds.size.y / pixelRatio);
            absoluteMinX = topBar_Holder.transform.position.x - (a.sprite.bounds.size.x * 4 * a.gameObject.transform.localScale.x);
            absoluteMaxX = topBar_Holder.transform.position.x;
        }
    }

    public void deleteTopBar()
    {
        Destroy(topBar_Holder);
        scrollArea = new Rect();
        topBar_Script = null;
    }

    void OnGUI()
    {
        if (topBar_Script != null)
        {
            GUI.Box(scrollArea, "Collider");
        }
    }

}
