public class Pos
{
    public int X { get; internal set; }
    public int Y { get; internal set; }

    public Edge Left {  get { return new Edge(this, new Pos() { X = X - 1, Y = Y }); } }
    public Edge Right { get { return new Edge(this, new Pos() { X = X + 1, Y = Y }); } }
    public Edge Up { get { return new Edge(this, new Pos() { X = X, Y = Y + 1 }); } }
    public Edge Down { get { return new Edge(this, new Pos() { X = X, Y = Y - 1 }); } }

    public Pos()
    {
    }

    public Pos(Pos other)
    {
        X = other.X;
        Y = other.Y;
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

    public override string ToString()
    {
        return string.Format("[{0}, {1}]", X, Y);
    }
}