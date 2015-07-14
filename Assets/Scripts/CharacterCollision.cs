using UnityEngine;
using System.Collections;

public class CharacterCollision : MonoBehaviour
{
	GameObject carryObject;
	GameObject solveHit;
	float putAwayTime;
	private Animator anim;
	private System.DateTime startPickUpTime;
	private const string PATH_TO_RIGHT_HAND = "Boy:Hips/Boy:Spine/Boy:Spine1/Boy:Spine2/Boy:RightShoulder/Boy:RightArm/Boy:RightForeArm/Boy:RightHand";
	
	// Use this for initialization
	void Start ()
	{
		//chest = GameObject.FindGameObjectWithTag ("Chest");
		//carryObject = GameObject.FindGameObjectWithTag ("BallTag");
		anim = GetComponent <Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		checkForPickUp ();
		
		moveToSolve ();
	}
	
	void checkForPickUp ()
	{
		if (putAwayTime == 0 && carryObject != null && ((System.DateTime.UtcNow - startPickUpTime).TotalMilliseconds >= 1300)) {
			carryObject.transform.parent = this.transform.Find(PATH_TO_RIGHT_HAND);
			carryObject.transform.localPosition = new Vector3 (0.1f, -0.22f, 0);
			anim.SetLayerWeight(anim.GetLayerIndex("Carry"),(float)(System.DateTime.UtcNow - startPickUpTime).TotalSeconds/4);
		} 
		if (putAwayTime == 0 && carryObject != null && (System.DateTime.UtcNow - startPickUpTime).TotalMilliseconds >= 3500) {
			this.GetComponent<ClickToMove> ().resumeWalking ();
			ProblemSourceScript source = carryObject.GetComponent<ProblemSourceScript> ();
			anim.SetBool (source.AnimType.ToString (), false);
			anim.SetLayerWeight(anim.GetLayerIndex("Carry"),(float)(System.DateTime.UtcNow - startPickUpTime).TotalSeconds/4);
		}
	}
	
	void moveToSolve ()
	{
		if (solveHit != null && carryObject != null) {
			if (putAwayTime <= 2.0) {
				//this.GetComponent<ClickToMove>().pauseWalking();
				putAwayTime += Time.deltaTime;
				ProblemSourceScript source = carryObject.GetComponent<ProblemSourceScript> ();
				carryObject.transform.parent=null;
				carryObject.transform.position = Vector3.Lerp (carryObject.transform.position, source.GetSolveObject ().transform.position + new Vector3 (0, 2, 0), Time.deltaTime * 2);
				carryObject.transform.localScale = Vector3.Lerp (carryObject.transform.localScale, new Vector3 (0.2f, 0.2f, 0.2f), Time.deltaTime * 2);
				anim.SetLayerWeight(anim.GetLayerIndex("Carry"),0);
			} else {
				//this.GetComponent<ClickToMove>().resumeWalking();
				carryObject = null;
				solveHit = null;
				Debug.Log ("Removed");
			}
		}
	}
	
	void OnTriggerEnter (Collider coll)
	{
		
		ProblemSourceScript source = coll.gameObject.GetComponent<ProblemSourceScript> ();
		
		if (source != null && carryObject == null) {
			Debug.Log ("Pickup");
			carryObject = coll.gameObject;
			startPickUpTime = System.DateTime.UtcNow;
			this.GetComponent<ClickToMove> ().pauseWalking ();
			anim.SetBool (source.AnimType.ToString (), true);
			putAwayTime = 0;
		} else {
			if (carryObject != null && coll.gameObject != null) {
				GameObject solve = carryObject.GetComponent<ProblemSourceScript> ().GetSolveObject ();
				if (coll.gameObject == solve) {
					solveHit = coll.gameObject;
				}
			}
		}
	}
}
