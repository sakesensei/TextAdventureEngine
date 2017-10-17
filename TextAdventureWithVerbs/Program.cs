using System;
using System.Collections.Generic;
using System.Linq;

using Enums;
using GameElements;
using GameSystem;

namespace TextAdventureWithVerbs
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			#region Initialize
			// Initialize World and Rooms
			List<Room> World = new List<Room>();

			// Initialize Player
			Player player = new Player();

			// Create Rooms
			Room mainRoom = new Room("Main Hall", "You're in a big hall. There's a door to the East.");
			Room mainRoomCorridor = new Room("Hall Corridor", "You're in a long corridor. There's a Hall to the West.");

			// Starting room:
			Room currentRoom = mainRoom;

			// Put Rooms in the World
			World.Add(mainRoom);
			World.Add(mainRoomCorridor);

			// Create Room Exits
			mainRoom.Exits.Add(Direction.east, mainRoomCorridor);
			mainRoomCorridor.Exits.Add(Direction.west, mainRoom);

			// Create Items
			Item itemRock = new Item(true, "Rock", "It's just a simple rock...", "on the floor.");

			// Put Items inside Rooms
			mainRoom.Items.Add("rock", itemRock);
		
			/*
			public static void InitItemsInRoom(Room room, Item[] item)
			{
				for (int i = 0; i < item.Length; i++)
				{
					room.Items.Add(item[i]);
				}
			}
			*/

			// Init Variables
			string currentMessage = currentRoom.Description;

			bool isPlaying = true;
			bool isNewRoom = true;
			string playerInput;

			string[] inputArray = new string[2];

			#endregion

			#region Main Game Loop
			while (isPlaying)
			{
				if (isNewRoom)
				{
					// Name the room
					Message.Name(currentRoom);
					// Full room description
					Message.Description(currentMessage);
					// Items in the room
					foreach (var roomItems in currentRoom.Items)
					{
						Message.Description($"There is a {roomItems.Value.Name} {roomItems.Value.Place}");
					}
				}
				else
				{
					Message.Description(currentMessage);
				}

				// Get Console Command
				Console.Write("\n> ");
				playerInput = Console.ReadLine().ToLower();

				Command.GetInput(playerInput, out inputArray);
				Command.Action(inputArray, out string verb, out string target);

				if (playerInput != string.Empty)
				{
					if (Enum.TryParse<Verbs>(verb, out Verbs action))
					{
						switch (action)
						{
							// Movement
							case Verbs.go:
								if (!string.IsNullOrEmpty(target) && Enum.TryParse<Direction>(target, out Direction exit))
								{
									currentRoom.Exits.TryGetValue(exit, out Room destination);
									if (destination != null)
									{
										currentRoom = destination;
										currentMessage = currentRoom.Description;
										isNewRoom = true;
									}
									else
									{
										currentMessage = "There's no exit that way.";
										isNewRoom = false;
									}
								}
								else
								{
									currentMessage = "Go where?";
									isNewRoom = false;
								}
								break;
							// Inspect things
							case Verbs.inspect:
								if (!string.IsNullOrEmpty(target))
								{
									//TODO
									if (currentRoom.Items.TryGetValue(target, out Item inspectItem))
									{
										currentMessage = inspectItem.Description;
									}
									isNewRoom = false;
								}
								else
								{
									currentMessage = currentRoom.Description;
									isNewRoom = false;
								}
								break;
							// Clear Screen
							case Verbs.clear:
								Console.Clear();
								currentMessage = currentRoom.Description;
								isNewRoom = true;
								break;
							// Help
							case Verbs.help:
								if (string.IsNullOrEmpty(target))
								{
									currentMessage = $"You can \"Go\", \"Inspect\", \"Clear\", \"Quit\".";
									isNewRoom = false;
								}
								break;
							//Quit
							case Verbs.quit: isPlaying = false; break;
							default: break;
						}
					}
					else
					{
						currentMessage = "What?";
						isNewRoom = false;
					}
				}
				else
				{
					isNewRoom = false;
				}
			}
			#endregion
		}
	}
}