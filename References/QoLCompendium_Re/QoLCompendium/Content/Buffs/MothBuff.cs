using System;
using QoLCompendium.Content.Projectiles.Dedicated;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Buffs
{
	// Token: 0x020001FC RID: 508
	public class MothBuff : ModBuff
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x00049E94 File Offset: 0x00048094
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.vanityPet[base.Type] = true;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00049EDC File Offset: 0x000480DC
		public override void Update(Player player, ref int buffIndex)
		{
			bool unused = false;
			player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<Moth>(), 18000);
		}
	}
}
