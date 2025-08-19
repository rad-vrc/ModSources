using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using QoLCompendium.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Other
{
	// Token: 0x02000023 RID: 35
	public class GoldenPowderProjectile : ModProjectile
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004234 File Offset: 0x00002434
		public override void SetDefaults()
		{
			base.Projectile.CloneDefaults(10);
			base.Projectile.aiStyle = -1;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004250 File Offset: 0x00002450
		public override void AI()
		{
			base.Projectile.velocity *= 0.95f;
			base.Projectile.ai[0] += 1f;
			if (base.Projectile.ai[0] == 180f)
			{
				base.Projectile.Kill();
			}
			if (base.Projectile.ai[1] == 0f)
			{
				base.Projectile.ai[1] = 1f;
				for (int i = 0; i < 30; i++)
				{
					Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 246, base.Projectile.velocity.X, base.Projectile.velocity.Y, 50, default(Color), 1f);
				}
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004340 File Offset: 0x00002540
		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			if (Main.netMode != 1)
			{
				IEnumerable<NPC> npc2 = Main.npc;
				Func<NPC, bool> predicate;
				Func<NPC, bool> <>9__0;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((NPC n) => n.active && hitbox.Intersects(n.getRect())));
				}
				foreach (NPC npc in npc2.Where(predicate))
				{
					if (Common.NormalBunnies[npc.type])
					{
						npc.Transform(443);
					}
					if (Common.NormalSquirrels[npc.type])
					{
						npc.Transform(539);
					}
					if (Common.NormalButterflies[npc.type])
					{
						npc.Transform(444);
					}
					if (Common.NormalBirds[npc.type])
					{
						npc.Transform(442);
					}
					if (NPCID.Sets.IsDragonfly[npc.type] && npc.type != 601)
					{
						npc.Transform(601);
					}
					if (npc.type == 361)
					{
						npc.Transform(445);
					}
					if (npc.type == 55)
					{
						npc.Transform(592);
					}
					if (npc.type == 230)
					{
						npc.Transform(593);
					}
					if (npc.type == 377)
					{
						npc.Transform(446);
					}
					if (npc.type == 604)
					{
						npc.Transform(605);
					}
					if (npc.type == 300)
					{
						npc.Transform(447);
					}
					if (npc.type == 626)
					{
						npc.Transform(627);
					}
					if (npc.type == 612)
					{
						npc.Transform(613);
					}
					if (npc.type == 357 || npc.type == 374 || npc.type == 375)
					{
						npc.Transform(448);
					}
				}
			}
		}
	}
}
