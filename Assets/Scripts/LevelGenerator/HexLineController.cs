using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HexLineController : MonoBehaviour 
{
	public static int m_hexWidth = 18;

	public HexTile m_hexPrefab;

	public Material[] m_hexMaterials;

	private List<HexTile> m_hexes;

	 
	private float m_dx = 7.5f;
	private float m_dy = 5.0f;

	private void Start()
	{

	}

	private void Update()
	{
	}

	public void InitializeHexes(List<HexTile> hexes)
	{
		for (int hexIndex = 0; hexIndex < m_hexWidth; hexIndex ++)
		{
			m_hexes[hexIndex] = hexes[hexIndex];
			m_hexes[hexIndex].renderer.material = m_hexMaterials[(int)hexes[hexIndex].hexType];
		}
	}
}
