using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200031D RID: 797
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Remeus_Head : PatreonItem
	{
		// Token: 0x06002EBF RID: 11967 RVA: 0x00532625 File Offset: 0x00530825
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(34f);
		}
	}
}
