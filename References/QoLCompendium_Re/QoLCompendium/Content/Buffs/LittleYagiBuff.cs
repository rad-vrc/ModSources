using System;
using QoLCompendium.Content.Projectiles.Dedicated;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Buffs
{
	// Token: 0x020001FB RID: 507
	public class LittleYagiBuff : ModBuff
	{
		// Token: 0x06000B5F RID: 2911 RVA: 0x00049E94 File Offset: 0x00048094
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.vanityPet[base.Type] = true;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00049EB0 File Offset: 0x000480B0
		public override void Update(Player player, ref int buffIndex)
		{
			bool unused = false;
			player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<LittleYagi>(), 18000);
		}
	}
}
