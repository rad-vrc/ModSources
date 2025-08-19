using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000324 RID: 804
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Saethar_Legs : PatreonItem
	{
		// Token: 0x06002ED3 RID: 11987 RVA: 0x00532999 File Offset: 0x00530B99
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(22f, 18f);
		}
	}
}
