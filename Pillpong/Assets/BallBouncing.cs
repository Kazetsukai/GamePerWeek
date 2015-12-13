using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BallBouncing : MonoBehaviour {

    public float Speed;
    private Rigidbody2D _rigidBody;

    public AudioClip WallHit;
    public AudioClip PaddleHit;
    public AudioClip BallDie;

    AudioSource _audio;
    Renderer _renderer;

    public bool IsAlive { get; private set; }

    // Use this for initialization
    void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.AddForce(Vector2.left, ForceMode2D.Force);

        _audio = GetComponent<AudioSource>();
        _renderer = GetComponent<Renderer>();

        IsAlive = true;
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
        {
            _audio.PlayOneShot(PaddleHit);
            Speed += 0.2f;
        }

        if (col.gameObject.name.Contains("wall"))
            _audio.PlayOneShot(WallHit);

    }

    public IEnumerable DieAndRespawnWithSpeed(float speed)
    {
        IsAlive = false;
        _audio.PlayOneShot(BallDie);

        for (int i = 0; i < 2; i++)
        {
            _renderer.enabled = false;
            yield return new WaitForSeconds(0.1f + 0.01f * i);
            _renderer.enabled = true;
            yield return new WaitForSeconds(0.1f + 0.01f * i);
        }
        
        _renderer.enabled = false;
        Speed = 0;
        _rigidBody.MovePosition(Vector2.zero);

        yield return new WaitForSeconds(2);

        
        for (int i = 0; i < 4; i++)
        {
            _renderer.enabled = false;
            yield return new WaitForSeconds(0.1f + 0.01f * i);
            _renderer.enabled = true;
            yield return new WaitForSeconds(0.1f + 0.01f * i);
        }
        
        Speed = speed;
        IsAlive = true;
        _audio.PlayOneShot(PaddleHit);

        _rigidBody.velocity = Random.insideUnitCircle;
        while (_rigidBody.velocity.magnitude < 0.1f)
            _rigidBody.velocity = Random.insideUnitCircle;

    }
}
