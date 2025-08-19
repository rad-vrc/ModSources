using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002DF RID: 735
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class WitchDaggah_Head : PatreonItem
	{
		// Token: 0x06002E1D RID: 11805 RVA: 0x00531071 File Offset: 0x0052F271
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 34;
			base.Item.height = 22;
		}
	}
}
