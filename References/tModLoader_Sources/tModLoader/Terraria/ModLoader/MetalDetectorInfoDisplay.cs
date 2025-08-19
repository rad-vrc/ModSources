using System;

namespace Terraria.ModLoader
{
	// Token: 0x0200022A RID: 554
	public class MetalDetectorInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x0050B77F File Offset: 0x0050997F
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_10";
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600286D RID: 10349 RVA: 0x0050B786 File Offset: 0x00509986
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.104";
			}
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x0050B78D File Offset: 0x0050998D
		public override bool Active()
		{
			return Main.player[Main.myPlayer].accOreFinder;
		}
	}
}
