using UnityEngine;
using System.Collections;

public class Fly : BasePickupItem 
{
	private Vector3 m_startPosition;
	private float m_radius = 4.0f;
	private float m_velocity = 0.1f;
	
	private Vector3 m_destination;
	
	private void Awake()
	{
		onPickedUp = (Transform t) => {};
	}
	
	private void Start()
	{
		m_startPosition = this.transform.position;
		GoToRandomDestination ();
	}
	
	private IEnumerator GetToDestination()
	{
		while (this.transform.position != m_destination)
		{
			this.transform.position = Vector3.MoveTowards(
				this.transform.position,
				m_destination,
				m_velocity);
			
			yield return null;
		}
		
		GoToRandomDestination ();
	}
	
	private void GoToRandomDestination()
	{
		float randomDegree = Random.Range (0.0f, 360.0f);
		float x = m_radius * Mathf.Cos (Mathf.Deg2Rad * randomDegree);
		float y = m_radius * Mathf.Sin (Mathf.Deg2Rad * randomDegree);
		m_destination = new Vector3 (x + m_startPosition.x, y + m_startPosition.y, this.transform.position.z);
		this.transform.LookAt (new Vector3 (m_destination.x, m_destination.y, m_destination.z));
		StartCoroutine ("GetToDestination");
	}
}
