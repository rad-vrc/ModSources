using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using QoLCompendium.Content.Projectiles.Explosives;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace QoLCompendium.Content.Tiles.AutoStructures
{
	// Token: 0x02000021 RID: 33
	public class AutoHouserTile : ModTile
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003F64 File Offset: 0x00002164
		public override void SetStaticDefaults()
		{
			Main.tileSolid[(int)base.Type] = true;
			Main.tileMergeDirt[(int)base.Type] = true;
			Main.tileBlockLight[(int)base.Type] = true;
			Main.tileLighted[(int)base.Type] = true;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003F9C File Offset: 0x0000219C
		public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
		{
			noItem = true;
			if (Main.netMode != 1)
			{
				int p = (int)Player.FindClosest(new Vector2((float)(i * 16 + 8), (float)(j * 16 + 8)), 0, 0);
				if (p != -1)
				{
					Projectile.NewProjectile(new EntitySource_TileBreak(i, j, null), (float)(i * 16 + 8), (float)((j + 2) * 16), 0f, 0f, ModContent.ProjectileType<AutoHouserProj>(), 0, 0f, p, 0f, 0f, 0f);
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004018 File Offset: 0x00002218
		public override void NearbyEffects(int i, int j, bool closer)
		{
			WorldGen.KillTile(i, j, false, false, false);
			if (Main.netMode != 0)
			{
				NetMessage.SendData(17, -1, -1, null, 0, (float)i, (float)j, 0f, 0, 0, 0);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000404D File Offset: 0x0000224D
		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			return false;
		}
	}
}
