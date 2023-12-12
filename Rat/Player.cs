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
using Rat.Particles;

namespace Rat
{
    public class Player : IGameObject , IParticleEmitter
    {
        public Vector2 posistion;
        public Vector2 velocity = Vector2.Zero;
        public Vector2 Position { get
            {
                return posistion;
            } }
        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
        }
        private float maxVelocity = 300;
        private float velocityStep = 5;
        private Texture2D texture;
        public BoundingRectangle bounds;
        public Color color;
        private float scale;
        int frame=0;
        int frameOffset = 0;
        public BoundingRectangle feet;
        public bool jumping = false;
        private float jumptime = 0;
        public bool onGround = false;
        private float jumpTimer = 0;
        private bool canJump = false;
        public bool FallThrough = false;
        private float fallTimer = 0;
        private int _hunger = 100;
        public int Hunger
        {
            get
            {
                return _hunger;
            }
            set
            {
                if (value > 100)
                {
                    _hunger = 100;
                }
                else
                {
                    _hunger = value;
                }
            }
        }
        private float hungerTimer = 0;
        public RatState state = RatState.Normal;
        public float goldenTimer = 0;

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
            texture = content.Load<Texture2D>("ratsprites");
            
        }

        public void Update(GameTime gametime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
          
            float dt = (float)gametime.ElapsedGameTime.TotalSeconds;

            velocity.Y += 20;  // gravity
            if (velocity.Y > 600)
            {
                velocity.Y = 600;
            }
            
            switch (state)
            {
                case RatState.Normal:
                    maxVelocity = 300;
                    frameOffset = 0;
                    break;
                case RatState.Sick:
                    maxVelocity = 300;
                    frameOffset = 1;
                    break;
                case RatState.Golden:
                    maxVelocity = 600;
                    frameOffset = 2;
                    goldenTimer += (float)gametime.ElapsedGameTime.TotalSeconds;
                    if (goldenTimer > 15)
                    {
                        goldenTimer = 0;
                        state = RatState.Normal;
                    }
                    break;
            }

            hungerTimer += dt;
            if(hungerTimer >= .75f)
            {
                Hunger--;
                hungerTimer = 0;
            }

            //player input
            if (keyboardState.IsKeyDown(Keys.D)) //|| gamepad.ThumbSticks.Left.X > .2f)
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
            if (keyboardState.IsKeyDown(Keys.S))
            {
                FallThrough = true;
                fallTimer =  .2f;
            }
            if(fallTimer > 0)
            {
                fallTimer -= (float)gametime.ElapsedGameTime.TotalSeconds;

            }
            else
            {
                FallThrough = false;
            }
            if (onGround)
            {
                velocity.Y = 0;
                canJump = true;
                jumpTimer = 0f;
            }
            jumpTimer += (float)gametime.ElapsedGameTime.TotalSeconds;
            if(jumpTimer > .2f)
            {
                canJump = false;
            }
            if (keyboardState.IsKeyDown(Keys.Space) && !jumping && canJump)
            {
                jumping = true;
                jumptime = .1f;
                canJump = false;
            }
            onGround = false;
            if (jumping)
            {
                velocity.Y = -600;
                jumptime -= (float)gametime.ElapsedGameTime.TotalSeconds;
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
            feet = new BoundingRectangle(posistion.X-28, posistion.Y + 28, 56, 4);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, posistion, new Rectangle(64*(frame+frameOffset*2),0,64,64), color, 0f, Vector2.One * 32 , scale, spriteEffect, 0f);
        }
    }

    public enum RatState
    {
        Normal=0,
        Sick=1,
        Golden=2
    }
}
