using UnityEngine;
using System.Collections;
using System;

public class EatFish : MonoBehaviour, ITriggerEnter {

    public LevelController LevelController;
    private SoundPlayer _soundPlayer;

    public void OnTriggerEnter(Collider other)
    {
        var obj = FindRoot(other.gameObject.transform);

        if (obj.name.StartsWith("cruscarp"))
        {
            // Eat fish
            foreach (var col in obj.GetComponentsInChildren<Collider>())
                col.enabled = false;

            Destroy(obj);

            LevelController.CheckVictory();

            _soundPlayer.PlayNom();
        }
    }

    private GameObject FindRoot(Transform obj)
    {
        if (obj.parent != null)
            return FindRoot(obj.parent);
        return obj.gameObject;
    }

    // Use this for initialization
    void Start () {
        _soundPlayer = FindObjectOfType<SoundPlayer>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
