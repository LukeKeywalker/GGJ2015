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
			m_limbs[i].Wound = m_wounds[i];
			m_limbs[i].Grab();
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

		Debug.Log (((LimbId)obj).ToString() + " release.");
	}

	void HandlePawGriped (LimbId obj)
	{
		int limb = (int)((LimbId)obj);
		m_limbs[limb].Grab();

		Debug.Log (((LimbId)obj).ToString() + " grab.");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q))
		{
			//m_armLeft.ToggleAttach();
		}
		else if (Input.GetKeyDown(KeyCode.W))
		{
		}
		else if (Input.GetKeyDown(KeyCode.A))
		{
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
		}
	}
}
