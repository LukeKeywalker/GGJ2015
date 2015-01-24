using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData  
{
	//time for a rock to fall off while grabbed
	public static float rockLifetime = 2.5f;

	public static int[] areaHeights = new int[]
	{
		0, 5, 10, 15, 20, 25
	};

	//Dirt, Trees, Grass, Rocks, Sand, Water, Ice, Spikes, Tard, Lava
	public static float[][] probabilities = new float[][]
	{
<<<<<<< HEAD
		/*new float[] { 0.5f, 0.0f, 0.5f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
		new float[] { 0.4f, 0.0f, 0.3f, 0.3f, 0.0f, 0f, 0f, 0f, 0f, 0f},
		new float[] { 0.4f, 0.0f, 0.3f, 0.3f, 0.0f, 0f, 0f, 0f, 0f, 0f},
		new float[] { 0.4f, 0.0f, 0.3f, 0.3f, 0.0f, 0f, 0f, 0f, 0f, 0f},
		new float[] { 0.4f, 0.0f, 0.3f, 0.3f, 0.0f, 0f, 0f, 0f, 0f, 0f}*/

		new float[] { 0.4f, 0.35f, 0.25f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
=======
//		new float[] { 0.4f, 0.35f, 0.25f, 0f, 0f, 0f, 0f, 0f, 0f, 0f},
		new float[] { 0.5f, 0.0f, 0.0f, 0.5f, 0f, 0f, 0f, 0f, 0f, 0f},
>>>>>>> origin/master
		new float[] { 0.3f, 0.3f, 0.15f, 0.15f, 0.1f, 0f, 0f, 0f, 0f, 0f},
		new float[] { 0.2f, 0.2f, 0.1f, 0.15f, 0.15f, 0.1f, 0.1f, 0f, 0f,0f },
		new float[] { 0.27f, 0.0f, 0f, 0.30f, 0.16f, 0.1f, 0.08f, 0.04f, 0.05f,0f },
		new float[] { 0.24f, 0f, 0f, 0.27f, 0.1f, 0f, 0.2f, 0.06f, 0.08f, 0.05f }
	};

	public static Dictionary<HexTile.HexType, HexTile.HexLogic> hexesLogic = new Dictionary<HexTile.HexType, HexTile.HexLogic> ()
	{
<<<<<<< HEAD
		{ HexTile.HexType.Dirt, new HexTile.HexLogic(0.9f, 0) },
		{ HexTile.HexType.Grass, new HexTile.HexLogic(1, 0) },
		{ HexTile.HexType.Ice, new HexTile.HexLogic(1, 0) },
		{ HexTile.HexType.Lava, new HexTile.HexLogic(1, 0) },
		{ HexTile.HexType.Rocks, new HexTile.HexLogic(1, 0) },
		{ HexTile.HexType.Sand, new HexTile.HexLogic(1, 0) },
		{ HexTile.HexType.Spikes, new HexTile.HexLogic(1, 0) },
		{ HexTile.HexType.Tard, new HexTile.HexLogic(1, 0) },
		{ HexTile.HexType.Trees, new HexTile.HexLogic(1, 0) },
		{ HexTile.HexType.Water, new HexTile.HexLogic(1, 0) },
=======
		{ HexTile.HexType.Dirt, new HexTile.HexLogic(1, true) },
		{ HexTile.HexType.Grass, new HexTile.HexLogic(1, true) },
		{ HexTile.HexType.Ice, new HexTile.HexLogic(1, true) },
		{ HexTile.HexType.Lava, new HexTile.HexLogic(1, false) },
		{ HexTile.HexType.Rocks, new HexTile.HexLogic(1, true) },
		{ HexTile.HexType.Sand, new HexTile.HexLogic(1, true) },
		{ HexTile.HexType.Spikes, new HexTile.HexLogic(1, false) },
		{ HexTile.HexType.Tard, new HexTile.HexLogic(1, true) },
		{ HexTile.HexType.Trees, new HexTile.HexLogic(0.5f, true) },
		{ HexTile.HexType.Water, new HexTile.HexLogic(1, false) },
>>>>>>> origin/master
	};
}
