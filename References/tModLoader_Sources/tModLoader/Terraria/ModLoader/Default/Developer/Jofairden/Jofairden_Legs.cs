using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x02000347 RID: 839
	[LegacyName(new string[]
	{
		"PowerRanger_Legs"
	})]
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Legs
	})]
	internal class Jofairden_Legs : JofairdenArmorItem
	{
		// Token: 0x06002F3B RID: 12091 RVA: 0x005338E9 File Offset: 0x00531AE9
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(22f, 18f);
		}
	}
}
