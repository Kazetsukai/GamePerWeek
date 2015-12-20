using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GenerateLevel : MonoBehaviour {

    public GameObject ProtoPlatform;
    public GameObject InitialPlatform;
    public GameObject FinalPlatform;

    public int Score;
    public TextMesh ScoreText;

    private List<GameObject> _platforms = new List<GameObject>();

    public float Distance;
    private float _curDist;

	// Use this for initialization
	void Start () {
        var pos = InitialPlatform.transform.position;
        var color = new Color(Random.value / 4 + 0.75f, Random.value / 4 + 0.75f, Random.value / 4 + 0.75f);

        Regenerate(InitialPlatform);
	}

    public void GenerateMore()
    {
        _curDist += 0.3f;
        Regenerate(FinalPlatform, false);
    }

    public void Regenerate(GameObject initPlatform, bool delete = true)
    {
        if (delete)
        {
            _curDist = Distance;
            foreach (var platform in _platforms)
            {
                if (platform != initPlatform)
                    Destroy(platform);
            }
            _platforms.Clear();

            Score = 0;
        }
        else
        {
            foreach (var platform in _platforms)
            {
                if (platform == FinalPlatform)
                    continue;

                foreach (var comp in platform.GetComponents<SpringJoint>())
                {
                    Destroy(comp);
                }
            }

            Score++;
        }
        
        var pos = initPlatform.transform.position;
        var color = new Color(Random.value / 4 + 0.75f, Random.value / 4 + 0.75f, Random.value / 4 + 0.75f);

        for (int i = 0; i < 10; i++)
        {
            var width = Random.Range(2f, 4f);
            var length = Random.Range(2f, 4f);

            var move = Vector3.left;
            while (Mathf.Abs(move.x) + Mathf.Abs(move.y) > move.z)
            {
                move = Random.onUnitSphere;
            }
            move *= _curDist;

            pos += new Vector3(0, 0, length);
            pos += move;

            var newPlat = (GameObject)Instantiate(ProtoPlatform, pos, Quaternion.identity);
            newPlat.GetComponent<Renderer>().material.color = color;
            newPlat.transform.localScale = new Vector3(width, 1, length);
            _platforms.Add(newPlat);
        }

        ScoreText.text = "Score: " + Score;

        FinalPlatform = _platforms.Last();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
