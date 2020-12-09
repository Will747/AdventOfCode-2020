using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode
{
    internal static class Program
    {
        private static int GetNum2(List<int> numbers)
        {
            for (var i = 0; i <= numbers.Count - 1; i++)
            {
                for (var a = 0; a <= numbers.Count - 1; a++)
                {
                    for (var b = 0; b <= numbers.Count - 1; b++)
                    {
                        if (numbers[i] + numbers[a] + numbers[b] == 2020 && a != i && b != a && i != b)
                        {
                            return numbers[i] * numbers[a] * numbers[b];
                        }
                    }
                }
            }

            return -1;
        }

        private static int GetNum(List<int> numbers)
        {
            for (var i = 0; i <= numbers.Count - 1; i++)
            {
                for (var a = 0; a <= numbers.Count - 1; a++)
                {
                    if (numbers[i] + numbers[a] == 2020 && a != i)
                    {
                        return numbers[i] * numbers[a];
                    }
                }
            }

            return -1;
        }
        
        static void Main(string[] args)
        {
            var numbers = new List<int>();
            using (var reader = File.OpenText("input.txt"))
            {
                string s;
                while ((s = reader.ReadLine()) != null)
                {
                    numbers.Add(Convert.ToInt32(s));
                }
            }
            
            Console.WriteLine("Part 1: " + GetNum(numbers));
            Console.WriteLine("Part 2: " + GetNum2(numbers));
        }
    }
}