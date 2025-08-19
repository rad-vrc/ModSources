using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000314 RID: 788
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Orian_Legs : PatreonItem
	{
		// Token: 0x06002EA4 RID: 11940 RVA: 0x0053237B File Offset: 0x0053057B
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
