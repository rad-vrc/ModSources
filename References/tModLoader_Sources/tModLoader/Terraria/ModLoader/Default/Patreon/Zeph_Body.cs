using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200033E RID: 830
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Zeph_Body : PatreonItem
	{
		// Token: 0x06002F1E RID: 12062 RVA: 0x0053355E File Offset: 0x0053175E
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 42;
			base.Item.height = 24;
		}
	}
}
