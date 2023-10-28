using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rat.Collison;


namespace Rat
{
    public class Player : IGameObject
    {
        public Vector2 posistion;
        public Vector2 velocity;
        private float maxVelocity = 300;
        private float velocityStep = 5;
        private Texture2D texture;
        public BoundingRectangle bounds;
        public Color color;
        private float scale;
        int frame=0;
        public BoundingRectangle feet;
        private bool jumping = false;
        private int jumptime;
        public bool onGround = false;

        SpriteEffects spriteEffect = SpriteEffects.None;
        public Player(Vector2 position, Color color, float scale)
        {
            posistion = position;
            this.color = color;
            this.scale = scale;
            bounds = new BoundingRectangle(posistion.X + (30 - 64) * scale, posistion.Y + (40 - 64) * scale, 70 * scale, 40 * scale);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("ratsprite");
            //debug = content.Load<Texture2D>("sea");
        }

        public void Update(GameTime gametime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float dt = (float)gametime.ElapsedGameTime.TotalSeconds;

            velocity.Y = 100;
            //player input
            if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity.X = maxVelocity;
                frame = 1;
                spriteEffect = SpriteEffects.None;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity.X = -maxVelocity;
                frame = 1;
                spriteEffect = SpriteEffects.FlipHorizontally;
            }
            if (keyboardState.IsKeyDown(Keys.Space) && !jumping && onGround)
            {
                jumping = true;
                jumptime = 30;
                onGround = false;
            }
            onGround = false;
            if (jumping)
            {
                velocity.Y = -200;
                jumptime--;
                if(jumptime <= 0) jumping = false;
            }
       

            if (velocity.X != 0 && keyboardState.IsKeyUp(Keys.D) && keyboardState.IsKeyUp(Keys.A))
            {
                velocity.X = 0;
                
                frame = 0;
            }

            if (velocity.Y > -velocityStep && velocity.Y < velocityStep)
            {
                velocity.Y = 0;
            }
            if (velocity.X > -velocityStep && velocity.X < velocityStep)
            {
                velocity.X = 0;
            }
            
            posistion += dt * velocity;



            UpdateBounds();
            

        }
        public void UpdateBounds()
        {
            bounds = new BoundingRectangle(posistion.X - 32, posistion.Y - 32, 64, 64);
            feet = new BoundingRectangle(posistion.X, posistion.Y + 28, 1, 4);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, posistion, new Rectangle(64*frame,0,64,64), color, 0f, Vector2.One * 32 , scale, spriteEffect, 0f);

        }
    }
}
