using System;
using System.Collections.Generic;
using System.IO;

namespace Day3
{
    internal static class Program
    {
        static int Trees(string[,] grid, int right, int down)
        {
            var treeCount = 0;
            var x = 0;
            var y = 0;

            while (y != grid.GetLength(0) - 1)
            {
                x += right;
                if (x > grid.GetLength(1) - 1)
                {
                    x -= grid.GetLength(1);
                }

                y += down;

                if (grid[y, x] == "#")
                {
                    treeCount++;
                }
            }
            
            return treeCount;
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

            //Converts the file into a 2D array for each character
            var grid = new string[lines.Count, lines[0].Length];

            for (var i = 0; i <= lines.Count - 1; i++)
            {
                for (var a = 0; a <= lines[i].Length - 1; a++)
                {
                    grid[i, a] = lines[i].Substring(a, 1);
                }
            }
            
            Console.WriteLine("Part 1: " + Trees(grid, 3, 1));
            Console.WriteLine("Part 2: " + 
                              Trees(grid, 3, 1) * 
                              Trees(grid, 1, 1) * 
                              Trees(grid, 5, 1) * 
                              Trees(grid, 7, 1) * 
                              Trees(grid, 1, 2)
                              );
            Console.ReadKey();
        }
    }
}