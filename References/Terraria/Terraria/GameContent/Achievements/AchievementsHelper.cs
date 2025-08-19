using System;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x02000206 RID: 518
	public class AchievementsHelper
	{
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06001D91 RID: 7569 RVA: 0x00505A34 File Offset: 0x00503C34
		// (remove) Token: 0x06001D92 RID: 7570 RVA: 0x00505A68 File Offset: 0x00503C68
		public static event AchievementsHelper.ItemPickupEvent OnItemPickup;

		// Token: 0x14000045 RID: 69
		// (add) Token: 0x06001D93 RID: 7571 RVA: 0x00505A9C File Offset: 0x00503C9C
		// (remove) Token: 0x06001D94 RID: 7572 RVA: 0x00505AD0 File Offset: 0x00503CD0
		public static event AchievementsHelper.ItemCraftEvent OnItemCraft;

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06001D95 RID: 7573 RVA: 0x00505B04 File Offset: 0x00503D04
		// (remove) Token: 0x06001D96 RID: 7574 RVA: 0x00505B38 File Offset: 0x00503D38
		public static event AchievementsHelper.TileDestroyedEvent OnTileDestroyed;

		// Token: 0x14000047 RID: 71
		// (add) Token: 0x06001D97 RID: 7575 RVA: 0x00505B6C File Offset: 0x00503D6C
		// (remove) Token: 0x06001D98 RID: 7576 RVA: 0x00505BA0 File Offset: 0x00503DA0
		public static event AchievementsHelper.NPCKilledEvent OnNPCKilled;

		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06001D99 RID: 7577 RVA: 0x00505BD4 File Offset: 0x00503DD4
		// (remove) Token: 0x06001D9A RID: 7578 RVA: 0x00505C08 File Offset: 0x00503E08
		public static event AchievementsHelper.ProgressionEventEvent OnProgressionEvent;

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001D9B RID: 7579 RVA: 0x00505C3B File Offset: 0x00503E3B
		// (set) Token: 0x06001D9C RID: 7580 RVA: 0x00505C42 File Offset: 0x00503E42
		public static bool CurrentlyMining
		{
			get
			{
				return AchievementsHelper._isMining;
			}
			set
			{
				AchievementsHelper._isMining = value;
			}
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x00505C4A File Offset: 0x00503E4A
		public static void NotifyTileDestroyed(Player player, ushort tile)
		{
			if (Main.gameMenu || !AchievementsHelper._isMining)
			{
				return;
			}
			if (AchievementsHelper.OnTileDestroyed != null)
			{
				AchievementsHelper.OnTileDestroyed(player, tile);
			}
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00505C6E File Offset: 0x00503E6E
		public static void NotifyItemPickup(Player player, Item item)
		{
			if (AchievementsHelper.OnItemPickup != null)
			{
				AchievementsHelper.OnItemPickup(player, (short)item.netID, item.stack);
			}
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00505C8F File Offset: 0x00503E8F
		public static void NotifyItemPickup(Player player, Item item, int customStack)
		{
			if (AchievementsHelper.OnItemPickup != null)
			{
				AchievementsHelper.OnItemPickup(player, (short)item.netID, customStack);
			}
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x00505CAB File Offset: 0x00503EAB
		public static void NotifyItemCraft(Recipe recipe)
		{
			if (AchievementsHelper.OnItemCraft != null)
			{
				AchievementsHelper.OnItemCraft((short)recipe.createItem.netID, recipe.createItem.stack);
			}
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x00505CD5 File Offset: 0x00503ED5
		public static void Initialize()
		{
			Player.Hooks.OnEnterWorld += AchievementsHelper.OnPlayerEnteredWorld;
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x00505CE8 File Offset: 0x00503EE8
		internal static void OnPlayerEnteredWorld(Player player)
		{
			if (AchievementsHelper.OnItemPickup != null)
			{
				for (int i = 0; i < 58; i++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.inventory[i].type, player.inventory[i].stack);
				}
				for (int j = 0; j < player.armor.Length; j++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.armor[j].type, player.armor[j].stack);
				}
				for (int k = 0; k < player.dye.Length; k++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.dye[k].type, player.dye[k].stack);
				}
				for (int l = 0; l < player.miscEquips.Length; l++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.miscEquips[l].type, player.miscEquips[l].stack);
				}
				for (int m = 0; m < player.miscDyes.Length; m++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.miscDyes[m].type, player.miscDyes[m].stack);
				}
				for (int n = 0; n < player.bank.item.Length; n++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.bank.item[n].type, player.bank.item[n].stack);
				}
				for (int num = 0; num < player.bank2.item.Length; num++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.bank2.item[num].type, player.bank2.item[num].stack);
				}
				for (int num2 = 0; num2 < player.bank3.item.Length; num2++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.bank3.item[num2].type, player.bank3.item[num2].stack);
				}
				for (int num3 = 0; num3 < player.bank4.item.Length; num3++)
				{
					AchievementsHelper.OnItemPickup(player, (short)player.bank4.item[num3].type, player.bank4.item[num3].stack);
				}
				for (int num4 = 0; num4 < player.Loadouts.Length; num4++)
				{
					Item[] array = player.Loadouts[num4].Armor;
					for (int num5 = 0; num5 < array.Length; num5++)
					{
						AchievementsHelper.OnItemPickup(player, (short)array[num5].type, array[num5].stack);
					}
					array = player.Loadouts[num4].Dye;
					for (int num6 = 0; num6 < array.Length; num6++)
					{
						AchievementsHelper.OnItemPickup(player, (short)array[num6].type, array[num6].stack);
					}
				}
			}
			if (player.statManaMax > 20)
			{
				Main.Achievements.GetCondition("STAR_POWER", "Use").Complete();
			}
			if (player.statLifeMax == 500 && player.statManaMax == 200)
			{
				Main.Achievements.GetCondition("TOPPED_OFF", "Use").Complete();
			}
			if (player.miscEquips[4].type > 0)
			{
				Main.Achievements.GetCondition("HOLD_ON_TIGHT", "Equip").Complete();
			}
			if (player.miscEquips[3].type > 0)
			{
				Main.Achievements.GetCondition("THE_CAVALRY", "Equip").Complete();
			}
			for (int num7 = 0; num7 < player.armor.Length; num7++)
			{
				if (player.armor[num7].wingSlot > 0)
				{
					Main.Achievements.GetCondition("HEAD_IN_THE_CLOUDS", "Equip").Complete();
					break;
				}
			}
			if (player.armor[0].stack > 0 && player.armor[1].stack > 0 && player.armor[2].stack > 0)
			{
				Main.Achievements.GetCondition("MATCHING_ATTIRE", "Equip").Complete();
			}
			if (player.armor[10].stack > 0 && player.armor[11].stack > 0 && player.armor[12].stack > 0)
			{
				Main.Achievements.GetCondition("FASHION_STATEMENT", "Equip").Complete();
			}
			bool flag = true;
			for (int num8 = 0; num8 < 10; num8++)
			{
				if (player.IsItemSlotUnlockedAndUsable(num8) && (player.dye[num8].type < 1 || player.dye[num8].stack < 1))
				{
					flag = false;
				}
			}
			if (flag)
			{
				Main.Achievements.GetCondition("DYE_HARD", "Equip").Complete();
			}
			if (player.unlockedBiomeTorches)
			{
				Main.Achievements.GetCondition("GAIN_TORCH_GODS_FAVOR", "Use").Complete();
			}
			WorldGen.CheckAchievement_RealEstateAndTownSlimes();
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x00506200 File Offset: 0x00504400
		public static void NotifyNPCKilled(NPC npc)
		{
			if (Main.netMode == 0)
			{
				if (npc.playerInteraction[Main.myPlayer])
				{
					AchievementsHelper.NotifyNPCKilledDirect(Main.player[Main.myPlayer], npc.netID);
					return;
				}
			}
			else
			{
				for (int i = 0; i < 255; i++)
				{
					if (npc.playerInteraction[i])
					{
						NetMessage.SendData(97, i, -1, null, npc.netID, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x00506275 File Offset: 0x00504475
		public static void NotifyNPCKilledDirect(Player player, int npcNetID)
		{
			if (AchievementsHelper.OnNPCKilled != null)
			{
				AchievementsHelper.OnNPCKilled(player, (short)npcNetID);
			}
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0050628C File Offset: 0x0050448C
		public static void NotifyProgressionEvent(int eventID)
		{
			if (Main.netMode == 2)
			{
				NetMessage.SendData(98, -1, -1, null, eventID, 0f, 0f, 0f, 0, 0, 0);
				return;
			}
			if (AchievementsHelper.OnProgressionEvent != null)
			{
				AchievementsHelper.OnProgressionEvent(eventID);
			}
		}

		// Token: 0x06001DA6 RID: 7590 RVA: 0x005062D4 File Offset: 0x005044D4
		public static void HandleOnEquip(Player player, Item item, int context)
		{
			if (context == 16)
			{
				Main.Achievements.GetCondition("HOLD_ON_TIGHT", "Equip").Complete();
			}
			if (context == 17)
			{
				Main.Achievements.GetCondition("THE_CAVALRY", "Equip").Complete();
			}
			if ((context == 10 || context == 11) && item.wingSlot > 0)
			{
				Main.Achievements.GetCondition("HEAD_IN_THE_CLOUDS", "Equip").Complete();
			}
			if (context == 8 && player.armor[0].stack > 0 && player.armor[1].stack > 0 && player.armor[2].stack > 0)
			{
				Main.Achievements.GetCondition("MATCHING_ATTIRE", "Equip").Complete();
			}
			if (context == 9 && player.armor[10].stack > 0 && player.armor[11].stack > 0 && player.armor[12].stack > 0)
			{
				Main.Achievements.GetCondition("FASHION_STATEMENT", "Equip").Complete();
			}
			if (context == 12 || context == 33)
			{
				for (int i = 0; i < 10; i++)
				{
					if (player.IsItemSlotUnlockedAndUsable(i) && (player.dye[i].type < 1 || player.dye[i].stack < 1))
					{
						return;
					}
				}
				for (int j = 0; j < player.miscDyes.Length; j++)
				{
					if (player.miscDyes[j].type < 1 || player.miscDyes[j].stack < 1)
					{
						return;
					}
				}
				Main.Achievements.GetCondition("DYE_HARD", "Equip").Complete();
			}
		}

		// Token: 0x06001DA7 RID: 7591 RVA: 0x00506478 File Offset: 0x00504678
		public static void HandleSpecialEvent(Player player, int eventID)
		{
			if (player.whoAmI != Main.myPlayer)
			{
				return;
			}
			switch (eventID)
			{
			case 1:
				Main.Achievements.GetCondition("STAR_POWER", "Use").Complete();
				if (player.statLifeMax == 500 && player.statManaMax == 200)
				{
					Main.Achievements.GetCondition("TOPPED_OFF", "Use").Complete();
					return;
				}
				break;
			case 2:
				Main.Achievements.GetCondition("GET_A_LIFE", "Use").Complete();
				if (player.statLifeMax == 500 && player.statManaMax == 200)
				{
					Main.Achievements.GetCondition("TOPPED_OFF", "Use").Complete();
					return;
				}
				break;
			case 3:
				Main.Achievements.GetCondition("NOT_THE_BEES", "Use").Complete();
				return;
			case 4:
				Main.Achievements.GetCondition("WATCH_YOUR_STEP", "Hit").Complete();
				return;
			case 5:
				Main.Achievements.GetCondition("RAINBOWS_AND_UNICORNS", "Use").Complete();
				return;
			case 6:
				Main.Achievements.GetCondition("YOU_AND_WHAT_ARMY", "Spawn").Complete();
				return;
			case 7:
				Main.Achievements.GetCondition("THROWING_LINES", "Use").Complete();
				return;
			case 8:
				Main.Achievements.GetCondition("LUCKY_BREAK", "Hit").Complete();
				return;
			case 9:
				Main.Achievements.GetCondition("VEHICULAR_MANSLAUGHTER", "Hit").Complete();
				return;
			case 10:
				Main.Achievements.GetCondition("ROCK_BOTTOM", "Reach").Complete();
				return;
			case 11:
				Main.Achievements.GetCondition("INTO_ORBIT", "Reach").Complete();
				return;
			case 12:
				Main.Achievements.GetCondition("WHERES_MY_HONEY", "Reach").Complete();
				return;
			case 13:
				Main.Achievements.GetCondition("JEEPERS_CREEPERS", "Reach").Complete();
				return;
			case 14:
				Main.Achievements.GetCondition("ITS_GETTING_HOT_IN_HERE", "Reach").Complete();
				return;
			case 15:
				Main.Achievements.GetCondition("FUNKYTOWN", "Reach").Complete();
				return;
			case 16:
				Main.Achievements.GetCondition("I_AM_LOOT", "Peek").Complete();
				return;
			case 17:
				Main.Achievements.GetCondition("FLY_A_KITE_ON_A_WINDY_DAY", "Use").Complete();
				return;
			case 18:
				Main.Achievements.GetCondition("FOUND_GRAVEYARD", "Reach").Complete();
				return;
			case 19:
				Main.Achievements.GetCondition("GO_LAVA_FISHING", "Do").Complete();
				return;
			case 20:
				Main.Achievements.GetCondition("TALK_TO_NPC_AT_MAX_HAPPINESS", "Do").Complete();
				return;
			case 21:
				Main.Achievements.GetCondition("PET_THE_PET", "Do").Complete();
				return;
			case 22:
				Main.Achievements.GetCondition("FIND_A_FAIRY", "Do").Complete();
				return;
			case 23:
				Main.Achievements.GetCondition("DIE_TO_DEAD_MANS_CHEST", "Do").Complete();
				return;
			case 24:
				Main.Achievements.GetCondition("GAIN_TORCH_GODS_FAVOR", "Use").Complete();
				return;
			case 25:
				Main.Achievements.GetCondition("DRINK_BOTTLED_WATER_WHILE_DROWNING", "Use").Complete();
				return;
			case 26:
				Main.Achievements.GetCondition("PLAY_ON_A_SPECIAL_SEED", "Do").Complete();
				return;
			case 27:
				Main.Achievements.GetCondition("PURIFY_ENTIRE_WORLD", "Do").Complete();
				break;
			default:
				return;
			}
		}

		// Token: 0x06001DA8 RID: 7592 RVA: 0x00506837 File Offset: 0x00504A37
		public static void HandleNurseService(int coinsSpent)
		{
			((CustomFloatCondition)Main.Achievements.GetCondition("FREQUENT_FLYER", "Pay")).Value += (float)coinsSpent;
		}

		// Token: 0x06001DA9 RID: 7593 RVA: 0x00506860 File Offset: 0x00504A60
		public static void HandleAnglerService()
		{
			Main.Achievements.GetCondition("SERVANT_IN_TRAINING", "Finish").Complete();
			CustomIntCondition customIntCondition = (CustomIntCondition)Main.Achievements.GetCondition("GOOD_LITTLE_SLAVE", "Finish");
			int value = customIntCondition.Value;
			customIntCondition.Value = value + 1;
			CustomIntCondition customIntCondition2 = (CustomIntCondition)Main.Achievements.GetCondition("TROUT_MONKEY", "Finish");
			value = customIntCondition2.Value;
			customIntCondition2.Value = value + 1;
			CustomIntCondition customIntCondition3 = (CustomIntCondition)Main.Achievements.GetCondition("FAST_AND_FISHIOUS", "Finish");
			value = customIntCondition3.Value;
			customIntCondition3.Value = value + 1;
			CustomIntCondition customIntCondition4 = (CustomIntCondition)Main.Achievements.GetCondition("SUPREME_HELPER_MINION", "Finish");
			value = customIntCondition4.Value;
			customIntCondition4.Value = value + 1;
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x00506926 File Offset: 0x00504B26
		public static void HandleRunning(float pixelsMoved)
		{
			((CustomFloatCondition)Main.Achievements.GetCondition("MARATHON_MEDALIST", "Move")).Value += pixelsMoved;
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x00506950 File Offset: 0x00504B50
		public static void HandleMining()
		{
			CustomIntCondition customIntCondition = (CustomIntCondition)Main.Achievements.GetCondition("BULLDOZER", "Pick");
			int value = customIntCondition.Value;
			customIntCondition.Value = value + 1;
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x00506988 File Offset: 0x00504B88
		public static void CheckMechaMayhem(int justKilled = -1)
		{
			if (!AchievementsHelper.mayhemOK)
			{
				if (NPC.AnyNPCs(127) && NPC.AnyNPCs(134) && NPC.AnyNPCs(126) && NPC.AnyNPCs(125))
				{
					AchievementsHelper.mayhemOK = true;
					AchievementsHelper.mayhem1down = false;
					AchievementsHelper.mayhem2down = false;
					AchievementsHelper.mayhem3down = false;
					return;
				}
			}
			else
			{
				if (justKilled == 125 || justKilled == 126)
				{
					AchievementsHelper.mayhem1down = true;
				}
				else if (!NPC.AnyNPCs(125) && !NPC.AnyNPCs(126) && !AchievementsHelper.mayhem1down)
				{
					AchievementsHelper.mayhemOK = false;
					return;
				}
				if (justKilled == 134)
				{
					AchievementsHelper.mayhem2down = true;
				}
				else if (!NPC.AnyNPCs(134) && !AchievementsHelper.mayhem2down)
				{
					AchievementsHelper.mayhemOK = false;
					return;
				}
				if (justKilled == 127)
				{
					AchievementsHelper.mayhem3down = true;
				}
				else if (!NPC.AnyNPCs(127) && !AchievementsHelper.mayhem3down)
				{
					AchievementsHelper.mayhemOK = false;
					return;
				}
				if (AchievementsHelper.mayhem1down && AchievementsHelper.mayhem2down && AchievementsHelper.mayhem3down)
				{
					AchievementsHelper.NotifyProgressionEvent(21);
				}
			}
		}

		// Token: 0x04004565 RID: 17765
		private static bool _isMining;

		// Token: 0x04004566 RID: 17766
		private static bool mayhemOK;

		// Token: 0x04004567 RID: 17767
		private static bool mayhem1down;

		// Token: 0x04004568 RID: 17768
		private static bool mayhem2down;

		// Token: 0x04004569 RID: 17769
		private static bool mayhem3down;

		// Token: 0x02000627 RID: 1575
		// (Invoke) Token: 0x0600339D RID: 13213
		public delegate void ItemPickupEvent(Player player, short itemId, int count);

		// Token: 0x02000628 RID: 1576
		// (Invoke) Token: 0x060033A1 RID: 13217
		public delegate void ItemCraftEvent(short itemId, int count);

		// Token: 0x02000629 RID: 1577
		// (Invoke) Token: 0x060033A5 RID: 13221
		public delegate void TileDestroyedEvent(Player player, ushort tileId);

		// Token: 0x0200062A RID: 1578
		// (Invoke) Token: 0x060033A9 RID: 13225
		public delegate void NPCKilledEvent(Player player, short npcId);

		// Token: 0x0200062B RID: 1579
		// (Invoke) Token: 0x060033AD RID: 13229
		public delegate void ProgressionEventEvent(int eventID);
	}
}
