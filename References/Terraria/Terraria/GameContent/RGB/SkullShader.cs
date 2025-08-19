using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200023B RID: 571
	public class SkullShader : ChromaShader
	{
		// Token: 0x06001F0E RID: 7950 RVA: 0x00510390 File Offset: 0x0050E590
		public SkullShader(Color skullColor, Color bloodDark, Color bloodLight)
		{
			this._skullColor = skullColor.ToVector4();
			this._bloodDark = bloodDark.ToVector4();
			this._bloodLight = bloodLight.ToVector4();
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x005103E0 File Offset: 0x0050E5E0
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._skullColor, this._bloodLight, (float)Math.Sin((double)(time * 2f + canvasPositionOfIndex.X * 2f)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x00510448 File Offset: 0x0050E648
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				ref Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector4 vector = this._backgroundColor;
				float num = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 10f + time * 0.75f) % 10f + canvasPositionOfIndex.Y - 1f;
				if (num > 0f)
				{
					float amount = Math.Max(0f, 1.2f - num);
					if (num < 0.2f)
					{
						amount = num * 5f;
					}
					vector = Vector4.Lerp(vector, this._skullColor, amount);
				}
				float num2 = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.5f + new Vector2(12.5f, time * 0.2f));
				num2 = Math.Max(0f, 1f - num2 * num2 * 4f * num2 * (1f - canvasPositionOfIndex.Y * canvasPositionOfIndex.Y)) * canvasPositionOfIndex.Y * canvasPositionOfIndex.Y;
				num2 = MathHelper.Clamp(num2, 0f, 1f);
				Vector4 value = Vector4.Lerp(this._bloodDark, this._bloodLight, num2);
				vector = Vector4.Lerp(vector, value, num2);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0400461A RID: 17946
		private readonly Vector4 _skullColor;

		// Token: 0x0400461B RID: 17947
		private readonly Vector4 _bloodDark;

		// Token: 0x0400461C RID: 17948
		private readonly Vector4 _bloodLight;

		// Token: 0x0400461D RID: 17949
		private readonly Vector4 _backgroundColor = Color.Black.ToVector4();
	}
}
