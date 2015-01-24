using UnityEngine;
using System.Collections;

public class BodyController : MonoBehaviour
{
	public Limb[] m_limbs;
	public ParticleSystem[] m_wounds;
	private Vector3[] m_pawDirection;

	// Use this for initialization
	void Start () {
		LizardInput input = LizardInput.Instance;
		input.PawGriped += HandlePawGriped;
		input.PawReleased += HandlePawReleased;
		input.PawDirectionChanged += HandlePawDirectionChanged;
		m_pawDirection = new Vector3[4];

		for (int i = 0; i < 4; ++i)
		{
			HandlePawGriped((LimbId)i);
			m_limbs[i].Wound = m_wounds[i];
		}
	}

	void HandlePawDirectionChanged (LimbId arg1, Vector3 arg2)
	{
		Vector3 direction = (Vector3)arg2;
		LimbId limb = (LimbId)arg1;

		m_pawDirection[(int)limb] = direction;
	}

	void HandlePawReleased (LimbId obj)
	{
		int limb = (int)((LimbId)obj);
		Vector3 direction = m_pawDirection[limb];
		m_limbs[limb].Shoot(m_pawDirection[limb]);
	}

	void HandlePawGriped (LimbId obj)
	{
		int limb = (int)((LimbId)obj);
		m_limbs[limb].Action(m_pawDirection[limb]);
		m_limbs[limb].Grab();
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
