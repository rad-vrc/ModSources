using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200059F RID: 1439
	public class SandstormShader : ChromaShader
	{
		// Token: 0x06004261 RID: 16993 RVA: 0x005F66F4 File Offset: 0x005F48F4
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
				float staticNoise = NoiseHelper.GetStaticNoise(fragment.GetCanvasPositionOfIndex(i) * 0.3f + new Vector2(time, 0f - time) * 0.5f);
				Vector4 color = Vector4.Lerp(this._backColor, this._frontColor, staticNoise);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x04005993 RID: 22931
		private readonly Vector4 _backColor = new Vector4(0.2f, 0f, 0f, 1f);

		// Token: 0x04005994 RID: 22932
		private readonly Vector4 _frontColor = new Vector4(1f, 0.5f, 0f, 1f);
	}
}
