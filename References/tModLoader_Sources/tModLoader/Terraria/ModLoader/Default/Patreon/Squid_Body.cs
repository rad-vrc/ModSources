using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200032B RID: 811
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Squid_Body : PatreonItem
	{
		// Token: 0x06002EE3 RID: 12003 RVA: 0x00532B60 File Offset: 0x00530D60
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(34f, 26f);
		}
	}
}
