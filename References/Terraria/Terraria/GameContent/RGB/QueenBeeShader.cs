using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000239 RID: 569
	public class QueenBeeShader : ChromaShader
	{
		// Token: 0x06001F08 RID: 7944 RVA: 0x0051015D File Offset: 0x0050E35D
		public QueenBeeShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x00510180 File Offset: 0x0050E380
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, (float)Math.Sin((double)(time * 2f + canvasPositionOfIndex.X * 10f)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x005101E8 File Offset: 0x0050E3E8
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
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, amount);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04004616 RID: 17942
		private readonly Vector4 _primaryColor;

		// Token: 0x04004617 RID: 17943
		private readonly Vector4 _secondaryColor;
	}
}
