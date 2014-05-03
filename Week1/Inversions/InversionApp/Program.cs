using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InversionApp
{
    public class Program
    {
        public const string inputFilePath = @"D:\algorithms\Week1\IntegerArray.txt";

        public static void Main()
        {
            var stream = new StreamReader(inputFilePath);
            var inputNumbersList = new List<int>();
            string line = stream.ReadLine();

            while (line != null)
            {
                inputNumbersList.Add(int.Parse(line));
                line = stream.ReadLine();
            }

            var inputNumbersArray = inputNumbersList.ToArray();
            //var inputNumbersList = new int[6] { 1, 2, 3, 4, 5, 6 };
            var numberOfInversions = SortAndCount(inputNumbersArray);
            Console.WriteLine("The inversions in the array are {0}.", numberOfInversions.Item1);
        }

        public static Tuple<long, int[]> SortAndCount(int[] array)
        {
            if (array.Length == 1)
            {
                return new Tuple<long, int[]>(0, array);
            }

            var firstHalfArray = new int[array.Length / 2];
            Array.Copy(array, 0, firstHalfArray, 0, firstHalfArray.Length);

            var secondHalfArray = new int[array.Length - array.Length / 2];
            Array.Copy(array, firstHalfArray.Length, secondHalfArray, 0, secondHalfArray.Length);

            var firstHalfInversions = SortAndCount(firstHalfArray);
            var secondHalfInversions = SortAndCount(secondHalfArray);
            var splitInversions = MergeAndCountSplitInversions(firstHalfArray, secondHalfArray);

            Array.Sort(array);
            return new Tuple<long, int[]>(firstHalfInversions.Item1 + secondHalfInversions.Item1 + splitInversions, array);
        }

        public static long MergeAndCountSplitInversions(int[] firstHalfArray, int[] secondHalfArray)
        {
            var firstArrayCounter = 0;
            var secondArrayCounter = 0;
            long numberOfInversions = 0;
            bool isFinished = false;

            while (!isFinished)
            {
                if ((firstArrayCounter >= firstHalfArray.Length) || (secondArrayCounter >= secondHalfArray.Length))
                {
                    isFinished = true;
                }
                else if (secondHalfArray[secondArrayCounter] < firstHalfArray[firstArrayCounter])
                {
                    numberOfInversions += firstHalfArray.Length - firstArrayCounter;
                    secondArrayCounter++;
                }
                else
                {
                    firstArrayCounter++;
                }
            }

            return numberOfInversions;
        }
    }
}
