using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003CA RID: 970
	public class SnowBiome : AShoppingBiome
	{
		// Token: 0x06002A87 RID: 10887 RVA: 0x00599AE8 File Offset: 0x00597CE8
		public SnowBiome()
		{
			base.NameKey = "Snow";
		}

		// Token: 0x06002A88 RID: 10888 RVA: 0x00599AFB File Offset: 0x00597CFB
		public override bool IsInBiome(Player player)
		{
			return player.ZoneSnow;
		}
	}
}
