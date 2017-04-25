using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace GraphSolver
{
    public class Graph
    {
        private List<List<Edge>> _graph;
        private Dictionary<int, int> _idDictionary;
        private int _countVertex;

        public Graph()
        {
            _graph = new List<List<Edge>>();
            _idDictionary.Clear();
            _countVertex = 0;
        }

        public void AddEdge(Edge edge)
        {
            var max = Math.Max(edge.From, edge.To);
            var min = Math.Min(edge.From, edge.To);

            if (min < 0)
                return;

            if(max >= _countVertex)
                while (max >= _countVertex)
                {
                    _countVertex++;
                    _graph.Add(new List<Edge>());
                }
            _graph[edge.From].Add(edge);
            return;
        }

        public Graph(string inputFile)
        {
            ReadGraph(inputFile);
        }

        public Graph(List<List<Edge>> graph)
        {
            _graph = new List<List<Edge>>();
            _countVertex = 0;
            for (var i = 0; i < graph.Count; ++i)
            {
                _graph[i] = new List<Edge>(graph[i]);
                _countVertex++;
            }
        }
        public Graph(List<Edge>[] graphInListEdges)
        {
            _countVertex = graphInListEdges.Length;
            _graph = new List<List<Edge>>();
            for (var i = 0; i < _countVertex; ++i)
            {
                _graph.Add(new List<Edge>());
            }
            for (var i = 0; i < graphInListEdges.Length;++i)
                    {
                        if(graphInListEdges[i] != null)
                            _graph[i] = new List<Edge>(graphInListEdges[i]);
                        else
                            _graph[i] = new List<Edge>();
                        
                    }
        }

        public int Count
        {
            get { return _countVertex; }
        }

        public IEnumerable<Edge> GetEdgesByVertexId(int id)
        {
            if(id >= 0 && id < _countVertex)
                return _graph[id];
            else
                return new List<Edge>();
        }

        public void ReadGraph(string inputGraph)
        {
            using (var input = new StreamReader(@inputGraph))
            {
                var buffer = input.ReadLine().Split(' ');

                 
                var countOfVertex = Convert.ToInt32(buffer[0]);
                var countOfEdges = Convert.ToInt32(buffer[1]);
                _countVertex = countOfVertex;

                //_graph = new List<Edge>[countOfVertex];
                _graph = new List<List<Edge>>();
                for(var i = 0; i < _countVertex;++i)
                    _graph.Add(new List<Edge>());

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
