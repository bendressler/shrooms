using UnityEngine;
using System.Collections;

public class TreeScript : MonoBehaviour {

	public GameObject spriteholder;

	GameObject player;
	GameObject cam;
	float zoom;
	float zoomsize;
	float maxdistance;
	bool visible;
	SpriteRenderer ownspriter;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		player = GameObject.FindGameObjectWithTag ("Player");

		zoom = cam.GetComponent<CameraZoom> ().currentzoom;
		maxdistance = 3;
		zoomsize = 12;
		ownspriter = GetComponentInChildren<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		zoom = cam.GetComponent<CameraZoom> ().currentzoom;
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
