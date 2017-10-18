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
			Room mainRoom = new Room("Small Cell", "You're in a small dark cell.", "You wake up feeling awful in a dark damp tiny cell. You hear distant echos. There's a door to the north.");
			Room cellCorridor = new Room("Hall Corridor", "You're in a long corridor.", "You reach a long and dark corridor. There's a huge holding cell to your right. There's a faint light to the north and a door to the south.");

			// Starting room:
			Room currentRoom = mainRoom;

			// Put Rooms in the World
			World.Add(mainRoom);
			World.Add(cellCorridor);

			// Create Room Exits
			mainRoom.Exits.Add(Direction.north, cellCorridor);
			cellCorridor.Exits.Add(Direction.south, mainRoom);


			// Create Items
			Item itemCellKey = new Item(true, "key", "It's a small rusty cell key.", "on the floor.");
			Item itemCellSkylight = new Item(false, "skylight", "There's a couple of bricks missing in the ceiling letting through a faint light.", "above you.");
			Item itemCellDoor = new Item(false, "door", "It's a cell door.", "in front of you.");


			// Put Items inside Rooms
			mainRoom.Items.Add("key", itemCellKey);
			mainRoom.Items.Add("skylight", itemCellSkylight);
			mainRoom.Items.Add("door", itemCellDoor);

			// Init Variables
			string currentMessage = currentRoom.FirstDescription;

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
							Message.Description($"{roomItems.Value.Name} {roomItems.Value.Place}");
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
										currentMessage = (destination.IsFirstTime) ? destination.FirstDescription : destination.Description;
										destination.IsFirstTime = false;

										currentRoom = destination;
										isNewRoom = true;
									}
									else
									{
										currentMessage = "You can't go that way.";
										isNewRoom = false;
									}
								}
								else
								{
									currentMessage = $"Go where?";
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
											currentRoom.Items.Remove(pickItem.Name);
											player.Inventory.Add($"{pickItem.Name}", pickItem);
											currentMessage = $"Picked up {pickItem.Name}";
										}
										else
										{
											currentMessage = "You can't pick that up";
										}
									}
									else
									{
										currentMessage = "Pick what?";
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
									Console.WriteLine($"\nInventory: ");
									foreach (var item in player.Inventory)
									{
										Console.Write($"- {item.Key}");
										currentMessage = "";
									}
								}
								else
								{
									currentMessage = $"\nYou inventory is empty.";
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
									else
									{
										currentMessage = $"There is no such thing as a {target}";
									}
									isNewRoom = false;
								}
								else
								{
									currentMessage = "";
									Console.WriteLine($"You look arround and see:\n");
									foreach (var roomItems in currentRoom.Items)
									{
										Message.Description($"{roomItems.Value.Name} {roomItems.Value.Place}");
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
									currentMessage = $"You can \"Go\", \"Pick\", \"Inspect\", \"Clear\", \"Quit\".";
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