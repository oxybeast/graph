using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using Assert = Xunit.Assert;

namespace GraphSolver.UnitTests
{
    public class GraphUnitTests
    {
        [Fact]
        public void EmptyGraph_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Graph(""));
            //Assert.Same(0, graph.Count);
        }
        [Fact]
        public void EmptyGraph_Zero()//чяднт?
        {
            var g = new Graph();
            Assert.Equal(0, g.Count);
            //Assert.Same((int)0, g.Count);
        }

        [Fact]
        public void EmptyListForEdge_EmptyList()
        {
            var a = new List<Edge>[3];
            var g = new Graph(a);
            Assert.Equal(new List<Edge>(), g.GetEdgesByVertexId(0));
        }

        [Fact]
        public void SomeGraph_EmptyListForNonExistVertex()
        {
            var g = new Graph();
            Assert.Equal(new List<Edge>(), g.GetEdgesByVertexId(15));
        }

        [Fact]
        public void AddEdgeWithNonPositiveVertex_NoChanges()
        {
            var g = new Graph();
            g.AddEdge(new Edge(-1, 2, 3));
            Assert.Equal(0, g.Count);
        }

        [Fact]
        public void AddEdge1_ThisEdgeExistInGraph()
        {
            var g = new Graph();
            var edge = new Edge(0,1,2);
            g.AddEdge(edge);
            var flag = g.GetEdgesByVertexId(0).Contains(edge);
            Assert.Equal(true, flag);
        }
        [Fact]
        public void AddEdge2_ThisEdgeExistInGraph()
        {
            var g = new Graph();
            g.AddEdge(new Edge(0,1,2));
            g.AddEdge(new Edge(0,2,4));
            g.AddEdge(new Edge(1,2,3));
            var edge = new Edge(0, 1, 2);
            var flag = false;
            foreach (var currentEdge in g.GetEdgesByVertexId(0))
            {
                var comparator = new CompareSomething();
                flag|=comparator.CompareEdges(currentEdge, edge) == 0;
            }
            Assert.Equal(true, flag);
        }
    }
}
