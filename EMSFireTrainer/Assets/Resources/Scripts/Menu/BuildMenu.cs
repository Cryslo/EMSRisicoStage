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

	public Vector3 test1;
	public Rect test2;

	void Awake() {

		//Init Font
		menuFont = Resources.Load("Fonts/HelveticaNeue") as Font;
		fontSize = 50;
		//Init MainMenu Buttons
		connectButton = new GameObject();
		createButton = new GameObject();
		playButton = new GameObject();
		smokeButton = new GameObject();

		//Give Buttons a Name
		connectButton.name = "Connect_Button";
		createButton.name = "Create_Button";
		playButton.name = "Play_Button";
		smokeButton.name = "Smoke_Button";

		//Give Buttons the right components
		connectButton.AddComponent<GUIText>();
		createButton.AddComponent<GUIText>();
		playButton.AddComponent<GUIText>();
		smokeButton.AddComponent<GUIText>();

		//Select the Components a variable
		connectButtonText = connectButton.GetComponent<GUIText>();
		createButtonText = createButton.GetComponent<GUIText>();
		playButtonText = playButton.GetComponent<GUIText>();
		smokeButtonText = smokeButton.GetComponent<GUIText>();

		//Give Components right values
		//Text
		connectButtonText.text = "Connect";
		createButtonText.text = "Create";
		playButtonText.text = "Play";
		smokeButtonText.text = "Smoke Control";

		//Posistion
		connectButton.transform.position = new Vector3(0.04f, 0.7f, 0);
		createButton.transform.position = new Vector3(0.04f, 0.6f, 0);
		playButton.transform.position = new Vector3(0.04f, 0.5f, 0);
		smokeButton.transform.position = new Vector3(0.04f, 0.4f, 0);

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

		//set Rect
		connectButtonRect = new Rect(50,220, connectButtonText.text.Length * 30, fontSize );
		createButtonRect = new Rect(50,293, createButtonText.text.Length * 28, fontSize );
		playButtonRect = new Rect(50,368, playButtonText.text.Length * 28, fontSize );
		smokeButtonRect = new Rect(50,435, smokeButtonText.text.Length * 28, fontSize );

	}

	void Start() {
		mainCamera = GameObject.Find("Main Camera").camera;
	}

	void Update() {
		Vector2 mousePos = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y); 

		if(connectButtonRect.Contains(mousePos))
		{
			connectButtonText.color = Color.red;
		} else {
			connectButtonText.color = Color.white;
		}

		if(createButtonRect.Contains(mousePos))
		{
			createButtonText.color = Color.red;
		} else {
			createButtonText.color = Color.white;
		}

		if(playButtonRect.Contains(mousePos))
		{
			playButtonText.color = Color.red;
		} else {
			playButtonText.color = Color.white;
		}

		if(smokeButtonRect.Contains(mousePos))
		{
			smokeButtonText.color = Color.red;
		} else {
			smokeButtonText.color = Color.white;
		}

	}

	void OnGUI() {
		//GUI.Box(connectButtonRect, "Connect");
		//GUI.Box(createButtonRect, "Create");
		//GUI.Box(playButtonRect, "Play");
		//GUI.Box(smokeButtonRect, "Smoke");
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

	}

	private static void DeconstructStartMenu() {

	}

	public static void BuildPlayMenu() {

	}
	
	private static void DeconstructPlayMenu() {

	}

	public static void BuildCreateMenu() {

	}
	
	private static void DeconstructCreateMenu() {

	}
	public static void BuildConnectMenu() {
		
	}
	
	private static void DeconstructConnectMenu() {
		
	}
}
