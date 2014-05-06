using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;

public class BuildMenu : MonoBehaviour {

	private static Font menuFont;
	private static int fontSize;

	private static GameObject playButton;
	private static GUIText playButtonText;
	private static Rect playButtonRect;

	public Vector3 test1;
	public Rect test2;

	void Awake() {
		print("Start!");
		//Init Font
		menuFont = Resources.Load("Fonts/HelveticaNeue") as Font;
		fontSize = 50;
		//Init MainMenu Buttons
		playButton = new GameObject();

		//Give Buttons a Name
		playButton.name = "Play_Button";

		//Give Buttons the right components
		playButton.AddComponent<GUIText>();

		//Select the Components a variable
		playButtonText = playButton.GetComponent<GUIText>();

		//Give Components right values
		//Text
		playButtonText.text = "Play";

		//Posistion
		playButton.transform.position = test1;

		//Color
		playButtonText.color = Color.white;

		//Font
		playButtonText.font = menuFont;	

		//Font Style
		playButtonText.fontStyle = FontStyle.Bold;

		//Font Size
		playButtonText.fontSize = 50;

		//set Rect
		playButtonRect = new Rect(0,8, playButtonText.text.Length * 26, fontSize );


		test2 = new Rect(0,0, playButtonText.text.Length * 26, fontSize );
	}

	void Start() {

	}

	void Update() {
		playButton.transform.position = test1;
		playButtonRect = test2;
	}
	void OnGUI() {
		GUI.Box(playButtonRect, " ");
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
