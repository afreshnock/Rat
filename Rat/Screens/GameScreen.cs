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
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Rat.Screens
{
    public class GameScreen : Screen
    {
        private BasicTilemap _currentBmap;
        
        private Player _player;
        private int[] tileIndexArray;
        private SpriteFont _spriteFont;
        private Vector2 _spawnPos = new Vector2(300, 300);
        public bool NextLevel = false;
        public int PizzaCollected = 0;
        Billboard bill;
        FPSCamera camera;
        Room CurrentRoom;
        Pizza pizza = new Pizza(Vector2.Zero, 0);

        public GameScreen(Game game) : base(game)
        {

        }
        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            _player = new Player(_spawnPos, Color.White, 1);
            tileIndexArray = new int[9];
            
            base.Initialize();
        }

        public override void LoadContent(ContentManager content)
        {
            CurrentRoom = new Room(RoomManager.AllMaps.ElementAt(0));
            _currentBmap = CurrentRoom.Map.basicTilemap;
            bill = new Billboard(game);
            pizza.LoadContent(content);
            _spriteFont = content.Load<SpriteFont>("File");
            _player.LoadContent(content);
            
            
            camera = new FPSCamera(game, new Vector3(0, 0, 4));
            base.LoadContent(content);
        }


        public override void Update(GameTime gameTime)
        {
            NextLevel = false;
            if(_player.Hunger <= 0)
            {
                NextLevel = true;
                return;
            }
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

            foreach(IGameObject obj in CurrentRoom.ObjList)
            {
                if(obj is Pizza p)
                {
                    if (p.Collected == false && _player.bounds.CollidesWith(p.boundingRectangle))
                    {
                        if(p.flavor == PizzaFlavor.Stinky)
                        {
                            _player.state = RatState.Sick;
                            _player.Hunger -= 5;
                        }
                        else if(p.flavor == PizzaFlavor.Sticks)
                        {
                            if (_player.state == RatState.Sick)
                            {
                                _player.state = RatState.Normal;
                            }
                            else
                            {
                                _player.state = RatState.Golden;
                                _player.goldenTimer = 0;
                                _player.Hunger += 20;
                            }
                        }
                        else
                        {
                            _player.Hunger += 5;
                        }
                        
                        p.Collected = true;
                        PizzaCollected++;
                        break;
                    }
                }
                
            }
            foreach(Door d in CurrentRoom.Doors)
            {
                if (_player.bounds.CollidesWith(d.boundingRectangle)) 
                {
                    if(d.pos == DoorPosition.Left) _player.posistion.X = (_currentBmap.MapWidth - 1) * _currentBmap.TileWidth; 
                    else if (d.pos == DoorPosition.Right) _player.posistion.X = 32;
                    else if (d.pos == DoorPosition.Bottom) _player.posistion.Y = 32;
                    else if (d.pos == DoorPosition.Top) _player.posistion.Y = (_currentBmap.MapHeight - 1) * _currentBmap.TileHeight;
                    CurrentRoom = d.Open(CurrentRoom);
                    _currentBmap = CurrentRoom.Map.basicTilemap;
                    
                    break;
                }
                
            }


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
                
                if (tileIndexArray[i] > -1 && tileIndexArray[i] < _currentBmap.MapHeight * _currentBmap.MapWidth)
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

            float playerY = MathHelper.Clamp(_player.posistion.Y, 500, 970);
            
            float offsetX = 500 - playerX;
            float offsetY = 500- playerY;

            Matrix transform = Matrix.CreateTranslation(offsetX, offsetY, 0);
            bill.Draw(camera);

            spriteBatch.Begin(transformMatrix: transform);

            CurrentRoom.Draw(spriteBatch);
            _player.Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();
            spriteBatch.DrawString(_spriteFont, "Hunger: "+_player.Hunger.ToString() , new Vector2(50, 50), Color.SaddleBrown, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.End();

            
            base.Draw(spriteBatch);
            
        }

        
    }
}
