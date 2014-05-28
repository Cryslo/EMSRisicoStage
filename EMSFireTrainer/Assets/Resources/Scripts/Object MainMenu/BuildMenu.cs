using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System;

public class BuildMenu : MonoBehaviour
{
    private static Camera mainCamera;

    private FolderBuilder folderBuilder;

    private static Font menuFont;
    private static int fontSize;

    private static List<GameObject> Objects;

    public static GameObject background;
    public static GUITexture backgroundTexture;
    public static Texture2D backgroundImage;

    private static GameObject connectButton;
    private static GUIText connectButtonText;
    private static Rect connectButtonRect;

    private static GameObject createButton;
    private static GUIText createButtonText;
    private static Rect createButtonRect;

    private static GameObject playButton;
    private static GUIText playButtonText;
    private static Rect playButtonRect;

    private static GameObject smokeButton;
    private static GUIText smokeButtonText;
    private static Rect smokeButtonRect;

    private static GameObject product;
    private static GUIText productName;
    private static GameObject company;
    private static GUIText companyName;
    //TODO:Init and place an logo on screen
    private static GameObject companylogo;
    private static GUITexture companyTexture;
    private static Texture2D companyIcon;

    private static GameObject homeButton;
    private static GUITexture homeTexture;
    private static Texture2D homeIcon;
    private static Rect homeIconRect;

    private static GameObject createMenuScriptHolder;
    private static GameObject createdSceneObjectHolder;

    #region Create buttons
    private static int createButtonWidth = 76;
    private static int createButtonHeight = 86;

    private static GameObject settingsIcon;
    private static Sprite settingsIconSprite;
    private static SpriteRenderer settingsIconRenderer;
    private static GUIText settingsTitle;
    private static GameObject settingsTitleObject;
    private static BoxCollider2D settingsCollider;
    private static Rect settingsTextRect;

    private static GameObject fireIcon;
    private static Sprite fireIconSprite;
    private static SpriteRenderer fireIconRenderer;
    private static GUIText fireTitle;
    private static GameObject fireTitleObject;
    private static BoxCollider2D fireCollider;
    private static Rect fireTextRect;

    private static GameObject plusIcon;
    private static Sprite plusIconSprite;
    private static SpriteRenderer plusIconRenderer;
    private static GUIText plusTitle;
    private static GameObject plusTitleObject;
    private static BoxCollider2D plusCollider;
    private static Rect plusTextRect;

    private static GameObject saveIcon;
    private static Sprite saveIconSprite;
    private static SpriteRenderer saveIconRenderer;
    private static GUIText saveTitle;
    private static GameObject saveTitleObject;
    private static BoxCollider2D saveCollider;
    private static Rect saveTextRect;

    private static GameObject backIcon;
    private static Sprite backIconSprite;
    private static SpriteRenderer backIconRenderer;
    private static GameObject backTitleObject;
    private static GUIText backTitle;
    private static BoxCollider2D backCollider;

    private static bool textClicked = false;

    private static Camera camera;


    public enum SwipeDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    public static event Action<SwipeDirection> Swipe;
    private static bool swiping = false;
    private static bool eventSent = false;
    private static Vector2 lastPosition;
    private static SwipeDirection swipeDirection;

    #endregion

    private static float nativeWidth = 1280;
    private static float nativeHeight = 720;
    private static Matrix4x4 test;

    public Vector3 test1;
    public Rect test2;

    public static float screenDPI;
    public static Rect leftScreenRect;
    public static Rect rightScreenRect;
    public static Rect bottomScreenRect;

    void Awake()
    {
        screenDPI = Screen.dpi / 160;
        folderBuilder = gameObject.GetComponent<FolderBuilder>();
        camera = GameObject.Find("Main Camera").camera;
        Objects = new List<GameObject>();

        //Init Font
        menuFont = Resources.Load("Fonts/HelveticaNeue") as Font;
        //set standard font size
        fontSize = 90;

        //Init Textures
        homeIcon = Resources.Load("Icons/Play_Icons/previous-36") as Texture2D;
        companyIcon = Resources.Load("Icons/Company_Icons/ISCRisk-36") as Texture2D;
        backgroundImage = Resources.Load("Backgrounds/MenuBackgrounds/MenuBackgrounds/Orange") as Texture2D;
        settingsIconSprite = Resources.Load("Icons/Create_Icons/SettingsIcon") as Sprite;
        fireIconSprite = Resources.Load("Icons/Create_Icons/FireIcon") as Sprite;
        plusIconSprite = Resources.Load("Icons/Create_Icons/PlusIcon") as Sprite;
        saveIconSprite = Resources.Load("Icons/Create_Icons/SaveIcon") as Sprite;
        backIconSprite = Resources.Load("Icons/Create_Icons/CrossIcon") as Sprite;


        background = new GameObject();
        background.isStatic = true;
        background.layer = 8;
        background.name = "Background";
        background.AddComponent<GUITexture>();
        backgroundTexture = background.GetComponent<GUITexture>();
        backgroundTexture.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
        backgroundTexture.texture = backgroundImage;

    }

    void Start()
    {

        mainCamera = GameObject.Find("Main Camera").camera;
        //Screen.SetResolution(1280,720,true);
        Vector3 scale = new Vector3(nativeWidth, nativeHeight, 1.0f);
        // test = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
    }

    void Update()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        Vector3 mouseStoWoldPos = camera.ScreenToWorldPoint(Input.mousePosition);
        #region MainMenu
        if (GameManager.getGameState == GameState.MainMenu)
        {
            if (connectButtonRect.Contains(mousePos))
            {
                connectButtonText.color = Color.black;
                connectButtonText.fontSize = 70;
                if (Input.GetMouseButtonUp(0))
                {
                    GameManager.SetGameState(GameState.ConnectMenu);
                }
            }
            else if (GameManager.getGameState != GameState.ConnectMenu)
            {
                connectButtonText.color = Color.white;
                connectButtonText.fontSize = fontSize;
            }

            if (createButtonRect.Contains(mousePos))
            {
                createButtonText.color = Color.black;
                createButtonText.fontSize = 70;
                if (Input.GetMouseButtonUp(0))
                {
                    GameManager.SetGameState(GameState.CreateMenu);
                }
            }
            else if (GameManager.getGameState != GameState.CreateMenu)
            {
                createButtonText.color = Color.white;
                createButtonText.fontSize = fontSize;
            }

            if (playButtonRect.Contains(mousePos))
            {
                playButtonText.color = Color.black;
                playButtonText.fontSize = 70;
                playButtonText.anchor = TextAnchor.MiddleLeft;
                if (Input.GetMouseButtonUp(0))
                {
                    GameManager.SetGameState(GameState.PlayMenu);
                }
            }
            else if (GameManager.getGameState != GameState.PlayMenu)
            {
                playButtonText.color = Color.white;
                playButtonText.fontSize = fontSize;
            }

            if (smokeButtonRect.Contains(mousePos))
            {
                smokeButtonText.color = Color.black;
                smokeButtonText.fontSize = 70;
                //TODO: give smoke a gamestate
                if (Input.GetMouseButtonUp(0))
                {
                    GameManager.SetGameState(GameState.PlayMenu);
                }
            }
            else
            {
                smokeButtonText.color = Color.white;
                smokeButtonText.fontSize = fontSize;
            }
        }
        #endregion

        if (GameManager.getGameState == GameState.CreateMenu)
        {
            //Plus      
            //Find an object to attach the script to
            GameObject FPM = GameObject.Find("CreateMenuScriptHolder");

            RaycastHit2D hit = Physics2D.Raycast(mouseStoWoldPos, Vector2.zero);
            IconMenuBehaviour();

            if (plusTextRect.Contains(mousePos) && !textClicked)
            {
                plusTitle.fontSize = 70;
                if (Input.GetMouseButtonUp(0))
                {
                    FPM.GetComponent<TopBar_Script>().deleteTopBar();
                    if (!FPM.GetComponent<BackGround_Image_Load_Script>())
                    {
                        FPM.AddComponent<BackGround_Image_Load_Script>();
                    }
                    DeleteText();
                    textClicked = true;
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu && !textClicked)
            {
                plusTitle.fontSize = fontSize;
            }

            if (hit.transform == plusIcon.transform)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    FPM.GetComponent<TopBar_Script>().deleteTopBar();
                    if (!FPM.GetComponent<BackGround_Image_Load_Script>())
                    {
                        FPM.AddComponent<BackGround_Image_Load_Script>();
                    }
                    DeleteText();
                    textClicked = true;
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu)
            {

            }
            //Fire
            if (fireTextRect.Contains(mousePos) && !textClicked)
            {
                fireTitle.fontSize = 70;
                if (Input.GetMouseButtonUp(0))
                {
                    //Create the Top Bar for fire selection
                    FPM.GetComponent<TopBar_Script>().createTopBar();

                    //Remove the Image Selector if the user is coming from the ImageBrowse Window
                    if (FPM.GetComponent<BackGround_Image_Load_Script>())
                    {
                        FPM.GetComponent<BackGround_Image_Load_Script>().DestroyMe();
                        Destroy(FPM.GetComponent<BackGround_Image_Load_Script>());
                    }
                    DeleteText();
                    textClicked = true;
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu && !textClicked)
            {
                fireTitle.fontSize = fontSize;
            }

            if (hit.transform == fireIcon.transform)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    //Create the Top Bar for fire selection
                    FPM.GetComponent<TopBar_Script>().createTopBar();

                    //Remove the Image Selector if the user is coming from the ImageBrowse Window
                    if (FPM.GetComponent<BackGround_Image_Load_Script>())
                    {
                        FPM.GetComponent<BackGround_Image_Load_Script>().DestroyMe();
                        Destroy(FPM.GetComponent<BackGround_Image_Load_Script>());
                    }
                    DeleteText();
                    textClicked = true;
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu)
            {
            }
            //Settings
            if (settingsTextRect.Contains(mousePos) && !textClicked)
            {
                settingsTitle.fontSize = 70;
                if (Input.GetMouseButtonUp(0))
                {
                    FPM.GetComponent<TopBar_Script>().deleteTopBar();

                    //Remove the Image Selector if the user is coming from the ImageBrowse Window
                    if (FPM.GetComponent<BackGround_Image_Load_Script>())
                    {
                        FPM.GetComponent<BackGround_Image_Load_Script>().DestroyMe();
                        Destroy(FPM.GetComponent<BackGround_Image_Load_Script>());
                    }
                    DeleteText();
                    textClicked = true;
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu && !textClicked)
            {
                settingsTitle.fontSize = fontSize;
            }

            if (hit.transform == settingsIcon.transform)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    FPM.GetComponent<TopBar_Script>().deleteTopBar();

                    //Remove the Image Selector if the user is coming from the ImageBrowse Window
                    if (FPM.GetComponent<BackGround_Image_Load_Script>())
                    {
                        FPM.GetComponent<BackGround_Image_Load_Script>().DestroyMe();
                        Destroy(FPM.GetComponent<BackGround_Image_Load_Script>());
                    }
                    DeleteText();
                    textClicked = true;
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu)
            {
            }
            //Save
            if (saveTextRect.Contains(mousePos) && !textClicked)
            {
                saveTitle.fontSize = 70;
                if (Input.GetMouseButtonUp(0))
                {
                    FPM.GetComponent<TopBar_Script>().deleteTopBar();

                    //Remove the Image Selector if the user is coming from the ImageBrowse Window
                    if (FPM.GetComponent<BackGround_Image_Load_Script>())
                    {
                        FPM.GetComponent<BackGround_Image_Load_Script>().DestroyMe();
                        Destroy(FPM.GetComponent<BackGround_Image_Load_Script>());
                    }
                    DeleteText();
                    textClicked = true;
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu && !textClicked)
            {
                saveTitle.fontSize = fontSize;
            }

            if (hit.transform == saveIcon.transform)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    FPM.GetComponent<TopBar_Script>().deleteTopBar();

                    //Remove the Image Selector if the user is coming from the ImageBrowse Window
                    if (FPM.GetComponent<BackGround_Image_Load_Script>())
                    {
                        FPM.GetComponent<BackGround_Image_Load_Script>().DestroyMe();
                        Destroy(FPM.GetComponent<BackGround_Image_Load_Script>());
                    }
                    DeleteText();
                    textClicked = true;
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu)
            {
            }
            //Quit
            if (hit.transform == backIcon.transform)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    FPM.GetComponent<TopBar_Script>().deleteTopBar();

                    //Remove the Image Selector if the user is coming from the ImageBrowse Window
                    if (FPM.GetComponent<BackGround_Image_Load_Script>())
                    {
                        FPM.GetComponent<BackGround_Image_Load_Script>().DestroyMe();
                        Destroy(FPM.GetComponent<BackGround_Image_Load_Script>());
                    }
                    textClicked = false;
                    GameManager.SetGameState(GameState.MainMenu);
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu)
            {
            }
        }

        if (GameManager.getGameState != GameState.MainMenu || GameManager.getGameState != GameState.CreateMenu)
        {
            if (homeIconRect.Contains(mousePos))
            {
                homeTexture.color = Color.black;
                if (Input.GetMouseButtonUp(0))
                {
                    GameManager.SetGameState(GameState.MainMenu);
                }
            }
            else if (GameManager.getGameState != GameState.MainMenu && GameManager.getGameState != GameState.CreateMenu)
            {
                homeTexture.color = Color.white;
            }
        }
    }

    private static void IconMenuBehaviour()
    {

        leftScreenRect = new Rect(0, 0, Screen.width * 0.1f, Screen.height);
        rightScreenRect = new Rect(Screen.width - (Screen.width * 0.1f), 0, Screen.width * 0.1f, Screen.height);
        bottomScreenRect = new Rect(0, 0, Screen.width, Screen.height * 0.2f);
        for (int i = 0; i < createdSceneObjectHolder.transform.childCount; i++)
        {
            if (bottomScreenRect.Contains(camera.WorldToScreenPoint(GameObject.Find("FireIcon_" + i).transform.position)))
            {
                //print("Bottom");
                if (!rightScreenRect.Contains(camera.WorldToScreenPoint(GameObject.Find("FireIcon_" + i).transform.position)))
                {
                    print("Right");
                }
                else if (rightScreenRect.Contains(camera.WorldToScreenPoint(GameObject.Find("FireIcon_" + i).transform.position)))
                {
                    if (!leftScreenRect.Contains(camera.WorldToScreenPoint(GameObject.Find("FireIcon_" + i).transform.position)))
                    {
                        print("Left");
                    }
                    else if (leftScreenRect.Contains(camera.WorldToScreenPoint(GameObject.Find("FireIcon_" + i).transform.position)))
                    {
                                print("Bottom");
                    }
                }
            }
        }
    }

    private static void DeleteText()
    {
        
        if (!textClicked)
        {
            List<GUIText> textList = new List<GUIText>();
            textList.AddMany(settingsTitle, fireTitle, plusTitle, saveTitle);
            foreach (GUIText item in textList)
            {
                Destroy(item);
            }
        }
    }

    void OnGUI()
    {
        //GUI.matrix = test;
        if (playButtonText != null)
        {
        }
        GUI.Box(leftScreenRect, "Left");
        GUI.Box(rightScreenRect, "Right");
        GUI.Box(new Rect(bottomScreenRect.x, new Vector2(0, Screen.height - bottomScreenRect.y).y, bottomScreenRect.width, -bottomScreenRect.height), "Bottom");
        //GUI.Box(smokeButtonRect, "Smoke");
        //GUI.Box(homeIconRect, "Home");
        /*GUI.Box(plusIconRect, "PlusIcon");
        GUI.Box(plusTextRect, "Plus");

        GUI.Box(settingsRect, "settingsIcon");
        GUI.Box(settingsTextRect, "settingsText");

        GUI.Box(fireIconRect, "fireIcon");
        GUI.Box(fireTextRect, "fireText");

        GUI.Box(saveIconRect, "saveIcon");
        GUI.Box(saveTextRect, "saveText");
        GUI.Box(backIconRect, "saveText");*/
    }

    private static void Deconstruct()
    {
        //Check the PreviousGameState and decide from that which he has to deconstuct

        switch (GameManager.PreviousGameState)
        {
            case GameState.MainMenu:
                DeconstructStartMenu();
                break;
            case GameState.PlayMenu:
                DeconstructPlayMenu();
                break;
            case GameState.CreateMenu:
                DeconstructCreateMenu();
                break;
            case GameState.ConnectMenu:
                DeconstructConnectMenu();
                break;
        }
        Objects = new List<GameObject>();
    }

    public static void BuildStartMenu()
    {
        Deconstruct();

        #region BuildMenu

        backgroundImage = Resources.Load("Backgrounds/MenuBackgrounds/Orange") as Texture2D;
        backgroundTexture.texture = backgroundImage;

        fontSize = 90;

        connectButton = new GameObject();
        createButton = new GameObject();
        playButton = new GameObject();
        smokeButton = new GameObject();

        product = new GameObject();
        company = new GameObject();
        companylogo = new GameObject();

        Objects.AddMany(connectButton, createButton, playButton, smokeButton, product, company, companylogo);

        //Give Buttons a Name
        connectButton.name = "Connect_Button";
        createButton.name = "Create_Button";
        playButton.name = "Play_Button";
        smokeButton.name = "Smoke_Button";
        product.name = "Product_Name";
        company.name = "Company_Name";
        companylogo.name = "Company_Logo";


        connectButton.AddComponent<GUIText>();
        createButton.AddComponent<GUIText>();
        playButton.AddComponent<GUIText>();
        smokeButton.AddComponent<GUIText>();
        product.AddComponent<GUIText>();
        company.AddComponent<GUIText>();
        companylogo.AddComponent<GUITexture>();


        connectButtonText = connectButton.GetComponent<GUIText>();
        createButtonText = createButton.GetComponent<GUIText>();
        playButtonText = playButton.GetComponent<GUIText>();
        smokeButtonText = smokeButton.GetComponent<GUIText>();
        productName = product.GetComponent<GUIText>();
        companyName = company.GetComponent<GUIText>();

        companyTexture = companylogo.GetComponent<GUITexture>();


        companyTexture.texture = companyIcon;

        //Give Components right values
        //Text
        connectButtonText.text = "CONNECT";
        createButtonText.text = "CREATE";
        playButtonText.text = "PLAY";
        smokeButtonText.text = "SMOKE";
        productName.text = "FIRE TRAINER";
        companyName.text = "ISC RISK";

        connectButton.transform.position = new Vector3(0, 1, 0);
        createButton.transform.position = new Vector3(0, 1, 0);
        playButton.transform.position = new Vector3(0, 1, 0);
        smokeButton.transform.position = new Vector3(0, 1, 0);
        product.transform.position = new Vector3(0, 1, 0);
        company.transform.position = new Vector3(0, 1, 0);


        //Posistion
        connectButtonText.pixelOffset = new Vector2(50, -(Screen.height - 340));
        createButtonText.pixelOffset = new Vector2(50, -(Screen.height - 270));
        playButtonText.pixelOffset = new Vector2(50, -(Screen.height - 202));
        smokeButtonText.pixelOffset = new Vector2(50, -(Screen.height - 135));
        productName.pixelOffset = new Vector2(Screen.width - 200, -50);
        companyName.pixelOffset = new Vector2(Screen.width - 200, -70);
        companyTexture.pixelInset = new Rect(Screen.width - 250, Screen.height - 90, 36, 36);

        //scale
        companylogo.transform.localScale = new Vector3(0, 0, 1);

        //anchor
        connectButtonText.anchor = TextAnchor.MiddleLeft;
        createButtonText.anchor = TextAnchor.MiddleLeft;
        playButtonText.anchor = TextAnchor.MiddleLeft;
        smokeButtonText.anchor = TextAnchor.MiddleLeft;
        productName.anchor = TextAnchor.MiddleLeft;
        companyName.anchor = TextAnchor.MiddleLeft;

        //Color
        connectButtonText.color = Color.white;
        createButtonText.color = Color.white;
        playButtonText.color = Color.white;
        smokeButtonText.color = Color.white;
        productName.color = Color.white;
        companyName.color = Color.white;

        //Font
        connectButtonText.font = menuFont;
        createButtonText.font = menuFont;
        playButtonText.font = menuFont;
        smokeButtonText.font = menuFont;
        productName.font = menuFont;

        //Font Style
        connectButtonText.fontStyle = FontStyle.Normal;
        createButtonText.fontStyle = FontStyle.Normal;
        playButtonText.fontStyle = FontStyle.Normal;
        smokeButtonText.fontStyle = FontStyle.Normal;
        productName.fontStyle = FontStyle.Normal;
        companyName.fontStyle = FontStyle.Normal;

        //Font Size
        connectButtonText.fontSize = fontSize;
        createButtonText.fontSize = fontSize;
        playButtonText.fontSize = fontSize;
        smokeButtonText.fontSize = fontSize;
        productName.fontSize = 20;
        companyName.fontSize = 18;

        //set Color
        connectButtonText.color = Color.white;
        createButtonText.color = Color.white;
        playButtonText.color = Color.white;
        smokeButtonText.color = Color.white;
        productName.color = Color.white;
        companyName.color = Color.white;

        //set Rect
        connectButtonRect = new Rect(50, (Screen.height - 389), connectButtonText.text.Length * 65, fontSize - 16);
        createButtonRect = new Rect(50, (Screen.height - 315), createButtonText.text.Length * 60, fontSize - 20);
        playButtonRect = new Rect(50, (Screen.height - 246), playButtonText.text.Length * 60, fontSize - 22);
        smokeButtonRect = new Rect(50, (Screen.height - 178), smokeButtonText.text.Length * 68, fontSize - 16);
        #endregion
    }

    private static void DeconstructStartMenu()
    {
        connectButtonRect = new Rect();
        createButtonRect = new Rect();
        playButtonRect = new Rect();
        smokeButtonRect = new Rect();

        for (int i = 0; i < Objects.Count; i++)
        {
            Destroy(Objects[i]);
        }
    }

    public static void BuildPlayMenu()
    {
        Deconstruct();

        FolderBuilder.instance.OpenLoading();

        backgroundImage = Resources.Load("Backgrounds/MenuBackgrounds/Green") as Texture2D;
        backgroundTexture.texture = backgroundImage;

        homeButton = new GameObject();

        Objects.AddMany(homeButton);
        //Give Buttons a Name
        homeButton.name = "Home_Button";

        //Give Buttons the right components
        homeButton.AddComponent<GUITexture>();

        //Select the Components a variable
        homeTexture = homeButton.GetComponent<GUITexture>();

        homeTexture.texture = homeIcon;

        //position
        homeButton.transform.position = new Vector3(0, 0, 0);

        homeTexture.pixelInset = new Rect(50, Screen.height - 30 - 70, 30, 70);

        //scale
        homeButton.transform.localScale = new Vector3(0, 0, 1);

        homeIconRect = new Rect(50, 50, 30, 70);
    }

    private static void DeconstructPlayMenu()
    {
        homeIconRect = new Rect();

        for (int i = 0; i < Objects.Count; i++)
        {
            Destroy(Objects[i]);
        }

        FolderBuilder.instance.CloseLoading();
    }

    public static void BuildCreateMenu()
    {
        Deconstruct();
        #region createmenu onnodig voor nu
        List<GUIText> textList = new List<GUIText>();
        List<GameObject> textObjectList = new List<GameObject>();
        textList.Clear();
        textObjectList.Clear();
        //Creating Scene Buttons
        settingsIcon = new GameObject("Settings_Icon");
        fireIcon = new GameObject("Fire_Icon");
        plusIcon = new GameObject("Plus_Icon");
        saveIcon = new GameObject("Save_Icon");
        backIcon = new GameObject("Back_Icon");

        settingsCollider = settingsIcon.AddComponent<BoxCollider2D>();
        fireCollider = fireIcon.AddComponent<BoxCollider2D>();
        plusCollider = plusIcon.AddComponent<BoxCollider2D>();
        saveCollider = saveIcon.AddComponent<BoxCollider2D>();
        backCollider = backIcon.AddComponent<BoxCollider2D>();



        settingsIconSprite = Resources.Load<Sprite>("Sprites/Create_Icons/SettingsIcon");
        fireIconSprite = Resources.Load<Sprite>("Sprites/Create_Icons/FireIcon");
        plusIconSprite = Resources.Load<Sprite>("Sprites/Create_Icons/PlusIcon");
        saveIconSprite = Resources.Load<Sprite>("Sprites/Create_Icons/SaveIcon");
        backIconSprite = Resources.Load<Sprite>("Sprites/Create_Icons/Crossicon");

        settingsTitleObject = new GameObject("Settings_Text");
        fireTitleObject = new GameObject("Fire_Text");
        plusTitleObject = new GameObject("Plus_Text");
        saveTitleObject = new GameObject("Save_Text");

        createdSceneObjectHolder = new GameObject("createdSceneObjectHolder");
        createMenuScriptHolder = new GameObject("CreateMenuScriptHolder");
        createMenuScriptHolder.AddComponent<TopBar_Script>();

        Objects.AddMany(settingsIcon, fireIcon, plusIcon, saveIcon, backIcon, settingsTitleObject, fireTitleObject, plusTitleObject, saveTitleObject);

        settingsIconRenderer = settingsIcon.AddComponent<SpriteRenderer>();
        fireIconRenderer = fireIcon.AddComponent<SpriteRenderer>();
        plusIconRenderer = plusIcon.AddComponent<SpriteRenderer>();
        saveIconRenderer = saveIcon.AddComponent<SpriteRenderer>();
        backIconRenderer = backIcon.AddComponent<SpriteRenderer>();

        settingsIconRenderer.sprite = settingsIconSprite;
        fireIconRenderer.sprite = fireIconSprite;
        plusIconRenderer.sprite = plusIconSprite;
        saveIconRenderer.sprite = saveIconSprite;
        backIconRenderer.sprite = backIconSprite;

        settingsTitle = settingsTitleObject.AddComponent<GUIText>();
        fireTitle = fireTitleObject.AddComponent<GUIText>();
        plusTitle = plusTitleObject.AddComponent<GUIText>();
        saveTitle = saveTitleObject.AddComponent<GUIText>();

        settingsTitle.text = "SETTINGS";
        fireTitle.text = "FIRE";
        plusTitle.text = "BACKGROUND";
        saveTitle.text = "SAVE";

        textList.AddMany(settingsTitle, fireTitle, plusTitle, saveTitle);

        //Background
        backgroundImage = Resources.Load("Backgrounds/MenuBackgrounds/Yellow") as Texture2D;
        backgroundTexture.texture = backgroundImage;

        //Scale

        float xSize = settingsIconSprite.bounds.size.x;
        float ySize = settingsIconSprite.bounds.size.y;
        float width;
        float height;

        if (screenDPI > 0)
        {
            width = 76 * screenDPI;
            height = 86 * screenDPI;
        }
        else
        {
            width = 76;
            height = 86;
        }

        float worldwidth = (camera.orthographicSize * 2 / Screen.height * width) / xSize;
        float worldHeight = (camera.orthographicSize * 2 / Screen.height * height) / ySize;

        settingsIcon.transform.localScale = new Vector3(worldwidth, worldHeight, 1);
        plusIcon.transform.localScale = new Vector3(worldwidth, worldHeight, 1);
        fireIcon.transform.localScale = new Vector3(worldwidth, worldHeight, 1);
        backIcon.transform.localScale = new Vector3(worldwidth, worldHeight, 1);
        saveIcon.transform.localScale = new Vector3(worldwidth, worldHeight, 1);


        //position
        float x = camera.ScreenToWorldPoint(new Vector3((Screen.width / 2), 0, 0)).x;
        float y = camera.ScreenToWorldPoint(new Vector3(0, 50, 0)).y;
        Vector3 position = new Vector3(x, y, -1);

        //Left 2
        plusIcon.transform.position = new Vector3(position.x - (plusIconSprite.bounds.size.x * 1.1f * 2) * settingsIcon.transform.localScale.x, position.y + (plusIconSprite.bounds.size.y / 2), 1);
        //Left 1
        fireIcon.transform.position = new Vector3(position.x - (fireIconSprite.bounds.size.x * 1.1f) * settingsIcon.transform.localScale.x, position.y + (fireIconSprite.bounds.size.y / 2), 1);
        //Midle
        settingsIcon.transform.position = new Vector3(position.x, position.y + (settingsIconSprite.bounds.size.y / 2), 1);
        //Right 1
        saveIcon.transform.position = new Vector3(position.x + (saveIconSprite.bounds.size.x * 1.1f) * settingsIcon.transform.localScale.x, position.y + (saveIconSprite.bounds.size.y / 2), 1);
        //Right 2
        backIcon.transform.position = new Vector3(position.x + (backIconSprite.bounds.size.x * 1.1f * 2) * settingsIcon.transform.localScale.x, position.y + (backIconSprite.bounds.size.y / 2), 1);

        plusTitle.pixelOffset = new Vector2(50, (Screen.height - 65));
        settingsTitle.pixelOffset = new Vector2(50, (Screen.height - 132));
        fireTitle.pixelOffset = new Vector2(50, (Screen.height - 200));
        saveTitle.pixelOffset = new Vector2(50, (Screen.height - 265));

        foreach (GUIText item in textList)
        {
            item.anchor = TextAnchor.MiddleLeft;
            item.color = Color.black;
            item.font = menuFont;
            item.fontStyle = FontStyle.Normal;
            item.fontSize = fontSize;
        }

        //Rect
        //PlusIcon
        plusTextRect = new Rect(50,
            (Screen.height - plusTitle.GetScreenRect().yMax + 14),
            plusTitle.GetScreenRect().width,
            fontSize - 20);

        //FireIcon
        fireTextRect = new Rect(50,
            (Screen.height - fireTitle.GetScreenRect().yMax + 14),
            fireTitle.GetScreenRect().width,
            fontSize - 20);

        //SettingsIcon
        settingsTextRect = new Rect(50,
            (Screen.height - settingsTitle.GetScreenRect().yMax + 14),
            settingsTitle.GetScreenRect().width,
            fontSize - 20);

        //SaveIcon
        saveTextRect = new Rect(50,
            (Screen.height - saveTitle.GetScreenRect().yMax + 14),
            saveTitle.GetScreenRect().width,
            fontSize - 20);
        //Left Top Width Height
        #endregion

    }

    private static Rect scaleGuiTexture(GUITexture texture, Vector3 scale)
    {
        Rect size = texture.pixelInset;
        size.x *= scale.x;
        size.width *= scale.x;
        size.y *= scale.x;
        size.height *= scale.x;
        return size;
    }

    private static void DeconstructCreateMenu()
    {
        for (int i = 0; i < Objects.Count; i++)
        {
            Destroy(Objects[i]);
        }

        Destroy(createMenuScriptHolder);
        Destroy(createdSceneObjectHolder);

        saveTextRect = new Rect();
        fireTextRect = new Rect();
        settingsTextRect = new Rect();
        plusTextRect = new Rect();


    }

    public static void BuildConnectMenu()
    {
        Deconstruct();

        homeButton = new GameObject();

        Objects.AddMany(homeButton);
        //Give Buttons a Name
        homeButton.name = "Home_Button";

        //Give Buttons the right components
        homeButton.AddComponent<GUITexture>();

        //Select the Components a variable
        homeTexture = homeButton.GetComponent<GUITexture>();

        homeTexture.texture = homeIcon;

        //position
        homeButton.transform.position = new Vector3(0, 0, 0);

        homeTexture.pixelInset = new Rect(50, Screen.height - 30 - 70, 30, 70);

        //scale
        homeButton.transform.localScale = new Vector3(0, 0, 1);

        homeIconRect = new Rect(50, 50, 30, 70);
    }

    private static void DeconstructConnectMenu()
    {
        homeIconRect = new Rect();

        for (int i = 0; i < Objects.Count; i++)
        {
            Destroy(Objects[i]);
        }
    }
}
