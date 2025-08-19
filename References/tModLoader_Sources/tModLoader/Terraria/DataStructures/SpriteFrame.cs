using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.DataStructures
{
	// Token: 0x02000733 RID: 1843
	public struct SpriteFrame
	{
		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06004ADE RID: 19166 RVA: 0x00668BB8 File Offset: 0x00666DB8
		// (set) Token: 0x06004ADF RID: 19167 RVA: 0x00668BC0 File Offset: 0x00666DC0
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

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06004AE0 RID: 19168 RVA: 0x00668BC9 File Offset: 0x00666DC9
		// (set) Token: 0x06004AE1 RID: 19169 RVA: 0x00668BD1 File Offset: 0x00666DD1
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

		// Token: 0x06004AE2 RID: 19170 RVA: 0x00668BDA File Offset: 0x00666DDA
		public SpriteFrame(byte columns, byte rows)
		{
			this.PaddingX = 2;
			this.PaddingY = 2;
			this._currentColumn = 0;
			this._currentRow = 0;
			this.ColumnCount = columns;
			this.RowCount = rows;
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x00668C06 File Offset: 0x00666E06
		public SpriteFrame(byte columns, byte rows, byte currentColumn, byte currentRow)
		{
			this.PaddingX = 2;
			this.PaddingY = 2;
			this._currentColumn = currentColumn;
			this._currentRow = currentRow;
			this.ColumnCount = columns;
			this.RowCount = rows;
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x00668C34 File Offset: 0x00666E34
		public SpriteFrame With(byte columnToUse, byte rowToUse)
		{
			SpriteFrame result = this;
			result.CurrentColumn = columnToUse;
			result.CurrentRow = rowToUse;
			return result;
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x00668C5C File Offset: 0x00666E5C
		public Rectangle GetSourceRectangle(Texture2D texture)
		{
			int num = texture.Width / (int)this.ColumnCount;
			int num2 = texture.Height / (int)this.RowCount;
			return new Rectangle((int)this.CurrentColumn * num, (int)this.CurrentRow * num2, num - ((this.ColumnCount != 1) ? this.PaddingX : 0), num2 - ((this.RowCount != 1) ? this.PaddingY : 0));
		}

		// Token: 0x04006010 RID: 24592
		public int PaddingX;

		// Token: 0x04006011 RID: 24593
		public int PaddingY;

		// Token: 0x04006012 RID: 24594
		private byte _currentColumn;

		// Token: 0x04006013 RID: 24595
		private byte _currentRow;

		// Token: 0x04006014 RID: 24596
		public readonly byte ColumnCount;

		// Token: 0x04006015 RID: 24597
		public readonly byte RowCount;
	}
}
