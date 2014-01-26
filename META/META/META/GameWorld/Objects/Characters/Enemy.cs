using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using META.Engine.Sprites;

namespace META.GameWorld.Objects.Characters
{
    public abstract class Enemy : Character
    {
		public SoundEffect killSound;
		public SoundEffect deathSound;

        public Enemy(Vector2 _position, SpriteID _sprite, SoundEffect _killSound, SoundEffect _deathSound, float _moveSpeed, float _gravity = DEFAULT_GRAVITY)
            : base(_position, _sprite, _moveSpeed)
        {
			killSound = _killSound;
			deathSound = _deathSound;
        }
		
		public Enemy(Rectangle _collisionBox, SpriteID _sprite, SoundEffect _killSound, SoundEffect _deathSound, float _moveSpeed, float _gravity = DEFAULT_GRAVITY, Rectangle? _relativeSpriteArea = null)
			: base(_collisionBox, _sprite, _moveSpeed, _gravity, _relativeSpriteArea)
		{
			killSound = _killSound;
			deathSound = _deathSound;
		}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Reset()
        {
            base.Reset();
        }
    }
}
