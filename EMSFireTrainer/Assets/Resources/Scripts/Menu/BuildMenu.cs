using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;

public class BuildMenu : MonoBehaviour {
	private static Camera mainCamera;

	private static Font menuFont;
	private static int fontSize;

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

		//Init Font
		menuFont = Resources.Load("Fonts/HelveticaNeue") as Font;
		//set standard font size
		fontSize = 90;
		//Init Textures
		homeIcon = Resources.Load("Icons/home-36") as Texture2D;
		companyIcon = Resources.Load("Icons/ISCRisk-36") as Texture2D;

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
		connectButton.name = "Connect_Button";
		createButton.name = "Create_Button";
		playButton.name = "Play_Button";
		smokeButton.name = "Smoke_Button";
		product.name = "Product_Name";
		company.name = "Company_Name";
		homeButton.name = "Home_Button";
		companylogo.name = "Company_Logo";

		//Give Buttons the right components
		connectButton.AddComponent<GUIText>();
		createButton.AddComponent<GUIText>();
		playButton.AddComponent<GUIText>();
		smokeButton.AddComponent<GUIText>();
		product.AddComponent<GUIText>();
		company.AddComponent<GUIText>();
		homeButton.AddComponent<GUITexture>();
		companylogo.AddComponent<GUITexture>();

		//Select the Components a variable
		connectButtonText = connectButton.GetComponent<GUIText>();
		createButtonText = createButton.GetComponent<GUIText>();
		playButtonText = playButton.GetComponent<GUIText>();
		smokeButtonText = smokeButton.GetComponent<GUIText>();
		productName = product.GetComponent<GUIText>();
		companyName = company.GetComponent<GUIText>();
		homeTexture = homeButton.GetComponent<GUITexture>();
		companyTexture = companylogo.GetComponent<GUITexture>();

		homeTexture.texture = homeIcon;
		companyTexture.texture = companyIcon;

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
		GUI.Box(homeIconRect, "Home");
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
		connectButton.SetActive(true);
		createButton.SetActive(true);
		playButton.SetActive(true);
		smokeButton.SetActive(true);
		product.SetActive(true);
		company.SetActive(true);

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
		connectButtonText.pixelOffset = new Vector2 (50,-370);
		createButtonText.pixelOffset = new Vector2 (50,-443);
		playButtonText.pixelOffset = new Vector2 (50,-515);
		smokeButtonText.pixelOffset = new Vector2 (50,-585);
		productName.pixelOffset = new Vector2 (Screen.width - 250,-50);
		companyName.pixelOffset = new Vector2 (Screen.width - 250,-75);
		
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
		connectButtonRect = new Rect(40,220, connectButtonText.text.Length * 30, fontSize );
		createButtonRect = new Rect(40,293, createButtonText.text.Length * 28, fontSize );
		playButtonRect = new Rect(40,368, playButtonText.text.Length * 28, fontSize );
		smokeButtonRect = new Rect(40,435, smokeButtonText.text.Length * 28, fontSize );
		#endregion
	}

	private static void DeconstructStartMenu() {
		connectButton.SetActive(false);
		createButton.SetActive(false);
		playButton.SetActive(false);
		smokeButton.SetActive(false);
		product.SetActive(false);
		company.SetActive(false);
	}

	public static void BuildPlayMenu() {
		Deconstruct();

		#region BuildMenu
		connectButton.SetActive(true);
		createButton.SetActive(true);
		playButton.SetActive(true);
		smokeButton.SetActive(true);
		homeButton.SetActive(true);
		
		//Give Components right values
		//Text
		connectButtonText.text = "Connect";
		createButtonText.text = "Create";
		playButtonText.text = "Play";
		smokeButtonText.text = "Smoke";
		
		connectButton.transform.position = new Vector3(0, 1, 0);
		createButton.transform.position = new Vector3(0, 1, 0);
		playButton.transform.position = new Vector3(0, 1, 0);
		smokeButton.transform.position = new Vector3(0, 1, 0);
		homeButton.transform.position = new Vector3(0, 1, 0);

		//Scale
		homeButton.transform.localScale = new Vector3(0,0,1);
		
		//Posistion
		connectButtonText.pixelOffset = new Vector2 (122,-245);
		createButtonText.pixelOffset = new Vector2 (100,-318);
		playButtonText.pixelOffset = new Vector2 (70,-390);
		smokeButtonText.pixelOffset = new Vector2 (104,-460);
		homeTexture.pixelInset = new Rect(20,-36,36,36);
		
		//anchor
		connectButtonText.anchor = TextAnchor.MiddleCenter;
		createButtonText.anchor = TextAnchor.MiddleCenter;
		playButtonText.anchor = TextAnchor.MiddleCenter;
		smokeButtonText.anchor = TextAnchor.MiddleCenter;
		
		//Color
		connectButtonText.color = Color.white;
		createButtonText.color = Color.white;
		playButtonText.color = Color.white;
		smokeButtonText.color = Color.white;
		
		//Font
		connectButtonText.font = menuFont;	
		createButtonText.font = menuFont;	
		playButtonText.font = menuFont;	
		smokeButtonText.font = menuFont;	
		
		//Font Style
		connectButtonText.fontStyle = FontStyle.Bold;
		createButtonText.fontStyle = FontStyle.Bold;
		playButtonText.fontStyle = FontStyle.Bold;
		smokeButtonText.fontStyle = FontStyle.Bold;
		
		//Font Size
		connectButtonText.fontSize = 50;
		createButtonText.fontSize = 50;
		playButtonText.fontSize = 50;
		smokeButtonText.fontSize = 50;

		//set Color
		connectButtonText.color = Color.white;
		createButtonText.color = Color.white;
		playButtonText.color = Color.black;
		smokeButtonText.color = Color.white;
		productName.color = Color.white;
		companyName.color = Color.white;

		homeTexture.color = Color.white;
		
		//set Rect
		connectButtonRect = new Rect(18,220, connectButtonText.text.Length * 30, fontSize );
		createButtonRect = new Rect(18,293, createButtonText.text.Length * 28, fontSize );
		playButtonRect = new Rect(18,368, playButtonText.text.Length * 28, fontSize );
		smokeButtonRect = new Rect(18,435, smokeButtonText.text.Length * 34, fontSize );
		homeIconRect = new Rect(20,4,36,36);
		#endregion
	}
	
	private static void DeconstructPlayMenu() {
		connectButton.SetActive(false);
		createButton.SetActive(false);
		playButton.SetActive(false);
		smokeButton.SetActive(false);
		homeButton.SetActive(false);

	}

	public static void BuildCreateMenu() {
		Deconstruct();
	}
	
	private static void DeconstructCreateMenu() {

	}
	public static void BuildConnectMenu() {
		Deconstruct();
	}
	
	private static void DeconstructConnectMenu() {
		
	}
}
