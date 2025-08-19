using System;
using Terraria.Localization;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000309 RID: 777
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Linus_Body : PatreonItem
	{
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06002E84 RID: 11908 RVA: 0x00531DDD File Offset: 0x0052FFDD
		public override LocalizedText Tooltip
		{
			get
			{
				return this.GetLocalization("Tooltip", () => "");
			}
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x00531E09 File Offset: 0x00530009
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 34;
			base.Item.height = 24;
		}
	}
}
