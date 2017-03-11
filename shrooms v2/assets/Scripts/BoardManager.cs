using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	//count Class that contains a minium and a maximum value and is accessible in the inspector
	[Serializable]
	public class Count
	{
		public int minimum;
		public int maximum;

		public Count (int min, int max)
		{
			minimum = min;
			maximum = max;
		}
	}
		
	public int columns = 60;
	public int rows = 60;
	public GameObject dirtTile;
	public GameObject leftWall;
	public GameObject rightWall;
	public GameObject topWall;
	public GameObject botWall;
	public GameObject tlCorner;
	public GameObject trCorner;
	public GameObject blCorner;
	public GameObject brCorner;
	public GameObject[] trees;
	public GameObject[] lakes;
	public GameObject[] berries;

	public HelperElf[] helpers;
	public Species lake;
	public Species tree;
	public Species grass;

	public Transform boardHolder;
	public Transform lakeHolder;
	public Transform treeHolder;
	public Transform grassHolder;

	public List<GameObject> allObjects = new List<GameObject> ();
	public GameObject[] allHelpers;
	public int maxHelpers;
	public int maxObjects;

	private List<Vector3> gridPositions = new List<Vector3>();

	private int helperSent;
	public int helperCounter;


	//summary function to initiate startup components
	public void SetupScene ()
	{

		BoardSetup ();
		InitialiseList ();
		helperSent = 0;

	}


	//clears the gridPositions Vector3 list and adds a new position for each tile on the map constrained by column and row values
	void InitialiseList()
	{
		gridPositions.Clear ();

		for (int x = 1; x < columns - 1; x++)
		{
			for (int z = 1; z < rows - 1; z++)
			{
				gridPositions.Add(new Vector3(x,0f,z));
			}
		}
	}

	//iterates through rows and columns and creates the tiles
	void BoardSetup()
	{

		boardHolder = new GameObject ("Board").transform;
		lakeHolder = new GameObject ("Lakes").transform;
		grassHolder = new GameObject ("Grass").transform;
		treeHolder = new GameObject ("Trees").transform;

		for (int x = -1; x < columns + 1; x++) 
		{
			for (int z = -1; z < rows + 1; z++) 
			{
				//set default to instantiate, which can be overwritten based on conditions
				GameObject toInstantiate = dirtTile;

				//based on position, the tile selection is overwritten with a wall
				if (x == -1 || x == columns || z == -1 || z == rows) 
				{
					if (z == -1) 
					{
						toInstantiate = botWall;
					}

					if (z == rows) 
					{
						toInstantiate = topWall;
					}

					if (x == -1) 
					{
						toInstantiate = leftWall;
						if (z == -1) 
						{
							toInstantiate = blCorner;
						}
						if (z == rows) {
							toInstantiate = tlCorner;
						}
					}

					if (x == columns) 
					{
						toInstantiate = rightWall;
						if (z == -1) 
						{
							toInstantiate = brCorner;
						}
						if (z == rows) {
							toInstantiate = trCorner;
						}
					}
				}

				//the tile selection is instantiated
				GameObject instance = Instantiate (toInstantiate, new Vector3 (x, 0f, z), Quaternion.identity) as GameObject;

				//the instantiated element receives the boardHolder as parent
				instance.transform.SetParent (boardHolder);
			}
		}
	}
		


	//a function that returns a random position on the grid
	Vector3 RandomPosition()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}

	//instantiate one of the helpers preset in the boardmanager prefab
	public void SendHelper(int i){
		GameObject helper = helpers[i].gameObject;
		GameObject instance;
		instance = Instantiate (helper, new Vector3 ((Random.Range(1,columns-1)), 1, (Random.Range(1,rows-1))), Quaternion.identity) as GameObject;
		instance.GetComponent<HelperElf> ().boardManager = this;
	}



	//iterates through all generated objects and adds them to their respective holders
	void OrganizeObjects(){
		foreach (GameObject i in allObjects) {
			if (i != null) {
				if (i.GetComponent<Species> ().speciesName == "Tree") {
					i.transform.SetParent (treeHolder);
				} else if (i.GetComponent<Species> ().speciesName == "Lake") {
					i.transform.SetParent (lakeHolder);
				} else if (i.GetComponent<Species> ().speciesName == "Grass") {
					i.transform.SetParent (grassHolder);
				}
			}
		}
	}


	// Use this for initialization
	void Start () {
		helperCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//send a helper if the number of helpers sent out equals the number of helpers finished and less than the preset helper array
		/*if ((helperSent == helperCounter) && (helperSent < helpers.Length)) {
			SendHelper (helperCounter);
			helperSent++;
		}
		allHelpers = GameObject.FindGameObjectsWithTag ("Helper");
		OrganizeObjects ();

		*/
	}
}
