using System;

namespace Terraria.UI
{
	// Token: 0x0200008C RID: 140
	public struct StyleDimension
	{
		// Token: 0x06001226 RID: 4646 RVA: 0x004913F4 File Offset: 0x0048F5F4
		public StyleDimension(float pixels, float precent)
		{
			this.Pixels = pixels;
			this.Precent = precent;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x004913F4 File Offset: 0x0048F5F4
		public void Set(float pixels, float precent)
		{
			this.Pixels = pixels;
			this.Precent = precent;
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00491404 File Offset: 0x0048F604
		public float GetValue(float containerSize)
		{
			return this.Pixels + this.Precent * containerSize;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00491415 File Offset: 0x0048F615
		public static StyleDimension FromPixels(float pixels)
		{
			return new StyleDimension(pixels, 0f);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00491422 File Offset: 0x0048F622
		public static StyleDimension FromPercent(float percent)
		{
			return new StyleDimension(0f, percent);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0049142F File Offset: 0x0048F62F
		public static StyleDimension FromPixelsAndPercent(float pixels, float percent)
		{
			return new StyleDimension(pixels, percent);
		}

		// Token: 0x04001008 RID: 4104
		public static StyleDimension Fill = new StyleDimension(0f, 1f);

		// Token: 0x04001009 RID: 4105
		public static StyleDimension Empty = new StyleDimension(0f, 0f);

		// Token: 0x0400100A RID: 4106
		public float Pixels;

		// Token: 0x0400100B RID: 4107
		public float Precent;
	}
}
