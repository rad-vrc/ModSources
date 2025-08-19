using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x0200022B RID: 555
	public class DeathShader : ChromaShader
	{
		// Token: 0x06001EE0 RID: 7904 RVA: 0x0050E7EE File Offset: 0x0050C9EE
		public DeathShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x0050E810 File Offset: 0x0050CA10
		[RgbProcessor(new EffectDetailLevel[]
		{
			1,
			0
		})]
		private void ProcessLowDetail(RgbDevice device, Fragment fragment, EffectDetailLevel quality, float time)
		{
			time *= 3f;
			float amount = 0f;
			float num = time % 12.566371f;
			if (num < 3.1415927f)
			{
				amount = (float)Math.Sin((double)num);
			}
			for (int i = 0; i < fragment.Count; i++)
			{
				Vector4 vector = Vector4.Lerp(this._primaryColor, this._secondaryColor, amount);
				fragment.SetColor(i, vector);
			}
		}

		// Token: 0x040045EF RID: 17903
		private readonly Vector4 _primaryColor;

		// Token: 0x040045F0 RID: 17904
		private readonly Vector4 _secondaryColor;
	}
}
