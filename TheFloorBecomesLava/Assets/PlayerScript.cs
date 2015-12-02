using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    public MazeGenerator MazeGenerator;

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(MazeGenerator.BlockScale, MazeGenerator.BlockScale, MazeGenerator.BlockScale);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0) ShiftPlayer(1, 0, 0);
    }

    void ShiftPlayer(float x, float y, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x + x * Time.fixedDeltaTime,
                                              transform.localPosition.y + y * Time.fixedDeltaTime,
                                              transform.localPosition.z + z * Time.fixedDeltaTime);
    }
}
