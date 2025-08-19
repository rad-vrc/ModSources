using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Explosives
{
	// Token: 0x02000033 RID: 51
	public class LakemakerProj : ModProjectile
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00006FCE File Offset: 0x000051CE
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Content/Items/Tools/Explosives/Lakemaker";
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006FD8 File Offset: 0x000051D8
		public override void SetDefaults()
		{
			base.Projectile.width = 19;
			base.Projectile.height = 31;
			base.Projectile.aiStyle = 16;
			base.Projectile.friendly = true;
			base.Projectile.penetrate = -1;
			base.Projectile.timeLeft = 170;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool? CanDamage()
		{
			return new bool?(false);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00007034 File Offset: 0x00005234
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			fallThrough = false;
			return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007044 File Offset: 0x00005244
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			base.Projectile.Kill();
			return true;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007054 File Offset: 0x00005254
		public override void OnKill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item15, new Vector2?(base.Projectile.Center), null);
			SoundEngine.PlaySound(SoundID.Item14, new Vector2?(base.Projectile.Center), null);
			if (Main.netMode == 1)
			{
				return;
			}
			Vector2 position = base.Projectile.Center;
			int width = 50;
			int height = 50;
			for (int x = -width / 2; x <= width / 2; x++)
			{
				for (int y = 0; y <= height; y++)
				{
					int xPosition = (int)((float)x + position.X / 16f);
					int yPosition = (int)((float)y + position.Y / 16f);
					if (xPosition >= 0 && xPosition < Main.maxTilesX && yPosition >= 0 && yPosition < Main.maxTilesY)
					{
						Tile tile = Main.tile[xPosition, yPosition];
						if (!(tile == null) && CheckDestruction.OkayToDestroyTileAt(xPosition, yPosition) && !CheckDestruction.TileIsLiterallyAir(tile))
						{
							Destruction.ClearTileAndLiquid(xPosition, yPosition, true);
							if (y == height || Math.Abs(x) == width / 2)
							{
								WorldGen.PlaceTile(xPosition, yPosition, 38, false, false, -1, 0);
							}
							else
							{
								WorldGen.PlaceLiquid(xPosition, yPosition, 0, byte.MaxValue);
							}
						}
					}
				}
			}
			Main.refreshMap = true;
		}
	}
}
