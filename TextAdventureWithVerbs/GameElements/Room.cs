using System;
using System.Collections.Generic;

using Enums;

namespace GameElements
{
	public class Room : Base
	{
		private Dictionary<Direction, Room> _exits = new Dictionary<Direction, Room>();
		private Dictionary<string, Item> _items = new Dictionary<string, Item>();

		public Dictionary<Direction, Room> Exits
		{
			get { return _exits; }
			set { _exits = value; }
		}
		public Dictionary<string, Item> Items
		{
			get { return _items; }
			set { _items = value; }
		}

		public Room(string name, string description) : base(name, description){}

		public string Look()
		{
			return Description;
		}
	}
}
