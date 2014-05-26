using UnityEngine;
using System.Collections;

public class ServerScript : MonoBehaviour
{
	
	private PasswordGui passGui;

	private LANBroadcastService broadcastSever;

	public static string username = "";
	public static string password = "";

	private int number;
	private int lengthOfPassword = 4;
	private static bool created = false;

	// Use this for initialization
	void Start ()
	{
		if (!created) {
			// this is the first instance - make it persist
			DontDestroyOnLoad(this.gameObject);
			created = true;
		} else {
			// this must be a duplicate from a scene reload - DESTROY!
			Destroy(this.gameObject);
		}
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
		print("Player Connected");
	}

    IEnumerator waitAndDestroy() {
        float waitForSeconds = 5.0f;
        Debug.Log("Wait for " + waitForSeconds + " Seconds");
        yield return new WaitForSeconds(waitForSeconds);
        passGui.DeleteGui();
    }


}
