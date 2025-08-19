using System;
using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
	// Token: 0x02000445 RID: 1093
	public class ColorSlidersSet
	{
		// Token: 0x06002BD2 RID: 11218 RVA: 0x0059F6AC File Offset: 0x0059D8AC
		public void SetHSL(Color color)
		{
			Vector3 vector = Main.rgbToHsl(color);
			this.Hue = vector.X;
			this.Saturation = vector.Y;
			this.Luminance = vector.Z;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x0059F6E4 File Offset: 0x0059D8E4
		public void SetHSL(Vector3 vector)
		{
			this.Hue = vector.X;
			this.Saturation = vector.Y;
			this.Luminance = vector.Z;
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x0059F70C File Offset: 0x0059D90C
		public Color GetColor()
		{
			Color result = Main.hslToRgb(this.Hue, this.Saturation, this.Luminance, byte.MaxValue);
			result.A = (byte)(this.Alpha * 255f);
			return result;
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x0059F74B File Offset: 0x0059D94B
		public Vector3 GetHSLVector()
		{
			return new Vector3(this.Hue, this.Saturation, this.Luminance);
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x0059F764 File Offset: 0x0059D964
		public void ApplyToMainLegacyBars()
		{
			Main.hBar = this.Hue;
			Main.sBar = this.Saturation;
			Main.lBar = this.Luminance;
			Main.aBar = this.Alpha;
		}

		// Token: 0x04004FF4 RID: 20468
		public float Hue;

		// Token: 0x04004FF5 RID: 20469
		public float Saturation;

		// Token: 0x04004FF6 RID: 20470
		public float Luminance;

		// Token: 0x04004FF7 RID: 20471
		public float Alpha = 1f;
	}
}
