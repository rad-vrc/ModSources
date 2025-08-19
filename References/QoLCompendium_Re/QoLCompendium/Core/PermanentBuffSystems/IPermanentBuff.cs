using System;

namespace QoLCompendium.Core.PermanentBuffSystems
{
	// Token: 0x0200029C RID: 668
	public abstract class IPermanentBuff : IComparable
	{
		// Token: 0x06001142 RID: 4418 RVA: 0x0008707E File Offset: 0x0008527E
		public int CompareTo(object obj)
		{
			return base.GetType().Name.CompareTo(obj.GetType().Name);
		}

		// Token: 0x06001143 RID: 4419
		internal abstract void ApplyEffect(PermanentBuffPlayer player);
	}
}
