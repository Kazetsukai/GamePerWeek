using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleLight : MonoBehaviour {

    public GameObject LightBulbProto;
    public Material[] SkyBoxes;

    const float SPACING = 1.4f;

    LightBulb[][] _lights = new LightBulb[0][];

    int _level = 0;

    private float _lightSpawnFactor = 0.85f;
    private float _lightConnectFactor = 0.6f;

    // Use this for initialization
    void Start () {
        StartLevel(0);
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit rayCastInfo;

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayCastInfo))
            {
                if (rayCastInfo.collider.name.StartsWith("Light"))
                {
                    var bulb = rayCastInfo.collider.gameObject.GetComponent<LightBulb>();
                    if (bulb != null)
                    {
                        foreach (var row in _lights)
                            foreach (var light in row)
                            {
                                if (light != null)
                                {
                                    light.GetComponentInChildren<ReflectionProbe>().RenderProbe();
                                }
                            }

                        bulb.Toggle();

                        CheckConditions();

                    }
                }
            }
        }


	}

    private void CheckConditions()
    {
        foreach (var row in _lights)
            foreach (var light in row)
            {
                if (light != null)
                {
                    if (!light.IsOn)
                        return;
                }
            }

        GetComponent<AudioSource>().Play();

        StartLevel(_level + 1);
    }

    void StartLevel(int level)
    {
        _level = level;

        RenderSettings.skybox = SkyBoxes[level % SkyBoxes.Length];

        foreach (var row in _lights)
            foreach (var light in row)
            {
                if (light != null)
                {
                    Destroy(light.gameObject);
                }
            }

        foreach (var pipe in GameObject.FindGameObjectsWithTag("Pipe"))
        {
            Destroy(pipe);
        }

        int levelFactor = level + 3;

        _lights = new LightBulb[levelFactor][];

        for (int y = 0; y < levelFactor; y++)
        {
            _lights[y] = new LightBulb[levelFactor];

            for (int x = 0; x < levelFactor; x++)
            {
                var pos = new Vector3(
                    (x - (levelFactor - 1) / 2f) * SPACING,
                    0,
                    (y - (levelFactor - 1) / 2f) * SPACING
                );


                if (Random.value < _lightSpawnFactor)
                {
                    var light = ((GameObject)Instantiate(LightBulbProto, pos, Quaternion.identity)).GetComponent<LightBulb>();
                    _lights[y][x] = light;
                }

            }
        }

        for (int y = 0; y < levelFactor; y++)
        {
            for (int x = 0; x < levelFactor; x++)
            {
                if (_lights[y][x] == null || Random.value > _lightConnectFactor)
                    continue;

                if (x > 0)
                    _lights[y][x].Connect(_lights[y][x - 1]);
                if (x < levelFactor - 1)
                    _lights[y][x].Connect(_lights[y][x + 1]);
                if (y > 0)
                    _lights[y][x].Connect(_lights[y - 1][x]);
                if (y < levelFactor - 1)
                    _lights[y][x].Connect(_lights[y + 1][x]);
            }
        }



        foreach (var row in _lights)
            foreach (var light in row)
            {
                if (light != null)
                {
                    if (Random.value < 0.5f)
                        light.Toggle();
                }
            }

        foreach (var row in _lights)
            foreach (var light in row)
            {
                if (light != null)
                    light.GetComponent<AudioSource>().enabled = true;
            }
    }
}
