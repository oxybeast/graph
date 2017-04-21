using System.Collections.Generic;
using System.Linq;

namespace GraphSolver
{
    public class PathFinder
    {
        public List<Path> AllPaths;
        private List<Edge>[,] ArrayOfPaths;
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

            var usedVertex = new bool[currentGraph.graph.Length, MaximumLengthOfPaths + 1];

            ArrayOfPaths = new List<Edge>[currentGraph.graph.Length, MaximumLengthOfPaths + 1];

            for (var i = 0; i < currentGraph.graph.Length; ++i)
            for (var j = 0; j < MaximumLengthOfPaths + 1; ++j)
                ArrayOfPaths[i, j] = new List<Edge>();

            usedVertex[StartVertex, 0] = true;
            bfsQueue.Enqueue(new VertexInQueue(StartVertex, 0));

            while (bfsQueue.Count > 0)
            {
                var current = bfsQueue.Dequeue();

                if (current.Turn == MaximumLengthOfPaths)
                    continue;

                foreach (var edge in currentGraph.graph[current.IdVert])
                {
                    if (usedVertex[edge.To, current.Turn + 1] == false)
                    {
                        bfsQueue.Enqueue(new VertexInQueue(edge.To, current.Turn + 1));
                        usedVertex[edge.To, current.Turn + 1] = true;
                    }
                    ArrayOfPaths[edge.To, current.Turn + 1].Add(edge);
                }
            }
        }
        private void GetPathsInGoodForm()
        {
            var tempPath = new List<Edge>();
            AllPaths = new List<Path>();

            void GetAns(int idVertex, int turns)
            {
                if (turns == 0)
                {
                    AllPaths.Add(new Path(tempPath));
                    return;
                }
                foreach (var edge in ArrayOfPaths[idVertex, turns])
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