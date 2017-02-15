using UnityEngine;
using System.Collections;

public class photoInteraction : MonoBehaviour, IGvrGazeResponder {

	// ogScale numbers: (0.01, 0.01, 1)


	private float scale = 0.01f;
	private float maxScale = 0.02f;
	private float minScale = 0.01f;
	private float scaleSpeed = 0.1f;

	private string name;

	private Vector3 ogPosition;

	private bool isFocused = false;
	private bool isClicked = false;

	private float _animated_lerp;

	private GameObject camParent;
	private GameObject camChild;
	private GameObject grandpa;

	public void OnGazeEnter(){
		Focus ();
	}

	public void OnGazeExit(){
		leaveFocus ();
	}

	public void OnGazeTrigger(){
		isClicked = isClicked ? false : true;
		if (isClicked) {
			attachParent ();
		} else {
			detachParent ();
		}
	}

	// Use this for initialization
	void Start () {
		name = gameObject.name;
		ogPosition = gameObject.transform.position;
		camParent = GameObject.FindGameObjectWithTag ("camParent");
		camChild = camParent.transform.Find("Head").gameObject;
		grandpa = GameObject.Find ("grandpa");
	}

	public void Focus() {
		isFocused = true;

	}

	public void leaveFocus() {
		isFocused = false;

	}

	public void growAndShrink() {
		// grows if focused and not yet clicked
		if (isFocused && !isClicked) {
			// Grow
			scale += scaleSpeed * Time.deltaTime;
			// Limit the growth
			if (scale > maxScale) {
				scale = maxScale;
			}
			// Apply the new scale
			gameObject.transform.localScale = new Vector3 (scale, scale, scale);
			// move photo forward
			gameObject.transform.position = Vector3.Lerp(ogPosition,Vector3.forward * -.10f, Time.deltaTime * scaleSpeed) ;
			// disable the photos to the left and right of Panoramas so there isn't overlap
			givePanoramaPriority(true);
		} else if(scale > (minScale)) {
			// shrink
			scale -= scaleSpeed * Time.deltaTime;
			gameObject.transform.localScale = new Vector3 (scale, scale, scale);
			// shrinking isn't exact so set it original state at the end
			if (scale < minScale) gameObject.transform.localScale = new Vector3 (.01f, .01f, .01f);
			gameObject.transform.position = ogPosition;
			givePanoramaPriority (false);
		}
	}

	public void attachParent() {
		camParent.transform.parent = gameObject.transform;
		camParent.transform.localPosition = new Vector3 (0f, 0f, -65f);
		camParent.transform.localRotation = Quaternion.Euler (0f, -(camChild.transform.localRotation.eulerAngles.y), 0f);
//		Debug.Log (camChild.transform.localRotation.eulerAngles.y);
	}

	public void detachParent() {
		camParent.transform.parent = null;
		camParent.transform.localPosition = new Vector3 (0f, 0f, -1.75f);
		camParent.transform.localRotation = Quaternion.Euler (0f,0f,0f);
		// reset isClicked to false to re-enable growing
		isClicked = false;
	}

	public void givePanoramaPriority(bool enableOrDisable){
		if (gameObject.tag == "Panorama") {
			var columnNumber = int.Parse(name.Substring (3,1));
			var rowNumber = int.Parse(name.Substring (5, 1));
			var leftNeighbor = GameObject.Find ("col" + (columnNumber - 1) + "r" + rowNumber);
			var rightNeighbor = GameObject.Find ("col" + (columnNumber + 1) + "r" + rowNumber);
			leftNeighbor.GetComponent<Renderer> ().enabled = !enableOrDisable;
			rightNeighbor.GetComponent<Renderer> ().enabled = !enableOrDisable;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// for hovering (aka GazeEnter)
		growAndShrink ();

		// for clicking (aka onGazeTriggered)
//		Debug.Log(camParent);
	}

}
