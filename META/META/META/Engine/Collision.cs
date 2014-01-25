using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace META
{
	public static class Collision
	{
		public static Rectangle? Rect(Rectangle a, Rectangle b)
		{
			int left, right, top, bottom, width, height;

			left = Math.Max(a.Left, b.Left);
			right = Math.Min(a.Right, b.Right);
			top = Math.Max(a.Top, b.Top);
			bottom = Math.Min(a.Bottom, b.Bottom);

			width = right - left;
			height = bottom - top;

			if (width < 0 || height < 0)
				return null;

			return new Rectangle(left, top, width, height);
		}
	}
}
