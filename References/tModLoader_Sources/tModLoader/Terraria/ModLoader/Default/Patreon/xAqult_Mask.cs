using System;
using Terraria.Localization;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000337 RID: 823
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class xAqult_Mask : PatreonItem
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06002F06 RID: 12038 RVA: 0x00532F49 File Offset: 0x00531149
		public override LocalizedText Tooltip
		{
			get
			{
				return this.GetLocalization("Tooltip", () => "");
			}
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x00532F75 File Offset: 0x00531175
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 30;
			base.Item.height = 34;
		}
	}
}
