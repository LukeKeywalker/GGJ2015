using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour
{
    private UITexture m_tex;

    void Awake()
    {
        m_tex = GetComponent<UITexture>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Rect rect = m_tex.uvRect;
        rect.x += Time.deltaTime * 0.1f;
        m_tex.uvRect = rect;
	}
}
