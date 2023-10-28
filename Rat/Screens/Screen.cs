using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rat.Screens
{
    public class Screen : IGameObject, IGameComponent
    {

        public Screen()
        {
            _objectList = new List<IGameObject>();
        }

        public List<IGameObject> _objectList;

        public virtual void Initialize()
        {
            // todo
        }

        public virtual void LoadContent(ContentManager content)
        {
            foreach (IGameObject obj in _objectList)
            {
                obj.LoadContent(content);
            }
        }
        public virtual void Update(GameTime gameTime)
        {
            foreach (IGameObject obj in _objectList)
            {
                obj.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            foreach (IGameObject obj in _objectList)
            {
                obj.Draw(spriteBatch);
            }
            //spriteBatch.End();
        }
    }
}
