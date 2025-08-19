using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000582 RID: 1410
	public class DesertShader : ChromaShader
	{
		// Token: 0x06004201 RID: 16897 RVA: 0x005F3454 File Offset: 0x005F1654
		public DesertShader(Color baseColor, Color sandColor)
		{
			this._baseColor = baseColor.ToVector4();
			this._sandColor = sandColor.ToVector4();
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x005F3478 File Offset: 0x005F1678
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
				fragment.GetGridPositionOfIndex(i);
				canvasPositionOfIndex.Y += (float)Math.Sin((double)(canvasPositionOfIndex.X * 2f + time * 2f)) * 0.2f;
				float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * new Vector2(0.1f, 0.5f));
				Vector4 color = Vector4.Lerp(this._baseColor, this._sandColor, staticNoise * staticNoise);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0400593C RID: 22844
		private readonly Vector4 _baseColor;

		// Token: 0x0400593D RID: 22845
		private readonly Vector4 _sandColor;
	}
}
