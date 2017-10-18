using System;
using System.Collections.Generic;

using Enums;

namespace GameElements
{
	public class Room : Base
	{
		private string _firstDescription;
		private bool _isFirstTime = true;
		private Dictionary<Direction, Room> _exits = new Dictionary<Direction, Room>();
		private Dictionary<string, Item> _items = new Dictionary<string, Item>();

		public string FirstDescription
		{
			get { return _firstDescription; }
			private set { _firstDescription = value; }
		}
		public bool IsFirstTime
		{
			get { return _isFirstTime; }
			set { _isFirstTime = value; }
		}
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

		public Room(string name, string description, string firstDescription) : base(name, description)
		{
			FirstDescription = firstDescription;
		}

		public string Look()
		{
			return Description;
		}
	}
}
