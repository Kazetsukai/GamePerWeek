using UnityEngine;
using System.Collections;

public class CameraStart : MonoBehaviour {

    Quaternion _initialRotation;
    Vector3 _initialPosition;

    Quaternion _subjectLookRotation;
    Vector3 _subjectLookPosition;

    float _lerpFactor = 0;

    MonoBehaviour _controllerScript;

    public GameObject Subject;
    public float Delay = 3;

    // Use this for initialization
    void Start () {
        _initialRotation = transform.localRotation;
        _initialPosition = transform.localPosition;

        _subjectLookPosition = Subject.transform.position + Subject.transform.forward * 1 + Subject.transform.up * 0.5f;
        _subjectLookRotation = Quaternion.LookRotation((Subject.transform.position - _subjectLookPosition).normalized, Vector3.up);

        transform.localPosition = _subjectLookPosition;
        transform.localRotation = _subjectLookRotation;

        _controllerScript = Subject.GetComponent<MouseMove>();
        _controllerScript.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (Delay > 0)
        {
            Delay -= Time.fixedDeltaTime;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(_subjectLookPosition, _initialPosition, _lerpFactor);
            transform.localRotation = Quaternion.Lerp(_subjectLookRotation, _initialRotation, _lerpFactor);

            if (_lerpFactor < 1) _lerpFactor += Time.fixedDeltaTime;
            else
            {
                _lerpFactor = 1;

                _controllerScript.enabled = true;
                this.enabled = false;
            }
        }
    }
}
