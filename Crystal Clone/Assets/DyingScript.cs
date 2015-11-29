using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DyingScript : MonoBehaviour, ITriggerExit, ITriggerEnter
{
    private bool _dying;
    private float _timeToDeath;
    private IEnumerable<Renderer> _renderers;

    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    public void OnTriggerEnter(Collider other)
    {
        if (!_dying)
        {
            if (other.gameObject.name.StartsWith("SpikeBall"))
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<MouseMove>().enabled = false;


                // Meow
                Die();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Iceberg")
        {
            GetComponent<MouseMove>().enabled = false;

            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.useGravity = true;
        }
        else if (other.gameObject.name == "Water")
        {
            // Splash
            Die();
        }
    }

    // Use this for initialization
    void Start () {
        _renderers = GetComponentsInChildren<Renderer>();
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (_dying)
        {
            _timeToDeath -= Time.fixedDeltaTime;

            var visible = (int)(_timeToDeath * 4) % 2 == 1 && _timeToDeath > 0;
            foreach (var renderer in _renderers)
                renderer.enabled = visible;

            if (_timeToDeath < -1)
            {
                Respawn();
            }
        }
	}

    void Die()
    {
        _dying = true;
        _timeToDeath = 2;
        GetComponent<Animator>().speed = 0;
    }

    void Respawn()
    {
        _dying = false;

        GetComponent<Animator>().speed = 1;
        GetComponent<Animator>().Play("Stop");

        transform.localPosition = _initialPosition;
        transform.localRotation = _initialRotation;

        var rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.useGravity = false;

        GetComponent<MouseMove>().enabled = true;

        foreach (var renderer in _renderers)
            renderer.enabled = true;
    }
}
