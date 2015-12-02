using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using Rand = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{

    public const float BlockScale = 0.2f;

    public GameObject ProtoWall;
    public GameObject Player;

    public GameObject[,] _wallGrid;
    public bool[,] _inMaze;
    public List<List<Edge>> _mazeSegments;

    private int _height;
    private int _width;
    private int _abstractHeight;
    private int _abstractWidth;
    
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

        _wallGrid = new GameObject[_width, _height];
        _inMaze = new bool[_abstractWidth, _abstractHeight];
        _mazeSegments = new List<List<Edge>>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _wallGrid[x, y] = (GameObject)Instantiate(ProtoWall, WallCoordToReal(x, y), Quaternion.identity);
                _wallGrid[x, y].transform.localScale = new Vector3(BlockScale, BlockScale, BlockScale);
                _wallGrid[x, y].SendMessage("Rise");
            }
        }
    }

    public Vector3 WallCoordToReal(int x, int y)
    {
        return new Vector3(x - _width / 2, 0, y - _height / 2) * BlockScale;
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
        Func<Pos, bool> insideMaze = p =>
        {
            return p.X >= 0 && p.X < _abstractWidth &&
                   p.Y >= 0 && p.Y < _abstractHeight;
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
                var from = new Pos(nextEdge.From);
                from.X *= 2;
                from.Y *= 2;
                from.X += 1;
                from.Y += 1;
                var to = new Pos(nextEdge.To);
                to.X *= 2;
                to.Y *= 2;
                to.X += 1;
                to.Y += 1;
                var mid = new Pos() { X = (from.X + to.X) / 2, Y = (from.Y + to.Y) / 2 };

                Debug.Log(from + " - " + mid + " - " + to);
                _wallGrid[from.X, from.Y].SendMessage("Drop");
                _wallGrid[mid.X, mid.Y].SendMessage("Drop");
                _wallGrid[to.X, to.Y].SendMessage("Drop");
            }
        }
    }
}
