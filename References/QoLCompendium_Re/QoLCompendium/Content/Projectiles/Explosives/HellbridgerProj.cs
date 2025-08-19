using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Explosives
{
	// Token: 0x02000030 RID: 48
	public class HellbridgerProj : ModProjectile
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000698F File Offset: 0x00004B8F
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Content/Items/Tools/Explosives/Hellbridger";
			}
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006998 File Offset: 0x00004B98
		public override void SetDefaults()
		{
			base.Projectile.width = 37;
			base.Projectile.height = 19;
			base.Projectile.aiStyle = 16;
			base.Projectile.friendly = true;
			base.Projectile.penetrate = -1;
			base.Projectile.timeLeft = 1;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool? CanDamage()
		{
			return new bool?(false);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000069F0 File Offset: 0x00004BF0
		public override void OnKill(int timeLeft)
		{
			Vector2 position = base.Projectile.Center;
			SoundEngine.PlaySound(SoundID.Item14, new Vector2?(position), null);
			if (Main.netMode == 1)
			{
				return;
			}
			bool stopRight = false;
			bool stopLeft = false;
			int x = Main.maxTilesX / 2;
			while (x < Main.maxTilesX && !stopRight)
			{
				for (int y = -40; y <= 0; y++)
				{
					int xPosition = x;
					int yPosition = (int)((float)y + position.Y / 16f);
					if (xPosition >= 0 && xPosition < Main.maxTilesX && yPosition >= 0 && yPosition < Main.maxTilesY)
					{
						Tile tile = Main.tile[xPosition, yPosition];
						if (!(tile == null))
						{
							if (!CheckDestruction.OkayToDestroyTile(tile, true))
							{
								stopRight = true;
							}
							if (y == -20 || y == 0)
							{
								Destruction.ClearEverything(xPosition, yPosition, false);
								WorldGen.PlaceTile(xPosition, yPosition, 19, false, false, -1, 13);
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
				x++;
			}
			int x2 = Main.maxTilesX / 2;
			while (x2 > 0 && !stopLeft)
			{
				for (int y2 = -40; y2 <= 0; y2++)
				{
					int xPosition2 = x2;
					int yPosition2 = (int)((float)y2 + position.Y / 16f);
					if (xPosition2 >= 0 && xPosition2 < Main.maxTilesX && yPosition2 >= 0 && yPosition2 < Main.maxTilesY)
					{
						Tile tile2 = Main.tile[xPosition2, yPosition2];
						if (!(tile2 == null))
						{
							if (!CheckDestruction.OkayToDestroyTile(tile2, true))
							{
								stopLeft = true;
							}
							if (y2 == -20 || y2 == 0)
							{
								Destruction.ClearEverything(xPosition2, yPosition2, false);
								WorldGen.PlaceTile(xPosition2, yPosition2, 19, false, false, -1, 13);
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
				x2--;
			}
		}
	}
}
