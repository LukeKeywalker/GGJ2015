using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelGeneratorController : MonoBehaviour 
{
	public GameObject m_hexLinePrefab;
	public HexTile m_hexTilePrefab;
	public BasePickupItem[] m_itemPrefabs;

	public Transform topmostHexLine
	{
		get { return m_hexLines[m_hexLines.Count - 1].transform; }
	}

	private List<GameObject> m_hexLines;

	private float m_hexHeight;
	private float m_dy;
	private float m_dx;


	private int m_initialHeight = 60;
	private int m_width = 18;

	private int m_currentHeight = 0;
	private int m_refills = 0;

	public static HexTile GetHexByPosition(Vector3 position)
	{
		RaycastHit hit;
		if (Physics.Raycast(position, Vector3.forward, out hit))
			return hit.transform.parent.GetComponent<HexTile>();

		Debug.LogWarning ("No hex under hand");
		return null;
	}

	public void ResetAndFill()
	{
		foreach (Transform child in this.transform)
			Destroy(child.gameObject);
		FillMap ();
	}

	public void AddMoreTiles()
	{
		int newHeight = m_refills * ((GameData.areaHeights [GameData.areaHeights.Length - 1]) -
						GameData.areaHeights [GameData.areaHeights.Length - 2]);
		for (int count = m_currentHeight + 1; count < m_currentHeight + 2; count ++)
			AddLine(count);

		m_currentHeight += 1;
	}

	private void Awake()
	{
		float scale = m_hexTilePrefab.transform.localScale.x;
		m_hexHeight = 10 * scale;
		m_dy = 0.75f * m_hexHeight;
		m_dx = 0.5f * m_hexHeight;
		ResetAndFill ();
	}
	
	private void Start()
	{

	}

	private void AddLine(int height)
	{
		GameObject container = (GameObject)Instantiate(m_hexLinePrefab);
		container.transform.parent = this.transform;
		container.transform.localScale = Vector3.one;
		container.transform.localRotation = Quaternion.identity;
		container.transform.localPosition = new Vector3(0, 1.5f * m_hexHeight * height, 0);
		m_hexLines.Add(container);
		FillLine(container, height);
	}

	private void FillMap ()
	{
		m_hexLines = new List<GameObject> ();
		for (int count = 0; count < m_initialHeight; count++)
		{
			AddLine(count);
		}
		m_currentHeight = m_initialHeight - 1;
	}

	private void FillLine(GameObject line, int height)
	{
		for (int count = 0; count < 2*m_width; count ++)
		{
			HexTile hex = (HexTile)Instantiate (m_hexTilePrefab);
			hex.transform.parent = line.transform;
			hex.transform.localScale = m_hexTilePrefab.transform.localScale;
			hex.transform.localRotation = Quaternion.identity;
			hex.transform.localPosition = new Vector3(count * m_dx, (count % 2) * m_dy, 0);
			int area = Array.FindLastIndex<int>(GameData.areaHeights, ((x) => { return (height >= x);} ));
			if (area == -1)
				area = GameData.areaHeights.Length - 2;
			float[] probabilities = GameData.probabilities[area];
			/*
			if (count < 7 || count > 2 * m_width - 8)
				hex.hexType = HexTile.HexType.Water;
			else if (count < 10 || count > 2 * m_width - 11)
				hex.hexType = HexTile.HexType.Sand;
			else
			*/
				hex.hexType = GetRandomTile(probabilities);

			if (UnityEngine.Random.Range(0, 1.0f) > 0.9f)
			{
				Fly bug = GetRandomBug();
				if (bug != null)
				{
					BasePickupItem item = (BasePickupItem)Instantiate (bug);
					item.transform.parent = hex.transform;
					item.transform.localScale = Vector3.one;
					item.transform.localRotation = Quaternion.Euler(0, 180, 0);
					item.transform.localPosition = new Vector3(0, 0, -10);
				}
			}
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

	private Fly GetRandomBug()
	{
		float random = UnityEngine.Random.Range (0.0f, 1.0f);
		float sum = 0;
		for (int i = 0; i < GameData.bugProbabilities.Length; i++)
		{
			sum += GameData.bugProbabilities[i];
			if (random < sum)
				return m_itemPrefabs[i] as Fly;
		}
		return null;
	}

	private void Update()
	{

	}
}
