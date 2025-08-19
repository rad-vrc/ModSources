using System;
using Terraria.ModLoader;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005B2 RID: 1458
	public abstract class AShoppingBiome : IShoppingBiome, ILoadable
	{
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x06004297 RID: 17047 RVA: 0x005F9FCF File Offset: 0x005F81CF
		// (set) Token: 0x06004298 RID: 17048 RVA: 0x005F9FD7 File Offset: 0x005F81D7
		public string NameKey { get; protected set; }

		// Token: 0x06004299 RID: 17049 RVA: 0x005F9FE0 File Offset: 0x005F81E0
		internal AShoppingBiome()
		{
		}

		// Token: 0x0600429A RID: 17050
		public abstract bool IsInBiome(Player player);

		// Token: 0x0600429B RID: 17051 RVA: 0x005F9FE8 File Offset: 0x005F81E8
		void ILoadable.Load(Mod mod)
		{
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x005F9FEA File Offset: 0x005F81EA
		void ILoadable.Unload()
		{
		}
	}
}
