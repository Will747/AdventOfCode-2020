using System;
using System.Collections.Generic;
using System.IO;

namespace Day10
{
    internal static class Program
    {

        private static long Part2(List<int> input, long[] cache, int value = 0, int index = 0)
        {
            if (input[input.Count - 1] == value) return 1;
            
            long count = 0;
            
            for (var i = index; i < input.Count; i++)
            {
                var difference = input[i] - value;
                if (difference <= 3 && difference > 0)
                {
                    if (cache[i] == 0)
                    {
                        cache[i] = Part2(input, cache, input[i],i);
                    }
                    count += cache[i];
                }
            }
            return count;
           
        }
        
        private static double Part1(List<int> input)
        { 
           var jolts = 0;
           var complete = new bool[input.Count];
           var count1 = 1;
           var count3 = 1;
           while (jolts <= input[input.Count - 1] - 1)
           {
               for (var i = 1; i < input.Count; i++)
               {
                   if (input[i] - jolts <= 3 && input[i] - jolts > 0 && complete[i] == false)
                   {
                       if (input[i] - jolts <= 3)
                       {
                           if (input[i] - jolts == 3)
                           {
                               count3++;
                           }
                           else
                           {
                               count1++;
                           }
                           
                           jolts += input[i] - jolts;
                           complete[i] = true;
                       }
                       
                   }
               }
           }

           return count1 * count3;
        }
        
        static void Main(string[] args)
        {
            var lines = new List<int>();
            using (var reader = File.OpenText("input.txt"))
            {
                string s;
                while ((s = reader.ReadLine()) != null)
                {
                    lines.Add(Convert.ToInt32(s));
                }
            }
            lines.Sort();

            Console.WriteLine("Part 1: " + Part1(lines));
            Console.WriteLine("Part 2: " + Part2(lines, new long[lines.Count]));
            Console.ReadKey();
        }
    }
}