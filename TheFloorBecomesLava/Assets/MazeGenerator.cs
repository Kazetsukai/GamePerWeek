using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using Rand = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{

    public const float BlockScale = 0.3f;

    public GameObject ProtoWall;

    public GameObject[,] _wallGrid;
    public bool[,] _inMaze;
    public List<List<Pos>> _mazeSegments;

    private int _height;
    private int _width;
    private int _abstractHeight;
    private int _abstractWidth;
    
    // Use this for initialization
    void Start()
    {
        InitialiseGrid();
    }

    private void InitialiseGrid()
    {
        _width = (int)(10 * transform.localScale.x / BlockScale);
        _height = (int)(10 * transform.localScale.z / BlockScale);
        _abstractWidth = _width / 2 + 1;
        _abstractHeight = _width / 2 + 1;

        _wallGrid = new GameObject[_width, _height];
        _inMaze = new bool[_abstractWidth, _abstractHeight];
        _mazeSegments = new List<List<Pos>>();

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _wallGrid[x, y] = (GameObject)Instantiate(ProtoWall, new Vector3(x - _width / 2, 0, y - _height / 2) * BlockScale, Quaternion.identity);
                _wallGrid[x, y].transform.localScale = new Vector3(BlockScale, BlockScale, BlockScale);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateMaze(Pos start)
    {
        _inMaze[start.X, start.Y] = true;

        var edges = new SortedList<int, Edge>();
        Func<Pos, int> value = p =>
        {
            return Rand.Range(1, (_abstractHeight + _abstractWidth) * 2)
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

            // If the position is inside the maze bounds, but not already part of the maze, then add it
            if (insideMaze(nextPos) && !_inMaze[nextPos.X, nextPos.Y])
            {

            }
        }
    }
}
