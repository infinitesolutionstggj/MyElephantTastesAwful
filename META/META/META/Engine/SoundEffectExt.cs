using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using META.GameWorld;

namespace META.Engine
{
	public static class SoundEffectExt
	{
		public static void PlayIfNotMuted(this SoundEffect sound)
		{
			if (!GameStats.Muted)
				sound.Play();
		}
	}
}
