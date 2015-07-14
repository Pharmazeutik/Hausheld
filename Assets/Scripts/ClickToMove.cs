using UnityEngine;
using System.Collections;


public class ClickToMove : MonoBehaviour 
{
	public float speed;
	public CharacterController controller;
	public LayerMask myLayerMask;
	public LayerMask problemLayerMask;

	private Animator anim;
	private Vector3 position;
	private NavMeshAgent nav;
	private bool isWalkingPaused;
	
	
	public static Vector3 cursorPosition;
	
	
	// Use this for initialization
	void Start () 
	{
		position = transform.position;
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButton(0)&&!isWalkingPaused)
		{
			//Locate where the player clicked on the terrain
			anim.SetBool("IsWalking",true);
			locatePosition();
			
		}
		
		//Move the player to the position
		nav.SetDestination (position);
		if (transform.position.x == position.x && transform.position.z == position.z) 
		{
			anim.SetBool("IsWalking",false);
		}
	}
	
	void locatePosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if(Physics.Raycast(ray, out hit, 1000,myLayerMask))
		{
			
			position = hit.point;
			
		}
		else if(Physics.Raycast(ray, out hit, 1000,problemLayerMask))
		{
			
			position = hit.point;
			Debug.Log("hit");
			
		}
	}

	public void stopWalking(){
		position = transform.position;
		anim.SetBool("IsWalking",false);
	}

	public void pauseWalking(){
		stopWalking ();
		isWalkingPaused = true;
	}

	public void resumeWalking(){
		isWalkingPaused = false;
	}
	
}
