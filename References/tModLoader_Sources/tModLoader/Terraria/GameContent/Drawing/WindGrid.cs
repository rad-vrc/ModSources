using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x0200063E RID: 1598
	public class WindGrid
	{
		// Token: 0x0600463E RID: 17982 RVA: 0x00630AF0 File Offset: 0x0062ECF0
		public void SetSize(int targetWidth, int targetHeight)
		{
			this._width = Math.Max(this._width, targetWidth);
			this._height = Math.Max(this._height, targetHeight);
			this.ResizeGrid();
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x00630B1C File Offset: 0x0062ED1C
		public void Update()
		{
			this._gameTime++;
			if (Main.SettingsEnabled_TilesSwayInWind)
			{
				this.ScanPlayers();
			}
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x00630B3C File Offset: 0x0062ED3C
		public void GetWindTime(int tileX, int tileY, int timeThreshold, out int windTimeLeft, out int directionX, out int directionY)
		{
			WindGrid.WindCoord windCoord = this._grid[tileX % this._width, tileY % this._height];
			directionX = windCoord.DirectionX;
			directionY = windCoord.DirectionY;
			if (windCoord.Time + timeThreshold < this._gameTime)
			{
				windTimeLeft = 0;
				return;
			}
			windTimeLeft = this._gameTime - windCoord.Time;
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x00630B9C File Offset: 0x0062ED9C
		private void ResizeGrid()
		{
			if (this._width > this._grid.GetLength(0) || this._height > this._grid.GetLength(1))
			{
				this._grid = new WindGrid.WindCoord[this._width, this._height];
			}
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x00630BE8 File Offset: 0x0062EDE8
		private void SetWindTime(int tileX, int tileY, int directionX, int directionY)
		{
			int num = tileX % this._width;
			int num2 = tileY % this._height;
			this._grid[num, num2].Time = this._gameTime;
			this._grid[num, num2].DirectionX = directionX;
			this._grid[num, num2].DirectionY = directionY;
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x00630C48 File Offset: 0x0062EE48
		private void ScanPlayers()
		{
			if (Main.netMode == 0)
			{
				this.ScanPlayer(Main.myPlayer);
				return;
			}
			if (Main.netMode == 1)
			{
				for (int i = 0; i < 255; i++)
				{
					this.ScanPlayer(i);
				}
			}
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x00630C88 File Offset: 0x0062EE88
		private void ScanPlayer(int i)
		{
			Player player = Main.player[i];
			if (!player.active || player.dead || (player.velocity.X == 0f && player.velocity.Y == 0f) || !Utils.CenteredRectangle(Main.Camera.Center, Main.Camera.UnscaledSize).Intersects(player.Hitbox) || player.velocity.HasNaNs())
			{
				return;
			}
			int directionX = Math.Sign(player.velocity.X);
			int directionY = Math.Sign(player.velocity.Y);
			foreach (Point item in Collision.GetTilesIn(player.TopLeft, player.BottomRight))
			{
				this.SetWindTime(item.X, item.Y, directionX, directionY);
			}
		}

		// Token: 0x04005B65 RID: 23397
		private WindGrid.WindCoord[,] _grid = new WindGrid.WindCoord[1, 1];

		// Token: 0x04005B66 RID: 23398
		private int _width = 1;

		// Token: 0x04005B67 RID: 23399
		private int _height = 1;

		// Token: 0x04005B68 RID: 23400
		private int _gameTime;

		// Token: 0x02000CDA RID: 3290
		private struct WindCoord
		{
			// Token: 0x04007A42 RID: 31298
			public int Time;

			// Token: 0x04007A43 RID: 31299
			public int DirectionX;

			// Token: 0x04007A44 RID: 31300
			public int DirectionY;
		}
	}
}
