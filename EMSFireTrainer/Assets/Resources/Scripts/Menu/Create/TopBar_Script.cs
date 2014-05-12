using UnityEngine;
using System.Collections;

public class TopBar_Script {
    private GameObject Topbar;
    private Sprite[] topBar_Sprite;
    private SpriteRenderer topBar_Renderer;
    private float scale;

    public void AddBar(Vector3 position, int i, GameObject parent)
    {
        Topbar = new GameObject("TopBar_" + i);
        Topbar.transform.parent = parent.transform;
        topBar_Sprite = Resources.LoadAll<Sprite>("BackGrounds/TopBar/TopBar");
        topBar_Renderer = Topbar.AddComponent<SpriteRenderer>();

        topBar_Renderer.sprite = topBar_Sprite[0];
        scale = 1f / topBar_Sprite[0].texture.width;
        Topbar.transform.position = position;

        Topbar.transform.localScale = new Vector3(1, 1, 1);

        float width = topBar_Renderer.sprite.bounds.size.x;
        float height = topBar_Renderer.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 1.0f / 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Topbar.transform.localScale = new Vector3(worldScreenWidth / width, 1, 10);
    }
}
