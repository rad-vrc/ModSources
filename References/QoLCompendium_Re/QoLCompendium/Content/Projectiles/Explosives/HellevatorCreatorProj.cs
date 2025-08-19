using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Explosives
{
	// Token: 0x02000032 RID: 50
	public class HellevatorCreatorProj : ModProjectile
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00006E3D File Offset: 0x0000503D
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Content/Items/Tools/Explosives/HellevatorCreator";
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006E44 File Offset: 0x00005044
		public override void SetDefaults()
		{
			base.Projectile.width = 13;
			base.Projectile.height = 31;
			base.Projectile.aiStyle = 16;
			base.Projectile.friendly = true;
			base.Projectile.penetrate = -1;
			base.Projectile.timeLeft = 1;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool? CanDamage()
		{
			return new bool?(false);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006E9C File Offset: 0x0000509C
		public override void OnKill(int timeLeft)
		{
			Vector2 position = base.Projectile.Center;
			SoundEngine.PlaySound(SoundID.Item14, new Vector2?(position), null);
			if (Main.netMode == 1)
			{
				return;
			}
			for (int x = -3; x <= 3; x++)
			{
				for (int y = (int)(1f + position.Y / 16f); y <= Main.maxTilesY - 40; y++)
				{
					int xPosition = (int)((float)x + position.X / 16f);
					if (xPosition >= 0 && xPosition < Main.maxTilesX && y >= 0 && y < Main.maxTilesY)
					{
						Tile tile = Main.tile[xPosition, y];
						if (!(tile == null) && CheckDestruction.OkayToDestroyTile(tile, false))
						{
							Destruction.ClearEverything(xPosition, y, false);
							if (x == -3 || x == 3)
							{
								WorldGen.PlaceTile(xPosition, y, 38, false, false, -1, 0);
							}
							else if (x == -2 || x == 2 || x == -1 || x == 1)
							{
								WorldGen.PlaceWall(xPosition, y, 5, false);
							}
							else if (x == 0)
							{
								WorldGen.PlaceTile(xPosition, y, 213, false, false, -1, 0);
								WorldGen.PlaceWall(xPosition, y, 155, false);
							}
							NetMessage.SendTileSquare(-1, xPosition, y, 1, 0, TileChangeType.None);
						}
					}
				}
			}
		}
	}
}
