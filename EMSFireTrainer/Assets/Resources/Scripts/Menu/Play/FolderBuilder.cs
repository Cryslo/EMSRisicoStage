using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class FolderBuilder : MonoBehaviour {
	static public FolderBuilder instance;

	private bool guestmode = false;
	private bool loggedin;

	private GameObject Folders;
	
	private Camera MainCamera;
	private FolderManager folderManager;
	private bool FolderLoaded = false;
	
	private List<DirectoryInfo> exampleProjects;
	
	public List<DirectoryInfo> ExampleProjects {
		get{return this.exampleProjects;}
	}
	
	private List<DirectoryInfo> buildedFolderArray;
	private DirectoryInfo CreateFolder = new DirectoryInfo("/");
	private List<FileInfo> buildedFileArray;
	
	private int boxesX = 5;
	private int maxBoxesX;
	
	private int scaling;
	
	private List<DirectoryBox> boxesDirectoryBox; 	
	private DirectoryBox directoryBox;
	
	private int openFolder;
	private bool folderOpen;
	
	private Rect scrollPanel;
	private float scrollOffset;
	
	private Vector2 scrollOffset2;

	private int columns;
	
	private float mouseStart;
	private float startpointX;
	private float startpointY;
	private float finalDirectoryWidth;
	private float finalDirectoryHight;
	
	// Use this for initialization
	void Start () {
		instance = this;
		folderManager = this.GetComponent<FolderManager> ();
		MainCamera = GameObject.Find ("Main Camera").camera;
		exampleProjects = new List<DirectoryInfo> ();
		
		exampleProjects.Add (CreateFolder);
		
		foreach (DirectoryInfo folder in folderManager.GetExamples())
			exampleProjects.Add (folder);
	}
	
	void Update() {
		//TODO als hij vast komt kan hij niet meer naar beneden meteen
		if (boxesDirectoryBox != null) {
			for (int i = 0; i < boxesDirectoryBox.Count; i++) {
				boxesDirectoryBox[i].Update();
			}
		}
		
		if (scrollPanel != null) {
			if(boxesDirectoryBox != null)
			{
				if(Input.GetMouseButtonDown(0))
				{
					Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
						Debug.Log("Test");
					startpointX = Input.mousePosition.x;
					startpointY = Input.mousePosition.y;
					for (int i = 0; i < boxesDirectoryBox.Count; i++) {
						boxesDirectoryBox[i].StartY = boxesDirectoryBox[i].BoxY;
						
						boxesDirectoryBox[i].NewStartY = boxesDirectoryBox[i].TestY;
					}
				}
				if(Input.GetMouseButton(0))
				{
					/*if(boxesDirectoryBox[0].Y > (finalDirectoryHight/2) && (startpointY - Input.mousePosition.y) > 0)
					{
						
					}else if(boxesDirectoryBox[boxesDirectoryBox.Count - 1].Y < Screen.height - (finalDirectoryHight/2) && (startpointY - Input.mousePosition.y) < 0) {
						
						
					}else{
						//print(boxesDirectoryBox[boxesDirectoryBox.Count - 1].Y);
						for (int i = 0; i < boxesDirectoryBox.Count; i++) {
							boxesDirectoryBox[i].BoxY = boxesDirectoryBox[i].StartY - (startpointY - Input.mousePosition.y);
							boxesDirectoryBox[i].TestY = boxesDirectoryBox[i].NewStartY + (startpointY - Input.mousePosition.y);
						}
					}*/
					for (int i = 0; i < boxesDirectoryBox.Count; i++) {
						boxesDirectoryBox[i].BoxY = boxesDirectoryBox[i].StartY - (startpointY - Input.mousePosition.y);
						boxesDirectoryBox[i].TestY = boxesDirectoryBox[i].NewStartY + (startpointY - Input.mousePosition.y);
					}
				}
			}
		}
	}
	
	// Use this for rebuilding the array
	public bool rebuildFolderArray () {

		if(boxesDirectoryBox != null)
		{
			for (int i = 0; i < boxesDirectoryBox.Count; i++) {
				boxesDirectoryBox[i].Remove();
				
			}
		}
		buildedFolderArray = new List<DirectoryInfo> ();
		
		foreach (DirectoryInfo folder in folderManager.GetFolders())
			buildedFolderArray.Add (folder);
		//buildedFolderArray += folderManager.GetFolders();
		rebuildGUI ();
		return false;
	}
	
	public bool rebuildSceneArray (string FolderName) {
		if(boxesDirectoryBox != null)
		{
			for (int i = 0; i < boxesDirectoryBox.Count; i++) {
				boxesDirectoryBox[i].Remove();
			}
		}
		
		buildedFileArray = new List<FileInfo> ();
		//buildedFileArray.Add (CreateFolder);
		
		foreach (FileInfo file in folderManager.GetScenesByName(FolderName))
			buildedFileArray.Add (file);
		
		Debug.Log("Build Files: " + buildedFileArray.Count);
		rebuildGUI ();
		return false;
	}

	void OnGUI() {
		if(boxesDirectoryBox != null)
		{
			//GUI.Box(scrollPanel, "");
			for (int i = 0; i < boxesDirectoryBox.Count; i++) {
				//GUI.Box(boxesDirectoryBox[i].BoxCollider, boxesDirectoryBox[i].DirectoryName);
			}
		}
	}
	
	bool rebuildGUI () {
		
		Destroy(Folders.gameObject);
		Folders = new GameObject();
		Folders.name = "Folders";


		
		boxesDirectoryBox = new List<DirectoryBox> ();
		
		float finalScale = 1;//checkScale();
		
		while (finalScale > 1.5f) {
			boxesX++;
			finalScale = 1;//checkScale();
		}
		
		finalDirectoryWidth = 200 * finalScale; //* menuScript.getXScale();
		finalDirectoryHight = 200 * finalScale; //* menuScript.getYScale();

		columns = 3;
		
		if (!folderOpen) {
			for (int j = 0; j < exampleProjects.Count; j++) {
				directoryBox = new DirectoryBox ();
				directoryBox.folderBuilderScript = this;
				directoryBox.FolderManagerScript = this.folderManager;
				directoryBox.Parent = Folders;

				directoryBox.DirectorNumber = j.ToString ();
				directoryBox.DirectoryName = exampleProjects[j].Name;
				
				directoryBox.ProtectedBox = true;

				directoryBox.X = directoryBox.X = (j - (j / columns) * columns);   
				directoryBox.Y = directoryBox.Y = (j / columns);
				//print( Mathf.Round(((j - 1)%2) + 1));
				//directoryBox.X = (((Mathf.Round((j)/2))) * finalDirectoryWidth) + (finalDirectoryWidth / 2);
				//directoryBox.Y = ((Screen.height - (finalDirectoryWidth * 2)) - ((2 - ((int)(((j)%2)) + 1f) * finalDirectoryWidth ) + (finalDirectoryHight /2)));
				directoryBox.BoxNumber = j;
				
				directoryBox.Setup ();
				boxesDirectoryBox.Add (directoryBox);
			}
			
			for (int i = 0; i < buildedFolderArray.Count; i++) {
				int number = exampleProjects.Count + i;
				
				directoryBox = new DirectoryBox ();
				directoryBox.folderBuilderScript = this;
				directoryBox.FolderManagerScript = this.folderManager;
				directoryBox.Parent = Folders;
				
				directoryBox.DirectorNumber = number.ToString ();
				directoryBox.DirectoryName = buildedFolderArray [i].Name;
				
				if(guestmode)
				{
					directoryBox.ProtectedBox = true;
				}
				directoryBox.X = directoryBox.X = (number - (number / columns) * columns);   
				directoryBox.Y = directoryBox.Y = (number / columns);
				//directoryBox.X = (((Mathf.Round((number)/2))) * finalDirectoryWidth) + (finalDirectoryWidth / 2);
				//directoryBox.Y = ((Screen.height - (finalDirectoryWidth * 2)) - ((2 - ((int)(((number)%2)) + 1f) * finalDirectoryWidth ) + (finalDirectoryHight /2)));
				directoryBox.BoxNumber = number;
				
				directoryBox.Setup ();
				boxesDirectoryBox.Add (directoryBox);
			}
		} else {
			//box 0
			directoryBox = new DirectoryBox ();
			directoryBox.folderBuilderScript = this;
			directoryBox.FolderManagerScript = this.folderManager;
			directoryBox.Parent = Folders;
			
			directoryBox.DirectorNumber = "0";
			directoryBox.DirectoryName = "Back";
			
			directoryBox.FileMode = true;
			
			if(guestmode)
			{
				directoryBox.ProtectedBox = true;
			}
			
			directoryBox.X = directoryBox.X = (0 - (0 / columns) * columns);   
			directoryBox.Y = directoryBox.Y = (0 / columns);
			//directoryBox.X = (((Mathf.Round((0)/2))) * finalDirectoryWidth) + (finalDirectoryWidth / 2);
			//directoryBox.Y = ((Screen.height - (finalDirectoryWidth * 2)) - ((2 - ((int)(((0)%2)) + 1f) * finalDirectoryWidth ) + (finalDirectoryHight /2)));
			directoryBox.BoxNumber = 0;
			
			directoryBox.Setup ();
			boxesDirectoryBox.Add (directoryBox);
			
			for (int i = 0; i < buildedFileArray.Count; i++) {
				
				int number = i + 1;
				
				directoryBox = new DirectoryBox ();
				directoryBox.folderBuilderScript = this;
				directoryBox.FolderManagerScript = this.folderManager;
				directoryBox.Parent = Folders;
				
				directoryBox.DirectorNumber = number.ToString ();
				directoryBox.DirectoryName = buildedFileArray [i].Name;
				
				directoryBox.FileMode = true;
				
				directoryBox.X = directoryBox.X = (i - (i / columns) * columns);   
				directoryBox.Y = directoryBox.Y = (i / columns);
				//directoryBox.X = (((Mathf.Round((number)/2))) * finalDirectoryWidth) + (finalDirectoryWidth / 2);
				//directoryBox.Y = ((Screen.height - (finalDirectoryWidth * 2)) - ((2 - ((int)(((number)%2)) + 1f) * finalDirectoryWidth ) + (finalDirectoryHight /2)));
				directoryBox.BoxNumber = number;
				
				directoryBox.Setup ();
				boxesDirectoryBox.Add (directoryBox);
			}
		}
		scrollPanel = new Rect(Screen.width - ((Screen.width / 4) * columns), 0, ((Screen.width / 4) * columns), boxesDirectoryBox.Count * ((Screen.height / 4)));
		return false;
		
	}
	
	public bool OpenFolder(int FolderNumber, string FolderName) {
		openFolder = FolderNumber;
		folderOpen = true;
		rebuildSceneArray (FolderName);
		return true;
	}
	
	public  bool CloseFolder () {
		folderOpen = false;
		rebuildFolderArray ();
		return true;
	}
	public void unselect() {
		for (int i = 0; i < boxesDirectoryBox.Count; i++) {
			boxesDirectoryBox[i].Unselect();
		}
	}
	
	public bool OpenLoading()
	{
		rebuildFolderArray ();
		
		return false;
	}
	
	public bool CloseLoading()
	{
		if(boxesDirectoryBox != null)
		{
			for (int i = 0; i < boxesDirectoryBox.Count; i++) {
				boxesDirectoryBox[i].Remove();
				boxesDirectoryBox[i] = null;
			}
		}
		boxesDirectoryBox = null;
		Destroy(Folders);

		return false;
	}
	
	public int RandomNumber(int max, int min)
	{
		int result;
		result = Random.Range(min, max);
		return result;
	}
	
}