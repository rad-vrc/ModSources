using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002E6 RID: 742
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Back
	})]
	internal class AlejandroAkbal_Back : PatreonItem
	{
		// Token: 0x06002E2C RID: 11820 RVA: 0x005311EA File Offset: 0x0052F3EA
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
			base.Item.accessory = true;
		}
	}
}
