using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Schema;
using System.Text;

struct Fire{
	public GameObject obj;
	public bool expandable;
	public int ExtinguishAgent;
}

public class XmlBehaviour : MonoBehaviour {
	private static string xmlDefaultPath;
	private static string projectsMapName = "/Projects";
	private static string exampleMapName = "/Examples";
	private static string tempName = "/Temp.xml";

	private static List<GameObject> gameObjects = new List<GameObject> ();
	private static List<Fire> fireObjects = new List<Fire> ();

	private static List<GameObject> loadedGameObjects = new List<GameObject> ();
	private static List<Fire> loadedFireObjects = new List<Fire> ();

	private static Fire tempFire = new Fire();

	private static XmlWriter xmlDoc;
	private static XmlWriterSettings writerSettings;

	private static XmlDocument doc;


	private void Awake() {
		xmlDefaultPath = Application.persistentDataPath;

		writerSettings = new XmlWriterSettings();
		writerSettings.Indent = true;
		writerSettings.OmitXmlDeclaration = true;
		writerSettings.NewLineOnAttributes = true;

		CreateScene ();

	}

	public static void CreateScene() {
		gameObjects = new List<GameObject> ();
		fireObjects = new List<Fire> ();
	}

	public static void DeleteScene() {
	}

	public static void SaveScene(bool timerActive, float scenelength, bool timerVisable, int audioNumber, string backgroundName) {
		xmlDoc = XmlWriter.Create((xmlDefaultPath + projectsMapName + tempName), writerSettings);
		xmlDoc.WriteStartDocument();
		xmlDoc.WriteStartElement("Scene");

		xmlDoc.WriteStartElement("SceneProperties");
		xmlDoc.WriteElementString("TimerActive", timerActive.ToString());
		xmlDoc.WriteElementString("Scenelength", scenelength.ToString());
		xmlDoc.WriteElementString("TimerVisable", timerVisable.ToString());
		xmlDoc.WriteElementString("AudioNumber", audioNumber.ToString());
		xmlDoc.WriteElementString("BackgroundName", backgroundName.ToString());

		xmlDoc.WriteEndElement ();
		
		xmlDoc.WriteStartElement("Fires");
		#region FireObjects
		foreach (Fire FireObj in fireObjects) {
			xmlDoc.WriteStartElement("Fire");
			//Save the name
			xmlDoc.WriteElementString("Name", FireObj.obj.name);
			
			//Save position of the object
			xmlDoc.WriteStartElement("Position");
			xmlDoc.WriteElementString("posX", FireObj.obj.transform.position.x.ToString());
			xmlDoc.WriteElementString("posY", FireObj.obj.transform.position.y.ToString());
			xmlDoc.WriteElementString("posZ", FireObj.obj.transform.position.z.ToString());
			xmlDoc.WriteEndElement();
			
			//Save rotatie
			xmlDoc.WriteStartElement("Rotation");
			xmlDoc.WriteElementString("RotationX", FireObj.obj.transform.rotation.x.ToString());
			xmlDoc.WriteElementString("RotationY", FireObj.obj.transform.rotation.y.ToString());
			xmlDoc.WriteElementString("RotationZ", FireObj.obj.transform.rotation.z.ToString());
			xmlDoc.WriteEndElement();
			
			//Save if fire is Expandable
			xmlDoc.WriteElementString("Expanding", FireObj.expandable.ToString());
			
			//Save Extinguish Agent
			xmlDoc.WriteElementString("ExtinguishAgent", FireObj.ExtinguishAgent.ToString());
			
			xmlDoc.WriteEndElement();
		}
		#endregion
		xmlDoc.WriteEndElement();

		xmlDoc.WriteStartElement("GameObjects");

		#region gameObjects
		foreach (GameObject obj in gameObjects) {
			xmlDoc.WriteStartElement("GameObject");
			//Save the name
			xmlDoc.WriteElementString("Name", obj.name);

			//Save position of the object
			xmlDoc.WriteStartElement("Position");
				xmlDoc.WriteElementString("posX", obj.transform.position.x.ToString());
				xmlDoc.WriteElementString("posY", obj.transform.position.y.ToString());
				xmlDoc.WriteElementString("posZ", obj.transform.position.z.ToString());
			xmlDoc.WriteEndElement();

			//Save rotatie
			xmlDoc.WriteStartElement("Rotation");
				xmlDoc.WriteElementString("RotationX", obj.transform.rotation.x.ToString());
				xmlDoc.WriteElementString("RotationY", obj.transform.rotation.y.ToString());
				xmlDoc.WriteElementString("RotationZ", obj.transform.rotation.z.ToString());
			xmlDoc.WriteEndElement();

			xmlDoc.WriteEndElement();
		}
		#endregion

		xmlDoc.WriteEndElement();
		xmlDoc.WriteEndElement();
		xmlDoc.WriteEndDocument ();
		xmlDoc.Flush();
		xmlDoc.Close ();
	}
	
	public static void SaveObject(GameObject obj) {
		gameObjects.Add (obj);
	}

	public static void SaveObject(GameObject obj, bool expandable, int ExtinguishAgent) {
		tempFire = new Fire ();
		tempFire.obj = obj;
		tempFire.expandable = expandable;
		tempFire.ExtinguishAgent = ExtinguishAgent;

		fireObjects.Add (tempFire);
	}

	public static void LoadScene(string path) {
		doc = new XmlDocument();

		XmlReaderSettings ReaderSettings = new XmlReaderSettings();
		ReaderSettings.IgnoreWhitespace = true;

		// Create an XmlReader
		using (XmlReader reader = XmlReader.Create(xmlDefaultPath + projectsMapName + tempName, ReaderSettings))
		{
			doc.Load(reader);
			XmlElement root = doc.DocumentElement;
			// Create an XPathNavigator to use for the transform.
			XPathNavigator nav = root.CreateNavigator();

			#region SceneProperties
			XPathNodeIterator scenePropertiesNodus = nav.Select("/Scene/SceneProperties"); //Het pat dat hij moet afleggen in de XML File voor dat hij bij de juiste nodes is.
			while(scenePropertiesNodus.MoveNext()){ // Terwijl die elke keer volgende doet doet hij het volgende.
				XPathNavigator nodesNavigator = scenePropertiesNodus.Current; // hij begint vanaf het begin waar hij het path krijgt.
				
				XPathNodeIterator nodesText = nodesNavigator.SelectDescendants(XPathNodeType.Text, false);//maakt een variable aan die je kunt opvragen wat voor text er in staat.
				XPathNodeIterator scenePropertiesElement = nodesNavigator.SelectDescendants(XPathNodeType.Element, false);
				
				
				while (scenePropertiesElement.MoveNext()) //pakt de volgende text.
				{
					//itemNodusElement.MoveNext();
					//Debug.Log(itemNodusElement.Current.Name);
					switch (scenePropertiesElement.Current.Name) {
					case "TimerActive":
						print(scenePropertiesElement.Current.Value);
						break;
					case "Scenelength":
						print(scenePropertiesElement.Current.Value);
						break;
					case "TimerVisable":
						print(scenePropertiesElement.Current.Value);
						break;
					case "AudioNumber":		
						print(scenePropertiesElement.Current.Value);			
						break;
					case "BackgroundName":
						print(scenePropertiesElement.Current.Value);
						break;
						
					default:
						
						break;
					}
					
				}
			}
			#endregion

			#region SceneProperties
			XPathNodeIterator firesNodus = nav.Select("/Scene/Fires/Fire"); //Het pat dat hij moet afleggen in de XML File voor dat hij bij de juiste nodes is.
			while(firesNodus.MoveNext()){ // Terwijl die elke keer volgende doet doet hij het volgende.

				float posX = 0;
				float posY = 0;
				float posZ = 0;

				float rotationX = 0;
				float rotationY = 0;
				float rotationZ = 0;

				XPathNavigator nodesNavigator = firesNodus.Current; // hij begint vanaf het begin waar hij het path krijgt.
				
				XPathNodeIterator nodesText = nodesNavigator.SelectDescendants(XPathNodeType.Text, false);//maakt een variable aan die je kunt opvragen wat voor text er in staat.
				XPathNodeIterator FiresElement = nodesNavigator.SelectDescendants(XPathNodeType.Element, false);
				
				tempFire = new Fire();
				tempFire.obj = new GameObject();
				
				while (FiresElement.MoveNext()) //pakt de volgende text.
				{

					//itemNodusElement.MoveNext();
					switch (FiresElement.Current.Name) {
					case "Name":
						tempFire.obj.name = FiresElement.Current.Value;
						break;
					case "posX":
						posX = float.Parse(FiresElement.Current.Value);
						break;
					case "posY":
						print("Test: " + FiresElement.Current.Value);
						posY = float.Parse(FiresElement.Current.Value);
						break;
					case "posZ":
						posZ = float.Parse(FiresElement.Current.Value);
						break;
					case "RotationX":
						rotationX = float.Parse(FiresElement.Current.Value);
						break;
					case "RotationY":
						rotationY = float.Parse(FiresElement.Current.Value);
						break;
					case "RotationZ":
						rotationZ = float.Parse(FiresElement.Current.Value);
						break;
					case "Expanding":
						tempFire.expandable = bool.Parse(FiresElement.Current.Value);
						break;
					case "ExtinguishAgent":
						tempFire.ExtinguishAgent = int.Parse(FiresElement.Current.Value);
						break;
					}

					
				}
				
				print("Test");
				tempFire.obj.transform.position = new Vector3(posX, posY, posZ);
				tempFire.obj.transform.rotation = new Quaternion(rotationX, rotationY, rotationZ,0);
				loadedFireObjects.Add(tempFire);
			}
			#endregion

			#region SceneProperties
			XPathNodeIterator gameObjectNodus = nav.Select("/Scene/GameObjects/GameObject"); //Het pat dat hij moet afleggen in de XML File voor dat hij bij de juiste nodes is.
			while(gameObjectNodus.MoveNext()){ // Terwijl die elke keer volgende doet doet hij het volgende.
				GameObject tempObj;

				float posX = 0;
				float posY = 0;
				float posZ = 0;
				
				float rotationX = 0;
				float rotationY = 0;
				float rotationZ = 0;
				
				XPathNavigator nodesNavigator = gameObjectNodus.Current; // hij begint vanaf het begin waar hij het path krijgt.
				
				XPathNodeIterator nodesText = nodesNavigator.SelectDescendants(XPathNodeType.Text, false);//maakt een variable aan die je kunt opvragen wat voor text er in staat.
				XPathNodeIterator gameObjectElement = nodesNavigator.SelectDescendants(XPathNodeType.Element, false);

				tempObj = new GameObject();
				
				while (gameObjectElement.MoveNext()) //pakt de volgende text.
				{
					
					//itemNodusElement.MoveNext();
					switch (gameObjectElement.Current.Name) {
					case "Name":
						tempObj.name = gameObjectElement.Current.Value;
						break;
					case "posX":
						posX = float.Parse(gameObjectElement.Current.Value);
						break;
					case "posY":
						print("Test: " + gameObjectElement.Current.Value);
						posY = float.Parse(gameObjectElement.Current.Value);
						break;
					case "posZ":
						posZ = float.Parse(gameObjectElement.Current.Value);
						break;
					case "RotationX":
						rotationX = float.Parse(gameObjectElement.Current.Value);
						break;
					case "RotationY":
						rotationY = float.Parse(gameObjectElement.Current.Value);
						print(rotationY);
						break;
					case "RotationZ":
						rotationZ = float.Parse(gameObjectElement.Current.Value);
						break;
					}
					
					
				}
				
				print("Test");
				tempObj.transform.position = new Vector3(posX, posY, posZ);
				tempObj.transform.rotation = new Quaternion(rotationX, rotationY, rotationZ,0);
				loadedGameObjects.Add(tempObj);
			}
			#endregion
		}

	}
}
