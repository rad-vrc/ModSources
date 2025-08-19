using System;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.PlayerChanges
{
	// Token: 0x02000231 RID: 561
	public class RespawnPlayer : ModPlayer
	{
		// Token: 0x06000D77 RID: 3447 RVA: 0x00068998 File Offset: 0x00066B98
		public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
		{
			if (QoLCompendium.mainConfig.InstantRespawn && Main.netMode == 0)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					if (!Main.npc[i].friendly && Main.npc[i].active && !Common.LunarPillarIDs.Contains(Main.npc[i].type))
					{
						RespawnPlayer.DespawnNPC(i);
					}
					base.Player.respawnTimer = 60;
				}
			}
			if (QoLCompendium.mainConfig.KeepBuffsOnDeath)
			{
				if (this.buffCache == null)
				{
					this.buffCache = new ValueTuple<int, int>[Player.MaxBuffs];
				}
				for (int j = 0; j < Player.MaxBuffs; j++)
				{
					this.buffCache[j] = new ValueTuple<int, int>(base.Player.buffType[j], base.Player.buffTime[j]);
				}
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00068A70 File Offset: 0x00066C70
		public override void OnRespawn()
		{
			if (QoLCompendium.mainConfig.FullHealthRespawn)
			{
				base.Player.statLife = base.Player.statLifeMax2;
				base.Player.statMana = base.Player.statManaMax2;
			}
			if (QoLCompendium.mainConfig.KeepBuffsOnDeath)
			{
				foreach (ValueTuple<int, int> valueTuple in this.buffCache)
				{
					int num = valueTuple.Item1;
					int num2 = valueTuple.Item2;
					if ((QoLCompendium.mainConfig.KeepDebuffsOnDeath || !Main.debuff[num]) && num > 0 && !Main.persistentBuff[num] && num2 > 2)
					{
						int num3 = (int)((float)num2);
						base.Player.AddBuff(num, num3, false, false);
					}
				}
			}
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00068B28 File Offset: 0x00066D28
		public static void DespawnNPC(int npc)
		{
			Main.npc[npc].life = 0;
			Main.npc[npc].active = false;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(23, -1, -1, null, npc, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		// Token: 0x0400059D RID: 1437
		[TupleElementNames(new string[]
		{
			"type",
			"time"
		})]
		private ValueTuple<int, int>[] buffCache;
	}
}
