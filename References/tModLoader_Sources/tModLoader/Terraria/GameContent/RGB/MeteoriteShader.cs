using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000596 RID: 1430
	public class MeteoriteShader : ChromaShader
	{
		// Token: 0x0600423D RID: 16957 RVA: 0x005F573C File Offset: 0x005F393C
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time + fragment.GetCanvasPositionOfIndex(i).X)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._baseColor, this._secondaryColor, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x005F5798 File Offset: 0x005F3998
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector4 baseColor = this._baseColor;
				float dynamicNoise = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 10f);
				baseColor = Vector4.Lerp(baseColor, this._secondaryColor, dynamicNoise * dynamicNoise);
				float dynamicNoise2 = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.5f + new Vector2(0f, time * 0.05f), time / 20f);
				dynamicNoise2 = Math.Max(0f, 1f - dynamicNoise2 * 2f);
				baseColor = Vector4.Lerp(baseColor, this._glowColor, (float)Math.Sqrt((double)dynamicNoise2) * 0.75f);
				fragment.SetColor(i, baseColor);
			}
		}

		// Token: 0x04005978 RID: 22904
		private readonly Vector4 _baseColor = new Color(39, 15, 26).ToVector4();

		// Token: 0x04005979 RID: 22905
		private readonly Vector4 _secondaryColor = new Color(69, 50, 43).ToVector4();

		// Token: 0x0400597A RID: 22906
		private readonly Vector4 _glowColor = Color.DarkOrange.ToVector4();
	}
}
