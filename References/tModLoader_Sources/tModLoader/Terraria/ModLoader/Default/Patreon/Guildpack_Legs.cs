using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000302 RID: 770
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Guildpack_Legs : PatreonItem
	{
		// Token: 0x06002E71 RID: 11889 RVA: 0x00531B3A File Offset: 0x0052FD3A
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
