using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000339 RID: 825
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Wings
	})]
	internal class xAqult_Wings : PatreonItem
	{
		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06002F0E RID: 12046 RVA: 0x005330A6 File Offset: 0x005312A6
		public override LocalizedText Tooltip
		{
			get
			{
				return this.GetLocalization("Tooltip", () => "");
			}
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x005330D2 File Offset: 0x005312D2
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(150, 7f, 1f, false, -1f, 1f);
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0053310E File Offset: 0x0053130E
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.vanity = false;
			base.Item.width = 28;
			base.Item.height = 52;
			base.Item.accessory = true;
		}
	}
}
