using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using META.Engine;
using META.Engine.InterfaceSystem;
using META.Engine.GameObjects;
using META.Engine.Sprites;

namespace META.GameWorld.Objects.Characters
{
    public class Crab : Character
    {
        public float? lastTurnAroundTime;
        public float turnAroundPeriod;
        public int direction;

        public Crab(Vector2 _position, float _turnAroundPeriod)
            : base(new Rectangle((int)_position.X, (int)_position.Y, 100, 100), SpriteID.Crab, 350, DEFAULT_GRAVITY, null)
        {
            turnAroundPeriod = _turnAroundPeriod;
            lastTurnAroundTime = null;
            direction = -1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (StateMachineManager.CurrentState == State.Playing &&
                lastTurnAroundTime == null)
                lastTurnAroundTime = GameStats.TotalGameTime;

            while (GameStats.TotalGameTime >= lastTurnAroundTime + turnAroundPeriod &&
                turnAroundPeriod > 0)
            {//Have Character Turn around, use while loop as a lag resistant IF statement
                direction *= -1;
                lastTurnAroundTime += turnAroundPeriod;
            }
        }

        public override int GetDirection()
        {
            return direction;
        }
    }
}
