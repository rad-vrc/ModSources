using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020005A7 RID: 1447
	public class UndergroundHallowShader : ChromaShader
	{
		// Token: 0x06004279 RID: 17017 RVA: 0x005F7658 File Offset: 0x005F5858
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time * 2f + fragment.GetCanvasPositionOfIndex(i).X)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._pinkCrystalColor, this._blueCrystalColor, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x005F76BC File Offset: 0x005F58BC
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 baseColor = this._baseColor;
				float dynamicNoise = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.4f, time * 0.05f);
				dynamicNoise = Math.Max(0f, 1f - 2.5f * dynamicNoise);
				float dynamicNoise2 = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.4f + new Vector2(0.05f, 0f), time * 0.05f);
				dynamicNoise2 = Math.Max(0f, 1f - 2.5f * dynamicNoise2);
				baseColor = ((dynamicNoise <= dynamicNoise2) ? Vector4.Lerp(baseColor, this._blueCrystalColor, dynamicNoise2) : Vector4.Lerp(baseColor, this._pinkCrystalColor, dynamicNoise));
				fragment.SetColor(i, baseColor);
			}
		}

		// Token: 0x040059B0 RID: 22960
		private readonly Vector4 _baseColor = new Color(0.05f, 0.05f, 0.05f).ToVector4();

		// Token: 0x040059B1 RID: 22961
		private readonly Vector4 _pinkCrystalColor = Color.HotPink.ToVector4();

		// Token: 0x040059B2 RID: 22962
		private readonly Vector4 _blueCrystalColor = Color.DeepSkyBlue.ToVector4();
	}
}
