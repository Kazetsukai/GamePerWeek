using System;
using UnityEngine;

public class Pos
{
    public int X { get; internal set; }
    public int Y { get; internal set; }
    public float Distance { get; internal set; }

    public Edge Left { get { return new Edge(this, new Pos(X-1, Y, Distance + 1), Distance + 1); } }
    public Edge Right { get { return new Edge(this, new Pos(X+1, Y, Distance + 1), Distance + 1); } }
    public Edge Up { get { return new Edge(this, new Pos(X, Y+1, Distance + 1), Distance + 1); } }
    public Edge Down { get { return new Edge(this, new Pos(X, Y-1, Distance + 1), Distance + 1); } }

    public Pos()
    {
    }

    public Pos(int x, int y, float distance)
    {
        X = x;
        Y = y;
        Distance = distance;
    }

    public Pos(Pos other)
    {
        X = other.X;
        Y = other.Y;
        Distance = other.Distance;
    }

    public override bool Equals(object obj)
    {
        if (obj is Pos)
        {
            var other = obj as Pos;
            return other.X == X && other.Y == Y;
        }

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }

    public override string ToString()
    {
        return string.Format("[{0}, {1}]", X, Y);
    }
}