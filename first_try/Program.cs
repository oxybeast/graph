using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GraphSolver
{
    internal class Program
    {
        public void SolveGraphProblem(string inputGoodEdges, string inputBadEdges, string inputGoodVertex, string inputBadVertex)
        {
            
            var currentGraph = new Graph("input.txt");
            var thePathFinder = new PathFinder(0,4,4);
            thePathFinder.GetAllPaths(currentGraph);
            var conditionOnPath =
                new SievePaths(inputGoodVertex, inputBadVertex, inputBadEdges, inputGoodEdges);
            List<List<Edge>> resultPaths = new List<List<Edge>>();
            foreach (var path in thePathFinder.AllPaths)
            {
                if(conditionOnPath.CheckAllCondition(path) == true)
                    resultPaths.Add(new List<Edge>(path.OnePath));
            }
            Output_data(resultPaths);
        }
        public void Output_data(List<List<Edge>> paths)
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
                    var startVertex = currentPath.Last().From;
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
            var q = new Program ();
            q.SolveGraphProblem("goodEdges.txt", "badEdges.txt", "goodVertex.txt", "badVertex.txt");
        }
    }
    
}
