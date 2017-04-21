using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphSolver
{
    public class Graph
    {
        public List<Edge>[] graph;
        

        public Graph(string inputFile)
        {
            ReadGraph(inputFile);
        }

        

        public void ReadGraph(string inputGraph)
        {
            using (var input = new StreamReader(@inputGraph))
            {
                var buffer = input.ReadLine().Split(' ');

                var countOfVertex = Convert.ToInt32(buffer[0]);
                var countOfEdges = Convert.ToInt32(buffer[1]);

                graph = new List<Edge>[countOfVertex];
                for (var i = 0; i < countOfVertex; ++i)
                    graph[i] = new List<Edge>();
                for (var i = 0; i < countOfEdges; ++i)
                {
                    buffer = input.ReadLine().Split(' ');
                    var @from = Convert.ToInt32(buffer[0]);
                    var to = Convert.ToInt32(buffer[1]);
                    var id = Convert.ToInt32(buffer[2]);
                    var toAdd = new Edge(from, to, id);
                    graph[from].Add(toAdd);
                }
            }
        }
    }
    
}
