using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200057E RID: 1406
	public class CultistShader : ChromaShader
	{
		// Token: 0x060041F7 RID: 16887 RVA: 0x005F2E70 File Offset: 0x005F1070
		[RgbProcessor(new EffectDetailLevel[]
		{
			1,
			0
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			time *= 2f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 backgroundColor = this._backgroundColor;
				float num = time * 0.5f + canvasPositionOfIndex.X + canvasPositionOfIndex.Y;
				float value = (float)Math.Cos((double)num) * 2f + 2f;
				value = MathHelper.Clamp(value, 0f, 1f);
				num = (num + 3.1415927f) % 18.849556f;
				Vector4 value2;
				if (num < 6.2831855f)
				{
					float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.2f));
					staticNoise = Math.Max(0f, 1f - staticNoise * staticNoise * 4f * staticNoise);
					staticNoise = MathHelper.Clamp(staticNoise, 0f, 1f);
					value2 = Vector4.Lerp(this._fireDarkColor, this._fireBrightColor, staticNoise);
				}
				else if (num < 12.566371f)
				{
					float dynamicNoise = NoiseHelper.GetDynamicNoise(new Vector2((canvasPositionOfIndex.X + canvasPositionOfIndex.Y) * 0.2f, 0f), time / 5f);
					dynamicNoise = Math.Max(0f, 1f - dynamicNoise * 1.5f);
					value2 = Vector4.Lerp(this._iceDarkColor, this._iceBrightColor, dynamicNoise);
				}
				else
				{
					float dynamicNoise2 = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.15f, time * 0.05f);
					dynamicNoise2 = (float)Math.Sin((double)(dynamicNoise2 * 15f)) * 0.5f + 0.5f;
					dynamicNoise2 = Math.Max(0f, 1f - 5f * dynamicNoise2);
					value2 = Vector4.Lerp(this._lightningDarkColor, this._lightningBrightColor, dynamicNoise2);
				}
				backgroundColor = Vector4.Lerp(backgroundColor, value2, value);
				fragment.SetColor(i, backgroundColor);
			}
		}

		// Token: 0x04005931 RID: 22833
		private readonly Vector4 _lightningDarkColor = new Color(23, 11, 23).ToVector4();

		// Token: 0x04005932 RID: 22834
		private readonly Vector4 _lightningBrightColor = new Color(249, 140, 255).ToVector4();

		// Token: 0x04005933 RID: 22835
		private readonly Vector4 _fireDarkColor = Color.Red.ToVector4();

		// Token: 0x04005934 RID: 22836
		private readonly Vector4 _fireBrightColor = new Color(255, 196, 0).ToVector4();

		// Token: 0x04005935 RID: 22837
		private readonly Vector4 _iceDarkColor = new Color(4, 4, 148).ToVector4();

		// Token: 0x04005936 RID: 22838
		private readonly Vector4 _iceBrightColor = new Color(208, 233, 255).ToVector4();

		// Token: 0x04005937 RID: 22839
		private readonly Vector4 _backgroundColor = Color.Black.ToVector4();
	}
}
