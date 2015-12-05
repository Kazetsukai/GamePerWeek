using UnityEngine;
using System.Collections;
using System;

public class PlayerScript : MonoBehaviour
{

    public MazeGenerator MazeGenerator;
    public Pos TargetLocation = new Pos(1, 1, 0);
    public Pos Location = new Pos(1, 1, 0);
    public float MoveSpeed;

    float _moveProgress = 0;
    bool _moving = true;

    // Use this for initialization
    void Start()
    {
        transform.localScale = new Vector3(MazeGenerator.BlockScale * 0.8f, MazeGenerator.BlockScale * 0.8f, MazeGenerator.BlockScale * 0.8f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (_moving)
        {
            _moveProgress += Time.fixedDeltaTime * MoveSpeed;

            var moveAmount = Mathf.Clamp(_moveProgress, 0, 1);
            var to = MazeGenerator.WallCoordToReal(TargetLocation) + Vector3.up * MazeGenerator.BlockScale / 2;
            var from = MazeGenerator.WallCoordToReal(Location) + Vector3.up * MazeGenerator.BlockScale / 2;

            if (_moveProgress >= 1)
            {
                _moving = false;
                _moveProgress -= 1;
                Location = TargetLocation;
                MazeGenerator.CheckGoal(Location);
            }
            transform.localPosition = (to * (moveAmount) + from * (1 - moveAmount));
        }

        if (!_moving)
        {
            if (Input.GetAxis("Horizontal") > 0) { Move(Location.Right.To); }
            if (Input.GetAxis("Horizontal") < 0) { Move(Location.Left.To); }
            if (Input.GetAxis("Vertical") > 0) { Move(Location.Up.To); }
            if (Input.GetAxis("Vertical") < 0) { Move(Location.Down.To); }
        }
    }

    private void Move(Pos to)
    {
        if (MazeGenerator.IsCorridor(to))
        {
            TargetLocation = to;
            _moving = true;
        }
    }
}
