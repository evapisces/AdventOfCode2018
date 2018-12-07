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

            PuzzlePartA(fileLines);

            PuzzlePartB(fileLines);

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
            var orderedLines = fileLines.OrderBy(f => f).ToList();

            for (int i = 0; i < orderedLines.Count(); i++)
            {
                for (int j = i+1; j < orderedLines.Count()-i; j++) 
                {
                    var differingChars = 0;
                    var sameChars = new List<char>();
                    for (var k = 0; k < orderedLines[i].Length; k++) 
                    {
                        if (orderedLines[i][k] != orderedLines[j][k]) {
                            differingChars++;
                        } else {
                            sameChars.Add(orderedLines[i][k]);
                        }
                    }

                    if (differingChars == 1) {
                        Console.WriteLine($"orderedLines[{i}] = {orderedLines[i]}, orderedLines[{j}] = {orderedLines[j]}");
                        Console.WriteLine($"same chars = {new String(sameChars.ToArray())}");
                        break;
                    }
                }
            }
        }
    }

    public class Character {
        public char Char { get; set; }
        public int Count { get; set; }
    }
}
