using System;
using System.Collections.Generic;
using Units;
using GameSystem;
using Enums;

namespace TextAdventureBattle
{
	class Program
	{
		public static Random rng = new Random();
		public static bool isValid = true;

		static void Main(string[] args)
		{
			// Init
			Soldier Hero = new Soldier("Hero", 100, 30, 30, 3, 2) { IsHero = true };
			Soldier Enemy = new Soldier("Enemy", 100, 30, 20, 15, 14) { IsHero = false };

			Battle.Init(Hero, Enemy);

			string input;

			// Main Loop
			while (Hero.IsAlive && Enemy.IsAlive)
			{
				Battle.Print($"Your hero is currently on ({Hero.X}, {Hero.Y}) and has {Hero.Health} Health Points.", Hero.IsHero);
				Battle.Print($"The enemy is currently on ({Enemy.X}, {Enemy.Y}) and has {Enemy.Health} Health Points.", Enemy.IsHero);

				Console.WriteLine("You can: \"strike\", \"move\", \"wait\".\n");

				Console.Write("> ");
				input = Console.ReadLine();

				// Hero Turn
				switch (input)
				{
					case "strike": Hero.Strike(Enemy, rng.Next(-5, 5)); isValid = true; break;
					case "move": Hero.Move(Enemy.X, Enemy.Y); isValid = true; break;
					//case "Attack": Hero.Attack(Enemy, rng.Next(-5, 4)); isValid = true; break;
					//case "Info": Hero.GetPosition(Hero); isValid = true; break;
					case "wait": isValid = true; break;
					default: Console.WriteLine("Invalid command - Try Again."); isValid = false; break;
				}

				if (isValid)
				{
					Battle.AutoAttack(Enemy, Hero, rng.Next(-5, 5));
					Console.WriteLine("\n=========================================");
				}
			}
			Console.ReadLine();
		}
	}
}
