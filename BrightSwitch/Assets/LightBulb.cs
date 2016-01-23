using UnityEngine;
using System.Collections;

public class LightBulb : MonoBehaviour {

    bool _isOn = true;

	// Use this for initialization
	void Start () {
        Toggle();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Toggle()
    {
        _isOn = !_isOn;

        Color lightColor = _isOn ? new Color(1, 1, 1, 0.6f) : new Color(0.5f, 0.5f, 0.5f, 0.6f);
        GetComponent<Renderer>().material.color = lightColor;
        GetComponentInChildren<Light>().enabled = _isOn;
    }
}
