using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Day3
{
    public class Program
    {
        public const int MAX_X = 1500;
        public const int MAX_Y = 1500;

        public static void Main(string[] args)
        {
            var testLines = File.ReadAllLines("../../../test-input.txt");

            var lines = File.ReadAllLines("../../../puzzle-input.txt").ToList();

            var fabric = new List<FabricSquare>();
            var inputs = new List<Input>();
            var space = new string[MAX_X,MAX_Y];

            var multipleClaims = new List<string>();
            var distinct = 0;

            space.ClearTo(".");

            var cleanClaim = 0;

            foreach (var obj in lines)
            {
                var splitObj = obj.Split(' ');
                var input = new Input()
                {
                    ClaimId = Convert.ToInt32(splitObj[0].Replace("#", "")),
                    UpperLeftLocation = splitObj[2].Replace(":", ""),
                    RectangleDimensions = splitObj[3]
                };

                var i = Convert.ToInt32(input.UpperLeftLocation.Split(',')[0]);
                var j = Convert.ToInt32(input.UpperLeftLocation.Split(',')[1]);
                var rx = Convert.ToInt32(input.RectangleDimensions.Split('x')[0]);
                var ry = Convert.ToInt32(input.RectangleDimensions.Split('x')[1]);
                var overlapped = false;

                for (var index = j; index < j+ ry; index++)
                {
                    for (var index2 = i; index2 < i+ rx; index2++)
                    {
                        if (space[index, index2] != ".")
                        {
                            if (space[index, index2] != "X" && Convert.ToInt32(space[index, index2]) == cleanClaim)
                            {
                                cleanClaim = 0;
                            }

                            space[index, index2] = "X";
                            if (!multipleClaims.Contains($"{index}, {index2}"))
                            {
                                multipleClaims.Add($"{index}, {index2}");
                                overlapped = true;
                            }
                        }
                        else
                        {
                            space[index, index2] = input.ClaimId.ToString();
                        }
                    }
                }

                if (!overlapped)
                {
                    cleanClaim = input.ClaimId;
                }

                distinct = multipleClaims.Distinct().Count();

                for (var k = 0; k < MAX_X; k++)
                {
                    for (var l = 0; l < MAX_Y; l++)
                    {
                        Console.Write(space[k, l] + " ");
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine($"Multiple Claims = {distinct}");

            Console.WriteLine($"Clean claim = {cleanClaim}");

            Console.Read();
        }
    }

    public class Input
    {
        public int ClaimId { get; set; }
        public string UpperLeftLocation { get; set; }
        public string RectangleDimensions { get; set; }
    }

    public class FabricSquare
    {
        public int NumberOfHits { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public bool IsClaimed { get; set; }

        public FabricSquare()
        {
            NumberOfHits = 0;
            IsClaimed = false;
        }
    }

    public static class TwoDArrayExtensions
    {
        public static void ClearTo(this string[,] a, string val)
        {
            for (int i = a.GetLowerBound(0); i <= a.GetUpperBound(0); i++)
            {
                for (int j = a.GetLowerBound(1); j <= a.GetUpperBound(1); j++)
                {
                    a[i, j] = val;
                }
            }
        }
    }
}
