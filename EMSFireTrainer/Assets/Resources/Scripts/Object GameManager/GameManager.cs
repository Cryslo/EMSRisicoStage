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
	Play,
	none
}

public class GameManager : MonoBehaviour {
	public static GameManager instance;

	private static GameState gameState;
	private static GameState previousGameState;
	
	public static GameState queGameState;

	public static string directoryName;
	public static string sceneName;

	public static bool inFirst;
	private static bool created = false;

  public static GameState getGameState{
		get{return gameState;}
	}
	public static GameState PreviousGameState{
		get{return previousGameState;}
	}

	// Use this for initialization
	void Start () {
		if (!created) {
			// this is the first instance - make it persist
			DontDestroyOnLoad(this.gameObject);
			created = true;
		} else {
			// this must be a duplicate from a scene reload - DESTROY!
			Destroy(this.gameObject);
		}
		instance = this;
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

	public static void SetGameStateBack() {
		instance.StartCoroutine("FinishFirst",2.0f);
		instance.StartCoroutine("DoLast");
    }

	public IEnumerator FinishFirst(float waitTime) {
		inFirst = true;
		Application.LoadLevel("Jippe");
		yield return new WaitForSeconds(waitTime);
		print("leave FinishFirst");
		inFirst = false;
	}
	
	public IEnumerator DoLast() {
		
		while(inFirst)       
			yield return new WaitForSeconds(0.1f);
		print("Do stuff.");
		previousGameState = GameState.none;
		SetGameState(queGameState);
	}

	public static void SetGameState(GameState state, string DirectoryName, string SceneName )
	{
		previousGameState = gameState;
		switch(state)
		{

		case GameState.Play:
			gameState = GameState.Play;
			directoryName = DirectoryName;
			sceneName = SceneName;

			Application.LoadLevel("Play_Scene");
			break;
			
		default:
			Debug.LogError("State not exist please change: " + state.ToString());
			break;
		}
		return;
	}
}