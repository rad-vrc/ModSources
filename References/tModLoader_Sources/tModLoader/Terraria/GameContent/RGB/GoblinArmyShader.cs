using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200058C RID: 1420
	public class GoblinArmyShader : ChromaShader
	{
		// Token: 0x0600421F RID: 16927 RVA: 0x005F46A1 File Offset: 0x005F28A1
		public GoblinArmyShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x005F46C4 File Offset: 0x005F28C4
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			time *= 0.5f;
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				canvasPositionOfIndex.Y = 1f;
				float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.2f));
				staticNoise = Math.Max(0f, 1f - staticNoise * staticNoise * 4f * staticNoise);
				staticNoise = MathHelper.Clamp(staticNoise, 0f, 1f);
				Vector4 value = Vector4.Lerp(this._primaryColor, this._secondaryColor, staticNoise);
				value = Vector4.Lerp(value, Vector4.One, staticNoise * staticNoise);
				Vector4 color = Vector4.Lerp(new Vector4(0f, 0f, 0f, 1f), value, staticNoise);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x005F47A8 File Offset: 0x005F29A8
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float staticNoise = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.2f));
				staticNoise = Math.Max(0f, 1f - staticNoise * staticNoise * 4f * staticNoise * (1.2f - canvasPositionOfIndex.Y)) * canvasPositionOfIndex.Y * canvasPositionOfIndex.Y;
				staticNoise = MathHelper.Clamp(staticNoise, 0f, 1f);
				Vector4 value = Vector4.Lerp(this._primaryColor, this._secondaryColor, staticNoise);
				value = Vector4.Lerp(value, Vector4.One, staticNoise * staticNoise * staticNoise);
				Vector4 color = Vector4.Lerp(new Vector4(0f, 0f, 0f, 1f), value, staticNoise);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0400595D RID: 22877
		private readonly Vector4 _primaryColor;

		// Token: 0x0400595E RID: 22878
		private readonly Vector4 _secondaryColor;
	}
}
