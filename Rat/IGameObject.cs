using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Rat
{
    public interface IGameObject
    {
        public void LoadContent(ContentManager contentmanager);

        public void Update(GameTime gameTime);

        public void Draw(SpriteBatch spritebatch);        
    }
}
