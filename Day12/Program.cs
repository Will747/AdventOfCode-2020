using System;
using System.Collections.Generic;
using System.IO;

namespace Day12
{
    internal static class Program
    {
        private static void Rotate(ref int waypointNorth, ref int waypointWest, int direction)
        {
            int newNorth;
            
            switch (direction)
            {
                case 90:
                    newNorth = -waypointWest;
                    waypointWest = waypointNorth;
                    waypointNorth = newNorth;
                    break;
                case 180:
                    waypointNorth = -waypointNorth;
                    waypointWest = -waypointWest;
                    break;
                case 270:
                    newNorth = waypointWest;
                    waypointWest = -waypointNorth;
                    waypointNorth = newNorth;
                    break;
            }
        }
        
        private static int Part2(List<string> input)
        {
            var north = 0;
            var west = 0;
            var waypointNorth = 1;
            var waypointWest = -10;

            foreach (var command in input)
            {
                switch (command.Substring(0, 1))
                {
                    case "N":
                        waypointNorth += Convert.ToInt32(command.Substring(1));
                        break;
                    case "S":
                        waypointNorth -= Convert.ToInt32(command.Substring(1));
                        break;
                    case "E":
                        waypointWest -= Convert.ToInt32(command.Substring(1));
                        break;
                    case "W":
                        waypointWest += Convert.ToInt32(command.Substring(1));
                        break;
                    case "L":
                        Rotate(ref waypointNorth, ref waypointWest, Convert.ToInt32(command.Substring(1)));
                        break;
                    case "R":
                        Rotate(ref waypointNorth, ref waypointWest, 360 - Convert.ToInt32(command.Substring(1)));
                        break;
                    case "F":
                        north += waypointNorth * Convert.ToInt32(command.Substring(1));
                        west += waypointWest * Convert.ToInt32(command.Substring(1));
                        break;
                }
            }

            return Math.Abs(north + west);
        }
        
        private static int Part1(List<string> input)
        {
            var north = 0;
            var west = 0;
            
            //starts east
            var direction = 90;
            
            foreach (var command in input)
            {
                switch (command.Substring(0, 1))
                {
                    case "N":
                        north += Convert.ToInt32(command.Substring(1));
                        break;
                    case "S":
                        north -= Convert.ToInt32(command.Substring(1));
                        break;
                    case "E":
                        west -= Convert.ToInt32(command.Substring(1));
                        break;
                    case "W":
                        west += Convert.ToInt32(command.Substring(1));
                        break;
                    case "L":
                        direction -= Convert.ToInt32(command.Substring(1));
                        if (direction < 0)
                        {
                            direction = 360 + direction;
                        }
                        break;
                    case "R":
                        direction += Convert.ToInt32(command.Substring(1));
                        if (direction >= 360)
                        {
                            direction = direction - 360;
                        }
                        break;
                    case "F":
                        switch (direction)
                        {
                            case 0:
                                north += Convert.ToInt32(command.Substring(1));
                                break;
                            case 90:
                                west -= Convert.ToInt32(command.Substring(1));
                                break;
                            case 180:
                                north -= Convert.ToInt32(command.Substring(1));
                                break;
                            case 270:
                                west += Convert.ToInt32(command.Substring(1));
                                break;
                        }
                        break;
                }
            }

            return Math.Abs(north + west);
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