using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002DE RID: 734
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class AetherBreaker_Head : PatreonItem
	{
		// Token: 0x06002E1B RID: 11803 RVA: 0x00531047 File Offset: 0x0052F247
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 34;
			base.Item.height = 22;
		}
	}
}
