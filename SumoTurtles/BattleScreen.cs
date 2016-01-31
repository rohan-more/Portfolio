using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using System.Collections.Generic;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Common;

namespace SumoTurtles
{
    class BattleScreen : GameScreen
    {
        private Vector2 screenCenter;
        private Vector2 arenaCenter;
        private ContentManager Content;
        private float forceAmount = GameConfig.Default.minTurtleForce;
        private Sprite background = null;
        private Sprite shadow = null;
        private Player rTurtle = null;
        private Player oTurtle = null;
        private Player bTurtle = null;
        private Player pTurtle = null;
        private World world = null;
        private SoundEffect introMusic = null;
        private SoundEffectInstance introInstance = null;
        private List<AnimatedSprite> spriteList = null;
        private List<AnimatedSprite> cleanList = null;
        private Texture2D death_sprite = null;
        private Texture2D orange_coll_sprite = null;
        private Texture2D red_coll_sprite = null;
        private Texture2D blue_coll_sprite = null;
        private Texture2D purple_coll_sprite = null;
        private SpriteFont font = null;
        private Body[] obstacles = null;

        private string winner = null;
        private short noOfInActive;
        private bool gameFinished;

        private bool countDown;
        private int countFrameTime;
        private int count;
        private char wallId;

        public BattleScreen(ContentManager Content)
        {
            this.Content = Content;
        }

        public void Initialize()
        {
            winner = null;
            countDown = false;
            count = 3;
            noOfInActive = 0;
            gameFinished = false;
            wallId = 'w';
            if (world == null)
                world = new World(Vector2.Zero);
            else
                world.Clear();

            obstacles = new Body[4];
            
            obstacles[0] = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(190), ConvertUnits.ToSimUnits(60), 1, wallId);
            obstacles[0].IsStatic = true;
            obstacles[0].Restitution = 0.6f;
            obstacles[0].Friction = 0.2f;
            obstacles[0].Position = ConvertUnits.ToSimUnits(new Vector2(730, 100));
            obstacles[0].Rotation = 40f * (float)Math.PI / 180f;

            obstacles[1] = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(260), ConvertUnits.ToSimUnits(60), 1, wallId);
            obstacles[1].IsStatic = true;
            obstacles[1].Restitution = 0.6f;
            obstacles[1].Friction = 0.2f;
            obstacles[1].Position = ConvertUnits.ToSimUnits(new Vector2(680, 630));
            obstacles[1].Rotation = -60f * (float)Math.PI / 180f;

            obstacles[2] = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(260), ConvertUnits.ToSimUnits(60), 1, wallId);
            obstacles[2].IsStatic = true;
            obstacles[2].Restitution = 0.6f;
            obstacles[2].Friction = 0.2f;
            obstacles[2].Position = ConvertUnits.ToSimUnits(new Vector2(320, 620));
            obstacles[2].Rotation = 60f * (float)Math.PI / 180f;

            obstacles[3] = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(190), ConvertUnits.ToSimUnits(60), 1, wallId);
            obstacles[3].IsStatic = true;
            obstacles[3].Restitution = 0.6f;
            obstacles[3].Friction = 0.2f;
            obstacles[3].Position = ConvertUnits.ToSimUnits(new Vector2(280, 100));
            obstacles[3].Rotation = -40f * (float)Math.PI / 180f;

            spriteList = new List<AnimatedSprite>();
            cleanList = new List<AnimatedSprite>();
            screenCenter = new Vector2(MainGame.bufferWidth / 2, MainGame.bufferHeight / 2);
            arenaCenter = new Vector2(500, 360);
            rTurtle = new Player(new Vector2(500, 50), world, 'r');
            rTurtle.fixture.OnCollision += turtleSpinCollision;
            oTurtle = new Player(new Vector2(780, 330), world, 'o');
            oTurtle.fixture.OnCollision += turtleSpinCollision;
            bTurtle = new Player(new Vector2(250, 350), world, 'b');
            bTurtle.fixture.OnCollision += turtleSpinCollision;
            pTurtle = new Player(new Vector2(500, 600), world, 'p');
            pTurtle.fixture.OnCollision += turtleSpinCollision;

        }

        public void LoadContent()
        {
            introMusic = Content.Load<SoundEffect>("gong_short");
            introInstance = introMusic.CreateInstance();
            font = Content.Load<SpriteFont>("Zenzai");
            background = new Sprite(Content.Load<Texture2D>("arena_1"));
            death_sprite = Content.Load<Texture2D>("death_clouds");
            orange_coll_sprite = Content.Load<Texture2D>("orange_col");
            red_coll_sprite = Content.Load<Texture2D>("red_col");
            blue_coll_sprite = Content.Load<Texture2D>("blue_col");
            purple_coll_sprite = Content.Load<Texture2D>("purple_col");
            shadow = new Sprite(Content.Load<Texture2D>("shadow"));
            
            Texture2D rTurtleTexture = Content.Load<Texture2D>("red_turtle_sprite_sheet_new");
            rTurtle.sprite = new AnimatedSprite(rTurtleTexture, 2, 5, 8);

            Texture2D oTurtleTexture = Content.Load<Texture2D>("orange_turtle_sprite_sheet_new");
            oTurtle.sprite = new AnimatedSprite(oTurtleTexture, 2, 5, 8);

            Texture2D bTurtleTexture = Content.Load<Texture2D>("blue_turtle_sprite_sheet_new");
            bTurtle.sprite = new AnimatedSprite(bTurtleTexture, 2, 5, 8);

            Texture2D pTurtleTexture = Content.Load<Texture2D>("purple_turtle_sprite_sheet_new");
            pTurtle.sprite = new AnimatedSprite(pTurtleTexture, 2, 5, 8);


            
        }

        public void Update(GameTime gameTime)
        {
            if (!countDown)
            {
                countFrameTime += gameTime.ElapsedGameTime.Milliseconds;
                if (countFrameTime > 1000)
                {
                    count--;
                    countFrameTime = 0;
                    if (count == 0) introInstance.Play();
                    if (count == -1) countDown = true;
                }
            }

            if (!gameFinished)
            {
                GameLogic(gameTime);
                rTurtle.Update(gameTime);
                oTurtle.Update(gameTime);
                bTurtle.Update(gameTime);
                pTurtle.Update(gameTime);
                world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            }
            if (countDown)
            {
                HandleInput(gameTime);
                foreach (AnimatedSprite sprite in spriteList)
                {
                    sprite.Update(gameTime);
                    if (sprite.doneAnimation)
                    {
                        cleanList.Add(sprite);
                    }
                }

                foreach (AnimatedSprite sprite in cleanList)
                {
                    spriteList.Remove(sprite);
                }

                cleanList.Clear();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 shadowOffset = new Vector2(2,2);
            background.Draw(spriteBatch, screenCenter);
            if (rTurtle.isActive)
                shadow.Draw(spriteBatch, rTurtle.position + shadowOffset);
            
            rTurtle.Draw(spriteBatch);

            if (oTurtle.isActive)
                shadow.Draw(spriteBatch, oTurtle.position + shadowOffset);
            
            oTurtle.Draw(spriteBatch);

            if (bTurtle.isActive)
                shadow.Draw(spriteBatch, bTurtle.position + shadowOffset);

            bTurtle.Draw(spriteBatch);

            if (pTurtle.isActive)
                shadow.Draw(spriteBatch, pTurtle.position + shadowOffset);

            pTurtle.Draw(spriteBatch);

            foreach (Sprite sprite in spriteList)
            {
                sprite.Draw(spriteBatch, sprite.position);
            }

            if (!countDown)
            {
                string countString;
                if (count > 0)
                {
                    countString = count.ToString();
                    
                }
                else
                {
                    countString = "Fight";
                }
                Vector2 countOrigin = font.MeasureString(countString) / 2;
                spriteBatch.DrawString(font, countString, arenaCenter, Color.OrangeRed, 0, countOrigin, 1.0f, SpriteEffects.None, 0.5f);

            }

            if (gameFinished)
            {
                Vector2 fontOrigin = font.MeasureString(winner) / 2;
                spriteBatch.DrawString(font, winner, new Vector2(540, 200), Color.Red, 0, fontOrigin, 1.0f, SpriteEffects.None, 0.5f);

                string restartText = "Press    R   to restart";
                Vector2 restartOrigin = font.MeasureString(restartText) / 2;
                spriteBatch.DrawString(font, restartText, new Vector2(540, 300), Color.Red, 0, restartOrigin, 1.0f, SpriteEffects.None, 0.5f);

            }

        }       

        public void UnloadContent()
        {
            introInstance.Stop();
            introMusic.Dispose();
            Content.Unload();
        }
        
        private void HandleInput(GameTime gameTime)
        {
            GamePadCapabilities capabilities_1 = GamePad.GetCapabilities(PlayerIndex.One);

            if (capabilities_1.IsConnected)
            {
                
                rTurtle.force = Vector2.Zero;
                GamePadState state_1 = GamePad.GetState(PlayerIndex.One);

                if (capabilities_1.HasLeftXThumbStick)
                {
                    if (state_1.ThumbSticks.Left.X > 0.5f)
                        rTurtle.force += new Vector2(forceAmount, 0);
                    if (state_1.ThumbSticks.Left.X < -0.5f)
                        rTurtle.force += new Vector2(-forceAmount, 0);
                    if (state_1.ThumbSticks.Left.Y > 0.5f)
                        rTurtle.force += new Vector2(0, -forceAmount);
                    if (state_1.ThumbSticks.Left.Y < -0.5f)
                        rTurtle.force += new Vector2(0, forceAmount);
                }
            }

           GamePadCapabilities capabilities_2 = GamePad.GetCapabilities(PlayerIndex.Two);

            if (capabilities_2.IsConnected)
            {
                oTurtle.force = Vector2.Zero;
                GamePadState state_2 = GamePad.GetState(PlayerIndex.Two);

                if (capabilities_2.HasLeftXThumbStick)
                {
                    if (state_2.ThumbSticks.Left.X > 0.5f)
                        oTurtle.force += new Vector2(forceAmount, 0);
                    if (state_2.ThumbSticks.Left.X < -0.5f)
                        oTurtle.force += new Vector2(-forceAmount, 0);
                    if (state_2.ThumbSticks.Left.Y > 0.5f)
                        oTurtle.force += new Vector2(0, -forceAmount);
                    if (state_2.ThumbSticks.Left.Y < -0.5f)
                        oTurtle.force += new Vector2(0, forceAmount);
                }
            }

            GamePadCapabilities capabilities_3 = GamePad.GetCapabilities(PlayerIndex.Three);

            if (capabilities_3.IsConnected)
            {
                pTurtle.force = Vector2.Zero;
                GamePadState state_3 = GamePad.GetState(PlayerIndex.Three);

                if (capabilities_3.HasLeftXThumbStick)
                {
                    if (state_3.ThumbSticks.Left.X > 0.5f)
                        pTurtle.force += new Vector2(forceAmount, 0);
                    if (state_3.ThumbSticks.Left.X < -0.5f)
                        pTurtle.force += new Vector2(-forceAmount, 0);
                    if (state_3.ThumbSticks.Left.Y > 0.5f)
                        pTurtle.force += new Vector2(0, -forceAmount);
                    if (state_3.ThumbSticks.Left.Y < -0.5f)
                        pTurtle.force += new Vector2(0, forceAmount);
                }
            }

            GamePadCapabilities capabilities_4 = GamePad.GetCapabilities(PlayerIndex.Four);

            if (capabilities_4.IsConnected)
            {

                bTurtle.force = Vector2.Zero;
                GamePadState state_4 = GamePad.GetState(PlayerIndex.Four);

                if (capabilities_4.HasLeftXThumbStick)
                {
                    if (state_4.ThumbSticks.Left.X > 0.5f)
                        bTurtle.force += new Vector2(forceAmount, 0);
                    if (state_4.ThumbSticks.Left.X < -0.5f)
                        bTurtle.force += new Vector2(-forceAmount, 0);
                    if (state_4.ThumbSticks.Left.Y > 0.5f)
                        bTurtle.force += new Vector2(0, -forceAmount);
                    if (state_4.ThumbSticks.Left.Y < -0.5f)
                        bTurtle.force += new Vector2(0, forceAmount);
                }
            }


            KeyboardState state = Keyboard.GetState();
            /*rTurtle.force = Vector2.Zero;
            rTurtle.torque = 0f;

            if (state.IsKeyDown(Keys.A))
                rTurtle.force += new Vector2(-forceAmount, 0);
            if (state.IsKeyDown(Keys.S))
                rTurtle.force += new Vector2(0, forceAmount);
            if (state.IsKeyDown(Keys.D))
                rTurtle.force += new Vector2(forceAmount, 0);
            if (state.IsKeyDown(Keys.W))
                rTurtle.force += new Vector2(0, -forceAmount);

            oTurtle.force = Vector2.Zero;
            oTurtle.torque = 0f;

            if (state.IsKeyDown(Keys.Left))
                oTurtle.force += new Vector2(-forceAmount, 0);
            if (state.IsKeyDown(Keys.Down))
                oTurtle.force += new Vector2(0, forceAmount);
            if (state.IsKeyDown(Keys.Right))
                oTurtle.force += new Vector2(forceAmount, 0);
            if (state.IsKeyDown(Keys.Up))
                oTurtle.force += new Vector2(0, -forceAmount);

            bTurtle.force = Vector2.Zero;
            bTurtle.torque = 0f;

            if (state.IsKeyDown(Keys.J))
                bTurtle.force += new Vector2(-forceAmount, 0);
            if (state.IsKeyDown(Keys.K))
                bTurtle.force += new Vector2(0, forceAmount);
            if (state.IsKeyDown(Keys.L))
                bTurtle.force += new Vector2(forceAmount, 0);
            if (state.IsKeyDown(Keys.I))
                bTurtle.force += new Vector2(0, -forceAmount);*/


            if (state.IsKeyDown(Keys.R))
            {
                restart();
            }
        }

        private void restart()
        { 
            UnloadContent();
            Initialize();
            LoadContent();
        }

        private void GameLogic(GameTime gameTime)
        {
            
            if (oTurtle.isActive && checkKnockOut(oTurtle.position))
            {
                oTurtle.Remove();
                AnimatedSprite oDeath = new AnimatedSprite(death_sprite, 3, 3, 9);
                oDeath.playOnce = true;
                oDeath.position = oTurtle.position;
                spriteList.Add(oDeath);
                noOfInActive++;
            }

            if (rTurtle.isActive && checkKnockOut(rTurtle.position))
            {
                rTurtle.Remove();
                AnimatedSprite rDeath = new AnimatedSprite(death_sprite, 3, 3, 9);
                rDeath.playOnce = true;
                rDeath.position = rTurtle.position;
                spriteList.Add(rDeath);
                noOfInActive++;
            }

            if (bTurtle.isActive && checkKnockOut(bTurtle.position))
            {
                bTurtle.Remove();
                AnimatedSprite bDeath = new AnimatedSprite(death_sprite, 3, 3, 9);
                bDeath.playOnce = true;
                bDeath.position = bTurtle.position;
                spriteList.Add(bDeath);
                noOfInActive++;
            }

            if (pTurtle.isActive && checkKnockOut(pTurtle.position))
            {
                pTurtle.Remove();
                AnimatedSprite pDeath = new AnimatedSprite(death_sprite, 3, 3, 9);
                pDeath.playOnce = true;
                pDeath.position = pTurtle.position;
                spriteList.Add(pDeath);
                noOfInActive++;
            }
            
            if (noOfInActive == 3)
            {
                if (rTurtle.isActive)
                {
                    winner = "Red Wins";
                }
                else if (oTurtle.isActive) {
                    winner = "Orange Wins";
                }
                else if (bTurtle.isActive)
                {
                    winner = "Blue Wins";
                }
                else if (pTurtle.isActive)
                {
                    winner = "Purple Wins";
                }
                gameFinished = true;
            }

        }

        public bool turtleSpinCollision(Fixture f1, Fixture f2, Contact contact)
        {
            Console.WriteLine(f2.Body.UserData);
            if (f1.UserData != null && f2.UserData != null)
            {
                Vector2 normal = Vector2.Zero;
                FixedArray2<Vector2> points;
                contact.GetWorldManifold(out normal, out points);

                char id = (char)f1.UserData;
                
                AnimatedSprite Coll;
                if (id == 'o')
                    Coll = new AnimatedSprite(orange_coll_sprite, 2, 3, 4);
                else if (id == 'r')
                    Coll = new AnimatedSprite(red_coll_sprite, 2, 3, 4);
                else if (id == 'b')
                    Coll = new AnimatedSprite(blue_coll_sprite, 2, 3, 4);
                else if (id == 'p')
                    Coll = new AnimatedSprite(purple_coll_sprite, 2, 3, 4);
                else
                    Coll = new AnimatedSprite(orange_coll_sprite, 2, 3, 4);
                Coll.playOnce = true;
                Coll.position = ConvertUnits.ToDisplayUnits(points[0]);
                Coll.normal = ConvertUnits.ToDisplayUnits(f1.Body.Position) - Coll.position;
                spriteList.Add(Coll);
            }
            return true;
        }

        private Boolean checkKnockOut(Vector2 playerPosition)
        {
            //XXX: To be generalized
            return Vector2.Distance(playerPosition, screenCenter) > 390;            
        }

    }
}
