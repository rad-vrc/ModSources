using System;
using Microsoft.Xna.Framework;
using ReLogic.Threading;
using Terraria.Utilities;

namespace Terraria.Graphics.Light
{
	// Token: 0x0200011C RID: 284
	public class LightMap
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x004D1733 File Offset: 0x004CF933
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x004D173B File Offset: 0x004CF93B
		public int NonVisiblePadding { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x004D1744 File Offset: 0x004CF944
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x004D174C File Offset: 0x004CF94C
		public int Width { get; private set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x004D1755 File Offset: 0x004CF955
		// (set) Token: 0x0600171F RID: 5919 RVA: 0x004D175D File Offset: 0x004CF95D
		public int Height { get; private set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x004D1766 File Offset: 0x004CF966
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x004D176E File Offset: 0x004CF96E
		public float LightDecayThroughAir { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x004D1777 File Offset: 0x004CF977
		// (set) Token: 0x06001723 RID: 5923 RVA: 0x004D177F File Offset: 0x004CF97F
		public float LightDecayThroughSolid { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x004D1788 File Offset: 0x004CF988
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x004D1790 File Offset: 0x004CF990
		public Vector3 LightDecayThroughWater { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x004D1799 File Offset: 0x004CF999
		// (set) Token: 0x06001727 RID: 5927 RVA: 0x004D17A1 File Offset: 0x004CF9A1
		public Vector3 LightDecayThroughHoney { get; set; }

		// Token: 0x1700020D RID: 525
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

		// Token: 0x0600172A RID: 5930 RVA: 0x004D17D8 File Offset: 0x004CF9D8
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

		// Token: 0x0600172B RID: 5931 RVA: 0x004D188A File Offset: 0x004CFA8A
		public void GetLight(int x, int y, out Vector3 color)
		{
			color = this._colors[this.IndexOf(x, y)];
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x004D18A5 File Offset: 0x004CFAA5
		public LightMaskMode GetMask(int x, int y)
		{
			return this._mask[this.IndexOf(x, y)];
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x004D18B8 File Offset: 0x004CFAB8
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

		// Token: 0x0600172E RID: 5934 RVA: 0x004D1923 File Offset: 0x004CFB23
		public void SetMaskAt(int x, int y, LightMaskMode mode)
		{
			this._mask[this.IndexOf(x, y)] = mode;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x004D1935 File Offset: 0x004CFB35
		public void Blur()
		{
			this.BlurPass();
			this.BlurPass();
			this._random.NextSeed();
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x004D194E File Offset: 0x004CFB4E
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

		// Token: 0x06001731 RID: 5937 RVA: 0x004D1984 File Offset: 0x004CFB84
		private void BlurLine(int startIndex, int endIndex, int stride)
		{
			Vector3 zero = Vector3.Zero;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			for (int num = startIndex; num != endIndex + stride; num += stride)
			{
				if (zero.X < this._colors[num].X)
				{
					zero.X = this._colors[num].X;
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
						this._colors[num].X = zero.X;
					}
				}
				if (zero.Y < this._colors[num].Y)
				{
					zero.Y = this._colors[num].Y;
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
						this._colors[num].Y = zero.Y;
					}
				}
				if (zero.Z < this._colors[num].Z)
				{
					zero.Z = this._colors[num].Z;
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
						this._colors[num].Z = zero.Z;
					}
				}
				if (!flag || !flag3 || !flag2)
				{
					switch (this._mask[num])
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
						float num2 = (float)this._random.WithModifier((ulong)((long)num)).Next(98, 100) / 100f;
						if (!flag)
						{
							zero.X *= this.LightDecayThroughWater.X * num2;
						}
						if (!flag2)
						{
							zero.Y *= this.LightDecayThroughWater.Y * num2;
						}
						if (!flag3)
						{
							zero.Z *= this.LightDecayThroughWater.Z * num2;
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

		// Token: 0x06001732 RID: 5938 RVA: 0x004D1C5C File Offset: 0x004CFE5C
		private int IndexOf(int x, int y)
		{
			return x * this.Height + y;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x004D1C68 File Offset: 0x004CFE68
		public void SetSize(int width, int height)
		{
			if (width * height > this._colors.Length)
			{
				this._colors = new Vector3[width * height];
				this._mask = new LightMaskMode[width * height];
			}
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x040013E7 RID: 5095
		private Vector3[] _colors;

		// Token: 0x040013E8 RID: 5096
		private LightMaskMode[] _mask;

		// Token: 0x040013ED RID: 5101
		private FastRandom _random = FastRandom.CreateWithRandomSeed();

		// Token: 0x040013EE RID: 5102
		private const int DEFAULT_WIDTH = 203;

		// Token: 0x040013EF RID: 5103
		private const int DEFAULT_HEIGHT = 203;
	}
}
