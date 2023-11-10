using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rat.Collison;
using Rat.ThreeD;
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
        private BasicTilemap _currentBmap;
        private BasicTilemap [,] WorldMap;
        private Player _player;
        private int[] tileIndexArray;
        private SpriteFont _spriteFont;
        private Vector2 _spawnPos = new Vector2(300, 300);
        public bool NextLevel = false;
        private int WorldmapX = 1;
        private int WorldmapY = 1;
        Billboard bill;
        FPSCamera camera;
        

        public GameScreen(Game game) : base(game)
        {

        }
        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            _player = new Player(_spawnPos, Color.White, 1);
            tileIndexArray = new int[9];
            WorldMap = new BasicTilemap[10,10];
            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            bill = new Billboard(game);
            
            _spriteFont = content.Load<SpriteFont>("File");
            _player.LoadContent(content);
            var middle = content.Load<BasicTilemap>("rat3");
            var leftEnter = content.Load<BasicTilemap>("leftenterance");
            var rightEnter = content.Load<BasicTilemap>("rightenterance");
            var top = content.Load<BasicTilemap>("TopEnterance");
            var bottom = content.Load<BasicTilemap>("BottomEntrance");
            // loads basic map
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0) WorldMap[i, j] = rightEnter;
                    if (i == 1) WorldMap[i, j] = middle;
                    if (i == 2) WorldMap[i, j] = leftEnter;
                }
            }
            WorldMap[1, 0] = bottom;
            WorldMap[1, 2] = top;
            _currentBmap = WorldMap[WorldmapX, WorldmapY];
            camera = new FPSCamera(game, new Vector3(0, 0, 4));
            base.LoadContent(content);
        }


        public override void Update(GameTime gameTime)
        {
            NextLevel = false;
            if (_player.posistion.Y >= _currentBmap.TileHeight * _currentBmap.MapHeight +200) _player.posistion = _spawnPos;
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.R))
            {
                _player.posistion = _spawnPos;
                _player.velocity = Vector2.Zero;
            }
            //_priorKeyboardState = keyboardState;
            _player.Update(gameTime);
            
            TileMapUpdate();
            camera.Update(gameTime);
            base.Update(gameTime);
        }


        private void TileMapUpdate()
        {
            int playerIndex = ((int)_player.posistion.Y / _currentBmap.TileHeight * _currentBmap.MapWidth) + ((int)_player.posistion.X / _currentBmap.TileWidth);
            //int playerindex = _tilemap.TileIndices[index] - 1;
            tileIndexArray[0] = playerIndex - 1 - _currentBmap.MapWidth;
            tileIndexArray[1] = playerIndex - _currentBmap.MapWidth;
            tileIndexArray[2] = playerIndex - _currentBmap.MapWidth + 1;
            tileIndexArray[3] = playerIndex - 1;
            tileIndexArray[4] = playerIndex;
            tileIndexArray[5] = playerIndex + 1;
            tileIndexArray[6] = playerIndex + _currentBmap.MapWidth - 1;
            tileIndexArray[7] = playerIndex + _currentBmap.MapWidth;
            tileIndexArray[8] = playerIndex + _currentBmap.MapWidth + 1;

            // need to add logic for edge cases when player is in edge of map
            //[0][1][2]
            //[3][*][5]
            //[6][7][8]

            for (int i = 0; i < tileIndexArray.Length; i++)
            {
                _player.UpdateBounds();
                if (_player.posistion.Y < 0)
                {
                    WorldmapY--;
                    _currentBmap = WorldMap[WorldmapX, WorldmapY];
                    _player.posistion.Y = (_currentBmap.MapHeight - 1) * _currentBmap.TileHeight;
                    
                }
                else if (_player.posistion.Y > _currentBmap.MapHeight * _currentBmap.TileHeight)
                {
                    WorldmapY++;
                    _currentBmap = WorldMap[WorldmapX, WorldmapY];
                    _player.posistion.Y = 32;
                   
                }
                else if (_player.posistion.X < 0)
                {
                    WorldmapX--;
                    _currentBmap = WorldMap[WorldmapX, WorldmapY];
                    _player.posistion.X = (_currentBmap.MapWidth - 1) * _currentBmap.TileWidth;
                    camera.position = new Vector3(2.5f, 0, 4);
                }
                else if (_player.posistion.X > _currentBmap.MapWidth * _currentBmap.TileWidth)
                {
                    WorldmapX++;
                    _currentBmap = WorldMap[WorldmapX, WorldmapY];
                    _player.posistion.X = 32;
                    camera.position = new Vector3(0, 0, 4);
                }
                if (tileIndexArray[i] > -1 && tileIndexArray[i] <= _currentBmap.MapHeight * _currentBmap.MapWidth)
                {
                    switch (_currentBmap.TileIndices[tileIndexArray[i]])
                    {
                        case 1:
                            break;
                        case 5:
                            var bounds = new BoundingRectangle(_currentBmap.TileHeight * (tileIndexArray[i] % _currentBmap.MapWidth), _currentBmap.TileHeight * ((tileIndexArray[i] / _currentBmap.MapWidth)), _currentBmap.TileHeight, _currentBmap.TileHeight / 4);

                            if (_player.velocity.Y > 0 && _player.feet.CollidesWith(bounds) && _player.FallThrough == false)
                            {
                                _player.posistion.Y = bounds.Top - 32;
                                _player.onGround = true;
                            }
                            break;
                        case 10:
                            if (i == 4)
                            {
                                NextLevel = true;
                                _player.posistion = _spawnPos;
                            }
                            break;

                        default:
                            bounds = new BoundingRectangle(_currentBmap.TileHeight * (tileIndexArray[i] % _currentBmap.MapWidth), _currentBmap.TileHeight * ((tileIndexArray[i] / _currentBmap.MapWidth)), _currentBmap.TileHeight, _currentBmap.TileHeight);

                            if (_player.bounds.CollidesWith(bounds))
                            {
                                switch (i + 1)
                                {
                                    case 2:
                                        _player.posistion.Y = bounds.Bottom + 31;
                                        _player.jumping = false;
                                        _player.velocity.Y = 100;
                                        break;
                                    case 4:
                                        _player.posistion.X = bounds.Right + 31;
                                        break;


                                    case 6:
                                        _player.posistion.X = bounds.Left - 31;
                                        break;
                                    //case 8:
                                    //    _player.posistion.Y = bounds.Top - 31;
                                    //    _player.onGround = true;
                                    //    break;
                                    //case 9:
                                    //    _player.posistion.Y = bounds.Top - 31;
                                    //    _player.onGround = true;
                                    //    break;
                                    default:
                                        break;
                                }
                            }
                            _player.UpdateBounds();
                            if (_player.velocity.Y > 0 && _player.feet.CollidesWith(bounds))
                            {
                                _player.posistion.Y = bounds.Top - 33;
                                _player.onGround = true;
                            }
                            break;

                    }
                }
                

            }
        }



        public override void Draw(SpriteBatch spriteBatch)
        {

            float playerX = MathHelper.Clamp(_player.posistion.X, 500, _currentBmap.TileHeight * (_currentBmap.MapWidth-8) );

            float playerY = MathHelper.Clamp(_player.posistion.Y, 200, 680);
            
            float offsetX = 500 - playerX;
            float offsetY = 200- playerY;

            Matrix transform = Matrix.CreateTranslation(offsetX, offsetY, 0);
            bill.Draw(camera);
            spriteBatch.Begin(transformMatrix: transform);

            //spriteBatch.Begin();
            _currentBmap.Draw( spriteBatch);
          //  spriteBatch.DrawString(_spriteFont, "Press r to reset", new Vector2(700, 1200), Color.SaddleBrown, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
          //  spriteBatch.DrawString(_spriteFont, "Use A and D to move and SPACE to Jump", _spawnPos * Vector2.One * .9f, Color.SaddleBrown, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
          //  spriteBatch.DrawString(_spriteFont, "Enter door for next level", new Vector2(1400,700), Color.SaddleBrown, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            _player.Draw(spriteBatch);

            spriteBatch.End();
            

            
            base.Draw(spriteBatch);
            
        }

        
    }
}
