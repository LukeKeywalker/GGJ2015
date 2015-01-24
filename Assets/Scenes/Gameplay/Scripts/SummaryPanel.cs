using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SummaryPanel : MonoBehaviour
{
    public UILabel m_pointsLabel;
    public UILabel m_recordLabel;

    public ButtonBehavior m_restartButton;
    public GameplayController m_gameplay;

    public PlayerScoreControl[] m_playerScores;
    public UIWidget[] m_placeholders;

	void Awake()
    {
        m_restartButton.Clicked += m_restartButton_Clicked;
    }

    void m_restartButton_Clicked(object obj)
    {
        gameObject.SetActive(false);
        m_gameplay.Restart();
    }

    public void SetSummary(
        int points,
        int record,
        int player1Points,
        int player2Points,
        int player3Points,
        int player4Points)
    {
        m_pointsLabel.text = points.ToString();
        m_recordLabel.text = record.ToString();

        m_playerScores[0].Set("", Color.white, player1Points);
        m_playerScores[1].Set("", Color.white, player2Points);
        m_playerScores[2].Set("", Color.white, player3Points);
        m_playerScores[3].Set("", Color.white, player4Points);

        List<PlayerScoreControl> playersToSort = new List<PlayerScoreControl>
        {
            m_playerScores[0],
            m_playerScores[1],
            m_playerScores[2],
            m_playerScores[3]
        };

        playersToSort.Sort((a, b) => { return b.Scores.CompareTo(a.Scores); });

        for (int i = 0; i < 4; i++)
        {
            playersToSort[i].transform.parent = m_placeholders[i].transform;
            playersToSort[i].transform.localPosition = Vector3.zero;
        }
    }
}
