using System;
using Units;

namespace GameSystem
{
	public static class Battle
	{
		public static void Init(Soldier hero, Soldier enemy)
		{
			hero = new Soldier(hero.Name, hero.Health, hero.Strenght, hero.Armor, hero.X, hero.Y);
			enemy = new Soldier(enemy.Name, enemy.Health, enemy.Strenght, enemy.Armor, enemy.X, enemy.Y);
		}

		public static void Print(string message, bool hero)
		{
			if (hero)
			{
				Console.ForegroundColor = ConsoleColor.Green;
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
			}

			Console.WriteLine($"{message}");

			Console.ResetColor();
		}

		public static void AutoAttack(Soldier enemy, Soldier hero, int multiplier)
		{
			if (enemy.X != hero.X && enemy.Y != hero.Y)
			{
				enemy.Move(hero.X, hero.Y);
			}
			else
			{
				enemy.Strike(hero, multiplier);
			}
		}
	}
}
