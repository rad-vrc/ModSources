using System;

namespace Terraria.GameContent.Personalities
{
	// Token: 0x020005BE RID: 1470
	public class NPCPreferenceTrait : IShopPersonalityTrait
	{
		// Token: 0x060042B6 RID: 17078 RVA: 0x005FA1B2 File Offset: 0x005F83B2
		public void ModifyShopPrice(HelperInfo info, ShopHelper shopHelperInstance)
		{
			if (info.nearbyNPCsByType[this.NpcId])
			{
				shopHelperInstance.ApplyNpcRelationshipEffect(this.NpcId, this.Level);
			}
		}

		// Token: 0x040059CC RID: 22988
		public AffectionLevel Level;

		// Token: 0x040059CD RID: 22989
		public int NpcId;
	}
}
