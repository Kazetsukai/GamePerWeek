using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {
    private AudioSource _audioSource;

    public AudioClip Die;
    public AudioClip Splash;
    public AudioClip LevelUp;
    public AudioClip Nom;

    // Use this for initialization
    void Start () {
        _audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void PlayDie()
    {
        _audioSource.PlayOneShot(Die);
    }

    public void PlaySplash()
    {
        _audioSource.PlayOneShot(Splash);
    }

    public void PlayLevelUp()
    {
        _audioSource.PlayOneShot(LevelUp);
    }

    public void PlayNom()
    {
        _audioSource.PlayOneShot(Nom);
    }
}
