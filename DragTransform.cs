using System.Collections;
using UnityEngine;

class DragTransform : MonoBehaviour
{
	private Color mouseOverColor = Color.blue;
	private Color originalColor = Color.yellow;
	private bool dragging = false;
	private float distance;
	public GameObject camera;


	void Awake(){

	}
	
	
	void OnMouseEnter()
	{
		GetComponent<Renderer>().material.color = mouseOverColor;
	}
	
	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = originalColor;
	}
	
	void OnMouseDown()
	{
		distance = Vector3.Distance(this.transform.position, camera.transform.position);
		dragging = true;
	}
	
	void OnMouseUp()
	{
		dragging = false;
	}
	
	void Update()
	{
		if (dragging)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Vector3 rayPoint = ray.GetPoint(distance);
			this.transform.position = rayPoint;
		}
	}
}
