using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200030C RID: 780
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Mayne_Head : PatreonItem
	{
		// Token: 0x06002E8E RID: 11918 RVA: 0x00531F33 File Offset: 0x00530133
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 28;
			base.Item.height = 18;
		}
	}
}
