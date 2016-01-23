using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleLight : MonoBehaviour {

    public GameObject LightBulbProto;

    const float SPACING = 1.4f;

    List<LightBulb> _lights = new List<LightBulb>();

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
                        bulb.Toggle();

                        foreach (var light in _lights)
                        {
                            light.GetComponentInChildren<ReflectionProbe>().RenderProbe();
                        }
                    }
                }
            }
        }


	}

    void StartLevel(int level)
    {
        foreach (var light in _lights)
        {
            Destroy(light.gameObject);
        }
        _lights.Clear();

        int levelFactor = level + 3;

        for (int x = 0; x < levelFactor; x++)
        {
            for (int y = 0; y < levelFactor; y++)
            {
                var pos = new Vector3(
                    (x - (levelFactor-1) / 2f) * SPACING, 
                    0, 
                    (y - (levelFactor-1) / 2f) * SPACING
                );

                var light = ((GameObject)Instantiate(LightBulbProto, pos, Quaternion.identity)).GetComponent<LightBulb>();
                _lights.Add(light);
            }
        }

        Debug.Log(_lights.Count + " lights");
    }
}
