using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TopBar_Script {
    private GameObject Topbar;
    private GameObject FireAnimation;
    private Sprite[] topBar_Sprite;
    private SpriteRenderer topBar_Renderer;
    private GameObject naamText;
    private float scale;

    
    public void AddBar(Vector3 position, int i, GameObject parent, string text)
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

        Topbar.transform.localScale = new Vector3(worldScreenWidth / width, 1, 1);
        AddFire(i, Topbar);
        //Topbar.AddComponent<BoxCollider2D>();
    }

    private void AddFire(int i, GameObject parent)
    {
        GameObject sprite;
        GameObject sprite2;
        FireAnimation = (GameObject)Resources.Load("Prefabs/Scene_1_Prefabs/fireSprite_01");
        FireAnimation.transform.localScale = new Vector3(parent.transform.localScale.x, parent.transform.localScale.x / 1.5f, parent.transform.localScale.z);
        sprite = GameObject.Instantiate(FireAnimation, new Vector3(parent.transform.position.x, parent.transform.position.y, parent.transform.position.z), parent.transform.rotation) as GameObject;
        sprite2 = GameObject.Instantiate(FireAnimation, new Vector3(parent.transform.position.x + (FireAnimation.transform.localScale.DpToPixel().x * 2), parent.transform.position.y, parent.transform.position.z), parent.transform.rotation) as GameObject;
        sprite.name = "Fire_" + i;
        sprite2.name = "Fire_" + i + i;
        sprite.transform.parent = parent.transform;
        sprite2.transform.parent = parent.transform;
    }

    private void AddText(int i, GameObject parent, string text)
    {
        naamText = new GameObject("Naam_" + i);
        Font HelveticaNeue = Resources.Load<Font>("Fonts/HelveticaNeue");
        TextMesh naamTextMesh = naamText.AddComponent<TextMesh>();
        Material HelveticaNeueMat = Resources.Load<Material>("Fonts/HelveticaNeue");

        naamText.AddComponent<MeshRenderer>();
        naamText.GetComponent<MeshRenderer>().material = HelveticaNeueMat;
        naamTextMesh.text = "Vuurtje" + i;
        naamTextMesh.font = HelveticaNeue;
        naamTextMesh.fontSize = 40;

        naamText.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        naamText.transform.localPosition = new Vector3(parent.transform.position.x + 2.2f, parent.transform.position.y, parent.transform.position.z - 1);
        naamText.transform.parent = parent.transform;
    }
}
