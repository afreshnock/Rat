using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rat.Screens
{
    public class CreditScreen:Screen
    {
        private SpriteFont _font;

        Texture2D background;

        public CreditScreen(Game game) : base(game) { }

        public override void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("File");

            background = content.Load<Texture2D>("ratbackground");
            base.LoadContent(content);
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);


            spriteBatch.DrawString(_font, "Thanks to: ", new Vector2(800 / 2 - 200, 100), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Alex Como for pizza art and sound effects", new Vector2(800 / 2 - 200, 200), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Nathan Bean for his guidance and tutorials", new Vector2(800 / 2 - 200, 300), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "PixaBay for water drip sounds", new Vector2(800 / 2 - 200, 400), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Everyone who helped playtest", new Vector2(800 / 2 - 200, 500), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}
