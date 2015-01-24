using UnityEngine;
using System.Collections;

public class WaterSlice : MonoBehaviour
{
    private float m_power;
    private bool m_isMoveing;
    private float m_top;
    private float m_time;

    public float SmoothDampVelocity
    {
        get;
        set;
    }

    public bool IsMoving
    {
        get { return m_isMoveing; }
    }

    public float Top
    {
        get { return m_top; }
        set { m_top = value; }
    }

    public float Power
    {
        get { return m_power; }
        set { m_power = value; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (m_isMoveing)
        {
            m_top = Mathf.Sin(m_time) * m_power;

            m_power -= Time.deltaTime * 0.05f;
            if (m_power < 0.0f)
                m_power = m_power = 0.0f;

            m_time += Time.deltaTime * 5.0f;
        }

        Vector3 position = transform.position;
        position.y = m_top;

        transform.position = position;
	}

    public void Move(float power)
    {
        m_isMoveing = true;
        m_power = power;
        m_time = Mathf.PI;
    }
}
