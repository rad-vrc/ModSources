using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000328 RID: 808
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Sailing_Squid_Legs : PatreonItem
	{
		// Token: 0x06002EDC RID: 11996 RVA: 0x00532A94 File Offset: 0x00530C94
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
