﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;

public class BuildMenu : MonoBehaviour {
	private static Camera mainCamera;

	private FolderBuilder folderBuilder;

	private static Font menuFont;
	private static int fontSize;

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

	public Vector3 test1;
	public Rect test2;

	void Awake() {
		folderBuilder = gameObject.GetComponent<FolderBuilder>();

		//Init Font
		menuFont = Resources.Load("Fonts/HelveticaNeue") as Font;
		//set standard font size
		fontSize = 90;
		//Init Textures
		homeIcon = Resources.Load("Icons/previous-36") as Texture2D;
		companyIcon = Resources.Load("Icons/ISCRisk-36") as Texture2D;
		backgroundImage = Resources.Load("Backgrounds/Orange") as Texture2D;

		background = new GameObject();
		background.isStatic = true;
		background.layer = 8;
		//Init MainMenu Buttons
		connectButton = new GameObject();
		createButton = new GameObject();
		playButton = new GameObject();
		smokeButton = new GameObject();

		product = new GameObject();
		company = new GameObject();
		companylogo = new GameObject();

		homeButton = new GameObject();

		//Give Buttons a Name
		background.name = "Background";
		connectButton.name = "Connect_Button";
		createButton.name = "Create_Button";
		playButton.name = "Play_Button";
		smokeButton.name = "Smoke_Button";
		product.name = "Product_Name";
		company.name = "Company_Name";
		homeButton.name = "Home_Button";
		companylogo.name = "Company_Logo";

		//Give Buttons the right components
		background.AddComponent<GUITexture>();
		connectButton.AddComponent<GUIText>();
		createButton.AddComponent<GUIText>();
		playButton.AddComponent<GUIText>();
		smokeButton.AddComponent<GUIText>();
		product.AddComponent<GUIText>();
		company.AddComponent<GUIText>();
		homeButton.AddComponent<GUITexture>();
		companylogo.AddComponent<GUITexture>();

		//Select the Components a variable
		backgroundTexture = background.GetComponent<GUITexture>();
		connectButtonText = connectButton.GetComponent<GUIText>();
		createButtonText = createButton.GetComponent<GUIText>();
		playButtonText = playButton.GetComponent<GUIText>();
		smokeButtonText = smokeButton.GetComponent<GUIText>();
		productName = product.GetComponent<GUIText>();
		companyName = company.GetComponent<GUIText>();
		homeTexture = homeButton.GetComponent<GUITexture>();
		companyTexture = companylogo.GetComponent<GUITexture>();
		
		backgroundTexture.texture = backgroundImage;
		homeTexture.texture = homeIcon;
		companyTexture.texture = companyIcon;


		backgroundTexture.pixelInset = new Rect(0,0,Screen.width,Screen.height);

		connectButton.SetActive(false);
		createButton.SetActive(false);
		playButton.SetActive(false);
		smokeButton.SetActive(false);
		product.SetActive(false);
		company.SetActive(false);
		homeButton.SetActive(false);
		companylogo.SetActive(false);

	}

	void Start() {
		mainCamera = GameObject.Find("Main Camera").camera;
	}

	void Update() {
		Vector2 mousePos = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y); 

		if(connectButtonRect.Contains(mousePos))
		{
			connectButtonText.color = Color.black;
			if (Input.GetMouseButtonUp(0)) {
				GameManager.SetGameState(GameState.ConnectMenu);
			}
		} else if (GameManager.getGameState != GameState.ConnectMenu){
			connectButtonText.color = Color.white;
		}

		if(createButtonRect.Contains(mousePos))
		{
			createButtonText.color = Color.black;
			if (Input.GetMouseButtonUp(0)) {
				GameManager.SetGameState(GameState.CreateMenu);
			}
		} else if (GameManager.getGameState != GameState.CreateMenu) {
			createButtonText.color = Color.white;
		}

		if(playButtonRect.Contains(mousePos))
		{
			playButtonText.color = Color.black;
			if (Input.GetMouseButtonUp(0)) {
				GameManager.SetGameState(GameState.PlayMenu);
			}
		} else if (GameManager.getGameState != GameState.PlayMenu) {
			playButtonText.color = Color.white;
		}

		if(smokeButtonRect.Contains(mousePos))
		{
			smokeButtonText.color = Color.black;
			//TODO: give smoke a gamestate
			if (Input.GetMouseButtonUp(0)) {
				GameManager.SetGameState(GameState.PlayMenu);
			}
		} else {
			smokeButtonText.color = Color.white;
		}

		if(homeIconRect.Contains(mousePos))
		{
			homeTexture.color = Color.black;
			if (Input.GetMouseButtonUp(0)) {
				GameManager.SetGameState(GameState.MainMenu);
			}
		} else if (GameManager.getGameState != GameState.MainMenu)  {
			homeTexture.color = Color.white;
		}

	}

	void OnGUI() {
		//GUI.Box(connectButtonRect, "Connect");
		//GUI.Box(createButtonRect, "Create");
		//GUI.Box(playButtonRect, "Play");
		//GUI.Box(smokeButtonRect, "Smoke");
		//GUI.Box(homeIconRect, "Home");
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
	}

	public static void BuildStartMenu() {
		Deconstruct();

		#region BuildMenu
		
		backgroundImage = Resources.Load("Backgrounds/Orange") as Texture2D;
		backgroundTexture.texture = backgroundImage;
		
		fontSize = 90;

		connectButton.SetActive(true);
		createButton.SetActive(true);
		playButton.SetActive(true);
		smokeButton.SetActive(true);
		product.SetActive(true);
		company.SetActive(true);
		companylogo.SetActive(true);

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
		connectButtonText.pixelOffset = new Vector2 (50, - (Screen.height - 400));
		createButtonText.pixelOffset = new Vector2 (50,- (Screen.height - 330));
		playButtonText.pixelOffset = new Vector2 (50,- (Screen.height - 262));
		smokeButtonText.pixelOffset = new Vector2 (50,- (Screen.height - 195));
		productName.pixelOffset = new Vector2 (Screen.width - 250,-50);
		companyName.pixelOffset = new Vector2 (Screen.width - 250,-70);
		companyTexture.pixelInset = new Rect(Screen.width - 300,Screen.height - 90,36,36);

		//scale
		companylogo.transform.localScale = new Vector3(0,0,1);
		
		//anchor
		connectButtonText.anchor = TextAnchor.UpperLeft;
		createButtonText.anchor = TextAnchor.UpperLeft;
		playButtonText.anchor = TextAnchor.UpperLeft;
		smokeButtonText.anchor = TextAnchor.UpperLeft;
		productName.anchor = TextAnchor.UpperLeft;
		companyName.anchor = TextAnchor.UpperLeft;
		
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
		connectButtonText.fontStyle = FontStyle.Bold;
		createButtonText.fontStyle = FontStyle.Bold;
		playButtonText.fontStyle = FontStyle.Bold;
		smokeButtonText.fontStyle = FontStyle.Bold;
		productName.fontStyle = FontStyle.Bold;
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

		connectButton.SetActive(false);
		createButton.SetActive(false);
		playButton.SetActive(false);
		smokeButton.SetActive(false);
		product.SetActive(false);
		company.SetActive(false);
		companylogo.SetActive(false);
	}

	public static void BuildPlayMenu() {
		Deconstruct();

		FolderBuilder.instance.OpenLoading();

		backgroundImage = Resources.Load("Backgrounds/Green") as Texture2D;
		backgroundTexture.texture = backgroundImage;

		homeButton.SetActive(true);

		//position
		homeButton.transform.position = new Vector3(0,0,0);

		homeTexture.pixelInset = new Rect( 50,Screen.height - 36 - 50,19,36);
		
		//scale
		homeButton.transform.localScale = new Vector3(0,0,1);

		homeIconRect = new Rect(50,50, 19, 36);
	}
	
	private static void DeconstructPlayMenu() {
		homeIconRect = new Rect();
		homeButton.SetActive(false);
		
		FolderBuilder.instance.CloseLoading();
	}

	public static void BuildCreateMenu() {
		Deconstruct();

		homeButton.SetActive(true);
		
		//position
		homeButton.transform.position = new Vector3(0,0,0);
		
		homeTexture.pixelInset = new Rect( 50,Screen.height - 36 - 50,19,36);
		
		//scale
		homeButton.transform.localScale = new Vector3(0,0,1);
        
		homeIconRect = new Rect(50,50, 19, 36);
	}
	
	private static void DeconstructCreateMenu() {
		homeIconRect = new Rect();
		homeButton.SetActive(false);

	}
	public static void BuildConnectMenu() {
		Deconstruct();

		homeButton.SetActive(true);
		
		//position
		homeButton.transform.position = new Vector3(0,0,0);
		
		homeTexture.pixelInset = new Rect(50,Screen.height - 36 - 50,19,36);
		
		//scale
		homeButton.transform.localScale = new Vector3(0,0,1);
        
		homeIconRect = new Rect(50,50, 19, 36);
	}
	
	private static void DeconstructConnectMenu() {
		homeIconRect = new Rect();
		homeButton.SetActive(false);
		
	}
}
