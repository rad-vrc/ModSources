using System;
using Terraria.Localization;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000308 RID: 776
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Linus_Head : PatreonItem
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x00531D87 File Offset: 0x0052FF87
		public override LocalizedText Tooltip
		{
			get
			{
				return this.GetLocalization("Tooltip", () => "");
			}
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x00531DB3 File Offset: 0x0052FFB3
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 34;
			base.Item.height = 30;
		}
	}
}
