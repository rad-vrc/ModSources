using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000331 RID: 817
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class toplayz_Head : PatreonItem
	{
		// Token: 0x06002EF3 RID: 12019 RVA: 0x00532D61 File Offset: 0x00530F61
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 28;
			base.Item.height = 26;
		}
	}
}
