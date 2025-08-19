using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000251 RID: 593
	public class SurfaceBiomeShader : ChromaShader
	{
		// Token: 0x06001F55 RID: 8021 RVA: 0x005126F8 File Offset: 0x005108F8
		public SurfaceBiomeShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x0051271C File Offset: 0x0051091C
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

		// Token: 0x06001F57 RID: 8023 RVA: 0x005127B4 File Offset: 0x005109B4
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
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(value, value2, (float)Math.Sin((double)(time * 0.5f + canvasPositionOfIndex.X)) * 0.5f + 0.5f);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00512834 File Offset: 0x00510A34
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
				Vector4 vector = Vector4.Lerp(value, value2, amount);
				float num = NoiseHelper.GetDynamicNoise(gridPositionOfIndex.X, gridPositionOfIndex.Y, time / 60f);
				num = Math.Max(0f, 1f - num * 20f);
				num *= 1f - this._surfaceColor.X;
				vector = Vector4.Max(vector, new Vector4(num * this._starVisibility));
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x0400465F RID: 18015
		private readonly Vector4 _primaryColor;

		// Token: 0x04004660 RID: 18016
		private readonly Vector4 _secondaryColor;

		// Token: 0x04004661 RID: 18017
		private Vector4 _surfaceColor;

		// Token: 0x04004662 RID: 18018
		private float _starVisibility;
	}
}
