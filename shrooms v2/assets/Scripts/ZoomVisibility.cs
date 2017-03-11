using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZoomVisibility : MonoBehaviour {

	public GameObject gameManager;
	public float visLowerThresh;
	public float visHigherThresh;
	GameObject player;
	GameObject cam;
	float zoom;
	public Material[] transMat;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		cam = GameObject.FindGameObjectWithTag ("MainCamera");
		player = GameObject.FindGameObjectWithTag ("Player");

		cam.GetComponent<CameraZoom> ().onZoom += AdjustVisibility;
		zoom = cam.GetComponent<CameraZoom> ().currentZoom;

		transMat = GetComponentInChildren<MeshRenderer>().materials;
		foreach (Material i in transMat) {
			Color itemColor = i.color;
			Color tempColor;
			tempColor = new Color (itemColor.r, itemColor.g, itemColor.b, 0f);
			i.color = tempColor;
		}
	}
	
	// Update is called once per frame
	void Update () {
		zoom = cam.GetComponent<CameraZoom> ().currentZoom;
	}

	public void AdjustVisibility(object obj, EventArgs e){

		float zoomMax = cam.GetComponent<CameraZoom> ().maxzoom;
		float zoomMin = cam.GetComponent<CameraZoom> ().minzoom;

		//get range of visibility for this item
		float visRange = visHigherThresh - visLowerThresh;
		//get zoom level normalised on item visibility range
		float zoomPctg = (zoom - zoomMin) / visRange;
		float visPctg = 0;

		//if item is within visibility range, apply normalised zoom to visibility scale
		if (zoom < visHigherThresh) {
			visPctg = 1 - ((visRange * zoomPctg) / visRange);

		}

		//apply visibility to alpha value of all materials in array
		foreach (Material i in transMat) {

			Color itemColor = i.color;
			Color tempColor;
			tempColor = new Color (itemColor.r, itemColor.g, itemColor.b, visPctg);
			i.color = tempColor;

		}
	}
}
