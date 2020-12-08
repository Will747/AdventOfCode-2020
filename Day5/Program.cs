using System;
using System.Collections.Generic;
using System.IO;

namespace Day5
{
    internal static class Program
    {
        private static int GetId(string input) 
        {
            var upper = 127;
            var lower = 0;
			
            //rows
            for (var a = 0; a <= 6; a++)
            {
                if(input.Substring(a, 1) == "F")
                {
                    upper = (int)Math.Floor(Convert.ToDecimal(upper + lower) / 2);
                } else {
                    lower = (int)Math.Ceiling(Convert.ToDecimal(upper + lower) / 2);
                }
            }
			
			
            var upperCol = 7;
            var lowerCol = 0;
			
            //columns
            for (var a = 7; a<= 9; a++)
            {
                if(input.Substring(a, 1) == "L")
                {
                    upperCol = (int)Math.Floor(Convert.ToDecimal(upperCol + lowerCol) / 2);
                } else {
                    lowerCol = (int)Math.Ceiling(Convert.ToDecimal(upperCol + lowerCol) / 2);
                }
            }

            return lower * 8 + lowerCol;
        }
	
        private static int GetMissingSeat(List<string> input)
        {
            var ids = new List<int>();
		
            for(var i = 0; i <= input.Count - 1; i++)
            {
                ids.Add(GetId(input[i]));
            }
		
            ids.Sort();
		
            for (var a = 0; a <= ids.Count - 2; a++)
            {
                if(ids[a] != ids[a + 1] - 1) {
                    return ids[a] + 1;
                }
            }
		
            return -1;
        }
	
        private static int HighestSeatId(List<string> values) 
        {
            var highest = 0;
		
            for(var i = 0; i<= values.Count - 1; i++)
            {
                var id = GetId(values[i]);
                if (id > highest) 
                {
                    highest = id;
                }
            }
		
            return highest;
        }

        
        static void Main(string[] args)
        {
            var lines = new List<string>();
            using (var reader = File.OpenText("input.txt"))
            {
                string s;
                while ((s = reader.ReadLine()) != null)
                {
                    lines.Add(s);
                }
            }
            
            Console.WriteLine("Part 1: " + HighestSeatId(lines));
            Console.WriteLine("Part 2: " + GetMissingSeat(lines));
            Console.ReadKey();
        }
    }
}