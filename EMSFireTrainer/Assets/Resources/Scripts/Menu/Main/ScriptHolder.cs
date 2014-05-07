using UnityEngine;
using System.Collections;
using System.IO;

public class ScriptHolder : MonoBehaviour
{

    private AndroidJavaClass jc;
    private AndroidJavaObject jo;
    private string Url;
    private string prefUrl;
    private GameObject menuHolder;
    private GameObject vuforiaHolder;
    private GameObject bg;
    private string imagePicked;
    private string imagePickerStarted;
    private Texture2D bgTexture;
    private string tempUrl;

	// Use this for initialization
	void Start () {
        bg = GameObject.Find("BackGround");
	}
	
	// Update is called once per frame
    void Update()
    {
    }
    private string returnStringNow()
    {
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        return jo.CallStatic<string>("returnImageLoc");
    }
    private string returnImageNow()
    {
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        return jo.CallStatic<string>("returnImagePicker");
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 200, 150), "SelectImage"))
        {
            jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            //Create new java class instance and activity
            //Call function from MyPlugin
           jo.Call("getImageLoc");
        }
        if (GUI.Button(new Rect(200, 0, 200, 150), "SelectImage"))
        {
            jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            string b = jo.CallStatic<string>("returnImageLoc");
            jo.Call("showMessage", b);
            File.Move(b, Application.persistentDataPath + "/" + "PETER.jpg");
        }
        if (GUI.Button(new Rect(400, 0, 200, 150), "SelectImage"))
        {
            bgTexture = new Texture2D(0, 0);
            bgTexture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + "/" + "PETER.jpg"));
            bg.renderer.material.mainTexture = bgTexture;
            tempUrl = "";
        }

    }
    public void copyImage()
    {
        print("Moving Boss");
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        string b = jo.CallStatic<string>("returnImageLoc");
        //string a = message;
        File.Move(b, Application.persistentDataPath + "/" + Path.GetFileName(b)); 
        //jo.Call("showMessage", a);

        //Call function from MyPlugin
    }
}
