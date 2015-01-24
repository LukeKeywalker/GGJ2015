using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
			FillLine(container);
		}
	}

	private void FillLine(GameObject line)
	{
		for (int count = 0; count < m_width; count ++)
		{
			HexTile hex = (HexTile)Instantiate (m_hexTilePrefab);
			hex.transform.parent = line.transform;
			hex.transform.localScale = Vector3.one;
			hex.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
			hex.transform.localPosition = new Vector3(count * m_dx, (count % 2) * m_dy, 0);
			hex.hexType = (Random.Range(0, 1.0f)) > 0.5f ? HexTile.HexType.Blue : HexTile.HexType.Green;
			hex.renderer.material = m_hexMaterials[(int)hex.hexType];
		}
	}
	
}
