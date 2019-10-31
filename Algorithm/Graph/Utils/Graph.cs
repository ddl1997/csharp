using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm.Graph.Utils
{
    public class Graph
    {
        public List<Vertex> Vertices { get; set; }
        public List<Edge> Edges { get; set; }
    }

    public class Vertex
    {
        public string Id { get; set; }
    }

    public class Edge
    {
        public int Weight { get; set; }
        public Vertex Start { get; set; }
        public Vertex End { get; set; }
    }
}
