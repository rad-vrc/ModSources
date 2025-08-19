using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002F5 RID: 757
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Elfinlocks_Legs : PatreonItem
	{
		// Token: 0x06002E4F RID: 11855 RVA: 0x00531599 File Offset: 0x0052F799
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
