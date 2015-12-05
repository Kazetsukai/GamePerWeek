using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using Rand = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{

    public const float BlockScale = 0.2f;
    public static readonly Vector3 ScaleVector = new Vector3(BlockScale, BlockScale, BlockScale);

    public GameObject ProtoWall;
    public GameObject Player;
    public GameObject ProtoGoal;
    
    public WallBlock[,] _wallGrid;
    public bool[,] _isCorridor;
    public bool[,] _inMaze;
    public List<List<Edge>> _mazeSegments;

    private int _height;
    private int _width;
    private int _abstractHeight;
    private int _abstractWidth;
    private Pos _goalPos;
    private GameObject _goal;

    Func<Pos, bool> insideMaze;

    // Use this for initialization
    void Start()
    {
        InitialiseGrid();

        GenerateMaze(new Pos() { X = 0, Y = 0 });
        Player.transform.localPosition = WallCoordToReal(1, 1);
    }

    private void InitialiseGrid()
    {
        _width = (int)(10 * transform.localScale.x / BlockScale);
        _height = (int)(10 * transform.localScale.z / BlockScale);
        _abstractWidth = _width / 2;
        _abstractHeight = _width / 2;

        _goal = Instantiate(ProtoGoal);
        _goal.transform.localScale = ScaleVector;

        insideMaze = p =>
        {
            return p.X >= 0 && p.X < _abstractWidth &&
                   p.Y >= 0 && p.Y < _abstractHeight;
        };

        _wallGrid = new WallBlock[_width, _height];
        _inMaze = new bool[_abstractWidth, _abstractHeight];
        _mazeSegments = new List<List<Edge>>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _wallGrid[x, y] = ((GameObject)Instantiate(ProtoWall, WallCoordToReal(x, y), Quaternion.identity)).GetComponent<WallBlock>();
                _wallGrid[x, y].transform.localScale = ScaleVector;
                _wallGrid[x, y].Rise();
            }
        }
    }

    public Vector3 WallCoordToReal(int x, int y)
    {
        return new Vector3(x - _width / 2, 0, y - _height / 2) * BlockScale;
    }

    public Vector3 WallCoordToReal(Pos pos)
    {
        return WallCoordToReal(pos.X, pos.Y);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateMaze(Pos start)
    {
        var mazeSegment = new List<Edge>();

        _inMaze[start.X, start.Y] = true;

        var edges = new SortedList<float, Edge>();
        Func<Pos, float> value = p =>
        {
            return Rand.Range(1f, 20f)
                    + Math.Abs(start.X - p.X)
                    + Math.Abs(start.Y - p.Y);
        };


        edges.Add(value(start), start.Left);
        edges.Add(value(start), start.Right);
        edges.Add(value(start), start.Up);
        edges.Add(value(start), start.Down);

        while (edges.Count > 0)
        {
            var nextEdge = edges.Values[0];
            var nextPos = nextEdge.To;
            edges.RemoveAt(0);

            // If the position is inside the maze bounds, but not already part of the maze, then add it
            if (insideMaze(nextPos) && !_inMaze[nextPos.X, nextPos.Y] && value(nextPos) < 20)
            {
                _inMaze[nextPos.X, nextPos.Y] = true;
                mazeSegment.Add(nextEdge);

                try
                {
                    edges.Add(value(nextPos), nextPos.Left);
                    edges.Add(value(nextPos), nextPos.Right);
                    edges.Add(value(nextPos), nextPos.Down);
                    edges.Add(value(nextPos), nextPos.Up);
                }
                catch { }

                // Lower appropriate blocks
                var from = AbstractToWall(nextEdge.From);
                var to = AbstractToWall(nextEdge.To);
                var mid = new Pos() { X = (from.X + to.X) / 2, Y = (from.Y + to.Y) / 2 };
                
                _wallGrid[from.X, from.Y].Drop();
                _wallGrid[mid.X, mid.Y].Drop();
                _wallGrid[to.X, to.Y].Drop();
            }
        }

        var maxDist = 0.0;
        var max = start;
        foreach (var m in mazeSegment)
        {
            var dist = Math.Sqrt(Math.Pow(start.X - m.To.X, 2) + Math.Pow(start.Y - m.To.Y, 2));
            if (dist > maxDist)
            {
                max = m.To;
                maxDist = dist;
            }
        }

        _goalPos = AbstractToWall(max);
        _goal.transform.position = WallCoordToReal(_goalPos) + (Vector3.up * BlockScale / 2);

        _mazeSegments.Add(mazeSegment);
    }

    private Pos AbstractToWall(Pos pos)
    {
        return new Pos(pos.X * 2 + 1, pos.Y * 2 + 1);
    }

    public bool IsCorridor(Pos pos)
    {
        return pos.X > 0 && 
               pos.Y > 0 &&
               pos.X < _wallGrid.GetLength(0) && 
               pos.Y < _wallGrid.GetLength(1) &&
               !_wallGrid[pos.X, pos.Y].IsUp;
    }

    public void CheckGoal(Pos pos)
    {
        if (pos.Equals(_goalPos))
        {
            Debug.Log("Woop woop!");
        }
    }
}
