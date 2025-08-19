using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x020006D7 RID: 1751
	public class ColorSlidersSet
	{
		// Token: 0x06004917 RID: 18711 RVA: 0x0064C9B4 File Offset: 0x0064ABB4
		public void SetHSL(Color color)
		{
			Vector3 vector = Main.rgbToHsl(color);
			this.Hue = vector.X;
			this.Saturation = vector.Y;
			this.Luminance = vector.Z;
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x0064C9EC File Offset: 0x0064ABEC
		public void SetHSL(Vector3 vector)
		{
			this.Hue = vector.X;
			this.Saturation = vector.Y;
			this.Luminance = vector.Z;
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x0064CA14 File Offset: 0x0064AC14
		public Color GetColor()
		{
			Color result = Main.hslToRgb(this.Hue, this.Saturation, this.Luminance, byte.MaxValue);
			result.A = (byte)(this.Alpha * 255f);
			return result;
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x0064CA53 File Offset: 0x0064AC53
		public Vector3 GetHSLVector()
		{
			return new Vector3(this.Hue, this.Saturation, this.Luminance);
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x0064CA6C File Offset: 0x0064AC6C
		public void ApplyToMainLegacyBars()
		{
			Main.hBar = this.Hue;
			Main.sBar = this.Saturation;
			Main.lBar = this.Luminance;
			Main.aBar = this.Alpha;
		}

		// Token: 0x04005E79 RID: 24185
		public float Hue;

		// Token: 0x04005E7A RID: 24186
		public float Saturation;

		// Token: 0x04005E7B RID: 24187
		public float Luminance;

		// Token: 0x04005E7C RID: 24188
		public float Alpha = 1f;
	}
}
