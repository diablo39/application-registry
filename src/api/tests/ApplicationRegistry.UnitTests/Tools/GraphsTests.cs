using ApplicationRegistry.Tools.Graph;
using System;
using System.Linq;
using Xunit;

namespace ApplicationRegistry.UnitTests
{
    public class GraphsTests
    {
        [Fact]
        public void NoAdjecentsNodes()
        {
            var graph = new Graph();

            graph.AddNode(new Node("1"));
            graph.AddNode(new Node("2"));
            graph.AddNode(new Node("3"));
            var node4 = new Node("4");

            graph.AddNode(node4);

            graph.AddEdges("1", new[] { "2", "3" });

            graph.AddEdges("2", new[] { "3", "4" });


            var goutput = graph.GetGraphConnectedToNodes(node4);

            Assert.Contains<Node>(node4, goutput.Nodes.Values);
            Assert.True(goutput.Nodes.Count == 1);
            var edges = goutput.Edges.SelectMany(e => e.Value);

            Assert.True(edges.Count() == 0);
        }
    }
}
