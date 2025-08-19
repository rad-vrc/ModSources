using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000248 RID: 584
	public class IceShader : ChromaShader
	{
		// Token: 0x06001F39 RID: 7993 RVA: 0x00511B3C File Offset: 0x0050FD3C
		public IceShader(Color baseColor, Color iceColor)
		{
			this._baseColor = baseColor.ToVector4();
			this._iceColor = iceColor.ToVector4();
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00511B88 File Offset: 0x0050FD88
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
				float num = NoiseHelper.GetDynamicNoise(new Vector2((canvasPositionOfIndex.X - canvasPositionOfIndex.Y) * 0.2f, 0f), time / 5f);
				num = Math.Max(0f, 1f - num * 1.5f);
				float num2 = NoiseHelper.GetDynamicNoise(new Vector2((canvasPositionOfIndex.X - canvasPositionOfIndex.Y) * 0.3f, 0.3f), time / 20f);
				num2 = Math.Max(0f, 1f - num2 * 5f);
				Vector4 vector = Vector4.Lerp(this._baseColor, this._iceColor, num);
				vector = Vector4.Lerp(vector, this._shineColor, num2);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0400464B RID: 17995
		private readonly Vector4 _baseColor;

		// Token: 0x0400464C RID: 17996
		private readonly Vector4 _iceColor;

		// Token: 0x0400464D RID: 17997
		private readonly Vector4 _shineColor = new Vector4(1f, 1f, 0.7f, 1f);
	}
}
