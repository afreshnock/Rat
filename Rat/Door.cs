using Rat.Collison;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rat
{
    public class Door
    {
        public DoorPosition pos;

        public List<Room> Rooms;

        public BoundingRectangle boundingRectangle;

        public Door(Room CurrentRoom, DoorPosition pos, BoundingRectangle boundingBox)
        {
            Rooms = new List<Room>
            {
                CurrentRoom
            };
            boundingRectangle = boundingBox;
            this.pos = pos;
        }

        public Door(Room CurrentRoom, Room NextRoom, DoorPosition pos, BoundingRectangle boundingBox)
        {
            Rooms = new List<Room>
            {
                CurrentRoom,
                NextRoom
            };
            boundingRectangle = boundingBox;
            this.pos = pos;
        }

        public Room Open(Room CurretRoom)
        {
            if(Rooms.Count == 1)
            {
                var r = RoomManager.CreateRoom(FlipPosition(pos),this);
                Rooms.Add(r);
                return r;
            }
            else if(Rooms.Count == 2)
            {
                foreach( Room r in Rooms)
                {
                    if (!r.Equals(CurretRoom))
                    {
                        return r;
                    }
                }
            }
            return null;
        }

        
        public static DoorPosition FlipPosition(DoorPosition d)
        {
            if (d == DoorPosition.Left) return DoorPosition.Right;
            else if (d == DoorPosition.Right) return DoorPosition.Left;
            else if (d == DoorPosition.Top) return DoorPosition.Bottom;
            else return DoorPosition.Top;
        }

        
    }
    public enum DoorPosition
    {
        Left = 0,
        Right = 1,
        Top = 2,
        Bottom = 3,
    }
    
}
