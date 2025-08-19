using System;
using QoLCompendium.Content.Projectiles.Dedicated;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Buffs
{
	// Token: 0x020001FE RID: 510
	public class SnakeBuff : ModBuff
	{
		// Token: 0x06000B68 RID: 2920 RVA: 0x00049E94 File Offset: 0x00048094
		public override void SetStaticDefaults()
		{
			Main.buffNoTimeDisplay[base.Type] = true;
			Main.vanityPet[base.Type] = true;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00049F24 File Offset: 0x00048124
		public override void Update(Player player, ref int buffIndex)
		{
			bool unused = false;
			player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<Snake>(), 18000);
		}
	}
}
