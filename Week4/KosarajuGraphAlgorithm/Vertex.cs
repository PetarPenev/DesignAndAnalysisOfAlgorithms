using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KosarajuGraphAlgorithm
{
    public class Vertex
    {
        public HashSet<int> OutgoingVertices { get; set; }

        public HashSet<int> IngoingVertices { get; set; }

        public int ComponentNumber { get; set; }

        public Vertex(int number)
        {
            OutgoingVertices = new HashSet<int>();
            IngoingVertices = new HashSet<int>();
            ComponentNumber = number;
        }
    }
}
