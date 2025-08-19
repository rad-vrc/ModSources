using System;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Dyes
{
	// Token: 0x0200021E RID: 542
	public class LegacyHairShaderData : HairShaderData
	{
		// Token: 0x06001EB5 RID: 7861 RVA: 0x0050C419 File Offset: 0x0050A619
		public LegacyHairShaderData() : base(null, null)
		{
			this._shaderDisabled = true;
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x0050C42C File Offset: 0x0050A62C
		public override Color GetColor(Player player, Color lightColor)
		{
			bool flag = true;
			Color result = this._colorProcessor(player, player.hairColor, ref flag);
			if (flag)
			{
				return new Color(result.ToVector4() * lightColor.ToVector4());
			}
			return result;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0050C46D File Offset: 0x0050A66D
		public LegacyHairShaderData UseLegacyMethod(LegacyHairShaderData.ColorProcessingMethod colorProcessor)
		{
			this._colorProcessor = colorProcessor;
			return this;
		}

		// Token: 0x040045BE RID: 17854
		private LegacyHairShaderData.ColorProcessingMethod _colorProcessor;

		// Token: 0x0200062D RID: 1581
		// (Invoke) Token: 0x060033B1 RID: 13233
		public delegate Color ColorProcessingMethod(Player player, Color color, ref bool lighting);
	}
}
