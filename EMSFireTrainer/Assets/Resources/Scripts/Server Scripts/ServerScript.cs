using UnityEngine;
using System.Collections;

public class ServerScript : MonoBehaviour
{
	
	private PasswordGui passGui;

	private string ip = "127.0.0.1";
	private LANBroadcastService broadcastSever;

	public static string username = "";
	public static string password = "";

	private int number;
	private int lengthOfPassword = 4;

	// Use this for initialization
	void Start ()
	{
		broadcastSever = this.GetComponent<LANBroadcastService> ();

		for (int i = 0; i < lengthOfPassword; i++) {
			number = Random.Range (0, 9);
			password += number.ToString ();
		}
		
		LaunchServer (password);
	}
    
	void LaunchServer (string pass)
	{
		broadcastSever.StartAnnounceBroadCasting ();
		Network.incomingPassword = pass;
		Network.InitializeServer (1, 25000);
	}
	
	void OnServerInitialized ()
	{
		Debug.Log ("Server initialized and ready");
		passGui = new PasswordGui(password);
		StartCoroutine("waitAndDestroy");
	}

	void OnPlayerConnected (NetworkPlayer player)
	{
		passGui.DeleteGui();
	}

    IEnumerator waitAndDestroy() {
        float waitForSeconds = 5.0f;
        Debug.Log("Wait for " + waitForSeconds + " Seconds");
        yield return new WaitForSeconds(waitForSeconds);
        passGui.DeleteGui();
    }


}
