using UnityEngine;
using System.Collections;
using System;

public class DyingScript : MonoBehaviour, ITriggerExit
{
    private bool _falling;

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Iceberg")
        {
            _falling = true;
            GetComponent<MouseMove>().enabled = false;

            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.useGravity = true;
        }
        else if (other.gameObject.name == "Water")
        {
            // Splash
        }
    }

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	}
}
