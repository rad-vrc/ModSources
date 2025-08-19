using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200022F RID: 559
	public class FrostLegionShader : ChromaShader
	{
		// Token: 0x06001EEC RID: 7916 RVA: 0x0050EF4D File Offset: 0x0050D14D
		public FrostLegionShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x0050EF70 File Offset: 0x0050D170
		[RgbProcessor(new EffectDetailLevel[]
		{
			1,
			0
		})]
		private void ProcessHighDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector2 canvasPositionOfIndex = fragment.GetCanvasPositionOfIndex(i);
				float staticNoise = NoiseHelper.GetStaticNoise(fragment.GetGridPositionOfIndex(i).X / 2);
				float num = (canvasPositionOfIndex.Y + canvasPositionOfIndex.X / 2f - staticNoise + time) % 2f;
				if (num < 0f)
				{
					num += 2f;
				}
				if (num < 0.2f)
				{
					num = 1f - num / 0.2f;
				}
				float amount = num / 2f;
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, amount);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x040045F7 RID: 17911
		private readonly Vector4 _primaryColor;

		// Token: 0x040045F8 RID: 17912
		private readonly Vector4 _secondaryColor;
	}
}
