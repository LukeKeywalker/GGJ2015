using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HexLineController : MonoBehaviour 
{
	private LinkedList<HexTile> m_hexes;
	
	private float m_dx = 7.5f;
	private float m_dy = 5.0f;

	/// <summary>
	/// Switches the last hex to the left/right.
	/// </summary>
	/// <param name="toTheLeft">If set to <c>true</c> to the left, false to the <c>right</c>.</param>
	public void SwitchHex(bool toTheLeft)
	{
		HexTile hex;
		if (toTheLeft)
		{
			hex = m_hexes.Last.Value;
			m_hexes.RemoveLast();
			m_hexes.AddFirst(hex);
		}
		else
		{
			hex = m_hexes.First.Value;
			m_hexes.RemoveFirst();
			m_hexes.AddLast(hex);
		}
		float newX = hex.transform.localPosition.x + (m_hexes.Count * m_dx * ((toTheLeft) ? -1 : 1));
		float newY;
		if (m_hexes.Count % 2 == 0)
			newY = hex.transform.localPosition.y;
		else
			newY = (hex.transform.localPosition.y == 0) ? -m_dy : 0;
		hex.transform.localPosition = new Vector3 (newX, newY, 0);
	}

	private void Start()
	{
		HexTile[] hexes = GetComponentsInChildren<HexTile> ();
		SortHexesByX (hexes);
		m_hexes = new LinkedList<HexTile> ();
		foreach (HexTile hex in hexes)
			m_hexes.AddLast(hex);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
			SwitchHex(true);
		if (Input.GetKeyDown(KeyCode.S))
			SwitchHex(false);
	}

	private void SortHexesByX(HexTile[] hexes)
	{
		Comparison<HexTile> cmp = (x, y) =>
		{
			if (x.transform.localPosition.x < y.transform.localPosition.x)
				return -1;
			if (x.transform.localPosition.x == y.transform.localPosition.x)
				return 0;
			return 1;
		};
		Array.Sort<HexTile> (hexes, cmp);
	}
}
