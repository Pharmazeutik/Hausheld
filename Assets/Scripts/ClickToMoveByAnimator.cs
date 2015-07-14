using UnityEngine;
using System.Collections;


public class ClickToMoveByAnimator : MonoBehaviour 
{
	public float speed;
	public CharacterController controller;
	public LayerMask myLayerMask;
	
	private Animator anim;
	private Vector3 position;
	private NavMeshAgent nav;
	private bool isWalkingPaused;

	public float deadZone = 5f;
	public float speedDampTime = 0.1f;              // Damping time for the Speed parameter.
	public float angularSpeedDampTime = 0.7f;       // Damping time for the AngularSpeed parameter
	public float angleResponseTime = 0.6f;          // Response time for turning an angle into angularSpeed.

	public static Vector3 cursorPosition;
	
	
	// Use this for initialization
	void Start () 
	{
		position = transform.position;
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();	

		// We need to convert the angle for the deadzone from degrees to radians.
		deadZone *= Mathf.Deg2Rad;
	}


	void OnAnimatorMove ()
	{
		// Set the NavMeshAgent's velocity to the change in position since the last frame, by the time it took for the last frame.
		nav.velocity = anim.deltaPosition / Time.deltaTime;
		
		// The gameobject's rotation is driven by the animation's rotation.
		transform.rotation = anim.rootRotation;
	}

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButton(0)&&!isWalkingPaused)
		{
			//Locate where the player clicked on the terrain
			//anim.SetBool("IsWalking",true);
			locatePosition();
			//Move the player to the position
			nav.SetDestination (position);
		}
		

		// Otherwise the speed is a projection of desired velocity on to the forward vector...
		float speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
		
		// ... and the angle is the angle between forward and the desired velocity.
		float angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);

		if(Mathf.Abs(angle) < deadZone)
		{
			// ... set the direction to be along the desired direction and set the angle to be zero.
			transform.LookAt(transform.position + nav.desiredVelocity);
			angle = 0f;
		}

		// Angular speed is the number of degrees per second.
		float angularSpeed = angle / angleResponseTime;
		
		// Set the mecanim parameters and apply the appropriate damping to them.
		anim.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
		anim.SetFloat("Angular", angularSpeed, angularSpeedDampTime, Time.deltaTime);

//		if (transform.position.x == position.x && transform.position.z == position.z) 
//			anim.SetBool("IsWalking",false);

	}

	float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
	{
		// If the vector the angle is being calculated to is 0...
		if(toVector == Vector3.zero)
			// ... the angle between them is 0.
			return 0f;
		
		// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
		float angle = Vector3.Angle(fromVector, toVector);
		
		// Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
		Vector3 normal = Vector3.Cross(fromVector, toVector);
		
		// The dot product of the normal with the upVector will be positive if they point in the same direction.
		angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
		
		// We need to convert the angle we've found from degrees to radians.
		angle *= Mathf.Deg2Rad;
		
		return angle;
	}

	void locatePosition()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if(Physics.Raycast(ray, out hit, 1000,myLayerMask))
		{
			
			position = hit.point;
			Debug.Log(position);
			
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
