using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200033D RID: 829
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Zeph_Head : PatreonItem
	{
		// Token: 0x06002F1C RID: 12060 RVA: 0x00533534 File Offset: 0x00531734
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 34;
			base.Item.height = 22;
		}
	}
}
