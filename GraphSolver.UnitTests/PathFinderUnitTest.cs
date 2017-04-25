using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Xunit.Assert;

namespace GraphSolver.UnitTests
{
    public class PathFinderUnitTest
    {
        private List<Edge>[] GenGraph()
        {
            var a = new List<Edge>[5];
            a[0] = new List<Edge> { new Edge(0, 1, 1), new Edge(0, 2, 2), new Edge(0, 3, 3) };
            a[1] = new List<Edge> { new Edge(1, 2, 4), new Edge(1, 2, 5), new Edge(1, 4, 6) };
            a[2] = new List<Edge> { new Edge(2, 3, 7), new Edge(2, 4, 8) };
            a[3] = new List<Edge> { new Edge(3, 4, 9) };
            a[4] = new List<Edge>();
            return a;
        }

        private Graph GenerateSampleGraph()
        {
            var g = new Graph();
            g.AddEdge(new Edge(0, 1, 1));
            g.AddEdge(new Edge(0, 2, 2));
            g.AddEdge(new Edge(0, 3, 3));
            g.AddEdge(new Edge(1, 2, 4));
            g.AddEdge(new Edge(1, 2, 5));
            g.AddEdge(new Edge(1, 4, 6));
            g.AddEdge(new Edge(2, 3, 7));
            g.AddEdge(new Edge(2, 4, 8));
            g.AddEdge(new Edge(3, 4, 9));
            return g;
        }
        [Fact]

        public void GraphWithoutStart_ReturnNoPaths()
        {
            var g = new Graph();
            g.AddEdge(new Edge(0, 1, 1));
            g.AddEdge(new Edge(1, 2, 2));
            g.AddEdge(new Edge(2, 0, 3));

            var somePathFinder = new PathFinder(3, 1, 2);
            somePathFinder.GetAllPaths(g);
            //Assert.Throws<IndexOutOfRangeException>(() => somePathFinder.GetAllPaths(g));
            Assert.Equal(new List<Path> (), somePathFinder.AllPaths);
        }

        [Fact]
        public void GraphWithoutFinish_ReturnNoPaths()
        {
            var g = new Graph();
            g.AddEdge(new Edge(0, 1, 1));
            g.AddEdge(new Edge(1, 2, 2));
            g.AddEdge(new Edge(2, 0, 3));
            var somePathFinder = new PathFinder(0, 3, 2);
            somePathFinder.GetAllPaths(g);
            //Assert.Throws<IndexOutOfRangeException>(() => somePathFinder.GetAllPaths(g));
            Assert.Equal(new List<Path> (), somePathFinder.AllPaths);
        }

        [Fact]
        public void GraphWithCycle_PathsShorterThanK()
        {
            var g = new Graph();
            g.AddEdge(new Edge(0, 1, 1));
            g.AddEdge(new Edge(1, 2, 2));
            g.AddEdge(new Edge(2, 0, 3));

            var somePathFinder = new PathFinder(0, 2, 10);
            somePathFinder.GetAllPaths(g);

            var flag = true;
            foreach (var path in somePathFinder.AllPaths)
            {
                if (path.Length() > 10)
                    flag = false;
            }
            Assert.Equal(true, flag);
        }

        [Fact]
        public void GraphWithoutPaths1_NoPaths()
        {
            var g = new Graph();
            g.AddEdge(new Edge(0, 1, 1));
            g.AddEdge(new Edge(1, 2, 2));
            g.AddEdge(new Edge(2, 1, 3));

            var somePathFinder = new PathFinder(2, 0, 10);
            somePathFinder.GetAllPaths(g);
            Assert.Equal(new List<Path>(), somePathFinder.AllPaths);
        }

        [Fact]
        public void GraphWithoutPaths2_NoPaths()
        {
            var a = new List<Edge>[4];
            a[0] = new List<Edge> {new Edge(0, 1, 1)};
            a[1] = new List<Edge> {new Edge(1, 0, 2)};
            a[2] = new List<Edge> {new Edge(2, 3, 3)};
            a[3] = new List<Edge> {new Edge(3, 2, 4)};
            var g = new Graph(a);
            var somePathFinder = new PathFinder(2, 0, 10);
            somePathFinder.GetAllPaths(g);
            Assert.Equal(new List<Path>(), somePathFinder.AllPaths);
        }

        [Fact]
        public void GraphWithPathLongerThanK_NoPaths()
        {
            var a = new List<Edge>[4];
            a[0] = new List<Edge> {new Edge(0, 1, 1)};
            a[1] = new List<Edge> {new Edge(1, 0, 2), new Edge(1, 2, 5)};
            a[2] = new List<Edge> {new Edge(2, 3, 3)};
            a[3] = new List<Edge> {new Edge(3, 2, 4)};
            var g = new Graph(a);
            var somePathFinder = new PathFinder(0, 3, 2);
            somePathFinder.GetAllPaths(g);
            Assert.Equal(new List<Path>(), somePathFinder.AllPaths);
        }

        [Fact]
        public void GraphWithManyPaths1_ManyPaths()
        {
            var g = new Graph(GenGraph());

            var somePathFinder = new PathFinder(0, 4, 2);
            somePathFinder.GetAllPaths(g);

            var toCheck = new List<Path>(somePathFinder.AllPaths);

            var answer = new List<Path>
            {
                new Path(new List<Edge> {new Edge(0, 3, 3), new Edge(3, 4, 9)}),
                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 4, 6)}),
                new Path(new List<Edge> {new Edge(0, 2, 2), new Edge(2, 4, 8)})
            };

            //...
            
            
            var comp = new CompareSomething();
            toCheck.Sort(comp.ComparePaths);
            answer.Sort(comp.ComparePaths);

            var flag = comp.CompareListPaths(answer, toCheck) == 0;
            Assert.Equal(true, flag);
        }
        [Fact]
        public void GraphWithManyPaths2_ManyPaths()
        {
            
            var g = new Graph(GenGraph());

            var somePathFinder = new PathFinder(0, 4, 3);
            somePathFinder.GetAllPaths(g);

            var toCheck = new List<Path>(somePathFinder.AllPaths);

            var answer = new List<Path>
            {
                new Path(new List<Edge> {new Edge(0, 3, 3), new Edge(3, 4, 9)}),
                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 4, 6)}),
                new Path(new List<Edge> {new Edge(0, 2, 2), new Edge(2, 4, 8)}),

                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 2, 4), new Edge(2,4,8)}),
                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 2, 5), new Edge(2,4,8)}),
                new Path(new List<Edge> {new Edge(0, 2, 2), new Edge(2, 3, 7), new Edge(3,4,9)})
            };

            //...
            

            var comp = new CompareSomething();
            toCheck.Sort(comp.ComparePaths);
            answer.Sort(comp.ComparePaths);

            var flag = comp.CompareListPaths(answer, toCheck) == 0;
            Assert.Equal(true, flag);
        }
        [Fact]
        public void GraphWithManyPaths3_ManyPaths()
        {
            var g = new Graph(GenGraph());

            var somePathFinder = new PathFinder(0, 4, 4);
            somePathFinder.GetAllPaths(g);

            var toCheck = new List<Path>(somePathFinder.AllPaths);

            var answer = new List<Path>
            {
                new Path(new List<Edge> {new Edge(0, 3, 3), new Edge(3, 4, 9)}),
                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 4, 6)}),
                new Path(new List<Edge> {new Edge(0, 2, 2), new Edge(2, 4, 8)}),

                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 2, 4), new Edge(2,4,8)}),
                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 2, 5), new Edge(2,4,8)}),
                new Path(new List<Edge> {new Edge(0, 2, 2), new Edge(2, 3, 7), new Edge(3,4,9)}),

                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 2, 4), new Edge(2,3,7), new Edge(3,4,9)}),
                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 2, 5), new Edge(2,3,7), new Edge(3,4,9)})
            };

            //...
            

            var comp = new CompareSomething();
            toCheck.Sort(comp.ComparePaths);
            answer.Sort(comp.ComparePaths);

            var flag = comp.CompareListPaths(answer, toCheck) == 0;
            Assert.Equal(true, flag);
        }
        [Fact]
        public void GraphWithManyPaths4_ManyPaths()
        {
            var a = new List<Edge>[5];
            a[0] = new List<Edge> { new Edge(0, 1, 1), new Edge(0, 2, 2), new Edge(0, 3, 3) };
            a[1] = new List<Edge> { new Edge(1, 2, 4), new Edge(1, 2, 5), new Edge(1, 4, 6) };
            a[2] = new List<Edge> { new Edge(2, 3, 7), new Edge(2, 4, 8) };
            a[3] = new List<Edge> { new Edge(3, 4, 9) };
            a[4] = new List<Edge>();

            var g = new Graph(a);

            var somePathFinder = new PathFinder(0, 4, 100);
            somePathFinder.GetAllPaths(g);

            var toCheck = new List<Path>(somePathFinder.AllPaths);

            var answer = new List<Path>
            {
                new Path(new List<Edge> {new Edge(0, 3, 3), new Edge(3, 4, 9)}),
                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 4, 6)}),
                new Path(new List<Edge> {new Edge(0, 2, 2), new Edge(2, 4, 8)}),

                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 2, 4), new Edge(2,4,8)}),
                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 2, 5), new Edge(2,4,8)}),
                new Path(new List<Edge> {new Edge(0, 2, 2), new Edge(2, 3, 7), new Edge(3,4,9)}),

                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 2, 4), new Edge(2,3,7), new Edge(3,4,9)}),
                new Path(new List<Edge> {new Edge(0, 1, 1), new Edge(1, 2, 5), new Edge(2,3,7), new Edge(3,4,9)})
            };

            //...
            

            var comp = new CompareSomething();
            toCheck.Sort(comp.ComparePaths);
            answer.Sort(comp.ComparePaths);

            var flag = comp.CompareListPaths(answer, toCheck) == 0;
            Assert.Equal(true, flag);
        }

        [Fact]
        public void GeneratedGraph_ReturRightCount()
        {
            var g = GenerateSampleGraph();
            Assert.Equal(5, g.Count);
        }
    }
}
