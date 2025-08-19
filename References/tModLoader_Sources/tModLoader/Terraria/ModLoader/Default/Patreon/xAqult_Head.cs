using System;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000334 RID: 820
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class xAqult_Head : PatreonItem
	{
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x00532E13 File Offset: 0x00531013
		public override LocalizedText Tooltip
		{
			get
			{
				return this.GetLocalization("Tooltip", () => "");
			}
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x00532E3F File Offset: 0x0053103F
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Head.Sets.DrawFullHair[base.Item.headSlot] = true;
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x00532E59 File Offset: 0x00531059
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 22;
			base.Item.height = 10;
		}
	}
}
