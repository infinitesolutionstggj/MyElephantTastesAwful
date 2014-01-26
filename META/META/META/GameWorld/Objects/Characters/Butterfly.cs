using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using META.Engine.Sprites;
using META.Engine.InterfaceSystem;

namespace META.GameWorld.Objects.Characters
{
    public class Butterfly : Enemy
    {
        public float? lastTurnAroundTime;
        public float turnAroundPeriod;
        public int direction;

        public Butterfly(Vector2 _position, float _turnAroundPeriod)
            : base(_position, SpriteID.Butterfly, Sound.ButterflyKill, Sound.ButterflyDeath, 100, 0)
        {
            turnAroundPeriod = _turnAroundPeriod;
            lastTurnAroundTime = null;
            direction = -1;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (StateMachineManager.CurrentState == State.Playing && lastTurnAroundTime == null)
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
