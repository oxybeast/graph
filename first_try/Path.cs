using System.Collections.Generic;

namespace GraphSolver
{
    public class Path
    {
        public List<Edge> OnePath;

        public Path(List<Edge> path) => OnePath = new List<Edge>(path);
        public void ReadPathFromFile(string s)
        {
            OnePath = ReadSomething.ReadListEdges(s);
        }
    }
    
}
