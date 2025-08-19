using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020005A8 RID: 1448
	public class UndergroundMushroomShader : ChromaShader
	{
		// Token: 0x0600427C RID: 17020 RVA: 0x005F77F0 File Offset: 0x005F59F0
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time * 0.5f + fragment.GetCanvasPositionOfIndex(i).X)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._edgeGlowColor, this._sporeColor, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x005F7854 File Offset: 0x005F5A54
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 value = this._baseColor;
				float num = ((NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 10f + time * 0.2f) % 10f - (1f - canvasPositionOfIndex.Y)) * 2f;
				if (num > 0f)
				{
					float amount = Math.Max(0f, 1.5f - num);
					if (num < 0.5f)
					{
						amount = num * 2f;
					}
					value = Vector4.Lerp(value, this._sporeColor, amount);
				}
				float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(0f, time * 0.1f));
				staticNoise = Math.Max(0f, 1f - staticNoise * (1f + (1f - canvasPositionOfIndex.Y) * 4f));
				staticNoise *= Math.Max(0f, (canvasPositionOfIndex.Y - 0.3f) / 0.7f);
				value = Vector4.Lerp(value, this._edgeGlowColor, staticNoise);
				fragment.SetColor(i, value);
			}
		}

		// Token: 0x040059B3 RID: 22963
		private readonly Vector4 _baseColor = new Color(10, 10, 10).ToVector4();

		// Token: 0x040059B4 RID: 22964
		private readonly Vector4 _edgeGlowColor = new Color(0, 0, 255).ToVector4();

		// Token: 0x040059B5 RID: 22965
		private readonly Vector4 _sporeColor = new Color(255, 230, 150).ToVector4();
	}
}
