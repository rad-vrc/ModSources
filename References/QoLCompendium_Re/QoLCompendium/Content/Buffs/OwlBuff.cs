using System;
using QoLCompendium.Content.Projectiles.Dedicated;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Buffs
{
	// Token: 0x020001FD RID: 509
	public class OwlBuff : ModBuff
	{
		// Token: 0x06000B65 RID: 2917 RVA: 0x00049E94 File Offset: 0x00048094
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.vanityPet[base.Type] = true;
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00049F00 File Offset: 0x00048100
		public override void Update(Player player, ref int buffIndex)
		{
			bool unused = false;
			player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<Owl>(), 18000);
		}
	}
}
