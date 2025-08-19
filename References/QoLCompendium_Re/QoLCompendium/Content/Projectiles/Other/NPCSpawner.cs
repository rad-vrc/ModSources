using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Other
{
	// Token: 0x02000025 RID: 37
	public class NPCSpawner : ModProjectile
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000046D4 File Offset: 0x000028D4
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Assets/Projectiles/Invisible";
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000046DC File Offset: 0x000028DC
		public override void SetDefaults()
		{
			base.Projectile.width = 2;
			base.Projectile.height = 2;
			base.Projectile.aiStyle = -1;
			base.Projectile.timeLeft = 1;
			base.Projectile.tileCollide = false;
			base.Projectile.ignoreWater = true;
			base.Projectile.hide = true;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool? CanDamage()
		{
			return new bool?(false);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004748 File Offset: 0x00002948
		public override void OnKill(int timeLeft)
		{
			if (Main.netMode == 1)
			{
				return;
			}
			int npc = NPC.NewNPC(NPC.GetBossSpawnSource(base.Projectile.owner), (int)base.Projectile.Center.X, (int)base.Projectile.Center.Y, (int)base.Projectile.ai[0], 0, 0f, 0f, 0f, 0f, 255);
			if (npc != Main.maxNPCs && Main.netMode == 2)
			{
				NetMessage.SendData(23, -1, -1, null, npc, 0f, 0f, 0f, 0, 0, 0);
			}
		}
	}
}
