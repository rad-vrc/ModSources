﻿using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200022E RID: 558
	public class EyeOfCthulhuShader : ChromaShader
	{
		// Token: 0x06001EE9 RID: 7913 RVA: 0x0050ED78 File Offset: 0x0050CF78
		public EyeOfCthulhuShader(Color eyeColor, Color veinColor, Color backgroundColor)
		{
			this._eyeColor = eyeColor.ToVector4();
			this._veinColor = veinColor.ToVector4();
			this._backgroundColor = backgroundColor.ToVector4();
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x0050EDA8 File Offset: 0x0050CFA8
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._veinColor, this._eyeColor, (float)Math.Sin((double)(time + canvasPositionOfIndex.X * 4f)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0050EE0C File Offset: 0x0050D00C
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
			Vector2 value = new Vector2(num * 7f - 3.5f, 0f) + fragment.CanvasCenter;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = this._backgroundColor;
				Vector2 vector2 = canvasPositionOfIndex - value;
				float num3 = vector2.Length();
				if (num3 < 0.5f)
				{
					float amount = 1f - MathHelper.Clamp((num3 - 0.5f + 0.2f) / 0.2f, 0f, 1f);
					float num4 = MathHelper.Clamp((vector2.X + 0.5f - 0.2f) / 0.6f, 0f, 1f);
					if (num2 == 1)
					{
						num4 = 1f - num4;
					}
					Vector4 value2 = Vector4.Lerp(this._eyeColor, this._veinColor, num4);
					vector = Vector4.Lerp(vector, value2, amount);
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x040045F4 RID: 17908
		private readonly Vector4 _eyeColor;

		// Token: 0x040045F5 RID: 17909
		private readonly Vector4 _veinColor;

		// Token: 0x040045F6 RID: 17910
		private readonly Vector4 _backgroundColor;
	}
}
