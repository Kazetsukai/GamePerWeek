using UnityEngine;
using System.Collections;

public class ControlScript : MonoBehaviour {
    private Vector3 _control;
    private Rigidbody _rigidbody;

    public GenerateLevel LevelGenerator;

    private Vector3 _origPosition;

    public AudioSource HitSource;
    public AudioSource DieSource;
    public AudioSource JumpSource;

    private bool _dead = false;
    private float _lastHitHeight = 0;

    private bool _jumped = false;
    public Collider JumpDetectCollider;

    private QuerySDMecanimController _controller;

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _controller = GetComponent<QuerySDMecanimController>();

        _origPosition = _rigidbody.position;
    }
	
    void FixedUpdate()
    {
        var target = _control * 10;
        target = Quaternion.FromToRotation(Vector3.forward, Camera.main.transform.forward) * target;

        var diff = target - _rigidbody.velocity;
        diff.y = 0;
        _rigidbody.AddForce(diff, ForceMode.Acceleration);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.contacts[0].thisCollider == JumpDetectCollider)
        {
            HitSource.Play();
            Debug.Log("Detect! " + Time.realtimeSinceStartup);
            _jumped = false;
            _lastHitHeight = col.collider.transform.position.y;

            if (col.collider.gameObject == LevelGenerator.FinalPlatform)
            {
                LevelGenerator.GenerateMore();
            }
        }
    }

	// Update is called once per frame
	void Update () {
        _control = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        var xyVel = _rigidbody.velocity;
        xyVel.y = 0;
        if (xyVel.magnitude > 0.5f)
        {
            if (xyVel.magnitude > 5)
                _controller.ChangeAnimation(QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_RUN);
            else
                _controller.ChangeAnimation(QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_WALK);

            var yLess = _rigidbody.velocity;
            yLess.y = 0;
            _rigidbody.MoveRotation(Quaternion.LookRotation(yLess, Vector3.up));
        }
        else
        {
            _controller.ChangeAnimation(QuerySDMecanimController.QueryChanSDAnimationType.NORMAL_IDLE);
        }

        if (Input.GetButtonDown("Fire1") && !_jumped)
        {
            JumpSource.Play();
            var target = Vector3.up * 4;
            var diff = target - _rigidbody.velocity;
            diff.x = 0;
            diff.z = 0;

            _rigidbody.AddForce(diff, ForceMode.VelocityChange);
            _jumped = true;
        }

        if (_dead && !DieSource.isPlaying)
        {
            LevelGenerator.Regenerate(LevelGenerator.InitialPlatform);

            _lastHitHeight = float.NegativeInfinity;

            _rigidbody.MovePosition(_origPosition);
            _rigidbody.velocity = Vector3.zero;
            
            _dead = false;
        }
        else if (transform.position.y < _lastHitHeight - 5 && !_dead)
        {
            DieSource.Play();
            _dead = true;
        }

	}
}
