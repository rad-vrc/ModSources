using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002E3 RID: 739
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class AlejandroAkbal_Head : PatreonItem
	{
		// Token: 0x06002E26 RID: 11814 RVA: 0x0053116C File Offset: 0x0052F36C
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 34;
			base.Item.height = 22;
		}
	}
}
