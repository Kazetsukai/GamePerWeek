using UnityEngine;
using System.Collections;

public class MouseMove : MonoBehaviour {
    private Animator _animator;
    Rigidbody _rigidbody;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        var delta = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
        delta /= 3;
        
        // Stopping power
        //if (Vector3.Dot(delta, _rigidbody.velocity) < -0.0f)
        //    delta *= 3f;

        _rigidbody.AddForce(delta, ForceMode.VelocityChange);
        if (_rigidbody.velocity.magnitude > 0.000001f)
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.LookRotation(_rigidbody.velocity.normalized, Vector3.up), 10);

        _animator.SetFloat("Speed", _rigidbody.velocity.magnitude * 10);
        Debug.Log(_rigidbody.velocity.magnitude * 10);
	}
}
