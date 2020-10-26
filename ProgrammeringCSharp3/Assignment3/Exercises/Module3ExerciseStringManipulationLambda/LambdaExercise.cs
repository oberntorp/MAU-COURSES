using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3ExerciseStringManipulationLambda
{
    class LambdaExercise
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Removing empty or null elements from array: {RemoveEmptyOrNullElements(new string[] { "    ", "", "Hi", "My Name Is", "Oskar Berntorp" })}");
            Console.Write("Create an array of random chars: ");
            Console.WriteLine(GenerateCharArrayFromString("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 8));
            Console.WriteLine(ShortestAndLongestString(new string[] { "cherry", "apple", "blueberry" }));
            Console.Write("Press key to quit...");
            Console.ReadKey();
        }

        private static string RemoveEmptyOrNullElements(string[] stringToAlter)
        {
            return string.Join(" ", stringToAlter.Where(x => (x.Trim().Length > 0 && !string.IsNullOrEmpty(x))));
        }

        private static char[] GenerateCharArrayFromString(string stringToGenerateCharArrayFrom, int lengthOfGeneratedArray)
        {
            return Enumerable.Repeat(stringToGenerateCharArrayFrom, lengthOfGeneratedArray).Select(x => x[new Random().Next(x.Length)]).ToArray();
        }

        private static string ShortestAndLongestString(string[] words)
        {
            string result = $"Shortest String: {words.Min(x => x.Length), 10}";
            result += Environment.NewLine + $"Longest String: {words.Max(x => x.Length), 11}";
            return result;
        }
    }
}
