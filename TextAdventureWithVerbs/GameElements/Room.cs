using System;
using System.Collections.Generic;

using Enums;

namespace GameElements
{
	public class Room : Base
	{
		private Dictionary<Direction, Room> exits = new Dictionary<Direction, Room>();
		private List<Item> items = new List<Item>();

		public Dictionary<Direction, Room> Exits
		{
			get { return exits; }
			set { exits = value; }
		}
		public List<Item> Items
		{
			get { return items; }
			set { items = value; }
		}

		public Room(string name, string description) : base(name, description){}

		public string Look()
		{
			return Description;
		}
	}
}
