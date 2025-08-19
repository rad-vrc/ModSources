using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200059C RID: 1436
	public class QueenBeeShader : ChromaShader
	{
		// Token: 0x06004258 RID: 16984 RVA: 0x005F63B7 File Offset: 0x005F45B7
		public QueenBeeShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06004259 RID: 16985 RVA: 0x005F63DC File Offset: 0x005F45DC
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time * 2f + fragment.GetCanvasPositionOfIndex(i).X * 10f)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._primaryColor, this._secondaryColor, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x005F6444 File Offset: 0x005F4644
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			time *= 0.5f;
			for (int i = 0; i < fragment.Count; i++)
			{
				float amount = MathHelper.Clamp((float)Math.Sin((double)fragment.GetCanvasPositionOfIndex(i).X * 5.0 - (double)(4f * time)) * 1.5f, 0f, 1f);
				Vector4 color = Vector4.Lerp(this._primaryColor, this._secondaryColor, amount);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0400598E RID: 22926
		private readonly Vector4 _primaryColor;

		// Token: 0x0400598F RID: 22927
		private readonly Vector4 _secondaryColor;
	}
}
