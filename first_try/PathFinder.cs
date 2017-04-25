using System.Collections.Generic;
using System.Linq;

namespace GraphSolver
{
    public class PathFinder
    {
        public List<Path> AllPaths;
        private List<Edge>[,] _arrayOfPaths;
        public int StartVertex, FinishVertex, MaximumLengthOfPaths;

        public PathFinder(int start, int finish, int maximumLength)
        {
            StartVertex = start;
            FinishVertex = finish;
            MaximumLengthOfPaths = maximumLength;
        }

        private void GetArrayOfPaths(Graph currentGraph)
        {

            var bfsQueue = new Queue<VertexInQueue>();

            var usedVertex = new bool[currentGraph.Count, MaximumLengthOfPaths + 1];

            _arrayOfPaths = new List<Edge>[currentGraph.Count, MaximumLengthOfPaths + 1];

            for (var i = 0; i < currentGraph.Count; ++i)
            for (var j = 0; j < MaximumLengthOfPaths + 1; ++j)
                _arrayOfPaths[i, j] = new List<Edge>();

            if (currentGraph.Count <= StartVertex || StartVertex < 0)
                return;

            usedVertex[StartVertex, 0] = true;
            bfsQueue.Enqueue(new VertexInQueue(StartVertex, 0));

            

            while (bfsQueue.Count > 0)
            {
                var current = bfsQueue.Dequeue();

                if (current.Turn == MaximumLengthOfPaths)
                    continue;

                foreach (var edge in currentGraph.GetEdgesByVertexId(current.IdVert))
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
        private void GetPathsInGoodForm()
        {
            var tempPath = new List<Edge>();
            AllPaths = new List<Path>();
            if (FinishVertex < 0 || FinishVertex >= (_arrayOfPaths.Length)/(MaximumLengthOfPaths+1))
                return;
            void GetAns(int idVertex, int turns)
            {
                if (turns == 0)
                {
                    tempPath.Reverse();
                    AllPaths.Add(new Path(tempPath));
                    tempPath.Reverse();
                    return;
                }
                foreach (var edge in _arrayOfPaths[idVertex, turns])
                {
                    tempPath.Add(edge);
                    GetAns(edge.From, turns - 1);
                    tempPath.Remove(tempPath.Last());
                }
            }

            for (var lengthOfPath = MaximumLengthOfPaths; lengthOfPath > 0; lengthOfPath--)
                GetAns(FinishVertex, lengthOfPath);
        }

        public void GetAllPaths(Graph currentGraph)
        {
            GetArrayOfPaths(currentGraph);
            GetPathsInGoodForm();
        }

    }
}