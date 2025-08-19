using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000318 RID: 792
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class POCKETS_Legs : PatreonItem
	{
		// Token: 0x06002EB3 RID: 11955 RVA: 0x005324E6 File Offset: 0x005306E6
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(22f, 18f);
		}
	}
}
