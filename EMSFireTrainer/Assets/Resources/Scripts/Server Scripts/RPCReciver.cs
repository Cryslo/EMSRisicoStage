using UnityEngine;
using System.Collections;

public class RPCReciver : MonoBehaviour
{

	private ServerScript severScript;

	// Use this for initialization
	void Start()
	{
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
