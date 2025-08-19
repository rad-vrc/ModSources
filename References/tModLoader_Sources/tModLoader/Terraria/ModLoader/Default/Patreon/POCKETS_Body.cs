using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000317 RID: 791
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class POCKETS_Body : PatreonItem
	{
		// Token: 0x06002EB1 RID: 11953 RVA: 0x005324BC File Offset: 0x005306BC
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(30f, 18f);
		}
	}
}
