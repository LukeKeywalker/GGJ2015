using UnityEngine;
using System.Collections;

public class Limb : MonoBehaviour 
{	
	public delegate void LimbBrokeHandler();
	public LimbBrokeHandler LimbBroke;

	public Transform m_limbSlot;
	public Color m_colorHold;
	public Color m_colorLet;
	public Color m_colorTired;
	public GameObject m_handOpen;
	public GameObject m_handClosed;
	public int m_id;

	private GUIView m_gui;

	public ParticleSystem Wound { get; set; }
	private Rigidbody2D m_rigidbody;
	private SpringJoint2D m_springJoint;
	private Vector3 m_position;
	private LineRenderer m_line;
	private float m_grip = 1.0f;
	public float m_gripLoseRate = 0.15f;
	public float m_grippLooseRateMultiplier = 1.0f;
	public float m_jumpForce = 3500.0f;
	public ParticleSystem m_handWound;

	private bool m_cooldown;
	private float m_cooldownTimer = 0.5f;


	void Awake() {
		m_gui = FindObjectOfType<GUIView> ();
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_line = GetComponent<LineRenderer>();
		m_position = transform.position;
		m_springJoint = GetComponent<SpringJoint2D>();
	}

	private Color GripIndicator
	{
		set
		{
			m_line.SetColors(m_colorHold, value);
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

	public void NotifyHandDropped()
	{
		if (!m_springJoint.enabled) return;
		StopCoroutine("LooseGrip");
		m_grip = 1.0f;
		m_rigidbody.isKinematic = false;
		OpenHand();
	}

	public void Shoot(Vector2 normalizedDirection)
	{
		Vector2 direction = m_jumpForce * normalizedDirection;
		//if (!m_springJoint.enabled) return;

		StopCoroutine("LooseGrip");
		m_grip = 1.0f;
		m_rigidbody.isKinematic = false;
		m_rigidbody.AddForce(direction);
		OpenHand();

		try
		{
			LevelGeneratorController.GetHexByPosition (this.transform.position).OnHandDrop ();
		}
		catch (System.NullReferenceException)
		{
			Debug.Log("ignoring hand released event");
		}
	}
	public void resetCooldown(){
		m_cooldown =false;
	}

	public void Grab()
	{
		if(!m_cooldown){
			m_cooldown=true;
			Invoke("resetCooldown",m_cooldownTimer);
			GripIndicator = m_colorHold;
			StartCoroutine("LooseGrip");
			CloseHand();


			HexTile hex = LevelGeneratorController.GetHexByPosition (this.transform.position);
			if (hex != null)
			{
				hex.OnHandGrab (this.transform);
				m_rigidbody.isKinematic = hex.m_logic.grabable;
			}
			else
				Debug.Log("ignoring on hand grab event");
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
				if (LimbBroke != null && m_springJoint.enabled)
				{
					LimbBroke();
				}

				m_springJoint.enabled = false;
				Wound.Play();
				m_handWound.Play();

				return false;
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

	void OnTriggerEnter2D(Collider2D col)
	{
		BasePickupItem item = col.transform.parent.GetComponent<BasePickupItem> ();
		if (item != null)
		{
			GameData.scores[m_id] += item.scoreValue;
			m_gui.RefreshView();
			item.onPickedUp(this.transform);
			Destroy(item.gameObject);
		}
	}
}

