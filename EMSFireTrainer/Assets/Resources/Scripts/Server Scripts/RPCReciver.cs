using UnityEngine;
using System.Collections;

public class RPCReciver : MonoBehaviour
{

	private ServerScript severScript;

	// Use this for initialization
	void Start ()
	{
		if (Network.isServer) {
			severScript = GameObject.Find ("Server").GetComponent<ServerScript> ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	[RPC]
	void ConnectionSucces (string Username)
	{
		Notify.notify(Username);
	}
}
