using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200058D RID: 1421
	public class GolemShader : ChromaShader
	{
		// Token: 0x06004222 RID: 16930 RVA: 0x005F4893 File Offset: 0x005F2A93
		public GolemShader(Color glowColor, Color coreColor, Color backgroundColor)
		{
			this._glowColor = glowColor.ToVector4();
			this._coreColor = coreColor.ToVector4();
			this._backgroundColor = backgroundColor.ToVector4();
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x005F48C4 File Offset: 0x005F2AC4
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 value = Vector4.Lerp(this._backgroundColor, this._coreColor, Math.Max(0f, (float)Math.Sin((double)(time * 0.5f))));
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = Math.Max(0f, (float)Math.Sin((double)(fragment.GetCanvasPositionOfIndex(i).X * 2f + time + 101f)));
				Vector4 color = Vector4.Lerp(value, this._glowColor, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x005F4950 File Offset: 0x005F2B50
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			float num = 0.5f + (float)Math.Sin((double)(time * 3f)) * 0.1f;
			Vector2 vector;
			vector..ctor(1.6f, 0.5f);
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector4 vector2 = this._backgroundColor;
				float num2 = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.Y) * 10f + time * 2f) % 10f - Math.Abs(canvasPositionOfIndex.X - vector.X);
				if (num2 > 0f)
				{
					float amount = Math.Max(0f, 1.2f - num2);
					if (num2 < 0.2f)
					{
						amount = num2 * 5f;
					}
					vector2 = Vector4.Lerp(vector2, this._glowColor, amount);
				}
				float num3 = (canvasPositionOfIndex - vector).Length();
				if (num3 < num)
				{
					float amount2 = 1f - MathHelper.Clamp((num3 - num + 0.1f) / 0.1f, 0f, 1f);
					vector2 = Vector4.Lerp(vector2, this._coreColor, amount2);
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x0400595F RID: 22879
		private readonly Vector4 _glowColor;

		// Token: 0x04005960 RID: 22880
		private readonly Vector4 _coreColor;

		// Token: 0x04005961 RID: 22881
		private readonly Vector4 _backgroundColor;
	}
}
