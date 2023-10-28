﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rat.Screens;
namespace Rat
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BasicTilemap _BMap;
        private Screen[] _screenArray;
        private int _screenSelect = 0;
        private KeyboardState _current;
        private KeyboardState _prev;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            IsMouseVisible = true;
            _screenArray = new Screen[]{ new MainMenuScreen(), new GameScreen(), new WinScreen() };
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _screenArray[1].Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            
            _BMap = Content.Load<BasicTilemap>("ratlevel1");
            _screenArray[0].LoadContent(Content);
            _screenArray[1].LoadContent(Content);
            _screenArray[2].LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //_BMap.Update(gameTime);
            _prev = _current;
            _current = Keyboard.GetState();

            if (_current.IsKeyDown(Keys.Escape) && _prev.IsKeyUp(Keys.Escape))
            {
                if (_screenSelect == 0)
                {
                    Exit();
                }
                else
                {
                    _screenSelect = 0;
                }
            }
            if (_current.IsKeyDown(Keys.Enter) && _prev.IsKeyUp(Keys.Enter) && _screenArray[_screenSelect] is MainMenuScreen s)
            {
                switch (s.menuCounter)
                {
                    case 0: _screenSelect = 1; break;
                    case 1: _screenSelect = 0; break; // TODO 
                    case 2: _screenSelect = 0; break; // TODO
                    case 3: Exit(); break;
                    default: throw new System.Exception();
                }


            }
            _screenArray[_screenSelect].Update(gameTime);
            if(_screenArray[_screenSelect] is GameScreen level && level.NextLevel == true)
            {
                _screenSelect++;
            }
            if (_screenArray[_screenSelect] is WinScreen win && win.saved == false)
            {
                
                win.yourScore = (float)gameTime.TotalGameTime.TotalSeconds;
                if (win.highScore > win.yourScore)
                {
                    win.highScore = win.yourScore;
                    win.SaveScore();
                }
                else
                {
                    win.saved = true;
                }
                
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);

            // TODO: Add your drawing code here
            
            //_BMap.Draw(gameTime, _spriteBatch);
            _screenArray[_screenSelect].Draw(_spriteBatch);
            
            base.Draw(gameTime);
        }
    }
}