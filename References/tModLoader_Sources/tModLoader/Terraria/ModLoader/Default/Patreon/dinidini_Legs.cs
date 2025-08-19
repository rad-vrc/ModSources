using System;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002ED RID: 749
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class dinidini_Legs : PatreonItem
	{
		// Token: 0x06002E3C RID: 11836 RVA: 0x00531389 File Offset: 0x0052F589
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Legs.Sets.OverridesLegs[base.Item.legSlot] = true;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x005313A3 File Offset: 0x0052F5A3
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
