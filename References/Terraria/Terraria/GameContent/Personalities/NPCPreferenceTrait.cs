using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020003C4 RID: 964
	public class NPCPreferenceTrait : IShopPersonalityTrait
	{
		// Token: 0x06002A76 RID: 10870 RVA: 0x005995E0 File Offset: 0x005977E0
		public void ModifyShopPrice(HelperInfo info, ShopHelper shopHelperInstance)
		{
			if (!info.nearbyNPCsByType[this.NpcId])
			{
				return;
			}
			AffectionLevel level = this.Level;
			if (level <= AffectionLevel.Dislike)
			{
				if (level != AffectionLevel.Hate)
				{
					if (level != AffectionLevel.Dislike)
					{
						return;
					}
					shopHelperInstance.DislikeNPC(this.NpcId);
					return;
				}
				else
				{
					shopHelperInstance.HateNPC(this.NpcId);
				}
			}
			else
			{
				if (level == AffectionLevel.Like)
				{
					shopHelperInstance.LikeNPC(this.NpcId);
					return;
				}
				if (level == AffectionLevel.Love)
				{
					shopHelperInstance.LoveNPC(this.NpcId);
					return;
				}
			}
		}

		// Token: 0x04004D35 RID: 19765
		public AffectionLevel Level;

		// Token: 0x04004D36 RID: 19766
		public int NpcId;
	}
}
