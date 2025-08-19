using System;
using Microsoft.Xna.Framework;
using ReLogic.Threading;
using Terraria.Utilities;

namespace Terraria.Graphics.Light
{
	// Token: 0x02000462 RID: 1122
	public class LightMap
	{
		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060036E7 RID: 14055 RVA: 0x00581D02 File Offset: 0x0057FF02
		// (set) Token: 0x060036E8 RID: 14056 RVA: 0x00581D0A File Offset: 0x0057FF0A
		public int NonVisiblePadding { get; set; }

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060036E9 RID: 14057 RVA: 0x00581D13 File Offset: 0x0057FF13
		// (set) Token: 0x060036EA RID: 14058 RVA: 0x00581D1B File Offset: 0x0057FF1B
		public int Width { get; private set; }

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060036EB RID: 14059 RVA: 0x00581D24 File Offset: 0x0057FF24
		// (set) Token: 0x060036EC RID: 14060 RVA: 0x00581D2C File Offset: 0x0057FF2C
		public int Height { get; private set; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060036ED RID: 14061 RVA: 0x00581D35 File Offset: 0x0057FF35
		// (set) Token: 0x060036EE RID: 14062 RVA: 0x00581D3D File Offset: 0x0057FF3D
		public float LightDecayThroughAir { get; set; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060036EF RID: 14063 RVA: 0x00581D46 File Offset: 0x0057FF46
		// (set) Token: 0x060036F0 RID: 14064 RVA: 0x00581D4E File Offset: 0x0057FF4E
		public float LightDecayThroughSolid { get; set; }

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x00581D57 File Offset: 0x0057FF57
		// (set) Token: 0x060036F2 RID: 14066 RVA: 0x00581D5F File Offset: 0x0057FF5F
		public Vector3 LightDecayThroughWater { get; set; }

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x00581D68 File Offset: 0x0057FF68
		// (set) Token: 0x060036F4 RID: 14068 RVA: 0x00581D70 File Offset: 0x0057FF70
		public Vector3 LightDecayThroughHoney { get; set; }

		// Token: 0x17000687 RID: 1671
		public Vector3 this[int x, int y]
		{
			get
			{
				return this._colors[this.IndexOf(x, y)];
			}
			set
			{
				this._colors[this.IndexOf(x, y)] = value;
			}
		}

		// Token: 0x060036F7 RID: 14071 RVA: 0x00581DA4 File Offset: 0x0057FFA4
		public LightMap()
		{
			this.LightDecayThroughAir = 0.91f;
			this.LightDecayThroughSolid = 0.56f;
			this.LightDecayThroughWater = new Vector3(0.88f, 0.96f, 1.015f) * 0.91f;
			this.LightDecayThroughHoney = new Vector3(0.75f, 0.7f, 0.6f) * 0.91f;
			this.Width = 203;
			this.Height = 203;
			this._colors = new Vector3[41209];
			this._mask = new LightMaskMode[41209];
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x00581E56 File Offset: 0x00580056
		public void GetLight(int x, int y, out Vector3 color)
		{
			color = this._colors[this.IndexOf(x, y)];
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x00581E71 File Offset: 0x00580071
		public LightMaskMode GetMask(int x, int y)
		{
			return this._mask[this.IndexOf(x, y)];
		}

		// Token: 0x060036FA RID: 14074 RVA: 0x00581E84 File Offset: 0x00580084
		public void Clear()
		{
			for (int i = 0; i < this._colors.Length; i++)
			{
				this._colors[i].X = 0f;
				this._colors[i].Y = 0f;
				this._colors[i].Z = 0f;
				this._mask[i] = LightMaskMode.None;
			}
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x00581EEF File Offset: 0x005800EF
		public void SetMaskAt(int x, int y, LightMaskMode mode)
		{
			this._mask[this.IndexOf(x, y)] = mode;
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x00581F01 File Offset: 0x00580101
		public void Blur()
		{
			this.BlurPass();
			this.BlurPass();
			this._random.NextSeed();
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x00581F1A File Offset: 0x0058011A
		private void BlurPass()
		{
			FastParallel.For(0, this.Width, delegate(int start, int end, object context)
			{
				for (int i = start; i < end; i++)
				{
					this.BlurLine(this.IndexOf(i, 0), this.IndexOf(i, this.Height - 1 - this.NonVisiblePadding), 1);
					this.BlurLine(this.IndexOf(i, this.Height - 1), this.IndexOf(i, this.NonVisiblePadding), -1);
				}
			}, null);
			FastParallel.For(0, this.Height, delegate(int start, int end, object context)
			{
				for (int i = start; i < end; i++)
				{
					this.BlurLine(this.IndexOf(0, i), this.IndexOf(this.Width - 1 - this.NonVisiblePadding, i), this.Height);
					this.BlurLine(this.IndexOf(this.Width - 1, i), this.IndexOf(this.NonVisiblePadding, i), -this.Height);
				}
			}, null);
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x00581F50 File Offset: 0x00580150
		private void BlurLine(int startIndex, int endIndex, int stride)
		{
			Vector3 zero = Vector3.Zero;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int i = startIndex; i != endIndex + stride; i += stride)
			{
				if (zero.X < this._colors[i].X)
				{
					zero.X = this._colors[i].X;
					flag = false;
				}
				else if (!flag)
				{
					if (zero.X < 0.0185f)
					{
						flag = true;
					}
					else
					{
						this._colors[i].X = zero.X;
					}
				}
				if (zero.Y < this._colors[i].Y)
				{
					zero.Y = this._colors[i].Y;
					flag2 = false;
				}
				else if (!flag2)
				{
					if (zero.Y < 0.0185f)
					{
						flag2 = true;
					}
					else
					{
						this._colors[i].Y = zero.Y;
					}
				}
				if (zero.Z < this._colors[i].Z)
				{
					zero.Z = this._colors[i].Z;
					flag3 = false;
				}
				else if (!flag3)
				{
					if (zero.Z < 0.0185f)
					{
						flag3 = true;
					}
					else
					{
						this._colors[i].Z = zero.Z;
					}
				}
				if (!flag || !flag3 || !flag2)
				{
					switch (this._mask[i])
					{
					case LightMaskMode.None:
						if (!flag)
						{
							zero.X *= this.LightDecayThroughAir;
						}
						if (!flag2)
						{
							zero.Y *= this.LightDecayThroughAir;
						}
						if (!flag3)
						{
							zero.Z *= this.LightDecayThroughAir;
						}
						break;
					case LightMaskMode.Solid:
						if (!flag)
						{
							zero.X *= this.LightDecayThroughSolid;
						}
						if (!flag2)
						{
							zero.Y *= this.LightDecayThroughSolid;
						}
						if (!flag3)
						{
							zero.Z *= this.LightDecayThroughSolid;
						}
						break;
					case LightMaskMode.Water:
					{
						float num = (float)this._random.WithModifier((ulong)((long)i)).Next(98, 100) / 100f;
						if (!flag)
						{
							zero.X *= this.LightDecayThroughWater.X * num;
						}
						if (!flag2)
						{
							zero.Y *= this.LightDecayThroughWater.Y * num;
						}
						if (!flag3)
						{
							zero.Z *= this.LightDecayThroughWater.Z * num;
						}
						break;
					}
					case LightMaskMode.Honey:
						if (!flag)
						{
							zero.X *= this.LightDecayThroughHoney.X;
						}
						if (!flag2)
						{
							zero.Y *= this.LightDecayThroughHoney.Y;
						}
						if (!flag3)
						{
							zero.Z *= this.LightDecayThroughHoney.Z;
						}
						break;
					}
				}
			}
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x00582228 File Offset: 0x00580428
		private int IndexOf(int x, int y)
		{
			return x * this.Height + y;
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x00582234 File Offset: 0x00580434
		public void SetSize(int width, int height)
		{
			int neededSize = (width + 1) * (height + 1);
			if (neededSize > this._colors.Length)
			{
				this._colors = new Vector3[neededSize];
				this._mask = new LightMaskMode[neededSize];
			}
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x040050C5 RID: 20677
		private Vector3[] _colors;

		// Token: 0x040050C6 RID: 20678
		private LightMaskMode[] _mask;

		// Token: 0x040050C7 RID: 20679
		private FastRandom _random = FastRandom.CreateWithRandomSeed();

		// Token: 0x040050C8 RID: 20680
		private const int DEFAULT_WIDTH = 203;

		// Token: 0x040050C9 RID: 20681
		private const int DEFAULT_HEIGHT = 203;
	}
}
