namespace GraphSolver
{
    class VertexInQueue
    {
        public int IdVert;
        public int Turn;
        public VertexInQueue(int idVertex, int lengthFromStartToThis)
        {
            IdVert = idVertex;
            Turn = lengthFromStartToThis;
        }
    }
}
