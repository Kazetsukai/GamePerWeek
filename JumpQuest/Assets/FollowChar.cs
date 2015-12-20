using UnityEngine;
using System.Collections;

public class FollowChar : MonoBehaviour {

    public GameObject FollowTarget;
    private Vector3 _diff;

    // Use this for initialization
    void Start () {
        _diff = FollowTarget.transform.position - transform.position;
	}
	
    void FixedUpdate()
    {
        var target = FollowTarget.transform.position - _diff;
        var diff = target - transform.position;

        if (diff.y > 5)
            transform.position = target;
        else
            transform.position += diff / 100;
    }

	// Update is called once per frame
	void Update () {
        
	}
}
