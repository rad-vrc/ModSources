using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000589 RID: 1417
	public class EyeOfCthulhuShader : ChromaShader
	{
		// Token: 0x06004218 RID: 16920 RVA: 0x005F425B File Offset: 0x005F245B
		public EyeOfCthulhuShader(Color eyeColor, Color veinColor, Color backgroundColor)
		{
			this._eyeColor = eyeColor.ToVector4();
			this._veinColor = veinColor.ToVector4();
			this._backgroundColor = backgroundColor.ToVector4();
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x005F428C File Offset: 0x005F248C
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time + fragment.GetCanvasPositionOfIndex(i).X * 4f)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._veinColor, this._eyeColor, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x005F42F0 File Offset: 0x005F24F0
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
			float num = time * 0.2f % 2f;
			int num2 = 1;
			if (num > 1f)
			{
				num = 2f - num;
				num2 = -1;
			}
			Vector2 vector = new Vector2(num * 7f - 3.5f, 0f) + fragment.CanvasCenter;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector2 = this._backgroundColor;
				Vector2 vector3 = canvasPositionOfIndex - vector;
				float num3 = vector3.Length();
				if (num3 < 0.5f)
				{
					float amount = 1f - MathHelper.Clamp((num3 - 0.5f + 0.2f) / 0.2f, 0f, 1f);
					float num4 = MathHelper.Clamp((vector3.X + 0.5f - 0.2f) / 0.6f, 0f, 1f);
					if (num2 == 1)
					{
						num4 = 1f - num4;
					}
					Vector4 value = Vector4.Lerp(this._eyeColor, this._veinColor, num4);
					vector2 = Vector4.Lerp(vector2, value, amount);
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x04005952 RID: 22866
		private readonly Vector4 _eyeColor;

		// Token: 0x04005953 RID: 22867
		private readonly Vector4 _veinColor;

		// Token: 0x04005954 RID: 22868
		private readonly Vector4 _backgroundColor;
	}
}
