using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;

public class BuildMenu : MonoBehaviour {

	private static Font MenuFont;

	private static GameObject PlayButton;
	private static GUIText PlayButtonText;

	void Awake() {
		print("Start!");
		//Init Font
		MenuFont = Resources.Load("Fonts/HelveticaNeue") as Font;
		//Init MainMenu Buttons
		PlayButton = new GameObject();

		//Give Buttons a Name
		PlayButton.name = "Play_Button";

		//Give Buttons the right components
		PlayButton.AddComponent<GUIText>();

		//Select the Components a variable
		PlayButtonText = PlayButton.GetComponent<GUIText>();

		//Give Components right values
		//Text
		PlayButtonText.text = "Play";

		//Posistion
		PlayButton.transform.position = new Vector3( 0, 1, 0);

		//Color
		PlayButtonText.color = Color.white;

		//Font
		PlayButtonText.font = MenuFont;	

		//Font Style
		PlayButtonText.fontStyle = FontStyle.Bold;

		//Font Size
		PlayButtonText.fontSize = 50;


	}

	void Start() {

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
