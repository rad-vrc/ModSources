using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x02000458 RID: 1112
	public struct SpriteFrame
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06002C85 RID: 11397 RVA: 0x005BB566 File Offset: 0x005B9766
		// (set) Token: 0x06002C86 RID: 11398 RVA: 0x005BB56E File Offset: 0x005B976E
		public byte CurrentColumn
		{
			get
			{
				return this._currentColumn;
			}
			set
			{
				this._currentColumn = value;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06002C87 RID: 11399 RVA: 0x005BB577 File Offset: 0x005B9777
		// (set) Token: 0x06002C88 RID: 11400 RVA: 0x005BB57F File Offset: 0x005B977F
		public byte CurrentRow
		{
			get
			{
				return this._currentRow;
			}
			set
			{
				this._currentRow = value;
			}
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x005BB588 File Offset: 0x005B9788
		public SpriteFrame(byte columns, byte rows)
		{
			this.PaddingX = 2;
			this.PaddingY = 2;
			this._currentColumn = 0;
			this._currentRow = 0;
			this.ColumnCount = columns;
			this.RowCount = rows;
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x005BB5B4 File Offset: 0x005B97B4
		public SpriteFrame(byte columns, byte rows, byte currentColumn, byte currentRow)
		{
			this.PaddingX = 2;
			this.PaddingY = 2;
			this._currentColumn = currentColumn;
			this._currentRow = currentRow;
			this.ColumnCount = columns;
			this.RowCount = rows;
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x005BB5E4 File Offset: 0x005B97E4
		public SpriteFrame With(byte columnToUse, byte rowToUse)
		{
			SpriteFrame result = this;
			result.CurrentColumn = columnToUse;
			result.CurrentRow = rowToUse;
			return result;
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x005BB60C File Offset: 0x005B980C
		public Rectangle GetSourceRectangle(Texture2D texture)
		{
			int num = texture.Width / (int)this.ColumnCount;
			int num2 = texture.Height / (int)this.RowCount;
			return new Rectangle((int)this.CurrentColumn * num, (int)this.CurrentRow * num2, num - ((this.ColumnCount == 1) ? 0 : this.PaddingX), num2 - ((this.RowCount == 1) ? 0 : this.PaddingY));
		}

		// Token: 0x040050D7 RID: 20695
		public int PaddingX;

		// Token: 0x040050D8 RID: 20696
		public int PaddingY;

		// Token: 0x040050D9 RID: 20697
		private byte _currentColumn;

		// Token: 0x040050DA RID: 20698
		private byte _currentRow;

		// Token: 0x040050DB RID: 20699
		public readonly byte ColumnCount;

		// Token: 0x040050DC RID: 20700
		public readonly byte RowCount;
	}
}
