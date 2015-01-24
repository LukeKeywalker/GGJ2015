using UnityEngine;
using System.Collections;

public class WaterfallSlice : MonoBehaviour
{
    public float m_speed = 1.0f;
    public float m_offset;
    public GameObject m_sliceModel;

    private bool m_isUsed;

    public bool IsUsed
    {
        get { return m_isUsed; }
    }

	void Start()
    {
	
	}
	
	void Update()
    {
        if (!m_isUsed)
            return;

        //m_speed = 4.0f;
        m_offset += Time.deltaTime * m_speed;

        m_sliceModel.renderer.material.mainTextureOffset = new Vector2(0, m_offset);

        if (m_offset >= 1.0f)
            m_isUsed = false;
	}

    public void Restart()
    {
        m_isUsed = true;

        m_offset = -1.0f;

        m_speed = Random.Range(0.5f, 1.5f);

        transform.localScale = new Vector3(
            Random.Range(1.0f, 10.0f),
            Random.Range(2.0f, 4.0f),
            1.0f);

        transform.localPosition = new Vector3(
            Random.Range(-0.9f, 0.9f),
            Random.Range(1.0f, 2.0f),
            0.0f);

        //m_sliceModel.renderer.material.color = m_sliceModel.renderer.material.color * Random.Range(0.1f, 0.12f);
    }
}
