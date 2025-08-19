using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200057A RID: 1402
	public class BrainShader : ChromaShader
	{
		// Token: 0x060041EB RID: 16875 RVA: 0x005F2941 File Offset: 0x005F0B41
		public BrainShader(Color brainColor, Color veinColor)
		{
			this._brainColor = brainColor.ToVector4();
			this._veinColor = veinColor.ToVector4();
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x005F2964 File Offset: 0x005F0B64
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 color = Vector4.Lerp(this._brainColor, this._veinColor, Math.Max(0f, (float)Math.Sin((double)(time * 3f))));
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x005F29B8 File Offset: 0x005F0BB8
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			new Vector2(1.6f, 0.5f);
			Vector4 value = Vector4.Lerp(this._brainColor, this._veinColor, Math.Max(0f, (float)Math.Sin((double)(time * 3f))) * 0.5f + 0.5f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 brainColor = this._brainColor;
				float dynamicNoise = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.15f + new Vector2(time * 0.002f), time * 0.03f);
				dynamicNoise = (float)Math.Sin((double)(dynamicNoise * 10f)) * 0.5f + 0.5f;
				dynamicNoise = Math.Max(0f, 1f - 5f * dynamicNoise);
				brainColor = Vector4.Lerp(brainColor, value, dynamicNoise);
				fragment.SetColor(i, brainColor);
			}
		}

		// Token: 0x04005927 RID: 22823
		private readonly Vector4 _brainColor;

		// Token: 0x04005928 RID: 22824
		private readonly Vector4 _veinColor;
	}
}
