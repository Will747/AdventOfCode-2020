using System;
using System.Collections.Generic;
using System.IO;

namespace Day2
{
    class Program
    {
        static int ValidPasswords2(List<string> passwords)
        {
            int validPasswords = 0;

            for (var i = 0; i <= passwords.Count - 1; i++)
            {
                var parts = passwords[i].Split(Convert.ToChar("-"), Convert.ToChar(" "));

                if (parts != null)
                {
                    var min = Convert.ToInt32(parts[0]);
                    var max = Convert.ToInt32(parts[1]);
                    var letter = parts[2].Substring(0, 1);
                    var word = parts[3];

                    if (word.Substring(min - 1, 1) == letter ^ word.Substring(max - 1, 1) == letter)
                    {
                        validPasswords++;
                    }
                }

            }

            return validPasswords;
        }

        static int ValidPasswords(List<string> passwords)
        {
            int validPasswords = 0;
            
            for (var i = 0; i <= passwords.Count - 1; i++)
            {
                var parts = passwords[i].Split(Convert.ToChar("-"), Convert.ToChar(" "));

                if (parts != null)
                {
                    var min = Convert.ToInt32(parts[0]);
                    var max = Convert.ToInt32(parts[1]);;
                    var letter = parts[2].Substring(0, 1);
                    var word = parts[3];
                    var count = 0;
                
                    for (var a = 0; a <= word.Length - 1; a++)
                    {
                        if (word.Substring(a, 1) == letter)
                        {
                            count++;
                        }
                    }

                    if (count >= min && count <= max)
                    {
                        validPasswords++;
                    }
                }
                
            }
            
            return validPasswords;
        }
        
        static void Main(string[] args)
        {
            var passwords = new List<string>();
            using (var reader = File.OpenText("input.txt"))
            {
                string s;
                while ((s = reader.ReadLine()) != null)
                {
                    passwords.Add(s);
                }
            }
            Console.WriteLine("Part 1: " + ValidPasswords(passwords));
            Console.WriteLine("Part 2: " + ValidPasswords2(passwords));
            Console.ReadKey();
        }
    }
}