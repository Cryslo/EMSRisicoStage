using UnityEngine;
using System;
using System.Collections;
using System.Globalization;

public class DirectoryBox
{
  #region Variables
    private FolderBuilder folderBuilder;
    private FolderManager folderManager;
    private Camera mainCamera;
    private GameObject parent;
    private GameObject folderBox;
    private Texture2D backgroundImage;
    private Sprite backgroundSprite;
    private SpriteRenderer backgroundRenderer;
    private BoxCollider2D folderBoxCollider;
    private GameObject folderIcon;
    private Texture2D folderIconImage;
    private SpriteRenderer folderIconRenderer;
	private BoxCollider2D folderIconCollider;
    private Material folderMaterial;
    private string directoryName;
    private string directorNumber;
    private bool editActive;
    private bool fileMode = false;
    private bool Protected = false;
  #if UNITY_ANDROID
  TouchScreenKeyboard keyboard;
  #endif
    string newName;
    private float timer;
    private float timerPoint = 2f;
    private float x;
    private float y;
    private float boxX;
    private float boxY;
    private float width = 200;
    private float height = 200;
    private float startY;
    private int boxNumber;
    private int columns;
    private Rect boxCollider;
    private float testY;
    private float newStartY;
    private GameObject deleteIcon;
    private GUITexture deleteTexture;
    private Texture2D deleteImage;
    private Rect deleteRect;
	private bool moveMode;
	private bool emptyFolder;
	private int exampleFolders;
  #endregion
  
  #region Porperties
    public float X
    {
        set{ this.x = value;}
        get{ return this.x;}
    }
  
    public float Y
    {
        set{ this.y = value;}
        get{ return this.y;}
    }

    public float TestY
    {
        set{ this.testY = value;}
        get{ return this.testY;}
    }
  
    public float BoxX
    {
        set{ this.boxX = value;}
        get{ return this.boxX;}
    }

    public float BoxY
    {
        set{ this.boxY = value;}
        get{ return this.boxY;}
    }

    public int Columns
    {
        set{ this.columns = value;}
        get{ return this.columns;}
    }

    public float StartY
    {
        set{ this.startY = value;}
        get{ return this.startY;}
    }

    public float NewStartY
    {
        set{ this.newStartY = value;}
        get{ return this.newStartY;}
    }

    public GameObject Parent
    {
        set{ this.parent = value;}
        get{ return this.parent;}
    }
  
    public int BoxNumber
    {
        set{ this.boxNumber = value;}
        get{ return this.boxNumber;}
    }
  
    public string DirectoryName
    {
        set{ this.directoryName = value;}
        get{ return this.directoryName;}
    }
  
    public string DirectorNumber
    {
        set{ this.directorNumber = value;}
        get{ return this.directorNumber;}
    }
  
    public bool FileMode
    {
        set{ this.fileMode = value;}
        get{ return this.fileMode;}
    }

    public bool ProtectedBox
    {
        set{ this.Protected = value;}
        get{ return this.Protected;}
    }
  
    public FolderManager FolderManagerScript
    {
        set{ this.folderManager = value;}
    }
  
    public FolderBuilder folderBuilderScript
    {
        set{ this.folderBuilder = value;}
    }

    public Rect BoxCollider
    {
        get{ return this.boxCollider;}
    }

    public Rect DeleteRect
    {
        get{ return this.deleteRect;}
    }
  #endregion
  
    // Use this for initialization
    public void Setup()
    {
        mainCamera = GameObject.Find("Main Camera").camera;
		exampleFolders = 

        columns = 3;
    
        width = Screen.width / 5;
        height = Screen.height / 5;
    
        boxX = (Screen.width - ((width * columns) - width * x));
        boxY = (Screen.height - (height + (height * y)));
        TestY = (height * y);

        deleteImage = Resources.Load("Icons/Play_Icons/Delete") as Texture2D;
        folderMaterial = Resources.Load("Materials/BackgroundGrayscale") as Material;

        backgroundSprite = new Sprite();
        folderBox = new GameObject(directoryName);
        folderBox.transform.parent = parent.transform;
        backgroundRenderer = folderBox.AddComponent<SpriteRenderer>();
        
        if(boxNumber != 0 && !fileMode)
        {
			Debug.Log(folderManager.GetFirstSceneByFolderName(directoryName));

			if(folderManager.GetFirstSceneByFolderName(directoryName) == "" || folderManager.GetFirstSceneByFolderName(directoryName) == ".DS_Store")
			{
				emptyFolder = true;
				//backgroundImage = Resources.Load<Texture2D>("Icons/Play_Icons/emptyFolderBg");
				folderIconImage = Resources.Load<Texture2D>("Icons/Play_Icons/emptyFolder");
				backgroundSprite = Sprite.Create(backgroundImage, new Rect(0, 0, backgroundImage.width, backgroundImage.height), new Vector2(0.5f, 0.5f), 100.0f);
				backgroundRenderer.sprite = backgroundSprite;
				backgroundRenderer.color = new Color(0,0,0,0);
			}else{
				Debug.Log(folderManager.GetFirstSceneByFolderName(directoryName));
				backgroundImage = folderManager.GetBackgroundsByName(folderManager.GetFirstSceneByFolderName(directoryName));
				//backgroundRenderer.material = folderMaterial;
				backgroundSprite = Sprite.Create(backgroundImage, new Rect(0, 0, backgroundImage.width, backgroundImage.height), new Vector2(0.5f, 0.5f), 100.0f);
				backgroundRenderer.sprite = backgroundSprite;
			}
           
		}else if(boxNumber != 0 && fileMode)
		{
			if(directoryName != ".DS_Store")
			{
				backgroundImage = folderManager.GetBackgroundsByName(directoryName);
				//backgroundRenderer.material = folderMaterial;
				backgroundSprite = Sprite.Create(backgroundImage, new Rect(0, 0, backgroundImage.width, backgroundImage.height), new Vector2(0.5f, 0.5f), 100.0f);
				backgroundRenderer.sprite = backgroundSprite;
			} else {
				backgroundImage = Resources.Load<Texture2D>("Icons/Play_Icons/emptyFolderBg");
				backgroundSprite = Sprite.Create(backgroundImage, new Rect(0, 0, backgroundImage.width, backgroundImage.height), new Vector2(0.5f, 0.5f), 100.0f);
				backgroundRenderer.sprite = backgroundSprite;
			}
		} else {
			backgroundImage = Resources.Load<Texture2D>("Icons/Play_Icons/emptyFolderBg");
            backgroundSprite = Sprite.Create(backgroundImage, new Rect(0, 0, backgroundImage.width, backgroundImage.height), new Vector2(0.5f, 0.5f), 100.0f);
            backgroundRenderer.sprite = backgroundSprite;
        }

    
        folderBoxCollider = folderBox.AddComponent<BoxCollider2D>();

        float swidth = backgroundRenderer.sprite.bounds.size.x;
        float sheight = backgroundRenderer.sprite.bounds.size.y;
    
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f / 5;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
    
        folderBox.transform.localScale = new Vector3(worldScreenWidth / swidth, worldScreenHeight / sheight, 1);

        folderBox.transform.position = mainCamera.ScreenToWorldPoint(new Vector3((mainCamera.pixelWidth + (width / 2)) - ((width * columns) - width * x ), (mainCamera.pixelHeight - (height / 2)) - (height * y) - 1, 1));
		//folderBox.transform.position = folderBox.transform.position + new Vector3(0.02f, 0.02f,0);

        //Folder icon
        folderIcon = new GameObject("FolderIcon");
        folderIcon.transform.parent = folderBox.transform;
        folderIconRenderer = folderIcon.AddComponent<SpriteRenderer>();
        folderIconRenderer.sortingOrder = 1;
        if(boxNumber == 0)
        {
            if(fileMode)
			{
                folderIconImage = Resources.Load<Texture2D>("Icons/Play_Icons/Folderback");
			}
            else
			{
                folderIconImage = Resources.Load<Texture2D>("Icons/Play_Icons/FolderPlus");
			}
        }
		if(folderIconImage != null)
		{
			folderIconRenderer.sprite = Sprite.Create(folderIconImage, new Rect(0, 0, folderIconImage.width, folderIconImage.height), new Vector2(0.5f, 0.5f), 100.0f);
		}
        folderIcon.transform.localPosition = new Vector3();//mainCamera.ScreenToWorldPoint(new Vector3((mainCamera.pixelWidth) - ((width * columns) - width * x), (mainCamera.pixelHeight - height) - (height * y), 1));
//    folderIcon.transform.localScale = new Vector3(worldScreenWidth / folderIconImage.width, worldScreenHeight / folderIconImage, 1);

    }
  
    public void Update()
    {
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if(hit.transform == folderBox.transform)
        { 
            if(Input.GetMouseButtonDown(0))
            {
                timer = 0;
            }
      
            if(Input.GetMouseButton(0))
            {

                #region DeleteButton
                if(boxNumber != 0)
                {
                    timer += 1 * Time.deltaTime;
                    if(timer > timerPoint)
                    {
						if(boxNumber != 0)
						{
							moveMode = true;
							folderIconImage = Resources.Load<Texture2D>("Icons/Play_Icons/FolderCross");
							folderIconRenderer.sprite = Sprite.Create(folderIconImage, new Rect(0, 0, folderIconImage.width, folderIconImage.height), new Vector2(0.5f, 0.5f), 100.0f);
							folderIcon.transform.localPosition = new Vector3();
							if(folderIconCollider == null)
								folderIconCollider = folderIcon.AddComponent<BoxCollider2D>();

						}
                    }
                }
                #endregion
            }
            if(Input.GetMouseButtonUp(0))
            {
				if(!moveMode)
				{
					if(boxNumber == 0 )
	                {
	                    if(!fileMode)
	                        DirectoryCreate();
	                    else
	                        DirectoryBack();
	                } else
	                {
	                    DirectoryClick();
	                }
				} else {

				}
            }
        } else {
			if(moveMode)
			{

				if(Input.GetMouseButtonUp(0))
				{
					if(boxNumber != 0)
					{
						if(emptyFolder)
						{
							folderIconImage = Resources.Load<Texture2D>("Icons/Play_Icons/emptyFolder");
							folderIconRenderer.sprite = Sprite.Create(folderIconImage, new Rect(0, 0, folderIconImage.width, folderIconImage.height), new Vector2(0.5f, 0.5f), 100.0f);
							folderIcon.transform.localPosition = new Vector3();
						}else{
							folderIconRenderer.sprite = null;
						}

						backgroundRenderer.sprite = backgroundSprite;
						if(folderIconCollider != null)
						{
							Component.Destroy(folderIconCollider);
						}
					}
				}
			}
		}

        if(deleteIcon != null)
        {
            if(deleteIcon.name == "Delete_Icon")
            {
                if(Input.GetMouseButtonDown(0) && !boxCollider.Contains(mousePos))
                {

                }
                if(Input.GetMouseButtonDown(0) && deleteRect.Contains(mousePos))
                {
                    Debug.Log("Delete: " + directoryName);
                }
            }
        }

        if(editActive)
        {
            #if UNITY_ANDROID
      if (!Input.GetMouseButton(0)) {
        if (keyboard.done && !keyboard.wasCanceled) {
          DirectoryClickEdit (keyboard.text);
        }
        if (keyboard.active) {
          newName = keyboard.text;
        }
      }
#endif

        }
    }
  
    // Call this Function when clicked on the box
    private void DirectoryClick()
    {
        if(fileMode)
        {

        } else
        {
            Debug.Log(directoryName);
            folderBuilder.OpenFolder(boxNumber, directoryName);
        }
    }
  
    // Call this Function when clicked on the Deletebox
    private void DirectoryDelete()
    {
        Debug.Log("Delete_Folder: " + directoryName);
        folderManager.DeleteFolderWithName(directoryName);
        folderBuilder.rebuildFolderArray();
    }
  
    // Call this Function when clicked on the Editbox
    private void DirectoryEdit()
    {
        Debug.Log("Edit_Folder: " + directoryName);
        buildEdit();
        //folderBuilder.rebuildArray ();
    }
    // Call this Function when clicked on the duplicate
    private void DirectoryDuplicate()
    {
        Debug.Log("Duplicate_Folder: " + directoryName);
        folderManager.CopyByName(directoryName);
        folderBuilder.rebuildFolderArray();
        //folderBuilder.rebuildArray ();
    }
  
    public void buildEdit()
    {
        editActive = true;
        #if UNITY_ANDROID
    keyboard = (TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false));
#endif
    }
  
    void DirectoryClickEdit(string newName)
    {
        editActive = false;
        folderManager.RenameFolderWithID(boxNumber, newName, folderBuilder.ExampleProjects.Count);
        folderBuilder.rebuildFolderArray();
    }
  
    // Call this Function when clicked on the Playbox
    private void DirectoryPlay()
    {
        Debug.Log("Play_Folder: " + directoryName);
    }
  
    // Call this Function to create this box
    public void DirectoryCreate()
    {
        Debug.Log("Create_Folder");
        folderManager.CreateFolder();
        folderBuilder.rebuildFolderArray();
    }
  
    // Call this Function to create this box
    public void DirectoryBack()
    {
        Debug.Log("Back");
        folderBuilder.CloseFolder();
    }

    // Call this Function to remove this box
    public void Remove()
    {
        boxCollider = new Rect();
    }
}
