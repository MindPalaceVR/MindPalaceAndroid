using UnityEngine;
using System.Linq;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class MakeNewImages : MonoBehaviour {

	public GameObject subjectX;

	void Start() {
		createPhotosphere ();
		assignImages ();
	}

	// Use this for initialization
	void createPhotosphere () {
//		double[] yPosition = {1.59,1.06,0.53,0,-0.53,-1.06,-1.59};
		double[] yPosition = {2.65,2.12,1.59,1.06,0.53,0,-0.53};
		double[] xPosition = {-1.75f,-1.6167f,-1.2374f,-0.66969f,0f,0.66969f,1.2374f,1.6167f,1.75f};
		double[] zPosition = {-1.75f,-1.0803f,-0.5125f,-0.1332f,0,-0.1332,-0.5125f,-1.0803f,-1.75f};
		double[] yAngles = {-90,-67.5,-45,-22.5,0,22.5,45,67.5,90};
//		double[] xAngles = {-45, -22.5, 0, 0, 0, 22.5, 45};
		double[] xAngles = {0,0,0,0,0,0,0};
		// number of columns
		for (int i = 0; i < 9; i++) {
			// number of rows
			for(int j = 0; j < 7; j++){
				GameObject photo = (GameObject)Object.Instantiate (subjectX, new Vector3((float)xPosition[i],(float)yPosition[j],(float)zPosition[i]), Quaternion.Euler((float)xAngles[j],(float)yAngles[i],0f));
				var grandpa = GameObject.Find ("grandpa");
				photo.transform.parent = grandpa.transform;
				photo.name = "col" + (i+1) + "r" + (j+1);
				Vector3 scale = new Vector3 (0.01f,0.01f,1);
				photo.transform.localScale = scale;

			}
		}
		subjectX.GetComponent<Renderer> ().enabled = false;
	}

	void assignImages() {
		try {
			Object[] imgPaths = Resources.LoadAll("", typeof(Sprite));
			GameObject[] images = GameObject.FindGameObjectsWithTag("Image");
			Debug.Log(imgPaths.Length);
			Debug.Log(images.Length);
			for(var i = 0; i < images.Length; i++){
//				Debug.Log(imgPaths[i]);
//				Debug.Log(images[i]);
				string path = "Assets/Resources/" + imgPaths[i];

				if(imgPaths[i] != null){
					double normalPhotoRatio = 4.0/3.0;
					var texture = imgPaths[i] as Sprite;

//					Debug.Log(imgPaths[i].ToString() +": "+texture.texture.width + "x" + texture.texture.height);
					if((texture.texture.width / texture.texture.height) > normalPhotoRatio){
						images[i].tag = "Panorama";
						Debug.Log(imgPaths[i].ToString() +" tagged Panorama");
					}
					images[i].GetComponent<SpriteRenderer>().sprite = texture;
				} else {
					Debug.Log("There is no image at " + path);
				}
					

			}
		} 

		finally {
//			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
