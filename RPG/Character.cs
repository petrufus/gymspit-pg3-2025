namespace Lecture18
{
    internal abstract class Character
    {
        protected const string TURN_CHOICE_ATTACK = "attack";
        protected const string TURN_CHOICE_WAIT = "wait";
        protected const string TURN_CHOICE_DEFENSE = "defense"; // Added defense action

        private string name;
        private int hitPoints;
        private int maxHitPoints;
        private int attack;
        private int defense;
        private int armorBonus; // Added armor bonus tracking

        public string Name => name;
        public int HitPoints => hitPoints;
        public int MaxHitPoints => maxHitPoints;
        public bool Alive => hitPoints > 0;
        public int Attack => attack;
        public int Defense => defense;
        public int ArmorBonus => armorBonus; // Expose armor bonus

        public Character(string name, int maxHitPoints, int attack, int defense)
        {
            this.name = name;
            this.maxHitPoints = maxHitPoints;
            this.attack = attack;
            this.defense = defense;
            Reset();
        }

        public void Reset()
        {
            hitPoints = maxHitPoints;
            armorBonus = 0; // Reset armor bonus to 0 at start
        }

        // Changed signature to accept a list of possible targets for better AI targeting
        public abstract Character ChooseTarget(List<Character> potentialTargets);
        protected abstract string ChooseAction(Character target);

        public void TakeTurn(TextWriter output, List<Character> enemies, Die die)
        {
            // Filter alive enemies
            var aliveEnemies = enemies.Where(e => e.Alive).ToList();
            if (aliveEnemies.Count == 0) return;

            Character target = ChooseTarget(aliveEnemies);
            string action = ChooseAction(target);

            switch (action)
            {
                case TURN_CHOICE_ATTACK:
                    AttackEnemy(output, target, die);
                    break;

                case TURN_CHOICE_DEFENSE:
                    Defend(output, die);
                    break;

                case TURN_CHOICE_WAIT:
                    Wait(output, die);
                    break;

                default:
                    output.WriteLine("{0} does nothing...", name);
                    break;
            }
        }

        private void AttackEnemy(TextWriter output, Character enemy, Die die)
        {
            int attackRoll = attack + die.Roll();
            output.WriteLine("{0} attacks {1}!", name, enemy.Name);
            enemy.ReceiveAttack(output, attackRoll, die);
        }

        private void ReceiveAttack(TextWriter output, int attackRoll, Die die)
        {
            // armorBonus dynamically adds to the total defense calculation
            int defenseRoll = defense + armorBonus + die.Roll();
            int damage = attackRoll - defenseRoll;

            if (damage > 0)
            {
                hitPoints -= damage;
                output.WriteLine("{0} takes {1} damage!", name, damage);
            }
            else
            {
                output.WriteLine("{0} takes no damage!", name);
            }

            // Armor bonus decreases when attacked (regardless of damage taken, as requested)
            if (armorBonus > 0)
            {
                armorBonus -= 5;
                output.WriteLine("{0}'s armor bonus decreased to {1}!", name, armorBonus);
            }
        }

        private void Defend(TextWriter output, Die die)
        {
            int boost = die.Roll();
            armorBonus += boost;
            output.WriteLine("{0} hunkers down and prepares a defense! Armor bonus increases by +{1} (Current Armor Bonus: {2}).", name, boost, armorBonus);
        }

        private void Wait(TextWriter output, Die die)
        {
            output.WriteLine("{0} waits and rolls a die...", name);
            output.WriteLine("They rolled a {0}!", die.Roll());
        }
    }
}

