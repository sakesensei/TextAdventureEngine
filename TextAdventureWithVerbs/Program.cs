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
			Item itemRock = new Item(true, "Rock", "It's just a simple rock...", " on the floor.");
			Item itemWindow = new Item(false, "Window", "You can see the sea. In fact, you can see she sells sea shells by the sea.", ".");


			// Put Items inside Rooms
			mainRoom.Items.Add("rock", itemRock);
			mainRoom.Items.Add("window", itemWindow);
					
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
					if (!currentRoom.Items.GetEnumerator().MoveNext())
					{
						foreach (var roomItems in currentRoom.Items)
						{
							Message.Description($"There is a {roomItems.Value.Name}{roomItems.Value.Place}");
						}
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
							#region Movement
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
							#endregion
							#region Pick up
							case Verbs.pick:
								if (!string.IsNullOrEmpty(target))
								{
									if (currentRoom.Items.TryGetValue(target, out Item pickItem))
									{
										if (pickItem.IsPickable)
										{
											currentMessage = $"Picked up {pickItem.Name}";
											player.Inventory.Add($"{pickItem.Name}", pickItem);
											currentRoom.Items.Remove(pickItem.Name);
										}
										else
										{
											currentMessage = "You can't pick that up";
										}
									}
									isNewRoom = false;
								}
								else
								{
									currentMessage = "Pick what?";
									isNewRoom = false;
								}
								break;
							#endregion
							#region Inventory
							case Verbs.inventory:
								if (player.Inventory.GetEnumerator().MoveNext())
								{
									Console.WriteLine($"Inventory: ");
									foreach (var item in player.Inventory)
									{
										Console.Write($" {item.Key}");
										currentMessage = "";
									}
								}
								else
								{
									currentMessage = "You inventory is empty.";
								}
								isNewRoom = false;
								break;
							#endregion
							#region Inspect Things
							case Verbs.inspect:
								if (!string.IsNullOrEmpty(target))
								{
									if (currentRoom.Items.TryGetValue(target, out Item inspectItem))
									{
										currentMessage = inspectItem.Description;
									}
									isNewRoom = false;
								}
								else
								{
									currentMessage = currentRoom.Description;
									foreach (var roomItems in currentRoom.Items)
									{
										Message.Description($"There is a {roomItems.Value.Name}{roomItems.Value.Place}");
									}
									isNewRoom = false;
								}
								break;
							#endregion
							#region Clear Screen
							case Verbs.clear:
								Console.Clear();
								currentMessage = currentRoom.Description;
								isNewRoom = true;
								break;
							#endregion
							#region Help
							case Verbs.help:
								if (string.IsNullOrEmpty(target))
								{
									currentMessage = $"You can \"Go\", \"Inspect\", \"Clear\", \"Quit\".";
									isNewRoom = false;
								}
								break;
							#endregion
							#region Quit
							case Verbs.quit: isPlaying = false; break;
							#endregion
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