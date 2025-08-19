using System;
using Microsoft.Xna.Framework;

namespace Terraria.ModLoader.Default.Developer.Jofairden
{
	// Token: 0x02000346 RID: 838
	[LegacyName(new string[]
	{
		"PowerRanger_Head"
	})]
	[AutoloadEquip(new EquipType[]
	{
		EquipType.Head
	})]
	internal class Jofairden_Head : JofairdenArmorItem
	{
		// Token: 0x06002F37 RID: 12087 RVA: 0x00533871 File Offset: 0x00531A71
		public override void SetDefaults()
		{
			base.SetDefaults();
			base.Item.Size = new Vector2(18f, 20f);
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x00533893 File Offset: 0x00531A93
		public override bool IsVanitySet(int head, int body, int legs)
		{
			return head == EquipLoader.GetEquipSlot(base.Mod, "Jofairden_Head", EquipType.Head) && body == EquipLoader.GetEquipSlot(base.Mod, "Jofairden_Body", EquipType.Body) && legs == EquipLoader.GetEquipSlot(base.Mod, "Jofairden_Legs", EquipType.Legs);
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x005338D3 File Offset: 0x00531AD3
		public override void UpdateVanitySet(Player player)
		{
			player.GetModPlayer<JofairdenArmorEffectPlayer>().HasSetBonus = true;
		}
	}
}
