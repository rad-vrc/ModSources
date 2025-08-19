using System;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002FC RID: 764
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Glory_Legs : PatreonItem
	{
		// Token: 0x06002E5E RID: 11870 RVA: 0x005316E3 File Offset: 0x0052F8E3
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Legs.Sets.OverridesLegs[base.Item.legSlot] = true;
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x005316FD File Offset: 0x0052F8FD
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(22f, 18f);
		}
	}
}
