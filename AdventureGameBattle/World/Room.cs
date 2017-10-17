using System.Collections.Generic;
using Enums;


namespace World
{
	public class Room
	{
		private Dictionary<Direction, Room> exits = new Dictionary<Direction, Room>();
		private string name;
		private string description;

		public Dictionary<Direction, Room> Exits
		{
			get { return exits; }
			set { exits = value; }
		}
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		public string Description
		{
			get { return description; }
			set { description = value; }
		}
	}
}