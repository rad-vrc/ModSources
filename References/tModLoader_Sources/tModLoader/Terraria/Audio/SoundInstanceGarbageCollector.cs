using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x0200076A RID: 1898
	public static class SoundInstanceGarbageCollector
	{
		// Token: 0x06004CBA RID: 19642 RVA: 0x00670CC5 File Offset: 0x0066EEC5
		public static void Track(SoundEffectInstance sound)
		{
			if (Program.IsFna)
			{
				SoundInstanceGarbageCollector._activeSounds.Add(sound);
			}
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x00670CDC File Offset: 0x0066EEDC
		public static void Update()
		{
			for (int i = 0; i < SoundInstanceGarbageCollector._activeSounds.Count; i++)
			{
				if (SoundInstanceGarbageCollector._activeSounds[i] == null)
				{
					SoundInstanceGarbageCollector._activeSounds.RemoveAt(i);
					i--;
				}
				else if (SoundInstanceGarbageCollector._activeSounds[i].State == 2)
				{
					SoundInstanceGarbageCollector._activeSounds[i].Dispose();
					SoundInstanceGarbageCollector._activeSounds.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x0400611A RID: 24858
		private static readonly List<SoundEffectInstance> _activeSounds = new List<SoundEffectInstance>(128);
	}
}
