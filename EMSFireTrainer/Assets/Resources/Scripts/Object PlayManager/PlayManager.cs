using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;

public class PlayManager : MonoBehaviour {
	static FolderManager folderManager;
	public static PlayManager instance;

	private static GameObject scene;

	private static string directoryName;
	private static string sceneName;

	private static FileInfo currentScene;
	private static List<FileInfo> scenes;
	public static List<FileInfo> scenesWaiting;
	private static List<string> scenesDone;

	private static int sceneNumber = 0;

	private static SceneOptions sceneOptions;

	void Awake () {
		folderManager = this.gameObject.GetComponent<FolderManager>();
	}
	// Use this for initialization
	void Start () {
		instance = this;
		scene = new GameObject("Scene");
		scenesDone = new List<string>();

		directoryName = GameManager.directoryName;
		sceneName = GameManager.sceneName;

		scenes = folderManager.GetScenesByName(directoryName);
		scenesWaiting = folderManager.GetScenesByName(directoryName);
		print(scenes.Count);

		for(int i = 0; i < scenes.Count; i++) {
			if(scenes[i].Name == sceneName)
			{
				currentScene = scenes[i];
				scenesDone.Add(scenes[i].Name);
				sceneNumber = i;
			}
		}

		print(currentScene.FullName);
		print(scenes.Count);
		RPCController.instance.SendScenes(scenes);

		sceneOptions = XmlBehaviour.LoadScene(currentScene.FullName, scene);

	}

	public static void NextScene() {
		if(scenes.Count > sceneNumber + 1)
		{
			GameObject.Destroy(scene);
			scene = new GameObject("Scene");
			sceneNumber ++;
			currentScene = scenes[sceneNumber];
			scenesDone.Add(scenes[sceneNumber].Name);
			
			print(scenes[sceneNumber].Name);
			string notifyName = scenes[sceneNumber].Name;
			Notify.notify(notifyName.Remove(notifyName.Length - 4));
			
			sceneOptions = XmlBehaviour.LoadScene(currentScene.FullName, scene);
		}else{
			GameManager.queGameState = GameState.PlayMenu;
			GameManager.SetGameStateBack();
		}
	}

	public static void PlayScene(FileInfo playScene) {
		if(File.Exists(playScene.FullName))
		{
			GameObject.Destroy(scene);
			scene = new GameObject("Scene");

			currentScene = scenes[sceneNumber];
			scenesDone.Add(scenes[sceneNumber].Name);
			
			print(playScene.Name);
			string notifyName = playScene.Name;
			Notify.notify(notifyName.Remove(notifyName.Length - 4));
			
			sceneOptions = XmlBehaviour.LoadScene(playScene.FullName, scene);
		}
	}

	public static void PreviousScene() {
		if(sceneNumber > 0) {
			GameObject.Destroy(scene);
			scene = new GameObject("Scene");

			sceneNumber --;
			currentScene = scenes[sceneNumber];
			scenesDone.Add(scenes[sceneNumber].Name);
			string notifyName = scenes[sceneNumber].Name;
			Notify.notify(notifyName.Remove(notifyName.Length - 4));

			print(scenes[sceneNumber].Name);
			
			sceneOptions = XmlBehaviour.LoadScene(currentScene.FullName, scene);
		}else{
			Notify.notify("No Previous Scene");
		}
	}
	

}
