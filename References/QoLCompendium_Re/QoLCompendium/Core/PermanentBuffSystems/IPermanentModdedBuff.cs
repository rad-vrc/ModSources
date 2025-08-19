using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core.PermanentBuffSystems
{
	// Token: 0x0200029E RID: 670
	public abstract class IPermanentModdedBuff : IComparable
	{
		// Token: 0x0600114C RID: 4428 RVA: 0x0008707E File Offset: 0x0008527E
		public int CompareTo(object obj)
		{
			return base.GetType().Name.CompareTo(obj.GetType().Name);
		}

		// Token: 0x0600114D RID: 4429
		internal abstract void ApplyEffect(PermanentBuffPlayer player);

		// Token: 0x0400075B RID: 1883
		public int index = 44;

		// Token: 0x0400075C RID: 1884
		public ModBuff buffToApply;
	}
}
