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

	public class Species
	{
		public int range;
		public string name;
		public float probability;
		public Species enabler;
		public Species disabler;
		public bool exclusive;
		public GameObject[,] speciesArray;
		public GameObject instance;

		public Species(int ownRange, string ownName, float ownProb, Species ownEnabler, Species ownDisabler, bool ownExcl, GameObject[,] ownArray, GameObject ownInstance)
		{
			range = ownRange;
			name = ownName;
			probability = ownProb;
			enabler = ownEnabler;
			disabler = ownDisabler;
			exclusive = ownExcl;
			speciesArray = ownArray;
			instance = ownInstance;
		}
	}
		
	public float grassProb = 0.6f; //higher value leads to more grass tiles
	public int columns = 60;
	public int rows = 60;
	public Count treeCount = new Count (3, 9);
	public Count lakeCount = new Count (1,4);
	public Count berryCount = new Count (20,100);
	public GameObject[] berries;
	public GameObject grassTile;
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

	public Species tree;
	public Species lake;
	public Species dirt;
	public Species grass;


	public GameObject[,] treeTiles;
	public GameObject[,] lakeTiles;
	public GameObject[,] berryTiles;
	public GameObject[,] groundTiles;

	private Transform boardHolder;
	private List<Vector3> gridPositions = new List<Vector3>();


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
		groundTiles = new GameObject[columns, rows];
		berryTiles = new GameObject[columns, rows];
		treeTiles = new GameObject[columns, rows];
		lakeTiles = new GameObject[columns, rows];
		tree = new Species (3, "tree", 0.2f, grass, dirt, true, treeTiles, trees[0]);
		grass = new Species (1, "grass", 0.5f, grass, dirt, true, groundTiles, grassTile);
		dirt = new Species (1, "dirt", 0.5f, grass, dirt, true, groundTiles, dirtTile);
		lake = new Species (5, "lake", 0.2f, dirt, grass, true, lakeTiles, lakes[0]);



		boardHolder = new GameObject ("Board").transform;

		for (int x = -1; x < columns + 1; x++) 
		{
			for (int z = -1; z < rows + 1; z++) 
			{
				//set default to instantiate, which can be overwritten based on conditions
				GameObject toInstantiate = grassTile;

				//based on a given probability, the grass tile selection is overwritten with a dirt tile
				bool isDirtTile = (Random.value > grassProb);

				if (isDirtTile) 
				{
					toInstantiate = dirtTile;
				}

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
				if ((x >= 0) && (z >= 0) && (x < columns) && (z < rows)) {
					groundTiles [x, z] = instance;
				}
				//the instantiated element receives the boardHolder as parent
				instance.transform.SetParent (boardHolder);
			}
		}
	}

	//needs to be called with a species array and species class, calls InstanceDecision on each map tile
	void MapIterator(GameObject[,] speciesArr, Species species){
		for (int x = 1; x < columns - 1; x++)
		{
			for (int z = 1; z < rows - 1; z++)
			{
				Species newItem = species;
				GameObject tile = speciesArr [x, z];
				Debug.Log (species.name);
				if (InstanceDecision (speciesArr, x, z, tile, species)) {
					GameObject newInstance = Instantiate (species.instance, new Vector3 (x, 0f, z), Quaternion.identity) as GameObject;
					speciesArr [x, z] = newInstance;
					Debug.Log ("created new " + species.name);
				}
			}
		}
	}

	//if the tile is already blocked for this species, return false, else check for probability and instantiate if higher than X
	bool InstanceDecision(GameObject[,] speciesArr, int x, int z, GameObject instance, Species species)
	{
		
			if (ProbabilityCheck (species, x, z) > 1f)
				return true;
			else
				return false;
	}

	float ProbabilityCheck(Species species, int x, int z)
	{
		Debug.Log (species.enabler.name);
		float enablerScore = CountNeighbours (species.enabler.range, species.enabler, x, z);
		float disablerScore = CountNeighbours (species.disabler.range, species.disabler, x, z);
		float result = (enablerScore * species.probability) / disablerScore;
		Debug.Log (result);
		return result;
	}

	float CountNeighbours(int range, Species species, int x, int z)
	{
		int count = 0;
		//iterate through neighbour tiles
		for (int neighbourX = x - range; neighbourX <= x + range; neighbourX ++) {
			for (int neighbourZ = z - range; neighbourZ <= z + range; neighbourZ ++) {
				//exclude walls
				if (neighbourX >= 0 && neighbourX < columns && neighbourZ >= 0 && neighbourZ < rows) {
					//exclude self
					if (neighbourX != x || neighbourZ != z) {
						//check for other species
						Debug.Log("is a neighbour");
						if (species.speciesArray [neighbourX, neighbourZ] != null) { 
							count += 1;
						} else {
							count -= 1;
						}
					}
				}
			}
		}

		return (float)count;
	}


	//a function that returns a random position on the grid
	Vector3 RandomPosition()
	{
		int randomIndex = Random.Range (0, gridPositions.Count);
		Vector3 randomPosition = gridPositions [randomIndex];
		gridPositions.RemoveAt (randomIndex);
		return randomPosition;
	}

	//a function that instantiates a random number of random objects from a given array
	void LayoutObjectAtRandom (GameObject[] tileArray, int minimum, int maximum)
	{
		int objectCount = Random.Range (minimum, maximum + 1);

		for (int i = 0; i < objectCount; i++)
		{
			Vector3 randomPosition = RandomPosition ();
			GameObject tileChoice = tileArray[Random.Range(0,tileArray.Length)];
			Instantiate (tileChoice, randomPosition, Quaternion.identity);
		}
	}

	public void SetupScene ()
	{
		BoardSetup ();
		InitialiseList ();
		//MapIterator (groundTiles, grass);
		//MapIterator (treeTiles, tree);

		LayoutObjectAtRandom (berries, berryCount.minimum, berryCount.maximum);
		LayoutObjectAtRandom (trees, treeCount.minimum, treeCount.maximum);
		LayoutObjectAtRandom (lakes, lakeCount.minimum, lakeCount.maximum);

	}
		



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
