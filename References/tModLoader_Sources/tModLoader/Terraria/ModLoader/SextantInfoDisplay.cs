using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000228 RID: 552
	public class SextantInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06002864 RID: 10340 RVA: 0x0050B6F8 File Offset: 0x005098F8
		public override string Texture
		{
			get
			{
				int index = 7;
				if ((Main.bloodMoon && !Main.dayTime) || (Main.eclipse && Main.dayTime))
				{
					index = 8;
				}
				return "Terraria/Images/UI/InfoIcon_" + index.ToString();
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x0050B736 File Offset: 0x00509936
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.102";
			}
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x0050B73D File Offset: 0x0050993D
		public override bool Active()
		{
			return Main.player[Main.myPlayer].accCalendar;
		}
	}
}
