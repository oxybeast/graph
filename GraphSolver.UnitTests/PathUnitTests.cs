using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xunit;

namespace GraphSolver.UnitTests
{
    public class PathUnitTests
    {
        [Fact]
        public void PathLeghtK_LengthK()
        {
            var p = new List<Edge>{new Edge(1, 2, 3), new Edge(2, 3, 4), new Edge(3, 4, 5)};
            var path = new Path(p);
            Assert.Equal(p.Count, path.Length());
        }
        [Fact]
        public void PathLength0_Length0()
        {
            var p = new List<Edge>();
            var path = new Path(p);
            Assert.Equal(p.Count, path.Length());
        }
    }
}
