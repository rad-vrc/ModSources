using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000230 RID: 560
	public class StopwatchInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06002884 RID: 10372 RVA: 0x0050B860 File Offset: 0x00509A60
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_9";
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06002885 RID: 10373 RVA: 0x0050B867 File Offset: 0x00509A67
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.103";
			}
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x0050B86E File Offset: 0x00509A6E
		public override bool Active()
		{
			return Main.player[Main.myPlayer].accStopwatch;
		}
	}
}
