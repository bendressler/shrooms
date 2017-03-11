using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {

	public Button yourButton;
	public GameObject panel;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		panel.gameObject.SetActive(false);
		GameObject player = GameObject.Find ("Player");
		player.GetComponent<UnityEngine.AI.NavMeshAgent> ().speed = 1;
		player.GetComponent<UnityEngine.AI.NavMeshAgent> ().destination = player.transform.position;
	}
}
	