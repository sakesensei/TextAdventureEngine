using System;
using System.Threading;
using GameSystem;

namespace Units
{
	public class Soldier
	{
		// Properties
		private bool isAlive = true;
		private bool isHero;
		private string name;
		private int health;
		private int strenght;
		private int armor;
		private int x;
		private int y;

		//private static int moveDelay = 20;
		private static int attackDelay = 300;

		public bool IsAlive
		{
			get { return isAlive; }
			set { isAlive = value; }
		}
		public bool IsHero
		{
			get { return isHero; }
			set { isHero = value; }
		}
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		public int Health
		{
			get { return health; }
			set { health = value; }
		}
		public int Strenght
		{
			get { return strenght; }
			set { strenght = value; }
		}
		public int Armor
		{
			get { return armor; }
			set { armor = value; }
		}
		public int X
		{
			get { return x; }
			set { x = value; }
		}
		public int Y
		{
			get { return y; }
			set { y = value; }
		}

		// Constructor
		public Soldier(string name, int health, int strenght, int armor, int x, int y)
		{
			Name = name;
			Health = health;
			Strenght = strenght;
			Armor = armor;
			X = x;
			Y = y;
		}

		// Strike target
		public void Strike(Soldier enemy, int multiplier)
		{
			if (X == enemy.X && Y == enemy.Y)
			{
				if (enemy.Health > 0)
				{
					int damage = (this.Strenght + multiplier) - (enemy.Armor / 5);
					if (enemy.Health - damage > 0)
					{
						enemy.Health -= damage;
						Battle.Print($"{this.Name} attacked {enemy.Name}! {damage} damage points were dealt! {enemy.Name} health is now {enemy.Health}.", IsHero);
					}
					else
					{
						enemy.IsAlive = false;
						Battle.Print($"Battle ended! {enemy.Name} is dead!", IsHero);
					}
					Thread.Sleep(attackDelay);
				}
			}
			else
			{
				Console.WriteLine($"{enemy.Name} is too far away to attack.");
			}
		}
		// Move to Place
		public void Move(int x, int y)
		{
			if (this.X != x || this.Y != y)
			{
				X += Math.Sign(x - this.X);
				Y += Math.Sign(y - this.Y);

				GetPosition(this);
			}
			else if (this.X == x && this.Y == y)
			{
				Battle.Print("Already near the Enemy.", this.IsHero);
			}
			//Thread.Sleep(300);
		}
		// Go Attack
		public void Attack(Soldier enemy, int multiplier)
		{
			do
			{
				Move(enemy.X, enemy.Y);
			}
			while (X != enemy.X && Y != enemy.Y);
			Strike(enemy, multiplier);
		}
		// Get Position
		public void GetPosition(Soldier unit)
		{
			Battle.Print($"{this.Name} is now on: ({X}, {Y}).", unit.isHero);
		}
	}
}
