using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Rat.Particles
{
    public class RainParticleSystem : ParticleSystem
    {
        //private Rectangle _source;

        public bool IsRaining { get; set; } = true;

        public RainParticleSystem( int drips) : base( drips)
        {
            

        }

        protected override void InitializeConstants()
        {
            textureFilename = "drop";
            minNumParticles = 1;
            maxNumParticles = 3;
        }


        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            p.Initialize(where,Vector2.UnitY * 260, Vector2.Zero,Color.DarkGreen,scale: RandomHelper.NextFloat(.1f,.4f), lifetime: 10);
        }

        public void PlaceDrips(Vector2 where) => AddParticles(where);

        public void PlaceDrips(Rectangle where) => AddParticles(where);

        //public override void Update(GameTime gameTime)
        //{
        //    base.Update(gameTime);
        //    if (IsRaining) AddParticles(_source);
        //}
    }
}
