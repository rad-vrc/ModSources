using System;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200030B RID: 779
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Wings
	})]
	internal class Linus_Wings : PatreonItem
	{
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x00531E89 File Offset: 0x00530089
		public override LocalizedText Tooltip
		{
			get
			{
				return this.GetLocalization("Tooltip", () => "");
			}
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x00531EB5 File Offset: 0x005300B5
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(150, 7f, 1f, false, -1f, 1f);
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x00531EF1 File Offset: 0x005300F1
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.vanity = false;
			base.Item.width = 32;
			base.Item.height = 32;
			base.Item.accessory = true;
		}
	}
}
