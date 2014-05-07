using UnityEngine;
using System.Collections;
using System.IO;

public class UnitytoJava : MonoBehaviour
{

    private AndroidJavaClass jc;
    private AndroidJavaObject jo;
    private GameObject bg;
    private Texture2D bgTexture;

	// Use this for initialization
	void Start () {
        bg = GameObject.Find("BackGround");
	}
	
    public void receive(string message)
    {
        Debug.Log("On recieve message: getURL");
        Debug.Log(message);
        File.Move(message, Application.persistentDataPath + "/" + Path.GetFileName(message));
        bgTexture.LoadImage(File.ReadAllBytes(Application.persistentDataPath + "/" + Path.GetFileName(message)));
        bg.transform.renderer.material.mainTexture = bgTexture;
    }
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 200, 150), "SelectImage"))
        {
            //Create new java class instance and activity
            //Call function from MyPlugin
            jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            jo.Call("getImageLoc");
        }

    }
}
