using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lecture18
{
    internal class Game
    {
        private List<Character> allCombatants;
        private Die die;

        public Game(List<Character> combatants, Die die)
        {
            this.allCombatants = combatants;
            this.die = die;
        }

        public void Run(TextWriter output)
        {
            output.WriteLine("=== Let the games begin! ===");

            foreach (var character in allCombatants)
            {
                character.Reset();
            }

            // Establish a predictable play order (Player goes first, or sort by a stat if preferred)
            // Here, we maintain the order they were supplied in
            output.WriteLine("\n--- Turn Rotation Established ---");
            for (int i = 0; i < allCombatants.Count; i++)
            {
                output.WriteLine($"{i + 1}. {allCombatants[i].Name}");
            }
            output.WriteLine("---------------------------------\n");

            while (IsGameOver() == false)
            {
                foreach (var active in allCombatants)
                {
                    if (!active.Alive) continue; // Skip dead combatants
                    if (IsGameOver()) break;     // End instantly if victory conditions met mid-round

                    output.WriteLine($"*** {active.Name}'s turn ***");

                    // Find enemies (everyone else who isn't this character)
                    var enemies = allCombatants.Where(c => c != active && c.Alive).ToList();

                    active.TakeTurn(output, enemies, die);
                    output.WriteLine();

                    PrintAllStatuses(output);
                    output.WriteLine(new string('-', 30));
                }
            }

            output.WriteLine("\n=== GAME OVER! ===");
            var survivors = allCombatants.Where(c => c.Alive).ToList();
            if (survivors.Count == 1)
            {
                output.WriteLine("The ultimate winner is {0}!", survivors[0].Name);
            }
            else
            {
                output.WriteLine("The battlefield falls quiet. No one survives.");
            }
        }

        private bool IsGameOver()
        {
            // Game ends if only 1 character (or 0) remains alive
            return allCombatants.Count(c => c.Alive) <= 1;
        }

        private void PrintAllStatuses(TextWriter output)
        {
            foreach (var character in allCombatants)
            {
                output.WriteLine(
                    "{0}: {1} | {2}/{3} HP | Armor Bonus: {4}",
                    character.Name,
                    character.Alive ? "ALIVE" : "DEAD ",
                    character.HitPoints,
                    character.MaxHitPoints,
                    character.ArmorBonus
                );
            }
        }
    }
}


