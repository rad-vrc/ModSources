using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x0200032F RID: 815
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Tantamount_Legs : PatreonItem
	{
		// Token: 0x06002EEE RID: 12014 RVA: 0x00532CB9 File Offset: 0x00530EB9
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(22f, 18f);
		}
	}
}
