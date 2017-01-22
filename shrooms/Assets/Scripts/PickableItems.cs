using UnityEngine;
using System.Collections;

public class PickableItems : MonoBehaviour {

	GameObject player;
	GameObject cam;
	float zoom;
	float zoomsize;
	float maxdistance;
	bool visible;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		player = GameObject.FindGameObjectWithTag ("Player");

		zoom = cam.GetComponent<CameraZoom> ().currentzoom;
		maxdistance = 3;
		zoomsize = 6;
		GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		zoom = cam.GetComponent<CameraZoom> ().currentzoom;
		if (Vector3.Distance (player.transform.position, gameObject.transform.position) > maxdistance) {
			GetComponent<MeshRenderer> ().enabled = false;
		}
		else{
			if (zoom < zoomsize) {
				GetComponent<MeshRenderer> ().enabled = true;
			} else {
				GetComponent<MeshRenderer> ().enabled = false;
			}
		}
	}
}
