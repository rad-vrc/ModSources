using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020005AA RID: 1450
	public class VineShader : ChromaShader
	{
		// Token: 0x06004282 RID: 17026 RVA: 0x005F7AE4 File Offset: 0x005F5CE4
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

		// Token: 0x06004283 RID: 17027 RVA: 0x005F7B18 File Offset: 0x005F5D18
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
				float staticNoise = NoiseHelper.GetStaticNoise(gridPositionOfIndex.X);
				staticNoise = (staticNoise * 10f + time * 0.4f) % 10f;
				float num = 1f;
				if (staticNoise > 1f)
				{
					num = 1f - MathHelper.Clamp((staticNoise - 0.4f - 1f) / 0.4f, 0f, 1f);
					staticNoise = 1f;
				}
				float num2 = staticNoise - canvasPositionOfIndex.Y / 1f;
				Vector4 color = this._backgroundColor;
				if (num2 > 0f)
				{
					float num3 = 1f;
					if (num2 < 0.2f)
					{
						num3 = num2 / 0.2f;
					}
					color = Vector4.Lerp(this._backgroundColor, this._vineColor, num3 * num);
				}
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x040059B9 RID: 22969
		private readonly Vector4 _backgroundColor = new Color(46, 17, 6).ToVector4();

		// Token: 0x040059BA RID: 22970
		private readonly Vector4 _vineColor = Color.Green.ToVector4();
	}
}
