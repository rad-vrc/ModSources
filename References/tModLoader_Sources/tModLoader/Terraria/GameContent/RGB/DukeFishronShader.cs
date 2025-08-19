using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000585 RID: 1413
	public class DukeFishronShader : ChromaShader
	{
		// Token: 0x0600420A RID: 16906 RVA: 0x005F377E File Offset: 0x005F197E
		public DukeFishronShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x005F37A0 File Offset: 0x005F19A0
		[RgbProcessor(new EffectDetailLevel[]
		{
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				float num = Math.Max(0f, (float)Math.Sin((double)(time * 2f + fragment.GetCanvasPositionOfIndex(i).X)));
				Vector4 color = Vector4.Lerp(this._primaryColor, this._secondaryColor, num);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x005F3800 File Offset: 0x005F1A00
		[RgbProcessor(new EffectDetailLevel[]
		{
			1
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float dynamicNoise = NoiseHelper.GetDynamicNoise(fragment.GetGridPositionOfIndex(i).Y, time);
				float val = (float)Math.Sin((double)(canvasPositionOfIndex.X + 2f * time + dynamicNoise)) - 0.2f;
				val = Math.Max(0f, val);
				Vector4 color = Vector4.Lerp(this._primaryColor, this._secondaryColor, val);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x04005942 RID: 22850
		private readonly Vector4 _primaryColor;

		// Token: 0x04005943 RID: 22851
		private readonly Vector4 _secondaryColor;
	}
}
