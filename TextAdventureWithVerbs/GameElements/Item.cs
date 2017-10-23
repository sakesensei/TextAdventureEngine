using System;

using GameSystem;
using Enums;

namespace GameElements
{
	public class Item : Base
	{
		private string _place;
		private bool _isPickable;
		private bool _isUsable;

		public string Place
		{
			get
			{
				return _place;
			}
			set
			{
				_place = value;
			}
		}
		public bool IsPickable
		{
			get
			{
				return _isPickable;
			}
			set
			{
				_isPickable = value;
			}
		}
		public bool IsUsable
		{
			get
			{
				return _isUsable;
			}
			set
			{
				_isUsable = value;
			}
		}

		public Item(bool isPickable, bool isUsable, string name, string description, string place) : base(name, description)
		{
			IsPickable = isPickable;
			IsUsable = IsUsable;
			Place = place;
		}
	}

	public class Door : Base
	{
		private bool _isLocked;

		public bool IsLocked
		{
			get
			{
				return _isLocked;
			}
			set
			{
				_isLocked = value;
			}
		}

		public Door(string name, string description, bool isLocked) : base(name, description)
		{
			IsLocked = isLocked;
		}
	}
}
