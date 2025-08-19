using System;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.BuffChanges
{
	// Token: 0x02000263 RID: 611
	public class HideBuffsPlayer : ModPlayer
	{
		// Token: 0x06000E2C RID: 3628 RVA: 0x00071FB4 File Offset: 0x000701B4
		public override void PostUpdateBuffs()
		{
			if (!QoLCompendium.mainConfig.HideBuffs)
			{
				return;
			}
			HashSet<int> reapplyBuffs = new HashSet<int>();
			HashSet<int> otherBuffs = new HashSet<int>();
			for (int i = 0; i < base.Player.CountBuffs(); i++)
			{
				if (!base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Contains(base.Player.buffType[i]))
				{
					otherBuffs.Add(base.Player.buffType[i]);
				}
				if (base.Player.GetModPlayer<QoLCPlayer>().activeBuffs.Contains(base.Player.buffType[i]))
				{
					reapplyBuffs.Add(base.Player.buffType[i]);
				}
			}
			foreach (int buff in reapplyBuffs)
			{
				base.Player.ClearBuff(buff);
				base.Player.AddBuff(buff, 2, true, false);
			}
		}
	}
}
