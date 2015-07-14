using UnityEngine;
using System.Collections;

public class ProblemSourceScript : MonoBehaviour {

	public GameObject SolveObject;
	public PickUpType AnimType; 

	public GameObject GetSolveObject()
	{
		return SolveObject;
	}
	
}

public enum PickUpType
{
	IsPickingUpOnFloor,
	IsPickingUpHorizontal
}
