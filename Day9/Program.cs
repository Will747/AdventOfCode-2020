using System;
using System.Collections.Generic;
using System.IO;

namespace Day9
{
    internal static class Program
    {
        private static bool IsSum(List<double> input, int index)
        {
            for (var i = index - 25; i < index; i++)
            {
                for (var a = index - 25; a < index; a++)
                {
                    if (input[i] + input[a] == input[index])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static double AddRange(List<double> input, int start, int end)
        {
            if (start == end) return 0;
            return input[start] + AddRange(input, start + 1, end);
        }
        
        private static double Part2(List<double> input)
        {
            var invalidNum = Part1(input);

            for (var i = 0; i < input.Count; i++)
            {
                //Search for a range of up to 20 digits
                //This may need changing depending on the input
                for (var a = 0; a < 20; a++)
                {
                    var total = AddRange(input, i, i + a);
                    if (total == invalidNum)
                    {
                        var contiguousSet = input.GetRange(i, a);
                        contiguousSet.Sort();
                        return contiguousSet[0] + contiguousSet[contiguousSet.Count - 1];
                    }
                }
            }
            
            return -1;
        }
        
        private static double Part1(List<double> input)
        {
            for (var i = 25; i < input.Count; i++)
            {
                if (!IsSum(input, i))
                {
                    return input[i];
                }
                
            }
            return -1;
        }
        
        static void Main(string[] args)
        {
            var lines = new List<double>();
            using (var reader = File.OpenText("input1.txt"))
            {
                string s;
                while ((s = reader.ReadLine()) != null)
                {
                    lines.Add(Convert.ToInt64(s));
                }
            }
            
            Console.WriteLine("Part 1: " + Part1(lines));
            Console.WriteLine("Part 2: " + Part2(lines));
            Console.ReadKey();
        }
    }
}