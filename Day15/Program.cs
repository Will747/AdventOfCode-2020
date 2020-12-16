using System;
using System.Collections.Generic;

namespace Day15
{
    internal static class Program
    {
        private static int Part1(string input, int until)
        {
            var startingNumbers = input.Split(',');
            var turn = 1;
            var numbers = new int[Int32.MaxValue];
            var lastNumber = 0;

            for (int i = 0; i < startingNumbers.Length; i++)
            {
                numbers[Convert.ToInt32(startingNumbers[i])] = turn;
                lastNumber = Convert.ToInt32(startingNumbers[i]);
                turn++;
            }

            while (turn != until + 1)
            {
                int newNumber;
                if (numbers[lastNumber] == 0)
                {
                    newNumber = 0;
                }
                else
                {
                    newNumber = turn - 1 - numbers[lastNumber];
                }

                numbers[lastNumber] = turn - 1;
                lastNumber = newNumber;
                turn++;
            }
            
            return lastNumber;
        }
        
        static void Main(string[] args)
        {
            var input = "0,13,1,8,6,15";
            Console.WriteLine("Part 1: " + Part1(input, 2020));
            Console.WriteLine("Part 1: " + Part1(input, 30000000));
        }
    }
}