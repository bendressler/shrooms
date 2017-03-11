using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TreeScript : MonoBehaviour {

	public GameObject spriteholder;

	GameObject player;
	GameObject cam;
	float zoom;
	float zoomsize;
	float maxdistance;
	bool visible;
	SpriteRenderer ownspriter;

	void GetCollisions(){
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1);
		if (hitColliders.Length > 0) {
			foreach (Collider i in hitColliders) {
				if (i.GetComponent<Species> () != null) {
					if (i.GetComponent<Species> ().speciesName == "Lake") {
							Destroy (this.gameObject);
					}
				}
			}
		}
	}


	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		player = GameObject.FindGameObjectWithTag ("Player");

		zoom = cam.GetComponent<CameraZoom> ().currentZoom;
		maxdistance = 3;
		zoomsize = 12;
		ownspriter = GetComponentInChildren<SpriteRenderer> ();

		GetCollisions ();
	}
	
	// Update is called once per frame
	void Update () {
		zoom = cam.GetComponent<CameraZoom> ().currentZoom;
		if (Vector3.Distance (player.transform.position, gameObject.transform.position) > maxdistance) {
			GetComponent<MeshRenderer> ().enabled = true;
			ownspriter.color = new Color (1f, 1f, 1f, 1f);
		}
		else{
			if (zoom < zoomsize) {
				GetComponent<MeshRenderer> ().enabled = false;
				ownspriter.color = new Color (1f, 1f, 1f, 0.25f);

			} else {
				GetComponent<MeshRenderer> ().enabled = true;
				ownspriter.color = new Color (1f, 1f, 1f, 1f);

			}
		}
	}
}
