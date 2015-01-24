using UnityEngine;
using System.Collections;

public class Waterfall : MonoBehaviour
{
    public WaterfallSlice m_waterfallSlicePrefab;

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
	}
}
