using System;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200031E RID: 798
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Remeus_Body : PatreonItem
	{
		// Token: 0x06002EC1 RID: 11969 RVA: 0x0053264A File Offset: 0x0053084A
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Body.Sets.HidesTopSkin[base.Item.bodySlot] = true;
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x00532664 File Offset: 0x00530864
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(30f, 18f);
		}
	}
}
