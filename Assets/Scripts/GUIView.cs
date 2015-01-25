using UnityEngine;
using System.Collections;

public class GUIView : MonoBehaviour 
{
	public UILabel m_heightLabel;
	public UILabel m_highestHeight;

	public UILabel[] m_playerScores;

	public void RefreshView()
	{
		for (int playerIndex = 0; playerIndex < m_playerScores.Length; playerIndex++)
			m_playerScores[playerIndex].text = GameData.scores[playerIndex].ToString();
	}

	public void ChangeScore(int playerIndex, int newScore)
	{
		m_playerScores [playerIndex].text = newScore.ToString ();
	}

	public void ChangeHeight(int newHeight)
	{
		m_heightLabel.text = newHeight.ToString ();
	}

	public void ChangeHighestHeight(int newValue)
	{
		m_highestHeight.text = newValue.ToString ();
	}

	private void Start()
	{
		RefreshView ();
	}


}
