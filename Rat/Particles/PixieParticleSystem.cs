using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rat.Particles
{
    public class PixieParticleSystem : ParticleSystem
    {

        IParticleEmitter emitter;


        public PixieParticleSystem(IParticleEmitter emitter) : base(100)
        {
            this.emitter = emitter;
        }

        protected override void InitializeConstants()
        {
            textureFilename = "circle";

            minNumParticles = 1;

            maxNumParticles = 2;

            blendState = BlendState.Additive;

           // DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            //var Velocity = emitter.Velocity;

            var Acceleration = Vector2.UnitY * 400;

            var scale = RandomHelper.NextFloat(.1f, .5f);

            var lifetime = RandomHelper.NextFloat(.1f, 1f);



            p.Initialize(where,Vector2.Zero,Acceleration,Color.Goldenrod,scale:scale,lifetime:lifetime);
        }


        public void PlacePixie() => AddParticles(emitter.Position);

    }
}
