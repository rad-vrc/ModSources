using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020005A1 RID: 1441
	public class SkyShader : ChromaShader
	{
		// Token: 0x06004266 RID: 16998 RVA: 0x005F69C4 File Offset: 0x005F4BC4
		public SkyShader(Color skyColor, Color spaceColor)
		{
			this._baseSkyColor = skyColor.ToVector4();
			this._baseSpaceColor = spaceColor.ToVector4();
		}

		// Token: 0x06004267 RID: 16999 RVA: 0x005F69E8 File Offset: 0x005F4BE8
		public override void Update(float elapsedTime)
		{
			float num = Main.player[Main.myPlayer].position.Y / 16f;
			this._backgroundTransition = MathHelper.Clamp((num - (float)Main.worldSurface * 0.25f) / ((float)Main.worldSurface * 0.1f), 0f, 1f);
			this._processedSkyColor = this._baseSkyColor * (Main.ColorOfTheSkies.ToVector4() * 0.75f + Vector4.One * 0.25f);
			this._processedCloudColor = Main.ColorOfTheSkies.ToVector4() * 0.75f + Vector4.One * 0.25f;
			if (Main.dayTime)
			{
				float num2 = (float)(Main.time / 54000.0);
				if (num2 < 0.25f)
				{
					this._starVisibility = 1f - num2 / 0.25f;
				}
				else if (num2 > 0.75f)
				{
					this._starVisibility = (num2 - 0.75f) / 0.25f;
				}
				else
				{
					this._starVisibility = 0f;
				}
			}
			else
			{
				this._starVisibility = 1f;
			}
			this._starVisibility = Math.Max(1f - this._backgroundTransition, this._starVisibility);
		}

		// Token: 0x06004268 RID: 17000 RVA: 0x005F6B30 File Offset: 0x005F4D30
		[RgbProcessor(new EffectDetailLevel[]
		{
			0,
			1
		})]
		private void ProcessAnyDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				float dynamicNoise = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * new Vector2(0.1f, 0.5f) + new Vector2(time * 0.05f, 0f), time / 20f);
				dynamicNoise = (float)Math.Sqrt((double)Math.Max(0f, 1f - 2f * dynamicNoise));
				Vector4 value = Vector4.Lerp(this._processedSkyColor, this._processedCloudColor, dynamicNoise);
				value = Vector4.Lerp(this._baseSpaceColor, value, this._backgroundTransition);
				float dynamicNoise2 = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 60f);
				dynamicNoise2 = Math.Max(0f, 1f - dynamicNoise2 * 20f);
				value = Vector4.Lerp(value, Vector4.One, dynamicNoise2 * 0.98f * this._starVisibility);
				fragment.SetColor(i, value);
			}
		}

		// Token: 0x04005999 RID: 22937
		private readonly Vector4 _baseSkyColor;

		// Token: 0x0400599A RID: 22938
		private readonly Vector4 _baseSpaceColor;

		// Token: 0x0400599B RID: 22939
		private Vector4 _processedSkyColor;

		// Token: 0x0400599C RID: 22940
		private Vector4 _processedCloudColor;

		// Token: 0x0400599D RID: 22941
		private float _backgroundTransition;

		// Token: 0x0400599E RID: 22942
		private float _starVisibility;
	}
}
