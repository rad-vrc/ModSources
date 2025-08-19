using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005B1 RID: 1457
	public interface IShoppingBiome
	{
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06004295 RID: 17045
		string NameKey { get; }

		// Token: 0x06004296 RID: 17046
		bool IsInBiome(Player player);
	}
}
