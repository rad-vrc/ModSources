using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200057D RID: 1405
	public class CorruptSurfaceShader : ChromaShader
	{
		// Token: 0x060041F2 RID: 16882 RVA: 0x005F2C4C File Offset: 0x005F0E4C
		public CorruptSurfaceShader(Color color)
		{
			this._baseColor = color.ToVector4();
			this._skyColor = Vector4.Lerp(this._baseColor, Color.DeepSkyBlue.ToVector4(), 0.5f);
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x005F2C8F File Offset: 0x005F0E8F
		public CorruptSurfaceShader(Color vineColor, Color skyColor)
		{
			this._baseColor = vineColor.ToVector4();
			this._skyColor = skyColor.ToVector4();
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x005F2CB1 File Offset: 0x005F0EB1
		public override void Update(float elapsedTime)
		{
			this._lightColor = Main.ColorOfTheSkies.ToVector4() * 0.75f + Vector4.One * 0.25f;
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x005F2CE4 File Offset: 0x005F0EE4
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 value = this._skyColor * this._lightColor;
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time * 0.5f + fragment.GetCanvasPositionOfIndex(i).X)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._baseColor, value, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x005F2D54 File Offset: 0x005F0F54
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector = this._skyColor * this._lightColor;
			for (int i = 0; i < fragment.Count; i++)
			{
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float staticNoise = NoiseHelper.GetStaticNoise(gridPositionOfIndex.X);
				staticNoise = (staticNoise * 10f + time * 0.4f) % 10f;
				float num = 1f;
				if (staticNoise > 1f)
				{
					num = MathHelper.Clamp(1f - (staticNoise - 1.4f), 0f, 1f);
					staticNoise = 1f;
				}
				float num2 = (float)Math.Sin((double)canvasPositionOfIndex.X) * 0.3f + 0.7f;
				float num3 = staticNoise - (1f - canvasPositionOfIndex.Y);
				Vector4 vector2 = vector;
				if (num3 > 0f)
				{
					float num4 = 1f;
					if (num3 < 0.2f)
					{
						num4 = num3 * 5f;
					}
					vector2 = Vector4.Lerp(vector2, this._baseColor, num4 * num);
				}
				if (canvasPositionOfIndex.Y > num2)
				{
					vector2 = this._baseColor;
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x0400592E RID: 22830
		private readonly Vector4 _baseColor;

		// Token: 0x0400592F RID: 22831
		private readonly Vector4 _skyColor;

		// Token: 0x04005930 RID: 22832
		private Vector4 _lightColor;
	}
}
