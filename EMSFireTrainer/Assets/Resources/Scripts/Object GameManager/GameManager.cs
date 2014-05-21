using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;

public enum GameState{
	MainMenu,
	CreateMenu,
	PlayMenu,
	ConnectMenu,
	Play
}

public class GameManager : MonoBehaviour {

	private static GameState gameState;
	private static GameState previousGameState;

    public static GameState getGameState{
		get{return gameState;}
	}
	public static GameState PreviousGameState{
		get{return previousGameState;}
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.gameObject);
		SetGameState(GameState.MainMenu);
	}

	public static void SetGameState(GameState state)
	{
		previousGameState = gameState;
		switch(state)
		{
		case GameState.MainMenu:
			gameState = GameState.MainMenu;
			BuildMenu.BuildStartMenu();
		break;

		case GameState.PlayMenu:
			gameState = GameState.PlayMenu;
			BuildMenu.BuildPlayMenu();
		break;

		case GameState.CreateMenu:
			gameState = GameState.CreateMenu;
			BuildMenu.BuildCreateMenu();
		break;

		case GameState.ConnectMenu:
			gameState = GameState.ConnectMenu;
			BuildMenu.BuildConnectMenu();
		break;

		case GameState.Play:
			Debug.LogError("please use second SetGameState function witch need a path");
			break;

		default:
			Debug.LogError("State not exist please change: " + state.ToString());
			break;
		}
		return;
	}

	public static void SetGameState(GameState state, string path)
	{
		previousGameState = gameState;
		switch(state)
		{

		case GameState.Play:
			gameState = GameState.Play;
			XmlBehaviour.LoadScene(path);
			break;
			
		default:
			Debug.LogError("State not exist please change: " + state.ToString());
			break;
		}
		return;
	}
}