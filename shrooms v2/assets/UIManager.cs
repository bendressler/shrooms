using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject lootPanel;
	public Button lootBtn;
	public Button exitBtn;
	public Text containerTxt;
	public Text lootTxt;
	public GameObject inventoryObject;

	private Inventory inventoryScript;
	private GameObject currentContainer;
	private GameObject gameManager;
	private Item loot;
	private int lootLevel;
	private Sprite lootSprite;
	private PickableContainerProb containerScript;

	// Use this for initialization
	void Start () {
		lootBtn.GetComponent<Button>().onClick.AddListener(LootItem);
		gameManager = GameObject.Find ("GameManager");

	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OpenLootPanel(Item lootItem, Sprite lootSprite, GameObject container)
	{
		if (currentContainer != container) {
			lootPanel.SetActive (false);
			currentContainer = container;
		}
		containerScript = container.GetComponent<PickableContainerProb> ();
		containerTxt.text = containerScript.containerName;
		lootTxt.text = "";
		lootPanel.SetActive (true);
		loot = lootItem;
		this.lootSprite = lootSprite;
		lootBtn.image.sprite = lootSprite;
		lootTxt.text = lootItem.nameStr;
		gameManager.GetComponent<GameManager>().inventoryOpen = true;
	}

	public void LootItem()
	{
		Debug.Log ("Lootlevel" + lootLevel);
		Debug.Log ("Item" + loot);
		inventoryObject.GetComponent<Inventory>().AddItem (loot, lootSprite);
		currentContainer = null;
		lootPanel.SetActive (false);
		containerScript.hasPickable = false;
		foreach (GameObject i in containerScript.pickables) {
			Destroy (i);
		}
		inventoryObject.GetComponent<Inventory>().CloseInventory();		
	}
}
