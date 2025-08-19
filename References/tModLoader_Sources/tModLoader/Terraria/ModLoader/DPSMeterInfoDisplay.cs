using System;

namespace Terraria.ModLoader
{
	// Token: 0x0200022F RID: 559
	public class DPSMeterInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x0050B838 File Offset: 0x00509A38
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_12";
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06002881 RID: 10369 RVA: 0x0050B83F File Offset: 0x00509A3F
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.106";
			}
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x0050B846 File Offset: 0x00509A46
		public override bool Active()
		{
			return Main.player[Main.myPlayer].accDreamCatcher;
		}
	}
}
