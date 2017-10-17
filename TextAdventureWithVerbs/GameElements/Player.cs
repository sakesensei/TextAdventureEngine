using System;
using System.Collections.Generic;

using Enums;
using GameSystem;


namespace GameElements
{
	public class Player
	{
		private Dictionary<string, Item> _inventory = new Dictionary<string, Item>();

		public Dictionary<string, Item> Inventory
		{
			get { return _inventory; }
			set { _inventory = value; }
		}
	}
}
