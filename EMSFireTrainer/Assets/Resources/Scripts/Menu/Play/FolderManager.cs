using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class FolderManager : MonoBehaviour
{
	
	private string directoryPath;
	private string savedDirectoryPath;
	private string savedBackgroundsPath;
	private string exampleDirectoryPath;
	private string exampleBackgroundsPath;
	private int addedBeforeFolders = 1;
	private string exampleDirectoryName = "Examples";
	private string savedDirectoryName = "Projects";
	private string savedBackgroundsName = "Backgrounds";
	private DirectoryInfo BgFolder;
	private DirectoryInfo DirectoryFolder;
	private DirectoryInfo exampleDirectoryFolder;
	private List<DirectoryInfo> folders;

	public List<DirectoryInfo> Folders {
		get{ return this.folders;}
	}
	
	public string SavedBackgroundsPath {
		get{ return this.savedBackgroundsPath;}
	}
	
	// Use this for initialization
	void Start()
	{
		// Setting the directoryPath to the assets folder
		this.directoryPath = Application.persistentDataPath;
		
		this.exampleDirectoryPath = directoryPath + "/" + exampleDirectoryName + "/" + savedDirectoryName;
		this.exampleBackgroundsPath = directoryPath + "/" + exampleDirectoryName + "/" + savedBackgroundsName;
		// Setting the savedDirectoryPath to the folder where all projects are stored
		this.savedDirectoryPath = directoryPath + "/" + savedDirectoryName;
		// Setting the savedBackgroundsPath to the folder where all Backgrounds are stored
		this.savedBackgroundsPath = directoryPath + "/" + savedBackgroundsName;
		// Setting the Directory folder to an variable
		this.DirectoryFolder = new DirectoryInfo(savedDirectoryPath);
		Debug.Log(DirectoryFolder.FullName);
		BuildFolderArray();
	}
	
	public List<DirectoryInfo> GetExamples()
	{
		List<DirectoryInfo> exampleList = new List<DirectoryInfo>();
		exampleDirectoryFolder = new DirectoryInfo(exampleDirectoryPath);
		
		if(exampleDirectoryFolder.Exists) {
			
			//Build Folder Array
			exampleList = new List<DirectoryInfo>();
			
			DirectoryInfo[] directoryItems = exampleDirectoryFolder.GetDirectories();
			
			foreach(DirectoryInfo folder in directoryItems)
				exampleList.Add(folder);
			
			return exampleList;
		}
		
		return exampleList;
	}
	
	// Use this for building the array of folders
	bool BuildFolderArray()
	{
		
		if(DirectoryFolder.Exists) {
			
			//Build Folder Array
			folders = new List<DirectoryInfo>();
			
			DirectoryInfo[] directoryItems = DirectoryFolder.GetDirectories();
			
			foreach(DirectoryInfo folder in directoryItems)
				folders.Add(folder);
			
			Debug.Log("Folders: " + directoryItems.Length);
			return true;
			
		} else {
			
			return false;
		}
		
		//			return false;
	}
	
	// Use this for rebuilding the array of folders
	public bool RebuildFolderArray()
	{
		//Build Folder Array
		folders = new List<DirectoryInfo>();
		
		DirectoryInfo[] directoryItems = DirectoryFolder.GetDirectories();
		
		foreach(DirectoryInfo folder in directoryItems)
			folders.Add(folder);
		
		return false;
	}
	
	// Use this for getting all folders made by the user
	public List<DirectoryInfo> GetFolders()
	{
		List<DirectoryInfo> TempFolders = new List<DirectoryInfo>();
		
		DirectoryInfo[] directoryItems = DirectoryFolder.GetDirectories();
		foreach(DirectoryInfo folder in directoryItems)
			TempFolders.Add(folder);
		
		return TempFolders;
	}
	
	// Use this for get al saved scenes in a project folder
	public List<FileInfo> GetScenesByName(string FolderName)
	{
		List<FileInfo> TempScenes = new List<FileInfo>();
		DirectoryInfo SelectedFolder;
		if(Directory.Exists(DirectoryFolder.FullName + "/" + FolderName)) {
			SelectedFolder = new DirectoryInfo(DirectoryFolder.FullName + "/" + FolderName);
		} else {
			SelectedFolder = new DirectoryInfo(exampleDirectoryFolder.FullName + "/" + FolderName);
		}
		//print(FolderName);
		
		FileInfo[] directoryItems = SelectedFolder.GetFiles();
		foreach(FileInfo Item in directoryItems)
		{
			if(Item.Name != ".DS_Store")
			{
				TempScenes.Add(Item);
			}
		}
		
		return TempScenes;
	}

	public string GetFirstSceneByFolderName(string FolderName)
	{
		List<FileInfo> TempScenes = new List<FileInfo>();
		TempScenes = GetScenesByName(FolderName);

		if(TempScenes.Count != 0)
		{
			return TempScenes[0].Name;
		}else{
			return "";
		}
	}
			
	
	// Use this for get al saved scenes in a project folder
	public List<FileInfo> GetScenesByNumber(int FolderNumber)
	{
		List<FileInfo> TempScenes = new List<FileInfo>();
		
		
		
		return TempScenes;
	}
	
	#region Folder Managment
	// Use this for Creating a file by Path
	public bool CreateFolderWithPath(string path)
	{
		Directory.CreateDirectory(path);
		RebuildFolderArray();	
		return false;
	}
	
	// Use this for Creating a file by Path
	public bool CreateFolder()
	{
		RebuildFolderArray();
		string path;
		int number = 0;
		string TempName = "Nameless";
		
		for(int i = 0; i < folders.Count; i++) {
			if(folders[i].Name == TempName) {
				number ++;
				TempName = "Nameless " + number;
				i = 0;
			}
		}
		
		path = savedDirectoryPath + "/" + TempName;
		Directory.CreateDirectory(path);
		RebuildFolderArray();	
		return false;
	}
	
	
	// Use this for Creating a file by Name
	public bool CreateFolderWithName(string name)
	{
		string path;
		path = savedDirectoryPath + "/" + name;
		Directory.CreateDirectory(path);
		RebuildFolderArray();	
		return false;
	}
	
	// Use this for Delete a file by Path
	public bool DeleteFolderWithPath(string path)
	{
		
		return false;
	}
	
	// Use this for Delete a file by Name
	public	bool DeleteFolderWithName(string name)
	{
		string path;
		path = savedDirectoryPath + "/" + name;
		Directory.Delete(path, true);
		RebuildFolderArray();	
		return false;
	}
	
	// Use this for Edit a file by Path
	public bool RenameFolderWithPath(string path)
	{
		
		return false;
	}
	
	// Use this for Edit a file by Name
	public bool RenameFolderWithID(int id, string newName, int idOffset)
	{
		RebuildFolderArray();
		int idPath = id - idOffset;
		
		string newPath = savedDirectoryPath + "/" + newName;
		string oldPath = folders[idPath].FullName;
		
		if(newPath != oldPath)
			Directory.Move(oldPath, newPath);
		
		return false;
	}
	#endregion
	
	// Use this to get how many backgrounds are in the folder
	public int GetBackgroundNumbers()
	{
		int tempNumber;
		
		BgFolder = new DirectoryInfo(savedBackgroundsPath);
		FileInfo[] FileArray = BgFolder.GetFiles("*.jpg");
		
		tempNumber = FileArray.Length;
		return tempNumber;
	}
	
	// Use this for getting a background by number
	public Texture2D GetBackgroundsByNumber(int BG_Number)
	{
		Texture2D tempTexture = new Texture2D(0, 0);
		
		tempTexture.LoadImage(File.ReadAllBytes(savedBackgroundsPath + "/" + BG_Number + ".jpg"));
		
		return tempTexture;
	}
	
	public List<Texture2D> GetBackgrounds()
	{
		List<Texture2D> tempBGList = new List<Texture2D>();
		Texture2D tempTexture;
		
		DirectoryInfo SelectedFolder = new DirectoryInfo(savedBackgroundsPath);
		
		FileInfo[] directoryItems = SelectedFolder.GetFiles("*.jpg");
		foreach(FileInfo Item in directoryItems) {
			tempTexture = new Texture2D(0, 0);
			tempTexture.LoadImage(File.ReadAllBytes(Item.FullName));
			tempBGList.Add(tempTexture);
		}
		
		//			print (tempBGList.Count);
		return tempBGList;
	}
	
	// Use this for getting a background by number
	public Texture2D GetRandomBackgrounds()
	{
		Texture2D tempTexture = new Texture2D(0, 0);
		int numberBackground;
		
		numberBackground = Random.Range(0, GetBackgroundNumbers());
		
		//tempTexture.LoadImage (File.ReadAllBytes (savedBackgroundsPath + "/" + 1 + ".jpg"));
		
		return tempTexture;
	}
	
	// Use this for getting a background by name
	public Texture2D GetBackgroundsByName(string name)
	{
		Texture2D tempTexture = new Texture2D(0, 0);

		Debug.Log(name.Remove(name.Length - 4));
		
		tempTexture.LoadImage(File.ReadAllBytes(savedBackgroundsPath + "/" + name.Remove(name.Length - 4) + ".jpg"));
		
		return tempTexture;
	}
	
	public void CopyByName(string name)
	{
		string path;
		string newPath;
		
		int number = 0;
		string TempName = name + "(Copy)";
		
		for(int i = 0; i < folders.Count; i++) {
			if(folders[i].Name == TempName) {
				number ++;
				TempName = name + " (Copy " + number + ")";
				i = 0;
			}
		}
		Debug.Log("Temp Name " + TempName);
		path = savedDirectoryPath + "/" + name;
		newPath = savedDirectoryPath + "/" + TempName;
		
		DirectoryCopy(path, newPath, true);
		RebuildFolderArray();
	}
	
	public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
	{
		Debug.Log("destDirName: " + destDirName);
		// Get the subdirectories for the specified directory.
		DirectoryInfo dir = new DirectoryInfo(sourceDirName);
		DirectoryInfo[] dirs = dir.GetDirectories();
		
		if(!dir.Exists) {
			throw new DirectoryNotFoundException(
				"Source directory does not exist or could not be found: "
				+ sourceDirName);
		}
		
		// If the destination directory doesn't exist, create it. 
		if(!Directory.Exists(destDirName)) {
			Directory.CreateDirectory(destDirName);
		}
		
		// Get the files in the directory and copy them to the new location.
		FileInfo[] files = dir.GetFiles();
		foreach(FileInfo file in files) {
			string temppath = destDirName + "/" + file.Name;
			file.CopyTo(temppath, false);
		}
		
		// If copying subdirectories, copy them and their contents to new location. 
		if(copySubDirs) {
			foreach(DirectoryInfo subdir in dirs) {
				string temppath = destDirName + "/" + subdir.Name;
				DirectoryCopy(subdir.FullName, temppath, copySubDirs);
			}
		}
	}
}
