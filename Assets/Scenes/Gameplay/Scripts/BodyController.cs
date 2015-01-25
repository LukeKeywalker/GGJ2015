using UnityEngine;
using System.Collections;

public class BodyController : MonoBehaviour
{


	public Limb[] m_limbs;
	public ParticleSystem[] m_wounds;
	private Vector3[] m_pawDirection;

	public float m_gripLoseRate = 0.15f;
	public float m_grippLooseRateMultiplier = 1.0f;
	public float m_jumpForce = 3500.0f;

	private int m_numLimbs = 4;

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
			m_limbs[i].m_jumpForce = m_jumpForce;
			m_limbs[i].m_gripLoseRate = m_gripLoseRate;
			m_limbs[i].m_grippLooseRateMultiplier = m_grippLooseRateMultiplier;
			m_limbs[i].LimbBroke = OnLimBroke;
		}
	}

	void OnLimBroke()
	{
		Debug.Log ("OnLimbBroke");
		m_numLimbs--;
		Debug.Log ("NumLimbs = " + m_numLimbs.ToString());
		for (int i = 0; i < 4; i++)
		{
			SpringJoint2D joint = m_limbs[i].GetComponent<SpringJoint2D>();
			joint.frequency = 0.3f + 0.2f * (float)m_numLimbs;
		}
		AudioManager.Instance.GetSoundByName ("Blood");
	}

	void HandlePawDirectionChanged (LimbId arg1, Vector3 arg2)
	{
		Vector3 direction = (Vector3)arg2;
		LimbId limb = (LimbId)arg1;

		m_pawDirection[(int)limb] = direction;
		m_limbs[(int)limb].transform.up = direction;
		m_limbs[(int)limb].Wound.transform.forward = direction;
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
}
