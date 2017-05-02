using System.Collections.Generic;
using System.Diagnostics.Eventing;
using System.IO;

namespace GraphSolver
{
    internal partial class Program
    {
        public class Solver
        {
            private List<Path> outputPaths;
            private Graph graph;
            public void solve()
            {
                graph = new Graph();
                graph.AddVertexIfNonExist(new VertexOfTable("A", new List<string>()));
                graph.AddVertexIfNonExist(new VertexOfTable("B", new List<string>()));
                graph.AddVertexIfNonExist(new VertexOfTable("C", new List<string>()));
                graph.AddVertexIfNonExist(new VertexOfTable("D", new List<string>()));
                graph.AddVertexIfNonExist(new VertexOfTable("E", new List<string>()));
                graph.AddEdgeIfVertexExist("A", "B", 0);
                graph.AddEdgeIfVertexExist("A", "C", 1);
                graph.AddEdgeIfVertexExist("A", "D", 2);
                graph.AddEdgeIfVertexExist("B", "C", 3);
                graph.AddEdgeIfVertexExist("C", "B", 4);
                graph.AddEdgeIfVertexExist("B", "E", 5);
                graph.AddEdgeIfVertexExist("C", "E", 6);
                graph.AddEdgeIfVertexExist("D", "C", 7);
                graph.AddEdgeIfVertexExist("D", "E", 8);
                var finder = new PathFinder("A", "E", 4, graph);
                outputPaths = finder.GetAllPaths();
            }

            private void WriteOnePath(StreamWriter output, Path path)
            {
                output.Write("(");
                output.Write(graph.GetNameOfTableById(path.OnePath[0].From));
                output.Write(")");
                output.Write(" ");
                foreach (var edge in path.OnePath)
                {
                    output.Write(edge.Id);
                    output.Write(" ");
                    output.Write("(");
                    output.Write(graph.GetNameOfTableById(edge.To));
                    output.Write(")");
                    output.Write(" ");
                }
                output.WriteLine();
            }
            public void WriteAllPaths()
            {
                var output = new StreamWriter(@"output.txt");
                foreach (var path in outputPaths)
                {
                    if(path.Length() != 0)
                        WriteOnePath(output, path);
                }
                output.Close();
            }
        }
    }
    
}
