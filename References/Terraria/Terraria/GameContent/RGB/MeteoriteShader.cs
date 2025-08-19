using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200024C RID: 588
	public class MeteoriteShader : ChromaShader
	{
		// Token: 0x06001F44 RID: 8004 RVA: 0x00511F8C File Offset: 0x0051018C
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._baseColor, this._secondaryColor, (float)Math.Sin((double)(time + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00511FE8 File Offset: 0x005101E8
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector4 vector = this._baseColor;
				float dynamicNoise = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 10f);
				vector = Vector4.Lerp(vector, this._secondaryColor, dynamicNoise * dynamicNoise);
				float num = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * 0.5f + new Vector2(0f, time * 0.05f), time / 20f);
				num = Math.Max(0f, 1f - num * 2f);
				vector = Vector4.Lerp(vector, this._glowColor, (float)Math.Sqrt((double)num) * 0.75f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004653 RID: 18003
		private readonly Vector4 _baseColor = new Color(39, 15, 26).ToVector4();

		// Token: 0x04004654 RID: 18004
		private readonly Vector4 _secondaryColor = new Color(69, 50, 43).ToVector4();

		// Token: 0x04004655 RID: 18005
		private readonly Vector4 _glowColor = Color.DarkOrange.ToVector4();
	}
}
