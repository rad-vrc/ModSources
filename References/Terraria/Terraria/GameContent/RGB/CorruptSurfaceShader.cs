using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000242 RID: 578
	public class CorruptSurfaceShader : ChromaShader
	{
		// Token: 0x06001F23 RID: 7971 RVA: 0x00510F60 File Offset: 0x0050F160
		public CorruptSurfaceShader(Color color)
		{
			this._baseColor = color.ToVector4();
			this._skyColor = Vector4.Lerp(this._baseColor, Color.DeepSkyBlue.ToVector4(), 0.5f);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00510FA3 File Offset: 0x0050F1A3
		public CorruptSurfaceShader(Color vineColor, Color skyColor)
		{
			this._baseColor = vineColor.ToVector4();
			this._skyColor = skyColor.ToVector4();
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00510FC5 File Offset: 0x0050F1C5
		public override void Update(float elapsedTime)
		{
			this._lightColor = Main.ColorOfTheSkies.ToVector4() * 0.75f + Vector4.One * 0.25f;
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00510FF8 File Offset: 0x0050F1F8
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 value = this._skyColor * this._lightColor;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._baseColor, value, (float)Math.Sin((double)(time * 0.5f + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x00511068 File Offset: 0x0050F268
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector = this._skyColor * this._lightColor;
			for (int i = 0; i < fragment.Count; i++)
			{
				ref Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = NoiseHelper.GetStaticNoise(gridPositionOfIndex.X);
				num = (num * 10f + time * 0.4f) % 10f;
				float num2 = 1f;
				if (num > 1f)
				{
					num2 = MathHelper.Clamp(1f - (num - 1.4f), 0f, 1f);
					num = 1f;
				}
				float num3 = (float)Math.Sin((double)canvasPositionOfIndex.X) * 0.3f + 0.7f;
				float num4 = num - (1f - canvasPositionOfIndex.Y);
				Vector4 vector2 = vector;
				if (num4 > 0f)
				{
					float num5 = 1f;
					if (num4 < 0.2f)
					{
						num5 = num4 * 5f;
					}
					vector2 = Vector4.Lerp(vector2, this._baseColor, num5 * num2);
				}
				if (canvasPositionOfIndex.Y > num3)
				{
					vector2 = this._baseColor;
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x04004634 RID: 17972
		private readonly Vector4 _baseColor;

		// Token: 0x04004635 RID: 17973
		private readonly Vector4 _skyColor;

		// Token: 0x04004636 RID: 17974
		private Vector4 _lightColor;
	}
}
