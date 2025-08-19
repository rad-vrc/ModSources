using System;

namespace Terraria.ModLoader.Default.Patreon
{
	// Token: 0x02000312 RID: 786
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Orian_Head : PatreonItem
	{
		// Token: 0x06002E9E RID: 11934 RVA: 0x005322D9 File Offset: 0x005304D9
		public override bool IsVanitySet(int head, int body, int legs)
		{
			return head == EquipLoader.GetEquipSlot(base.Mod, "Orian_Head", EquipType.Head) && body == EquipLoader.GetEquipSlot(base.Mod, "Orian_Body", EquipType.Body) && legs == EquipLoader.GetEquipSlot(base.Mod, "Orian_Legs", EquipType.Legs);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x00532319 File Offset: 0x00530519
		public override void UpdateVanitySet(Player player)
		{
			player.GetModPlayer<OrianSetEffectPlayer>().IsActive = true;
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x00532327 File Offset: 0x00530527
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.width = 24;
			base.Item.height = 24;
		}
	}
}
