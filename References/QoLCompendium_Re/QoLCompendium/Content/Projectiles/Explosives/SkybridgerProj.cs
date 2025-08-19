using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Explosives
{
	// Token: 0x02000035 RID: 53
	public class SkybridgerProj : ModProjectile
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00007482 File Offset: 0x00005682
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Content/Items/Tools/Explosives/Skybridger";
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000748C File Offset: 0x0000568C
		public override void SetDefaults()
		{
			base.Projectile.width = 37;
			base.Projectile.height = 19;
			base.Projectile.aiStyle = 16;
			base.Projectile.friendly = true;
			base.Projectile.penetrate = -1;
			base.Projectile.timeLeft = 1;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool? CanDamage()
		{
			return new bool?(false);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000074E4 File Offset: 0x000056E4
		public override void OnKill(int timeLeft)
		{
			Vector2 position = base.Projectile.Center;
			SoundEngine.PlaySound(SoundID.Item14, new Vector2?(position), null);
			if (Main.netMode == 1)
			{
				return;
			}
			for (int x = Main.maxTilesX / 2; x < Main.maxTilesX; x++)
			{
				for (int y = -90; y <= 0; y++)
				{
					int xPosition = x;
					int yPosition = (int)((float)y + position.Y / 16f);
					if (xPosition >= 0 && xPosition < Main.maxTilesX && yPosition >= 0 && yPosition < Main.maxTilesY)
					{
						Tile tile = Main.tile[xPosition, yPosition];
						if (!(tile == null))
						{
							if (y == -30 || y == 0)
							{
								Destruction.ClearEverything(xPosition, yPosition, false);
								WorldGen.PlaceTile(xPosition, yPosition, 19, false, false, -1, 0);
								if (Main.netMode == 2)
								{
									NetMessage.SendTileSquare(-1, xPosition, yPosition, 1, TileChangeType.None);
								}
							}
							else if (!CheckDestruction.TileIsLiterallyAir(tile))
							{
								Destruction.ClearEverything(xPosition, yPosition, true);
							}
						}
					}
				}
			}
			for (int x2 = Main.maxTilesX / 2; x2 > 0; x2--)
			{
				for (int y2 = -90; y2 <= 0; y2++)
				{
					int xPosition2 = x2;
					int yPosition2 = (int)((float)y2 + position.Y / 16f);
					if (xPosition2 >= 0 && xPosition2 < Main.maxTilesX && yPosition2 >= 0 && yPosition2 < Main.maxTilesY)
					{
						Tile tile2 = Main.tile[xPosition2, yPosition2];
						if (!(tile2 == null))
						{
							if (y2 == -30 || y2 == 0)
							{
								Destruction.ClearEverything(xPosition2, yPosition2, false);
								WorldGen.PlaceTile(xPosition2, yPosition2, 19, false, false, -1, 0);
								if (Main.netMode == 2)
								{
									NetMessage.SendTileSquare(-1, xPosition2, yPosition2, 1, TileChangeType.None);
								}
							}
							else if (!CheckDestruction.TileIsLiterallyAir(tile2))
							{
								Destruction.ClearEverything(xPosition2, yPosition2, true);
							}
						}
					}
				}
			}
		}
	}
}
