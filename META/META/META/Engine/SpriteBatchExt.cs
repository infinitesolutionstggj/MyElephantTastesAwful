using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace META.Engine
{
    public static class SpriteBatchExt
    {
        public static void DrawStringCentered(this SpriteBatch spriteBatch, SpriteFont font, string text, Vector2 position, Color color)
        {
            spriteBatch.DrawString(font, text, position - font.MeasureString(text) / 2, color);
        }
    }
}
