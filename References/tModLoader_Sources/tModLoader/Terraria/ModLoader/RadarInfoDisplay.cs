using System;

namespace Terraria.ModLoader
{
	// Token: 0x0200022C RID: 556
	public class RadarInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x0050B7CF File Offset: 0x005099CF
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_5";
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x0050B7D6 File Offset: 0x005099D6
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.100";
			}
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x0050B7DD File Offset: 0x005099DD
		public override bool Active()
		{
			return Main.player[Main.myPlayer].accThirdEye;
		}
	}
}
