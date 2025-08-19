using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002E5 RID: 741
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class AlejandroAkbal_Legs : PatreonItem
	{
		// Token: 0x06002E2A RID: 11818 RVA: 0x005311C0 File Offset: 0x0052F3C0
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
