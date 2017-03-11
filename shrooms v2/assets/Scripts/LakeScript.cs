using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakeScript : MonoBehaviour {


	void GetCollisions(){
		Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, 1);
		if (hitColliders.Length > 0) {
			foreach (Collider i in hitColliders) {
				if (i.GetComponent<Species> () != null) {
					if (i.GetComponent<Species> ().speciesName == "Lake") {
						if (i.gameObject != this.gameObject) {
							Destroy (this.gameObject);
						}
					}
				}
				else if (i.gameObject.tag == "Wall") {
					Destroy (this.gameObject);
				}
			}
		}
	}


	// Use this for initialization
	void Start () {
		GetCollisions ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
