using UnityEngine;
using System.Collections;

public class HexTile : MonoBehaviour 
{
	public enum HexType
	{
		Dirt,
		Trees,
		Grass,
		Rocks,
		Sand,
		Water,
		Ice,
		Spikes,
		Tard,
		Lava
	}

	public HexType hexType;

}
