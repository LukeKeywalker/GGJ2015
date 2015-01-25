using UnityEngine;
using System.Collections;

public class GameplayCamera : MonoBehaviour
{
    public Transform m_trackingTarget;
    public Water m_water;

    private Vector3 m_baseShift;
    private Vector3 m_velocity;

    void Awake()
    {
        m_baseShift = m_trackingTarget.position - transform.position;
    }

	void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, m_trackingTarget.position - m_baseShift, ref m_velocity, 0.5f);
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, 10.8f, 27),
            Mathf.Max(transform.position.y, m_water.WaterLevel),
            transform.position.z);
	}
}
