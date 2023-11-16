using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rat.Collison;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Rat
{
    public class Room
    {
        public TileDoor Map;

        public List<IGameObject> ObjList = new List<IGameObject>();

        Random r = new Random();

        public List<Door> Doors; 

        public Room(TileDoor map)
        {
            Map = map;
            Doors = new List<Door>();
            foreach(DoorPosition d in map.doors)
            {
                Doors.Add(new Door(this, d, CreateDoorBox(d)));
            }
            AddPizza();
        }

        public Room(TileDoor map, Door door)
        {
            Map = map;
            Doors = new List<Door>();
            //Doors.Add(door);
            foreach (DoorPosition d in map.doors)
            {
                if(d != Door.FlipPosition(door.pos))
                {
                    Doors.Add(new Door(this, d, CreateDoorBox(d)));
                }
                else
                {
                    var newdoor = new Door(this,d,CreateDoorBox(d));
                    newdoor.Rooms.Add(door.Rooms[0]);
                    Doors.Add(newdoor);
                }
            }
            AddPizza();
            
        }

        private BoundingRectangle CreateDoorBox(DoorPosition door)
        {
            if(door == DoorPosition.Left)
            {
                return new BoundingRectangle(0, Map.basicTilemap.TileHeight * 7, 1, Map.basicTilemap.TileHeight * 6);
            }
            if (door == DoorPosition.Right)
            {
                return new BoundingRectangle(Map.basicTilemap.TileWidth * Map.basicTilemap.MapWidth, Map.basicTilemap.TileHeight * 7, 1, Map.basicTilemap.TileHeight * 6);
            }
            else if (door == DoorPosition.Top)
            {
                return new BoundingRectangle(Map.basicTilemap.TileWidth * 12, -5, Map.basicTilemap.TileWidth * 8,-4);
            }
            else //if (door == DoorPosition.Bottom)
            {
                return new BoundingRectangle(Map.basicTilemap.TileWidth * 12, Map.basicTilemap.TileHeight * Map.basicTilemap.MapHeight, Map.basicTilemap.TileWidth * 8, 1);
            }
        }

        public void AddPizza()
        {
            int pizzaQuota = r.Next(1, 10); // each room has 1 pizza with max of 10
            int pizzaCount = 0;
            for(int i = Map.basicTilemap.MapWidth;i <Map.basicTilemap.MapWidth * (Map.basicTilemap.MapHeight-1);i++) // iterate through entire map 
            {
                if (Map.basicTilemap.TileIndices[i] == 1 && Map.basicTilemap.TileIndices[i+Map.basicTilemap.MapWidth] != 1) // check air with ground beneath
                {
                    if(r.Next(0,100) > 90) // add pizza 1/10 of time
                    {
                        ObjList.Add(new Pizza(new Vector2(Map.basicTilemap.TileHeight * (i % Map.basicTilemap.MapWidth), Map.basicTilemap.TileHeight * (i / Map.basicTilemap.MapWidth)), (PizzaFlavor)r.Next(0, 8)));
                        pizzaCount++;
                    }
                }
                if (pizzaCount > pizzaQuota) return;
            }
        }

        public void LoadContent(ContentManager content)
        {
            foreach(IGameObject obj in ObjList)
            {
                obj.LoadContent(content);
            }
        }

        public void Update(GameTime gametime , BoundingRectangle playerHit)
        {
            foreach (IGameObject obj in ObjList)
            {
                obj.Update(gametime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Map.basicTilemap.Draw(spriteBatch);
            foreach (IGameObject obj in ObjList)
            {
                obj.Draw(spriteBatch);
            }
        }


    }
}
