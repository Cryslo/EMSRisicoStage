using UnityEngine;
using System.Collections;

public class Notify : MonoBehaviour {

	static public Notify instance;

	private static GameObject notifyGui;
	private static SpriteRenderer notifyGuiRenderer;
	private static Sprite notifyGuiImage;
	private static Vector3 notifyGuiPos;
	private static Vector3 notifyGuiScale;
	
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
		notifyGuiPos = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight - height * 11, 10));


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void notify(string Message) {
		notifyGui = new GameObject("Notify");
		notifyGuiRenderer = notifyGui.AddComponent<SpriteRenderer>();
		notifyGuiRenderer.sprite = notifyGuiImage;
		notifyGui.transform.position = notifyGuiPos;
		notifyGui.transform.localScale = notifyGuiScale;

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
		textMesh.fontSize = 200;
		textMesh.color = Color.black;

		instance.StartCoroutine("waitAndDestroy");
	}

	IEnumerator waitAndDestroy() {
		float waitForSeconds = 5.0f;
		Debug.Log("Wait for " + waitForSeconds + " Seconds");
		yield return new WaitForSeconds(waitForSeconds);
		Debug.Log("Destory");
		GameObject.Destroy(notifyGui);
	}
}
