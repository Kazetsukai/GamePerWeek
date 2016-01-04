using UnityEngine;
using System.Collections;

public class PlantGrow : MonoBehaviour {

    public Sprite[] Sprites;

    private float _currentGrowthFactor = 0.02f + Random.Range(0.0f, 0.05f);
    private float _growth = 0;
    private int _growthStage = 1;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        _growth += Time.deltaTime * _currentGrowthFactor;

        if (_growth > _growthStage && _growthStage < Sprites.Length)
        {
            _growthStage++;
            GetComponent<SpriteRenderer>().sprite = Sprites[_growthStage - 1];
        }
	}
}
