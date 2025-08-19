using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000590 RID: 1424
	public class JungleShader : ChromaShader
	{
		// Token: 0x0600422B RID: 16939 RVA: 0x005F4DB8 File Offset: 0x005F2FB8
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float dynamicNoise = NoiseHelper.GetDynamicNoise(fragment.GetCanvasPositionOfIndex(i) * 0.3f, time / 5f);
				Vector4 color = Vector4.Lerp(this._backgroundColor, this._sporeColor, dynamicNoise);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x005F4E10 File Offset: 0x005F3010
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			bool flag = device.Type == null || device.Type == 6;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				float dynamicNoise = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.3f, time / 5f);
				dynamicNoise = Math.Max(0f, 1f - dynamicNoise * 2.5f);
				Vector4 vector = Vector4.Lerp(this._backgroundColor, this._sporeColor, dynamicNoise);
				if (flag)
				{
					float dynamicNoise2 = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 100f);
					dynamicNoise2 = Math.Max(0f, 1f - dynamicNoise2 * 20f);
					vector = Vector4.Lerp(vector, this._flowerColors[((gridPositionOfIndex.Y * 47 + gridPositionOfIndex.X) % 5 + 5) % 5], dynamicNoise2);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0400596A RID: 22890
		private readonly Vector4 _backgroundColor = new Color(40, 80, 0).ToVector4();

		// Token: 0x0400596B RID: 22891
		private readonly Vector4 _sporeColor = new Color(255, 255, 0).ToVector4();

		// Token: 0x0400596C RID: 22892
		private readonly Vector4[] _flowerColors = new Vector4[]
		{
			Color.Yellow.ToVector4(),
			Color.Pink.ToVector4(),
			Color.Purple.ToVector4(),
			Color.Red.ToVector4(),
			Color.Blue.ToVector4()
		};
	}
}
