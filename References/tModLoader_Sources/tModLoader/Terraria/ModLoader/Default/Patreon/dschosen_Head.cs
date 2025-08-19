using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002EF RID: 751
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class dschosen_Head : PatreonItem
	{
		// Token: 0x06002E42 RID: 11842 RVA: 0x0053144A File Offset: 0x0052F64A
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 34;
			base.Item.height = 22;
		}
	}
}
