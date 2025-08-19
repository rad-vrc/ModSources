using System;
using Terraria.Localization;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200030A RID: 778
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Linus_Legs : PatreonItem
	{
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06002E87 RID: 11911 RVA: 0x00531E33 File Offset: 0x00530033
		public override LocalizedText Tooltip
		{
			get
			{
				return this.GetLocalization("Tooltip", () => "");
			}
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x00531E5F File Offset: 0x0053005F
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 18;
		}
	}
}
