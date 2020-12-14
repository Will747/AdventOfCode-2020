using System;
using System.Collections.Generic;
using System.IO;

namespace Day14
{
    internal static class Program
    {
        private struct MemoryValue
        {
            public long Address { get; set; }
            public long Value { get; set; }
        }
        
        private static List<string> RemoveX(string input)
        {
            var output = new List<string>();

            if (input.Contains("X"))
            {
                var index = input.IndexOf('X');
                output.AddRange(RemoveX(input.Remove(index, 1).Insert(index, "1")));
                output.AddRange(RemoveX(input.Remove(index, 1).Insert(index, "0")));
            }
            else
            {
                output.Add(input);
            }
            
            return output;
        }
        
        private static List<string> Mask2(string number, string mask)
        {
            number = number.PadLeft(36, '0');
            
            for (int i = 0; i < number.Length; i++)
            {
                if (mask.Substring(i, 1) == "1")
                {
                    number = number.Remove(i, 1).Insert(i, Convert.ToString(1));
                } else if (mask.Substring(i, 1) == "X")
                {
                    number = number.Remove(i, 1).Insert(i, "X");
                }
            }
            return RemoveX(number);
        }
        
        private static long Part2(List<string> input)
        {
            var mask = "";
            var memory = new List<MemoryValue>();
            
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                if (parts[0] == "mask")
                {
                    mask = parts[2];
                }
                else
                {
                    var address = Convert.ToInt32(parts[0].Substring(4, parts[0].Length - 5));
                    var value = Convert.ToInt64(parts[2]);
                    var result = Mask2(Convert.ToString(address, 2), mask);

                    foreach (var newAddress in result)
                    {
                        var intAddress = Convert.ToInt64(newAddress, 2);

                        if (memory.Exists(memValue => memValue.Address == intAddress))
                        {
                            var index = memory.FindIndex(memValue => memValue.Address == intAddress);
                            memory[index] = new MemoryValue{Address = intAddress, Value = value};
                        }
                        else
                        {
                            memory.Add(new MemoryValue{Address = intAddress, Value = value});
                        }
                    }
                }
            }

            long total = 0;
            foreach (var value in memory)
            {
                total += value.Value;
            }
            
            return total;
        }
        
        private static string Mask(string number, string mask)
        {
            number = number.PadLeft(36, '0');
            
            for (int i = 0; i < mask.Length; i++)
            {
                if (mask.Substring(i, 1) == "X")
                {
                    mask = mask.Remove(i, 1).Insert(i, number.Substring(i, 1));
                }
            }

            return mask;
        }
        
        private static long Part1(List<string> input)
        {
            var mask = "";
            var memory = new long[Int32.MaxValue];
            var usedAddresses = new List<int>();
            
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                if (parts[0] == "mask")
                {
                    mask = parts[2];
                }
                else
                {
                    var address = Convert.ToInt32(parts[0].Substring(4, parts[0].Length - 5));
                    var binary = Convert.ToString(Convert.ToInt64(parts[2]), 2);

                    if (memory[address] == 0)
                    {
                        usedAddresses.Add(address);
                    }
                    
                    memory[address] = Convert.ToInt64(Mask(binary, mask), 2);
                }
            }

            long total = 0;
            foreach (var index in usedAddresses)
            {
                total += memory[index];
            }
            
            return total;
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