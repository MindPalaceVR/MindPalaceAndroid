using UnityEngine;
using System.Collections;

public class ResizeImages : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject[] images = GameObject.FindGameObjectsWithTag("Image");
		for (int i = 0; i < images.Length; i++) {
			images [i].transform.localScale = new Vector3 (1f,1f,1f);
		}
	}

}
