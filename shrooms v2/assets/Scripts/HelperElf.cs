using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperElf : MonoBehaviour {

	//class that contains an object and the helpers preference to it. use to define a relationship between two species
	[System.Serializable]
	public class Preference {
		public Species prefObject;
		public float prefValue;

		public Preference(Species prefObject, float prefValue){
			this.prefObject = prefObject;
			this.prefValue = prefValue;
		}
	}

	public Vector3 birthplace;
	public BoardManager boardManager;
	public Transform speciesHolder;

	public Preference[] preferences;
	public List<GameObject> collisions = new List<GameObject>();


	public Species dropObject;
	public float probDrop;
	public float probDie;
	public float probSplit;
	public float probTurn;
	public float dropSplitMulti;
	public bool randomStep;
	public float stepDist;
	public float range;

	Vector3 currentDir;
	Vector3 left;
	Vector3 up;
	Vector3 right;
	Vector3 down;

	float lifetime;

	public void Init(float stepDist, Vector3 birthplace, Preference[] preferences, Species dropObject, float probDrop, float probDie, float probSplit, float probTurn, float dropSplitMulti, bool randomStep, Transform speciesHolder){

		this.stepDist = stepDist;
		this.birthplace = birthplace;
		this.preferences = preferences;
		this.dropObject = dropObject;
		this.probDrop = probDrop;
		this.probDie = probDie;
		this.probSplit = probSplit;
		this.probTurn = probTurn;
		this.dropSplitMulti = dropSplitMulti;
		this.randomStep = randomStep;
		this.speciesHolder = speciesHolder;

		this.transform.position = birthplace;

	}

	// Use this for initialization
	void Start () {
		left = new Vector3(0 - stepDist, 0,0);
		up = new Vector3(0,0, stepDist);
		right = new Vector3(stepDist, 0,0);
		down = new Vector3(0,0,-stepDist);	
		currentDir = right;

	}

	// Update is called once per frame
	void Update () {
		if (!randomStep) {
			NotRandomStep ();
		} else {
			RandomStep ();
		}
		DropObject ();
		Spawn ();
		Die ();
		lifetime += 0.01f;
		GetCollisions();
	}

	//walks through rows left to right, at end jumps to first position of next upper row
	void NotRandomStep(){
		if (this.transform.position.x < boardManager.columns - 1) {
			this.transform.position = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
		} else {
			if (this.transform.position.z < boardManager.columns - 1) {
				this.transform.position = new Vector3 (0, transform.position.y, transform.position.z + 1);
			} else {
				boardManager.helperCounter++;
				Destroy (this.gameObject);
			}
		}
	}

	//randomly changes direction based on the preset probability
	void RandomStep(){
		if (Random.value < probTurn) {
			float shuffle = Random.value;

			if (shuffle <= 0.25f) {
				currentDir = left;
			} else if (shuffle <= 0.5f) {
				currentDir = up;
			} else if (shuffle <= 0.75f) {
				currentDir = right;
			} else if (shuffle <= 1f) {
				currentDir = down;
			}
		}
			Vector3 newPos = transform.position + currentDir;

			if ((newPos.x < boardManager.columns) && (newPos.x >= 0) && (newPos.z < boardManager.rows) && (newPos.z >= 0)) {
				transform.position = newPos;
				
			}
		

	}

	//call GetCollision, adapt likelihood to drop object and instantiate if applicable
	void DropObject(){
		float likelihood = probDrop;

		likelihood += checkPriorities (GetCollisions());
		if (Random.value < likelihood) {
			InstantiateObject ();
		}
	}

	//check priority values for all colliding objects and return positive or negative probability modifier
	float checkPriorities(List<GameObject> collisions){

		List<float> prefValues = new List<float> ();

		foreach (GameObject i in collisions) {
			if (i.GetComponent<Species> () != null) {
				foreach (Preference p in preferences) {
					if (string.Equals(p.prefObject.gameObject.name.ToString(),i.GetComponent<Species> ().speciesName.ToString())){
						prefValues.Add (p.prefValue);
					}
				}
			}
		}
		float sum = 0;
		foreach (float f in prefValues) {
			sum += f;
		}
		return sum;
	}

	//send a collision sphere to catch all collisions in preset range and return them
	List<GameObject> GetCollisions(){
		List<GameObject> collisionObjects = new List<GameObject>();
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, range);
		if (hitColliders.Length > 0) {
			foreach (Collider i in hitColliders) {
				collisionObjects.Add (i.gameObject);
			}
		}
		return collisionObjects;
	}
		

	//drop the preset species object and add it to the boardManagers list
	void InstantiateObject(){
		GameObject toInstantiate = dropObject.gameObject;
		GameObject instance = Instantiate (toInstantiate, new Vector3 (this.transform.position.x, 0f, this.transform.position.z), Quaternion.identity) as GameObject;
		boardManager.allObjects.Add (instance);

	}


	//Destroy instance on coin throw or increase death probability, if out of board bounds or if maximum helper or object bounds have been exceeded
	void Die(){
		if (Random.value < probDie) {
			Destroy (this.gameObject);
			boardManager.helperCounter++;
		} else {
			probDie = probDie * (0.1f + lifetime);
		}
		if ((transform.position.x > boardManager.columns - 1) || (transform.position.x < 0) || (transform.position.z > boardManager.rows - 1) || (transform.position.z < 0)) {
			Destroy (this.gameObject);
			boardManager.helperCounter++;
		}

		if (boardManager.allHelpers.Length > boardManager.maxHelpers) {
			Destroy(this.gameObject);
			boardManager.helperCounter++;
		}

		if (boardManager.allObjects.Count > boardManager.maxObjects) {
			Destroy(this.gameObject);

		}

	}

	//spawn a new instance of itself and adjust death probability
	void Spawn(){
		
		if (Random.value < probSplit) {
			GameObject helper = this.gameObject;
			GameObject instance;
			instance = Instantiate (helper, new Vector3 (transform.position.x, 0f, transform.position.z), Quaternion.identity) as GameObject;
			instance.GetComponent<HelperElf> ().boardManager = boardManager;
			probDie = probSplit * dropSplitMulti;
		}
	}


}
