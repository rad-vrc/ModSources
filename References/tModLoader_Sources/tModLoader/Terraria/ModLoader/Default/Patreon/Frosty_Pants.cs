using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002F8 RID: 760
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Frosty_Pants : PatreonItem
	{
		// Token: 0x06002E56 RID: 11862 RVA: 0x00531631 File Offset: 0x0052F831
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
