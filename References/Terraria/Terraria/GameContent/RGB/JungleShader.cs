using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000249 RID: 585
	public class JungleShader : ChromaShader
	{
		// Token: 0x06001F3B RID: 7995 RVA: 0x00511C68 File Offset: 0x0050FE68
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float dynamicNoise = NoiseHelper.GetDynamicNoise(fragment.GetCanvasPositionOfIndex(i) * 0.3f, time / 5f);
				Vector4 vector = Vector4.Lerp(this._backgroundColor, this._sporeColor, dynamicNoise);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00511CC0 File Offset: 0x0050FEC0
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
				float num = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.3f, time / 5f);
				num = Math.Max(0f, 1f - num * 2.5f);
				Vector4 vector = Vector4.Lerp(this._backgroundColor, this._sporeColor, num);
				if (flag)
				{
					float num2 = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 100f);
					num2 = Math.Max(0f, 1f - num2 * 20f);
					vector = Vector4.Lerp(vector, this._flowerColors[((gridPositionOfIndex.Y * 47 + gridPositionOfIndex.X) % 5 + 5) % 5], num2);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0400464E RID: 17998
		private readonly Vector4 _backgroundColor = new Color(40, 80, 0).ToVector4();

		// Token: 0x0400464F RID: 17999
		private readonly Vector4 _sporeColor = new Color(255, 255, 0).ToVector4();

		// Token: 0x04004650 RID: 18000
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
