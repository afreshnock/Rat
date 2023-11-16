using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rat
{
    public static class RoomManager
    {
        public static List<TileDoor> AllMaps;

        private static Random r = new Random();

        //static RoomManager()
        //{
        //    LoadMaps();
        //}

        public static Room CreateRoom(DoorPosition doorPos,Door door)
        {
            Room r = new Room(SelectMap(doorPos),door);
            return r;
        }

        public static void LoadMaps(ContentManager content)
        {
            AllMaps = new List<TileDoor>
            {
                new TileDoor(content.Load<BasicTilemap>("rat3"), new List<DoorPosition> { DoorPosition.Left, DoorPosition.Right, DoorPosition.Top, DoorPosition.Bottom }),
                new TileDoor(content.Load<BasicTilemap>("leftenterance"),new List<DoorPosition> { DoorPosition.Left }),
                new TileDoor(content.Load<BasicTilemap>("rightenterance"), new List<DoorPosition>{DoorPosition.Right}),
                new TileDoor(content.Load<BasicTilemap>("TopEnterance"), new List<DoorPosition>{DoorPosition.Top}),
                new TileDoor(content.Load<BasicTilemap>("BottomEntrance"), new List<DoorPosition>{DoorPosition.Bottom})
            };
        }

        private static TileDoor SelectMap(DoorPosition doorPos)
        {
            IEnumerable<TileDoor> possibleMaps = from map in AllMaps where map.doors.Contains(doorPos) select map;
            return possibleMaps.ElementAt(r.Next(possibleMaps.Count()));

        }
    }
}
