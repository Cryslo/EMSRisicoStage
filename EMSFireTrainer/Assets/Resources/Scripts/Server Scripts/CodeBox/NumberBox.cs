using UnityEngine;
using System.Collections;

public class NumberBox {

	private Camera mainCamera;

	private int boxNumber;
	private string number;

	private GameObject numberBox;
	private SpriteRenderer numberBoxRenderer;

	private GameObject numberText;
	private MeshRenderer numberTextTextRender;
	private TextMesh numberTextText;
	
	private float width;
	private float height;
	private float worldScreenHeight;
	private float worldScreenWidth;

	// Use this for initialization
	public NumberBox (string NumberShown, int BoxNumber, GameObject parent) {
		boxNumber = BoxNumber;
		number = NumberShown;
		mainCamera = GameObject.Find("Main Camera").camera;

		numberBox = new GameObject("Numberbox " + BoxNumber);
		numberBox.transform.parent = parent.transform;
		numberBoxRenderer = numberBox.AddComponent<SpriteRenderer>();
		numberBoxRenderer.sprite = Resources.Load<Sprite>("Sprites/Password_Connect_NumberBox");

		numberText = new GameObject("Number " + NumberShown);
		numberText.transform.parent = numberBox.transform;
		numberTextTextRender = numberText.AddComponent<MeshRenderer>();
		numberTextTextRender.material = Resources.Load<Material>("Fonts/HelveticaNeue");
		numberTextText = numberText.AddComponent<TextMesh>();
		numberTextText.text = NumberShown;
		numberTextText.alignment = TextAlignment.Center;
		numberTextText.anchor = TextAnchor.MiddleCenter;
		numberTextText.font = Resources.Load<Font>("Fonts/HelveticaNeue");
		numberTextText.fontSize = 200;
		numberTextText.color = Color.black;

		numberText.transform.localScale = new Vector3(0.1f,0.1f,1);
		numberText.transform.localPosition = new Vector3(0,0,-1);
		numberBox.transform.localScale = new Vector3(1,1,1);

		width = numberBoxRenderer.sprite.bounds.size.x;
		height = numberBoxRenderer.sprite.bounds.size.y;
		
		worldScreenHeight = Camera.main.orthographicSize * 1.0f;
		worldScreenWidth = Camera.main.orthographicSize * 1.0f / Screen.height * Screen.width;
		
		numberBox.transform.localScale = new Vector3(worldScreenWidth / width / 5f, worldScreenHeight / height / 2, 1);

		numberBox.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2,Screen.height / 2,10));
		numberBox.transform.position = numberBox.transform.position + new Vector3(((width / 2) * boxNumber) - ((width / 2) * 1.5f),0,0);
	}
}
