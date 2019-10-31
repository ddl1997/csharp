using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm.Graph.Utils.Comparer
{
    public class EdgeComparer : IEqualityComparer<Edge>
    {
        public static EdgeComparer Default = new EdgeComparer();
        public bool Equals(Edge x, Edge y)
        {
            return Vertex.Equals(x, y.Id);
        }

        public int GetHashCode(Edge obj)
        {
            return obj.GetHashCode();
        }
    }
}