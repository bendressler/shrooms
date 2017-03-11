using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPanel : MonoBehaviour {

	private bool active;

	// Use this for initialization
	void Start () {
		active = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			if (active) {
				this.gameObject.SetActive (false);
				active = false;
			} else {
				this.gameObject.SetActive (true);
				active = true;
			}
		}
	}
}
