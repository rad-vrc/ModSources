using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000326 RID: 806
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Sailing_Squid_Head : PatreonItem
	{
		// Token: 0x06002ED8 RID: 11992 RVA: 0x00532A40 File Offset: 0x00530C40
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 34;
			base.Item.height = 22;
		}
	}
}
