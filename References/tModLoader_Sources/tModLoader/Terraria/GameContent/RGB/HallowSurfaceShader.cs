using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200058E RID: 1422
	public class HallowSurfaceShader : ChromaShader
	{
		// Token: 0x06004225 RID: 16933 RVA: 0x005F4A85 File Offset: 0x005F2C85
		public override void Update(float elapsedTime)
		{
			this._lightColor = Main.ColorOfTheSkies.ToVector4() * 0.75f + Vector4.One * 0.25f;
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x005F4AB8 File Offset: 0x005F2CB8
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time + fragment.GetCanvasPositionOfIndex(i).X)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._skyColor, this._groundColor, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x005F4B14 File Offset: 0x005F2D14
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 vector = this._skyColor * this._lightColor;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				float dynamicNoise = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 20f);
				dynamicNoise = Math.Max(0f, 1f - dynamicNoise * 5f);
				Vector4 value = vector;
				value = (((gridPositionOfIndex.X * 100 + gridPositionOfIndex.Y) % 2 != 0) ? Vector4.Lerp(value, this._pinkFlowerColor, dynamicNoise) : Vector4.Lerp(value, this._yellowFlowerColor, dynamicNoise));
				float num = (float)Math.Sin((double)canvasPositionOfIndex.X) * 0.3f + 0.7f;
				if (canvasPositionOfIndex.Y > num)
				{
					value = this._groundColor;
				}
				fragment.SetColor(i, value);
			}
		}

		// Token: 0x04005962 RID: 22882
		private readonly Vector4 _skyColor = new Color(150, 220, 220).ToVector4();

		// Token: 0x04005963 RID: 22883
		private readonly Vector4 _groundColor = new Vector4(1f, 0.2f, 0.25f, 1f);

		// Token: 0x04005964 RID: 22884
		private readonly Vector4 _pinkFlowerColor = new Vector4(1f, 0.2f, 0.25f, 1f);

		// Token: 0x04005965 RID: 22885
		private readonly Vector4 _yellowFlowerColor = new Vector4(1f, 1f, 0f, 1f);

		// Token: 0x04005966 RID: 22886
		private Vector4 _lightColor;
	}
}
