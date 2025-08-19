using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000232 RID: 562
	public class DepthMeterInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600288C RID: 10380 RVA: 0x0050B8B3 File Offset: 0x00509AB3
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_4";
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x0050B8BA File Offset: 0x00509ABA
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.99";
			}
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x0050B8C1 File Offset: 0x00509AC1
		public override bool Active()
		{
			return Main.player[Main.myPlayer].accDepthMeter > 0;
		}
	}
}
