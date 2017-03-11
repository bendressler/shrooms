using UnityEngine;
using System.Collections;
using System;

public class CameraZoom : MonoBehaviour {

	public delegate void ZoomHandler(object obj, EventArgs e);
	public event ZoomHandler onZoom;

	public GameObject player;
	public GameObject playerMesh;
	public float currentZoom;

	public float maxzoom;
	public float minzoom;
	float zoomspeed;
	Vector3 ownpos;
	float xoffset;
	float zoffset;

	// Use this for initialization
	void Start () {
		minzoom = 1.4f;
		maxzoom = 10f;
		zoomspeed = 0.1f;
		ownpos = transform.position;
		xoffset = player.transform.position.x - ownpos.x;
		zoffset = player.transform.position.z - ownpos.z;
		currentZoom = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		ownpos = transform.position;
		zoomspeed = currentZoom * 0.025f;

		if (Input.GetKey (KeyCode.UpArrow) && currentZoom >= minzoom) {
			currentZoom = this.transform.position.y - zoomspeed;
			if (onZoom != null) {
				onZoom (null, null);
			}
			if (currentZoom < (minzoom + 0.2f)) {
				playerMesh.SetActive (false);
			}
		}
		if (Input.GetKey (KeyCode.DownArrow) && currentZoom <= maxzoom) {
			currentZoom = this.transform.position.y + zoomspeed;
			if (onZoom != null) {
				onZoom (null, null);
			}
			if (currentZoom > (minzoom + 0.2f)) {
				playerMesh.SetActive (true);
			}
		}

	}
		

	void LateUpdate(){
		Vector3 playerpos = player.transform.position;
		transform.position = new Vector3 (playerpos.x + xoffset, transform.position.y, playerpos.z + zoffset);
		this.transform.position = new Vector3 (this.transform.position.x, currentZoom, this.transform.position.z);
	}
//send value to playerMovement or adjust player movement speed from here
	//	public UnityEngine.AI.NavMeshAgent agent;


}
