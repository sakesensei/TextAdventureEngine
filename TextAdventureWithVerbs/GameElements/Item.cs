using System;

using GameSystem;
using Enums;

namespace GameElements
{
	public class Item : Base
	{
		private string place;

		public string Place
		{
			get { return place; }
			set { place = value; }
		}

		public Item(string name, string description, string place) : base(name, description)
		{
			Place = place;
		}
	}
}
