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
    }
}
