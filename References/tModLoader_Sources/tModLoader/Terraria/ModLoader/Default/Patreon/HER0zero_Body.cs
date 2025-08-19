using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000304 RID: 772
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Body
	})]
	internal class HER0zero_Body : PatreonItem
	{
		// Token: 0x06002E75 RID: 11893 RVA: 0x00531B8E File Offset: 0x0052FD8E
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 32;
			base.Item.height = 20;
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x00531BB0 File Offset: 0x0052FDB0
		public override bool IsVanitySet(int head, int body, int legs)
		{
			return head == EquipLoader.GetEquipSlot(base.Mod, "HER0zero_Head", EquipType.Head) && body == EquipLoader.GetEquipSlot(base.Mod, "HER0zero_Body", EquipType.Body) && legs == EquipLoader.GetEquipSlot(base.Mod, "HER0zero_Legs", EquipType.Legs);
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x00531BF0 File Offset: 0x0052FDF0
		public override void UpdateVanitySet(Player player)
		{
			player.GetModPlayer<HER0zeroPlayer>().glowEffect = true;
		}
	}
}
