using UnityEngine;
using System.Collections;

public class Fly : BasePickupItem 
{
	private Vector3 m_startPosition;
	private float m_radius = 10.0f;
	private float m_velocity = 0.1f;

	private float m_timeElapsed = 0.0f;

	private Vector3 m_destination;
	private float m_randomPeriodModifier;
	
	private void Awake()
	{
		onPickedUp = (Transform t) => {};
		m_randomPeriodModifier = Random.Range (0, 2 * Mathf.PI);
	}
	
	private void Start()
	{
		m_startPosition = this.transform.position;
//		GoToRandomDestination ();
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

	private void Update()
	{
		m_timeElapsed += Time.deltaTime;
		Vector3 newPosition = GetPosition ();
		this.transform.LookAt (newPosition);
		this.transform.position = newPosition;
	}

	private Vector3 GetPosition()
	{
		float r = Mathf.Sin (10 * m_velocity * (m_timeElapsed - m_randomPeriodModifier));
		float x = Mathf.Cos (m_velocity * m_timeElapsed) * r;
		float y = Mathf.Sin (m_velocity * m_timeElapsed) * (1 - r);
//		Debug.Log ((new Vector3 (x, y, m_startPosition.z) * m_radius + m_startPosition).x);
		return new Vector3 (x, y, 0) * m_radius + m_startPosition;
	}
}
