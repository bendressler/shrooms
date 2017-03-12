using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickableItems : MonoBehaviour {

	public GameObject gameManager;
	GameObject player;
	GameObject cam;
	float zoom;
	float zoomsize;
	float maxdistance;
	bool visible;
	public Item pickableItem;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		player = GameObject.FindGameObjectWithTag ("Player");

		zoom = cam.GetComponent<CameraZoom> ().currentZoom;
		maxdistance = 1.5f;
		zoomsize = 2;
		GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (player.transform.position, gameObject.transform.position) > maxdistance) {
			GetComponent<MeshRenderer> ().enabled = false;
		}
		else{
			zoom = cam.GetComponent<CameraZoom> ().currentZoom;
			if (zoom < zoomsize) {
				GetComponent<MeshRenderer> ().enabled = true;
			} else {
				GetComponent<MeshRenderer> ().enabled = false;
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit[] hits;

			hits = Physics.RaycastAll (Camera.main.ScreenPointToRay (Input.mousePosition), 100);

			foreach (RaycastHit i in hits){
				if (i.collider.gameObject == this.gameObject) {
					if (Vector3.Distance (player.transform.position, transform.position) <= 3) 
					{
						//gameManager.GetComponent<GameManager> ().inventory.GetComponentInParent<Inventory> ().AddItem (pickableItem,1);
						Destroy (gameObject);
					}
				}
			}
				
			}
		}


	
}
