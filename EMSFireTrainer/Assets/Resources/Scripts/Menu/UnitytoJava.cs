using UnityEngine;
using System.Collections;
using System.IO;

public class UnitytoJava
{
    //#if UNITY_ANDROID
    private AndroidJavaClass jc;
    private AndroidJavaObject jo;
    private GameObject bg;
    private Texture2D bgTexture;
    private string dataPath;
    private string image;
    private string name;
    private bool usingCamera = false;

    public string deviceName;
    private WebCamTexture wct;

    private BackGround_Image_Load_Script BGILscript;

    // Use this for initialization
    void Start()
    {
        BGILscript = GameObject.Find("CreateMenuScriptHolder").GetComponent<BackGround_Image_Load_Script>();
        bgTexture = new Texture2D(1, 1);
        bg = GameObject.Find("BackGround");
        dataPath = Application.persistentDataPath;
        WebCamDevice[] devices = WebCamTexture.devices;
        deviceName = devices[0].name;
    }
    //Receive the image from JAVA from either the Camera or Image browser
    public void receive(string message)
    {
        name = Path.GetFileName(message);
        //Copy image to app data location
        File.Copy(message, dataPath + "/" + name, true);
        Debug.Log(dataPath + "/" + name);
        image = dataPath + "/" + name;

        if (System.IO.File.Exists(image))
        {
            //Set image as background texture
            bgTexture.LoadImage(File.ReadAllBytes(image));
            bg.renderer.material.mainTexture = bgTexture;
        }
    }

    public void openImageBrowser()
    {
        //Create new java class instance and activity
        //Call function from MyPlugin
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("getImageLoc");
    }

    public void openCameraApp()
    {
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("openCamera");
    }

    /*private void OnGUI()
    {
            if (GUI.Button(new Rect(0, 0, 200, 150), "Select Image from Browser"))
            {
                //Create new java class instance and activity
                //Call function from MyPlugin
                jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                jo.Call("getImageLoc");
            }

            if (GUI.Button(new Rect(200, 0, 200, 150), "Take a Picture!"))
            {
                jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                jo.Call("openCamera");

            }
    }*/
    //#endif
}
