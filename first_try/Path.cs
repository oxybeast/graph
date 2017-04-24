using System.CodeDom;
using System.Collections.Generic;
using System.Xml.Schema;

namespace GraphSolver
{
    public class Path
    {
        public List<Edge> OnePath;

        public Path(List<Edge> path) => OnePath = new List<Edge>(path);

        public int Length()
        {
            return OnePath.Count;
        }
        public void ReadPathFromFile(string s)
        {
            OnePath = ReadSomething.ReadListEdges(s);
        }
    }
    
}
