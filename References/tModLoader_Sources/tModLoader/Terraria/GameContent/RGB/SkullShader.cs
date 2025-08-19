using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020005A0 RID: 1440
	public class SkullShader : ChromaShader
	{
		// Token: 0x06004263 RID: 16995 RVA: 0x005F67C4 File Offset: 0x005F49C4
		public SkullShader(Color skullColor, Color bloodDark, Color bloodLight)
		{
			this._skullColor = skullColor.ToVector4();
			this._bloodDark = bloodDark.ToVector4();
			this._bloodLight = bloodLight.ToVector4();
		}

		// Token: 0x06004264 RID: 16996 RVA: 0x005F6814 File Offset: 0x005F4A14
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time * 2f + fragment.GetCanvasPositionOfIndex(i).X * 2f)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._skullColor, this._bloodLight, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004265 RID: 16997 RVA: 0x005F687C File Offset: 0x005F4A7C
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
				Vector4 value = this._backgroundColor;
				float num = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 10f + time * 0.75f) % 10f + canvasPositionOfIndex.Y - 1f;
				if (num > 0f)
				{
					float amount = Math.Max(0f, 1.2f - num);
					if (num < 0.2f)
					{
						amount = num * 5f;
					}
					value = Vector4.Lerp(value, this._skullColor, amount);
				}
				float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.5f + new Vector2(12.5f, time * 0.2f));
				staticNoise = Math.Max(0f, 1f - staticNoise * staticNoise * 4f * staticNoise * (1f - canvasPositionOfIndex.Y * canvasPositionOfIndex.Y)) * canvasPositionOfIndex.Y * canvasPositionOfIndex.Y;
				staticNoise = MathHelper.Clamp(staticNoise, 0f, 1f);
				Vector4 value2 = Vector4.Lerp(this._bloodDark, this._bloodLight, staticNoise);
				value = Vector4.Lerp(value, value2, staticNoise);
				fragment.SetColor(i, value);
			}
		}

		// Token: 0x04005995 RID: 22933
		private readonly Vector4 _skullColor;

		// Token: 0x04005996 RID: 22934
		private readonly Vector4 _bloodDark;

		// Token: 0x04005997 RID: 22935
		private readonly Vector4 _bloodLight;

		// Token: 0x04005998 RID: 22936
		private readonly Vector4 _backgroundColor = Color.Black.ToVector4();
	}
}
