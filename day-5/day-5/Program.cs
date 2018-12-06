using System;
using System.Collections.Generic;
using System.Linq;

namespace day_5
{
    public class Program
    {
        public static List<int> PolymerLengths { get; set; }

        public static void Main(string[] args)
        {
            var testInputFile = "../../../test-input-1.txt";
            var inputFile = "../../../puzzle-input.txt";
            PolymerLengths = new List<int>();

            var content = System.IO.File.ReadAllText(inputFile);

            //PartA(content);

            PartB(content);

            Console.Read();
        }

        public static void PartA(string polymer)
        {
            var result = ReactPolymer(polymer);
        }

        public static void PartB(string polymer)
        {
            var i = 0;
            var uniqueCharacters = polymer.ToLower().Distinct().OrderBy(c => c).ToList();//.ToDictionary<int, char>(v => i++, v => v);

            Dictionary<int, char> dictionary = uniqueCharacters.ToDictionary(v => i++, v => v);
            
            foreach (KeyValuePair<int, char> pair in dictionary)
            {
                var temp = polymer;
                temp = temp.Replace(pair.Value.ToString().ToUpper(), "").Replace(pair.Value.ToString().ToLower(), "");

                var result = ReactPolymer(temp);
                //Console.WriteLine("result = " + result);
                PolymerLengths.Add(result.Length);
            }
            
            PolymerLengths.Sort();

            Console.WriteLine("Shortest polymer = " + PolymerLengths[0]);
        }

        public static string ReactPolymer(string polymer)
        {
            var removedSomething = false;

            do
            {
                removedSomething = false;

                for (var index = 0; index < polymer.Length - 1; index++)
                {
                    //Console.WriteLine($"content[{index}] = {content[index]}, content[{index + 1}] = {content[index + 1]}");
                    var letterOne = polymer[index];
                    var letterTwo = polymer[index + 1];

                    if (char.ToLower(letterOne) == char.ToLower(letterTwo) && ((char.IsLower(letterOne) && char.IsUpper(letterTwo)) || (char.IsUpper(letterOne) && char.IsLower(letterTwo))))
                    {
                        polymer = polymer.Remove(index, 2);
                        removedSomething = true;
                        break;
                    }
                }

                //Console.WriteLine("content = " + content);
            } while (removedSomething);

            //Console.WriteLine($"Number of units = {polymer.Length}");
            

            return polymer;
        }
    }
}
