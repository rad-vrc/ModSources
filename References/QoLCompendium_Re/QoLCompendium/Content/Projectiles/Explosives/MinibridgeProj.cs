using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Projectiles.Explosives
{
	// Token: 0x02000034 RID: 52
	public class MinibridgeProj : ModProjectile
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00007195 File Offset: 0x00005395
		public override string Texture
		{
			get
			{
				return "QoLCompendium/Content/Items/Tools/Explosives/Minibridge";
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000719C File Offset: 0x0000539C
		public override void SetDefaults()
		{
			base.Projectile.width = 20;
			base.Projectile.height = 20;
			base.Projectile.aiStyle = 16;
			base.Projectile.friendly = true;
			base.Projectile.penetrate = -1;
			base.Projectile.timeLeft = 1;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000473D File Offset: 0x0000293D
		public override bool? CanDamage()
		{
			return new bool?(false);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000071F4 File Offset: 0x000053F4
		public unsafe override void OnKill(int timeLeft)
		{
			Vector2 center = base.Projectile.Center;
			SoundEngine.PlaySound(SoundID.Item14, new Vector2?(center), null);
			if (Main.netMode == 1)
			{
				return;
			}
			bool centerX = base.Projectile.Center.X < Main.player[base.Projectile.owner].Center.X;
			int[] source = new int[]
			{
				80,
				5,
				32,
				352,
				69
			};
			Tile tileCenter = Main.tile[(int)(center.X / 16f), (int)(center.Y / 16f)];
			if (!(tileCenter == null))
			{
				if (source.Contains((int)(*tileCenter.TileType)))
				{
					Destruction.ClearEverything((int)(center.X / 16f), (int)(center.Y / 16f), true);
				}
				WorldGen.PlaceTile((int)(center.X / 16f), (int)(center.Y / 16f), 19, false, false, -1, 0);
				NetMessage.SendTileSquare(-1, (int)(center.X / 16f), (int)(center.Y / 16f), 1, TileChangeType.None);
			}
			int startLeft = centerX ? -100 : 0;
			int endLeft = (!centerX) ? 100 : 0;
			for (int i = startLeft; i < endLeft; i++)
			{
				int posX = (int)((float)i + center.X / 16f);
				int posY = (int)(center.Y / 16f);
				if (posX >= 0 && posX < Main.maxTilesX && posY >= 0 && posY < Main.maxTilesY)
				{
					Tile tile = Main.tile[posX, posY];
					if (!(tile == null))
					{
						if (source.Contains((int)(*tile.TileType)))
						{
							Destruction.ClearEverything(posX, posY, true);
						}
						WorldGen.PlaceTile(posX, posY, 19, false, false, -1, 0);
						NetMessage.SendTileSquare(-1, posX, posY, 1, TileChangeType.None);
					}
				}
			}
			int startRight = centerX ? 100 : 0;
			int endRight = (!centerX) ? -100 : 0;
			for (int j = startRight; j > endRight; j--)
			{
				int posX2 = (int)((float)j + center.X / 16f);
				int posY2 = (int)(center.Y / 16f);
				if (posX2 >= 0 && posX2 < Main.maxTilesX && posY2 >= 0 && posY2 < Main.maxTilesY)
				{
					Tile tile2 = Main.tile[posX2, posY2];
					if (!(tile2 == null))
					{
						if (source.Contains((int)(*tile2.TileType)))
						{
							Destruction.ClearEverything(posX2, posY2, true);
						}
						WorldGen.PlaceTile(posX2, posY2, 19, false, false, -1, 0);
						NetMessage.SendTileSquare(-1, posX2, posY2, 1, TileChangeType.None);
					}
				}
			}
		}
	}
}
