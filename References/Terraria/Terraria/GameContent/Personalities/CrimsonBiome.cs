using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003D2 RID: 978
	public class CrimsonBiome : AShoppingBiome
	{
		// Token: 0x06002A97 RID: 10903 RVA: 0x00599BC0 File Offset: 0x00597DC0
		public CrimsonBiome()
		{
			base.NameKey = "Crimson";
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x00599BD3 File Offset: 0x00597DD3
		public override bool IsInBiome(Player player)
		{
			return player.ZoneCrimson;
		}
	}
}
