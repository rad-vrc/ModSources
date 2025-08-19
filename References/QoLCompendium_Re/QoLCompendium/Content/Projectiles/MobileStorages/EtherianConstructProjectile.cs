using System;
using System.Collections.Generic;
using QoLCompendium.Content.Items.Tools.MobileStorages;
using QoLCompendium.Core.Changes.PlayerChanges;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.MobileStorages
{
	// Token: 0x02000026 RID: 38
	public class EtherianConstructProjectile : ModProjectile
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000047EC File Offset: 0x000029EC
		public override void SetDefaults()
		{
			base.Projectile.width = 22;
			base.Projectile.height = 36;
			base.Projectile.aiStyle = 97;
			base.Projectile.tileCollide = false;
			base.Projectile.timeLeft = 10800;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000483C File Offset: 0x00002A3C
		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverPlayers, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsOverWiresUI.Add(base.Projectile.whoAmI);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004850 File Offset: 0x00002A50
		public override void PostAI()
		{
			if (Main.netMode != 2)
			{
				Player player = Main.player[Main.myPlayer];
				BankPlayer modPlayer = player.GetModPlayer<BankPlayer>();
				PortableBankAI.BankAI(base.Projectile, ModContent.ItemType<EtherianConstruct>(), -4, ref modPlayer.defenders, player, modPlayer);
			}
		}
	}
}
