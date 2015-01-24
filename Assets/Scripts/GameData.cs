using UnityEngine;
using System.Collections;

public class GameData  
{
	public static int[] areaHeights = new int[]
	{
		0, 30, 60, 90, 120
	};

	public static float[][] probabilities = new float[][]
	{
		new float[] { 0.4f, 0.35f, 0.25f, 0f, 0f, 0f, 0f, 0f, 0f, },
		new float[] { 0.3f, 0.3f, 0.15f, 0.15f, 0.1f, 0f, 0f, 0f, 0f, },
		new float[] { 0.2f, 0.2f, 0.1f, 0.15f, 0.15f, 0.1f, 0.1f, 0f, 0f },
		new float[] { 0.2f, 0.1f, 0f, 0.15f, 0.1f, 0.1f, 0.15f, 0.5f, 0.15f },
		new float[] { 0.15f, 0f, 0f, 0.2f, 0.05f, 0f, 0.2f, 0.1f, 0.15f, 0.15f }
	};


}
