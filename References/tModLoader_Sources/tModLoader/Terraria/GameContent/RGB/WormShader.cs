using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020005AD RID: 1453
	public class WormShader : ChromaShader
	{
		// Token: 0x0600428B RID: 17035 RVA: 0x005F9207 File Offset: 0x005F7407
		public WormShader()
		{
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x005F920F File Offset: 0x005F740F
		public WormShader(Color skinColor, Color eyeColor, Color innerEyeColor)
		{
			this._skinColor = skinColor.ToVector4();
			this._eyeColor = eyeColor.ToVector4();
			this._innerEyeColor = innerEyeColor.ToVector4();
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x005F9240 File Offset: 0x005F7440
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float amount = Math.Max(0f, (float)Math.Sin((double)(time * -3f + fragment.GetCanvasPositionOfIndex(i).X)));
				Vector4 color = Vector4.Lerp(this._skinColor, this._eyeColor, amount);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0600428E RID: 17038 RVA: 0x005F92A0 File Offset: 0x005F74A0
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

		// Token: 0x040059BE RID: 22974
		private readonly Vector4 _skinColor;

		// Token: 0x040059BF RID: 22975
		private readonly Vector4 _eyeColor;

		// Token: 0x040059C0 RID: 22976
		private readonly Vector4 _innerEyeColor;
	}
}
