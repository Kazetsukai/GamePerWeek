using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour {
    private bool _triggered;
    private float _amount = 0;


    // Use this for initialization
    void Start () {
	
	}

    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, (Time.timeSinceLevelLoad * 100) % 360, 0);

        if (_triggered)
        {
            _amount += Time.deltaTime;
            transform.localScale = new Vector3(1 + _amount, 1 + _amount, 1 + _amount);
            if (_amount > 1)
                gameObject.SetActive(false);
        }
    }

    public void TriggerGoal()
    {
        GetComponent<Collider>().enabled = false;
        _triggered = true;
    }

    public void Reset()
    {
        _triggered = false;
        GetComponent<Collider>().enabled = true;
        transform.localScale = new Vector3(1, 1, 1);
        _amount = 0;
        gameObject.SetActive(true);
    }
}
