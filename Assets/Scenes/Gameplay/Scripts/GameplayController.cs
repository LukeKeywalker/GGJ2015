using UnityEngine;
using System.Collections;

enum GameplayState
{
    Playing,
    Summary
}

public class GameplayController : MonoBehaviour
{
    public Water m_water;
    public Transform m_frogBody;
    public SummaryPanel m_summaryPanel;
	public GUIView m_view;

    public float m_maxPoints;

    private GameplayState m_gameplayState;
	private LevelGeneratorController m_levelGenerator;

    void Awake()
    {
		m_levelGenerator = FindObjectOfType<LevelGeneratorController> ();
        SetSatate(GameplayState.Playing);

        m_maxPoints = 0.0f;

        GameData.scores[0] = 0;
        GameData.scores[1] = 0;
        GameData.scores[2] = 0;
        GameData.scores[3] = 0;
    }

	void Start()
	{
		AudioManager.Instance.PlayMusic ();
		AudioManager.Instance.GetSoundByName ("Waterfall").Play();
	}

    void Update()
    {
        if (m_maxPoints < m_frogBody.position.y)
            m_maxPoints = m_frogBody.position.y;

        switch (m_gameplayState)
        {
            case GameplayState.Playing:
                if (m_water.WaterLevel > m_frogBody.position.y)
                    Die();
                break;
        }

		if (m_gameplayState == GameplayState.Playing)
			m_view.ChangeHeight (Mathf.RoundToInt (m_frogBody.transform.position.y));

		if (m_frogBody.position.y > m_levelGenerator.topmostHexLine.position.y - 25)
			m_levelGenerator.AddMoreTiles();
    }

    public void Restart()
    {
        Application.LoadLevel("Gameplay");
    }

    private void SetSatate(GameplayState state)
    {
        m_gameplayState = state;

		m_view.m_heightLabel.gameObject.SetActive (state == GameplayState.Playing);
        switch (state)
        {
            case GameplayState.Playing:
                m_summaryPanel.gameObject.SetActive(false);
                break;
        }
    }

    private void Die()
    {
        int record = PlayerPrefs.GetInt("record", 0);

        m_summaryPanel.SetSummary(
            (int)m_maxPoints,
            record,
            GameData.scores[0],
            GameData.scores[1],
            GameData.scores[2],
            GameData.scores[3]);

        if ((int)m_maxPoints > record)
        {
            PlayerPrefs.SetInt("record", (int)m_maxPoints);
            PlayerPrefs.Save();
        }

		AudioManager.Instance.GetSoundByName ("Splash").Play();

        m_summaryPanel.gameObject.SetActive(true);

        SetSatate(GameplayState.Summary);
    }
}
