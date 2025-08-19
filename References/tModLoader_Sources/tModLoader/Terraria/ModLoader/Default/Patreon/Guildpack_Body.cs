using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000301 RID: 769
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Guildpack_Body : PatreonItem
	{
		// Token: 0x06002E6F RID: 11887 RVA: 0x00531B10 File Offset: 0x0052FD10
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 42;
			base.Item.height = 24;
		}
	}
}
