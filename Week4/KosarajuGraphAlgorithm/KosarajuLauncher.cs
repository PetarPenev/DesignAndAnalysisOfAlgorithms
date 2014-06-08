using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KosarajuGraphAlgorithm
{
    public class KosarajuLauncher
    {
        public const string InputFilePath = @"D:\algorithms\Week4\SCC.txt";

        public const int VerticesNumber = 875715;

        public static int GlobalCounter = 0;

        public static void Main()
        {
            var vertices = new Vertex[VerticesNumber];

            var rankings = new List<RankingInfo>(VerticesNumber - 1);

            var stream = new StreamReader(InputFilePath);
            string line = stream.ReadLine();

            vertices[0] = new Vertex(-1000);

            for (int i = 1; i < VerticesNumber; i++)
            {
                vertices[i] = new Vertex(i);
            }

            Console.WriteLine("Finished initializing");

            while (line != null)
            {
                var splittedStrings = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var outgoing = int.Parse(splittedStrings[0]);
                var ingoing = int.Parse(splittedStrings[1]);

                vertices[outgoing].OutgoingVertices.Add(ingoing);
                vertices[ingoing].IngoingVertices.Add(outgoing);

                line = stream.ReadLine();
            }

            ReverseDfsLoop(vertices, rankings);

            rankings = rankings.OrderByDescending(c => c.FinishTime).ToList();

            DfsLoop(vertices, rankings);

            var grouping = vertices.GroupBy(v => v.ComponentNumber).OrderByDescending(v => v.ToList().Count).ToList();

            for (int i = 0; i < 5; i++)
            {
                var gr = grouping[i].ToList();
                Console.WriteLine(gr.Count);
            }
        }

        private static void DfsLoop(Vertex[] vertices, List<RankingInfo> rankings)
        {
            var isVisited = new bool[VerticesNumber];
            foreach (var rank in rankings)
            {
                if (!isVisited[rank.Position])
                {
                    ReverseDfs(vertices, rankings, isVisited, rank.Position, true);
                }
            }
        }

        private static void ReverseDfsLoop(Vertex[] vertices, List<RankingInfo> rankings)
        {
            var isVisited = new bool[VerticesNumber];
            for (int i = 1; i < vertices.Length; i++)
            {
                if (!isVisited[i])
                {
                    ReverseDfs(vertices, rankings, isVisited, i);
                }
            }
        }

        private static void ReverseDfs(Vertex[] vertices, List<RankingInfo> rankings, bool[] isVisited, 
            int number, bool isSecondLoop = false)
        {
            var traversalStack = new Stack<int>();
            traversalStack.Push(number);

            do
            {
                var currentNode = traversalStack.Pop();

                if (!isVisited[currentNode])
                {
                    if (isSecondLoop)
                    {
                        vertices[currentNode].ComponentNumber = number;
                    }

                    isVisited[currentNode] = true;
                    var collection = isSecondLoop ? 
                        vertices[currentNode].OutgoingVertices : vertices[currentNode].IngoingVertices;

                    foreach (var item in collection)
                    {
                        if (!isVisited[item])
                        {
                            traversalStack.Push(item);
                        }
                    }

                    GlobalCounter++;
                    if (!isSecondLoop)
                    {
                        rankings.Add(new RankingInfo { FinishTime = GlobalCounter, Position = currentNode });
                    }
                }
                
            } while (traversalStack.Count > 0);      
        }
    }
}
