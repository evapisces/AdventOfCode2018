using System;
using System.Collections.Generic;
using System.Linq;

namespace day_2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var testFileLines = System.IO.File.ReadAllLines("test-input-2.txt");
            var fileLines = System.IO.File.ReadAllLines("puzzle-input.txt");

//            PuzzlePartA(fileLines);

            PuzzlePartB(testFileLines);

            Console.Read();
        }

        public static void PuzzlePartA(string[] fileLines) {
            var twos = 0;
            var threes = 0;

            foreach (var line in fileLines)
            {
                Console.WriteLine(line);
                var characters = line.GroupBy(c => c).Select(c => new Character { Char = c.Key, Count = c.Count() });

                foreach (var ch in characters)
                {
                    Console.Write($"{ch.Char} = {ch.Count}, ");
                }
                Console.WriteLine();

                var two = characters.Where(c => c.Count == 2).ToList();

                var three = characters.Where(c => c.Count == 3).ToList();

                if (two.Count() > 0)
                {
                    twos++;
                }

                if (three.Count() > 0)
                {
                    threes++;
                }
            }


            Console.WriteLine($"Checksum = {twos * threes}");
        }

        public static void PuzzlePartB(string[] fileLines) {
            var orderedLines = fileLines.OrderBy(f => f);

            foreach(var line in orderedLines) {
                Console.WriteLine(line);
            }
        }
    }

    public class Character {
        public char Char { get; set; }
        public int Count { get; set; }
    }
}
