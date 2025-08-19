using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200059A RID: 1434
	public class PirateInvasionShader : ChromaShader
	{
		// Token: 0x06004252 RID: 16978 RVA: 0x005F5FEC File Offset: 0x005F41EC
		public PirateInvasionShader(Color cannonBallColor, Color splashColor, Color waterColor, Color backgroundColor)
		{
			this._cannonBallColor = cannonBallColor.ToVector4();
			this._splashColor = splashColor.ToVector4();
			this._waterColor = waterColor.ToVector4();
			this._backgroundColor = backgroundColor.ToVector4();
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x005F6028 File Offset: 0x005F4228
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time * 0.5f + fragment.GetCanvasPositionOfIndex(i).X)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._waterColor, this._cannonBallColor, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x005F608C File Offset: 0x005F428C
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				gridPositionOfIndex.X /= 2;
				float num4 = (NoiseHelper.GetStaticNoise(gridPositionOfIndex.X) * 40f + time * 1f) % 40f;
				float amount = 0f;
				float num2 = num4 - canvasPositionOfIndex.Y / 1.2f;
				if (num4 > 1f)
				{
					float num3 = 1f - canvasPositionOfIndex.Y / 1.2f;
					amount = (1f - Math.Min(1f, num2 - num3)) * (1f - Math.Min(1f, num3 / 1f));
				}
				Vector4 vector = this._backgroundColor;
				if (num2 > 0f)
				{
					float amount2 = Math.Max(0f, 1.2f - num2 * 4f);
					if (num2 < 0.1f)
					{
						amount2 = num2 / 0.1f;
					}
					vector = Vector4.Lerp(vector, this._cannonBallColor, amount2);
					vector = Vector4.Lerp(vector, this._splashColor, amount);
				}
				if (canvasPositionOfIndex.Y > 0.8f)
				{
					vector = this._waterColor;
				}
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x04005987 RID: 22919
		private readonly Vector4 _cannonBallColor;

		// Token: 0x04005988 RID: 22920
		private readonly Vector4 _splashColor;

		// Token: 0x04005989 RID: 22921
		private readonly Vector4 _waterColor;

		// Token: 0x0400598A RID: 22922
		private readonly Vector4 _backgroundColor;
	}
}
