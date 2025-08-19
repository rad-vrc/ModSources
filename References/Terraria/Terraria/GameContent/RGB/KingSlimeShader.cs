using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000233 RID: 563
	public class KingSlimeShader : ChromaShader
	{
		// Token: 0x06001EF6 RID: 7926 RVA: 0x0050F5A1 File Offset: 0x0050D7A1
		public KingSlimeShader(Color slimeColor, Color debrisColor)
		{
			this._slimeColor = slimeColor.ToVector4();
			this._debrisColor = debrisColor.ToVector4();
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0050F5C4 File Offset: 0x0050D7C4
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

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0050F62C File Offset: 0x0050D82C
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

		// Token: 0x04004604 RID: 17924
		private readonly Vector4 _slimeColor;

		// Token: 0x04004605 RID: 17925
		private readonly Vector4 _debrisColor;
	}
}
