using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Rat.Screens
{
    public class WinScreen : Screen
    {
        private SpriteFont _font;

        public float Time;

        public int highScore;

        public int yourScore;

        public bool saved = false;

        Texture2D background;

        public WinScreen(Game game) : base(game) { }

        

        public override void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("File");
            background = content.Load<Texture2D>("ratbackground");
            LoadJson();
            base.LoadContent(content);
        }

        public void LoadJson()
        {

            using (StreamReader r = new StreamReader("Highscore.json"))
            {
                string score = r.ReadToEnd();
                int items = JsonConvert.DeserializeObject<int>(score);
                highScore = items;
            }
        }

        public void SaveScore()
        {
            string score = JsonConvert.SerializeObject(highScore);
            File.WriteAllText("./Highscore.json", score);
            saved = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Vector2(0, 0), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "You Starved to Death!", new Vector2(800 / 2 - 100, 100), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Score: " + yourScore.ToString(), new Vector2(800 / 2 - 80, 200), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "Time: " +Time.ToString("0.0") + " seconds", new Vector2(800 / 2 - 80, 300), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_font, "High Score: " + highScore.ToString(), new Vector2(800 / 2 - 70, 400), Color.ForestGreen, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();
            base.Draw(spriteBatch);
        }
    }
}
