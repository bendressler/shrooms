using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickableContainer : MonoBehaviour {

	GameObject gameManager;
	GameObject cam;
	GameObject player;
	GameObject currentContainer;

	public GameObject lootPanel;
	public Button lootBtn;
	public Text containerTxt;
	public Text lootTxt;
	public string containerName;

	public float activDistThresh;
	public float zoomTresh;
	public bool active;
	public bool hasPickable;
	public Item loot;

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
			currentContainer = this.gameObject;
			containerTxt.text = containerName;
			lootTxt.text = loot.name;
			lootBtn.image.sprite = loot.sprite;
			lootPanel.SetActive (true);
			gameManager.GetComponent<GameManager>().inventoryOpen = true;
		}
	}


	void LootItem()
	{
		if (currentContainer == this.gameObject) {
			//gameManager.GetComponent<GameManager> ().inventory.GetComponentInParent<Inventory> ().AddItem (loot, 1);
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
