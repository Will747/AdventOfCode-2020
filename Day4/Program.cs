using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day4
{
    internal static class Program
    {
        private static bool ValidateValues(List<String> values)
        {
            var valid = true;

            for (var i = 0; i <= values.Count - 1; i++)
            {
                var field = values[i].Substring(0, 3);
				var data = values[i].Substring(4);
                    switch (field)
                    {
                        case "byr":
                            var year = Convert.ToInt32(data);
                            if (year < 1920 || year > 2002)
                            {
                                valid = false;
                            }

                            break;
                        case "iyr":
                            var year2 = Convert.ToInt32(data);
                            if (year2 < 2010 || year2 > 2020)
                            {
                                valid = false;
                            }

                            break;
                        case "eyr":
                            var year3 = Convert.ToInt32(data);
                            if (year3 < 2020 || year3 > 2030)
                            {
                                valid = false;
                            }

                            break;
                        case "hgt":
							if (data.Length > 2) {
								var numHeight = Convert.ToInt32(data.Substring(0, data.Length - 2));
                            if (data.Contains("in"))
                            {
                                if (numHeight < 59 || numHeight > 76)
                                {
                                    valid = false;
                                }
                            }
                            else if (numHeight < 150 || numHeight > 193)
                            {
                                valid = false;
                            }
                            
							} else {
								valid = false;
							}
                            break;
                        case "hcl":
                            if (data.Substring(0, 1) != "#" && !Regex.Match(data.Substring(1), "^[0-9a-f]{6}$").Success)
                            {
								valid = false;	
							}

                            break;
                        case "ecl":
                            if (
                                !data.Contains("amb") &&
                                !data.Contains("blu") &&
                                !data.Contains("brn") &&
                                !data.Contains("gry") &&
                                !data.Contains("grn") &&
                                !data.Contains("hzl") &&
                                !data.Contains("oth")
                            )
                            {
                                valid = false;
                            }

                            break;
                        case "pid":
							if(!Regex.Match(data, "^[0-9]{9}$").Success) 
							{
								valid = false;	
							}
                            break;
                    }
                
            }

            return valid;
        }

        private static int ValidPassports(List<string> lines, bool part2)
        {
            var passports = new List<string>();
            var line = "";

            //Separate passports into individual strings
            for (var i = 0; i <= lines.Count - 1; i++)
            {
                if (lines[i] == "")
                {
                    passports.Add(line);
                    line = "";
                }
                else
                {
                    line += " " + lines[i];
                }
            }

            //Check number of fields if valid add one to validCount
            var validCount = 0;
            string[] fields = {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};
            for (var a = 0; a <= passports.Count - 1; a++)
            {
                var partCount = 0;

                for (var b = 0; b <= fields.Length - 1; b++)
                {
                    if (passports[a].Contains(fields[b] + ":"))
                    {
                        partCount++;
                    }
                }
				
                if (partCount >= 7)
                {
                    if (part2)
                    {
                        var values = new List<string>();
                        var parts = passports[a].Split(Convert.ToChar(" "));
						
						for (var d = 0; d <= parts.Length - 1; d++) 
						{
							if (parts[d].Length > 3) {
								values.Add(parts[d]);
							}
								
						}
						
                        if (ValidateValues(values))
                        {
                            validCount++;
                        }
                    }
                    else
                    {
                        validCount++;
                    }
                }
            }
            
            return validCount;
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
            
            Console.WriteLine("Part 1: " + ValidPassports(lines, false));
            Console.WriteLine("Part 2: " + ValidPassports(lines, true));
            Console.ReadKey();
        }
    }
}