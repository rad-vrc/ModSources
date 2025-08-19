using System;
using Microsoft.Xna.Framework;

namespace Terraria.WorldBuilding
{
	// Token: 0x02000082 RID: 130
	public class SimpleStructure : GenStructure
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0049FE50 File Offset: 0x0049E050
		public int Width
		{
			get
			{
				return this._width;
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06001406 RID: 5126 RVA: 0x0049FE58 File Offset: 0x0049E058
		public int Height
		{
			get
			{
				return this._height;
			}
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x0049FE60 File Offset: 0x0049E060
		public SimpleStructure(params string[] data)
		{
			this.ReadData(data);
		}

		// Token: 0x06001408 RID: 5128 RVA: 0x0049FE6F File Offset: 0x0049E06F
		public SimpleStructure(string data)
		{
			this.ReadData(data.Split('\n', StringSplitOptions.None));
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x0049FE88 File Offset: 0x0049E088
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

		// Token: 0x0600140A RID: 5130 RVA: 0x0049FF1A File Offset: 0x0049E11A
		public SimpleStructure SetActions(params GenAction[] actions)
		{
			this._actions = actions;
			return this;
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x0049FF24 File Offset: 0x0049E124
		public SimpleStructure Mirror(bool horizontalMirror, bool verticalMirror)
		{
			this._xMirror = horizontalMirror;
			this._yMirror = verticalMirror;
			return this;
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x0049FF38 File Offset: 0x0049E138
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
					if (this._data[i, j] != -1 && !this._actions[this._data[i, j]].Apply(origin, num + origin.X, num2 + origin.Y, Array.Empty<object>()))
					{
						return false;
					}
				}
			}
			structures.AddProtectedStructure(new Rectangle(origin.X, origin.Y, this._width, this._height), 0);
			return true;
		}

		// Token: 0x0400108D RID: 4237
		private int[,] _data;

		// Token: 0x0400108E RID: 4238
		private int _width;

		// Token: 0x0400108F RID: 4239
		private int _height;

		// Token: 0x04001090 RID: 4240
		private GenAction[] _actions;

		// Token: 0x04001091 RID: 4241
		private bool _xMirror;

		// Token: 0x04001092 RID: 4242
		private bool _yMirror;
	}
}
