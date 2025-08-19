using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200024F RID: 591
	public class SandstormShader : ChromaShader
	{
		// Token: 0x06001F50 RID: 8016 RVA: 0x005124EC File Offset: 0x005106EC
		[RgbProcessor(new EffectDetailLevel[]
		{
			0,
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			if (quality == null)
			{
				time *= 0.25f;
			}
			for (int i = 0; i < fragment.Count; i++)
			{
				float staticNoise = NoiseHelper.GetStaticNoise(fragment.GetCanvasPositionOfIndex(i) * 0.3f + new Vector2(time, -time) * 0.5f);
				Vector4 vector = Vector4.Lerp(this._backColor, this._frontColor, staticNoise);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0400465C RID: 18012
		private readonly Vector4 _backColor = new Vector4(0.2f, 0f, 0f, 1f);

		// Token: 0x0400465D RID: 18013
		private readonly Vector4 _frontColor = new Vector4(1f, 0.5f, 0f, 1f);
	}
}
