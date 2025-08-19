using System;
using Microsoft.Xna.Framework;
using ReLogic.Peripherals.RGB;

namespace Terraria.GameContent.RGB
{
	// Token: 0x02000580 RID: 1408
	public class DeathShader : ChromaShader
	{
		// Token: 0x060041FB RID: 16891 RVA: 0x005F32B3 File Offset: 0x005F14B3
		public DeathShader(Color primaryColor, Color secondaryColor)
		{
			this._primaryColor = primaryColor.ToVector4();
			this._secondaryColor = secondaryColor.ToVector4();
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x005F32D8 File Offset: 0x005F14D8
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
				Vector4 color = Vector4.Lerp(this._primaryColor, this._secondaryColor, amount);
				fragment.SetColor(i, color);
			}
		}

		// Token: 0x0400593A RID: 22842
		private readonly Vector4 _primaryColor;

		// Token: 0x0400593B RID: 22843
		private readonly Vector4 _secondaryColor;
	}
}
