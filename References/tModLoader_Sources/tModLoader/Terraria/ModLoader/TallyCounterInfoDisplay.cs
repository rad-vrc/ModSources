using System;

namespace Terraria.ModLoader
{
	// Token: 0x0200022D RID: 557
	public class TallyCounterInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06002878 RID: 10360 RVA: 0x0050B7F7 File Offset: 0x005099F7
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_6";
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06002879 RID: 10361 RVA: 0x0050B7FE File Offset: 0x005099FE
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.101";
			}
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x0050B805 File Offset: 0x00509A05
		public override bool Active()
		{
			return Main.player[Main.myPlayer].accJarOfSouls;
		}
	}
}
