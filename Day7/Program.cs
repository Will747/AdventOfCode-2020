using System;
using System.Collections.Generic;
using System.IO;

namespace Day7
{
    class Program
    {
        private static bool LeadsToGold(List<string[]> bags, int bagIndex)
        {
            for (var b = 5; b <= bags[bagIndex].Length - 3; b+=4)
            {
                if (bags[bagIndex][b] + bags[bagIndex][b + 1] == "shinygold")
                {
                    return true;	
                }

                if (bags[bagIndex][4] != "no") {
                    var newBag = bags[bagIndex][b] + bags[bagIndex][b + 1];
                    var newBagIndex = bags.FindIndex(bag => bag[0] + bag[1] == newBag);
                    if (LeadsToGold(bags, newBagIndex))
                    {
                        return true;	
                    }
                }

            }
			
            return false;	
        }

        private static int BagNum(List<string[]> bags, int bagIndex)
        {
            var count = 0;
			
            if(bags[bagIndex][4] != "no") 
            {
                for (var b = 4; b <= bags[bagIndex].Length - 3; b+=4) 
                {
                    var newBagIndex = bags.FindIndex(bag => bag[0] + bag[1] == bags[bagIndex][b + 1] + bags[bagIndex][b+2]);
                    var innerBags = BagNum(bags, newBagIndex);
					
                    if(innerBags == 0)
                    {
                        count += Convert.ToInt32(bags[bagIndex][b]);
                    } else {
                        count += Convert.ToInt32(bags[bagIndex][b]) + Convert.ToInt32(bags[bagIndex][b])*innerBags;
                    }
                }
            }
			
            return count;
        }

        private static int Part2(List<string> input) 
        {
            var bags = new List<string[]>();
			
            for (var i = 0; i <= input.Count - 1; i++)
            {
                bags.Add(input[i].Split(Convert.ToChar(" ")));	
            }
			
            var newBagIndex = bags.FindIndex(bag => bag[0] + bag[1] == "shinygold");
			
            return BagNum(bags, newBagIndex);
        }

        private static int Part1(List<string> input) 
        {
            var bags = new List<string[]>();
			
            for (var i = 0; i <= input.Count - 1; i++)
            {
                bags.Add(input[i].Split(Convert.ToChar(" ")));	
            }
			
            var count = 0;
			
            for (var a = 0; a <= bags.Count - 1; a++)
            {
                if(LeadsToGold(bags, a))
                {
                    count++;
                }
            }
			
            return count;
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