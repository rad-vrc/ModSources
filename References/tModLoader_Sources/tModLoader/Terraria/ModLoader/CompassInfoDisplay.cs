using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000231 RID: 561
	public class CompassInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06002888 RID: 10376 RVA: 0x0050B888 File Offset: 0x00509A88
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_3";
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06002889 RID: 10377 RVA: 0x0050B88F File Offset: 0x00509A8F
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.98";
			}
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x0050B896 File Offset: 0x00509A96
		public override bool Active()
		{
			return Main.player[Main.myPlayer].accCompass > 0;
		}
	}
}
