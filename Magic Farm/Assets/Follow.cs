using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public GameObject Target;

    private Vector3 _offset;

	// Use this for initialization
	void Start () {
        _offset = Target.transform.localPosition - transform.localPosition;
	}
	
	void LateUpdate () {
        transform.localPosition = Target.transform.localPosition - _offset;
	}
}
