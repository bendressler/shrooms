using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public float stamina;
	public Text staminaUI;

	public BoardManager boardScript;
	public GameObject inventory;
	public GameObject controlPanel;
	public GameObject gameOverPanel;
	public Rules ruleScript;
	public Button nextLevel;

	public bool inventoryOpen;
	private bool controlsActive;

	public bool gameOver;

	private Inventory inventoryScript;
	public int level;

	void Awake()
	{
		//boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame()
	{
		//boardScript.SetupScene ();
	}

	// Use this for initialization
	void Start () {
		stamina = 100;
		gameOver = false;
		inventoryScript = inventory.GetComponentInParent<Inventory> ();
		//Button btn = nextLevel.GetComponent<Button>();
		//btn.onClick.AddListener(NextLevel);
	}
	
	// Update is called once per frame
	void Update () {
		staminaUI.text = ("Stamina: " + Mathf.RoundToInt(stamina));
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			if (inventoryOpen) {
				inventory.SetActive (false);
				inventoryOpen = false;
			} else 
			{
				inventory.SetActive (true);
				inventoryOpen = true;
			}

		}
		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (controlsActive) {
				controlPanel.SetActive (false);
				controlsActive = false;
			} else {
				controlPanel.SetActive (true);
				controlsActive = true;
			}
		}

	}

	void NextLevel(){
		level++;
		SceneManager.LoadSceneAsync("Tutorial_2");
	}
		

	void LateUpdate()
	{
		//if (inventoryScript.countItem (ruleScript.CurrentGoalItem(level)) >= ruleScript.CurrentGoalCount(level)) {
		if (inventoryScript.countAll() > 15){
			gameOverPanel.SetActive (true);
			gameOver = true;
		}
	}

}
