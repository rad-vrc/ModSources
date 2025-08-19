using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200025B RID: 603
	public class VineShader : ChromaShader
	{
		// Token: 0x06001F78 RID: 8056 RVA: 0x00513740 File Offset: 0x00511940
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.GetCanvasPositionOfIndex(i);
				fragment.SetColor(i, this._backgroundColor);
			}
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x00513774 File Offset: 0x00511974
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				ref Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = NoiseHelper.GetStaticNoise(gridPositionOfIndex.X);
				num = (num * 10f + time * 0.4f) % 10f;
				float num2 = 1f;
				if (num > 1f)
				{
					num2 = 1f - MathHelper.Clamp((num - 0.4f - 1f) / 0.4f, 0f, 1f);
					num = 1f;
				}
				float num3 = num - canvasPositionOfIndex.Y / 1f;
				Vector4 vector = this._backgroundColor;
				if (num3 > 0f)
				{
					float num4 = 1f;
					if (num3 < 0.2f)
					{
						num4 = num3 / 0.2f;
					}
					vector = Vector4.Lerp(this._backgroundColor, this._vineColor, num4 * num2);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0400467F RID: 18047
		private readonly Vector4 _backgroundColor = new Color(46, 17, 6).ToVector4();

		// Token: 0x04004680 RID: 18048
		private readonly Vector4 _vineColor = Color.Green.ToVector4();
	}
}
