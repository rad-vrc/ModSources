using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002F1 RID: 753
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class dschosen_Legs : PatreonItem
	{
		// Token: 0x06002E46 RID: 11846 RVA: 0x0053149E File Offset: 0x0052F69E
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
