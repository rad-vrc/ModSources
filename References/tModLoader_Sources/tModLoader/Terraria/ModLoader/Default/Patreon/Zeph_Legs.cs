using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200033F RID: 831
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Zeph_Legs : PatreonItem
	{
		// Token: 0x06002F20 RID: 12064 RVA: 0x00533588 File Offset: 0x00531788
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
