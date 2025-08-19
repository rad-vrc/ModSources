using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000231 RID: 561
	public class GoblinArmyShader : ChromaShader
	{
		// Token: 0x06001EF0 RID: 7920 RVA: 0x0050F1BD File Offset: 0x0050D3BD
		public GoblinArmyShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0050F1E0 File Offset: 0x0050D3E0
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
				float num = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.2f));
				num = Math.Max(0f, 1f - num * num * 4f * num);
				num = MathHelper.Clamp(num, 0f, 1f);
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, num);
				vector = Vector4.Lerp(vector, Vector4.One, num * num);
				Vector4 vector2 = Vector4.Lerp(new Vector4(0f, 0f, 0f, 1f), vector, num);
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0050F2C4 File Offset: 0x0050D4C4
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float num = NoiseHelper.GetStaticNoise(canvasPositionOfIndex * 0.3f + new Vector2(12.5f, time * 0.2f));
				num = Math.Max(0f, 1f - num * num * 4f * num * (1.2f - canvasPositionOfIndex.Y)) * canvasPositionOfIndex.Y * canvasPositionOfIndex.Y;
				num = MathHelper.Clamp(num, 0f, 1f);
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, num);
				vector = Vector4.Lerp(vector, Vector4.One, num * num * num);
				Vector4 vector2 = Vector4.Lerp(new Vector4(0f, 0f, 0f, 1f), vector, num);
				fragment.SetColor(i, vector2);
			}
		}

		// Token: 0x040045FF RID: 17919
		private readonly Vector4 _primaryColor;

		// Token: 0x04004600 RID: 17920
		private readonly Vector4 _secondaryColor;
	}
}
