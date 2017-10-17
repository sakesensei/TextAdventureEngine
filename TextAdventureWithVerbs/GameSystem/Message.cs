using System;
using Enums;
using GameElements;


namespace TextAdventureWithVerbs
{
	public static class Message
	{
		public static void Name(Room room)
		{
			Console.WriteLine($"You're currently at: {room.Name}");
		}

		public static void Description(string message)
		{
			if (message != string.Empty)
			{
				Console.WriteLine(message);
			}
		}
	}
}
