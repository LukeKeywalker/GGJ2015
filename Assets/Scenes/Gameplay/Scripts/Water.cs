using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour
{
    public WaterSlice m_waterSlicePrefab;

    private WaterSlice[] m_waterSlices;

    void Awake()
    {
        InitializeWaterSlices();
    }

    private void InitializeWaterSlices()
    {
        float step = 0.02f;
        int count = (int)(2.0f / step) + 1;

        m_waterSlices = new WaterSlice[count];

        int index = 0;
        for (float x = -1.0f; x <= 1.0f; x += step)
        {
            m_waterSlices[index] = (WaterSlice)Instantiate(m_waterSlicePrefab);
            m_waterSlices[index].transform.localScale = Vector3.one;
            m_waterSlices[index].transform.localPosition = new Vector3(x, 0.0f, 0.0f);

            index++;
        }
    }
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
    {
        float range = 10.0f;

        if (Input.GetKeyDown(KeyCode.Space))
            m_waterSlices[20].Move(0.2f);

        for (int i = 0; i < m_waterSlices.Length; i++)
        {
            if (!m_waterSlices[i].IsMoving)
            {
                float left = m_waterSlices[i].Top;
                float right = m_waterSlices[i].Top;

                if (i > 0)
                    left = m_waterSlices[i - 1].Top;
                if (i < m_waterSlices.Length - 1)
                    right = m_waterSlices[i + 1].Top;

                float velocity = m_waterSlices[i].SmoothDampVelocity;
                m_waterSlices[i].Top = Mathf.SmoothDamp(m_waterSlices[i].Top, (left + right) / 2.0f, ref velocity, 0.001f);
                m_waterSlices[i].SmoothDampVelocity = velocity;
            }
        }
	}

    private void GetMovingWaves(int index, out int leftIndex, out int rightIndex)
    {
        leftIndex = -1;
        rightIndex = -1;

        for (int i = 0; i < index; i++)
        {
            if (m_waterSlices[i].IsMoving)
                leftIndex = i;
        }

        for (int i = index + 1; i < m_waterSlices.Length; i++)
        {
            if (m_waterSlices[i].IsMoving)
                rightIndex = i;
        }
    }
}
