using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public BoardManager boardScript;

	void Awake()
	{
		//boardScript = GetComponent<BoardManager> ();
		InitGame ();
	}

	void InitGame()
	{
		boardScript.SetupScene ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
