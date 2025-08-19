using System;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002F6 RID: 758
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Frosty_Hat : PatreonItem
	{
		// Token: 0x06002E51 RID: 11857 RVA: 0x005315C3 File Offset: 0x0052F7C3
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Head.Sets.DrawHatHair[base.Item.headSlot] = true;
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x005315DD File Offset: 0x0052F7DD
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 28;
			base.Item.height = 16;
		}
	}
}
