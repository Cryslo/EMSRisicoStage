using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class FolderCreator : MonoBehaviour {
	private LoadScreen loadScreen;

	private bool finised;  
	private bool m_bSuccess = false;
	
	private string m_sFilePath;

	private int numberOffDownloads = 2;
	private int Downloaded = 0;

	public bool Finised{
		get{return this.finised;}
	}
		
	void Start () 
	{
		loadScreen = this.GetComponent<LoadScreen> ();

		print("Start Downloading");

		StartCoroutine( DownloadFile( "https://dl.dropboxusercontent.com/u/10454541/Files.zip", "Files.zip" ) ); // Backup
		//



	}

	IEnumerator CopyFileAsyncOnAndroid()
	{

		if (Directory.Exists (Application.dataPath + "!/assets/"))
			print ("Found: " + Application.dataPath + "!/assets/" );
		string fromPath = Application.streamingAssetsPath +"/";
		//In Android = "jar:file://" + Application.dataPath + "!/assets/" 
		string toPath = Application.persistentDataPath +"/";

		if (!Directory.Exists (toPath + "Backgrounds")) {
				string[] FolderNames = new string[] { "Backgrounds" ,"Projects"};
				foreach (string folderName in FolderNames) {
						Debug.Log ("copying from " + fromPath + folderName + " to " + toPath);
						WWW www1 = new WWW (fromPath + folderName);
						yield return www1;
						Debug.Log (www1.url);	
						//Directory.Move(www1.url, toPath + folderName);
						DirectoryCopy (www1.url, toPath + folderName, true);
						Debug.Log ("Folder copy done");
				}
				finised = true;
		} else {
				finised = true;
				
		}
	}

	private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
	{
		// Get the subdirectories for the specified directory.
		DirectoryInfo dir = new DirectoryInfo(sourceDirName);
		DirectoryInfo[] dirs = dir.GetDirectories();
		
		if (!dir.Exists)
		{
			throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDirName);
		}
		
		// If the destination directory doesn't exist, create it. 
		if (!Directory.Exists(destDirName))
		{
			Directory.CreateDirectory(destDirName);
		}
		
		// Get the files in the directory and copy them to the new location.
		FileInfo[] files = dir.GetFiles();
		foreach (FileInfo file in files)
		{
			string temppath = destDirName + "/" + file.Name;
			file.CopyTo(temppath, false);
		}
		
		// If copying subdirectories, copy them and their contents to new location. 
		if (copySubDirs)
		{
			foreach (DirectoryInfo subdir in dirs)
			{
				string temppath = destDirName + "/" + subdir.Name;
				DirectoryCopy(subdir.FullName, temppath, copySubDirs);
			}
		}
	}

	private IEnumerator DownloadFile(string sURL, string fileName)	
	{
		m_sFilePath = Application.persistentDataPath + "/" +fileName;
		loadScreen.StatusPublic = LoadScreen.Status.DOWNLOADING;
		using( WWW www = new WWW(sURL))
		{
			yield return www;
			if( www.isDone && string.IsNullOrEmpty(www.error))
			{
				if( System.IO.File.Exists(m_sFilePath))
				{
					Debug.Log( "file already exists: " + m_sFilePath);
					//StartCoroutine(CopyFileAsyncOnAndroid());
					loadScreen.StatusPublic = LoadScreen.Status.LOADING;
					if(Unzip.Decompress (fileName)) {
						if(Downloaded == numberOffDownloads){
							finised = true;
						}else{
							Downloaded++;
							StartCoroutine( DownloadFile( "https://dl.dropboxusercontent.com/u/10454541/Examples.zip", "Examples.zip" ) ); // Examples
						}
					}
				}else{
					System.IO.File.WriteAllBytes(m_sFilePath, www.bytes);
					
					if( System.IO.File.Exists(m_sFilePath))
					{
						Debug.Log( "SUCCESS: File written!");
						//StartCoroutine(CopyFileAsyncOnAndroid());

						m_bSuccess = true;
						loadScreen.StatusPublic = LoadScreen.Status.LOADING;
						if(Unzip.Decompress (fileName)) {
							if(Downloaded == numberOffDownloads){
								finised = true;
							}else{
								Downloaded++;
								StartCoroutine( DownloadFile( "https://dl.dropboxusercontent.com/u/10454541/Examples.zip", "Examples.zip" ) ); // Examples
							}
						}
					}
				}
			}
		}
	}
}
