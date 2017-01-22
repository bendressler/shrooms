using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public GameObject player;
	public float currentzoom;

	float maxzoom;
	float minzoom;
	float zoomspeed;
	Vector3 ownpos;
	float xoffset;
	float zoffset;

	// Use this for initialization
	void Start () {
		minzoom = 2.5f;
		maxzoom = 25f;
		zoomspeed = 0.1f;
		ownpos = transform.position;
		xoffset = player.transform.position.x - ownpos.x;
		zoffset = player.transform.position.z - ownpos.z;
		currentzoom = ownpos.y;
	}
	
	// Update is called once per frame
	void Update () {
		ownpos = transform.position;

		if (Input.GetKey (KeyCode.UpArrow) && transform.position.y >= minzoom) {
			currentzoom = ownpos.y - zoomspeed;
		}
		if (Input.GetKey (KeyCode.DownArrow) && transform.position.y <= maxzoom) {
			currentzoom = ownpos.y + zoomspeed;
		}

	}

	void LateUpdate(){
		Vector3 playerpos = player.transform.position;
		transform.position = new Vector3 (playerpos.x + xoffset, currentzoom, playerpos.z + zoffset);
	}
}
