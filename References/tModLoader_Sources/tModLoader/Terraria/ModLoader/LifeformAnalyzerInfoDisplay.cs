using System;

namespace Terraria.ModLoader
{
	// Token: 0x0200022B RID: 555
	public class LifeformAnalyzerInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06002870 RID: 10352 RVA: 0x0050B7A7 File Offset: 0x005099A7
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_11";
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06002871 RID: 10353 RVA: 0x0050B7AE File Offset: 0x005099AE
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.105";
			}
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x0050B7B5 File Offset: 0x005099B5
		public override bool Active()
		{
			return Main.player[Main.myPlayer].accCritterGuide;
		}
	}
}
