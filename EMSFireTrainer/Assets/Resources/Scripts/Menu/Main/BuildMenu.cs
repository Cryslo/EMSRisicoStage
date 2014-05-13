using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;

public class BuildMenu : MonoBehaviour {
	private static Camera mainCamera;

	private FolderBuilder folderBuilder;

	private static Font menuFont;
	private static int fontSize;

	private static List<GameObject> Objects;

	private static GameObject background;
	private static GUITexture backgroundTexture;
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

	#region Create buttons
	private static int createButtonWidth = 76;
	private static int createButtonHeight = 86;

	private static GameObject settingsIcon;
	private static GUITexture settingsTexture;
	private static Texture2D settingsImage;
	private static GUIText settingsTitle;
    private static Rect settingsRect;
    private static Rect settingsTextRect;
    
	private static GameObject fireIcon;
	private static GUITexture fireTexture;
	private static Texture2D fireImage;
    private static GUIText fireTitle;
    private static Rect fireIconRect;
    private static Rect fireTextRect;
    
	private static GameObject plusIcon;
	private static GUITexture plusTexture;
	private static Texture2D plusImage;
    private static GUIText plusTitle;
    private static Rect plusIconRect;
    private static Rect plusTextRect;

	private static GameObject saveIcon;
	private static GUITexture saveTexture;
	private static Texture2D saveImage;
    private static GUIText saveTitle;
    private static Rect saveIconRect;
    private static Rect saveTextRect;

	private static GameObject backIcon;
	private static GUITexture backTexture;
	private static Texture2D backImage;
	private static GUIText backTitle;
    private static Rect backIconRect;
<<<<<<< HEAD
=======


>>>>>>> FETCH_HEAD
	#endregion

	private static float nativeWidth = 1280;
	private static float nativeHeight = 720;
	private static Matrix4x4 test;
	

	void Awake() {
		folderBuilder = gameObject.GetComponent<FolderBuilder>();

		Objects = new List<GameObject>();

		//Init Font
		menuFont = Resources.Load("Fonts/HelveticaNeue") as Font;
		//set standard font size
		fontSize = 90;

		//Init Textures
		homeIcon = Resources.Load("Icons/previous-36") as Texture2D;
		companyIcon = Resources.Load("Icons/ISCRisk-36") as Texture2D;
		backgroundImage = Resources.Load("Backgrounds/Orange") as Texture2D;
		settingsImage = Resources.Load("Icons/SettingsIcon") as Texture2D;
		fireImage = Resources.Load("Icons/FireIcon") as Texture2D;
		plusImage = Resources.Load("Icons/PlusIcon") as Texture2D;
		saveImage = Resources.Load("Icons/SaveIcon") as Texture2D;
		backImage = Resources.Load("Icons/CrossIcon") as Texture2D;


		background = new GameObject();
		background.isStatic = true;
		background.layer = 8;
		background.name = "Background";
		background.AddComponent<GUITexture>();
		backgroundTexture = background.GetComponent<GUITexture>();		
		backgroundTexture.pixelInset = new Rect(0,0,Screen.width,Screen.height);
		backgroundTexture.texture = backgroundImage;

	}

	void Start() {
		mainCamera = GameObject.Find("Main Camera").camera;
		//Screen.SetResolution(1280,720,true);
		Vector3 scale = new Vector3(nativeWidth,nativeHeight, 1.0f);
       // test = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);
	}

	void Update() {
		Vector2 mousePos = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y); 
#region MainMenu
		if(GameManager.getGameState == GameState.MainMenu)
		{
			if(connectButtonRect.Contains(mousePos))
			{
				connectButtonText.color = Color.black;
				connectButtonText.fontSize = 70;
				if (Input.GetMouseButtonUp(0)) {
					GameManager.SetGameState(GameState.ConnectMenu);
				}
			} else if (GameManager.getGameState != GameState.ConnectMenu){
				connectButtonText.color = Color.white;
				connectButtonText.fontSize = fontSize;
			}

			if(createButtonRect.Contains(mousePos))
			{
				createButtonText.color = Color.black;
				createButtonText.fontSize = 70;
				if (Input.GetMouseButtonUp(0)) {
					GameManager.SetGameState(GameState.CreateMenu);
				}
			} else if (GameManager.getGameState != GameState.CreateMenu) {
				createButtonText.color = Color.white;
				createButtonText.fontSize = fontSize;
			}

			if(playButtonRect.Contains(mousePos))
			{
				playButtonText.color = Color.black;
				playButtonText.fontSize = 70;
				playButtonText.anchor = TextAnchor.MiddleLeft;
				if (Input.GetMouseButtonUp(0)) {
					GameManager.SetGameState(GameState.PlayMenu);
				}
			} else if (GameManager.getGameState != GameState.PlayMenu) {
				playButtonText.color = Color.white;
				playButtonText.fontSize = fontSize;
			}

			if(smokeButtonRect.Contains(mousePos))
			{
				smokeButtonText.color = Color.black;
				smokeButtonText.fontSize = 70;
				//TODO: give smoke a gamestate
				if (Input.GetMouseButtonUp(0)) {
					GameManager.SetGameState(GameState.PlayMenu);
				}
			} else {
				smokeButtonText.color = Color.white;
				smokeButtonText.fontSize = fontSize;
            }
        }
#endregion

        if (GameManager.getGameState == GameState.CreateMenu)
        {
//Plus      
            GameObject FPM = GameObject.Find("Settings_Icon");
            if (plusTextRect.Contains(mousePos))
            {
                plusTitle.fontSize = 70;
                if (Input.GetMouseButtonUp(0))
                {
                }
            }

            else if (GameManager.getGameState == GameState.CreateMenu)
            {
                plusTitle.fontSize = fontSize;
            }
//Fire
            if (fireTextRect.Contains(mousePos))
            {
                fireTitle.fontSize = 70;
                if (Input.GetMouseButtonUp(0))
                {
                    FPM.GetComponent<Fire_Property_Menu>().createTopBar() ;
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu)
            {
                fireTitle.fontSize = fontSize;
            }

            if (fireIconRect.Contains(mousePos))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    FPM.GetComponent<Fire_Property_Menu>().createTopBar();
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu)
            {
            }
//Settings
            if (settingsTextRect.Contains(mousePos))
            {
                settingsTitle.fontSize = 70;
                if (Input.GetMouseButtonUp(0))
                {
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu)
            {
                settingsTitle.fontSize = fontSize;
            }
//Save
            if (saveTextRect.Contains(mousePos))
            {
                saveTitle.fontSize = 70;
                if (Input.GetMouseButtonUp(0))
                {
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu)
            {
                saveTitle.fontSize = fontSize;
            }
//Quit

            if (backIconRect.Contains(mousePos))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    GameManager.SetGameState(GameState.MainMenu);
                }
            }
            else if (GameManager.getGameState == GameState.CreateMenu)
            {
            }
        }

		if (GameManager.getGameState != GameState.MainMenu || GameManager.getGameState != GameState.CreateMenu) {
			if(homeIconRect.Contains(mousePos))
			{
				homeTexture.color = Color.black;
				if (Input.GetMouseButtonUp(0)) {
					GameManager.SetGameState(GameState.MainMenu);
				}
			} else if (GameManager.getGameState != GameState.MainMenu && GameManager.getGameState != GameState.CreateMenu)  {
                homeTexture.color = Color.white;
            }
		}
	}

	void OnGUI() {
		//GUI.matrix = test;
		if(playButtonText != null)
		{
		}
		//GUI.Box(connectButtonRect, "Connect");
		//GUI.Box(createButtonRect, "Create");
		//GUI.Box(playButtonRect, "Play");
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

	private static void Deconstruct() {
		//Check the PreviousGameState and decide from that which he has to deconstuct

		switch(GameManager.PreviousGameState)
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

	public static void BuildStartMenu() {
		Deconstruct();

		#region BuildMenu
		
		backgroundImage = Resources.Load("Backgrounds/Orange") as Texture2D;
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
		connectButtonText.pixelOffset = new Vector2 (50, - (Screen.height - 340));
		createButtonText.pixelOffset = new Vector2 (50,- (Screen.height - 270));
		playButtonText.pixelOffset = new Vector2 (50,- (Screen.height - 202));
		smokeButtonText.pixelOffset = new Vector2 (50,- (Screen.height - 135));
		productName.pixelOffset = new Vector2 (Screen.width - 200,-50);
		companyName.pixelOffset = new Vector2 (Screen.width - 200,-70);
		companyTexture.pixelInset = new Rect(Screen.width - 250,Screen.height - 90,36,36);

		//scale
		companylogo.transform.localScale = new Vector3(0,0,1);
		
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
		connectButtonRect = new Rect(50,(Screen.height - 389), connectButtonText.text.Length * 65, fontSize - 16 );
		createButtonRect = new Rect(50,(Screen.height - 315), createButtonText.text.Length * 60, fontSize - 20);
		playButtonRect = new Rect(50,(Screen.height - 246), playButtonText.text.Length * 60, fontSize - 22);
		smokeButtonRect = new Rect(50,(Screen.height - 178), smokeButtonText.text.Length * 68, fontSize - 16);
		#endregion
	}

	private static void DeconstructStartMenu() {
		connectButtonRect = new Rect();
		createButtonRect = new Rect();
		playButtonRect = new Rect();
		smokeButtonRect = new Rect();

		for (int i = 0; i < Objects.Count; i++) {
			Destroy(Objects[i]);	
		}
	}

	public static void BuildPlayMenu() {
		Deconstruct();

		FolderBuilder.instance.OpenLoading();

		backgroundImage = Resources.Load("Backgrounds/Green") as Texture2D;
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
		homeButton.transform.position = new Vector3(0,0,0);

		homeTexture.pixelInset = new Rect( 50,Screen.height - 36 - 50,19,36);
		
		//scale
		homeButton.transform.localScale = new Vector3(0,0,1);

		homeIconRect = new Rect(50,50, 19, 36);
	}
	
	private static void DeconstructPlayMenu() {
		homeIconRect = new Rect();

		for (int i = 0; i < Objects.Count; i++) {
			Destroy(Objects[i]);	
        }
		
		FolderBuilder.instance.CloseLoading();
	}

	public static void BuildCreateMenu() {
		Deconstruct();
        #region createmenu onnodig voor nu
        //Creating Scene Buttons
		settingsIcon = new GameObject();
		fireIcon = new GameObject();
		plusIcon = new GameObject();
		saveIcon = new GameObject();
		backIcon = new GameObject();

        settingsIcon.AddComponent<Fire_Property_Menu>();

		Objects.AddMany(settingsIcon, fireIcon, plusIcon, saveIcon, backIcon);
            
        settingsIcon.name = "Settings_Icon";
		fireIcon.name = "Fire_Icon";
		plusIcon.name = "Plus_Icon";
		saveIcon.name = "Save_Icon";
		backIcon.name = "Back_Icon";

		settingsIcon.AddComponent<GUITexture>();
		fireIcon.AddComponent<GUITexture>();
		plusIcon.AddComponent<GUITexture>();
		saveIcon.AddComponent<GUITexture>();
		backIcon.AddComponent<GUITexture>();
		
		settingsIcon.AddComponent<GUIText>();
		fireIcon.AddComponent<GUIText>();
		plusIcon.AddComponent<GUIText>();
		saveIcon.AddComponent<GUIText>();
        backIcon.AddComponent<GUIText>();

		settingsTexture = settingsIcon.GetComponent<GUITexture>();
		fireTexture = fireIcon.GetComponent<GUITexture>();
		plusTexture = plusIcon.GetComponent<GUITexture>();
		saveTexture = saveIcon.GetComponent<GUITexture>();
		backTexture = backIcon.GetComponent<GUITexture>();

		settingsTitle = settingsIcon.GetComponent<GUIText>();
		fireTitle = fireIcon.GetComponent<GUIText>();
		plusTitle = plusIcon.GetComponent<GUIText>();
		saveTitle = saveIcon.GetComponent<GUIText>();
		backTitle = backIcon.GetComponent<GUIText>();

		settingsTexture.texture = settingsImage;
		fireTexture.texture = fireImage;
		plusTexture.texture = plusImage;
		saveTexture.texture = saveImage;
		backTexture.texture = backImage;

		settingsTitle.text = "SETTINGS";
	    fireTitle.text = "FIRE";
		plusTitle.text = "BACKGROUND";
        saveTitle.text = "SAVE";
	    backTitle.text = "BACK";

		//Background
		backgroundImage = Resources.Load("Backgrounds/Yellow") as Texture2D;
		backgroundTexture.texture = backgroundImage;

		//position
		settingsIcon.transform.position = new Vector3(0,0,0);
		fireIcon.transform.position = new Vector3(0,0,0);
		plusIcon.transform.position = new Vector3(0,0,0);
		saveIcon.transform.position = new Vector3(0,0,0);
		backIcon.transform.position = new Vector3(0,0,0);

		//settingsTexture.pixelInset = new Rect( (Screen.width / 2) - (createButtonWidth / 2) - (2 * createButtonWidth + (10 * 2)), 50, createButtonWidth, createButtonHeight);
		plusTexture.pixelInset = new Rect( (Screen.width / 2) - (createButtonWidth / 2) - (2 * createButtonWidth + (10 * 2)), 50, createButtonWidth, createButtonHeight);
		settingsTexture.pixelInset = new Rect( (Screen.width / 2) - (createButtonWidth / 2) - ( 1 * createButtonWidth + (10 * 1)), 50, createButtonWidth,createButtonHeight);
		//plusTexture.pixelInset = new Rect( (Screen.width / 2) - (createButtonWidth / 2), 50, createButtonWidth, createButtonHeight);
		fireTexture.pixelInset = new Rect( (Screen.width / 2) - (createButtonWidth / 2), 50, createButtonWidth, createButtonHeight);
		saveTexture.pixelInset = new Rect( (Screen.width / 2) - (createButtonWidth / 2) + ( 1 * createButtonWidth + (10 * 1)), 50, createButtonWidth, createButtonHeight);
		backTexture.pixelInset = new Rect( (Screen.width / 2) - (createButtonWidth / 2) + ( 2 * createButtonWidth + (10 * 2)), 50, createButtonWidth, createButtonHeight);

		plusTitle.pixelOffset = new Vector2 (50,(Screen.height - 65));
		settingsTitle.pixelOffset = new Vector2 (50,(Screen.height - 132));
		fireTitle.pixelOffset = new Vector2 (50,(Screen.height - 200));
		saveTitle.pixelOffset = new Vector2 (50,(Screen.height - 265)); 

		//anchor
		settingsTitle.anchor = TextAnchor.MiddleLeft;
		fireTitle.anchor = TextAnchor.MiddleLeft;
		plusTitle.anchor = TextAnchor.MiddleLeft;
		saveTitle.anchor = TextAnchor.MiddleLeft;
		
		//Color
		settingsTitle.color = Color.black;
		fireTitle.color = Color.black;
		plusTitle.color = Color.black;
		saveTitle.color = Color.black;
		backTitle.color = Color.black;

		//Font
		settingsTitle.font = menuFont;	
		fireTitle.font = menuFont;	
		plusTitle.font = menuFont;	
		saveTitle.font = menuFont;	
		
		//Font Style
		settingsTitle.fontStyle = FontStyle.Normal;
		fireTitle.fontStyle = FontStyle.Normal;
		plusTitle.fontStyle = FontStyle.Normal;
		saveTitle.fontStyle = FontStyle.Normal;
        
        //Font Size
		settingsTitle.fontSize = fontSize;
		fireTitle.fontSize = fontSize;
		plusTitle.fontSize = fontSize;
		saveTitle.fontSize = fontSize;
        
        
        //scale
		settingsIcon.transform.localScale = new Vector3(0,0,1);
		fireIcon.transform.localScale = new Vector3(0,0,1);
		plusIcon.transform.localScale = new Vector3(0,0,1);
		saveIcon.transform.localScale = new Vector3(0,0,1);
		backIcon.transform.localScale = new Vector3(0,0,1);
        #endregion

        //Rect
        //PlusIcon
        plusTextRect = new Rect(50,
            (Screen.height - plusTitle.GetScreenRect().yMax + 14),
            plusTitle.GetScreenRect().width,
            fontSize - 20);

        plusIconRect = new Rect(plusTexture.GetScreenRect().xMin,
            Screen.height - plusTexture.GetScreenRect().yMax,
            plusTexture.GetScreenRect().width,
            plusTexture.GetScreenRect().height);

        //FireIcon
        fireTextRect = new Rect(50, 
            (Screen.height - fireTitle.GetScreenRect().yMax + 14),
            fireTitle.GetScreenRect().width, 
            fontSize - 20);
        
        fireIconRect = new Rect(fireTexture.GetScreenRect().xMin,
            Screen.height - fireTexture.GetScreenRect().yMax,
            fireTexture.GetScreenRect().width,
            fireTexture.GetScreenRect().height);

        //SettingsIcon
        settingsTextRect = new Rect(50,
            (Screen.height - settingsTitle.GetScreenRect().yMax + 14),
            settingsTitle.GetScreenRect().width,
            fontSize - 20);

        settingsRect = new Rect(settingsTexture.GetScreenRect().xMin,
            Screen.height - settingsTexture.GetScreenRect().yMax,
            settingsTexture.GetScreenRect().width,
            settingsTexture.GetScreenRect().height);

        //SaveIcon
        saveTextRect = new Rect(50,
            (Screen.height - saveTitle.GetScreenRect().yMax + 14),
            saveTitle.GetScreenRect().width,
            fontSize - 20);

        saveIconRect = new Rect(saveTexture.GetScreenRect().xMin,
            Screen.height - saveTexture.GetScreenRect().yMax,
            saveTexture.GetScreenRect().width,
            saveTexture.GetScreenRect().height);

        //Backicon
        backIconRect = new Rect(backTexture.GetScreenRect().xMin,
            Screen.height - backTexture.GetScreenRect().yMax,
            backTexture.GetScreenRect().width,
            backTexture.GetScreenRect().height);
        //Left Top Width Height
        
	}
	
	private static void DeconstructCreateMenu() {
		for (int i = 0; i < Objects.Count; i++) {
			Destroy(Objects[i]);	
		}

        backIconRect = new Rect();
        saveIconRect = new Rect();
        fireIconRect = new Rect();
        settingsRect = new Rect();
        plusIconRect = new Rect();

        saveTextRect = new Rect();
        fireTextRect = new Rect();
        settingsTextRect = new Rect();
        plusTextRect = new Rect();

        
    }

    public static void BuildConnectMenu() {
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
		homeButton.transform.position = new Vector3(0,0,0);
		
		homeTexture.pixelInset = new Rect( 50,Screen.height - 36 - 50,19,36);
		
		//scale
        homeButton.transform.localScale = new Vector3(0,0,1);
        
        homeIconRect = new Rect(50,50, 19, 36);
    }
    
    private static void DeconstructConnectMenu() {
		homeIconRect = new Rect();

		for (int i = 0; i < Objects.Count; i++) {
			Destroy(Objects[i]);	
        }
	}
}
