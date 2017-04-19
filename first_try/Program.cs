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
        public List<Edge>[] graph;
        //public List<List<Edge>> paths;
        
        public int maximumLengthOfPath = 4;
        public void ReadDataAndSetGraph()
        {
            StreamReader input = new StreamReader(@"input.txt");
            string[] buffer = input.ReadLine().Split(' ');

            int CountOfVertex = Convert.ToInt32(buffer[0]);
            int CountOfEdges = Convert.ToInt32(buffer[1]);

            graph = new List<Edge>[CountOfVertex];
            for(int i = 0; i < CountOfVertex; ++i)
            {
                graph[i] = new List<Edge>();
            }
            for (int i = 0; i < CountOfEdges; ++i)
            {
                buffer = input.ReadLine().Split(' ');
                int from, to, id;
                from = Convert.ToInt32(buffer[0]);
                to = Convert.ToInt32(buffer[1]);
                id = Convert.ToInt32(buffer[2]);
                Edge to_add = new Edge(from, to, id);
                graph[from].Add(to_add);
            }
            input.Close();
        }
        public List<Edge> [,] GetArrayWithPath(int start, int finish)
        {
            List<Edge>[,] allInputEdges;

            Queue<VertexInQueue> bfsQueue = new Queue<VertexInQueue>();

            bool[,] used_vertex = new bool[graph.Length, maximumLengthOfPath + 1];

            allInputEdges = new List<Edge>[graph.Length, maximumLengthOfPath + 1];

            for (int i = 0; i < graph.Length; ++i)
            {
                for (int j = 0; j < maximumLengthOfPath + 1; ++j)
                    allInputEdges[i, j] = new List<Edge>();
            }

            used_vertex[start, 0] = true;
            bfsQueue.Enqueue(new VertexInQueue(start, 0));

            while (bfsQueue.Count > 0)
            {
                VertexInQueue current = bfsQueue.Dequeue();

                if (current.turn == maximumLengthOfPath)
                    continue;

                foreach(Edge edge in graph[current.idVert])
                {
                    if(used_vertex[edge.to, current.turn + 1] == false)
                    {
                        bfsQueue.Enqueue(new VertexInQueue(edge.to, current.turn + 1));
                        used_vertex[edge.to, current.turn + 1] = true;
                    }
                    allInputEdges[edge.to, current.turn + 1].Add(edge);
                }
            }
            return allInputEdges;
        }

        
        
        public List<List<Edge>>  GetPathInGoodForm(List<Edge> [,] allInputEdges, int finishVertex)
        {
            List<Edge> temp_path = new List<Edge>();
            List<List<Edge>> result = new List<List<Edge>>();

            void Get_ans(int id_vertex, int turns)
            {
                if (turns == 0)
                {
                    result.Add(new List<Edge>(temp_path));
                    return;
                }
                foreach (Edge edge in allInputEdges[id_vertex, turns])
                {
                    temp_path.Add(edge);
                    Get_ans(edge.from, turns - 1);
                    temp_path.Remove(temp_path.Last());
                }
            }

            for (int lengthOfPath = maximumLengthOfPath; lengthOfPath > 0; lengthOfPath--)
            {
                 Get_ans(finishVertex, lengthOfPath);
            }

            return result;
        }
        void Output_data(List<List<Edge>> paths, int startVertex)
        {
            StreamWriter output = new StreamWriter(@"output.txt");
            foreach (List<Edge> currentPath in paths)
            {
                currentPath.Reverse();

                output.Write("(");
                output.Write(startVertex);
                output.Write(") ");

                foreach (Edge currentEdge in currentPath)
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
            Q.maximumLengthOfPath = 4;
            Q.ReadDataAndSetGraph();
            //Q.Get_array_with_path(k1, k2);
            Q.Output_data(Q.GetPathInGoodForm(Q.GetArrayWithPath(k1, k2), k2), k1);
        }
    }
}
