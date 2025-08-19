using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003C7 RID: 967
	public abstract class AShoppingBiome
	{
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x00599AA1 File Offset: 0x00597CA1
		// (set) Token: 0x06002A80 RID: 10880 RVA: 0x00599AA9 File Offset: 0x00597CA9
		public string NameKey { get; protected set; }

		// Token: 0x06002A81 RID: 10881
		public abstract bool IsInBiome(Player player);
	}
}
