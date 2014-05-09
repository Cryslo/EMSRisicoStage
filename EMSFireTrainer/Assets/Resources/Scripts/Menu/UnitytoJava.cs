using UnityEngine;
using System.Collections;
using System.IO;

public class UnitytoJava : MonoBehaviour
{
	#if UNITY_ANDROID
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

	// Use this for initialization
	void Start () {
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
        print(dataPath + "/" + name);
        image = dataPath + "/" + name;
        
        if (System.IO.File.Exists(image))
        {
            //Set image as background texture
            bgTexture.LoadImage(File.ReadAllBytes(image));
            bg.renderer.material.mainTexture = bgTexture;
        }
    }
    private void OnGUI()
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
    }
#endif
}
