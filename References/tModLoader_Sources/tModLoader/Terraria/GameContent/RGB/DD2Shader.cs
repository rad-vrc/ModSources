using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200057F RID: 1407
	public class DD2Shader : ChromaShader
	{
		// Token: 0x060041F9 RID: 16889 RVA: 0x005F3126 File Offset: 0x005F1326
		public DD2Shader(Color darkGlowColor, Color lightGlowColor)
		{
			this._darkGlowColor = darkGlowColor.ToVector4();
			this._lightGlowColor = lightGlowColor.ToVector4();
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x005F3148 File Offset: 0x005F1348
		[RgbProcessor(new EffectDetailLevel[]
		{
			0,
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector2 vector = fragment.CanvasCenter;
			if (quality == null)
			{
				vector..ctor(1.7f, 0.5f);
			}
			time *= 0.5f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 value;
				value..ctor(0f, 0f, 0f, 1f);
				float num = (canvasPositionOfIndex - vector).Length();
				float num2 = num * num * 0.75f;
				float num3 = (num - time) % 1f;
				if (num3 < 0f)
				{
					num3 += 1f;
				}
				num3 = ((num3 <= 0.8f) ? (num3 / 0.8f) : (num3 * (1f - (num3 - 1f + 0.2f) / 0.2f)));
				Vector4 value2 = Vector4.Lerp(this._darkGlowColor, this._lightGlowColor, num3 * num3);
				num3 *= MathHelper.Clamp(1f - num2, 0f, 1f) * 0.75f + 0.25f;
				value = Vector4.Lerp(value, value2, num3);
				if (num < 0.5f)
				{
					float amount = 1f - MathHelper.Clamp((num - 0.5f + 0.4f) / 0.4f, 0f, 1f);
					value = Vector4.Lerp(value, this._lightGlowColor, amount);
				}
				fragment.SetColor(i, value);
			}
		}

		// Token: 0x04005938 RID: 22840
		private readonly Vector4 _darkGlowColor;

		// Token: 0x04005939 RID: 22841
		private readonly Vector4 _lightGlowColor;
	}
}
