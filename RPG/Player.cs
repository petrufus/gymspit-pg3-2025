using System;
using System.Collections.Generic;
using System.IO;

namespace Lecture18
{
    internal class Player : Character
    {
        private TextReader input;
        private TextWriter? prompt;

        public Player(string name, int maxHitPoints, int attack, int defense, TextReader input, TextWriter? prompt = null) :
            base(name, maxHitPoints, attack, defense)
        {
            this.input = input;
            this.prompt = prompt;
        }

        public override Character ChooseTarget(List<Character> potentialTargets)
        {
            if (potentialTargets.Count == 1) return potentialTargets[0];

            while (true)
            {
                if (prompt != null)
                {
                    prompt.WriteLine("Choose a target:");
                    for (int i = 0; i < potentialTargets.Count; i++)
                    {
                        prompt.WriteLine($"({i + 1}) {potentialTargets[i].Name} [{potentialTargets[i].HitPoints} HP]");
                    }
                }

                string choice = input.ReadLine();
                if (int.TryParse(choice, out int index) && index >= 1 && index <= potentialTargets.Count)
                {
                    return potentialTargets[index - 1];
                }

                if (prompt != null) prompt.WriteLine("Invalid target!");
            }
        }

        protected override string ChooseAction(Character target)
        {
            while (true)
            {
                if (prompt != null)
                {
                    prompt.WriteLine($"Your Turn! (Current Armor Bonus: {ArmorBonus})");
                    prompt.WriteLine("Choose an action:");
                    prompt.WriteLine("(A)ttack target: {0}", target.Name);
                    prompt.WriteLine("(D)efense (Raise Armor Bonus)");
                    prompt.WriteLine("(W)ait");
                }

                string choice = input.ReadLine();
                if (choice == null) return null;

                switch (choice.ToLower())
                {
                    case "a":
                    case "attack":
                        return TURN_CHOICE_ATTACK;
                    case "d":
                    case "defense":
                        return TURN_CHOICE_DEFENSE;
                    case "w":
                    case "wait":
                        return TURN_CHOICE_WAIT;
                }

                if (prompt != null) prompt.WriteLine("Invalid choice!");
            }
        }
    }
}

