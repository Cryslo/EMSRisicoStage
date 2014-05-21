using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class PlayManager : MonoBehaviour {
	FolderManager folderManager;

	private GameObject scene;

	private string directoryName;
	private string sceneName;

	private FileInfo currentScene;
	private List<FileInfo> scenes;

	private SceneOptions sceneOptions;

	void Awake () {
		folderManager = this.gameObject.GetComponent<FolderManager>();

	}
	// Use this for initialization
	void Start () {
		scene = new GameObject("Scene");

		directoryName = GameManager.directoryName;
		sceneName = GameManager.sceneName;

		scenes = folderManager.GetScenesByName(directoryName);

		for(int i = 0; i < scenes.Count; i++) {
			if(scenes[i].Name == sceneName)
			{
				currentScene = scenes[i];
			}
		}

		print(currentScene.FullName);
		sceneOptions = XmlBehaviour.LoadScene(currentScene.FullName, scene);

	}
	

}
