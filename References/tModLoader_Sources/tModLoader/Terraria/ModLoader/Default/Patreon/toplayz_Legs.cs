using System;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000333 RID: 819
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class toplayz_Legs : PatreonItem
	{
		// Token: 0x06002EF8 RID: 12024 RVA: 0x00532DCF File Offset: 0x00530FCF
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Legs.Sets.HidesBottomSkin[base.Item.legSlot] = true;
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x00532DE9 File Offset: 0x00530FE9
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
