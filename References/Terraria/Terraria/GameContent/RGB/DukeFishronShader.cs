using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200022C RID: 556
	public class DukeFishronShader : ChromaShader
	{
		// Token: 0x06001EE2 RID: 7906 RVA: 0x0050E873 File Offset: 0x0050CA73
		public DukeFishronShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x0050E898 File Offset: 0x0050CA98
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, Math.Max(0f, (float)Math.Sin((double)(time * 2f + canvasPositionOfIndex.X))));
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0050E8F8 File Offset: 0x0050CAF8
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				ref Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float dynamicNoise = NoiseHelper.GetDynamicNoise(fragment.GetGridPositionOfIndex(i).Y, time);
				float num = (float)Math.Sin((double)(canvasPositionOfIndex.X + 2f * time + dynamicNoise)) - 0.2f;
				num = Math.Max(0f, num);
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, num);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x040045F1 RID: 17905
		private readonly Vector4 _primaryColor;

		// Token: 0x040045F2 RID: 17906
		private readonly Vector4 _secondaryColor;
	}
}
