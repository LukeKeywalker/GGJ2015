using UnityEngine;
using System.Collections;

public class WellcomeController : MonoBehaviour
{
    public ButtonBehavior m_play;
    public ButtonBehavior m_credits;
    public ButtonBehavior m_closeCredits;

    void Awake()
    {
        m_play.Clicked += m_play_Clicked;
        m_credits.Clicked += m_credits_Clicked;
        m_closeCredits.Clicked += m_closeCredits_Clicked;

        m_closeCredits.gameObject.SetActive(false);
    }

    void m_closeCredits_Clicked(object obj)
    {
        m_closeCredits.gameObject.SetActive(false);
        m_play.gameObject.SetActive(true);
        m_credits.gameObject.SetActive(true);
    }

    void m_credits_Clicked(object obj)
    {
        m_closeCredits.gameObject.SetActive(true);
        m_play.gameObject.SetActive(false);
        m_credits.gameObject.SetActive(false);
    }

    void m_play_Clicked(object obj)
    {
        Application.LoadLevel("Gameplay");
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
