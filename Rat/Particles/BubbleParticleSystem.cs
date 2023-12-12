
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rat.Particles
{
    public class BubbleParticleSystem : ParticleSystem
    {
        public BubbleParticleSystem(int maxBubbles) : base(maxBubbles)
        {

        }

        protected override void InitializeConstants()
        {
            textureFilename = "bubble2";
            minNumParticles = 1;
            maxNumParticles = 3;

            blendState = BlendState.Additive;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = -Vector2.UnitY * RandomHelper.NextFloat(75, 100);

            var lifetime = RandomHelper.NextFloat(8f, 16f);

            var scale = RandomHelper.NextFloat(.25f, 1f);
            //var acceleration = -velocity / lifetime;

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

            var alpha = Color.White * RandomHelper.NextFloat(.5f, .8f);

            p.Initialize(where, velocity, Vector2.Zero,alpha, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale: scale );
        }

    


        public void PlaceBubble(Vector2 where) => AddParticles(where);

        public void PlaceBubble(Rectangle where) => AddParticles(where);
    }
}
