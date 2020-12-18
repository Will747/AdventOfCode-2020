using System;
using System.Collections.Generic;
using System.IO;

namespace Day16
{
    internal static class Program
    {
        private struct Range
        {
            public string Name { get; set; }
            public int Lowest { get; set; }
            public int Biggest { get; set; }
            
            public int Lowest1 { get; set; }
            public int Biggest1 { get; set; }
        }

        private static long Part2(List<Range> parameters, List<int[]> tickets, string myTickets)
        {
            var order = new List<string>[parameters.Count];
            
            for (int i = 0; i < parameters.Count; i++)
            {
                foreach (var t in parameters)
                {
                    var valid = true;
                    foreach (var ticket in tickets)
                    {
                        if (!(ticket[i] >= t.Lowest && ticket[i] <= t.Biggest ||
                              ticket[i] >= t.Lowest1 && ticket[i] <= t.Biggest1))
                        {
                            valid = false;
                            break;
                        }
                    }

                    if (valid)
                    {
                        if (order[i] == null)
                        {
                            order[i] = new List<string>();
                        }
                        order[i].Add(t.Name);
                    }
                }
            }

            var sorting = true;
            while (sorting)
            {
                sorting = false;
                for (int i = 0; i < order.Length; i++)
                {
                    if (order[i].Count == 1)
                    {
                        for (int a = 0; a < order.Length; a++)
                        {
                            if (order[a].Contains(order[i][0]) && a != i)
                            {
                                order[a].Remove(order[i][0]);
                                sorting = true;
                            }
                        }
                    }
                }

                
            }

            var ticketRows = myTickets.Split(',');
            long count = 1;
            for (int b = 0; b < order.Length; b++)
            {
                if (order[b][0].Split(' ')[0] == "departure")
                {
                    count *= Convert.ToInt32(ticketRows[b]);
                }
            }
            
            return count;
        }
        
        private static long Part1(List<string> parameters, List<int[]> tickets, string myTicket = "" , bool part2 = false)
        {
            var intParameters = new List<Range>();
            
            foreach (var parameter in parameters)
            {
                var values = parameter.Split(':')[1].Split(' ');
                var val0 = Array.ConvertAll(values[1].Split('-'), Convert.ToInt32);
                var val1 = Array.ConvertAll(values[3].Split('-'), Convert.ToInt32);
                var name = parameter.Split(':')[0];
                intParameters.Add(new Range{Name = name, Lowest = val0[0], Biggest = val0[1], Lowest1 = val1[0], Biggest1 = val1[1]});
            }

            var count = 0;
            var remove = new List<int[]>();
            foreach (var t in tickets)
            {
                var ticket = t;
                foreach (var value in ticket)
                {
                    var valid = false;
                    foreach (var field in intParameters)
                    {
                        if (value >= field.Lowest && value <= field.Biggest ||
                            value >= field.Lowest1 && value <= field.Biggest1)
                        {
                            valid = true;
                        }
                    }

                    if (valid == false && part2 == false)
                    {
                        count += value;
                    }
                    else if (part2 && valid == false)
                    {
                        remove.Add(t);
                    }
                }
            }

            if (part2)
            {
                foreach (var ticket in remove)
                {
                    tickets.Remove(ticket);
                }
                return Part2(intParameters, tickets, myTicket);
            }
            
            return count;
        }
        
        static void Main(string[] args)
        {
            var parameters = new List<string>();
            string myTicket;
            var tickets = new List<int[]>();
            using (var reader = File.OpenText("input.txt"))
            {
                string s;
                while ((s = reader.ReadLine()) != "")
                {
                    parameters.Add(s);
                }
                
                reader.ReadLine();
                myTicket = reader.ReadLine();
                reader.ReadLine();
                
                reader.ReadLine();
                while ((s = reader.ReadLine()) != null)
                {
                    tickets.Add(Array.ConvertAll(s.Split(','), Convert.ToInt32));
                }
            }

            Console.WriteLine("Part 1: " + Part1(parameters, tickets));
            Console.WriteLine("Part 2: " + Part1(parameters, tickets, myTicket, true));
            Console.ReadKey();
        }
    }
}