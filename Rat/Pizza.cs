using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rat.Collison;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rat
{
    public class Pizza : IGameObject
    {

        static Texture2D texture;

        Vector2 pos;

        int frame;

        public PizzaFlavor flavor;

        public BoundingRectangle boundingRectangle;

        public bool Collected = false;

        public Pizza(Vector2 pos, PizzaFlavor flavor)
        {
            boundingRectangle = new BoundingRectangle(pos,64,64);
            frame = (int)flavor;
            this.flavor = flavor;
            this.pos = pos;
        }


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("pizza");
        }

        public void Update(GameTime gametime)
        {

        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            if (!Collected) spriteBatch.Draw(texture, pos, new Rectangle(64 * frame, 0, 64, 64), Color.White);
        }
    }

    public enum PizzaFlavor
    {
        Cheese = 0,
        ExtraCheese=1,
        Pepperoni=2,
        Spicy = 3,
        Veggie = 4,
        Marg = 5,
        Stinky = 6,
        Sticks = 7
    }
}
