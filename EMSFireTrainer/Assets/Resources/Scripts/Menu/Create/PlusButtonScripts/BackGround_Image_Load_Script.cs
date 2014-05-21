using UnityEngine;
using System.Collections;

public class BackGround_Image_Load_Script : MonoBehaviour {
    private Sprite[] Camera_Button_Sprite;
    private Sprite[] Browser_Button_Sprite;

    private SpriteRenderer Camera_Button_Renderer;
    private SpriteRenderer Browser_Button_Renderer;

    private GameObject Camera_Button_Object;
    private GameObject Browser_Button_Object;
    private GameObject ButtonHolder;

    private Rect CameraRect;
    private Rect BrowserRect;

    private Camera camera;

    private UnitytoJava unityToJava;
    void Awake()
    {
        camera = GameObject.Find("Main Camera").camera;
        float pixelRatio = (camera.orthographicSize * 2) / camera.pixelHeight;
        ButtonHolder = new GameObject("SelectImageObject");
        ButtonHolder.transform.position = new Vector3(0,0,0);

        Camera_Button_Object = new GameObject("Camera_Button");
        Camera_Button_Object.transform.parent = ButtonHolder.transform;
        Camera_Button_Sprite = Resources.LoadAll<Sprite>("Sprites/Buttons/CameraButton");
        Camera_Button_Renderer = Camera_Button_Object.AddComponent<SpriteRenderer>();
        Camera_Button_Renderer.sprite = Camera_Button_Sprite[0];
        Camera_Button_Object.transform.position = new Vector3(ButtonHolder.transform.position.x - Camera_Button_Renderer.sprite.bounds.size.x, 0, 0);
        Camera_Button_Object.AddComponent<BoxCollider2D>();

        Browser_Button_Object = new GameObject("Browser_Button");
        Browser_Button_Object.transform.parent = ButtonHolder.transform;
        Browser_Button_Sprite = Resources.LoadAll<Sprite>("Sprites/Buttons/GalleryButton");
        Browser_Button_Renderer = Browser_Button_Object.AddComponent<SpriteRenderer>();
        Browser_Button_Renderer.sprite = Browser_Button_Sprite[0];
        Browser_Button_Object.transform.position = new Vector3(ButtonHolder.transform.position.x + Browser_Button_Renderer.sprite.bounds.size.x, 0, 0);
        Browser_Button_Object.AddComponent<BoxCollider2D>();

        unityToJava = new UnitytoJava();
	}

	void Update ()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.transform == Camera_Button_Object.transform)
        {
            print("Inside Camera Button");
            Camera_Button_Renderer.sprite = Camera_Button_Sprite[1];
        }
        else
        {
            Camera_Button_Renderer.sprite = Camera_Button_Sprite[0];
        }
        if (hit.transform == Browser_Button_Object.transform)
        {
            print("Inside Browser Button");
            Browser_Button_Renderer.sprite = Browser_Button_Sprite[1];
        }
        else
        {
            Browser_Button_Renderer.sprite = Browser_Button_Sprite[0];
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (hit.transform == Camera_Button_Object.transform)
            {
                unityToJava.openCameraApp();
            }
            if (hit.transform == Browser_Button_Object.transform)
            {
                unityToJava.openImageBrowser();
            }
        }
	}

    public void DestroyMe()
    {
        Destroy(ButtonHolder);
    }
}