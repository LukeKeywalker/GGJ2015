using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelGeneratorController : MonoBehaviour 
{
	public GameObject m_hexLinePrefab;
	public HexTile m_hexTilePrefab;
	public Material[] m_hexMaterials;
	
	private List<GameObject> m_hexLines;

	private float m_hexHeight = 10;
	private float m_dy = 5.0f;
	private float m_dx = 7.5f;


	private int m_initialHeight = 100;
	private int m_width = 18;
	
	private void Start()
	{
		FillMap ();
	}

	private void FillMap ()
	{
		m_hexLines = new List<GameObject> ();
		for (int count = 0; count < m_initialHeight; count++)
		{
			GameObject container = (GameObject)Instantiate(m_hexLinePrefab);
			container.transform.parent = this.transform;
			container.transform.localScale = Vector3.one;
			container.transform.localRotation = Quaternion.identity;
			container.transform.localPosition = new Vector3(0, m_hexHeight * count, 0);
			m_hexLines.Add(container);
			FillLine(container, count);
		}
	}

	private void FillLine(GameObject line, int height)
	{
		for (int count = 0; count < m_width; count ++)
		{
			HexTile hex = (HexTile)Instantiate (m_hexTilePrefab);
			hex.transform.parent = line.transform;
			hex.transform.localScale = Vector3.one;
			hex.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
			hex.transform.localPosition = new Vector3(count * m_dx, (count % 2) * m_dy, 0);
			int area = Array.FindLastIndex<int>(GameData.areaHeights, ((x) => { return (height >= x);} ));
			float[] probabilities = GameData.probabilities[area];
			hex.hexType = GetRandomTile(probabilities);
			hex.renderer.material = m_hexMaterials[(int)hex.hexType];
		}
	}

	private HexTile.HexType GetRandomTile(float[] probabilities)
	{
		float random = UnityEngine.Random.Range (0.0f, 1.0f);
		float sum = 0;
		for (int i = 0; i < probabilities.Length; i++)
		{
			sum += probabilities[i];
			if (random < sum)
				return (HexTile.HexType)i;
		}
		Debug.Log (sum);
		throw new UnityException("Probabilities don't sum");
	}
	
}
