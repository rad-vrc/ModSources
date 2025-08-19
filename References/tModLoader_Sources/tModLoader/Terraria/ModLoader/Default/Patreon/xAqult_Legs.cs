using System;
using Terraria.Localization;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000336 RID: 822
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class xAqult_Legs : PatreonItem
	{
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06002F03 RID: 12035 RVA: 0x00532EF3 File Offset: 0x005310F3
		public override LocalizedText Tooltip
		{
			get
			{
				return this.GetLocalization("Tooltip", () => "");
			}
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x00532F1F File Offset: 0x0053111F
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
