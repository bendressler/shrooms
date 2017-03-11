using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootPanel : MonoBehaviour {

	public Button exitBtn;
	public LootPanel lootPanel;
	public GameObject gameManager;


	// Use this for initialization
	void Start () {
		Button btn = exitBtn.GetComponent<Button>();
		btn.onClick.AddListener(LootItem);
		gameManager = GameObject.Find ("GameManager");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LootItem()
	{
		if (lootPanel.enabled == true) {
			lootPanel.gameObject.SetActive(false);
			gameManager.GetComponent<GameManager>().inventoryOpen = false;
		}
	}
}
