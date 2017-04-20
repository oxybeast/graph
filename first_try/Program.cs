using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace first_try
{
    internal class Program
    {
        public List<Edge>[] Graph;

        public List<int> GoodVertex, BadVertex;
        public List<Edge> GoodEdges, BadEdges;
        //public List<List<Edge>> paths;

        public int MaximumLengthOfPath = 4;
        
        public void ReadDataAndSetGraph(string inputGraph)
        {
            var input = new StreamReader(@inputGraph);
            var buffer = input.ReadLine().Split(' ');

            var countOfVertex = Convert.ToInt32(buffer[0]);
            var countOfEdges = Convert.ToInt32(buffer[1]);

            Graph = new List<Edge>[countOfVertex];
            for(var i = 0; i < countOfVertex; ++i)
                Graph[i] = new List<Edge>();
            for (var i = 0; i < countOfEdges; ++i)
            {
                buffer = input.ReadLine().Split(' ');
                var @from = Convert.ToInt32(buffer[0]);
                var to = Convert.ToInt32(buffer[1]);
                var id = Convert.ToInt32(buffer[2]);
                var toAdd = new Edge(from, to, id);
                Graph[from].Add(toAdd);
            }
            input.Close();
        }

        public void ReadAllData(string inputGraph, string inputGoodEdges, string inputBadEdges, string inputGoodVertex, string inputBadVertex)
        {
            ReadDataAndSetGraph(inputGraph);
            GoodEdges = ReadSomething.ReadListEdges(inputGoodEdges);
            BadEdges = ReadSomething.ReadListEdges(inputBadEdges);
            GoodVertex = ReadSomething.ReadListIntegers(inputGoodVertex);
            BadVertex = ReadSomething.ReadListIntegers(inputBadVertex);
        }
        public List<Edge> [,] GetArrayWithPath(int start, int finish)
        {
            var bfsQueue = new Queue<VertexInQueue>();

            var usedVertex = new bool[Graph.Length, MaximumLengthOfPath + 1];

            var allInputEdges = new List<Edge>[Graph.Length, MaximumLengthOfPath + 1];

            for (var i = 0; i < Graph.Length; ++i)
            for (var j = 0; j < MaximumLengthOfPath + 1; ++j)
                allInputEdges[i, j] = new List<Edge>();

            usedVertex[start, 0] = true;
            bfsQueue.Enqueue(new VertexInQueue(start, 0));

            while (bfsQueue.Count > 0)
            {
                var current = bfsQueue.Dequeue();

                if (current.Turn == MaximumLengthOfPath)
                    continue;

                foreach(var edge in Graph[current.IdVert])
                {
                    if(usedVertex[edge.To, current.Turn + 1] == false)
                    {
                        bfsQueue.Enqueue(new VertexInQueue(edge.To, current.Turn + 1));
                        usedVertex[edge.To, current.Turn + 1] = true;
                    }
                    allInputEdges[edge.To, current.Turn + 1].Add(edge);
                }
            }
            return allInputEdges;
        }

        
        
        public List<List<Edge>>  GetPathInGoodForm(List<Edge> [,] allInputEdges, int finishVertex)
        {
            var tempPath = new List<Edge>();
            var result = new List<List<Edge>>();

            void GetAns(int idVertex, int turns)
            {
                if (turns == 0)
                {
                    result.Add(new List<Edge>(tempPath));
                    return;
                }
                foreach (var edge in allInputEdges[idVertex, turns])
                {
                    tempPath.Add(edge);
                    GetAns(edge.From, turns - 1);
                    tempPath.Remove(tempPath.Last());
                }
            }

            for (var lengthOfPath = MaximumLengthOfPath; lengthOfPath > 0; lengthOfPath--)
                GetAns(finishVertex, lengthOfPath);

            return result;
        }


        public List<List<Edge>> SievePathWithBadVertex(List<List<Edge>> paths, List<int> badVertex)
        {
            var result = new List<List<Edge>>();
            
            foreach (var path in paths)
            {
                var ifPathDoesntContainsBadVertex = true;
                foreach (var currentElement in path)
                    if (badVertex.Contains(currentElement.From) == true || badVertex.Contains(currentElement.To) == true)
                        ifPathDoesntContainsBadVertex = false;
                if(ifPathDoesntContainsBadVertex)
                    result.Add(new List<Edge>(path));
            }
            return result;
        }

        public List<List<Edge>> SievePathWithBadEdge(List<List<Edge>> paths, List<Edge> badEdges)
        {
            var result = new List<List<Edge>>();
            foreach (var path in paths)
            {
                var ifPathDoesntContainsBadEdges = true;
                foreach (var currentElement in path)
                    if (badEdges.Contains(currentElement) == true)
                        ifPathDoesntContainsBadEdges = false;
                if(ifPathDoesntContainsBadEdges)
                    result.Add(new List<Edge>(path));
            }
            return result;
        }

        public List<List<Edge>> SievePathWithoutGoodEdge(List<List<Edge>> paths, List<Edge> goodEdges)
        {
           var result = new List<List<Edge>>();

            foreach (var path in paths)
            {
                var ifPathContainsAllGoodEdges = true;
                foreach (var edgeToCheck in goodEdges)
                {
                    if (path.Contains(edgeToCheck) == false)
                        ifPathContainsAllGoodEdges = false;
                }
                if(ifPathContainsAllGoodEdges)
                    result.Add(new List<Edge>(path));
            }
           return result;
        }

        public List<List<Edge>> SievePathWithoutGoodVertex(List<List<Edge>> paths, List<int> goodVertex)
        {
            var result = new List<List<Edge>>();
            foreach (var path in paths)
            {
                var ifPathContainsAllGoodVertex = true;
                foreach (var vertexToCheck in goodVertex)
                {
                    var ifContains = false;
                    foreach (var edge in path)
                    {
                        if (edge.From == vertexToCheck || edge.To == vertexToCheck)
                            ifContains = true;
                    }
                    if (ifContains == false)
                        ifPathContainsAllGoodVertex = false;
                }
                if(ifPathContainsAllGoodVertex)
                    result.Add(new List<Edge>(path));
            }
            return result;
        }
        public List<List<Edge>> SievePaths(List<List<Edge>> paths)
        {
            var result = SievePathWithBadEdge(paths, BadEdges);
            result = SievePathWithBadVertex(result, BadVertex);
            result = SievePathWithoutGoodEdge(result, GoodEdges);
            result = SievePathWithoutGoodVertex(result, GoodVertex);
            return result;
        }
        public void Output_data(List<List<Edge>> paths, int startVertex)
        {
            
            var output = new StreamWriter(@"output.txt");
            if (paths.Count == 0)
            {
                output.WriteLine("No paths.");
            }
            else
            {
                foreach (var currentPath in paths)
                {
                    currentPath.Reverse();
                    
                    output.Write("(");
                    output.Write(startVertex);
                    output.Write(") ");
                    
                    foreach (var currentEdge in currentPath)
                    {
                        output.Write("id_");
                        output.Write(currentEdge.Id);
                        output.Write(' ');
                        output.Write("(");
                        output.Write(currentEdge.To);
                        output.Write(") ");
                    }
                    output.WriteLine("");
                }
            }
            
            output.Close();
        }

        private static void Main(string[] args)
        {
            var q = new Program();
            int k1 = 0, k2 = 4;
            q.MaximumLengthOfPath = 4;
            q.ReadAllData("input.txt", "goodEdges.txt", "badEdges.txt", "goodVertex.txt", "badVertex.txt");
            q.Output_data(q.SievePaths(q.GetPathInGoodForm(q.GetArrayWithPath(k1, k2), k2)), k1);
        }
    }
    
}
