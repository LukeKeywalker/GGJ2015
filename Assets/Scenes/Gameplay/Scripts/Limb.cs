﻿using UnityEngine;
using System.Collections;

public class Limb : MonoBehaviour {



	public Transform m_limbSlot;
	public Color m_colorHold;
	public Color m_colorLet;
	public Color m_colorTired;
	public GameObject m_handOpen;
	public GameObject m_handClosed;

	private Rigidbody2D m_rigidbody;
	private SpringJoint2D m_springJoint;
	private Vector3 m_position;
	private LineRenderer m_line;
	private float m_grip = 1.0f;
	private float m_gripLoseRate = 0.15f;
	public float m_grippLooseRateMultiplier = 1.0f;

	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_line = GetComponent<LineRenderer>();
		m_position = transform.position;
		m_springJoint = GetComponent<SpringJoint2D>();
	}

	private Color GripIndicator
	{
		set
		{
			m_line.SetColors(value, m_colorHold);
		}
	}

	private void OpenHand()
	{
		m_handOpen.SetActive(true);
		m_handClosed.SetActive(false);
	}

	private void CloseHand()
	{
		m_handOpen.SetActive(false);
		m_handClosed.SetActive(true);
	}

	public void Shoot(Vector2 direction)
	{
		if (!m_springJoint.enabled) return;

		StopCoroutine("LooseGrip");
		m_grip = 1.0f;
		m_rigidbody.isKinematic = false;
		m_rigidbody.AddForce(direction);
		OpenHand();
	}

	public void Grab()
	{
		m_rigidbody.isKinematic = true;
		GripIndicator = m_colorHold;
	 //StopCoroutine("GainGrip");
		StartCoroutine("LooseGrip");
		CloseHand();
	}

	public void Action(Vector2 normalizedDirection)
	{
		Vector2 direction = 7500.0f * normalizedDirection;
		if (m_rigidbody.isKinematic)
		{
			Shoot(direction);
		}
		else
		{
			Grab();
		}
	}

	private float reaction
	{
		get
		{
			return (transform.position - m_limbSlot.transform.position).magnitude / 2.5f;
		}
	}

	private IEnumerator LooseGrip()
	{
		while (true)
		{
			//renderer.material.color = Color.Lerp(m_colorHold, m_colorTired, 1.0f - m_grip);
			GripIndicator = Color.Lerp(m_colorHold, m_colorTired, 1.0f - m_grip);
			m_grip -= m_gripLoseRate * reaction * Time.deltaTime;
			if (m_grip <= 0.0f)
			{
				//Shoot(Vector2.zero);
				//m_springJoint.connectedBody = null;
				m_springJoint.enabled = false;
				break;
			}
			else {
				yield return null;
			}
		}
	}

	private IEnumerator GainGrip()
	{
		while (true)
		{
			//renderer.material.color = Color.Lerp(m_colorHold, m_colorTired, 1.0f - m_grip);
			GripIndicator = Color.Lerp(m_colorHold, m_colorTired, 1.0f - m_grip);
			m_grip += 1.0f * m_gripLoseRate * Time.deltaTime;
			if (m_grip >= 1.0f)
			{
				break;
			}
			else {
				yield return null;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (m_springJoint.enabled)
		{
			m_line.SetPosition(0, transform.position);
			m_line.SetPosition(1, m_limbSlot.position);
		}
		else
		{
			m_line.SetPosition(0, Vector2.zero);
			m_line.SetPosition(1, Vector3.zero);
		}
	}
}

