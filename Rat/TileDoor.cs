using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rat
{
    public class TileDoor
    {
        public BasicTilemap basicTilemap;

        public List<DoorPosition> doors;

        public TileDoor(BasicTilemap basicTilemap, List<DoorPosition> doors)
        {
            this.basicTilemap = basicTilemap;
            this.doors = doors;
        }
    }
}
