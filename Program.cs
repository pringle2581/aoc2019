﻿using aoc2019.day1;
using aoc2019.day2;
using aoc2019.day3;
using aoc2019.day4;
using aoc2019.day5;
using aoc2019.day6;
using aoc2019.day7;
using aoc2019.day8;
using aoc2019.day9;
using aoc2019.day10;
using aoc2019.day11;
using aoc2019.day12;
using aoc2019.day13;
using aoc2019.day14;
using aoc2019.day15;
using aoc2019.day16;
using aoc2019.day17;

string input = "../../../input/";

static void PrintResults(int day, string[] results)
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
PrintResults(9, Day9.Solve(File.ReadAllLines(input + 9)));
PrintResults(10, Day10.Solve(File.ReadAllLines(input + 10)));
PrintResults(11, Day11.Solve(File.ReadAllLines(input + 11)));
PrintResults(12, Day12.Solve(File.ReadAllLines(input + 12)));
PrintResults(13, Day13.Solve(File.ReadAllLines(input + 13)));
PrintResults(14, Day14.Solve(File.ReadAllLines(input + 14)));
PrintResults(15, Day15.Solve(File.ReadAllLines(input + 15)));
PrintResults(16, Day16.Solve(File.ReadAllLines(input + 16)));
PrintResults(17, Day17.Solve(File.ReadAllLines(input + 17)));