using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent
{
	// Token: 0x020001E9 RID: 489
	public class SpelunkerProjectileHelper
	{
		// Token: 0x06001CB7 RID: 7351 RVA: 0x004FC9C0 File Offset: 0x004FABC0
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

		// Token: 0x06001CB8 RID: 7352 RVA: 0x004FCA1A File Offset: 0x004FAC1A
		public void AddSpotToCheck(Vector2 spot)
		{
			if (this._positionsChecked.Add(spot))
			{
				this.CheckSpot(spot);
			}
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x004FCA34 File Offset: 0x004FAC34
		private void CheckSpot(Vector2 Center)
		{
			int num = (int)Center.X / 16;
			int num2 = (int)Center.Y / 16;
			int num3 = Utils.Clamp<int>(num - 30, this._clampBox.Left, this._clampBox.Right);
			int num4 = Utils.Clamp<int>(num + 30, this._clampBox.Left, this._clampBox.Right);
			int num5 = Utils.Clamp<int>(num2 - 30, this._clampBox.Top, this._clampBox.Bottom);
			int num6 = Utils.Clamp<int>(num2 + 30, this._clampBox.Top, this._clampBox.Bottom);
			Point item = default(Point);
			Vector2 position = default(Vector2);
			for (int i = num3; i <= num4; i++)
			{
				for (int j = num5; j <= num6; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile != null && tile.active() && Main.IsTileSpelunkable(tile))
					{
						Vector2 vector = new Vector2((float)(num - i), (float)(num2 - j));
						if (vector.Length() <= 30f)
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
		}

		// Token: 0x040043BF RID: 17343
		private HashSet<Vector2> _positionsChecked = new HashSet<Vector2>();

		// Token: 0x040043C0 RID: 17344
		private HashSet<Point> _tilesChecked = new HashSet<Point>();

		// Token: 0x040043C1 RID: 17345
		private Rectangle _clampBox;

		// Token: 0x040043C2 RID: 17346
		private int _frameCounter;
	}
}
