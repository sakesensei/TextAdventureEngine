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
			Room mainRoom = new Room("Small Cell", "You're in a small dark cell.", "You woke up feeling awful in a dark damp tiny cell. You hear distant echos. There's a door to the north.");
			Room cellCorridor = new Room("Cell Corridor", "You're in a long corridor.", "You reach a long and dark corridor. There's a huge holding cell to your right. There's a faint light to the north and a door to the south.");
			Room ladderRoom = new Room("Ladder Room", "You're in a small room. There's a ladder that goes up.", "You're in a small flooded room. There's someone sitting in a corner. There's a rusty ladder that goes Up.");
			Room bonfireRoom = new Room("Bonfire Room", "You're in a large hall. There's a ladder down and some gates south.", "You're in a large hall. In the middle of the hall there's a bonfire with an old sword burning amid the fire. There's a ladder down. A big wooden gate blocks your way to the south.");

			// Starting room:
			Room currentRoom = mainRoom;

			// Put Rooms in the World
			World.Add(mainRoom);
			World.Add(cellCorridor);
			World.Add(ladderRoom);
			World.Add(bonfireRoom);

			// Create Room Exits
			mainRoom.Exits.Add(Direction.north, cellCorridor);
			cellCorridor.Exits.Add(Direction.south, mainRoom);
			cellCorridor.Exits.Add(Direction.north, ladderRoom);
			ladderRoom.Exits.Add(Direction.south, cellCorridor);
			ladderRoom.Exits.Add(Direction.up, bonfireRoom);
			bonfireRoom.Exits.Add(Direction.down, ladderRoom);


			// Create Items
			Item itemCellKey = new Item(true, false, "key", "It's a small rusty cell key.", "on the floor.");
			Item itemCellSkylight = new Item(false, false, "skylight", "There's a couple of bricks missing in the ceiling letting through a faint light.", "above you.");
			Item itemCellDoor = new Item(false, true, "door", "It's a cell door.", "in front of you.");
			Item itemHollow = new Item(false, false, "person", "It's a catatonic husk of a person. There's no response.", "sitting in a corner.");
			Item itemBonfire = new Item(false, false, "bonfire", "It's a rather wild bonfire. Amidst the flames there is an incandescent old sword stuck to the ground.", "in the middle of the hall.");
			Item itemBonfireGates = new Item(false, true, "gate", "Big sturdy heavy old wooden gate, blocking your path to the south.", "on the south wall.");

			// Put Items inside Rooms
			mainRoom.Items.Add("key", itemCellKey);
			mainRoom.Items.Add("skylight", itemCellSkylight);
			mainRoom.Items.Add("door", itemCellDoor);
			ladderRoom.Items.Add("person", itemHollow);
			bonfireRoom.Items.Add("bonfire", itemBonfire);
			bonfireRoom.Items.Add("gate", itemBonfireGates);

			// Init Variables
			string currentMessage = currentRoom.FirstDescription;

			bool isPlaying = true;
			bool isNewRoom = true;
			string playerInput;

			string[] inputArray = new string[4];

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
				playerInput = Console.ReadLine();

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
								if (Command.IsValid(target) && Enum.TryParse<Direction>(target, out Direction exit))
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
										currentMessage = Command.ErrorMessage(action);
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
								if (Command.IsValid(target))
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
											currentMessage = Command.ErrorMessage(action);
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
							#region Drop
							case Verbs.drop:
								if (Command.IsValid(target))
								{
									if (player.Inventory.TryGetValue(target, out Item dropItem))
									{
										// Let's assume the player can drop anything

										player.Inventory.Remove(dropItem.Name);
										currentRoom.Items.Add($"{dropItem.Name}", dropItem);
										currentMessage = $"Dropped {dropItem.Name}";
									}
									else
									{
										currentMessage = "Drop what?";
									}
									isNewRoom = false;
								}
								else
								{
									currentMessage = "Drop what?";
									isNewRoom = false;
								}
								break;
							#endregion
							#region Use
							/*
						case Verbs.use:
							if (!string.IsNullOrEmpty(target))
							{
								Item useItem;
								if (currentRoom.Items.TryGetValue(target, out useItem) || player.Inventory.TryGetValue(target, out useItem))
								{
									currentMessage = $"Used {useItem.Name}";
								}
								else
								{
									currentMessage = "Use what?";
								}
								isNewRoom = false;
							}
							else
							{
								currentMessage = "Use what?";
								isNewRoom = false;
							}
							break;
							*/
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
								if (Command.IsValid(target))
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
									Console.WriteLine($"{currentRoom.FirstDescription}\n");
									Console.WriteLine($"You look arround and see:\n");
									foreach (var roomItems in currentRoom.Items)
									{
										Message.Description($"A {roomItems.Value.Name} {roomItems.Value.Place}");
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
								if (!Command.IsValid(target))
								{
									currentMessage = $"You can \"Go\", \"Pick\", \"Drop\", \"Inspect\", \"Clear\", \"Quit\".";
									isNewRoom = false;
								}
								break;
							#endregion
							#region Quit
							case Verbs.quit:
								isPlaying = false;
								break;
							#endregion
							default:
								currentMessage = Command.ErrorMessage(verb);
								isNewRoom = false;
								break;
						}
					}
					else
					{
						currentMessage = Command.ErrorMessage(verb);
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