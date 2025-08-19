using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200031A RID: 794
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Polyblank_Head : PatreonItem
	{
		// Token: 0x06002EB8 RID: 11960 RVA: 0x0053258D File Offset: 0x0053078D
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 24;
			base.Item.height = 22;
		}
	}
}
