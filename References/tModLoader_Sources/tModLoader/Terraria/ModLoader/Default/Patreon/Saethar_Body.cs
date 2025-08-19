using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000323 RID: 803
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Saethar_Body : PatreonItem
	{
		// Token: 0x06002ED1 RID: 11985 RVA: 0x0053296F File Offset: 0x00530B6F
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(30f, 18f);
		}
	}
}
