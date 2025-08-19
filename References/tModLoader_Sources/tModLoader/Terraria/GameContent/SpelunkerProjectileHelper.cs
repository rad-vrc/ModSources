using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x020004B7 RID: 1207
	public class SpelunkerProjectileHelper
	{
		// Token: 0x06003A07 RID: 14855 RVA: 0x005A4634 File Offset: 0x005A2834
		public void OnPreUpdateAllProjectiles()
		{
			this._clampBox = new Rectangle(2, 2, Main.maxTilesX - 2, Main.maxTilesY - 2);
			int num = this._frameCounter + 1;
			this._frameCounter = num;
			if (num >= 10)
			{
				this._frameCounter = 0;
				this._tilesChecked.Clear();
				this._positionsChecked.Clear();
			}
		}

		// Token: 0x06003A08 RID: 14856 RVA: 0x005A468E File Offset: 0x005A288E
		public void AddSpotToCheck(Vector2 spot)
		{
			if (this._positionsChecked.Add(spot))
			{
				this.CheckSpot(spot);
			}
		}

		// Token: 0x06003A09 RID: 14857 RVA: 0x005A46A8 File Offset: 0x005A28A8
		private void CheckSpot(Vector2 Center)
		{
			int num = (int)Center.X / 16;
			int num2 = (int)Center.Y / 16;
			int num6 = Utils.Clamp<int>(num - 30, this._clampBox.Left, this._clampBox.Right);
			int num3 = Utils.Clamp<int>(num + 30, this._clampBox.Left, this._clampBox.Right);
			int num4 = Utils.Clamp<int>(num2 - 30, this._clampBox.Top, this._clampBox.Bottom);
			int num5 = Utils.Clamp<int>(num2 + 30, this._clampBox.Top, this._clampBox.Bottom);
			Point item = default(Point);
			Vector2 position = default(Vector2);
			for (int i = num6; i <= num3; i++)
			{
				for (int j = num4; j <= num5; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.active() && Main.IsTileSpelunkable(i, j, tile) && new Vector2((float)(num - i), (float)(num2 - j)).Length() <= 30f)
					{
						item.X = i;
						item.Y = j;
						if (this._tilesChecked.Add(item) && Main.rand.Next(4) == 0)
						{
							position.X = (float)(i * 16);
							position.Y = (float)(j * 16);
							Dust dust = Dust.NewDustDirect(position, 16, 16, 204, 0f, 0f, 150, default(Color), 0.3f);
							dust.fadeIn = 0.75f;
							dust.velocity *= 0.1f;
							dust.noLight = true;
						}
					}
				}
			}
		}

		// Token: 0x04005297 RID: 21143
		private HashSet<Vector2> _positionsChecked = new HashSet<Vector2>();

		// Token: 0x04005298 RID: 21144
		private HashSet<Point> _tilesChecked = new HashSet<Point>();

		// Token: 0x04005299 RID: 21145
		private Rectangle _clampBox;

		// Token: 0x0400529A RID: 21146
		private int _frameCounter;
	}
}
