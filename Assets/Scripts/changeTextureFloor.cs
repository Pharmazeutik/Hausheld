using UnityEngine;
using System.Collections;



public class changeTextureFloor : MonoBehaviour {

	private bool ballIsColliding = true;
	private bool origamiIsColliding = true;

	private GameObject ball; 
	private GameObject origami;

	public Texture2D texture;

	// Use this for initialization
	void Start () {
		ball = GameObject.FindGameObjectWithTag("BallTag");
		origami = GameObject.Find("PaperCrane 2");
	}
	
	// Update is called once per frame
	void Update () {
		if (ballIsColliding == false && origamiIsColliding == false) {
			GetComponent<Renderer>().material.mainTexture = texture;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.gameObject == ball) {
			ballIsColliding = false;
		}

		if (other.gameObject == origami) {
			origamiIsColliding = false;
		}
	}

}
