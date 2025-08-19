using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;

namespace QoLCompendium.Core.Changes.NPCChanges
{
	// Token: 0x0200023F RID: 575
	public class ImprovedTownNPCSpawns : ModSystem
	{
		// Token: 0x06000DB1 RID: 3505 RVA: 0x0006D2C0 File Offset: 0x0006B4C0
		public override void Load()
		{
			On_Main.UpdateTime_SpawnTownNPCs += delegate(On_Main.orig_UpdateTime_SpawnTownNPCs orig)
			{
				ImprovedTownNPCSpawns._isExtraUpdate = false;
				orig.Invoke();
				int times = QoLCompendium.mainConfig.FastTownNPCSpawns - 1;
				double worldUpdateRate = WorldGen.GetWorldUpdateRate();
				ImprovedTownNPCSpawns._cachedReport = Main.GetBestiaryProgressReport();
				ImprovedTownNPCSpawns._isExtraUpdate = true;
				for (int i = 0; i < times; i++)
				{
					ImprovedTownNPCSpawns.TrySetNPCSpawn(orig, worldUpdateRate);
				}
				ImprovedTownNPCSpawns._isExtraUpdate = false;
			};
			On_WorldGen.QuickFindHome += delegate(On_WorldGen.orig_QuickFindHome orig, int npc)
			{
				if (!ImprovedTownNPCSpawns._isExtraUpdate)
				{
					orig.Invoke(npc);
				}
			};
			On_Main.GetBestiaryProgressReport += delegate(On_Main.orig_GetBestiaryProgressReport orig)
			{
				if (!ImprovedTownNPCSpawns._isExtraUpdate)
				{
					return orig.Invoke();
				}
				return ImprovedTownNPCSpawns._cachedReport;
			};
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0006D33C File Offset: 0x0006B53C
		public override void PreUpdateWorld()
		{
			if (QoLCompendium.mainConfig.TownNPCSpawnImprovements)
			{
				NPC.savedAngler = true;
				NPC.savedGolfer = true;
				NPC.savedStylist = true;
				if (NPC.downedGoblins)
				{
					NPC.savedGoblin = true;
				}
				if (NPC.downedBoss2)
				{
					NPC.savedBartender = true;
				}
				if (NPC.downedBoss3)
				{
					NPC.savedMech = true;
				}
				if (Main.hardMode)
				{
					NPC.savedWizard = true;
					NPC.savedTaxCollector = true;
				}
			}
			if (ImprovedTownNPCSpawns.HasEnoughMoneyForMerchant() && QoLCompendium.mainConfig.AutoMoneyQuickStack)
			{
				ImprovedTownNPCSpawns.TrySetNPCSpawn(17);
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0006D3BB File Offset: 0x0006B5BB
		private static void TrySetNPCSpawn(On_Main.orig_UpdateTime_SpawnTownNPCs orig, double worldUpdateRate)
		{
			orig.Invoke();
			if (Main.netMode == 1 || worldUpdateRate <= 0.0 || Main.checkForSpawns != 0)
			{
				return;
			}
			ImprovedTownNPCSpawns.SetupActiveTownNPCList();
			if (ImprovedTownNPCSpawns.HasEnoughMoneyForMerchant())
			{
				ImprovedTownNPCSpawns.TrySetNPCSpawn(17);
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0006D3F4 File Offset: 0x0006B5F4
		public static bool HasEnoughMoneyForMerchant()
		{
			int moneyCount = 0;
			for (int i = 0; i < 255; i++)
			{
				Player player = Main.player[i];
				if (player.active)
				{
					for (int j = 0; j < 40; j++)
					{
						if (player.bank.item[j] != null && player.bank.item[j].stack > 0)
						{
							Item item = player.bank.item[j];
							switch (item.type)
							{
							case 71:
								moneyCount += item.stack;
								break;
							case 72:
								moneyCount += item.stack * 100;
								break;
							case 73:
								return true;
							case 74:
								return true;
							}
							if (moneyCount >= 5000)
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0006D4C1 File Offset: 0x0006B6C1
		public static void TrySetNPCSpawn(int npcId)
		{
			if (ImprovedTownNPCSpawns._activeTownNPCs.Contains(npcId))
			{
				return;
			}
			Main.townNPCCanSpawn[npcId] = true;
			if (WorldGen.prioritizedTownNPCType == 0)
			{
				WorldGen.prioritizedTownNPCType = npcId;
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0006D4E8 File Offset: 0x0006B6E8
		private static void SetupActiveTownNPCList()
		{
			ImprovedTownNPCSpawns._activeTownNPCs = new HashSet<int>();
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && npc.townNPC && npc.friendly)
				{
					ImprovedTownNPCSpawns._activeTownNPCs.Add(npc.type);
				}
			}
		}

		// Token: 0x040005A8 RID: 1448
		private static bool _isExtraUpdate;

		// Token: 0x040005A9 RID: 1449
		private static HashSet<int> _activeTownNPCs = new HashSet<int>();

		// Token: 0x040005AA RID: 1450
		private static BestiaryUnlockProgressReport _cachedReport = default(BestiaryUnlockProgressReport);
	}
}
