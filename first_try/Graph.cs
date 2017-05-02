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
        private List<VertexOfTable> _nodes;
        private Dictionary<string, int> _idDictionary;
        private Dictionary<int, string> _nameOfId;
        private int _countVertex;

        public Graph()
        {
            _graph = new List<List<Edge>>();
            _idDictionary = new Dictionary<string, int>();
            _nameOfId = new Dictionary<int, string>();
            _nodes = new List<VertexOfTable>();
            _countVertex = 0;
        }
        public string GetNameOfTableById(int id)
        {
            return _nameOfId[id];
        }
        public int GetIdOfVertex(VertexOfTable vertex)
        {
            var name = vertex.GetName();
            if (_idDictionary.ContainsKey(name))
                return _idDictionary[name];
            else
                return -1;
        }
        public int GetIdOfVertex(string nameOfTable)
        {
            if (_idDictionary.ContainsKey(nameOfTable))
                return _idDictionary[nameOfTable];
            else
                return -1;
        }
        public void AddVertexIfNonExist(VertexOfTable vertex)
        {
            var nameOfTable = vertex.GetName();
            if (_idDictionary.ContainsKey(nameOfTable)) return;

            _idDictionary[nameOfTable] = _countVertex;
            _nameOfId[_idDictionary[nameOfTable]] = nameOfTable;

            _graph.Add(new List<Edge>());
            _nodes.Add(vertex);
            _countVertex++;
        }
        public void AddEdge(VertexOfTable tableFrom, VertexOfTable tableTo, int id)
        {
            AddVertexIfNonExist(tableFrom);
            AddVertexIfNonExist(tableTo);
            var edge = new Edge(_idDictionary[tableFrom.GetName()], _idDictionary[tableTo.GetName()], id);
            AddEdge(edge);
        }
        public void AddEdgeIfVertexExist(string tableFrom, string tableTo, int edgeId)
        {
            var idFrom = GetIdOfVertex(tableFrom);
            var idTo = GetIdOfVertex(tableTo);
            if (idFrom == -1 || idTo == -1) return;
            var edge = new Edge(idFrom, idTo, edgeId);
            AddEdge(edge);
        }
        private void AddEdge(Edge edge)
        {
            _graph[edge.From].Add(edge);
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

    }
    
}
