using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x020005A3 RID: 1443
	public class SurfaceBiomeShader : ChromaShader
	{
		// Token: 0x0600426C RID: 17004 RVA: 0x005F6D84 File Offset: 0x005F4F84
		public SurfaceBiomeShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x0600426D RID: 17005 RVA: 0x005F6DA8 File Offset: 0x005F4FA8
		public override void Update(float elapsedTime)
		{
			this._surfaceColor = Main.ColorOfTheSkies.ToVector4() * 0.75f + Vector4.One * 0.25f;
			if (Main.dayTime)
			{
				float num = (float)(Main.time / 54000.0);
				if (num < 0.25f)
				{
					this._starVisibility = 1f - num / 0.25f;
					return;
				}
				if (num > 0.75f)
				{
					this._starVisibility = (num - 0.75f) / 0.25f;
					return;
				}
			}
			else
			{
				this._starVisibility = 1f;
			}
		}

		// Token: 0x0600426E RID: 17006 RVA: 0x005F6E40 File Offset: 0x005F5040
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 value = this._primaryColor * this._surfaceColor;
			Vector4 value2 = this._secondaryColor * this._surfaceColor;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector4 color = Vector4.Lerp(value, value2, (float)Math.Sin((double)(time * 0.5f + fragment.GetCanvasPositionOfIndex(i).X)) * 0.5f + 0.5f);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0600426F RID: 17007 RVA: 0x005F6EBC File Offset: 0x005F50BC
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			Vector4 value = this._primaryColor * this._surfaceColor;
			Vector4 value2 = this._secondaryColor * this._surfaceColor;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Point gridPositionOfIndex = fragment.GetGridPositionOfIndex(i);
				float amount = (float)Math.Sin((double)(canvasPositionOfIndex.X * 1.5f + canvasPositionOfIndex.Y + time)) * 0.5f + 0.5f;
				Vector4 value3 = Vector4.Lerp(value, value2, amount);
				float dynamicNoise = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 60f);
				dynamicNoise = Math.Max(0f, 1f - dynamicNoise * 20f);
				dynamicNoise *= 1f - this._surfaceColor.X;
				value3 = Vector4.Max(value3, new Vector4(dynamicNoise * this._starVisibility));
				fragment.SetColor(i, value3);
			}
		}

		// Token: 0x040059A0 RID: 22944
		private readonly Vector4 _primaryColor;

		// Token: 0x040059A1 RID: 22945
		private readonly Vector4 _secondaryColor;

		// Token: 0x040059A2 RID: 22946
		private Vector4 _surfaceColor;

		// Token: 0x040059A3 RID: 22947
		private float _starVisibility;
	}
}
