using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var mat = GetComponent<Renderer>().material.name.Split()[0];

        Debug.Log(mat + " " + LayerMask.NameToLayer(mat));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
