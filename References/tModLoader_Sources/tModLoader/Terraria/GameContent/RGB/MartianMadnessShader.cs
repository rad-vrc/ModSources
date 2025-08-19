using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000595 RID: 1429
	public class MartianMadnessShader : ChromaShader
	{
		// Token: 0x0600423A RID: 16954 RVA: 0x005F53FC File Offset: 0x005F35FC
		public MartianMadnessShader(Color metalColor, Color glassColor, Color beamColor, Color backgroundColor)
		{
			this._metalColor = metalColor.ToVector4();
			this._glassColor = glassColor.ToVector4();
			this._beamColor = beamColor.ToVector4();
			this._backgroundColor = backgroundColor.ToVector4();
		}

		// Token: 0x0600423B RID: 16955 RVA: 0x005F5438 File Offset: 0x005F3638
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				float amount = (float)Math.Sin((double)(time * 2f + canvasPositionOfIndex.X * 5f)) * 0.5f + 0.5f;
				int num = (gridPositionOfIndex.X + gridPositionOfIndex.Y) % 2;
				if (num < 0)
				{
					num += 2;
				}
				Vector4 color = (num == 1) ? Vector4.Lerp(this._glassColor, this._beamColor, amount) : this._metalColor;
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0600423C RID: 16956 RVA: 0x005F54DC File Offset: 0x005F36DC
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			if (device.Type != null && device.Type != 6)
			{
				this.ProcessLowDetail(device, fragment, quality, time);
				return;
			}
			float num = time * 0.5f % 6.2831855f;
			if (num > 3.1415927f)
			{
				num = 6.2831855f - num;
			}
			Vector2 vector;
			vector..ctor(1.7f + (float)Math.Cos((double)num) * 2f, -0.5f + (float)Math.Sin((double)num) * 1.1f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector2 = this._backgroundColor;
				float num2 = Math.Abs(vector.X - canvasPositionOfIndex.X);
				if (canvasPositionOfIndex.Y > vector.Y && num2 < 0.2f)
				{
					float num3 = 1f - MathHelper.Clamp((num2 - 0.2f + 0.2f) / 0.2f, 0f, 1f);
					float num4 = Math.Abs((num - 1.5707964f) / 1.5707964f);
					num4 = Math.Max(0f, 1f - num4 * 3f);
					vector2 = Vector4.Lerp(vector2, this._beamColor, num3 * num4);
				}
				Vector2 vector3 = vector - canvasPositionOfIndex;
				vector3.X /= 1f;
				vector3.Y /= 0.2f;
				float num5 = vector3.Length();
				if (num5 < 1f)
				{
					float amount = 1f - MathHelper.Clamp((num5 - 1f + 0.2f) / 0.2f, 0f, 1f);
					vector2 = Vector4.Lerp(vector2, this._metalColor, amount);
				}
				Vector2 vector4 = vector - canvasPositionOfIndex + new Vector2(0f, -0.1f);
				vector4.X /= 0.3f;
				vector4.Y /= 0.3f;
				if (vector4.Y < 0f)
				{
					vector4.Y *= 2f;
				}
				float num6 = vector4.Length();
				if (num6 < 1f)
				{
					float amount2 = 1f - MathHelper.Clamp((num6 - 1f + 0.2f) / 0.2f, 0f, 1f);
					vector2 = Vector4.Lerp(vector2, this._glassColor, amount2);
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x04005974 RID: 22900
		private readonly Vector4 _metalColor;

		// Token: 0x04005975 RID: 22901
		private readonly Vector4 _glassColor;

		// Token: 0x04005976 RID: 22902
		private readonly Vector4 _beamColor;

		// Token: 0x04005977 RID: 22903
		private readonly Vector4 _backgroundColor;
	}
}
