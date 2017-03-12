using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickableContainerProb : MonoBehaviour {

	GameObject gameManager;
	GameObject cam;
	GameObject player;
	GameObject currentContainer;

	public string containerName;
	public GameObject[] pickables;
	public GameObject UIManager;
	public float activDistThresh;
	public float zoomTresh;
	public bool active;
	public bool hasPickable;
	public Item loot;
	public int lootLevel;
	public int quality;

	private bool lootCreated;
	private Sprite lootSprite;
	private UIManager UIScript;


	// Use this for initialization
	void Start () {
		lootCreated = false;
		gameManager = GameObject.Find ("GameManager");
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		player = GameObject.FindGameObjectWithTag ("Player");
		UIScript = UIManager.GetComponent<UIManager> ();


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
			if (lootCreated == false) {
				SetLoot (quality);
				lootCreated = true;
			} 
			UIScript.OpenLootPanel (loot, lootSprite, this.gameObject);
		}
	}

	void SetLoot(int level)
	{
		int itemLevel = GenerateItemLevel (quality);
		if (itemLevel > 0) {
			lootSprite = loot.sprites[itemLevel -1];
		}
	}

	int GenerateItemLevel(int quality)
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
