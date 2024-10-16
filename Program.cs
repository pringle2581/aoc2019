using aoc2019.day1;
using aoc2019.day2;
using aoc2019.day3;
using aoc2019.day4;
using aoc2019.day5;
using aoc2019.day6;
using aoc2019.day7;
using aoc2019.day8;

string input = "../../../input/";

void PrintResults(int day, int[] results)
{
    Console.WriteLine($"Day {day} Part 1: {results[0]}");
    Console.WriteLine($"Day {day} Part 2: {results[1]}\n");
}

PrintResults(1, Day1.Solve(File.ReadAllLines(input + 1)));
PrintResults(2, Day2.Solve(File.ReadAllLines(input + 2)));
PrintResults(3, Day3.Solve(File.ReadAllLines(input + 3)));
PrintResults(4, Day4.Solve(File.ReadAllLines(input + 4)));
PrintResults(5, Day5.Solve(File.ReadAllLines(input + 5)));
PrintResults(6, Day6.Solve(File.ReadAllLines(input + 6)));
PrintResults(7, Day7.Solve(File.ReadAllLines(input + 7)));
PrintResults(8, Day8.Solve(File.ReadAllLines(input + 8)));