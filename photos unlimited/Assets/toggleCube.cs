using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleCube : MonoBehaviour, IGvrGazeResponder {

	public GameObject canvas;
	public bool toggle = false;

	public void OnGazeEnter(){
		
	}

	public void OnGazeExit(){

	}

	public void OnGazeTrigger(){
		toggle = toggle ? false : true;
		canvas.SetActive (toggle);
	}

	// Use this for initialization
	void Start () {
		
	}



	// Update is called once per frame
	void Update () {
		
	}
}
