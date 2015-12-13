using UnityEngine;
using System.Collections;

public class BallBouncing : MonoBehaviour {

    public float Speed;
    private Rigidbody2D _rigidBody;

    public AudioClip WallHit;
    public AudioClip PaddleHit;
    public AudioClip BallDie;

    AudioSource _audio;

    // Use this for initialization
    void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.AddForce(Vector2.left, ForceMode2D.Force);

        _audio = GetComponent<AudioSource>();
	}
	
    void FixedUpdate()
    {
    }

	// Update is called once per frame
	void Update () {

        var mag = Speed / 2 - Mathf.Abs(_rigidBody.velocity.x);
        if (mag > 0)
        {
            _rigidBody.AddForce(_rigidBody.velocity.x < 0 ? Vector2.left * mag : Vector2.right * mag, ForceMode2D.Impulse);
        }

        var currentSpeed = _rigidBody.velocity.magnitude;
        if (currentSpeed != Speed && currentSpeed > 0)
        {
            _rigidBody.AddForce(_rigidBody.velocity.normalized * Speed - _rigidBody.velocity, ForceMode2D.Impulse);
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("paddle"))
            _audio.PlayOneShot(PaddleHit);
        if (col.gameObject.name.Contains("wall"))
            _audio.PlayOneShot(WallHit);

    }
}
