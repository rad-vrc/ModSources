using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000247 RID: 583
	public class HallowSurfaceShader : ChromaShader
	{
		// Token: 0x06001F35 RID: 7989 RVA: 0x00511933 File Offset: 0x0050FB33
		public override void Update(float elapsedTime)
		{
			this._lightColor = Main.ColorOfTheSkies.ToVector4() * 0.75f + Vector4.One * 0.25f;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00511964 File Offset: 0x0050FB64
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._skyColor, this._groundColor, (float)Math.Sin((double)(time + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x005119C0 File Offset: 0x0050FBC0
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
				float num = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 20f);
				num = Math.Max(0f, 1f - num * 5f);
				Vector4 vector2 = vector;
				if ((gridPositionOfIndex.X * 100 + gridPositionOfIndex.Y) % 2 == 0)
				{
					vector2 = Vector4.Lerp(vector2, this._yellowFlowerColor, num);
				}
				else
				{
					vector2 = Vector4.Lerp(vector2, this._pinkFlowerColor, num);
				}
				float num2 = (float)Math.Sin((double)canvasPositionOfIndex.X) * 0.3f + 0.7f;
				if (canvasPositionOfIndex.Y > num2)
				{
					vector2 = this._groundColor;
				}
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x04004646 RID: 17990
		private readonly Vector4 _skyColor = new Color(150, 220, 220).ToVector4();

		// Token: 0x04004647 RID: 17991
		private readonly Vector4 _groundColor = new Vector4(1f, 0.2f, 0.25f, 1f);

		// Token: 0x04004648 RID: 17992
		private readonly Vector4 _pinkFlowerColor = new Vector4(1f, 0.2f, 0.25f, 1f);

		// Token: 0x04004649 RID: 17993
		private readonly Vector4 _yellowFlowerColor = new Vector4(1f, 1f, 0f, 1f);

		// Token: 0x0400464A RID: 17994
		private Vector4 _lightColor;
	}
}
