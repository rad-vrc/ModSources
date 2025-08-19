using System;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000335 RID: 821
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class xAqult_Body : PatreonItem
	{
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06002EFF RID: 12031 RVA: 0x00532E83 File Offset: 0x00531083
		public override LocalizedText Tooltip
		{
			get
			{
				return this.GetLocalization("Tooltip", () => "");
			}
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x00532EAF File Offset: 0x005310AF
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Body.Sets.HidesHands[base.Item.bodySlot] = false;
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x00532EC9 File Offset: 0x005310C9
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 34;
			base.Item.height = 26;
		}
	}
}
