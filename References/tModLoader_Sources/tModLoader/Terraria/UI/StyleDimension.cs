using System;

namespace Terraria.UI
{
	/// <summary>
	/// Represents a dimension, either an absolute pixel size, a percentage of the available space, or a combination of both.
	/// <para /> For example <c>uiElement.Width.Set(200, 0f);</c> sets an absolute width of 200 pixels. <c>uiElement.Width.Set(0, 0.5f);</c> on the otherhand sets a width of 50% of the parent's avaiable <see cref="M:Terraria.UI.UIElement.GetInnerDimensions" />.
	/// <para /> Both values can be set for more complex logic. <c>uiElement.Width.Set(-10, 0.5f);</c> sets the width to 50% of the available space minus 10 pixels. This would leave room between 2 buttons filling a space. <c>uiElement.Height.Set(-100, 1f);</c> would fill the full height of the space but leave 100 pixels at the bottom empty.
	/// </summary>
	// Token: 0x020000B1 RID: 177
	public struct StyleDimension
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x004B0638 File Offset: 0x004AE838
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x004B0640 File Offset: 0x004AE840
		public float Percent
		{
			get
			{
				return this.Precent;
			}
			set
			{
				this.Precent = value;
			}
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x004B0649 File Offset: 0x004AE849
		public StyleDimension(float pixels, float precent)
		{
			this.Pixels = pixels;
			this.Precent = precent;
		}

		/// <summary>
		/// Sets the values for this StyleDimension.
		/// <para /> <b>StyleDimension documentation:</b><br /><inheritdoc cref="T:Terraria.UI.StyleDimension" />
		/// </summary>
		/// <param name="pixels"></param>
		/// <param name="precent"></param>
		// Token: 0x0600159B RID: 5531 RVA: 0x004B0659 File Offset: 0x004AE859
		public void Set(float pixels, float precent)
		{
			this.Pixels = pixels;
			this.Precent = precent;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x004B0669 File Offset: 0x004AE869
		public float GetValue(float containerSize)
		{
			return this.Pixels + this.Precent * containerSize;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x004B067A File Offset: 0x004AE87A
		public static StyleDimension FromPixels(float pixels)
		{
			return new StyleDimension(pixels, 0f);
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x004B0687 File Offset: 0x004AE887
		public static StyleDimension FromPercent(float percent)
		{
			return new StyleDimension(0f, percent);
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x004B0694 File Offset: 0x004AE894
		public static StyleDimension FromPixelsAndPercent(float pixels, float percent)
		{
			return new StyleDimension(pixels, percent);
		}

		// Token: 0x04001117 RID: 4375
		public static StyleDimension Fill = new StyleDimension(0f, 1f);

		// Token: 0x04001118 RID: 4376
		public static StyleDimension Empty = new StyleDimension(0f, 0f);

		// Token: 0x04001119 RID: 4377
		public float Pixels;

		// Token: 0x0400111A RID: 4378
		public float Precent;
	}
}
