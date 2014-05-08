using UnityEngine;
using System;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.Collections.Generic;

using ICSharpCode.SharpZipLib.Zip;

public class Unzip : MonoBehaviour {

	private static string path;
	private static string zipName;
	private static string zipPath;

	public static bool Decompress(string name)
	{
		path = Application.persistentDataPath + "/";
		zipName = name;	
		zipPath = path + zipName;
		print(zipPath);
		if (File.Exists (zipPath)) {

			using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipPath))) {
				ZipEntry theEntry;
				while ((theEntry = s.GetNextEntry()) != null)
				{
					print(theEntry.Name);

					if(theEntry.IsDirectory)
					{
						//print(theEntry.Name);
						Directory.CreateDirectory(path + theEntry.Name );
					}
					if(theEntry.IsFile)
					{
						string filename = zipPath.Substring(0, zipPath.Length - zipName.Length);
						filename += theEntry.Name;
						Debug.Log("Unzipping: " + filename);
						using (FileStream streamWriter = File.Create(filename))
						{
							int size = 2048;
							byte[] fdata = new byte[2048];
							while (true)
							{
								size = s.Read(fdata, 0, fdata.Length);
								if (size > 0)
								{
									streamWriter.Write(fdata, 0, size);
								}
								else
								{
									break;
								}
							}
						}
					}
				}
				return true;
			}
		}
		return false;
	}
}

