using System;
using Terraria.ModLoader;

namespace QoLCompendium.Core
{
	// Token: 0x0200020E RID: 526
	public class SubworldModConditionsPlayer : ModPlayer
	{
		// Token: 0x06000CEE RID: 3310 RVA: 0x00064D19 File Offset: 0x00062F19
		public override void OnEnterWorld()
		{
			if (SubworldModConditions.downedBereftVassal)
			{
				ModConditions.downedBereftVassal = true;
			}
		}
	}
}
