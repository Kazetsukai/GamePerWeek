using UnityEngine;
using System.Collections;

public class MarbleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
    void OnTriggerEnter(Collider col)
    {
        var goal = col.gameObject.GetComponent<GoalController>();
        if (goal != null)
        {
            goal.TriggerGoal();
            gameObject.SetActive(false);
        }
    }
}
