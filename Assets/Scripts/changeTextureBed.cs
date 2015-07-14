using UnityEngine;
using System.Collections;

public class changeTextureBed : MonoBehaviour {

	private bool origamiIsColliding = true;
	private bool bookFrontIsColliding = true;
	private bool bookMidIsColliding = true;
	private bool bookLastIsColliding = true;
	
	private GameObject origami;
	private GameObject bookFront; 
	private GameObject bookMid;
	private GameObject bookLast;
	
	public Texture2D texture;
	
	// Use this for initialization
	void Start () {
		origami = GameObject.Find("PaperCrane 3");
		bookFront = GameObject.Find("bookFront");
		bookMid = GameObject.Find("bookMid");
		bookLast = GameObject.Find("bookLast");
	}
	
	// Update is called once per frame
	void Update () {
		if (origamiIsColliding == false && bookFrontIsColliding == false &&
		    bookMidIsColliding == false && bookLastIsColliding == false) {
			GetComponent<Renderer>().material.mainTexture = texture;
		}
	}
	
	void OnTriggerExit (Collider other) {
		if (other.gameObject == origami) {
			origamiIsColliding = false;
		}
		
		if (other.gameObject == bookFront) {
			bookFrontIsColliding = false;
		}

		if (other.gameObject == bookMid) {
			bookMidIsColliding = false;
		}

		if (other.gameObject == bookLast) {
			bookLastIsColliding = false;
		}
	}
}
