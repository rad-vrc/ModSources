using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200031C RID: 796
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Polyblank_Legs : PatreonItem
	{
		// Token: 0x06002EBD RID: 11965 RVA: 0x005325FB File Offset: 0x005307FB
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
