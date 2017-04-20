using System;

namespace first_try
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Id { get; set; }
        public Edge(int @from, int to, int idEdge)
        {
            this.From = @from;
            this.To = to;
            Id = idEdge;
        }
    }
}
