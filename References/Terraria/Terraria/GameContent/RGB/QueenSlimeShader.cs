using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200023A RID: 570
	public class QueenSlimeShader : ChromaShader
	{
		// Token: 0x06001F0B RID: 7947 RVA: 0x00510268 File Offset: 0x0050E468
		public QueenSlimeShader(Color slimeColor, Color debrisColor)
		{
			this._slimeColor = slimeColor.ToVector4();
			this._debrisColor = debrisColor.ToVector4();
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0051028C File Offset: 0x0050E48C
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = NoiseHelper.GetDynamicNoise(fragment.GetCanvasPositionOfIndex(i), time * 0.25f);
				num = Math.Max(0f, 1f - num * 2f);
				Vector4 vector = Vector4.Lerp(this._slimeColor, this._debrisColor, num);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x005102F4 File Offset: 0x0050E4F4
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			new Vector2(1.6f, 0.5f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = this._slimeColor;
				float num = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(0f, time * 0.1f));
				num = Math.Max(0f, 1f - num * 3f);
				num = (float)Math.Sqrt((double)num);
				vector = Vector4.Lerp(vector, this._debrisColor, num);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004618 RID: 17944
		private readonly Vector4 _slimeColor;

		// Token: 0x04004619 RID: 17945
		private readonly Vector4 _debrisColor;
	}
}
