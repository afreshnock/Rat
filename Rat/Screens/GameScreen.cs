using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rat.Collison;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Rat.Screens
{
    public class GameScreen : Screen
    {
        private BasicTilemap _bMap;
        private Player _player;
        private int[] tileIndexArray;
        private SpriteFont _spriteFont;
        private Vector2 _spawnPos = new Vector2(128, 800);
        public bool NextLevel = false;
        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            _player = new Player(_spawnPos, Color.White, 1);
            tileIndexArray = new int[9];
            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            
            _spriteFont = content.Load<SpriteFont>("File");
            _player.LoadContent(content);
            _bMap = content.Load<BasicTilemap>("ratlevel1");
            base.LoadContent(content);
        }


        public override void Update(GameTime gameTime)
        {

            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.R))
            {
                _player.posistion = _spawnPos;
                _player.velocity = Vector2.Zero;
            }
            //_priorKeyboardState = keyboardState;
            _player.Update(gameTime);

            int playerIndex = ((int)_player.posistion.Y / 64 * 30) + ((int)_player.posistion.X / 64);
            //int playerindex = _tilemap.TileIndices[index] - 1;
            tileIndexArray[0] = playerIndex - 1 - 30;
            tileIndexArray[1] = playerIndex - 30;
            tileIndexArray[2] = playerIndex - 30 + 1;
            tileIndexArray[3] = playerIndex - 1;
            tileIndexArray[4] = playerIndex;
            tileIndexArray[5] = playerIndex + 1;
            tileIndexArray[6] = playerIndex + 30 - 1;
            tileIndexArray[7] = playerIndex + 30;
            tileIndexArray[8] = playerIndex + 30 + 1;
            // need to add logic for edge cases when player is in edge of map
            //[0][1][2]
            //[3][*][5]
            //[6][7][8]

            for (int i = 0; i < tileIndexArray.Length; i++)
            {
                _player.UpdateBounds();
                switch (_bMap.TileIndices[tileIndexArray[i]])
                {
                    case 1:
                        break;
                    case 10:
                        NextLevel = true;
                        break;

                    default:
                        var bounds = new BoundingRectangle(64 * (tileIndexArray[i] % 30), 64 * ((tileIndexArray[i] / 30)), 64, 64);

                        if (_player.bounds.CollidesWith(bounds))
                        {
                            switch (i + 1)
                            {
                               
                                case 4:
                                    _player.posistion.X = bounds.Right + 33;
                                    break;
                                

                                case 6:
                                    _player.posistion.X = bounds.Left - 33;
                                    break;
                               
                                default:
                                    break;
                            }
                        }
                        _player.UpdateBounds();
                        if (_player.feet.CollidesWith(bounds))
                        {
                            _player.posistion.Y = bounds.Top - 32;
                            _player.onGround = true;
                        }
                        break;

                }
            }



            base.Update(gameTime);
        }


        public override void Draw(SpriteBatch spriteBatch)
        {

            float playerX = MathHelper.Clamp(_player.posistion.X, 500, 1620);
            float playerY = MathHelper.Clamp(_player.posistion.Y, 300, 1080);

            float offsetX = 500 - playerX;
            float offsetY = 300 - playerY;

            Matrix transform = Matrix.CreateTranslation(offsetX, offsetY, 0);

            spriteBatch.Begin(transformMatrix: transform);
            
            _bMap.Draw( spriteBatch);
            spriteBatch.DrawString(_spriteFont, "Press r to reset", new Vector2(700, 1200), Color.SaddleBrown, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_spriteFont, "Use A and D to move and SPACE to Jump", _spawnPos * Vector2.One * .9f, Color.SaddleBrown, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.DrawString(_spriteFont, "Enter door for next level", new Vector2(1400,700), Color.SaddleBrown, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            _player.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(spriteBatch);
        }

        
    }
}
