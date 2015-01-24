using UnityEngine;
using System.Collections;

public class PlayerScoreControl : MonoBehaviour
{
    public UILabel m_playerName;
    public UILabel m_scoresLabel;

    private int m_scores;

    public int Scores
    {
        get { return m_scores; }
    }

    public void Set(string playerName, Color color, int scores)
    {
        //m_playerName.text = playerName;
        //m_playerName.gradientBottom = color;
        m_scores = scores;
        m_scoresLabel.text = scores.ToString();
    }
}
