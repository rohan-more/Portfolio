using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SumoTurtles
{
    class AnimatedSprite: Sprite
    {
        public int totalFrames { get; set; }
        public int milliSecondsPerFrame { get; set; } = GameConfig.Default.animationMilliSecondsPerFrame;
        public int rows { get; set; }
        public int columns { get; set; }

        public bool playOnce { get; set; }
        public bool doneAnimation { get; set; }
        public override Texture2D texture
        {
            get
            {
                return base.texture;
            }

            set
            {
                base.texture = value;
                currentFrame = 0;
                timeSinceLastFrame = 0;
                milliSecondsPerFrame = GameConfig.Default.animationMilliSecondsPerFrame;
                playOnce = false;
                doneAnimation = false;
            }
        }

        private int currentFrame;
        private int timeSinceLastFrame;

        public AnimatedSprite(Texture2D texture, int rows, int columns, int totalFrames) : base(texture)
        {
            
            SetSpriteSheet(texture, rows, columns, totalFrames);
        }

        public void SetSpriteSheet(Texture2D texture, int rows, int columns, int totalFrames)
        {
            this.texture = texture;
            this.rows = rows;
            this.columns = columns;
            this.totalFrames = totalFrames;
        }
        public override void Update(GameTime gameTime)
        {
            if (!doneAnimation)
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > milliSecondsPerFrame)
                {
                    timeSinceLastFrame -= milliSecondsPerFrame;
                    currentFrame++;

                    if (currentFrame == totalFrames)
                    {
                        currentFrame = 0;
                        if (playOnce)
                        {
                            doneAnimation = true;
                            timeSinceLastFrame = 0;
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if (!doneAnimation)
            {
                int width = texture.Width / this.columns;
                int height = texture.Height / this.rows;
                int row = (int)((float)currentFrame / this.columns);
                int column = currentFrame % this.columns;
                
                Rectangle sourceRectange = new Rectangle(width * column, height * row, width, height);
                //use origin ? or draw accordingly inside the rectangle ?
                Rectangle destinationRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
                //Rectangle destinationRectangle = new Rectangle((int)position.X - width / 2, (int)position.Y - height / 2, width, height);
                float angle = (float)System.Math.Atan2(normal.Y, normal.X);
                //spriteBatch.Draw(texture,destinationRectangle, sourceRectange, Color.White);
                Vector2 spriteOrigin = new Vector2(width / 2, height / 2);
                spriteBatch.Draw(texture, destinationRectangle: destinationRectangle, sourceRectangle: sourceRectange, origin: spriteOrigin, rotation: angle, color: Color.White);
            }
        }
        
        public void PlayAgain()
        {
            doneAnimation = true;
        }
    }
}
