namespace first_try
{
    class VertexInQueue
    {
        public int idVert;
        public int turn;
        public VertexInQueue(int idVertex, int lengthFromStartToThis)
        {
            idVert = idVertex;
            turn = lengthFromStartToThis;
        }
    }
}
