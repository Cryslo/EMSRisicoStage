using UnityEngine;
using System.Collections;

public class Fire_Property_Menu : MonoBehaviour {
    private GameObject topBar_Holder;
    private TopBar_Script topBar_Script;
    private Camera camera;

	// Use this for initialization
	void Start () {
        topBar_Holder = new GameObject("TopBar_Holder");
        camera = GameObject.Find("Main Camera").camera;
	}

    public void createTopBar()
    {
        if (topBar_Script == null)
        {
            int number = 4;
            for (int i = 0; i < number; i++)
            {
                topBar_Script = new TopBar_Script();
                topBar_Script.AddBar(camera.ScreenToWorldPoint(new Vector3(i * Screen.width / 4, camera.pixelHeight, 10)), 0, topBar_Holder);
            }
        }
    }

}
