using Lecture18;
using System;
using System.Collections.Generic;

Random random = new Random();
Die commonDie = new Die(random, 6);

// Instantiate Player and 3 distinct NPCs (2 brand new ones added)
Character luke = new Player("Luke (Player)", 25, 6, 3, Console.In, Console.Out);
Character c3PO = new NPC("C-3PO (NPC)", 15, 4, 4, random);
Character r2D2 = new NPC("R2-D2 (NPC)", 12, 5, 5, random);
Character bobaFett = new NPC("Boba Fett (NPC)", 20, 7, 2, random);
Character stormtrooper = new NPC("Stormtrooper (NPC)", 10, 4, 1, random);

// Bundle all of them into a single play order list
List<Character> combatants = new List<Character> { luke, c3PO, r2D2, bobaFett, stormtrooper };

// Initialize and execute game loop
Game game = new Game(combatants, commonDie);
game.Run(Console.Out);

Console.ReadLine();
