using aoc2019.day1;
using aoc2019.day2;

string input = "../../../input/";

int[] day1 = Day1.Solve(File.ReadAllLines(input+1));
Console.WriteLine($"Day 1 Part 1: {day1[0]}");
Console.WriteLine($"Day 1 Part 2: {day1[1]}");
Console.WriteLine("");

int[] day2 = Day2.Solve(File.ReadAllLines(input+2));
Console.WriteLine($"Day 2 Part 1: {day2[0]}");
Console.WriteLine($"Day 2 Part 2: {day2[1]}");
Console.WriteLine("");