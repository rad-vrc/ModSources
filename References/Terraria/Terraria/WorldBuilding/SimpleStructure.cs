using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000073 RID: 115
	public class SimpleStructure : GenStructure
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0048D00C File Offset: 0x0048B20C
		public int Width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0048D014 File Offset: 0x0048B214
		public int Height
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0048D01C File Offset: 0x0048B21C
		public SimpleStructure(params string[] data)
		{
			this.ReadData(data);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0048D02B File Offset: 0x0048B22B
		public SimpleStructure(string data)
		{
			this.ReadData(data.Split(new char[]
			{
				'\n'
			}));
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0048D04C File Offset: 0x0048B24C
		private void ReadData(string[] lines)
		{
			this._height = lines.Length;
			this._width = lines[0].Length;
			this._data = new int[this._width, this._height];
			for (int i = 0; i < this._height; i++)
			{
				for (int j = 0; j < this._width; j++)
				{
					int num = (int)lines[i][j];
					if (num >= 48 && num <= 57)
					{
						this._data[j, i] = num - 48;
					}
					else
					{
						this._data[j, i] = -1;
					}
				}
			}
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x0048D0DE File Offset: 0x0048B2DE
		public SimpleStructure SetActions(params GenAction[] actions)
		{
			this._actions = actions;
			return this;
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0048D0E8 File Offset: 0x0048B2E8
		public SimpleStructure Mirror(bool horizontalMirror, bool verticalMirror)
		{
			this._xMirror = horizontalMirror;
			this._yMirror = verticalMirror;
			return this;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0048D0FC File Offset: 0x0048B2FC
		public override bool Place(Point origin, StructureMap structures)
		{
			if (!structures.CanPlace(new Rectangle(origin.X, origin.Y, this._width, this._height), 0))
			{
				return false;
			}
			for (int i = 0; i < this._width; i++)
			{
				for (int j = 0; j < this._height; j++)
				{
					int num = this._xMirror ? (-i) : i;
					int num2 = this._yMirror ? (-j) : j;
					if (this._data[i, j] != -1 && !this._actions[this._data[i, j]].Apply(origin, num + origin.X, num2 + origin.Y, new object[0]))
					{
						return false;
					}
				}
			}
			structures.AddProtectedStructure(new Rectangle(origin.X, origin.Y, this._width, this._height), 0);
			return true;
		}

		// Token: 0x04000FB0 RID: 4016
		private int[,] _data;

		// Token: 0x04000FB1 RID: 4017
		private int _width;

		// Token: 0x04000FB2 RID: 4018
		private int _height;

		// Token: 0x04000FB3 RID: 4019
		private GenAction[] _actions;

		// Token: 0x04000FB4 RID: 4020
		private bool _xMirror;

		// Token: 0x04000FB5 RID: 4021
		private bool _yMirror;
	}
}
