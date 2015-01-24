using UnityEngine;
using System.Collections;

public class Limb : MonoBehaviour {



	public Transform m_limbSlot;
	public Color m_colorHold;
	public Color m_colorLet;

	private Rigidbody2D m_rigidbody;
	private Vector3 m_position;
	private LineRenderer m_line;

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_line = GetComponent<LineRenderer>();
		m_position = transform.position;
	}

	public void Shoot(Vector2 direction)
	{
		m_rigidbody.isKinematic = false;
		m_rigidbody.AddForce(direction);
		renderer.material.color = m_colorLet;
	}

	public void Grab()
	{
		m_rigidbody.isKinematic = true;
		renderer.material.color = m_colorHold;
	}

	public void Action(Vector2 normalizedDirection)
	{
		Vector2 direction = 500.0f * normalizedDirection;
		if (m_rigidbody.isKinematic)
		{
			Shoot(direction);
		}
		else
		{
			Grab();
		}
	}
	
	// Update is called once per frame
	void Update () {
		m_line.SetPosition(0, transform.position);
		m_line.SetPosition(1, m_limbSlot.position);
	}
}

