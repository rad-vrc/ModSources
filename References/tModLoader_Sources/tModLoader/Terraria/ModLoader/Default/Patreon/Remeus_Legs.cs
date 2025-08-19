using System;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200031F RID: 799
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Remeus_Legs : PatreonItem
	{
		// Token: 0x06002EC4 RID: 11972 RVA: 0x0053268E File Offset: 0x0053088E
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Legs.Sets.HidesBottomSkin[base.Item.legSlot] = true;
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x005326A8 File Offset: 0x005308A8
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(22f, 18f);
		}
	}
}
