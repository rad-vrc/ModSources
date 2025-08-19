using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000593 RID: 1427
	public class LavaIndicatorShader : ChromaShader
	{
		// Token: 0x06004234 RID: 16948 RVA: 0x005F517E File Offset: 0x005F337E
		public LavaIndicatorShader(Color backgroundColor, Color primaryColor, Color secondaryColor)
		{
			this._backgroundColor = backgroundColor.ToVector4();
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06004235 RID: 16949 RVA: 0x005F51B0 File Offset: 0x005F33B0
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float staticNoise = NoiseHelper.GetStaticNoise(fragment.GetCanvasPositionOfIndex(i) * 0.3f + new Vector2(12.5f, time * 0.2f));
				staticNoise = Math.Max(0f, 1f - staticNoise * staticNoise * 4f * staticNoise);
				staticNoise = MathHelper.Clamp(staticNoise, 0f, 1f);
				Vector4 color = Vector4.Lerp(this._primaryColor, this._secondaryColor, staticNoise);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004236 RID: 16950 RVA: 0x005F5248 File Offset: 0x005F3448
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = this._backgroundColor;
				float dynamicNoise = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.2f, time * 0.5f);
				float num = 0.4f;
				num += dynamicNoise * 0.4f;
				float num2 = 1.1f - canvasPositionOfIndex.Y;
				if (num2 < num)
				{
					float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.2f));
					staticNoise = Math.Max(0f, 1f - staticNoise * staticNoise * 4f * staticNoise);
					staticNoise = MathHelper.Clamp(staticNoise, 0f, 1f);
					Vector4 value = Vector4.Lerp(this._primaryColor, this._secondaryColor, staticNoise);
					float amount = 1f - MathHelper.Clamp((num2 - num + 0.2f) / 0.2f, 0f, 1f);
					vector = Vector4.Lerp(vector, value, amount);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04005970 RID: 22896
		private readonly Vector4 _backgroundColor;

		// Token: 0x04005971 RID: 22897
		private readonly Vector4 _primaryColor;

		// Token: 0x04005972 RID: 22898
		private readonly Vector4 _secondaryColor;
	}
}
