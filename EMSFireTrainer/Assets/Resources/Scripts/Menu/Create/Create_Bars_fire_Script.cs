﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Create_Bars_fire_Script {
    private GameObject Topbar;
    private GameObject FireAnimation;
    private Sprite[] topBar_Sprite;
    private SpriteRenderer topBar_Renderer;
    private GameObject naamText;
    private float scale;
    private Camera camera;

    
    public void AddBar(Vector3 position, int i, GameObject parent, string text)
    {
        camera = GameObject.Find("Main Camera").camera;
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

        float worldScreenHeight = camera.orthographicSize * 1.0f / 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Topbar.transform.localScale = new Vector3(worldScreenWidth / width, 1, 1);
        AddFire(i, Topbar);
        AddSecondFire(i, Topbar);
    }

    /*private void AddFire(int i, GameObject parent)
    {
        float pixelRatio = (camera.orthographicSize * 2) / camera.pixelHeight;
        GameObject sprite;
        GameObject sprite2;s


        FireAnimation = (GameObject)Resources.Load("Prefabs/Scene_1_Prefabs/fireSprite_01");
        Debug.Log("ScaleX: " + parent.transform.localScale.x);
        sprite = GameObject.Instantiate(FireAnimation, new Vector3(parent.transform.position.x + 0.15f, parent.transform.position.y, parent.transform.position.z), parent.transform.rotation) as GameObject;
        sprite2 = GameObject.Instantiate(FireAnimation, new Vector3(parent.transform.position.x + (FireAnimation.transform.localScale.DpToPixel().x * 2) + 0.30f, parent.transform.position.y, parent.transform.position.z), parent.transform.rotation) as GameObject;
        sprite.name = "Fire_" + i;
        sprite2.name = "Fire_" + i + i;
        sprite.transform.parent = parent.transform;
        sprite2.transform.parent = parent.transform;
        sprite.transform.localScale = new Vector3(Topbar.transform.localScale.x / 1.25f, Topbar.transform.localScale.y / 1.75f, parent.transform.localScale.z);
        sprite2.transform.localScale = new Vector3(Topbar.transform.localScale.x / 1.25f, Topbar.transform.localScale.y / 1.75f, parent.transform.localScale.z);
    }*/
    private void AddFire(int i, GameObject parent)
    {
        GameObject fireObject;
        Sprite[] fireSprite;
        SpriteRenderer fireRenderer;
        Bounds fireBounds;
        BoxCollider2D fireCollider;
        Vector3 position;


        fireObject = new GameObject("fire_" + i);
        fireSprite = Resources.LoadAll<Sprite>("Sprites/Fire/SmallFire");
        fireRenderer = fireObject.AddComponent<SpriteRenderer>();
        fireRenderer.sprite = fireSprite[0];
        fireCollider = fireObject.AddComponent<BoxCollider2D>();
        fireRenderer.sortingOrder = 1;
        fireObject.transform.parent = parent.transform;
        fireObject.AddComponent<Drag_Fire_Script>();

        float screenDPI = Screen.dpi / 160;

        float xSize = fireSprite[0].bounds.size.x;
        float ySize = fireSprite[0].bounds.size.y;

        float width;
        float height;

        if (screenDPI > 0)
        {
            width = 204 * screenDPI;
            height = 330 * screenDPI;
        }
        else
        {
            width = 204 * parent.transform.localScale.x / 2;
            height = 280 * parent.transform.localScale.y / 2;
        }

        float worldwidth = (camera.orthographicSize * 2 / Screen.height * width) / xSize;
        float worldHeight = (camera.orthographicSize * 2 / Screen.height * height) / ySize;

        fireObject.transform.localScale = new Vector3(worldwidth, worldHeight, 1);
        position = new Vector3(parent.transform.position.x + (parent.transform.localScale.DpToPixel().x / 4), parent.transform.position.y, parent.transform.position.z);
        fireObject.transform.position = position;
    }
    private void AddSecondFire(int i, GameObject parent)
    {
        GameObject fireObject;
        Sprite[] fireSprite;
        SpriteRenderer fireRenderer;
        Bounds fireBounds;
        BoxCollider2D fireCollider;
        Vector3 position;


        fireObject = new GameObject("fire_" + i + i);
        fireSprite = Resources.LoadAll<Sprite>("Sprites/Fire/SmallFire");
        fireRenderer = fireObject.AddComponent<SpriteRenderer>();
        fireRenderer.sprite = fireSprite[0];
        fireCollider = fireObject.AddComponent<BoxCollider2D>();
        fireRenderer.sortingOrder = 1;
        fireObject.transform.parent = parent.transform;
        fireObject.AddComponent<Drag_Fire_Script>();

        float screenDPI = Screen.dpi / 160;

        float xSize = fireSprite[0].bounds.size.x;
        float ySize = fireSprite[0].bounds.size.y;

        float width;
        float height;

        if (screenDPI > 0)
        {
            width = (204 * parent.transform.localScale.y / 2) * screenDPI;
            height = (280 * parent.transform.localScale.y / 2) * screenDPI;
        }
        else
        {
            width = 204 * parent.transform.localScale.x / 2;
            height = 280 * parent.transform.localScale.y / 2;
        }

        float worldwidth = (camera.orthographicSize * 2 / Screen.height * width) / xSize;
        float worldHeight = (camera.orthographicSize * 2 / Screen.height * height) / ySize;

        fireObject.transform.localScale = new Vector3(worldwidth, worldHeight, 1);
        position = new Vector3(parent.transform.position.x + (parent.transform.localScale.DpToPixel().x / 2) + fireSprite[0].bounds.size.x, parent.transform.position.y, parent.transform.position.z);
        fireObject.transform.position = position;
    }
}
