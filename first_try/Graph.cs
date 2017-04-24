using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphSolver
{
    public class Graph
    {
        private List<Edge>[] _graph;

        public Graph()
        {
            _graph = new List<Edge>[0];
        }
        public Graph(string inputFile)
        {
            ReadGraph(inputFile);
        }

        public Graph(List<Edge>[] graphInListEdges)
        {
            _graph = graphInListEdges;
            for (var i = 0; i < _graph.Count(); ++i)
            {
                if(_graph[i] == null)
                    _graph[i] = new List<Edge>();
            }
        }

        public int Count
        {
            get { return _graph.Length; }
        }

        public IEnumerable<Edge> GetEdgesByVertexId(int id)
        {
            return _graph[id];
        }

        public void ReadGraph(string inputGraph)
        {
            using (var input = new StreamReader(@inputGraph))
            {
                var buffer = input.ReadLine().Split(' ');

                var countOfVertex = Convert.ToInt32(buffer[0]);
                var countOfEdges = Convert.ToInt32(buffer[1]);

                _graph = new List<Edge>[countOfVertex];
                for (var i = 0; i < countOfVertex; ++i)
                    _graph[i] = new List<Edge>();
                for (var i = 0; i < countOfEdges; ++i)
                {
                    buffer = input.ReadLine().Split(' ');
                    var @from = Convert.ToInt32(buffer[0]);
                    var to = Convert.ToInt32(buffer[1]);
                    var id = Convert.ToInt32(buffer[2]);
                    var toAdd = new Edge(from, to, id);
                    _graph[from].Add(toAdd);
                }
            }
        }
    }
    
}
