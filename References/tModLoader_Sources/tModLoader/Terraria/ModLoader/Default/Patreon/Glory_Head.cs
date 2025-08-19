using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x020002FA RID: 762
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Glory_Head : PatreonItem
	{
		// Token: 0x06002E5A RID: 11866 RVA: 0x0053168F File Offset: 0x0052F88F
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(30f, 32f);
		}
	}
}
