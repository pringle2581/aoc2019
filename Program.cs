using aoc2019.day1;
using aoc2019.day2;
using aoc2019.day3;
using aoc2019.day4;

string input = "../../../input/";

int[] day1 = Day1.Solve(File.ReadAllLines(input + 1));
Console.WriteLine($"Day 1 Part 1: {day1[0]}");
Console.WriteLine($"Day 1 Part 2: {day1[1]}");
Console.WriteLine("");

int[] day2 = Day2.Solve(File.ReadAllLines(input + 2));
Console.WriteLine($"Day 2 Part 1: {day2[0]}");
Console.WriteLine($"Day 2 Part 2: {day2[1]}");
Console.WriteLine("");

int[] day3 = Day3.Solve(File.ReadAllLines(input + 3));
Console.WriteLine($"Day 3 Part 1: {day3[0]}");
Console.WriteLine($"Day 3 Part 2: {day3[1]}");
Console.WriteLine("");

int[] day4 = Day4.Solve(File.ReadAllLines(input + 4));
Console.WriteLine($"Day 4 Part 1: {day4[0]}");
Console.WriteLine($"Day 4 Part 2: {day4[1]}");
Console.WriteLine("");