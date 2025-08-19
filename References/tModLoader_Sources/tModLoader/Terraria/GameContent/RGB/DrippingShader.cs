using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000583 RID: 1411
	public class DrippingShader : ChromaShader
	{
		// Token: 0x06004203 RID: 16899 RVA: 0x005F350E File Offset: 0x005F170E
		public DrippingShader(Color baseColor, Color liquidColor, float viscosity = 1f)
		{
			this._baseColor = baseColor.ToVector4();
			this._liquidColor = liquidColor.ToVector4();
			this._viscosity = viscosity;
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x005F3538 File Offset: 0x005F1738
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time * 0.5f + fragment.GetCanvasPositionOfIndex(i).X)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._baseColor, this._liquidColor, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x005F359C File Offset: 0x005F179C
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
				float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * new Vector2(0.7f * this._viscosity, 0.075f) + new Vector2(0f, time * -0.1f * this._viscosity));
				staticNoise = Math.Max(0f, 1f - (canvasPositionOfIndex.Y * 4.5f + 0.5f) * staticNoise);
				Vector4 baseColor = this._baseColor;
				baseColor = Vector4.Lerp(baseColor, this._liquidColor, staticNoise);
				fragment.SetColor(i, baseColor);
			}
		}

		// Token: 0x0400593E RID: 22846
		private readonly Vector4 _baseColor;

		// Token: 0x0400593F RID: 22847
		private readonly Vector4 _liquidColor;

		// Token: 0x04005940 RID: 22848
		private readonly float _viscosity;
	}
}
