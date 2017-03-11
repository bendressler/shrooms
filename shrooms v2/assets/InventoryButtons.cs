using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryButtons : MonoBehaviour {

	public Button slotButton;
	public Inventory inventory;

	// Use this for initialization
	void Start () {
		inventory = GetComponentInParent<Inventory> ();
		Button btn = slotButton.GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
