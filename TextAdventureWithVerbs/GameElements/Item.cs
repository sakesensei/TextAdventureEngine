using System;

using GameSystem;
using Enums;

namespace GameElements
{
	public class Item : Base
	{
		private string _place;
		private bool _isPickable;

		public string Place
		{
			get { return _place; }
			set { _place = value; }
		}
		public bool IsPickable
		{
			get { return _isPickable; }
			set { _isPickable = value; }
		}

		public Item(bool isPickable, string name, string description, string place) : base(name, description)
		{
			IsPickable = isPickable;
			Place = place;
		}
	}
}
