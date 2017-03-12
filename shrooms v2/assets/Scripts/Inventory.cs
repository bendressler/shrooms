using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public int startPosX;
	public int startPosY;
	public int slotCount;
	public int slotColumns;
	public GameObject itemSlotPrefab;
	public ToggleGroup itemSlotToggle;
	public Text selectedItemText;
	public Button discardBtn;
	public Button actionABtn;
	public Button exitBtn;
	public GameObject itemPanel;
	public GameObject gameManager;

	private int xPos;
	private int yPos;
	private GameObject itemSlot;
	private int slotCounter;

	private GameObject[] invSlots;
	private Item[] invItems;
	private Item selectedItem;
	private Item prevItem;
	private Item activeItem;

	void Awake(){
		gameManager = GameObject.Find ("GameManager");
		discardBtn.GetComponent<Button>().onClick.AddListener(RemoveItem);
		actionABtn.GetComponent<Button> ().onClick.AddListener (EatItem);
		exitBtn.GetComponent<Button>().onClick.AddListener(CloseInventory);
	}

	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		SelectedItem ();
	}

	public void SetupInventoryWindow()
	{
		xPos = startPosX;
		yPos = startPosY;
		
		invSlots = new GameObject[slotCount];
		invItems = new Item[slotCount];
		for (int i = 0; i < slotCount; i++) {
			invItems [i] = null;
		}

		for (int i = 0; i < slotCount; i++) 
		{
			itemSlot = (GameObject)Instantiate (itemSlotPrefab) as GameObject;
			itemSlot.name = ("Slot" + i);
			itemSlot.GetComponent<Toggle> ().group = itemSlotToggle;
			invSlots[i] = itemSlot;
			itemSlot.transform.SetParent (this.gameObject.transform);
			itemSlot.GetComponent<RectTransform> ().localPosition = new Vector3 (xPos, yPos, 0);
			slotCounter++;
			xPos += (int) itemSlot.GetComponent<RectTransform>().rect.width;
			if (slotCounter % slotColumns == 0) 
			{
				slotCounter = 0;
				yPos -= (int) itemSlot.GetComponent<RectTransform>().rect.height;
				xPos = startPosX;
			}
		}
	}

	public void CloseInventory()
	{
		gameManager.GetComponent<GameManager>().inventoryOpen = false;
		this.gameObject.SetActive (false);
	}

	public void AddItem (Item itemToAdd, Sprite lootSprite)
	{
		for (int i = 0; i < invSlots.Length; i++) 
		{
			if (invItems[i] == null) 
			{
				invItems [i] = itemToAdd;
				invSlots [i].GetComponentsInChildren<Image> ()[1].sprite = lootSprite;
				return;
			}
		}

	}

	public void EatItem()
	{
		RemoveItem ();
		gameManager.GetComponent<GameManager> ().stamina += 5;
	}

	public void RemoveItem ()
	{
		for (int i = 0; i < invSlots.Length; i++) 
		{
			if (invItems[i] == selectedItem) 
			{
				invItems [i] = null;
				invSlots [i].GetComponentsInChildren<Image> ()[1].sprite = null;
				return;
			}
		}

	}

	public void SelectedItem()
	{
		for (int i = 0; i < invSlots.Length; i++) 
		{
			if (invSlots [i].GetComponent<Toggle> ().isOn) {
				if (invItems [i] == null) {
					selectedItemText.text = "This slot is empty!";
					selectedItem = null;
				} else {
					selectedItemText.text = invItems [i].name;
					selectedItem = invItems [i];
					itemPanel.SetActive(true);
					return;
				}	
			} else {
				itemPanel.SetActive(false);
			}
		}
	}

	public int countAll()
	{
		int count = 0;
		for (int i = 0; i < invSlots.Length; i++)  
		{
			if (invItems[i] != null) 
			{
				count += 1;
			}
		}
		return count;
	}
}
