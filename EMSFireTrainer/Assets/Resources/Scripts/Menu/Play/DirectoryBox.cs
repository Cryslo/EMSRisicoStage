using UnityEngine;
using System;
using System.Collections;
using System.Globalization;

namespace BlusSimulator {
	
	public class DirectoryBox {
		#region Variables
		private FolderBuilder folderBuilder;
		private FolderManager folderManager;
		
		private string directoryName;
		private string directorNumber;
		
		private bool editActive;
		private bool fileMode = false;
		private bool Protected = false;
		
		TouchScreenKeyboard keyboard;
		string newName;
		
		private float x;
		private float y;
		private float startY;
		private int boxNumber;
		#endregion
		
		#region Porperties
		public float X{
			set{this.x = value;}
			get{return this.x;}
		}
		
		public float Y{
			set{this.y = value;}
			get{return this.y;}
		}
		
		public float StartY{
			set{this.startY = value;}
			get{return this.startY;}
		}
		
		public int BoxNumber{
			set{this.boxNumber = value;}
			get{return this.boxNumber;}
		}
		
		public string DirectoryName{
			set{this.directoryName = value;}
			get{return this.directoryName;}
		}
		
		public string DirectorNumber{
			set{this.directorNumber = value;}
			get{return this.directorNumber;}
		}
		
		public bool FileMode {
			set{this.fileMode = value;}
			get{return this.fileMode;}
		}
		public bool ProtectedBox {
			set{this.Protected = value;}
			get{return this.Protected;}
		}
		
		public FolderManager FolderManagerScript{
			set{this.folderManager = value;}
		}
		
		public FolderBuilder folderBuilderScript{
			set{this.folderBuilder = value;}
		}
		#endregion
		
		// Use this for initialization
		public void Setup () {


		}
		
		public void Update()
		{
			if (editActive) {
				if (!Input.GetMouseButton(0)) {
					if (keyboard.done && !keyboard.wasCanceled) {
						DirectoryClickEdit (keyboard.text);
					}
					if (keyboard.active) {
						newName = keyboard.text;
					}
				}
			}
		}
		
		// Call this Function when clicked on the box
		private void DirectoryClick()
		{

		}
		
		// Call this Function when clicked on the Deletebox
		private void DirectoryDelete()
		{
			Debug.Log ("Delete_Folder: " + directoryName);
			folderManager.DeleteFolderWithName (directoryName);
			folderBuilder.rebuildFolderArray ();
		}
		
		// Call this Function when clicked on the Editbox
		private void DirectoryEdit()
		{
			Debug.Log ("Edit_Folder: " + directoryName);
			buildEdit ();
			//folderBuilder.rebuildArray ();
		}
		// Call this Function when clicked on the duplicate
		private void DirectoryDuplicate()
		{
			Debug.Log ("Duplicate_Folder: " + directoryName);
			folderManager.CopyByName (directoryName);
			folderBuilder.rebuildFolderArray ();
			//folderBuilder.rebuildArray ();
		}
		
		
		public void buildEdit(){
			editActive = true;
			keyboard = (TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false));
		}
		
		void DirectoryClickEdit (string newName)
		{
			editActive = false;
			Debug.Log ("");
			folderManager.RenameFolderWithID (boxNumber, newName, folderBuilder.ExampleProjects.Count);
			folderBuilder.rebuildFolderArray ();
		}
		
		// Call this Function when clicked on the Playbox
		private void DirectoryPlay()
		{
			Debug.Log ("Play_Folder: " + directoryName);
		}
		
		// Call this Function to create this box
		public void DirectoryCreate () {
			Debug.Log ("Create_Folder");
			folderManager.CreateFolder();
			folderBuilder.rebuildFolderArray ();
		}
		
		// Call this Function to create this box
		public void DirectoryBack () {
			Debug.Log("Back");
			folderBuilder.CloseFolder ();
		}
		
		// Call this Function to create this box
		public void show () {
			Debug.Log ("Show_Folder");
		}
		
		// Call this Function to remove this box
		public void Remove () {

		}
		
		// Call this Function when Selected
		public void Select () {
			Debug.Log ("Select_Folder");
		}
		
		// Call this Function when Select an other one
		public void Unselect () {

		}
	}
}
