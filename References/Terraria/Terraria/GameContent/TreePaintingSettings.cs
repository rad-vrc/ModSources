using System;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
	// Token: 0x020001DB RID: 475
	public class TreePaintingSettings
	{
		// Token: 0x06001C29 RID: 7209 RVA: 0x004F1B84 File Offset: 0x004EFD84
		public void ApplyShader(int paintColor, Effect shader)
		{
			shader.Parameters["leafHueTestOffset"].SetValue(this.HueTestOffset);
			shader.Parameters["leafMinHue"].SetValue(this.SpecialGroupMinimalHueValue);
			shader.Parameters["leafMaxHue"].SetValue(this.SpecialGroupMaximumHueValue);
			shader.Parameters["leafMinSat"].SetValue(this.SpecialGroupMinimumSaturationValue);
			shader.Parameters["leafMaxSat"].SetValue(this.SpecialGroupMaximumSaturationValue);
			shader.Parameters["invertSpecialGroupResult"].SetValue(this.InvertSpecialGroupResult);
			int index = Main.ConvertPaintIdToTileShaderIndex(paintColor, this.UseSpecialGroups, this.UseWallShaderHacks);
			shader.CurrentTechnique.Passes[index].Apply();
		}

		// Token: 0x04004372 RID: 17266
		public float SpecialGroupMinimalHueValue;

		// Token: 0x04004373 RID: 17267
		public float SpecialGroupMaximumHueValue;

		// Token: 0x04004374 RID: 17268
		public float SpecialGroupMinimumSaturationValue;

		// Token: 0x04004375 RID: 17269
		public float SpecialGroupMaximumSaturationValue;

		// Token: 0x04004376 RID: 17270
		public float HueTestOffset;

		// Token: 0x04004377 RID: 17271
		public bool UseSpecialGroups;

		// Token: 0x04004378 RID: 17272
		public bool UseWallShaderHacks;

		// Token: 0x04004379 RID: 17273
		public bool InvertSpecialGroupResult;
	}
}
