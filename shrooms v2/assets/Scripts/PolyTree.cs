using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolyTree : MonoBehaviour {

	GameObject cam;
	GameObject player;

	float zoom;
	float zoomSize;
	float maxDistance;

	Color canopyColor;
	Color tempColor;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		player = GameObject.FindGameObjectWithTag ("Player");

		zoom = cam.GetComponent<CameraZoom> ().currentZoom;
		maxDistance = 5;
		zoomSize = 25;

		canopyColor = GetComponentInChildren<MeshRenderer> ().materials [1].color;
	}

	// Update is called once per frame
	void Update () {
		zoom = cam.GetComponent<CameraZoom> ().currentZoom;
		if (Vector3.Distance (player.transform.position, gameObject.transform.position) > maxDistance) {
			GetComponentInChildren<MeshRenderer> ().enabled = true;
		}
		else{
			if (zoom < zoomSize) {
				tempColor = new Color(canopyColor.r,canopyColor.g,canopyColor.b,0.2f);
				//GetComponentInChildren<MeshRenderer> ().materials [1].shader.renderQueue.Equals ("Transparent");
			} else {
				GetComponentInChildren<MeshRenderer> ().enabled = true;
				tempColor = new Color(canopyColor.r,canopyColor.g,canopyColor.b,1f);
			}
	}
		GetComponentInChildren<MeshRenderer> ().materials [1].color = tempColor;
}
}
