using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData  
{
	//time for a rock to fall off while grabbed
	public static readonly float rockLifetime = 1.0f;

	public static readonly int[] areaHeights = new int[]
	{
		0, 10, 20, 30, 40, 50
	};

	//Dirt, Trees, Grass, Rocks, Sand, Water, Ice, Spikes, Tard, Lava
	public static readonly float[][] probabilities = new float[][]
	{
		/*new float[] { 0.5f, 0.0f, 0.5f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
		new float[] { 0.4f, 0.0f, 0.3f, 0.3f, 0.0f, 0f, 0f, 0f, 0f, 0f},
		new float[] { 0.4f, 0.0f, 0.3f, 0.3f, 0.0f, 0f, 0f, 0f, 0f, 0f},
		new float[] { 0.4f, 0.0f, 0.3f, 0.3f, 0.0f, 0f, 0f, 0f, 0f, 0f},
		new float[] { 0.4f, 0.0f, 0.3f, 0.3f, 0.0f, 0f, 0f, 0f, 0f, 0f}*/

		new float[] { 0.57f, 0.38f, 0f, 0.05f, 0f, 0f, 0f, 0f, 0f, 0f},
		new float[] { 0.45f, 0.35f, 0f, 0.14f, 0f, 0.06f, 0f, 0f, 0f, 0f},
		new float[] { 0.35f, 0.28f, 0f, 0.16f, 0f, 0.12f, 0.09f, 0f, 0f,0f },
		new float[] { 0.27f, 0.08f, 0f, 0.30f, 0f, 0.17f, 0.18f, 0f, 0f,0f },
		new float[] { 0.24f, 0f, 0f, 0.28f, 0f, 0.21f, 0.25f, 0.02f, 0f, 0f },
		new float[] { 0.20f, 0f, 0f, 0.25f, 0f, 0.25f, 0.25f, 0.05f, 0f, 0f }
	};

	//small bug (1 point), big bug (2 point)
	public static readonly float[] bugProbabilities = new float[]
	{
		0.2f, 0.05f
	};

	public static readonly Dictionary<HexTile.HexType, HexTile.HexLogic> hexesLogic = new Dictionary<HexTile.HexType, HexTile.HexLogic> ()
	{

		{ HexTile.HexType.Dirt, new HexTile.HexLogic(0.93f, true) },
		{ HexTile.HexType.Grass, new HexTile.HexLogic(0.97f, true) },
		{ HexTile.HexType.Ice, new HexTile.HexLogic(0.3f, true) },
		{ HexTile.HexType.Lava, new HexTile.HexLogic(1, false) },
		{ HexTile.HexType.Rocks, new HexTile.HexLogic(0.94f, true) },
		{ HexTile.HexType.Sand, new HexTile.HexLogic(0.85f, true) },
		{ HexTile.HexType.Spikes, new HexTile.HexLogic(1, true) },
		{ HexTile.HexType.Tard, new HexTile.HexLogic(0, true) },
		{ HexTile.HexType.Trees, new HexTile.HexLogic(1, true) },
		{ HexTile.HexType.Water, new HexTile.HexLogic(0, false) },
	};


	//DYNAMIC DATA
	public static int[] scores = new int[] {0, 0, 0, 0};
	
}
