using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200025A RID: 602
	public class UndergroundMushroomShader : ChromaShader
	{
		// Token: 0x06001F75 RID: 8053 RVA: 0x0051353C File Offset: 0x0051173C
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._edgeGlowColor, this._sporeColor, (float)Math.Sin((double)(time * 0.5f + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x005135A0 File Offset: 0x005117A0
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
				Vector4 vector = this._baseColor;
				float num = ((NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 10f + time * 0.2f) % 10f - (1f - canvasPositionOfIndex.Y)) * 2f;
				if (num > 0f)
				{
					float amount = Math.Max(0f, 1.5f - num);
					if (num < 0.5f)
					{
						amount = num * 2f;
					}
					vector = Vector4.Lerp(vector, this._sporeColor, amount);
				}
				float num2 = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(0f, time * 0.1f));
				num2 = Math.Max(0f, 1f - num2 * (1f + (1f - canvasPositionOfIndex.Y) * 4f));
				num2 *= Math.Max(0f, (canvasPositionOfIndex.Y - 0.3f) / 0.7f);
				vector = Vector4.Lerp(vector, this._edgeGlowColor, num2);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0400467C RID: 18044
		private readonly Vector4 _baseColor = new Color(10, 10, 10).ToVector4();

		// Token: 0x0400467D RID: 18045
		private readonly Vector4 _edgeGlowColor = new Color(0, 0, 255).ToVector4();

		// Token: 0x0400467E RID: 18046
		private readonly Vector4 _sporeColor = new Color(255, 230, 150).ToVector4();
	}
}
