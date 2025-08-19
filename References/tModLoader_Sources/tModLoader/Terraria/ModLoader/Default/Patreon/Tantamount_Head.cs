using System;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200032D RID: 813
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Tantamount_Head : PatreonItem
	{
		// Token: 0x06002EE7 RID: 12007 RVA: 0x00532BB4 File Offset: 0x00530DB4
		public override void OnCreated(ItemCreationContext context)
		{
			base.OnCreated(context);
			if (context is InitializationItemCreationContext)
			{
				EquipLoader.AddEquipTexture(base.Mod, this.Texture + "_Head", EquipType.Face, this, null, null);
			}
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x00532BE6 File Offset: 0x00530DE6
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			ArmorIDs.Head.Sets.DrawFullHair[base.Item.headSlot] = true;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x00532C00 File Offset: 0x00530E00
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.accessory = true;
			base.Item.Size = new Vector2(26f);
		}
	}
}
