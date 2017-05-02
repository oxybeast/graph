using System.Collections.Generic;
using System.Linq;

namespace GraphSolver
{
    public class PathFinder
    {
        private List<Path> AllPaths;
        private List<Edge>[,] _arrayOfPaths;
        private int StartVertex, FinishVertex, MaximumLengthOfPaths;
        private Graph _graph;

        public PathFinder(string tableFrom, string tableTo, int maximumLength, Graph graph)
        {
            _graph = graph;
            StartVertex = graph.GetIdOfVertex(tableFrom);
            FinishVertex = graph.GetIdOfVertex(tableTo);
            MaximumLengthOfPaths = maximumLength;
        }

        private void GetArrayOfPaths()
        {

            var bfsQueue = new Queue<VertexInQueue>();

            var usedVertex = new bool[_graph.Count, MaximumLengthOfPaths + 1];

            _arrayOfPaths = new List<Edge>[_graph.Count, MaximumLengthOfPaths + 1];

            for (var i = 0; i < _graph.Count; ++i)
            for (var j = 0; j < MaximumLengthOfPaths + 1; ++j)
                _arrayOfPaths[i, j] = new List<Edge>();

            if (_graph.Count <= StartVertex || StartVertex < 0)
                return;

            usedVertex[StartVertex, 0] = true;
            bfsQueue.Enqueue(new VertexInQueue(StartVertex, 0));

            

            while (bfsQueue.Count > 0)
            {
                var current = bfsQueue.Dequeue();

                if (current.Turn == MaximumLengthOfPaths)
                    continue;

                foreach (var edge in _graph.GetEdgesByVertexId(current.IdVert))
                {
                    if (usedVertex[edge.To, current.Turn + 1] == false)
                    {
                        bfsQueue.Enqueue(new VertexInQueue(edge.To, current.Turn + 1));
                        usedVertex[edge.To, current.Turn + 1] = true;
                    }
                    _arrayOfPaths[edge.To, current.Turn + 1].Add(edge);
                }
            }
        }

        private List<Edge> _tempPath;
        private void GetAns(int idVertex, int turns)
        {
            if (turns == 0)
            {
                _tempPath.Reverse();
                AllPaths.Add(new Path(_tempPath));
                _tempPath.Reverse();
                return;
            }
            foreach (var edge in _arrayOfPaths[idVertex, turns])
            {
                _tempPath.Add(edge);
                GetAns(edge.From, turns - 1);
                _tempPath.RemoveAt(_tempPath.Count - 1);
            }
        }
        private void GetPathsInGoodForm()
        {
            _tempPath = new List<Edge>();
            AllPaths = new List<Path>();
            if (FinishVertex < 0 || FinishVertex >= (_arrayOfPaths.Length)/(MaximumLengthOfPaths+1))
                return;
            

            for (var lengthOfPath = MaximumLengthOfPaths; lengthOfPath > 0; lengthOfPath--)
                GetAns(FinishVertex, lengthOfPath);
        }

        public List<Path> GetAllPaths()
        {
            GetArrayOfPaths();
            GetPathsInGoodForm();
            return new List<Path>(AllPaths);
        }

    }
}