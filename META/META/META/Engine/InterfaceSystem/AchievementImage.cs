using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using META.Engine;
using META.Engine.Sprites;
using META.Engine.Achievements;

namespace META.Engine.InterfaceSystem
{
    public class AchievementImage
    {
        public Vector2 position;
        public AchievementID achievementID;
        public SpriteInstance backgroundSprite;
        public bool active;

        public SpriteFont debugFont;

        public AchievementImage(Vector2 _position, AchievementID _achievementID, SpriteFont _debugFont)
        {
            backgroundSprite = new SpriteInstance(SpriteManager.GetSprite(SpriteID.AchievementBG));
            achievementID = _achievementID;
            active = false;
            debugFont = _debugFont;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!active)
                return;

            spriteBatch.DrawString(debugFont, "Testing", position, Color.Red);
        }
    }
}
