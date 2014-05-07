using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace BlusSimulator {
	public class FolderBuilder : MonoBehaviour {
		private bool guestmode = false;
		private bool loggedin;
		
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
		
		private float mouseStart;
		private float startpointX;
		private float startpointY;
		private float finalDirectoryWidth;
		private float finalDirectoryHight;
		
		// Use this for initialization
		void Start () {
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
						if(Physics.Raycast(ray, out hit, Mathf.Infinity))
						{
							startpointX = Input.mousePosition.x;
							startpointY = Input.mousePosition.y;
							for (int i = 0; i < boxesDirectoryBox.Count; i++) {
								boxesDirectoryBox[i].StartY = boxesDirectoryBox[i].Y;
							}
							
						}
					}
					if(Input.GetMouseButton(0))
					{
						if(boxesDirectoryBox[0].Y > (finalDirectoryHight/2) && (startpointY - Input.mousePosition.y) > 0)
						{
							
						}else if(boxesDirectoryBox[boxesDirectoryBox.Count - 1].Y < Screen.height - (finalDirectoryHight/2) && (startpointY - Input.mousePosition.y) < 0) {
							
							
						}else{
							//print(boxesDirectoryBox[boxesDirectoryBox.Count - 1].Y);
							for (int i = 0; i < boxesDirectoryBox.Count; i++) {
								boxesDirectoryBox[i].Y = boxesDirectoryBox[i].StartY + (startpointY - Input.mousePosition.y);
							}
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
		
		bool rebuildGUI () {
			
			boxesDirectoryBox = new List<DirectoryBox> ();
			
			float finalScale = 1;//checkScale();
			
			while (finalScale > 1.5f) {
				boxesX++;
				finalScale = 1;//checkScale();
			}
			
			finalDirectoryWidth = 200 * finalScale; //* menuScript.getXScale();
			finalDirectoryHight = 200 * finalScale; //* menuScript.getYScale();
			
			if (!folderOpen) {
				for (int j = 0; j < exampleProjects.Count; j++) {
					directoryBox = new DirectoryBox ();
					directoryBox.folderBuilderScript = this;
					directoryBox.FolderManagerScript = this.folderManager;
					
					directoryBox.DirectorNumber = j.ToString ();
					directoryBox.DirectoryName = exampleProjects[j].Name;
					
					directoryBox.ProtectedBox = true;
					
					print( Mathf.Round(((j - 1)%2) + 1));
					directoryBox.X = (((Mathf.Round((j)/2))) * finalDirectoryWidth) + (finalDirectoryWidth / 2);
					directoryBox.Y = ((Screen.height - (finalDirectoryWidth * 2)) - ((2 - ((int)(((j)%2)) + 1f) * finalDirectoryWidth ) + (finalDirectoryHight /2)));
					directoryBox.BoxNumber = j;
					
					directoryBox.Setup ();
					boxesDirectoryBox.Add (directoryBox);
				}
				
				for (int i = 0; i < buildedFolderArray.Count; i++) {
					int number = exampleProjects.Count + i;
					
					directoryBox = new DirectoryBox ();
					directoryBox.folderBuilderScript = this;
					directoryBox.FolderManagerScript = this.folderManager;
					
					directoryBox.DirectorNumber = number.ToString ();
					directoryBox.DirectoryName = buildedFolderArray [i].Name;
					
					if(guestmode)
					{
						directoryBox.ProtectedBox = true;
					}
					
					
					directoryBox.X = (((Mathf.Round((number)/2))) * finalDirectoryWidth) + (finalDirectoryWidth / 2);
					directoryBox.Y = ((Screen.height - (finalDirectoryWidth * 2)) - ((2 - ((int)(((number)%2)) + 1f) * finalDirectoryWidth ) + (finalDirectoryHight /2)));
					directoryBox.BoxNumber = number;
					
					directoryBox.Setup ();
					boxesDirectoryBox.Add (directoryBox);
				}
			} else {
				//box 0
				directoryBox = new DirectoryBox ();
				directoryBox.folderBuilderScript = this;
				directoryBox.FolderManagerScript = this.folderManager;
				
				directoryBox.DirectorNumber = "0";
				directoryBox.DirectoryName = "Back";
				
				directoryBox.FileMode = true;
				
				if(guestmode)
				{
					directoryBox.ProtectedBox = true;
				}
				
				
				directoryBox.X = (((Mathf.Round((0)/2))) * finalDirectoryWidth) + (finalDirectoryWidth / 2);
				directoryBox.Y = ((Screen.height - (finalDirectoryWidth * 2)) - ((2 - ((int)(((0)%2)) + 1f) * finalDirectoryWidth ) + (finalDirectoryHight /2)));
				directoryBox.BoxNumber = 0;
				
				directoryBox.Setup ();
				boxesDirectoryBox.Add (directoryBox);
				
				for (int i = 0; i < buildedFileArray.Count; i++) {
					
					int number = i + 1;
					
					directoryBox = new DirectoryBox ();
					directoryBox.folderBuilderScript = this;
					directoryBox.FolderManagerScript = this.folderManager;
					
					directoryBox.DirectorNumber = number.ToString ();
					directoryBox.DirectoryName = buildedFileArray [i].Name;
					
					directoryBox.FileMode = true;
					
					
					directoryBox.X = (((Mathf.Round((number)/2))) * finalDirectoryWidth) + (finalDirectoryWidth / 2);
					directoryBox.Y = ((Screen.height - (finalDirectoryWidth * 2)) - ((2 - ((int)(((number)%2)) + 1f) * finalDirectoryWidth ) + (finalDirectoryHight /2)));
					directoryBox.BoxNumber = number;
					
					directoryBox.Setup ();
					boxesDirectoryBox.Add (directoryBox);
				}
			}
			return false;
			
		}
		
		public bool OpenFolder(int FolderNumber, string FolderName) {
			openFolder = FolderNumber;
			folderOpen = true;
			rebuildSceneArray (FolderName);
			return true;
		}
		
		public bool CloseFolder () {
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
			for (int i = 0; i < boxesDirectoryBox.Count; i++) {
				boxesDirectoryBox[i].Remove();
			}
			return false;
		}
		
		public int RandomNumber(int max, int min)
		{
			int result;
			result = Random.Range(min, max);
			return result;
		}
		
	}
}