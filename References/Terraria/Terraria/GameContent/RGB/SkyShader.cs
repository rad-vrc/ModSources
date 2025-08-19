using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000255 RID: 597
	public class SkyShader : ChromaShader
	{
		// Token: 0x06001F67 RID: 8039 RVA: 0x00512D51 File Offset: 0x00510F51
		public SkyShader(Color skyColor, Color spaceColor)
		{
			this._baseSkyColor = skyColor.ToVector4();
			this._baseSpaceColor = spaceColor.ToVector4();
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x00512D74 File Offset: 0x00510F74
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

		// Token: 0x06001F69 RID: 8041 RVA: 0x00512EBC File Offset: 0x005110BC
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
				float num = NoiseHelper.GetDynamicNoise(canvasPositionOfIndex * new Vector2(0.1f, 0.5f) + new Vector2(time * 0.05f, 0f), time / 20f);
				num = (float)Math.Sqrt((double)Math.Max(0f, 1f - 2f * num));
				Vector4 vector = Vector4.Lerp(this._processedSkyColor, this._processedCloudColor, num);
				vector = Vector4.Lerp(this._baseSpaceColor, vector, this._backgroundTransition);
				float num2 = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 60f);
				num2 = Math.Max(0f, 1f - num2 * 20f);
				vector = Vector4.Lerp(vector, Vector4.One, num2 * 0.98f * this._starVisibility);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0400466B RID: 18027
		private readonly Vector4 _baseSkyColor;

		// Token: 0x0400466C RID: 18028
		private readonly Vector4 _baseSpaceColor;

		// Token: 0x0400466D RID: 18029
		private Vector4 _processedSkyColor;

		// Token: 0x0400466E RID: 18030
		private Vector4 _processedCloudColor;

		// Token: 0x0400466F RID: 18031
		private float _backgroundTransition;

		// Token: 0x04004670 RID: 18032
		private float _starVisibility;
	}
}
