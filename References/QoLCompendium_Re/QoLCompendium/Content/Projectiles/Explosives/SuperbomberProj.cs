using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Explosives
{
	// Token: 0x02000037 RID: 55
	public class SuperbomberProj : ModProjectile
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000078AF File Offset: 0x00005AAF
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Content/Items/Tools/Explosives/Superbomber";
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000078B8 File Offset: 0x00005AB8
		public override void SetDefaults()
		{
			base.Projectile.width = 29;
			base.Projectile.height = 29;
			base.Projectile.aiStyle = 16;
			base.Projectile.friendly = true;
			base.Projectile.penetrate = -1;
			base.Projectile.timeLeft = 300;
			base.DrawOffsetX = -13;
			base.DrawOriginOffsetY = -20;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool? CanDamage()
		{
			return new bool?(false);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007924 File Offset: 0x00005B24
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			base.Projectile.velocity.X = 0f;
			return base.OnTileCollide(oldVelocity);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007944 File Offset: 0x00005B44
		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item15, new Vector2?(base.Projectile.Center), null);
			SoundEngine.PlaySound(SoundID.Item14, new Vector2?(base.Projectile.Center), null);
			if (Main.netMode == 1)
			{
				return;
			}
			Vector2 position = base.Projectile.Center;
			int radius = 64;
			for (int x = -radius; x <= radius; x++)
			{
				for (int y = -radius * 2; y <= 0; y++)
				{
					int xPosition = (int)((float)x + position.X / 16f);
					int yPosition = (int)((float)y + position.Y / 16f);
					if (xPosition >= 0 && xPosition < Main.maxTilesX && yPosition >= 0 && yPosition < Main.maxTilesY)
					{
						Tile tile = Main.tile[xPosition, yPosition];
						if (!(tile == null) && CheckDestruction.OkayToDestroyTile(tile, false) && !CheckDestruction.TileIsLiterallyAir(tile))
						{
							Destruction.ClearTileAndLiquid(xPosition, yPosition, true);
						}
					}
				}
			}
			Main.refreshMap = true;
		}
	}
}
