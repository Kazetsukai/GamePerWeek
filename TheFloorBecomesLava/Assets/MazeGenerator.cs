using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour {

    public GameObject ProtoWall;

    public GameObject[,] _wallGrid;

    // Use this for initialization
    void Start() {
        
        var width = (int)(10 * transform.localScale.x);
        var height = (int)(10 * transform.localScale.z);

        _wallGrid = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _wallGrid[x, y] = (GameObject)Instantiate(ProtoWall, new Vector3(x - width / 2 + 0.5f, 0, y - height / 2 + 0.5f), Quaternion.identity);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
