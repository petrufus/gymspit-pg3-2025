using System;
using System.Collections.Generic;

namespace Lecture18
{
    internal class NPC : Character
    {
        private Random random;

        public NPC(string name, int maxHitPoints, int attack, int defense, Random random) :
            base(name, maxHitPoints, attack, defense)
        {
            this.random = random;
        }

        // Smart Target Selection: Focus fire on the weakest enemy to remove them from play
        public override Character ChooseTarget(List<Character> potentialTargets)
        {
            Character weakest = potentialTargets[0];
            foreach (var target in potentialTargets)
            {
                if (target.HitPoints < weakest.HitPoints)
                {
                    weakest = target;
                }
            }
            return weakest;
        }

        // Smart Actions: Defend if health drops too low, otherwise execute attacks
        protected override string ChooseAction(Character target)
        {
            double healthPercentage = (double)HitPoints / MaxHitPoints;

            if (healthPercentage < 0.35)
            {
                // Desperate defense! 75% chance to defend when low health
                return random.NextDouble() < 0.75 ? TURN_CHOICE_DEFENSE : TURN_CHOICE_ATTACK;
            }

            // Normal state: 80% chance to attack, 20% chance to build up armor early
            return random.NextDouble() < 0.80 ? TURN_CHOICE_ATTACK : TURN_CHOICE_DEFENSE;
        }
    }
}

