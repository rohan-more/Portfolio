using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SumoTurtles
{
    public class Sprite
    {
        public Vector2 origin { get; set; }
        public virtual Texture2D texture { get; set; }
        public Vector2 position { get; set; }

        public Vector2 normal { get; set; } = Vector2.Zero;

        public Sprite(Texture2D texture, Vector2 origin)
        {
            this.texture = texture;
            this.origin = origin;
        }

        public Sprite(Texture2D texture)
        {
            this.texture = texture;
            this.origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        /*public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(texture, position - origin, Color.White);
        }*/

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            float angle = (float)System.Math.Atan2(normal.Y, normal.X);
            spriteBatch.Draw(texture,position, origin: origin, rotation: angle);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position - origin);
        }


    }
}
