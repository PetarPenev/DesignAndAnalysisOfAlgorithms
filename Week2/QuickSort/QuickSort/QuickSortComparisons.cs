using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    public class QuickSortComparisons
    {
        public const string inputFilePath = @"D:\algorithms\Week2\IntegerArray.txt";

        public const int inputArraySize = 10000;

        public static void Main()
        {
            var stream = new StreamReader(inputFilePath);
            var inputNumbersList = new int[inputArraySize];
            string line = stream.ReadLine();
            int counter = 0;

            while (line != null)
            {
                inputNumbersList[counter++] = int.Parse(line);
                line = stream.ReadLine();
            }

            var routineComparisonNumbers = QuickSort(inputNumbersList, AlwaysChooseFirst, 0, inputNumbersList.Length - 1);
            //var routineComparisonNumbers = QuickSort(inputNumbersList, AlwaysChooseLast, 0, inputNumbersList.Length - 1);
            //var routineComparisonNumbers = QuickSort(inputNumbersList, AlwaysChooseMedianOfThree, 0, inputNumbersList.Length - 1);
            Console.WriteLine("The number of swaps in the current routine is: {0}", routineComparisonNumbers);
        }

        public static int QuickSort(int[] array, Action<int[], int, int> pivotChooser, int startPos, int endPos)
        {
            if (startPos > endPos)
            {
                return 0;
                //throw new ArgumentOutOfRangeException("Error in algorithm position computation");
            }

            if (startPos == endPos)
            {
                return 0;
            }

            pivotChooser(array, startPos, endPos);
            var dividerPosition = startPos;

            for (int i = startPos + 1; i <= endPos; i++)
			{
			    if (array[startPos] > array[i])
                {
                    dividerPosition++;
                    if (dividerPosition != i)
                    {
                        SwapElements(array, dividerPosition, i);
                    }
                }
			}

            if (startPos != dividerPosition)
            {
                SwapElements(array, startPos, dividerPosition);
            }

            return QuickSort(array, pivotChooser, startPos, dividerPosition - 1) + 
                QuickSort(array, pivotChooser, dividerPosition + 1, endPos) + endPos - startPos;
        }

        public static void AlwaysChooseFirst(int[] array, int startPos, int endPos)
        {
        }

        public static void AlwaysChooseLast(int[] array, int startPos, int endPos)
        {
            SwapElements(array, startPos, endPos);
        }

        public static void AlwaysChooseMedianOfThree(int[] array, int startPos, int endPos)
        {
            var medianPosition = startPos + (endPos - startPos) / 2;
            var first = array[startPos];
            var median = array[medianPosition];
            var last = array[endPos];

            if (first > median)
            {
                if (median > last)
                {
                    SwapElements(array, startPos, medianPosition);
                }
                else
                {
                    if (first > last)
                    {
                        SwapElements(array, startPos, endPos);
                    }
                }
            }
            else
            {
                if (last > median)
                {
                    SwapElements(array, startPos, medianPosition);
                }
                else if (last > first)
                {
                    SwapElements(array, startPos, endPos);
                }
            }
        }

        public static void SwapElements(int[] array, int startPos, int endPos)
        {
            array[startPos] += array[endPos];
            array[endPos] = array[startPos] - array[endPos];
            array[startPos] -= array[endPos];
        }
    }
}
