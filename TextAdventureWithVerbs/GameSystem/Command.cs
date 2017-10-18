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


	}
}