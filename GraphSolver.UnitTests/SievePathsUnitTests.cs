using System.Collections.Generic;
using Xunit;

namespace GraphSolver.UnitTests
{
    public class SievePathsUnitTests
    {
        [Fact]

        public void ReturnFalse_SameVertexInBadAndGoodLists()
        {
            var somePath = new Path (new List<Edge> {new Edge(0,1,2), new Edge(1,2,2),new  Edge(3,4,8)});
            var someIntList = new List<int> {0};
            var someSieverPaths = new SievePaths(someIntList, someIntList, new List<Edge>(), new List<Edge>());
            Assert.Equal(false, someSieverPaths.CheckAllCondition(somePath));
        }

        [Fact]
        public void ReturnNoPaths_NonContainsGoodVertex()
        {
            var listEdges = new List<Edge> {new Edge(1,2,3), new Edge( 2,3,4), new Edge(3,4,5) };
            var path = new Path(listEdges);
            var goodVertex = new List<int>{8};
            var sieverPaths = new SievePaths(goodVertex, new List<int>(), new List<Edge>(), new List<Edge>());
            Assert.Equal(false, sieverPaths.CheckAllCondition(path));
        }

        [Fact]
        public void ReturnNoPath1_pathContainBadVertex()
        {
            var badVertex = new List<int>{2};
            var listEdges = new List<Edge>{new Edge(1,2,3), new Edge(2,3,4)};
            var path = new Path(listEdges);
            var sieverPaths = new SievePaths(new List<int>(), badVertex, new List<Edge>(), new List<Edge>());
            Assert.Equal(false, sieverPaths.CheckAllCondition(path));
        }
        [Fact]
        public void ReturnNoPath2_pathContainBadVertex()
        {
            var badVertex = new List<int> { 3 };
            var listEdges = new List<Edge> { new Edge(1, 2, 3), new Edge(2, 3, 4) };
            var path = new Path(listEdges);
            var sieverPaths = new SievePaths(new List<int>(), badVertex, new List<Edge>(), new List<Edge>());
            Assert.Equal(false, sieverPaths.CheckAllCondition(path));
        }
        [Fact]
        public void ReturnNoPath3_pathContainBadVertex()
        {
            var badVertex = new List<int> { 1 };
            var listEdges = new List<Edge> { new Edge(1, 2, 3), new Edge(2, 3, 4) };
            var path = new Path(listEdges);
            var sieverPaths = new SievePaths(new List<int>(), badVertex, new List<Edge>(), new List<Edge>());
            Assert.Equal(false, sieverPaths.CheckAllCondition(path));
        }
    }
}
