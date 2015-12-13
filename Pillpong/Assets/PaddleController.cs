using UnityEngine;
using System.Collections;

public class PaddleController : MonoBehaviour {

    public Vector3 CenterLocation = new Vector3(2, 2, 0);
    public float Radius = 2;
    public bool FacingRight;

    private Vector3 CurrentDirection;
    private Vector3 _controlDirection;

    // Use this for initialization
    void Start () {
        CurrentDirection = FacingRight ? Vector3.left : Vector3.right;
	}

    void FixedUpdate()
    {

        if (FacingRight && _controlDirection.x > -0.01f)
            _controlDirection.x = -0.01f;
        if (!FacingRight && _controlDirection.x < 0.01f)
            _controlDirection.x = 0.01f;

        var rotateAmount = _controlDirection.magnitude * 10;
        
        if (rotateAmount > 3f)
        {
            CurrentDirection = Vector3.RotateTowards(CurrentDirection, _controlDirection, rotateAmount * Time.fixedDeltaTime, 1);
        }

        if (FacingRight && CurrentDirection.x > -0.01f)
            CurrentDirection.x = -0.01f;
        if (!FacingRight && CurrentDirection.x < 0.01f)
            CurrentDirection.x = 0.01f;

        CurrentDirection.z = 0;
        CurrentDirection = CurrentDirection.normalized;

        transform.localPosition = CurrentDirection * Radius + CenterLocation;


        transform.localRotation = Quaternion.LookRotation(Vector3.forward, new Vector3(CurrentDirection.y, -CurrentDirection.x, 0));
    }

	// Update is called once per frame
	void Update () {

        _controlDirection = FacingRight
            ? new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))
            : new Vector3(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));
    }
}
