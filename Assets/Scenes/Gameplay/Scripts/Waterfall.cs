using UnityEngine;
using System.Collections;

public class Waterfall : MonoBehaviour
{
    public WaterfallSlice m_waterfallSlicePrefab;
    public Transform m_waterContainer;
    public Water m_water;

    private WaterfallSlice[] m_waterfallSlices;

    void Awake()
    {
        InitializeWaterfallSlices();
    }

    private void InitializeWaterfallSlices()
    {
        int count = 30;

        m_waterfallSlices = new WaterfallSlice[count];

        for (int i = 0; i < count; i++)
        {
            m_waterfallSlices[i] = (WaterfallSlice)Instantiate(m_waterfallSlicePrefab);
            m_waterfallSlices[i].transform.parent = m_waterContainer;
            m_waterfallSlices[i].Restart();
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	void Update()
    {
	    for (int i = 0; i < m_waterfallSlices.Length; i++)
        {
            if (!m_waterfallSlices[i].IsUsed)
                m_waterfallSlices[i].Restart();
        }

        m_waterContainer.position = new Vector3(m_waterContainer.position.x, m_water.WaterLevel, m_waterContainer.position.z);
	}
}
