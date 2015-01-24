using UnityEngine;
using System.Collections;

public class InputTest : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(string.Format("{0}, {1}, {2}, {3}", Input.GetAxis("Pad 1 Grab"), Input.GetAxis("Pad 2 Grab"), Input.GetAxis("Pad 1 Stick Hori"), Input.GetAxis("Pad 1 Stick Vert")));
        //Debug.Log(string.Format("{0}, {1}", Input.GetAxis("Pad 1 Grab"), Input.GetAxis("Pad 2 Grab")));
	}
}
