using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000316 RID: 790
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class POCKETS_Head : PatreonItem
	{
		// Token: 0x06002EAF RID: 11951 RVA: 0x00532497 File Offset: 0x00530697
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(34f);
		}
	}
}
