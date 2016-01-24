using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightBulb : MonoBehaviour {

    bool _isOn = true;
    public bool IsOn {  get { return _isOn; } }

    public GameObject ProtoPipe;
    public AudioClip OnSound;
    public AudioClip OffSound;


    List<LightBulb> _bulbs = new List<LightBulb>();

	// Use this for initialization
	void Start () {
        Toggle(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Toggle(bool boop = true)
    {
        _isOn = !_isOn;

        Color lightColor = _isOn ? new Color(1, 1, 1, 0.6f) : new Color(0.5f, 0.5f, 0.5f, 0.6f);
        GetComponent<Renderer>().material.color = lightColor;
        GetComponentInChildren<Light>().enabled = _isOn;

        if (boop)
        {
            GetComponent<AudioSource>().PlayOneShot(_isOn ? OnSound : OffSound);

            foreach (var bulb in _bulbs)
            {
                bulb.Toggle(false);
            }
        }
    }

    public void Connect(LightBulb other, bool pipe = true)
    {
        if (other != null && !_bulbs.Contains(other))
        {
            _bulbs.Add(other);
            other.Connect(this, false);
            if (pipe)
                Instantiate(ProtoPipe, (transform.position + other.transform.position) / 2, Quaternion.LookRotation((transform.position - other.transform.position), Vector3.up));
        }
    }
}
