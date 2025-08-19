using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x02000345 RID: 837
	[LegacyName(new string[]
	{
		"PowerRanger_Body"
	})]
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Jofairden_Body : JofairdenArmorItem
	{
		// Token: 0x06002F35 RID: 12085 RVA: 0x00533847 File Offset: 0x00531A47
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(34f, 22f);
		}
	}
}
