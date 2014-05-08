using UnityEngine;
using System.Collections;

public class LoadScreen : MonoBehaviour {

	public enum Status{
		DOWNLOADING,
		LOADING
	}

	Status status;

	public Status StatusPublic{
		get{return this.status;}
		set{this.status = value;}
	}

	float seconds = 5F;

	string text_Status;

	FolderCreator folderCreator;
	bool done;


	// Use this for initialization
	void Start () {
		folderCreator = this.GetComponent<FolderCreator> ();
		
		StartCoroutine(WaitAndPrint(seconds));
	}
	
	// Update is called once per frame
	void Update () {
		if (folderCreator.Finised && done) {
			Application.LoadLevel ("Main");
		}

		if (status == Status.DOWNLOADING) {
			print("Downloading" + " " + "Backgrounds - Projects");
		}
		if (status == Status.LOADING) {
			print("Loading");
		}
	}

	IEnumerator WaitAndPrint(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		done = true;

	}
}
