using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200032A RID: 810
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Squid_Head : PatreonItem
	{
		// Token: 0x06002EE1 RID: 12001 RVA: 0x00532B3B File Offset: 0x00530D3B
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(26f);
		}
	}
}
