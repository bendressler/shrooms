using UnityEngine;
using System.Collections;
using System;

public class ZoomDepth : MonoBehaviour {

	//CameraZoom script with Depth Of Field instead of y position

	public delegate void ZoomHandler(object obj, EventArgs e);
	public event ZoomHandler onZoom;

	public GameObject player;
	public float currentZoom;

	public float maxzoom;
	public float minzoom;
	float zoomspeed;
	Vector3 ownpos;
	float xoffset;
	float zoffset;

	// Use this for initialization
	void Start () {
		minzoom = 6f;
		maxzoom = 65f;
		zoomspeed = 0.5f;
		ownpos = transform.position;
		xoffset = player.transform.position.x - ownpos.x;
		zoffset = player.transform.position.z - ownpos.z;
		currentZoom = GetComponent<Camera>().fieldOfView;
	}

	// Update is called once per frame
	void Update () {
		ownpos = transform.position;

		if (Input.GetKey (KeyCode.UpArrow) && GetComponent<Camera>().fieldOfView >= minzoom) {
			currentZoom = GetComponent<Camera>().fieldOfView - zoomspeed;
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - (0.05f), this.transform.position.z);
			if (onZoom != null) {
				onZoom (null, null);
			}
		}
		if (Input.GetKey (KeyCode.DownArrow) && GetComponent<Camera>().fieldOfView <= maxzoom) {
			currentZoom = GetComponent<Camera>().fieldOfView + zoomspeed;
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + (0.05f), this.transform.position.z);
			if (onZoom != null) {
				onZoom (null, null);
			}
		}

	}


	void LateUpdate(){
		Vector3 playerpos = player.transform.position;
		transform.position = new Vector3 (playerpos.x + xoffset, transform.position.y, playerpos.z + zoffset);
		GetComponent<Camera> ().fieldOfView = currentZoom;
	}
	//send value to playerMovement or adjust player movement speed from here
	//	public UnityEngine.AI.NavMeshAgent agent;


}
