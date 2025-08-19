using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002EB RID: 747
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class dinidini_Head : PatreonItem
	{
		// Token: 0x06002E37 RID: 11831 RVA: 0x0053131B File Offset: 0x0052F51B
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 28;
			base.Item.height = 20;
		}
	}
}
