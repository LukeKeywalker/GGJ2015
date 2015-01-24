using UnityEngine;
using System.Collections;

public class PawTest : MonoBehaviour
{
    private Vector3 m_basePosition;

    void Awake()
    {
        m_basePosition = transform.position;
    }

    public void SetDirection(Vector3 direction)
    {
        transform.position = m_basePosition + direction;
    }

    public void SetGrip(bool grip)
    {
        if (grip)
            renderer.material.color = Color.green;
        else
            renderer.material.color = Color.red;
    }
}
