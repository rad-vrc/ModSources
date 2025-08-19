using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200032C RID: 812
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Squid_Legs : PatreonItem
	{
		// Token: 0x06002EE5 RID: 12005 RVA: 0x00532B8A File Offset: 0x00530D8A
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(22f, 18f);
		}
	}
}
