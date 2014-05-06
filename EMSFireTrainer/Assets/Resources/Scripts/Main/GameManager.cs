using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;

public enum GameState{
	MainMenu,
	CreateMenu,
	PlayMenu,
	ConnectMenu
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

		default:
			Debug.LogError("State not exist please change: " + state.ToString());
			break;
		}
		return;
	}
}