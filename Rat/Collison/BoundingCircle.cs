using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Rat.Collison
{
    public struct BoundingCircle
    {
        public Vector2 Center;

        public float Radius;

        public BoundingCircle(Vector2 center, float radius)
        {
            this.Center = center;
            this.Radius = radius;
        }

        public bool CollidesWith(BoundingCircle other)
        {
            return CollisonHelper.Collides(this, other);
        }
        public bool CollidesWith(BoundingRectangle other)
        {
            return CollisonHelper.Collides(this, other);
        }

    }
}
