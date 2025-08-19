using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200023E RID: 574
	public class WallOfFleshShader : ChromaShader
	{
		// Token: 0x06001F18 RID: 7960 RVA: 0x00510A8F File Offset: 0x0050EC8F
		public WallOfFleshShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00510AB4 File Offset: 0x0050ECB4
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
				Vector4 vector = this._secondaryColor;
				float num = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.3f, time / 5f);
				num = Math.Max(0f, 1f - num * 2f);
				vector = Vector4.Lerp(vector, this._primaryColor, (float)Math.Sqrt((double)num) * 0.75f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004628 RID: 17960
		private readonly Vector4 _primaryColor;

		// Token: 0x04004629 RID: 17961
		private readonly Vector4 _secondaryColor;
	}
}
