using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KargerAlgorithm
{
    public class KargerLauncher
    {
        public static int NumberOfIterations = 100000;

        public static int inputElements = 200;

        public static int MinCuts = int.MaxValue;

        public const string inputFilePath = @"D:\algorithms\Week3\GraphInput.txt";

        public static void Main()
        {
            var inputNodeList = new List<Vertex>(inputElements);

            var stream = new StreamReader(inputFilePath);
            string line = stream.ReadLine();
            while (line != null)
            {
                var lineInput = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                var newVertex = new Vertex
                {
                    Edges = new List<int>(lineInput.Length - 1)
                };

                for (int i = 1; i < lineInput.Length; i++)
                {
                    newVertex.Edges.Add(int.Parse(lineInput[i]) -1);
                }

                inputNodeList.Add(newVertex);
                line = stream.ReadLine();
            }

            for (int i = 0; i < NumberOfIterations; i++)
            {
                Console.WriteLine("Iteration {0}", i);
                var intermediateInputNodeList = inputNodeList.ConvertAll(b => new Vertex {  
                    Edges = new List<int>(b.Edges) });
                var current = Karger(intermediateInputNodeList);

                if (current < MinCuts)
                {
                    MinCuts = current;
                }
            }

            Console.WriteLine("Min Cuts: {0}", MinCuts);
            Console.ReadLine();
        }

        private static int Karger(List<Vertex> intermediateInputNodeList)
        {
            while (intermediateInputNodeList.Count > 2)
            {
                ReduceEdge(intermediateInputNodeList);
            }

            return intermediateInputNodeList.First().Edges.Count;
        }

        private static void ReduceEdge(List<Vertex> intermediateInputNodeList)
        {
            var edges = ChooseRandomEdge(intermediateInputNodeList);
            intermediateInputNodeList[edges.Item1].Edges.AddRange(intermediateInputNodeList[edges.Item2].Edges);
            for (int i = 0; i < intermediateInputNodeList.Count; i++)
            {
                for (int j = 0; j < intermediateInputNodeList[i].Edges.Count; j++)
                {
                    if (intermediateInputNodeList[i].Edges[j] == edges.Item2)
                    {
                        intermediateInputNodeList[i].Edges[j] = edges.Item1;
                    }
                }
            }

            intermediateInputNodeList[edges.Item1].Edges.RemoveAll(c => c == edges.Item1);

            intermediateInputNodeList.RemoveAt(edges.Item2);

            for (int i = 0; i < intermediateInputNodeList.Count; i++)
            {
                for (int j = 0; j < intermediateInputNodeList[i].Edges.Count; j++)
                {
                    if (intermediateInputNodeList[i].Edges[j] >= edges.Item2)
                    {
                        intermediateInputNodeList[i].Edges[j]--;
                    }
                }
            }
        }

        private static Tuple<int, int> ChooseRandomEdge(List<Vertex> intermediateInputNodeList)
        {
            var generator = new Random();
            var firstPos = generator.Next(0, intermediateInputNodeList.Count);
            var secondPos = intermediateInputNodeList[firstPos].
                Edges[generator.Next(0, intermediateInputNodeList[firstPos].Edges.Count)];

            return new Tuple<int, int>(firstPos, secondPos);
        }
    }
}
