using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {


	public const int numItemSlots = 2;

	Image[] itemImages = new Image[numItemSlots]; 
	Item[] items = new Item[numItemSlots];
	Button[] buttons = new Button[numItemSlots];

	public void AddItem (Item itemToAdd, int level)
	{
		for (int i = 0; i < items.Length; i++) 
		{
			if (items[i] == null) 
			{
				Debug.Log ("Adding " + i);
				items [i] = itemToAdd;
				itemImages [i].sprite = itemToAdd.sprites [level];
				itemImages [i].enabled = true;
				return;
			}
		}

	}

	public void RemoveItem (Item itemToRemove)
	{
		for (int i = 0; i < items.Length; i++) 
		{
			if (items[i] == itemToRemove) 
			{
				Debug.Log ("Trying to remove " + i);
				items [i] = null;
				itemImages [i].sprite = null;
				itemImages [i].enabled = false;
				return;
			}
		}

	}

	public int countItem(Item toCount)
	{
		int count = 0;
		for (int i = 0; i < items.Length; i++) 
		{
			if (items[i] == toCount) 
			{
				count += 1;
			}
		}
		return count;
	}

	public int countAll()
	{
		int count = 0;
		foreach (Item i in items) 
		{
			if (i != null) 
			{
				count += 1;
			}
		}
		return count;
	}

	void Awake(){
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < items.Length; i++){
			Button btn = buttons[i].GetComponent<Button>();
			btn.onClick.AddListener(delegate{RemoveItem(items[i]);});
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
