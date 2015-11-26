using UnityEngine;
using System.Collections;

public class MouseMove : MonoBehaviour {

    Vector3 _oldMousePos;
    Vector3 _velocity;
    bool _firstFrame = true;

    Quaternion _oldRotation;

    CharacterController _controller;

	// Use this for initialization
	void Start () {
        _oldRotation = transform.localRotation;
        _controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        var mousePos = Input.mousePosition;

        var delta = mousePos - _oldMousePos;
        delta /= 2000;
        delta.z = delta.y;
        delta.y = 0;

        _velocity += delta;
        
        _controller.Move(_velocity);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.LookRotation(_velocity.normalized, Vector3.up), 10);

        _oldMousePos = mousePos;
	}
}
