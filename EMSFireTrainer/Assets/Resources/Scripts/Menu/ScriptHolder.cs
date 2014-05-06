using UnityEngine;
using System.Collections;
using System.IO;

public class ScriptHolder : MonoBehaviour
{

    private AndroidJavaClass jc;
    private AndroidJavaObject jo;
    private string Url;
    private string prefUrl;
    private string GameState;
    private GameObject menuHolder;
    private GameObject vuforiaHolder;

	// Use this for initialization
	void Start () {
        GameState = "Menu";
	}
	
	// Update is called once per frame
    void Update()
    {
    }
    public void receive(string message)
    {
        Debug.Log("On recieve message: getURL");
        Debug.Log(message);
        Url = message;
    }
    private string returnStringNow()
    {
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        return jo.CallStatic<string>("returnImageLoc");
    }
    private void OnGUI()
    {
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        if (GUI.Button(new Rect(0, 0, 200, 150), "SelectImage"))
        {
            //Create new java class instance and activity
            //Call function from MyPlugin
            jo.Call("getImageLoc");
        }
        if (GUI.Button(new Rect(200, 0, 200, 150), "SelectImage"))
        {
            //Create new java class instance and activity
            //Call function from MyPlugin
                string b = jo.CallStatic<string>("returnImageLoc");
                //jo.Call("showMessage", b);
        }

    }
    public void copyImage(string message)
    {
        print("Moving Boss");
        //Create new java class instance and activity
        //jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        string a = message;
        //jo.Call("showMessage", a);

        File.Move(a, Application.persistentDataPath + "/" + Path.GetFileName(a)); 
        //Call function from MyPlugin
    }
}
