using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTexture : MonoBehaviour {

	public Texture[] sprites;

	// Use this for initialization
	void Start () {
		int random = Random.Range (0, sprites.Length);
		GetComponentInChildren<MeshRenderer> ().material.mainTexture = sprites [random];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
