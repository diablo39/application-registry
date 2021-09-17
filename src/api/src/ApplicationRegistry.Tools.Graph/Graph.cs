using System;
using System.Collections.Generic;

namespace ApplicationRegistry.Tools.Graph
{
    public class Graph
    {
        public Dictionary<string, Node> Nodes { get; set; }

        public Dictionary<string, List<string>> Edges { get; set; }

        public Graph()
        {
            Nodes = new Dictionary<string, Node>();
            Edges = new Dictionary<string, List<string>>();
        }

        public Graph GetGraphConnectedToNodes(params Node[] nodes)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException(nameof(nodes));
            }

            var result = new Graph();

            ResetNodesVisitedState();

            var stack = new Stack<Node>();

            for (int i = 0; i < nodes.Length; i++)
            {
                stack.Push(nodes[i]);
            }

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (node.IsVisited) continue;

                node.IsVisited = true;
                result.AddNode(node);
                var edges = Edges[node.Id];

                for (int i = 0; i < edges.Count; i++)
                {
                    var edge = edges[i];
                    result.AddEdge(node.Id, edge);

                    var adjacentNode = Nodes[edge];
                    if (!adjacentNode.IsVisited)
                        stack.Push(adjacentNode);
                }
            }

            return result;
        }

        public void AddNode(Node node)
        {
            if (!Nodes.ContainsKey(node.Id))
            {
                Nodes.Add(node.Id, node);
                Edges.Add(node.Id, new List<string>());
            }
        }

        public void AddEdge(string from, string to)
        {
            Edges[from].Add(to);
        }

        public void AddEdges(string from, IEnumerable<string> to)
        {
            Edges[from].AddRange(to);
        }

        private void ResetNodesVisitedState()
        {
            foreach (var node in Nodes.Values)
            {
                node.IsVisited = false;
            }
        }
    }
}
