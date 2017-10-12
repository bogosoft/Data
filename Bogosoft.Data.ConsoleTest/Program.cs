using System;
using System.IO;

namespace Bogosoft.Data.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("Heroes.csv"))
            {
                var heroes = new CsvToEnumerableAdapter<Hero>(Map, reader);

                foreach (var hero in heroes)
                {
                    Console.WriteLine(hero);
                }
            }

            Console.ReadLine();
        }

        static Hero Map(string[] fields)
        {
            return new Hero
            {
                Height = int.Parse(fields[1]),
                MostNotableFeat = fields[2],
                Name = fields[0]
            };
        }
    }
}