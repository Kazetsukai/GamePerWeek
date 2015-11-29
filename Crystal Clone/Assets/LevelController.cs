using UnityEngine;
using System.Collections;
using System.Linq;

public class LevelController : MonoBehaviour {

    public GameObject Iceberg;
    public GameObject Cat;
    public GameObject ProtoFish;
    public GameObject ProtoSpike;

    Vector3 _centerOfIceberg;
    float _icebergScale;
    Vector3 _spawnPosition;

    int _currentLevel = 0;

    bool _levelFinishing;
    float _levelTransitionTimeRemaining;

	// Use this for initialization
	void Start () {
        _icebergScale = Iceberg.transform.localScale.x;
        _centerOfIceberg = Iceberg.transform.position + new Vector3(0, Iceberg.transform.localScale.y / 2, 0);
        _spawnPosition = Cat.transform.position;

        StartNextLevel();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.frameCount % 1000 == 0)
            CheckVictory();

        if (_levelFinishing)
        {
            _levelTransitionTimeRemaining -= Time.fixedDeltaTime;

            if (_levelTransitionTimeRemaining < 0)
            {
                _levelFinishing = false;
                StartNextLevel();
            }
        }
	}


    private GameObject FindRoot(Transform obj)
    {
        if (obj.parent != null)
            return FindRoot(obj.parent);
        return obj.gameObject;
    }

    public void CheckVictory()
    {
        // We only want to check fish that are likely to be on the Iceberg, don't care if they have fallen off.
        var fishies = Physics.OverlapSphere(_centerOfIceberg, _icebergScale / 1.5f).Where(f => FindRoot(f.transform).name.StartsWith(ProtoFish.name));
        if (fishies.Any())
        {
            Debug.Log(fishies.Count());
            // Show remaining fish count
        }
        else
        {
            // Here we have won the level.
            _levelFinishing = true;
            _levelTransitionTimeRemaining = 1;
        }
    }

    void StartNextLevel()
    {
        _currentLevel++;
        
        var numSpikesThisLevel = _currentLevel;
        var numFishThisLevel = 3 + _currentLevel / 2;

        for (int i = 0; i < numSpikesThisLevel; i++)
        {
            SpawnObjectOnIceberg(ProtoSpike);
        }
        for (int i = 0; i < numFishThisLevel; i++)
        {
            SpawnObjectOnIceberg(ProtoFish);
        }
    }

    private void SpawnObjectOnIceberg(GameObject protoObj)
    {

        var target = GeneratePosition();
        while (InvalidPosition(target))
        {
            target = GeneratePosition();
        }

        Debug.Log(target);
        Instantiate(protoObj, target + Vector3.up * (2 + Random.value * 3), Random.rotation);
    }

    private bool InvalidPosition(Vector3 target)
    {
        // Can't be too close to player
        return (Cat.transform.position - target).magnitude < 1;
        // ...or the spawn.
        return (_spawnPosition - target).magnitude < 1;
    }

    private Vector3 GeneratePosition()
    {
        var target = Random.insideUnitCircle * (_icebergScale / 2.3f);

        return new Vector3(target.x, 0, target.y) + _centerOfIceberg;
    }
}
