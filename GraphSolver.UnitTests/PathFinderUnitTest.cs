using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Xunit.Assert;

namespace GraphSolver.UnitTests
{
    public class PathFinderUnitTest
    {
        private Graph GenerateSampleGraph()
        {
            var graph = new Graph();
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
            return graph;
        }
        [Fact]

        public void GraphWithoutStart_ReturnNoPaths()
        {
            var g = new Graph();
            g.AddVertexIfNonExist(new VertexOfTable("1", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("2", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("3", new List<string>()));

            var somePathFinder = new PathFinder("4", "2", 5, g);
            //Assert.Throws<IndexOutOfRangeException>(() => somePathFinder.GetAllPaths(g));
            Assert.Equal(new List<Path> (), somePathFinder.GetAllPaths());
        }

        [Fact]
        public void GraphWithoutFinish_ReturnNoPaths()
        {
            var g = new Graph();
            g.AddVertexIfNonExist(new VertexOfTable("1", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("2", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("3", new List<string>()));

            var somePathFinder = new PathFinder("1", "8", 5, g);
            //Assert.Throws<IndexOutOfRangeException>(() => somePathFinder.GetAllPaths(g));
            Assert.Equal(new List<Path> (), somePathFinder.GetAllPaths());
        }
        
        [Fact]
        public void GraphWithCycle_PathsNolLongThanK()
        {
            var g = new Graph();
            g.AddVertexIfNonExist(new VertexOfTable("1", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("2", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("3", new List<string>()));

            g.AddEdgeIfVertexExist("1","2", 0);
            g.AddEdgeIfVertexExist("2", "3", 1);
            g.AddEdgeIfVertexExist("3", "1", 2);
            var somePathFinder = new PathFinder("1", "2", 10, g);

            var outputPaths = somePathFinder.GetAllPaths();

            var flag = true;
            foreach (var path in outputPaths)
            {
                if (path.Length() > 10)
                    flag = false;
            }
            Assert.Equal(true, flag);
        }
        
        [Fact]
        public void GraphWithoutPaths_NoPaths()
        {
            var g = new Graph();
            g.AddVertexIfNonExist(new VertexOfTable("1", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("2", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("3", new List<string>()));

            g.AddEdgeIfVertexExist("1", "2", 0);
            g.AddEdgeIfVertexExist("2", "3", 1);
            g.AddEdgeIfVertexExist("3", "2", 2);

            var somePathFinder = new PathFinder("3", "1", 10, g);
            Assert.Equal(new List<Path>(), somePathFinder.GetAllPaths());
        }
        
        [Fact]
        public void GraphWithUnconnectedComponents_NoPaths()
        {
            var g = new Graph();
            g.AddVertexIfNonExist(new VertexOfTable("1", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("2", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("3", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("4", new List<string>()));
            g.AddEdgeIfVertexExist("1", "2", 0);
            g.AddEdgeIfVertexExist("2", "1", 1);
            g.AddEdgeIfVertexExist("3", "4", 2);
            g.AddEdgeIfVertexExist("4", "3", 3);
            var somePathFinder = new PathFinder("1", "4", 10, g);
            Assert.Equal(new List<Path>(), somePathFinder.GetAllPaths());
        }
        
        [Fact]
        public void GraphWithPathLongerThanK_NoPaths()
        {
            var g = new Graph();
            g.AddVertexIfNonExist(new VertexOfTable("1", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("2", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("3", new List<string>()));
            g.AddVertexIfNonExist(new VertexOfTable("4", new List<string>()));
            g.AddEdgeIfVertexExist("1", "2", 0);
            g.AddEdgeIfVertexExist("2", "1", 1);
            g.AddEdgeIfVertexExist("2", "3", 2);
            g.AddEdgeIfVertexExist("3", "4", 3);
            g.AddEdgeIfVertexExist("4", "3", 4);
            var somePathFinder = new PathFinder("1", "4", 2, g);
            Assert.Equal(new List<Path>(), somePathFinder.GetAllPaths());
        }
        
        [Fact]
        public void SampleGraph_PathsNotLonger2()
        {
            var g = GenerateSampleGraph();

            var somePathFinder = new PathFinder("A", "E", 2, g);

            var toCheck = new List<Path>(somePathFinder.GetAllPaths());

            var idA = g.GetIdOfVertex("A");
            var idB = g.GetIdOfVertex("B");
            var idC = g.GetIdOfVertex("C");
            var idD = g.GetIdOfVertex("D");
            var idE = g.GetIdOfVertex("E");
            var answer = new List<Path>
            {
                new Path(new List<Edge> {new Edge(idA, idD, 2), new Edge(idD, idE, 8)}),
                new Path(new List<Edge> {new Edge(idA, idC, 1), new Edge(idC, idE, 6)}),
                new Path(new List<Edge> {new Edge(idA, idB, 0), new Edge(idB, idE, 5)})
            };

            var comp = new CompareSomething();
            toCheck.Sort(comp.ComparePaths);
            answer.Sort(comp.ComparePaths);

            var flag = comp.CompareListPaths(answer, toCheck) == 0;
            Assert.Equal(true, flag);
        }
        
        [Fact]
        public void SampleGraph_PathsNotLonger3()
        {

            var g = GenerateSampleGraph();

            var somePathFinder = new PathFinder("A", "E", 3, g);

            var toCheck = new List<Path>(somePathFinder.GetAllPaths());

            var idA = g.GetIdOfVertex("A");
            var idB = g.GetIdOfVertex("B");
            var idC = g.GetIdOfVertex("C");
            var idD = g.GetIdOfVertex("D");
            var idE = g.GetIdOfVertex("E");
            var answer = new List<Path>
            {
                new Path(new List<Edge> {new Edge(idA, idD, 2), new Edge(idD, idE, 8)}),
                new Path(new List<Edge> {new Edge(idA, idC, 1), new Edge(idC, idE, 6)}),
                new Path(new List<Edge> {new Edge(idA, idB, 0), new Edge(idB, idE, 5)}),

                new Path(new List<Edge> {new Edge(idA, idD, 2), new Edge(idD, idC, 7), new Edge(idC, idE, 6)}),
                new Path(new List<Edge> {new Edge(idA, idC, 1), new Edge(idC, idB, 4), new Edge(idB, idE, 5)}),
                new Path(new List<Edge> {new Edge(idA, idB, 0), new Edge(idB, idC, 3), new Edge(idC, idE, 6)}),
            };


            var comp = new CompareSomething();
            toCheck.Sort(comp.ComparePaths);
            answer.Sort(comp.ComparePaths);

            var flag = comp.CompareListPaths(answer, toCheck) == 0;
            Assert.Equal(true, flag);
        }
        
        [Fact]
        public void SampleGraph_pathsNotLonger4()
        {
            var g = GenerateSampleGraph();

            var somePathFinder = new PathFinder("A", "E", 4, g);

            var toCheck = new List<Path>(somePathFinder.GetAllPaths());

            var idA = g.GetIdOfVertex("A");
            var idB = g.GetIdOfVertex("B");
            var idC = g.GetIdOfVertex("C");
            var idD = g.GetIdOfVertex("D");
            var idE = g.GetIdOfVertex("E");
            var answer = new List<Path>
            {
                new Path(new List<Edge> {new Edge(idA, idD, 2), new Edge(idD, idE, 8)}),
                new Path(new List<Edge> {new Edge(idA, idC, 1), new Edge(idC, idE, 6)}),
                new Path(new List<Edge> {new Edge(idA, idB, 0), new Edge(idB, idE, 5)}),

                new Path(new List<Edge> {new Edge(idA, idD, 2), new Edge(idD, idC, 7), new Edge(idC, idE, 6)}),
                new Path(new List<Edge> {new Edge(idA, idC, 1), new Edge(idC, idB, 4), new Edge(idB, idE, 5)}),
                new Path(new List<Edge> {new Edge(idA, idB, 0), new Edge(idB, idC, 3), new Edge(idC, idE, 6)}),

                new Path(new List<Edge> {new Edge(idA, idD, 2), new Edge(idD, idC, 7), new Edge(idC, idB, 4), new Edge(idB, idE, 5)}),
                new Path(new List<Edge> {new Edge(idA, idB, 0), new Edge(idB, idC, 3), new Edge(idC, idB, 4), new Edge(idB, idE, 5)}),
                new Path(new List<Edge> {new Edge(idA, idC, 1), new Edge(idC, idB, 4), new Edge(idB, idC, 3), new Edge(idC, idE, 6)}),
            };


            var comp = new CompareSomething();
            toCheck.Sort(comp.ComparePaths);
            answer.Sort(comp.ComparePaths);

            var flag = comp.CompareListPaths(answer, toCheck) == 0;
            Assert.Equal(true, flag);
        }
        
        [Fact]
        public void GraphWithManyPaths4_ManyPaths()
        {
            var g = GenerateSampleGraph();

            var somePathFinder = new PathFinder("A", "E", 5, g);

            var toCheck = new List<Path>(somePathFinder.GetAllPaths());

            var idA = g.GetIdOfVertex("A");
            var idB = g.GetIdOfVertex("B");
            var idC = g.GetIdOfVertex("C");
            var idD = g.GetIdOfVertex("D");
            var idE = g.GetIdOfVertex("E");
            var answer = new List<Path>
            {
                new Path(new List<Edge> {new Edge(idA, idD, 2), new Edge(idD, idE, 8)}),
                new Path(new List<Edge> {new Edge(idA, idC, 1), new Edge(idC, idE, 6)}),
                new Path(new List<Edge> {new Edge(idA, idB, 0), new Edge(idB, idE, 5)}),

                new Path(new List<Edge> {new Edge(idA, idD, 2), new Edge(idD, idC, 7), new Edge(idC, idE, 6)}),
                new Path(new List<Edge> {new Edge(idA, idC, 1), new Edge(idC, idB, 4), new Edge(idB, idE, 5)}),
                new Path(new List<Edge> {new Edge(idA, idB, 0), new Edge(idB, idC, 3), new Edge(idC, idE, 6)}),

                new Path(new List<Edge> {new Edge(idA, idD, 2), new Edge(idD, idC, 7), new Edge(idC, idB, 4), new Edge(idB, idE, 5)}),
                new Path(new List<Edge> {new Edge(idA, idB, 0), new Edge(idB, idC, 3), new Edge(idC, idB, 4), new Edge(idB, idE, 5)}),
                new Path(new List<Edge> {new Edge(idA, idC, 1), new Edge(idC, idB, 4), new Edge(idB, idC, 3), new Edge(idC, idE, 6)}),

                new Path(new List<Edge> {new Edge(idA, idD, 2), new Edge(idD, idC, 7), new Edge(idC, idB, 4), new Edge(idB, idC, 3), new Edge(idC, idE, 6)}),
                new Path(new List<Edge> {new Edge(idA, idB, 0), new Edge(idB, idC, 3), new Edge(idC, idB, 4), new Edge(idB, idC, 3), new Edge(idC, idE, 6)}),
                new Path(new List<Edge> {new Edge(idA, idC, 1), new Edge(idC, idB, 4), new Edge(idB, idC, 3), new Edge(idC, idB, 4), new Edge(idB, idE, 5)}),
            };


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
