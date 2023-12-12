using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Rat.Particles
{
    public class PizzaParticleSystem : ParticleSystem
    {

        public PizzaParticleSystem(int maxPizza) : base(maxPizza)
        {

        }

        protected override void InitializeConstants()
        {
            textureFilename = "pizzaparticle";
            minNumParticles = 10;
            maxNumParticles = 25;

            blendState = BlendState.Additive;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = new Vector2( RandomHelper.NextFloat(-50, 50) , RandomHelper.NextFloat(-50, 50));

            var lifetime = RandomHelper.NextFloat(.3f,.7f);

            var scale = RandomHelper.NextFloat(.25f, .75f);

            var acceleration = 300 * Vector2.UnitY;

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

            var alpha = Color.White * RandomHelper.NextFloat(.75f, 1f);

            p.Initialize(where, velocity, acceleration, alpha, lifetime : lifetime, rotation : rotation, angularVelocity : angularVelocity, scale : scale);
        }




        public void PlacePizzaParticle(Vector2 where) => AddParticles(where);

        public void PlacePizzaParticle(Rectangle where) => AddParticles(where);




    }
}
