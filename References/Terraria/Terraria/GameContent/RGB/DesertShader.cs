using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000244 RID: 580
	public class DesertShader : ChromaShader
	{
		// Token: 0x06001F2C RID: 7980 RVA: 0x0051129C File Offset: 0x0050F49C
		public DesertShader(Color baseColor, Color sandColor)
		{
			this._baseColor = baseColor.ToVector4();
			this._sandColor = sandColor.ToVector4();
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x005112C0 File Offset: 0x0050F4C0
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
				Vector4 vector = Vector4.Lerp(this._baseColor, this._sandColor, staticNoise * staticNoise);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004637 RID: 17975
		private readonly Vector4 _baseColor;

		// Token: 0x04004638 RID: 17976
		private readonly Vector4 _sandColor;
	}
}
