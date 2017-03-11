using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour {

	public int totalLevel;

	public Item[] goals;
	public int[] numbers;

	void Start(){
		DontDestroyOnLoad (this);
		totalLevel = 2;
	}

	public Item CurrentGoalItem(int level){
		return goals [level];
	}

	public int CurrentGoalCount(int level){
		return numbers [level];
	}

}
