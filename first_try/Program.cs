using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace first_try
{
    class Program
    {
        public List<Edge>[] Graph;
        //public List<List<Edge>> paths;jfjkfghkfgkf
        
        public int MaximumLengthOfPath = 4;
        public void ReadDataAndSetGraph()
        {
            var input = new StreamReader(@"input.txt");
            var buffer = input.ReadLine().Split(' ');

            var countOfVertex = Convert.ToInt32(buffer[0]);
            var countOfEdges = Convert.ToInt32(buffer[1]);

            Graph = new List<Edge>[countOfVertex];
            for(var i = 0; i < countOfVertex; ++i)
            {
                Graph[i] = new List<Edge>();
            }
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
        public List<Edge> [,] GetArrayWithPath(int start, int finish)
        {
            var bfsQueue = new Queue<VertexInQueue>();

            var usedVertex = new bool[Graph.Length, MaximumLengthOfPath + 1];

            var allInputEdges = new List<Edge>[Graph.Length, MaximumLengthOfPath + 1];

            for (var i = 0; i < Graph.Length; ++i)
            {
                for (var j = 0; j < MaximumLengthOfPath + 1; ++j)
                    allInputEdges[i, j] = new List<Edge>();
            }

            usedVertex[start, 0] = true;
            bfsQueue.Enqueue(new VertexInQueue(start, 0));

            while (bfsQueue.Count > 0)
            {
                var current = bfsQueue.Dequeue();

                if (current.turn == MaximumLengthOfPath)
                    continue;

                foreach(var edge in Graph[current.idVert])
                {
                    if(usedVertex[edge.to, current.turn + 1] == false)
                    {
                        bfsQueue.Enqueue(new VertexInQueue(edge.to, current.turn + 1));
                        usedVertex[edge.to, current.turn + 1] = true;
                    }
                    allInputEdges[edge.to, current.turn + 1].Add(edge);
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
                    GetAns(edge.from, turns - 1);
                    tempPath.Remove(tempPath.Last());
                }
            }

            for (var lengthOfPath = MaximumLengthOfPath; lengthOfPath > 0; lengthOfPath--)
            {
                 GetAns(finishVertex, lengthOfPath);
            }

            return result;
        }
        void Output_data(List<List<Edge>> paths, int startVertex)
        {
            var output = new StreamWriter(@"output.txt");
            foreach (var currentPath in paths)
            {
                currentPath.Reverse();

                output.Write("(");
                output.Write(startVertex);
                output.Write(") ");

                foreach (var currentEdge in currentPath)
                {
                    output.Write("id_");
                    output.Write(currentEdge.id);
                    output.Write(' ');
                    output.Write("(");
                    output.Write(currentEdge.to);
                    output.Write(") ");
                }
                output.WriteLine("");
            }
            output.Close();
        }
        static void Main(string[] args)
        {
            Program Q = new Program();
            int k1 = 0, k2 = 4;
            Q.MaximumLengthOfPath = 4;
            Q.ReadDataAndSetGraph();
            //Q.Get_array_with_path(k1, k2);
            Q.Output_data(Q.GetPathInGoodForm(Q.GetArrayWithPath(k1, k2), k2), k1);
        }
    }
}
