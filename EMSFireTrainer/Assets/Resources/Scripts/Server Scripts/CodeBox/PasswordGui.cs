using UnityEngine;
using System.Collections;

public class PasswordGui {

	private GameObject backgroundBox;
	private SpriteRenderer NumberLockRenderer;
	private float width;
	private float height;
	private float worldScreenHeight;
	private float worldScreenWidth;
	private NumberBox numberBox;
	// Use this for initialization
	public PasswordGui (string Password) {

		backgroundBox = new GameObject("CodeLock");
		NumberLockRenderer = backgroundBox.AddComponent<SpriteRenderer>();
		NumberLockRenderer.sprite = Resources.Load<Sprite>("Sprites/Pass_Background");
		NumberLockRenderer.sortingOrder = -1;


		backgroundBox.transform.localScale = new Vector3(1,1,1);
		
		width = NumberLockRenderer.sprite.bounds.size.x;
		height = NumberLockRenderer.sprite.bounds.size.y;
		
		worldScreenHeight = Camera.main.orthographicSize * 1.0f;
		worldScreenWidth = Camera.main.orthographicSize * 1.0f / Screen.height * Screen.width;

		backgroundBox.transform.localScale = new Vector3(worldScreenWidth / width / 1f, worldScreenHeight / height / 2, 1);


		for (int i = 0; i < Password.Length; i++)
		{
			string number = Password[i].ToString();
			Debug.Log(number);
			numberBox = new NumberBox(number, i, backgroundBox);
		}
	}
	
	public void DeleteGui() {
		GameObject.Destroy(backgroundBox);
	}

	
}
