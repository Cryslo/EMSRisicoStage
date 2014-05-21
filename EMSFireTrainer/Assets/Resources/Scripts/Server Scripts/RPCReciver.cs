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
	void NextScene()
	{
		//severScript.DeviceName = DeviceName;
		if(GameManager.getGameState == GameState.Play) {
			print("Next Scene");
		}
	}
    
	[RPC]
	void PreviousScene()
	{
		//severScript.DeviceName = DeviceName;
		if(GameManager.getGameState == GameState.Play) {
			print("Previous Scene");
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
		}
	}
}
