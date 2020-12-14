using System;
using System.Collections.Generic;
using System.IO;

namespace Day13
{
    internal static class Program
    {
        //This could solve the examples fast but not for the puzzle input as it takes too long.
        private static ulong Part2(List<string> input)
        {
            var busesStr = input[1].Split(Convert.ToChar(","));
            var buses = new ulong[busesStr.Length];
            var busesIndex = new List<int>();
            var timeOffsets = new ulong[buses.Length];

            for (var i = 0; i < buses.Length; i++)
            {
                if (busesStr[i] != "x")
                {
                    busesIndex.Add(i);
                    buses[i] = Convert.ToUInt64(busesStr[i]);
                    timeOffsets[i] = buses[i] - Convert.ToUInt64(i);
                }
            }

            timeOffsets[0] = 0;

            var lowestId = Convert.ToUInt64(buses[0]);

            var incomplete = true;
            ulong count = 0;

            while (incomplete)
            {
                incomplete = false;
                foreach (var index in busesIndex)
                {
                    if (count % buses[index] != timeOffsets[index])
                    {
                        incomplete = true;
                        break;
                    }
                }
                count += lowestId;
            }
            
            return count - lowestId;
        }
        
        private static int Part1(List<string> input)
        {
            var time = Convert.ToInt32(input[0]);
            var buses = input[1].Split(Convert.ToChar(","));
            var lowestWait = 0;
            var bestBusId = 0;
            
            foreach (var bus in buses)
            {
                if (bus != "x")
                {
                    var count = (int)Math.Floor(time/Convert.ToDecimal(bus));
                    while (count * Convert.ToInt32(bus) < time)
                    {
                        count++;
                    }

                    if (lowestWait > count * Convert.ToInt32(bus) - time || lowestWait == 0)
                    {
                        lowestWait = count * Convert.ToInt32(bus) - time;
                        bestBusId = Convert.ToInt32(bus);
                    }
                }
            }
            
            return bestBusId * lowestWait;
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