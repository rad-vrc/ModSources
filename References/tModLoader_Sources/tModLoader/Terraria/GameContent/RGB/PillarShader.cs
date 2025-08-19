using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000599 RID: 1433
	public class PillarShader : ChromaShader
	{
		// Token: 0x0600424F RID: 16975 RVA: 0x005F5E51 File Offset: 0x005F4051
		public PillarShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x005F5E74 File Offset: 0x005F4074
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time * 2.5f + fragment.GetCanvasPositionOfIndex(i).X)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._primaryColor, this._secondaryColor, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x005F5ED8 File Offset: 0x005F40D8
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector2 vector;
			vector..ctor(1.5f, 0.5f);
			time *= 4f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 vector2 = fragment.GetCanvasPositionOfIndex(i) - vector;
				float num = vector2.Length() * 2f;
				float num2 = (float)Math.Atan2((double)vector2.Y, (double)vector2.X);
				float amount = (float)Math.Sin((double)(num * 4f - time - num2)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._primaryColor, this._secondaryColor, amount);
				if (num < 1f)
				{
					float num3 = num / 1f;
					num3 *= num3 * num3;
					float amount2 = (float)Math.Sin((double)(4f - time - num2)) * 0.5f + 0.5f;
					color = Vector4.Lerp(this._primaryColor, this._secondaryColor, amount2) * num3;
				}
				color.W = 1f;
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x04005985 RID: 22917
		private readonly Vector4 _primaryColor;

		// Token: 0x04005986 RID: 22918
		private readonly Vector4 _secondaryColor;
	}
}
