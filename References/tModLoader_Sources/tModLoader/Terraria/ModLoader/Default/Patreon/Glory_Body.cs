using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002FB RID: 763
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Glory_Body : PatreonItem
	{
		// Token: 0x06002E5C RID: 11868 RVA: 0x005316B9 File Offset: 0x0052F8B9
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(34f, 24f);
		}
	}
}
