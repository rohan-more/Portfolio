using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SumoTurtles
{
    class ScreenManager
    {
        private static ScreenManager instance = null;
        private GameScreen gameScreen;

        private ScreenManager() { }

        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScreenManager();
                }
                return instance;
            }
        }

        public void SetScreen(GameScreen gameScreen)
        {
            if (this.gameScreen != null)
            {
                this.gameScreen.UnloadContent();
            }
            this.gameScreen = gameScreen;
        }

        public void Update(GameTime gameTime)
        {
            if (gameScreen != null)
                gameScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (gameScreen != null)
                gameScreen.Draw(spriteBatch);
        }

        public void LoadContent()
        {
            gameScreen.LoadContent();
        }

        public void UnloadContent()
        {
            if (gameScreen != null)
            {
                gameScreen.UnloadContent();
                gameScreen = null;
            }
        }

        public void Initialize()
        {
            if (gameScreen != null)
            {
                gameScreen.Initialize();
            }
        }
    }
}
