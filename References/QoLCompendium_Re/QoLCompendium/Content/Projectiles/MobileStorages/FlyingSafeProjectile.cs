using System;
using System.Collections.Generic;
using QoLCompendium.Content.Items.Tools.MobileStorages;
using QoLCompendium.Core.Changes.PlayerChanges;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.MobileStorages
{
	// Token: 0x02000027 RID: 39
	public class FlyingSafeProjectile : ModProjectile
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00004892 File Offset: 0x00002A92
		public override void SetStaticDefaults()
		{
			Main.projFrames[base.Projectile.type] = 8;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000048A8 File Offset: 0x00002AA8
		public override void SetDefaults()
		{
			base.Projectile.width = 50;
			base.Projectile.height = 32;
			base.Projectile.aiStyle = 97;
			base.Projectile.tileCollide = false;
			base.Projectile.timeLeft = 10800;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000483C File Offset: 0x00002A3C
		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverPlayers, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsOverWiresUI.Add(base.Projectile.whoAmI);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000048F8 File Offset: 0x00002AF8
		public override void PostAI()
		{
			if (Main.netMode != 2)
			{
				Player player = Main.player[Main.myPlayer];
				BankPlayer modPlayer = player.GetModPlayer<BankPlayer>();
				PortableBankAI.BankAI(base.Projectile, ModContent.ItemType<FlyingSafe>(), -3, ref modPlayer.safe, player, modPlayer);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000493C File Offset: 0x00002B3C
		public override void AI()
		{
			Projectile projectile = base.Projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 15)
			{
				base.Projectile.frameCounter = 0;
				Projectile projectile2 = base.Projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= Main.projFrames[base.Projectile.type])
				{
					base.Projectile.frame = 0;
				}
			}
		}
	}
}
