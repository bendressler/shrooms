using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickableContainerProb : MonoBehaviour {

	GameObject gameManager;
	GameObject cam;
	GameObject player;
	GameObject currentContainer;

	public GameObject lootPanel;
	public Button lootBtn;
	public Button exitBtn;
	public Text containerTxt;
	public Text lootTxt;
	public string containerName;

	public float activDistThresh;
	public float zoomTresh;
	public bool active;
	public bool hasPickable;
	public Item loot;
	public int lootLevel;
	public float quality;

	public GameObject[] pickables;


	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		player = GameObject.FindGameObjectWithTag ("Player");

		Button btn = lootBtn.GetComponent<Button>();
		btn.onClick.AddListener(LootItem);
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (player.transform.position, this.transform.position) < activDistThresh) {
			CheckActive ();
		} else if (active) {
			active = false;
			this.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");

		}
	}

	void OnMouseDown()
	{
		if (active == true) {
			int itemLevel = GenerateItemLevel (quality);
			currentContainer = this.gameObject;
			containerTxt.text = containerName;
			lootTxt.text = "";
			lootBtn.image.sprite = null;
			lootPanel.SetActive (false);
			SetLoot (itemLevel);
			gameManager.GetComponent<GameManager>().inventoryOpen = true;
		}
	}

	void SetLoot(int level)
	{
		if (level > 0) {
			lootTxt.text = loot.nameStr;
			lootPanel.SetActive (true);
			lootBtn.image.sprite = loot.sprites[level -1];
		}
	}

	int GenerateItemLevel(float quality)
	{
		int level = 0;
		for (int i = 0; i < 3; i++) 
		{
			if (CoinToss (quality)) {
				level += 1;
			}
		}
		lootLevel = level;
		return level;
	}

	bool CoinToss(float prob)
	{
		float random = Random.Range (0, 100);
		if (random < prob) {
			return true;
		} else
			return false;
	}

	void LootItem()
	{
		if (currentContainer == this.gameObject) {
			gameManager.GetComponent<GameManager> ().inventory.GetComponentInParent<Inventory> ().AddItem (loot,lootLevel);
			currentContainer = null;
			lootPanel.SetActive (false);
			hasPickable = false;
			foreach (GameObject i in pickables) {
				Destroy (i);
			}
			gameManager.GetComponent<GameManager>().inventoryOpen = false;
		}
	}

	void CheckActive()
	{
		if (hasPickable) {
			if (cam.GetComponent<CameraZoom> ().currentZoom < zoomTresh) {
				if (!active) {
					active = true;
					this.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Self-Illumin/Diffuse");
					}
			}
			else {
				active = false;
				this.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");

			}
		}
		else {
			active = false;
			this.GetComponentInChildren<MeshRenderer>().material.shader = Shader.Find("Standard");

		}
		

	}



}
