using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020005AC RID: 1452
	public class WallOfFleshShader : ChromaShader
	{
		// Token: 0x06004289 RID: 17033 RVA: 0x005F9164 File Offset: 0x005F7364
		public WallOfFleshShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x005F9188 File Offset: 0x005F7388
		[RgbProcessor(new EffectDetailLevel[]
		{
			1,
			0
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 secondaryColor = this._secondaryColor;
				float dynamicNoise = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.3f, time / 5f);
				dynamicNoise = Math.Max(0f, 1f - dynamicNoise * 2f);
				secondaryColor = Vector4.Lerp(secondaryColor, this._primaryColor, (float)Math.Sqrt((double)dynamicNoise) * 0.75f);
				fragment.SetColor(i, secondaryColor);
			}
		}

		// Token: 0x040059BC RID: 22972
		private readonly Vector4 _primaryColor;

		// Token: 0x040059BD RID: 22973
		private readonly Vector4 _secondaryColor;
	}
}
