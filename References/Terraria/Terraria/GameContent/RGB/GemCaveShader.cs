using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000230 RID: 560
	public class GemCaveShader : ChromaShader
	{
		// Token: 0x06001EEE RID: 7918 RVA: 0x0050F01C File Offset: 0x0050D21C
		public GemCaveShader(Color primaryColor, Color secondaryColor, Vector4[] gemColors)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
			this._gemColors = gemColors;
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x0050F048 File Offset: 0x0050D248
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			time *= this.TimeRate;
			float num = time % 1f;
			bool flag = time % 2f > 1f;
			Vector4 vector = flag ? this._secondaryColor : this._primaryColor;
			Vector4 value = flag ? this._primaryColor : this._secondaryColor;
			num *= 1.2f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				float num2 = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.5f + new Vector2(0f, time * 0.5f));
				Vector4 vector2 = vector;
				num2 += num;
				if (num2 > 0.999f)
				{
					float amount = MathHelper.Clamp((num2 - 0.999f) / 0.2f, 0f, 1f);
					vector2 = Vector4.Lerp(vector2, value, amount);
				}
				float num3 = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / this.CycleTime);
				num3 = Math.Max(0f, 1f - num3 * this.ColorRarity);
				vector2 = Vector4.Lerp(vector2, this._gemColors[((gridPositionOfIndex.Y * 47 + gridPositionOfIndex.X) % this._gemColors.Length + this._gemColors.Length) % this._gemColors.Length], num3);
				fragment.SetColor(i, vector2);
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x040045F9 RID: 17913
		private readonly Vector4 _primaryColor;

		// Token: 0x040045FA RID: 17914
		private readonly Vector4 _secondaryColor;

		// Token: 0x040045FB RID: 17915
		private readonly Vector4[] _gemColors;

		// Token: 0x040045FC RID: 17916
		public float CycleTime;

		// Token: 0x040045FD RID: 17917
		public float ColorRarity;

		// Token: 0x040045FE RID: 17918
		public float TimeRate;
	}
}
