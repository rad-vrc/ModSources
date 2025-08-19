using System;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200032E RID: 814
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Tantamount_Body : PatreonItem
	{
		// Token: 0x06002EEB RID: 12011 RVA: 0x00532C34 File Offset: 0x00530E34
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Body.Sets.HidesArms[base.Item.bodySlot] = true;
			ArmorIDs.Body.Sets.HidesHands[base.Item.bodySlot] = false;
			ArmorIDs.Body.Sets.HidesTopSkin[base.Item.bodySlot] = false;
			ArmorIDs.Body.Sets.shouldersAreAlwaysInTheBack[base.Item.bodySlot] = true;
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x00532C8F File Offset: 0x00530E8F
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(26f, 24f);
		}
	}
}
