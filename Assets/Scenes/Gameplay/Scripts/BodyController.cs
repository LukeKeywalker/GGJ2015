using UnityEngine;
using System.Collections;

public class BodyController : MonoBehaviour
{
	public Limb[] m_limbs;

	// Use this for initialization
	void Start () {
	
	}

	public void UseLimb(LimbId limb, Vector2 direction)
	{
		m_limbs[(int)limb].Action(direction);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q))
		{
			//m_armLeft.ToggleAttach();
			m_limbs[0].Action(new Vector3(0.0f, 1.0f));
		}
		else if (Input.GetKeyDown(KeyCode.W))
		{
			m_limbs[1].Action(new Vector3(0.0f, 1.0f));
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
			m_limbs[2].Action(new Vector3(0.0f, 1.0f));
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			m_limbs[3].Action(new Vector3(0.0f, 1.0f));
		}
	}
}
