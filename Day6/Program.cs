using System;
using System.Collections.Generic;
using System.IO;

namespace Day6
{
    internal static class Program
    {
        private static int EveryoneSum(List<string> input) 
        {
            var count = 0;
            var newGroup = true;
            //Seperate into groups
            var questions = new List<string>();
            for (var i = 0; i <= input.Count - 1; i++)
            {
                if (input[i] == "") 
                {
                    count += questions.Count;
                    questions = new List<string>();
                    newGroup = true;
                } else if (newGroup) {
                    for (var a = 0; a <= input[i].Length - 1; a++) 
                    {
                        questions.Add(input[i].Substring(a, 1));	
                    }
                    newGroup = false;
                } else {
                    var removeQ = new List<string>();
                    for (var b = 0; b <= questions.Count - 1; b++) 
                    {
                        if (!input[i].Contains(questions[b]))
                        {
                            removeQ.Add(questions[b]);
                        }	
                    }
					
                    for (var c = 0; c <= removeQ.Count - 1; c++)
                    {
                        questions.Remove(removeQ[c]);
                    }
                }
            }
			
            return count + questions.Count;
        }

        private static int AnyoneSum(List<string> input) 
        {
            var count = 0;
			
            //Seperate into groups
            var questions = new List<string>();
            for (var i = 0; i <= input.Count - 1; i++)
            {
                if (input[i] == "") 
                {
                    count += questions.Count;
                    questions = new List<string>();
                } else {
                    for (var a = 0; a <= input[i].Length - 1; a++) 
                    {
                        if (!questions.Contains(input[i].Substring(a, 1)))
                        {
                            questions.Add(input[i].Substring(a, 1));
                        }
                    }	
                }
            }
			
            return count + questions.Count;
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
            
            Console.WriteLine("Part 1: " + AnyoneSum(lines));
            Console.WriteLine("Part 2: " + EveryoneSum(lines));
            Console.ReadKey();
        }
    }
}