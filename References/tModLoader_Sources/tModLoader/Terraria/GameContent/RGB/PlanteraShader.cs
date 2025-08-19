using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200059B RID: 1435
	public class PlanteraShader : ChromaShader
	{
		// Token: 0x06004255 RID: 16981 RVA: 0x005F61C9 File Offset: 0x005F43C9
		public PlanteraShader(Color bulbColor, Color vineColor, Color backgroundColor)
		{
			this._bulbColor = bulbColor.ToVector4();
			this._vineColor = vineColor.ToVector4();
			this._backgroundColor = backgroundColor.ToVector4();
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x005F61F8 File Offset: 0x005F43F8
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = (float)Math.Sin((double)(time * 2f + fragment.GetCanvasPositionOfIndex(i).X * 10f)) * 0.5f + 0.5f;
				Vector4 color = Vector4.Lerp(this._bulbColor, this._vineColor, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x005F6260 File Offset: 0x005F4460
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
				canvasPositionOfIndex.X -= 1.8f;
				if (canvasPositionOfIndex.X < 0f)
				{
					canvasPositionOfIndex.X *= -1f;
					gridPositionOfIndex.Y += 101;
				}
				float staticNoise = NoiseHelper.GetStaticNoise(gridPositionOfIndex.Y);
				staticNoise = (staticNoise * 5f + time * 0.4f) % 5f;
				float num = 1f;
				if (staticNoise > 1f)
				{
					num = 1f - MathHelper.Clamp((staticNoise - 0.4f - 1f) / 0.4f, 0f, 1f);
					staticNoise = 1f;
				}
				float num2 = staticNoise - canvasPositionOfIndex.X / 5f;
				Vector4 color = this._backgroundColor;
				if (num2 > 0f)
				{
					float num3 = 1f;
					if (num2 < 0.2f)
					{
						num3 = num2 / 0.2f;
					}
					color = (((gridPositionOfIndex.X + 7 * gridPositionOfIndex.Y) % 5 != 0) ? Vector4.Lerp(this._backgroundColor, this._vineColor, num3 * num) : Vector4.Lerp(this._backgroundColor, this._bulbColor, num3 * num));
				}
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0400598B RID: 22923
		private readonly Vector4 _bulbColor;

		// Token: 0x0400598C RID: 22924
		private readonly Vector4 _vineColor;

		// Token: 0x0400598D RID: 22925
		private readonly Vector4 _backgroundColor;
	}
}
