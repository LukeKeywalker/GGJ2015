using UnityEngine;
using System.Collections;
using System;

public class BasePickupItem : MonoBehaviour 
{
	public int scoreValue;

	public Action<Transform> onPickedUp;
	
}
