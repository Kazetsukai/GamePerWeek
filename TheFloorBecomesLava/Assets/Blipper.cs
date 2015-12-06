using UnityEngine;
using System.Collections;

public class Blipper : MonoBehaviour {

    float _count = 0;
    AudioSource _source;

	// Use this for initialization
	void Start () {
        _source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        _count += Time.fixedDeltaTime * MazeGenerator.LavaSpeed;

        if (_count > 1)
        {
            if (_source.enabled)
            {
                _count -= 1;
                _source.Play();
            }
        }
    }
}
