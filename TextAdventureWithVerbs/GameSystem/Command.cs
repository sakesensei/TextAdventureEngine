using System;

using GameElements;
using Enums;

namespace GameSystem
{
	public static class Command
	{
		// Get input
		public static void GetInput(string input, out string[] inputArray)
		{
			input.ToLower();
			input.Trim(' ');
			inputArray = input.Split(' ');
		}
		// Separate verb from item
		public static void Action(string[] inputArray, out string verb, out string item)
		{
			//if (inputArray.Length == 4)
			//{
			//	if (inputArray[0] == "use" && inputArray[2] == "with");
			//	{
			//		verb = inputArray[0];
			//		item = inputArray[1];

			//	}
			//}
			if (inputArray.Length == 2)
			{
				verb = inputArray[0];
				item = inputArray[1];
			}
			else if (inputArray.Length == 1)
			{
				verb = inputArray[0];
				item = "";
			}
			else
			{
				verb = "";
				item = "";
			}
		}
		// Check if string is valid
		public static bool IsValid(string value)
		{
			if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		// Error actions
		public static string ErrorMessage(Verbs action)
		{
			Random RNG = new Random();
			string message = "";

			switch (action)
			{
				case Verbs.go:
					switch (RNG.Next(0, 3))
					{
						case 0: message = "You can't go that way."; break;
						case 1: message = "There's no exit that way."; break;
						case 2: message = "There's no way you can go that way."; break;
						default: break;
					}
					break;
				case Verbs.pick:
					switch (RNG.Next(0, 3))
					{
						case 0: message = "You can't pick that up."; break;
						case 1: message = "You're not able to pick that up."; break;
						case 2: message = "There's no way you're going to pick that up."; break;
					}
					break;
				case Verbs.drop: break;
				case Verbs.use:
					switch (RNG.Next(0, 3))
					{
						case 0: message = "You can't use that."; break;
						case 1: message = "You're not able to use that right now."; break;
						case 2: message = "That's not usable."; break;
					}
					break;
			}
			return message;
		}
		public static string ErrorMessage(string word)
		{
			Random RNG = new Random();
			string message = "";


			switch (RNG.Next(0, 5))
			{
				case 0: message = "What?"; break;
				case 1: message = "What do you mean?"; break;
				case 2: message = "I don't understand."; break;
				case 3: message = $"What do you mean by {word}?"; break;
				case 4: message = $"I don't know what {word} means."; break;
				default: break;
			}

			return message;
		}

	}
}