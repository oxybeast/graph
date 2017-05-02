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
        public void EmptyGraph_Zero()//чяднт?
        {
            var g = new Graph();
            Assert.Equal(0, g.Count);
        }

        [Fact]
        public void SomeGraph_EmptyListForNonExistVertex()
        {
            var g = new Graph();
            Assert.Equal(new List<Edge>(), g.GetEdgesByVertexId(15));
        }

        [Fact]
        public void AddedVertex_IdNotEqualsNegOne()
        {
            var vertex = new VertexOfTable("sample", new List<string>());
            var graph = new Graph();
            graph.AddVertexIfNonExist(vertex);
            var flag = -1 != graph.GetIdOfVertex(vertex);
            Assert.Equal(true, flag);
        }

        [Fact]

        public void VertexNonAdded_ReturnNegOne()
        {
            var vertex = new VertexOfTable("sample", new List<string>());
            var graph = new Graph();
            var flag = -1 != graph.GetIdOfVertex(vertex);
            Assert.Equal(false, flag);
        }

        [Fact]

        public void VertexAddedTwice_CountOnlyOnce()
        {
            var vertex = new VertexOfTable("sample", new List<string>());
            var graph = new Graph();
            graph.AddVertexIfNonExist(vertex);
            graph.AddVertexIfNonExist(vertex);
            Assert.Equal(1, graph.Count);
        }

        [Fact]
        public void VertexAdded_ReturnTrueName()
        {
            var vertex1 = new VertexOfTable("sample", new List<string>());
            var vertex2 = new VertexOfTable("sample1", new List<string>());
            var graph = new Graph();
            graph.AddVertexIfNonExist(vertex1);
            var id = graph.GetIdOfVertex(vertex1.GetName());
            Assert.Equal(vertex1.GetName(), graph.GetNameOfTableById(id));
        }

        [Fact]

        public void NoEdgeForVertex_ReturnEmptyList()
        {
            var vertex1 = new VertexOfTable("sample", new List<string>());
            var vertex2 = new VertexOfTable("sample1", new List<string>());
            var graph = new Graph();
            graph.AddVertexIfNonExist(vertex1);
            graph.AddVertexIfNonExist(vertex2);
            Assert.Equal(new List<Edge> (), graph.GetEdgesByVertexId(graph.GetIdOfVertex(vertex1.GetName())));
        }

        [Fact]

        public void AddedEdge_ReturnEmptyListForEndOfTheEdge()
        {
            var vertex1 = new VertexOfTable("sample", new List<string>());
            var vertex2 = new VertexOfTable("sample1", new List<string>());
            var graph = new Graph();
            graph.AddVertexIfNonExist(vertex1);
            graph.AddVertexIfNonExist(vertex2);
            graph.AddEdgeIfVertexExist("sample", "sample1", 0);
            Assert.Equal(new List<Edge> (), graph.GetEdgesByVertexId(graph.GetIdOfVertex("sample1")));
        }

        [Fact]

        public void Added2EdgesForOneVertex_ReturnBoth()
        {
            var vertex1 = new VertexOfTable("sample", new List<string>());
            var vertex2 = new VertexOfTable("sample1", new List<string>());
            var graph = new Graph();
            graph.AddVertexIfNonExist(vertex1);
            graph.AddVertexIfNonExist(vertex2);
            graph.AddEdgeIfVertexExist("sample", "sample1", 0);
            graph.AddEdgeIfVertexExist("sample", "sample1", 1);
            var id1 = graph.GetIdOfVertex("sample");
            var id2 = graph.GetIdOfVertex("sample1");
            var outputList = new List<Edge>(graph.GetEdgesByVertexId( graph.GetIdOfVertex("sample")));
            var goodList = new List<Edge>{new Edge(id1, id2, 0), new Edge(id1, id2, 1) };
            var comp = new CompareSomething();
            goodList.Sort(comp.CompareEdges);
            outputList.Sort(comp.CompareEdges);
            var flag = comp.CompareListEdges(outputList, goodList) == 0;
            Assert.Equal(true, flag);
        }
    }
}
