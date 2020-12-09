using System;
using System.Collections.Generic;
using System.IO;

namespace Day8
{
    internal static class Program
    {
        private static int Run(List<string> input) 
        {
            var acc =  0;
            var visited = new bool[input.Count];

            for (var i = 0; i <= input.Count - 1; i++)
            {
                if (visited[i])
                {
                    return -1;	
                }
				
                switch(input[i].Substring(0, 3))
                {
                    case "acc":
                        acc += Convert.ToInt32(input[i].Substring(4));
                        break;
						   
                    case "jmp":
                        i += Convert.ToInt32(input[i].Substring(4)) - 1;
                        break;
						   
                    default:
                        break;
                }
                visited[i] = true;
            }
			
            return acc;
        }

        private static int Part2(List<string> input) 
        {
            for (var i = 0; i <= input.Count - 1; i++)
            {
                var original = input[i];
                switch(input[i].Substring(0, 3))
                {
                    case "jmp":
                        input[i] = "nop " + input[i].Substring(4);
                        break;
						   
                    case "nop":
                        input[i] = "jmp " + input[i].Substring(4);
                        break;
						   
                    default:
                        break;
                }
                var result = Run(input);
                if (result != -1)
                {
                    return result;	
                } else {
                    input[i] = original;	
                }
            }
			
            return -1;
        }

        private static int Part1(List<string> input) 
        {
            var acc =  0;
            var visited = new bool[input.Count];

            for (var i = 0; i <= input.Count - 1; i++)
            {
                if (visited[i])
                {
                    return acc;	
                }
				
                switch(input[i].Substring(0, 3))
                {
                    case "acc":
                        acc += Convert.ToInt32(input[i].Substring(4));
                        break;
						   
                    case "jmp":
                        i += Convert.ToInt32(input[i].Substring(4)) - 1;
                        break;
						   
                    default:
                        break;
                }
                visited[i] = true;
            }
			
            return acc;
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
            
            Console.WriteLine("Part 1: " + Part1(lines));
            Console.WriteLine("Part 2: " + Part2(lines));
            Console.ReadKey();
        }
    }
}