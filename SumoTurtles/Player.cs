using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision.Shapes;
using System;
namespace SumoTurtles
{
    class Player
    {
        public Vector2 position { get; set; }
        public AnimatedSprite sprite { get; set; }
        public bool isActive { get; set; }
        public enum state { idle, spin, dead };
        public state currentState { get; set; }
        public Vector2 force { get; set; }
        public float torque { get; set; }
        public float restitution { get; set; }
        public float spin { get; set; }

        private World world = null;
        public Body body = null;
        public Fixture fixture = null;
        private float radius = GameConfig.Default.turtleRadius;
        private Char type;


        public Player(Vector2 position, World world, char type)
        {
            this.position = position;
            this.world = world;
            this.type = type;
            Initialize();
        }

        public void Update(GameTime gameTime)
        {
            if (isActive)
            {
                sprite.Update(gameTime);
                body.ApplyForce(force);
                body.ApplyTorque(torque);
                body.Restitution = restitution;
                position = ConvertUnits.ToDisplayUnits(body.Position);
            }
        }

        public void SetPosition(Vector2 position)
        {
            body.Position = ConvertUnits.ToSimUnits(position);
            this.position = position;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                sprite.Draw(spriteBatch, position);
            }
        }

        public void Remove()
        {
            world.RemoveBody(body);
            isActive = false;
        }

        private void Initialize()
        {
            isActive = true;
            currentState = state.idle;
            restitution = 0.0f;
            force = Vector2.Zero;
            torque = 0;
            body = BodyFactory.CreateBody(world, ConvertUnits.ToSimUnits(position));

            CircleShape circle = new CircleShape(ConvertUnits.ToSimUnits(radius), 1);
            fixture = body.CreateFixture(circle, type);
            fixture.OnCollision += turtleOnCollision;
            body.BodyType = BodyType.Dynamic;
            spin = GameConfig.Default.minTutleSpin;

            //body = BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(radius), 1, ConvertUnits.ToSimUnits(position), type);
            sprite = null;
        }

        public bool turtleOnCollision(Fixture f1, Fixture f2, Contact contact)
        {
            if (f2.UserData != null)
            {
                f1.Body.ApplyForce(-force * spin * 0.25f);
                contact.Restitution = 0.0f;
                f2.Body.ApplyForce(force * spin * 0.75f);
            }
            return true;
        }
    }
}
