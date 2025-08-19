using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x02000634 RID: 1588
	public class LegacyHairShaderData : HairShaderData
	{
		// Token: 0x0600459A RID: 17818 RVA: 0x006144E9 File Offset: 0x006126E9
		public LegacyHairShaderData() : base(null, null)
		{
			this._shaderDisabled = true;
		}

		// Token: 0x0600459B RID: 17819 RVA: 0x006144FC File Offset: 0x006126FC
		public override Color GetColor(Player player, Color lightColor)
		{
			bool lighting = true;
			Color result = this._colorProcessor(player, player.hairColor, ref lighting);
			if (lighting)
			{
				return new Color(result.ToVector4() * lightColor.ToVector4());
			}
			return result;
		}

		// Token: 0x0600459C RID: 17820 RVA: 0x0061453D File Offset: 0x0061273D
		public LegacyHairShaderData UseLegacyMethod(LegacyHairShaderData.ColorProcessingMethod colorProcessor)
		{
			this._colorProcessor = colorProcessor;
			return this;
		}

		// Token: 0x04005B04 RID: 23300
		private LegacyHairShaderData.ColorProcessingMethod _colorProcessor;

		// Token: 0x02000CD5 RID: 3285
		// (Invoke) Token: 0x06006191 RID: 24977
		public delegate Color ColorProcessingMethod(Player player, Color color, ref bool lighting);
	}
}
