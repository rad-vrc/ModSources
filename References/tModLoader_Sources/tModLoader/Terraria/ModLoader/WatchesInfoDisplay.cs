using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000226 RID: 550
	public class WatchesInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600285C RID: 10332 RVA: 0x0050B6A4 File Offset: 0x005098A4
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_0";
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x0050B6AB File Offset: 0x005098AB
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.95";
			}
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x0050B6B2 File Offset: 0x005098B2
		public override bool Active()
		{
			return Main.player[Main.myPlayer].accWatch > 0;
		}
	}
}
