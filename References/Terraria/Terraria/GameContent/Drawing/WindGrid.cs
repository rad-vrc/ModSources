using System;
using Microsoft.Xna.Framework;

namespace Terraria.GameContent.Drawing
{
	// Token: 0x020002B4 RID: 692
	public class WindGrid
	{
		// Token: 0x06002216 RID: 8726 RVA: 0x00541440 File Offset: 0x0053F640
		public void SetSize(int targetWidth, int targetHeight)
		{
			this._width = Math.Max(this._width, targetWidth);
			this._height = Math.Max(this._height, targetHeight);
			this.ResizeGrid();
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x0054146C File Offset: 0x0053F66C
		public void Update()
		{
			this._gameTime++;
			if (Main.SettingsEnabled_TilesSwayInWind)
			{
				this.ScanPlayers();
			}
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x0054148C File Offset: 0x0053F68C
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

		// Token: 0x06002219 RID: 8729 RVA: 0x005414EC File Offset: 0x0053F6EC
		private void ResizeGrid()
		{
			if (this._width <= this._grid.GetLength(0) && this._height <= this._grid.GetLength(1))
			{
				return;
			}
			this._grid = new WindGrid.WindCoord[this._width, this._height];
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x0054153C File Offset: 0x0053F73C
		private void SetWindTime(int tileX, int tileY, int directionX, int directionY)
		{
			int num = tileX % this._width;
			int num2 = tileY % this._height;
			this._grid[num, num2].Time = this._gameTime;
			this._grid[num, num2].DirectionX = directionX;
			this._grid[num, num2].DirectionY = directionY;
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x0054159C File Offset: 0x0053F79C
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

		// Token: 0x0600221C RID: 8732 RVA: 0x005415DC File Offset: 0x0053F7DC
		private void ScanPlayer(int i)
		{
			Player player = Main.player[i];
			if (!player.active || player.dead || (player.velocity.X == 0f && player.velocity.Y == 0f))
			{
				return;
			}
			if (!Utils.CenteredRectangle(Main.Camera.Center, Main.Camera.UnscaledSize).Intersects(player.Hitbox))
			{
				return;
			}
			if (player.velocity.HasNaNs())
			{
				return;
			}
			int directionX = Math.Sign(player.velocity.X);
			int directionY = Math.Sign(player.velocity.Y);
			foreach (Point point in Collision.GetTilesIn(player.TopLeft, player.BottomRight))
			{
				this.SetWindTime(point.X, point.Y, directionX, directionY);
			}
		}

		// Token: 0x040047AC RID: 18348
		private WindGrid.WindCoord[,] _grid = new WindGrid.WindCoord[1, 1];

		// Token: 0x040047AD RID: 18349
		private int _width = 1;

		// Token: 0x040047AE RID: 18350
		private int _height = 1;

		// Token: 0x040047AF RID: 18351
		private int _gameTime;

		// Token: 0x02000695 RID: 1685
		private struct WindCoord
		{
			// Token: 0x040061B3 RID: 25011
			public int Time;

			// Token: 0x040061B4 RID: 25012
			public int DirectionX;

			// Token: 0x040061B5 RID: 25013
			public int DirectionY;
		}
	}
}
