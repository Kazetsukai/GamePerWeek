using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelControl : MonoBehaviour {

    public GameObject ProtoWallSeg;
    public GameObject ProtoWallCap;

    public PaddleController PaddleLeft;
    public PaddleController PaddleRight;
    public BallBouncing Ball;

    public Text LeftScore;
    public Text RightScore;
    int _leftScore = 0;
    int _rightScore = 0;

    public float LevelWidth;
    public float LevelHeight;

    private float PaddleWidth = 6.40f * 0.3f;

    // Use this for initialization
    void Start ()
    {
        // Setup walls
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

        // Setup balls
        Ball.Speed = 3;
        Ball.GetComponent<Rigidbody2D>().AddForce(Vector2.left, ForceMode2D.Impulse);

        // Setup paddles
        PaddleLeft.CenterLocation = new Vector3(-(LevelWidth+PaddleWidth) / 2, 0, 0);
        PaddleLeft.Radius = LevelHeight / 2;
        PaddleLeft.FacingRight = true;

        PaddleRight.CenterLocation = new Vector3((LevelWidth+PaddleWidth) / 2, 0, 0);
        PaddleRight.Radius = LevelHeight / 2;
        PaddleRight.FacingRight = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Ball.IsAlive)
        {
            var pos = Ball.transform.localPosition;

            if ((pos - PaddleLeft.CenterLocation).magnitude > PaddleLeft.Radius * 1.2f &&
                (pos - PaddleRight.CenterLocation).magnitude > PaddleRight.Radius * 1.2f &&
                (pos.x < -(LevelWidth + PaddleWidth * 1.1f) / 2 || pos.x > (LevelWidth + PaddleWidth * 1.1f) / 2) || (pos.y < -LevelHeight * 0.7f) || (pos.y > LevelHeight * 0.7f))
            {
                if (pos.x < 0)
                    _rightScore++;
                if (pos.x > 0)
                    _leftScore++;

                StartCoroutine(Ball.DieAndRespawnWithSpeed(3 + 0.1f * (_leftScore + _rightScore)).GetEnumerator());

                LeftScore.text = _leftScore.ToString();
                RightScore.text = _rightScore.ToString();
            }
        }
    }
}
