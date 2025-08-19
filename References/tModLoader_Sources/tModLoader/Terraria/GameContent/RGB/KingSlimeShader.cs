using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000592 RID: 1426
	public class KingSlimeShader : ChromaShader
	{
		// Token: 0x06004231 RID: 16945 RVA: 0x005F505A File Offset: 0x005F325A
		public KingSlimeShader(Color slimeColor, Color debrisColor)
		{
			this._slimeColor = slimeColor.ToVector4();
			this._debrisColor = debrisColor.ToVector4();
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x005F507C File Offset: 0x005F327C
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float dynamicNoise = NoiseHelper.GetDynamicNoise(fragment.GetCanvasPositionOfIndex(i), time * 0.25f);
				dynamicNoise = Math.Max(0f, 1f - dynamicNoise * 2f);
				Vector4 color = Vector4.Lerp(this._slimeColor, this._debrisColor, dynamicNoise);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x005F50E4 File Offset: 0x005F32E4
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			new Vector2(1.6f, 0.5f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 slimeColor = this._slimeColor;
				float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(0f, time * 0.1f));
				staticNoise = Math.Max(0f, 1f - staticNoise * 3f);
				staticNoise = (float)Math.Sqrt((double)staticNoise);
				slimeColor = Vector4.Lerp(slimeColor, this._debrisColor, staticNoise);
				fragment.SetColor(i, slimeColor);
			}
		}

		// Token: 0x0400596E RID: 22894
		private readonly Vector4 _slimeColor;

		// Token: 0x0400596F RID: 22895
		private readonly Vector4 _debrisColor;
	}
}
