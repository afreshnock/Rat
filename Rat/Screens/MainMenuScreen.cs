using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rat.Screens
{
    public class MainMenuScreen : Screen
    {
        public int menuCounter { get; private set; } = 0;
        private KeyboardState prev, current;
        private SpriteFont _font;
        private float[] scales = new float[] { 1, 1, 1, 1 };
        private bool scaling = true;

        public MainMenuScreen(Game game) :base(game){ }

        public override void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("File");
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            prev = current;
            current = Keyboard.GetState();
            if ((prev.IsKeyUp(Keys.Down) && current.IsKeyDown(Keys.Down)) || (prev.IsKeyUp(Keys.S) && current.IsKeyDown(Keys.S)))
            {
                menuCounter++;
                if (menuCounter == 4) menuCounter = 3;
                scales = new float[] { 1, 1, 1, 1 };
            }
            else if ((prev.IsKeyUp(Keys.Up) && current.IsKeyDown(Keys.Up)) || (prev.IsKeyUp(Keys.W) && current.IsKeyDown(Keys.W)))
            {
                menuCounter--;
                if (menuCounter == -1) menuCounter = 0;
                scales = new float[] { 1, 1, 1, 1 };
            }
            if (scales[menuCounter] > 1.5) scaling = false;
            else if (scales[menuCounter] < 1) scaling = true;
            if (scaling) scales[menuCounter] += .005f;
            else scales[menuCounter] -= .005f;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, "Play Game", new Vector2(800 / 2 - 100, 100), Color.SaddleBrown, 0, Vector2.Zero, scales[0], SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Controls", new Vector2(800 / 2 - 80, 200), Color.SaddleBrown, 0, Vector2.Zero, scales[1], SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Credits", new Vector2(800 / 2 - 70, 300), Color.SaddleBrown, 0, Vector2.Zero, scales[2], SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Exit", new Vector2(800 / 2 - 50, 400), Color.SaddleBrown, 0, Vector2.Zero, scales[3], SpriteEffects.None, 0);
            
            spriteBatch.End();
            base.Draw(spriteBatch);
        }

    }
}
