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
                var buffer = new string[3];

                var csv = new StandardFlatFileReader(reader);

                while (csv.NextLine(buffer))
                {

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