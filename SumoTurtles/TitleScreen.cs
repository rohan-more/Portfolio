using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace SumoTurtles
{
    class TitleScreen : GameScreen
    {
        private ContentManager Content;
        private Vector2 screenCenter;
        private Sprite background = null;
        private Sprite title = null;
        private Sprite playMenu = null;
        private Sprite exitMenu = null;
        private Sprite turtleSym = null;
        private Vector2 symPlayPos;
        private Vector2 symExitPos;
        private Vector2 titlePosition;
        private Vector2 playPosition;
        private Vector2 exitPosition;
        private float titlePOffset;
        private float titleNOffset;
        private float titleVelocity;
        private enum menu { play, exit};
        private menu menuState;

        private int inputFrameMilliSeconds;
        private int timeSinceLastInputFrame;

        private SoundEffect bgMusic;
        private SoundEffectInstance bgMusicInstance;        

        public TitleScreen(ContentManager Content)
        {
            this.Content = Content;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch, screenCenter);
            title.Draw(spriteBatch, titlePosition);
            if (menuState == menu.play)
            {
                turtleSym.Draw(spriteBatch, symPlayPos);
            }
            else
            {
                turtleSym.Draw(spriteBatch, symExitPos);
            }

            playMenu.Draw(spriteBatch, playPosition);
            exitMenu.Draw(spriteBatch, exitPosition);
        }

        public void Initialize()
        {
            inputFrameMilliSeconds = 100;
            timeSinceLastInputFrame = 0;
            screenCenter = new Vector2(MainGame.bufferWidth / 2, MainGame.bufferHeight / 2);
            titlePosition = new Vector2(MainGame.bufferWidth / 2, MainGame.bufferHeight / 3);
            titleVelocity = 20;
            titlePOffset = MainGame.bufferHeight / 3 + 5;
            titleNOffset = MainGame.bufferHeight / 3 - 5;

            menuState = menu.play;
            symPlayPos = new Vector2(MainGame.bufferWidth / 2 - 80, MainGame.bufferHeight / 2);
            symExitPos = new Vector2(MainGame.bufferWidth / 2 - 80, MainGame.bufferHeight / 2 + 75);
            playPosition = screenCenter;
            exitPosition = new Vector2(MainGame.bufferWidth/2 + 10, MainGame.bufferHeight/2 + 75);
        }

        public void LoadContent()
        {
            bgMusic = Content.Load<SoundEffect>("bg_title_screen");
            bgMusicInstance = bgMusic.CreateInstance();
            bgMusicInstance.IsLooped = true;
            bgMusicInstance.Play();
            background = new Sprite(Content.Load<Texture2D>("title_background"));
            title = new Sprite(Content.Load<Texture2D>("title"));
            playMenu = new Sprite(Content.Load<Texture2D>("play_menu"));
            exitMenu = new Sprite(Content.Load<Texture2D>("exit_menu"));
            turtleSym = new Sprite(Content.Load<Texture2D>("turtle_sym"));
        }

        public void UnloadContent()
        {
            bgMusicInstance.Stop();
            Content.Unload();
        }

        public void Update(GameTime gameTime)
        {
            
            timeSinceLastInputFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastInputFrame > inputFrameMilliSeconds)
            {
                HandleInput(gameTime);
                timeSinceLastInputFrame -= inputFrameMilliSeconds;
            }
            
            if (titlePosition.Y > titlePOffset || titlePosition.Y < titleNOffset)
            {
                titleVelocity *= -1;
            }

            titlePosition.Y += (float)gameTime.ElapsedGameTime.Milliseconds / 1000f * titleVelocity;
        }

        private void HandleInput(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.Up))
            {
                timeSinceLastInputFrame = 0;
                if (menuState == menu.play)
                {
                    menuState = menu.exit;
                }
                else
                {
                    menuState = menu.play;
                }
            }

            if (state.IsKeyDown(Keys.Enter))
            {
                if (menuState == menu.exit) MainGame.endGame = true;
                if (menuState == menu.play)
                {
                    ScreenManager screenManager = ScreenManager.Instance;
                    screenManager.SetScreen(new BattleScreen(Content));
                    screenManager.Initialize();
                    screenManager.LoadContent();
                }
            }
        }
    }
}
