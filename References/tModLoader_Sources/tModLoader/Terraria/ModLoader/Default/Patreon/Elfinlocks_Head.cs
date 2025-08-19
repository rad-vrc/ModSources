using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002F3 RID: 755
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Elfinlocks_Head : PatreonItem
	{
		// Token: 0x06002E4B RID: 11851 RVA: 0x00531545 File Offset: 0x0052F745
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 34;
			base.Item.height = 22;
		}
	}
}
