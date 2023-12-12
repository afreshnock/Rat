using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rat.Screens
{
    public class ControlsScreen : Screen
    {
        private SpriteFont _font;
        
        Texture2D background;

        public ControlsScreen(Game game) : base(game) { }

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
            

            spriteBatch.DrawString(_font, "Use A to move left and D to move Right", new Vector2(800 / 2 - 200, 100), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Press Space to Jump", new Vector2(800 / 2 - 200, 200), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Hold S to fall through slim platforms", new Vector2(800 / 2 - 200, 300), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Press ESC to go back to main menu", new Vector2(800 / 2 - 200, 400), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}
