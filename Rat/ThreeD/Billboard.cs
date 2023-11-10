using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rat.ThreeD
{
    public class Billboard
    {
        Game game;

        Texture2D texture;

        // The VertexBuffer of crate vertices
        VertexBuffer vertexBuffer;
        // The IndexBuffer defining the Crate's triangles
        IndexBuffer indexBuffer;

        // The effect to render the crate with
        BasicEffect effect;


        public Billboard(Game game)
        {
            texture = game.Content.Load<Texture2D>("ratback");
            this.game = game;
            InitializeVertices();
            InitializeIndices();
            InitializeEffect();
        }

        public void InitializeVertices()
        {
            var vertexData = new VertexPositionNormalTexture[] { 
            // Front Face
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3(-1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f,  1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, -1.0f, -1.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Forward },
            };
            vertexBuffer = new VertexBuffer(game.GraphicsDevice, typeof(VertexPositionNormalTexture), vertexData.Length, BufferUsage.None);
            vertexBuffer.SetData<VertexPositionNormalTexture>(vertexData);
        }

        public void InitializeIndices()
        {
            var indexData = new short[]
            {
                // Front face
                0, 2, 1,
                0, 3, 2,
            };

            indexBuffer = new IndexBuffer(game.GraphicsDevice, IndexElementSize.SixteenBits, indexData.Length, BufferUsage.None);
            indexBuffer.SetData<short>(indexData);
        }

        /// <summary>
        /// Initializes the BasicEffect to render our crate
        /// </summary>
        void InitializeEffect()
        {
            effect = new BasicEffect(game.GraphicsDevice);
            effect.TextureEnabled = true;
            effect.Texture = texture;
        }

        public void Draw(FPSCamera camera)
        {

            effect.World = Matrix.CreateScale(2.0f);
            effect.World *= Matrix.CreateBillboard(Vector3.One, camera.position, Vector3.Up, camera.View.Forward);
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            effect.CurrentTechnique.Passes[0].Apply();
            game.GraphicsDevice.SetVertexBuffer(vertexBuffer);
            // set the index buffer
            game.GraphicsDevice.Indices = indexBuffer;
            // Draw the triangles
            game.GraphicsDevice.DrawIndexedPrimitives(
                PrimitiveType.TriangleList, // Tye type to draw
                0,                          // The first vertex to use
                0,                          // The first index to use
                2                          // the number of triangles to draw
            );

        }

    }
}
