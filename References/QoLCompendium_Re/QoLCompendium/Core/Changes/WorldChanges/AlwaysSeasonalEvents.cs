using System;
using Terraria;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.WorldChanges
{
	// Token: 0x0200020F RID: 527
	public class AlwaysSeasonalEvents : ModSystem
	{
		// Token: 0x06000CF0 RID: 3312 RVA: 0x00064D28 File Offset: 0x00062F28
		public override void PostUpdateWorld()
		{
			if (QoLCompendium.mainConfig.HalloweenActive)
			{
				Main.halloween = true;
			}
			if (QoLCompendium.mainConfig.ChristmasActive)
			{
				Main.xMas = true;
			}
		}
	}
}
