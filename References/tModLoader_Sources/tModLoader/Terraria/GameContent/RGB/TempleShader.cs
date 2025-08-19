using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020005A4 RID: 1444
	public class TempleShader : ChromaShader
	{
		// Token: 0x06004270 RID: 17008 RVA: 0x005F6FB8 File Offset: 0x005F51B8
		[RgbProcessor(new EffectDetailLevel[]
		{
			0,
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector4 vector = this._backgroundColor;
				float num = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.Y * 7) * 10f + time) % 10f - (canvasPositionOfIndex.X + 2f);
				if (num > 0f)
				{
					float amount = Math.Max(0f, 1.2f - num);
					if (num < 0.2f)
					{
						amount = num * 5f;
					}
					vector = Vector4.Lerp(vector, this._glowColor, amount);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x040059A4 RID: 22948
		private readonly Vector4 _backgroundColor = new Vector4(0.05f, 0.025f, 0f, 1f);

		// Token: 0x040059A5 RID: 22949
		private readonly Vector4 _glowColor = Color.Orange.ToVector4();
	}
}
