using UnityEngine;
using System.Collections;

public class changeTextureCarpetV2 : MonoBehaviour {
	
	private bool firstIsColliding = true;
	private bool secondIsColliding = true;
	
	public GameObject first; 
	public GameObject second;
	
	public Texture2D texture;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (firstIsColliding == false && secondIsColliding == false) {
			GetComponent<Renderer>().material.mainTexture = texture;
		}
	}
	
	void OnTriggerExit (Collider other) {
		if (other.gameObject == first) {
			firstIsColliding = false;
		}

		Update();
		
		if (other.gameObject == second) {
			secondIsColliding = false;
		}
	}
}
