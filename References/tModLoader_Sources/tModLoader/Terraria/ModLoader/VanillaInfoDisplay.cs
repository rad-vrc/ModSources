using System;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.ModLoader
{
	// Token: 0x02000225 RID: 549
	[Autoload(false)]
	public abstract class VanillaInfoDisplay : InfoDisplay
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x0050B681 File Offset: 0x00509881
		public override LocalizedText DisplayName
		{
			get
			{
				return Language.GetText(this.LangKey);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x0050B68E File Offset: 0x0050988E
		public override string HoverTexture
		{
			get
			{
				return InfoDisplay.VanillaHoverTexture;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06002859 RID: 10329
		protected abstract string LangKey { get; }

		// Token: 0x0600285A RID: 10330 RVA: 0x0050B695 File Offset: 0x00509895
		public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
		{
			return "";
		}
	}
}
