public class Edge
{
    public Pos From;
    public Pos To;
    public float Distance;

    public Edge(Pos from, Pos to, float distance)
    {
        this.From = from;
        this.To = to;
        this.Distance = distance;
    }
}