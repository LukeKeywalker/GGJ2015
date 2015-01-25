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

    private GameplayState m_gameplayState;

    void Awake()
    {
        SetSatate(GameplayState.Playing);
    }

    void Update()
    {
        switch (m_gameplayState)
        {
            case GameplayState.Playing:
                if (m_water.WaterLevel > m_frogBody.position.y)
                    Die();
                break;
        }
    }

    public void Restart()
    {
        Application.LoadLevel("Gameplay");
    }

    private void SetSatate(GameplayState state)
    {
        m_gameplayState = state;

        switch (state)
        {
            case GameplayState.Playing:
                m_summaryPanel.gameObject.SetActive(false);
                break;
        }
    }

    private void Die()
    {
        m_summaryPanel.gameObject.SetActive(true);

        SetSatate(GameplayState.Summary);
    }
}
