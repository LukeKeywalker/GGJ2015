using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class HexTile : MonoBehaviour 
{
	public enum HexType
	{
		Dirt,
		Trees,
		Grass,
		Rocks,
		Sand,
		Water,
		Ice,
		Spikes,
		Tard,
		Lava
	}

	public ParticleSystem m_looseRockGrabParticles;

	public struct HexLogic
	{
		//acceleration modifier when hand grabs
		public float grip;

		public bool grabable;

		public Action<Transform> onHandHovers;
		public Action<Transform> onHandGrabs;

		public HexLogic(float _grip, bool _grabable)
		{
			grip = _grip;
			grabable = _grabable;
			onHandHovers = (Transform t) => {};
			onHandGrabs = (Transform t) => {};
		}
	}

	public GameObject m_model;
	public Material[] m_hexMaterials;

	private Dictionary<HexType, Action<Transform>> m_grabEffects;
	private Vector3 m_startingPosition;

	private Dictionary<HexType, Action<Transform>> m_hoverEffects;
	private bool m_wasTouched;

	public Transform m_grabbingHand;
	public List<Transform> m_hoverHands;
	public HexLogic m_logic;

	public HexType hexType
	{
		get { return m_hexType; }
		set 
		{ 


			m_hexType = value; 
			if (value == HexType.Water)
			{
				m_model.SetActive(false);
			}
			else
			{
				m_model.renderer.material = m_hexMaterials[(int)value];
			}
			m_logic = GameData.hexesLogic[value];
			m_logic.onHandGrabs = m_grabEffects[value];
		}
	}

	private HexType m_hexType;

	private float m_grabbingHandVelocity = 0.0f;
	private float m_slideAcceleration = 1.0f;
	private float m_handGrabTime = 0.0f;

	public void OnHandEnter(Transform hand)
	{
		if (!m_hoverHands.Contains(hand))
			m_hoverHands.Add(hand);
		if (m_logic.onHandHovers != null)
			m_logic.onHandHovers(hand);
	}

	public void OnHandExit(Transform hand)
	{
		if (m_hoverHands.Contains(hand))
			m_hoverHands.Remove(hand);
	}

	public bool OnHandGrab(Transform hand)
	{
		if (m_grabbingHand == null)
		{
			m_grabbingHand = hand;
			m_handGrabTime = 0.0f;
			if (m_logic.onHandGrabs != null)
			{
				m_wasTouched = true;
				m_logic.onHandGrabs(hand);
				return true;
			}
			return true;
		}

		return false;
	}

	public void OnHandDrop()
	{
		m_grabbingHand = null;
		//StopCoroutine ("RockFallOffCoroutine");
	}

	private void Awake()
	{
		m_grabEffects = new Dictionary<HexType, Action<Transform>> ()
		{
			{ HexType.Rocks, (Transform t) => { StartCoroutine ("RockFallOffCoroutine"); } },
			{ HexType.Dirt, (Transform t) => {}},
			{ HexType.Grass, (Transform t) => {}},
			{ HexType.Ice, (Transform t) => {}},
			{ HexType.Lava, (Transform t) => {}},
			{ HexType.Sand, (Transform t) => {}},
			{ HexType.Spikes, (Transform t) => {}},
			{ HexType.Tard, (Transform t) => {}},
			{ HexType.Trees, (Transform t) => {}},
			{ HexType.Water, (Transform t) => { OnHandDrop (); }}
		};

		m_hoverEffects = new Dictionary<HexType, Action<Transform>> ()
		{
			{ HexType.Rocks, (Transform t) => {} },
			{ HexType.Dirt, (Transform t) => {}},
			{ HexType.Grass, (Transform t) => {}},
			{ HexType.Ice, (Transform t) => {}},
			{ HexType.Lava, (Transform t) => {}},
			{ HexType.Sand, (Transform t) => {}},
			{ HexType.Spikes, (Transform t) => {}},
			{ HexType.Tard, (Transform t) => {}},
			{ HexType.Trees, (Transform t) => {}},
			{ HexType.Water, (Transform t) => {}},
		};
	}

	private void Start()
	{
		m_startingPosition = transform.localPosition;
	}

	private void Update()
	{
		if (m_grabbingHand != null)
			UpdateGrabbedHand();
		else
		{
			UpdateEmpty();
		}
	}

	private void UpdateEmpty()
	{
		if (hexType == HexType.Rocks)
		{
			//transform.localPosition = m_startingPosition;
			if (m_wasTouched)
				m_handGrabTime += Time.deltaTime;
		}
	}

	private void UpdateGrabbedHand()
	{
		m_handGrabTime += Time.deltaTime;
		m_grabbingHandVelocity += (1 - m_logic.grip) * m_slideAcceleration * Time.deltaTime;
		Vector3 oldPosition = m_grabbingHand.transform.position;
		float newY = oldPosition.y - m_grabbingHandVelocity * Time.deltaTime;
		m_grabbingHand.transform.position = new Vector3 (oldPosition.x, 
		                                                 newY,
		                                                 oldPosition.z);
		if (LevelGeneratorController.GetHexByPosition(m_grabbingHand.transform.position) != this)
		{
			m_grabbingHand.GetComponent<Limb>().NotifyHandDropped();
			OnHandDrop ();
		}

	}

	private IEnumerator RockFallOffCoroutine()
	{
		m_looseRockGrabParticles.Play();
		while (m_handGrabTime < GameData.rockLifetime)
		{
			Vector2 random = UnityEngine.Random.insideUnitCircle;
			transform.position += new Vector3(random.x, random.y) * Time.deltaTime *0.7f;
			yield return null;
		}
		m_looseRockGrabParticles.Stop();

		//hexType = HexType.Water;
		rigidbody2D.isKinematic = false;
		if (m_grabbingHand != null)
		{
			m_grabbingHand.GetComponent<Limb>().NotifyHandDropped();
			OnHandDrop ();
		}
	}	
}
