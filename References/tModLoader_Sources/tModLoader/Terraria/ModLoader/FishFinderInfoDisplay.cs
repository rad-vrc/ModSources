using System;

namespace Terraria.ModLoader
{
	// Token: 0x02000229 RID: 553
	public class FishFinderInfoDisplay : VanillaInfoDisplay
	{
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06002868 RID: 10344 RVA: 0x0050B757 File Offset: 0x00509957
		public override string Texture
		{
			get
			{
				return "Terraria/Images/UI/InfoIcon_2";
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x0050B75E File Offset: 0x0050995E
		protected override string LangKey
		{
			get
			{
				return "LegacyInterface.97";
			}
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x0050B765 File Offset: 0x00509965
		public override bool Active()
		{
			return Main.player[Main.myPlayer].accFishFinder;
		}
	}
}
