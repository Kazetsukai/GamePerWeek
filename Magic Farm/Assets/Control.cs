using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
    private CharacterController _controller;

    public GameObject PlantProto;

    // Use this for initialization
    void Start () {
        _controller = GetComponent<CharacterController>();
	}

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        if (_controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(PlantProto, transform.localPosition, Quaternion.identity);
        }
    }
}
