using System;
using System.Collections.Generic;

using Enums;
using GameElements;

namespace GameSystem
{
	public static class Init
	{
		public static List<Room> World = new List<Room>();

		public static Room InitRooms()
		{
			Room mainRoom = new Room("Main Hall", "You're in a big hall. There's a door West.");
			Room mainRoomCorridor = new Room("Hall Corridor", "You're in a long corridor. There's a Hall to the East.");

			World.Add(mainRoom);
			World.Add(mainRoomCorridor);

			mainRoom.Exits.Add(Direction.east, mainRoomCorridor);
			mainRoomCorridor.Exits.Add(Direction.west, mainRoom);

			return mainRoom;
		}

		public static void InitItems()
		{
			// TODO
			Item myItem = new Item("Rock", "It's just a rock...", "on the floor.");
		}

		/*
		public static void InitItemsInRoom(Room room, Item[] item)
		{
			for (int i = 0; i < item.Length; i++)
			{
				room.Items.Add(item[i]);
			}
		}
		*/
		public static void PutItemsInRooms()
		{
			
		}
	}
}