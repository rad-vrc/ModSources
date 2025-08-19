using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000327 RID: 807
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Sailing_Squid_Body : PatreonItem
	{
		// Token: 0x06002EDA RID: 11994 RVA: 0x00532A6A File Offset: 0x00530C6A
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 42;
			base.Item.height = 24;
		}
	}
}
