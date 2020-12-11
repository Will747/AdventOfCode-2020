using System;
using System.Collections.Generic;
using System.IO;

namespace Day11
{
    internal static class Program
    {
        private static int CheckToNextSeat(string[,] input, int seatX, int seatY, string letter)
        {
            var count = 0;

            for (var b = 0; b <= 8; b++)
            {
                count+= CheckSeats2(input, seatX, seatY, letter, b);
            }

            return count;
        }
        
        private static int CheckSeats2(string[,] input, int seatX, int seatY, string letter, int direction)
        {
            var i = seatY;
            var a = seatX;
            var count = 0;

            switch (direction)
            {
                case 0:
                    //top left
                    if ( i != 0 && a != 0 && input[i - 1,a - 1] == letter)
                    {
                        count++;
                    } else if ( i != 0 && a != 0 && input[i - 1,a - 1] == ".")
                    {
                        count += CheckSeats2(input, a - 1, i - 1, letter, 0);
                    }
                    
                    break;
                case 1:
                    //top center
                    if (i != 0 && input[i - 1,a] == letter)
                    {
                        count++;
                    } else if (i != 0 && input[i - 1,a] == ".")
                    {
                        count += CheckSeats2(input, a, i - 1, letter, 1);
                    }
                    break;
                case 2:
                    //top right
                    if (i != 0 && a != input.GetLength(1) - 1 && input[i - 1,a + 1] == letter)
                    {
                        count++;
                    } else if (i != 0 && a != input.GetLength(1) - 1 && input[i - 1,a + 1] == ".")
                    {
                        count += CheckSeats2(input, a + 1, i - 1, letter, 2);
                    }
                    break;
                case 3:
                    //left
                    if (a != 0 && input[i,a - 1] == letter)
                    {
                        count++;
                    } else if (a != 0 && input[i,a - 1] == ".")
                    {
                        count += CheckSeats2(input, a - 1, i, letter, 3);
                    }
                    break;
                case 4:
                    //right
                    if (a != input.GetLength(1) - 1 && input[i,a + 1] == letter)
                    {
                        count++;
                    } else if (a != input.GetLength(1) - 1 && input[i,a + 1] == ".")
                    {
                        count += CheckSeats2(input, a + 1, i, letter, 4);
                    }
                    break;
                case 5:
                    //bottom left
                    if (i != input.GetLength(0) - 1 && a != 0 && input[i + 1,a - 1] == letter)
                    {
                        count++;
                    } else if (i != input.GetLength(0) - 1 && a != 0 && input[i + 1,a - 1] == ".")
                    {
                        count += CheckSeats2(input, a - 1, i + 1, letter, 5);
                    }
                    break;
                case 6:
                    //bottom center
                    if (i != input.GetLength(0) - 1 && input[i + 1,a] == letter)
                    {
                        count++;
                    } else if (i != input.GetLength(0) - 1 && input[i + 1,a] == ".")
                    {
                        count += CheckSeats2(input, a, i + 1, letter, 6);
                    }
                    break;
                case 7:
                    //bottom right
                    if (i != input.GetLength(0) - 1 && a != input.GetLength(1) - 1  && input[i + 1,a + 1] == letter)
                    {
                        count++;
                    } else if (i != input.GetLength(0) - 1 && a != input.GetLength(1) - 1  && input[i + 1,a + 1] == ".")
                    {
                        count += CheckSeats2(input, a + 1, i + 1, letter, 7);
                    }
                    break;
            }

            return count;
        }
        
        private static int CheckSeats(string[,] input, int seatX, int seatY, string letter)
        {
            var i = seatY;
            var a = seatX;
            var count = 0;
            //top left
            if ( i != 0 && a != 0 && input[i - 1,a - 1] == letter)
            {
                count++;
            }
            //top center
            if (i != 0 && input[i - 1,a] == letter)
            {
                count++;
            }
            //top right
            if (i != 0 && a != input.GetLength(1) - 1 && input[i - 1,a + 1] == letter)
            {
                count++;
            }
            //left
            if (a != 0 && input[i,a - 1] == letter)
            {
                count++;
            }
            //right
            if (a != input.GetLength(1) - 1 && input[i,a + 1] == letter)
            {
                count++;
            }
            //bottom left
            if (i != input.GetLength(0) - 1 && a != 0 && input[i + 1,a - 1] == letter)
            {
                count++;
            }
            //bottom center
            if (i != input.GetLength(0) - 1 && input[i + 1,a] == letter)
            {
                count++;
            }
            //bottom right
            if (i != input.GetLength(0) - 1 && a != input.GetLength(1) - 1  && input[i + 1,a + 1] == letter)
            {
                count++;
            }

            return count;
        }
        
        private static int Part1(List<List<string>> input, bool part2 = false)
        {
            var oldInput = new string[input.Count,input[0].Count];

            for (var b = 0; b < input.Count; b++)
            {
                for (var c = 0; c < input[0].Count; c++)
                {
                    oldInput[b, c] = input[b][c];
                }
            }
            
            var newInput = new string[input.Count,input[0].Count];
            var changed = true;
            var occupiedSeats = 0;
            
            while (changed)
            {
                occupiedSeats = 0;
                changed = false;
                for (var i = 0; i < input.Count; i++)
                {
                    for (var a = 0; a < input[i].Count; a++)
                    {
                        switch (oldInput[i,a])
                        {
                            case "#":
                                occupiedSeats++;
                                if (part2 == false && CheckSeats(oldInput, a, i, "#") > 3)
                                {
                                    newInput[i,a] = "L";
                                    changed = true;
                                }
                                else if (part2 && CheckToNextSeat(oldInput, a, i, "#") > 4)
                                {
                                    newInput[i,a] = "L";
                                    changed = true;
                                } else
                                {
                                    newInput[i, a] = "#";
                                }
                                break;
                            case "L":
                                if (part2 == false && CheckSeats(oldInput, a, i, "#") <= 0)
                                {
                                    newInput[i,a] = "#";
                                    changed = true;
                                }
                                else if (part2 && CheckToNextSeat(oldInput, a, i, "#") <= 0)
                                {
                                    newInput[i,a] = "#";
                                    changed = true;
                                } else
                                {
                                    newInput[i, a] = "L";
                                }
                                break;
                            case ".":
                                newInput[i, a] = ".";
                                break;
                        }
                    }
                }
                oldInput = (string[,])newInput.Clone();
            }
            
            return occupiedSeats;
        }
        
        static void Main(string[] args)
        {
            var lines = new List<List<string>>();
            using (var reader = File.OpenText("input.txt"))
            {
                string s;
                while ((s = reader.ReadLine()) != null)
                {
                    lines.Add(new List<string>());

                    for (var i = 0; i < s.Length; i++)
                    {
                        lines[lines.Count - 1].Add(s.Substring(i, 1));   
                    }
                }
            }
            
            Console.WriteLine("Part 1: " + Part1(lines));
            Console.WriteLine("Part 2: " + Part1(lines, true));
            Console.ReadKey();
        }
    }
}