namespace aoc2019.day14
{
    public class Day14
    {
        static public string[] Solve(string[] input) {

            var reactiondict = ParseReactions(input);
            long part1 = CalculateOre(1);
            long part2 = SpendOre(1000000000000);
            return [part1.ToString(), part2.ToString()];

            long SpendOre(long ore)
            {
                long fuel = ore / part1;
                long increment = 1;
                while (CalculateOre(fuel+increment) < ore)
                {
                    increment *= 10;
                }
                while (increment > 0)
                {
                    long result = 0;
                    while (result < ore)
                    {
                        fuel += increment;
                        result = CalculateOre(fuel);
                    }
                    fuel -= increment;
                    increment /= 10;
                }
                return fuel;
            }

            long CalculateOre(long fuel)
            {
                Dictionary<string, long> needs = [];
                Dictionary<string, long> surplus = [];
                needs["FUEL"] = fuel;
                long ores = 0;
                while (needs.Count > 0)
                {
                    var need = needs.First();
                    {
                        if (need.Key == "ORE")
                        {
                            ores += need.Value; // it's free ore
                        }
                        else
                        {
                            surplus.TryGetValue(need.Key, out long surplusvalue);
                            if (surplusvalue >= need.Value)
                            {
                                surplus[need.Key] -= need.Value;
                            }
                            else
                            {
                                surplus[need.Key] = 0;
                                long produced = ExecuteReaction(need.Key, need.Value - surplusvalue);
                                long extra = produced - need.Value;
                                surplus[need.Key] = surplusvalue + extra;
                            }
                        }
                        needs.Remove(need.Key);
                    }
                }
                return ores;

                long ExecuteReaction(string product, long count)
                {
                    Reaction reaction = reactiondict[product];
                    long reactions = (count + reaction.quantity - 1) / reaction.quantity;
                    foreach (Ingredient ingredient in reaction.list)
                    {
                        needs.TryGetValue(ingredient.name, out long needed);
                        needs[ingredient.name] = ingredient.quantity * reactions + needed;
                    }
                    return reaction.quantity * reactions;
                }
            }

            Dictionary<string, Reaction> ParseReactions(string[] input)
            {
                Dictionary<string, Reaction> dict = [];
                foreach (string line in input)
                {
                    List<Ingredient> list = [];
                    string[] split = line.Split(" => ");
                    string[] result = split[1].Split(" ");
                    string[] ingredients = split[0].Split(", ");
                    foreach (string ingredient in ingredients)
                    {
                        list.Add(new(ingredient.Split(" ")));
                    }
                    dict.Add(result[1], new Reaction(long.Parse(result[0]), list));
                }
                return dict;
            }
        }

        class Ingredient(string[] raw)
        {
            public long quantity = long.Parse(raw[0]);
            public string name = raw[1];
        }

        class Reaction(long quantity, List<Ingredient> list)
        {
            public long quantity = quantity;
            public List<Ingredient> list = list;
        }
    }
}