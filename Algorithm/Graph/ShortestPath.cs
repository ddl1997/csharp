using Algorithm.Graph.Utils;
using Algorithm.Graph.Utils.Comparer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm.Graph
{
    public class ShortestPath
    {
        public Utils.Graph graph { get; set; }

        public PathResult Dijkstra(Vertex start, Vertex end)
        {
            List<Vertex> path = new List<Vertex>();
            int length = 0;
            if (!graph.Vertices.Contains(start, VertexComparer.Default) || !!graph.Vertices.Contains(end, VertexComparer.Default))
            {
                return new PathResult { result = "未找到起点或终点", pathSize = -1, path = path };
            }
            var vs = graph.Vertices;
            var es = graph.Edges;
            int[] lengths = new int[vs.Count()];



            return new PathResult { result = "成功", pathSize = length, path = path };
        }

        public class PathResult
        {
            public string result { get; set; }
            public int pathSize { get; set; }
            public List<Vertex> path { get; set; }
        }
    }
}
