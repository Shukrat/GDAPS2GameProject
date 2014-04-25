#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace _4D13TowerDefenseGame
{
    // Adam K. Dean; class for drawing circles in XNA/Monogame
    // http://www.adamkdean.co.uk/blog/read/54/circle-primitive-class-for-xna-monogame
    class Circle
    {
        public Circle(float x, float y, int radius,
        GraphicsDeviceManager graphics)
            : this(x, y, radius, Color.White, graphics) { }

        public Circle(float x, float y, int radius,
            Color color, GraphicsDeviceManager graphics)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.color = color;
            this.graphics = graphics;

            Initialize();
        }

        public void Draw()
        {
            effect.CurrentTechnique.Passes[0].Apply();
            graphics.GraphicsDevice.DrawUserPrimitives
                (PrimitiveType.LineStrip, vertices, 0, vertices.Length - 1);
        }

        private void Initialize()
        {
            InitializeBasicEffect();
            InitializeVertices();
        }

        private void InitializeBasicEffect()
        {
            effect = new BasicEffect(graphics.GraphicsDevice);
            effect.VertexColorEnabled = true;
            effect.Projection = Matrix.CreateOrthographicOffCenter
                (0, graphics.GraphicsDevice.Viewport.Width,
                 graphics.GraphicsDevice.Viewport.Height, 0,
                 0, 1);
        }

        private void InitializeVertices()
        {
            vertices = new VertexPositionColor[CalculatePointCount()];
            var pointTheta = ((float)Math.PI * 2) / (vertices.Length - 1);
            for (int i = 0; i < vertices.Length; i++)
            {
                var theta = pointTheta * i;
                var x = X + ((float)Math.Sin(theta) * Radius);
                var y = Y + ((float)Math.Cos(theta) * Radius);
                vertices[i].Position = new Vector3(x, y, 0);
                vertices[i].Color = Color;
            }
            vertices[vertices.Length - 1] = vertices[0];
        }

        private int CalculatePointCount()
        {
            return (int)Math.Ceiling(Radius * Math.PI);
        }

        private GraphicsDeviceManager graphics;
        private VertexPositionColor[] vertices;
        private BasicEffect effect;

        private float x;
        public float X
        {
            get { return x; }
            set { x = value; InitializeVertices(); }
        }
        private float y;
        public float Y
        {
            get { return y; }
            set { y = value; InitializeVertices(); }
        }
        private float radius;
        public float Radius
        {
            get { return radius; }
            set { radius = (value < 1) ? 1 : value; InitializeVertices(); }
        }
        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; InitializeVertices(); }
        }
        public int Points
        {
            get { return CalculatePointCount(); }
        }
    }
}