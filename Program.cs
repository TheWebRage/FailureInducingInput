using QuickSortTests;
using System;
using System.Collections.Generic;

namespace FailureInducingInput
{
    class Program
    {
        static public List<List<int>> GetDividedSections(int sizeOfArrays, List<int> array)
        {
            List<List<int>> result = new List<List<int>>();

            for (int i = 0; i < array.Count; i += sizeOfArrays)
            {
                List<int> newSection = new List<int>();

                for (int j = i; j < i + sizeOfArrays; j++)
                {
                    if (array.Count <= j)
                    {
                        break;
                    }
                    newSection.Add(array[j]);
                }
                result.Add(newSection);
            }

            return result;
        }

        static void Main(string[] args)
        {
            // Hard coded failing test input
            List<int> array = new List<int>(){ 72, 12, 6, 20, 81, 97, 37, 59, 52, 1, 20 };

            // Amount of items to split the groups into
            int sizeOfArrays = 2;

            // Divides the array into equal sized sections
            List<List<int>> sectionedArray = GetDividedSections(sizeOfArrays, array);

            // Test the sections for failing input
            foreach (List<int> arrSection in sectionedArray)
            {
                bool isSuccessful = QuickSortTest.NoDuplicateEntries(arrSection.ToArray());

                if (!isSuccessful)
                {
                    // Section off failing input and try again
                }
            }

            // Test the compliments of the sections for failing input
            for (int i = 0; i < array.Count; i++)
            {
                // Get the array section compliment


            }

        }
    }
}
