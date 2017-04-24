using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        public void SomeGraph_ThrowsExceptionForNonExistVertex()
        {
            var g = new Graph();
            Assert.Throws<IndexOutOfRangeException > (() => g.GetEdgesByVertexId(5));
        }
    }
}
