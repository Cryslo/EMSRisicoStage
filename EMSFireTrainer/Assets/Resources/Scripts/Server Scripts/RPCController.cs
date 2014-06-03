using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class RPCController : MonoBehaviour
{

	private ServerScript severScript;
	public static RPCController instance;

	// Use this for initialization
	void Start()
	{
		instance = this;
		if(Network.isServer) {
			severScript = GameObject.Find("Server").GetComponent<ServerScript>();
		}
	}

	public void SendControllerOn() {
		networkView.RPC("Controller", RPCMode.Others);
	}

	public void SendControllerOff() {
		networkView.RPC("ControllerOut", RPCMode.Others);
	}

	public void SendScenes (List<FileInfo> Scenes) {
		MemoryStream o = new MemoryStream(); //Create something to hold the data
		
		BinaryFormatter bf = new BinaryFormatter(); //Create a formatter
		bf.Serialize(o, Scenes); //Save the list
		string data = Convert.ToBase64String(o.GetBuffer()); //Convert the data to a string
		
		networkView.RPC("ReciveScenes", RPCMode.Others, data);
	}
	
	public void StartScene (FileInfo Scene) {

		MemoryStream o = new MemoryStream(); //Create something to hold the data
		
		BinaryFormatter bf = new BinaryFormatter(); //Create a formatter
		bf.Serialize(o, Scene); //Save the list
		string data = Convert.ToBase64String(o.GetBuffer()); //Convert the data to a string

		networkView.RPC("ReciveStartScene", RPCMode.Others, data);
	}
	
	[RPC]
	void ReciveScenes (string data) {
		BinaryFormatter bf = new BinaryFormatter(); //Create a formatter
		//Reading it back in
		MemoryStream ins = new MemoryStream(Convert.FromBase64String(data)); //Create an input stream from the string
		//Read back the data
		List<FileInfo> x = bf.Deserialize(ins) as List<FileInfo>;
		print(x.Count);
		//BuildMenu.instance.scenes = x;
	}
	
	[RPC]
	void ReciveStartScene (string Data) {
		//Server Only
		BinaryFormatter bf = new BinaryFormatter(); //Create a formatter
		//Reading it back in
		MemoryStream ins = new MemoryStream(Convert.FromBase64String(Data)); //Create an input stream from the string
		//Read back the data
		FileInfo x = bf.Deserialize(ins) as FileInfo;

		PlayManager.PlayScene(x);
	}

	[RPC]
	void ConnectionSucces(string Username)
	{
		if(Network.isServer) {
			Notify.notify(Username);
//			print(Username);
		} else {

		}
	}

	[RPC]
	void Controller()
	{
		//Client only
	}
	
	[RPC]
	void ControllerOut()
	{
		//Client only
		//PlayController.controller = false;	
	}
    
	[RPC]
	void NextScene()
	{
		//severScript.DeviceName = DeviceName;
		if(GameManager.getGameState == GameState.Play) {
			print("Next Scene");
			PlayManager.NextScene();
		}
	}
    
	[RPC]
	void PreviousScene()
	{
		//severScript.DeviceName = DeviceName;
		if(GameManager.getGameState == GameState.Play) {
			print("Previous Scene");
			PlayManager.PreviousScene();
		}
	}
    
	[RPC]
	void PauzeScene()
	{
		//severScript.DeviceName = DeviceName;
		if(GameManager.getGameState == GameState.Play) {
			print("Pauze Scene");
		}
	}
	
	[RPC]
	void StopScene()
	{
		//severScript.DeviceName = DeviceName;
		if(GameManager.getGameState == GameState.Play) {
			GameManager.queGameState = GameState.PlayMenu;
			GameManager.SetGameStateBack();
		}
	}
}
