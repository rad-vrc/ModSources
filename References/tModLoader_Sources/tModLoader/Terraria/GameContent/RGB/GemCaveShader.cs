using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200058B RID: 1419
	public class GemCaveShader : ChromaShader
	{
		// Token: 0x0600421D RID: 16925 RVA: 0x005F4500 File Offset: 0x005F2700
		public GemCaveShader(Color primaryColor, Color secondaryColor, Vector4[] gemColors)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
			this._gemColors = gemColors;
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x005F452C File Offset: 0x005F272C
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
				float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.5f + new Vector2(0f, time * 0.5f));
				Vector4 value2 = vector;
				staticNoise += num;
				if (staticNoise > 0.999f)
				{
					float amount = MathHelper.Clamp((staticNoise - 0.999f) / 0.2f, 0f, 1f);
					value2 = Vector4.Lerp(value2, value, amount);
				}
				float dynamicNoise = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / this.CycleTime);
				dynamicNoise = Math.Max(0f, 1f - dynamicNoise * this.ColorRarity);
				value2 = Vector4.Lerp(value2, this._gemColors[((gridPositionOfIndex.Y * 47 + gridPositionOfIndex.X) % this._gemColors.Length + this._gemColors.Length) % this._gemColors.Length], dynamicNoise);
				fragment.SetColor(i, value2);
				fragment.SetColor(i, value2);
			}
		}

		// Token: 0x04005957 RID: 22871
		private readonly Vector4 _primaryColor;

		// Token: 0x04005958 RID: 22872
		private readonly Vector4 _secondaryColor;

		// Token: 0x04005959 RID: 22873
		private readonly Vector4[] _gemColors;

		// Token: 0x0400595A RID: 22874
		public float CycleTime;

		// Token: 0x0400595B RID: 22875
		public float ColorRarity;

		// Token: 0x0400595C RID: 22876
		public float TimeRate;
	}
}
