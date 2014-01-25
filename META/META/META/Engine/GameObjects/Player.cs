using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using META.Engine.Sprites;

namespace META.Engine.GameObjects
{
    class Player : GameObject
    {
        public float moveSpeed = 100;
        public Player(Rectangle _collisionBox, SpriteID _sprite, Rectangle? _relativeSpriteArea)
            : base(_collisionBox, _sprite, _relativeSpriteArea)
        {
            InputManager.AddCommand("Left", Keys.Left, Buttons.LeftThumbstickLeft);
            InputManager.AddCommand("Right", Keys.Right, Buttons.LeftThumbstickRight);
            InputManager.AddCommand("Jump", Keys.Space, Buttons.A);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 displacement = Vector2.Zero;

            if(InputManager.GetCommand("Left"))
                displacement += new Vector2(-moveSpeed, 0);
            if (InputManager.GetCommand("Right"))
                displacement += new Vector2(moveSpeed, 0);

            Move(displacement * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        void Move(Vector2 displacement)
        {
            position += displacement;
            
        }
    }
}
