using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace GraphSolver
{
    public class Path
    {
        public List<Edge> OnePath;

        private bool CheckOneEdge(int i)
        {
            if (i == 0)
                return true;
            else
                return OnePath[i - 1].To == OnePath[i].From;
        }

        private bool CheckAddedEdge(Edge edge)
        {
            if (OnePath == null)
                return true;
            return OnePath.Last().To == edge.From;
        }
        private bool IsItPath()
        {
            var result = true;
            if (OnePath == null)
                return true;
            for (var i = 0; i < OnePath.Count; ++i)
                result &= CheckOneEdge(i);
            return result;
        }

        public Path(List<Edge> path)
        {
            OnePath = new List<Edge>(path);
            if(!IsItPath())
                OnePath = new List<Edge>();
        }

        public Path()
        {
            OnePath = new List<Edge>();
        }

        public void AddEdge(Edge edge)
        {
            if (OnePath == null)
            {
                OnePath = new List<Edge>{edge};
                return;
            }
            if (CheckAddedEdge(edge))
                OnePath.Add(edge);
        }
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
