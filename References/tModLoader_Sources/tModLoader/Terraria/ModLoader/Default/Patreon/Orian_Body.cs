using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000313 RID: 787
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class Orian_Body : PatreonItem
	{
		// Token: 0x06002EA2 RID: 11938 RVA: 0x00532351 File Offset: 0x00530551
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 30;
			base.Item.height = 20;
		}
	}
}
