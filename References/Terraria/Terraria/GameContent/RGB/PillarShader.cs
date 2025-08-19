using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000236 RID: 566
	public class PillarShader : ChromaShader
	{
		// Token: 0x06001EFF RID: 7935 RVA: 0x0050FBF4 File Offset: 0x0050DDF4
		public PillarShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x0050FC18 File Offset: 0x0050DE18
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, (float)Math.Sin((double)(time * 2.5f + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x0050FC7C File Offset: 0x0050DE7C
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector2 value = new Vector2(1.5f, 0.5f);
			time *= 4f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 vector = fragment.GetCanvasPositionOfIndex(i) - value;
				float num = vector.Length() * 2f;
				float num2 = (float)Math.Atan2((double)vector.Y, (double)vector.X);
				float amount = (float)Math.Sin((double)(num * 4f - time - num2)) * 0.5f + 0.5f;
				Vector4 vector2 = Vector4.Lerp(this._primaryColor, this._secondaryColor, amount);
				if (num < 1f)
				{
					float num3 = num / 1f;
					num3 *= num3 * num3;
					float amount2 = (float)Math.Sin((double)(4f - time - num2)) * 0.5f + 0.5f;
					vector2 = Vector4.Lerp(this._primaryColor, this._secondaryColor, amount2) * num3;
				}
				vector2.W = 1f;
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x0400460D RID: 17933
		private readonly Vector4 _primaryColor;

		// Token: 0x0400460E RID: 17934
		private readonly Vector4 _secondaryColor;
	}
}
