using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour {

	private float minSpeed;
	private float maxSpeed;
	public GameManager gameManager;
	Transform target;
	public UnityEngine.AI.NavMeshAgent agent;
	GameObject cam;

	// Use this for initialization
	void Start () {
		cam = GameObject.FindGameObjectWithTag ("MainCamera");

		cam.GetComponent<CameraZoom> ().onZoom += AdjustSpeed;

		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		minSpeed = 0.3f;
		maxSpeed = 1.8f;
	}
		

	// Update is called once per frame
	void Update () {
		if (agent.velocity.magnitude > 0) {
			gameManager.stamina -= 0.03f;
		}

		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;

			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {

				if ((hit.collider.tag != "Collectible") && (hit.collider.tag != "Container") && (!gameManager.inventoryOpen)) {
					agent.destination = hit.point;
				}
			}
		}

	}

	public void AdjustSpeed(object obj, EventArgs e){
		float zoom = cam.GetComponent<CameraZoom> ().currentZoom;
		float zoomMax = cam.GetComponent<CameraZoom> ().maxzoom;
		float zoomMin = cam.GetComponent<CameraZoom> ().minzoom;

		//calculate new speed
		float zoomRange = zoomMax - zoomMin;
		float zoomPctg = (zoom-zoomMin) / zoomRange;
		float speedRange = maxSpeed - minSpeed;
		float speedNorm = speedRange * zoomPctg;
		float newSpeed = minSpeed + speedNorm; 
		//apply new speed
		agent.speed = newSpeed;
	}



}
