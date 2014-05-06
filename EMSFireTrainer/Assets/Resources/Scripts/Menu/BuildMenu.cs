using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;

public class BuildMenu : MonoBehaviour {

	private static GameObject PlayButton;
	private static GUIText PlayButtonText;

	void Awake() {
		print("Start!");
		//Init MainMenu Buttons
		PlayButton = new GameObject();
		PlayButtonText = new GUIText();

		//Give Buttons a Name
		PlayButton.name = "Play Button";

		//Give Buttons the right components
		PlayButton.AddComponent(PlayButtonText);

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
