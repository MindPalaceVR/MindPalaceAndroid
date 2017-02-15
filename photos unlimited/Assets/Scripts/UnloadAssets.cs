using UnityEngine;
using System.Collections;

public class UnloadAssets : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Resources.UnloadUnusedAssets ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
