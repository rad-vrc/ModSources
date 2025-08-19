using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x020004C0 RID: 1216
	public class TreePaintingSettings
	{
		// Token: 0x06003A4A RID: 14922 RVA: 0x005A6E58 File Offset: 0x005A5058
		public void ApplyShader(int paintColor, Effect shader)
		{
			EffectParameter effectParameter = shader.Parameters["leafHueTestOffset"];
			if (effectParameter != null)
			{
				effectParameter.SetValue(this.HueTestOffset);
			}
			EffectParameter effectParameter2 = shader.Parameters["leafMinHue"];
			if (effectParameter2 != null)
			{
				effectParameter2.SetValue(this.SpecialGroupMinimalHueValue);
			}
			EffectParameter effectParameter3 = shader.Parameters["leafMaxHue"];
			if (effectParameter3 != null)
			{
				effectParameter3.SetValue(this.SpecialGroupMaximumHueValue);
			}
			EffectParameter effectParameter4 = shader.Parameters["leafMinSat"];
			if (effectParameter4 != null)
			{
				effectParameter4.SetValue(this.SpecialGroupMinimumSaturationValue);
			}
			EffectParameter effectParameter5 = shader.Parameters["leafMaxSat"];
			if (effectParameter5 != null)
			{
				effectParameter5.SetValue(this.SpecialGroupMaximumSaturationValue);
			}
			EffectParameter effectParameter6 = shader.Parameters["invertSpecialGroupResult"];
			if (effectParameter6 != null)
			{
				effectParameter6.SetValue(this.InvertSpecialGroupResult);
			}
			int index = Main.ConvertPaintIdToTileShaderIndex(paintColor, this.UseSpecialGroups, this.UseWallShaderHacks);
			shader.CurrentTechnique.Passes[index].Apply();
		}

		// Token: 0x040053D7 RID: 21463
		public float SpecialGroupMinimalHueValue;

		// Token: 0x040053D8 RID: 21464
		public float SpecialGroupMaximumHueValue;

		// Token: 0x040053D9 RID: 21465
		public float SpecialGroupMinimumSaturationValue;

		// Token: 0x040053DA RID: 21466
		public float SpecialGroupMaximumSaturationValue;

		// Token: 0x040053DB RID: 21467
		public float HueTestOffset;

		// Token: 0x040053DC RID: 21468
		public bool UseSpecialGroups;

		// Token: 0x040053DD RID: 21469
		public bool UseWallShaderHacks;

		// Token: 0x040053DE RID: 21470
		public bool InvertSpecialGroupResult;
	}
}
