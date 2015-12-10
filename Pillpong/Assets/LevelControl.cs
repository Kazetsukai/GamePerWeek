using UnityEngine;
using System.Collections;

public class LevelControl : MonoBehaviour {

    public GameObject ProtoWallSeg;
    public GameObject ProtoWallCap;

    public float LevelWidth;
    public float LevelHeight;

	// Use this for initialization
	void Start ()
    {
        var topWall = Instantiate(ProtoWallSeg);
        var botWall = Instantiate(ProtoWallSeg);

        var topLeftCap = Instantiate(ProtoWallCap);
        var botLeftCap = Instantiate(ProtoWallCap);
        var topRightCap = Instantiate(ProtoWallCap);
        var botRightCap = Instantiate(ProtoWallCap);

        topWall.transform.localScale = new Vector3(LevelWidth / 0.09f, 0.3f, 0.3f);
        topWall.transform.localPosition = new Vector3(0, LevelHeight / 2, 0);
        topLeftCap.transform.localPosition = new Vector3(-LevelWidth / 2, LevelHeight / 2, 0);
        topRightCap.transform.localPosition = new Vector3(LevelWidth / 2, LevelHeight / 2, 0);
        topRightCap.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);

        botWall.transform.localScale = new Vector3(LevelWidth / 0.09f, 0.3f, 0.3f);
        botWall.transform.localPosition = new Vector3(0, -LevelHeight / 2, 0);
        botLeftCap.transform.localPosition = new Vector3(-LevelWidth / 2, -LevelHeight / 2, 0);
        botRightCap.transform.localPosition = new Vector3(LevelWidth / 2, -LevelHeight / 2, 0);
        botRightCap.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
