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

        static public List<int> GetSectionCompliment(List<List<int>> array, int sectionIndex)
        {
            List<int> compliment = new List<int>();

            foreach (List<int> section in array)
            {
                if (array.IndexOf(section) == sectionIndex)
                {
                    continue;
                }

                compliment.AddRange(section);
            }

            return compliment;
        }

        static public bool IsTestSuccessful(List<int> array)
        {
            try
            {
                QuickSortTest.NoDuplicateEntries(array.ToArray());
            }
            catch
            {
                return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            // Hard coded failing test input
            List<int> array = new List<int>(){ 20, 72, 12, 6, 81, 97, 37, 59, 52, 1, 20 };

            // Amount of items to split the groups into
            int sizeOfArrays = 2;

            // Loop until found smallest input
            while (true)
            {
                bool isSuccessful = true;

                // Divides the array into equal sized sections
                List<List<int>> sectionedArray = GetDividedSections(sizeOfArrays, array);

                // Test the sections for failing input
                foreach (List<int> arrSection in sectionedArray)
                {
                    isSuccessful = IsTestSuccessful(arrSection);

                    if (!isSuccessful)
                    {
                        // Section off failing input and try again
                        array = arrSection;
                        sizeOfArrays = 2;

                        break;
                    }

                }

                if (!isSuccessful)
                {
                    continue;
                }

                // Test the compliments of the sections for failing input
                foreach (List<int> arrSection in sectionedArray)
                {
                    List<int> compliment = GetSectionCompliment(sectionedArray, sectionedArray.IndexOf(arrSection));
                    isSuccessful = IsTestSuccessful(compliment);

                    if (!isSuccessful)
                    {
                        // Section off failing input and try again
                        array = compliment;

                        // n = max(n-1, 2)
                        sizeOfArrays = Math.Max(sizeOfArrays - 1, 2);

                        break;
                    }

                }

                if (!isSuccessful)
                {
                    continue;
                }

                // If all previous inputs pass, increase granularity, create more equal parts
                // array = array;
                sizeOfArrays = Math.Min(2 * sizeOfArrays, array.Count);

                if (sizeOfArrays <= array.Count)
                {
                    //return array;
                    break;
                }

                continue;

            }

            Console.WriteLine(array);

        }
    }
}
