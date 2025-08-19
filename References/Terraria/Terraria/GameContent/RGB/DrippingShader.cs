using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000258 RID: 600
	public class DrippingShader : ChromaShader
	{
		// Token: 0x06001F6F RID: 8047 RVA: 0x00513258 File Offset: 0x00511458
		public DrippingShader(Color baseColor, Color liquidColor, float viscosity = 1f)
		{
			this._baseColor = baseColor.ToVector4();
			this._liquidColor = liquidColor.ToVector4();
			this._viscosity = viscosity;
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x00513284 File Offset: 0x00511484
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._baseColor, this._liquidColor, (float)Math.Sin((double)(time * 0.5f + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x005132E8 File Offset: 0x005114E8
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * new Vector2(0.7f * this._viscosity, 0.075f) + new Vector2(0f, time * -0.1f * this._viscosity));
				num = Math.Max(0f, 1f - (canvasPositionOfIndex.Y * 4.5f + 0.5f) * num);
				Vector4 vector = this._baseColor;
				vector = Vector4.Lerp(vector, this._liquidColor, num);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004676 RID: 18038
		private readonly Vector4 _baseColor;

		// Token: 0x04004677 RID: 18039
		private readonly Vector4 _liquidColor;

		// Token: 0x04004678 RID: 18040
		private readonly float _viscosity;
	}
}
