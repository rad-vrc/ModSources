using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200023F RID: 575
	public class WormShader : ChromaShader
	{
		// Token: 0x06001F1A RID: 7962 RVA: 0x0050EBD1 File Offset: 0x0050CDD1
		public WormShader()
		{
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x00510B33 File Offset: 0x0050ED33
		public WormShader(Color skinColor, Color eyeColor, Color innerEyeColor)
		{
			this._skinColor = skinColor.ToVector4();
			this._eyeColor = eyeColor.ToVector4();
			this._innerEyeColor = innerEyeColor.ToVector4();
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x00510B64 File Offset: 0x0050ED64
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float amount = Math.Max(0f, (float)Math.Sin((double)(time * -3f + canvasPositionOfIndex.X)));
				Vector4 vector = Vector4.Lerp(this._skinColor, this._eyeColor, amount);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00510BC8 File Offset: 0x0050EDC8
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			time *= 0.25f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				canvasPositionOfIndex.X -= time * 1.5f;
				canvasPositionOfIndex.X %= 2f;
				if (canvasPositionOfIndex.X < 0f)
				{
					canvasPositionOfIndex.X += 2f;
				}
				float num = (canvasPositionOfIndex - new Vector2(0.5f)).Length();
				Vector4 vector = this._skinColor;
				if (num < 0.5f)
				{
					float num2 = MathHelper.Clamp((num - 0.5f + 0.2f) / 0.2f, 0f, 1f);
					vector = Vector4.Lerp(vector, this._eyeColor, 1f - num2);
					if (num < 0.4f)
					{
						num2 = MathHelper.Clamp((num - 0.4f + 0.2f) / 0.2f, 0f, 1f);
						vector = Vector4.Lerp(vector, this._innerEyeColor, 1f - num2);
					}
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0400462A RID: 17962
		private readonly Vector4 _skinColor;

		// Token: 0x0400462B RID: 17963
		private readonly Vector4 _eyeColor;

		// Token: 0x0400462C RID: 17964
		private readonly Vector4 _innerEyeColor;
	}
}
