using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Terraria.Audio
{
	// Token: 0x02000487 RID: 1159
	public static class SoundInstanceGarbageCollector
	{
		// Token: 0x06002E2C RID: 11820 RVA: 0x005C3448 File Offset: 0x005C1648
		public static void Track(SoundEffectInstance sound)
		{
			if (Program.IsFna)
			{
				SoundInstanceGarbageCollector._activeSounds.Add(sound);
			}
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x005C345C File Offset: 0x005C165C
		public static void Update()
		{
			for (int i = 0; i < SoundInstanceGarbageCollector._activeSounds.Count; i++)
			{
				if (SoundInstanceGarbageCollector._activeSounds[i] == null)
				{
					SoundInstanceGarbageCollector._activeSounds.RemoveAt(i);
					i--;
				}
				else if (SoundInstanceGarbageCollector._activeSounds[i].State == SoundState.Stopped)
				{
					SoundInstanceGarbageCollector._activeSounds[i].Dispose();
					SoundInstanceGarbageCollector._activeSounds.RemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x040051BB RID: 20923
		private static readonly List<SoundEffectInstance> _activeSounds = new List<SoundEffectInstance>(128);
	}
}
