using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm.Graph.Utils.Comparer
{
    public class VertexComparer : IEqualityComparer<Vertex>
    {
        public static VertexComparer Default = new VertexComparer();
        public bool Equals(Vertex x, Vertex y)
        {
            return string.Equals(x.Id, y.Id);
        }

        public int GetHashCode(Vertex obj)
        {
            return obj.GetHashCode();
        }
    }
}
