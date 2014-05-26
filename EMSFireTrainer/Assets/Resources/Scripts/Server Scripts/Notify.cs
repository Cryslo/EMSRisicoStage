using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Notify : MonoBehaviour {

	static public Notify instance;

	private static GameObject notifyGui;
	private static SpriteRenderer notifyGuiRenderer;
	private static Sprite notifyGuiImage;
	private static Vector3 notifyGuiPos;
	private static Vector3 notifyGuiScale;
	private static List<string> que = new List<string>();
	
	private float width;
	private float height;
	private float worldScreenHeight;
	private float worldScreenWidth;

	private static GameObject text;
	private static MeshRenderer textRender;
	private static TextMesh textMesh;
	private static Material textMaterial;
	private static Font textFont;



	// Use this for initialization
	void Start () {
		instance = this;

		notifyGuiImage = Resources.Load<Sprite>("Sprites/Notify");
		textMaterial = Resources.Load<Material>("Fonts/HelveticaNeue");
		textFont = Resources.Load<Font>("Fonts/HelveticaNeue");
		
		width = notifyGuiImage.bounds.size.x;
		height = notifyGuiImage.bounds.size.y;
		
		worldScreenHeight = Camera.main.orthographicSize * 1.0f;
		worldScreenWidth = Camera.main.orthographicSize * 1.0f / Screen.height * Screen.width;

		notifyGuiScale = new Vector3(worldScreenWidth / width , worldScreenHeight / height / 4 , 1);
	}
	
	// Update is called once per frame
	void Update () {
		if(que.Count > 0)
		{
			if(notifyGui == null)
			{
				notifyGui = new GameObject("Notify");
				notifyGuiRenderer = notifyGui.AddComponent<SpriteRenderer>();
				notifyGuiRenderer.sprite = notifyGuiImage;
				notifyGui.transform.localScale = notifyGuiScale;
				notifyGuiPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight , 10));
				
				notifyGui.transform.position = notifyGuiPos - new Vector3(0,notifyGuiRenderer.sprite.bounds.size.x / 10,0);;
				
				text = new GameObject("Notify Text");
				text.transform.parent = notifyGui.transform;
				
				text.transform.localScale = new Vector3(0.1f,0.1f,1);
				text.transform.localPosition = new Vector3(0,0,-1);
				
				
				textRender = text.AddComponent<MeshRenderer>();
				textRender.material = textMaterial;
				textMesh = text.AddComponent<TextMesh>();
				textMesh.alignment = TextAlignment.Center;
				textMesh.anchor = TextAnchor.MiddleCenter;
				textMesh.text = que[0];
				textMesh.font = textFont;
				textMesh.fontSize = 50;
				textMesh.color = Color.black;
				que.RemoveAt(0);
				
				instance.StartCoroutine("waitAndDestroy");
			}
		}
	}

	public static void notify(string Message) {
		if(notifyGui == null)
		{
			notifyGui = new GameObject("Notify");
			notifyGuiRenderer = notifyGui.AddComponent<SpriteRenderer>();
			notifyGuiRenderer.sprite = notifyGuiImage;
			notifyGui.transform.localScale = notifyGuiScale;
			notifyGuiPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight , 10));

			notifyGui.transform.position = notifyGuiPos - new Vector3(0,notifyGuiRenderer.sprite.bounds.size.x / 10,0);;

			text = new GameObject("Notify Text");
			text.transform.parent = notifyGui.transform;

			text.transform.localScale = new Vector3(0.1f,0.1f,1);
			text.transform.localPosition = new Vector3(0,0,-1);


			textRender = text.AddComponent<MeshRenderer>();
			textRender.material = textMaterial;
			textMesh = text.AddComponent<TextMesh>();
			textMesh.alignment = TextAlignment.Center;
			textMesh.anchor = TextAnchor.MiddleCenter;
			textMesh.text = Message;
			textMesh.font = textFont;
			textMesh.fontSize = 50;
			textMesh.color = Color.black;


			instance.StartCoroutine("waitAndDestroy");
		} else {
			que.Add(Message);
		}
	}

	IEnumerator waitAndDestroy() {
		float waitForSeconds = 5.0f;
		Debug.Log("Wait for " + waitForSeconds + " Seconds");
		yield return new WaitForSeconds(waitForSeconds);
		Debug.Log("Destory");
		GameObject.Destroy(notifyGui);
	}
}
