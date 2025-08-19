using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200030D RID: 781
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Mayne_Body : PatreonItem
	{
		// Token: 0x06002E90 RID: 11920 RVA: 0x00531F5D File Offset: 0x0053015D
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 26;
			base.Item.height = 26;
		}
	}
}
