using System.Collections.Generic;
using System.Linq;

namespace GraphSolver
{
    public class SievePaths
    {
        public List<int> GoodVertex, BadVertex;
        public List<Edge> BadEdges, GoodEdges;
        //private Path path;

        public SievePaths(List<int> goodVertex, List<int> badVertex, List<Edge> badEdges, List<Edge> goodEdges)
        {
            GoodEdges = goodEdges;
            GoodVertex = goodVertex;
            BadEdges = badEdges;
            BadVertex = badVertex;
        }

        public SievePaths(string inputGoodVertex, string inputBadVertex, string inputBadEdges,
            string inputGoodEdges)
        {
            GoodEdges = ReadSomething.ReadListEdges(inputGoodEdges);
            BadEdges = ReadSomething.ReadListEdges(inputBadEdges);
            GoodVertex = ReadSomething.ReadListIntegers(inputGoodVertex);
            BadVertex = ReadSomething.ReadListIntegers(inputBadVertex);
        }
        private bool CheckIfPathContainsGoodVertex(Path path)
        {           
            var ifPathContainsAllGoodVertex = true;
            foreach (var vertexToCheck in GoodVertex)
            {
                var ifContains = false;
                foreach (var edge in path.OnePath)
                {
                    if (edge.From == vertexToCheck || edge.To == vertexToCheck)
                        ifContains = true;
                }
                if (ifContains == false)
                    ifPathContainsAllGoodVertex = false;
            }           
            return ifPathContainsAllGoodVertex;
        }

        private bool CheckIfPathDoesNotContainsBadVertex(Path path)
        {           
            var ifPathDoesntContainsBadVertex = true;
            foreach (var currentElement in path.OnePath)
                if (BadVertex.Contains(currentElement.From) == true || BadVertex.Contains(currentElement.To) == true)
                    ifPathDoesntContainsBadVertex = false;
            return ifPathDoesntContainsBadVertex;
        }

        private bool CheckIfPathContainsAllGoodEdges(Path path)
        {                   
            var ifPathContainsAllGoodEdges = true;
            foreach (var edgeToCheck in GoodEdges)
            {
                if (path.OnePath.Contains(edgeToCheck) == false)
                    ifPathContainsAllGoodEdges = false;
            }
            return ifPathContainsAllGoodEdges;
        }

        private bool CheckIfPathDoesNotContainsBadEdges(Path path)
        {        
            var ifPathDoesntContainsBadEdges = true;
            foreach (var currentElement in path.OnePath)
                if (BadEdges.Contains(currentElement) == true)
                    ifPathDoesntContainsBadEdges = false;
            return ifPathDoesntContainsBadEdges;
        }

        public IEnumerable<Path>  CheckAllCondition(params Path[] paths)
        {
            foreach (var path in paths)
            {
                if (CheckAllCondition(path))
                    yield return path;
            }
        }

        public bool CheckAllCondition(Path path)
        {
            return CheckIfPathContainsAllGoodEdges(path) && CheckIfPathContainsGoodVertex(path) &&
                   CheckIfPathDoesNotContainsBadEdges(path) && CheckIfPathDoesNotContainsBadVertex(path);
        }
    }
    
}
