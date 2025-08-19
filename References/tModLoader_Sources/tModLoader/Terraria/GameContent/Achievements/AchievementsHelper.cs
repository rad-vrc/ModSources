using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Terraria.GameContent.Achievements
{
	// Token: 0x020006B7 RID: 1719
	public class AchievementsHelper
	{
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060048A1 RID: 18593 RVA: 0x0064A768 File Offset: 0x00648968
		// (set) Token: 0x060048A2 RID: 18594 RVA: 0x0064A76F File Offset: 0x0064896F
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

		// Token: 0x1400006E RID: 110
		// (add) Token: 0x060048A3 RID: 18595 RVA: 0x0064A778 File Offset: 0x00648978
		// (remove) Token: 0x060048A4 RID: 18596 RVA: 0x0064A7AC File Offset: 0x006489AC
		public static event AchievementsHelper.ItemPickupEvent OnItemPickup;

		// Token: 0x1400006F RID: 111
		// (add) Token: 0x060048A5 RID: 18597 RVA: 0x0064A7E0 File Offset: 0x006489E0
		// (remove) Token: 0x060048A6 RID: 18598 RVA: 0x0064A814 File Offset: 0x00648A14
		public static event AchievementsHelper.ItemCraftEvent OnItemCraft;

		// Token: 0x14000070 RID: 112
		// (add) Token: 0x060048A7 RID: 18599 RVA: 0x0064A848 File Offset: 0x00648A48
		// (remove) Token: 0x060048A8 RID: 18600 RVA: 0x0064A87C File Offset: 0x00648A7C
		public static event AchievementsHelper.TileDestroyedEvent OnTileDestroyed;

		// Token: 0x14000071 RID: 113
		// (add) Token: 0x060048A9 RID: 18601 RVA: 0x0064A8B0 File Offset: 0x00648AB0
		// (remove) Token: 0x060048AA RID: 18602 RVA: 0x0064A8E4 File Offset: 0x00648AE4
		public static event AchievementsHelper.NPCKilledEvent OnNPCKilled;

		// Token: 0x14000072 RID: 114
		// (add) Token: 0x060048AB RID: 18603 RVA: 0x0064A918 File Offset: 0x00648B18
		// (remove) Token: 0x060048AC RID: 18604 RVA: 0x0064A94C File Offset: 0x00648B4C
		public static event AchievementsHelper.ProgressionEventEvent OnProgressionEvent;

		// Token: 0x060048AD RID: 18605 RVA: 0x0064A97F File Offset: 0x00648B7F
		public static void NotifyTileDestroyed(Player player, ushort tile)
		{
			if (!Main.gameMenu && AchievementsHelper._isMining && AchievementsHelper.OnTileDestroyed != null)
			{
				AchievementsHelper.OnTileDestroyed(player, tile);
			}
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x0064A9A2 File Offset: 0x00648BA2
		public static void NotifyItemPickup(Player player, Item item)
		{
			if (AchievementsHelper.OnItemPickup != null)
			{
				AchievementsHelper.OnItemPickup(player, (short)item.netID, item.stack);
			}
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x0064A9C3 File Offset: 0x00648BC3
		public static void NotifyItemPickup(Player player, Item item, int customStack)
		{
			if (AchievementsHelper.OnItemPickup != null)
			{
				AchievementsHelper.OnItemPickup(player, (short)item.netID, customStack);
			}
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x0064A9DF File Offset: 0x00648BDF
		public static void NotifyItemCraft(Recipe recipe)
		{
			if (AchievementsHelper.OnItemCraft != null)
			{
				AchievementsHelper.OnItemCraft((short)recipe.createItem.netID, recipe.createItem.stack);
			}
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x0064AA09 File Offset: 0x00648C09
		public static void Initialize()
		{
			Action<Player> value;
			if ((value = AchievementsHelper.<>O.<0>__OnPlayerEnteredWorld) == null)
			{
				value = (AchievementsHelper.<>O.<0>__OnPlayerEnteredWorld = new Action<Player>(AchievementsHelper.OnPlayerEnteredWorld));
			}
			Player.Hooks.OnEnterWorld += value;
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x0064AA2C File Offset: 0x00648C2C
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
					Item[] armor = player.Loadouts[num4].Armor;
					for (int num5 = 0; num5 < armor.Length; num5++)
					{
						AchievementsHelper.OnItemPickup(player, (short)armor[num5].type, armor[num5].stack);
					}
					armor = player.Loadouts[num4].Dye;
					for (int num6 = 0; num6 < armor.Length; num6++)
					{
						AchievementsHelper.OnItemPickup(player, (short)armor[num6].type, armor[num6].stack);
					}
				}
			}
			if (player.ConsumedManaCrystals > 0)
			{
				Main.Achievements.GetCondition("STAR_POWER", "Use").Complete();
			}
			if (player.ConsumedLifeCrystals == 15 && player.ConsumedLifeFruit == 20 && player.ConsumedManaCrystals == 9)
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

		// Token: 0x060048B3 RID: 18611 RVA: 0x0064AF48 File Offset: 0x00649148
		public static void NotifyNPCKilled(NPC npc)
		{
			if (Main.netMode == 0)
			{
				if (npc.playerInteraction[Main.myPlayer])
				{
					AchievementsHelper.NotifyNPCKilledDirect(Main.player[Main.myPlayer], npc.netID);
				}
				return;
			}
			for (int i = 0; i < 255; i++)
			{
				if (npc.playerInteraction[i])
				{
					NetMessage.SendData(97, i, -1, null, npc.netID, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x0064AFBD File Offset: 0x006491BD
		public static void NotifyNPCKilledDirect(Player player, int npcNetID)
		{
			if (AchievementsHelper.OnNPCKilled != null)
			{
				AchievementsHelper.OnNPCKilled(player, (short)npcNetID);
			}
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x0064AFD4 File Offset: 0x006491D4
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

		// Token: 0x060048B6 RID: 18614 RVA: 0x0064B01C File Offset: 0x0064921C
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
			if (new int[]
			{
				10,
				11,
				-10,
				11
			}.Contains(context) && item.wingSlot > 0)
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
			if (context != 12 && context != 33)
			{
				return;
			}
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

		// Token: 0x060048B7 RID: 18615 RVA: 0x0064B1D0 File Offset: 0x006493D0
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
				if (player.ConsumedLifeCrystals == 15 && player.ConsumedLifeFruit == 20 && player.ConsumedManaCrystals == 9)
				{
					Main.Achievements.GetCondition("TOPPED_OFF", "Use").Complete();
					return;
				}
				break;
			case 2:
				Main.Achievements.GetCondition("GET_A_LIFE", "Use").Complete();
				if (player.ConsumedLifeCrystals == 15 && player.ConsumedLifeFruit == 20 && player.ConsumedManaCrystals == 9)
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

		// Token: 0x060048B8 RID: 18616 RVA: 0x0064B59D File Offset: 0x0064979D
		public static void HandleNurseService(int coinsSpent)
		{
			((CustomFloatCondition)Main.Achievements.GetCondition("FREQUENT_FLYER", "Pay")).Value += (float)coinsSpent;
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x0064B5C8 File Offset: 0x006497C8
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

		// Token: 0x060048BA RID: 18618 RVA: 0x0064B68E File Offset: 0x0064988E
		public static void HandleRunning(float pixelsMoved)
		{
			((CustomFloatCondition)Main.Achievements.GetCondition("MARATHON_MEDALIST", "Move")).Value += pixelsMoved;
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x0064B6B8 File Offset: 0x006498B8
		public static void HandleMining()
		{
			CustomIntCondition customIntCondition = (CustomIntCondition)Main.Achievements.GetCondition("BULLDOZER", "Pick");
			int value = customIntCondition.Value;
			customIntCondition.Value = value + 1;
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x0064B6F0 File Offset: 0x006498F0
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
				}
				return;
			}
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

		// Token: 0x04005C66 RID: 23654
		private static bool _isMining;

		// Token: 0x04005C67 RID: 23655
		private static bool mayhemOK;

		// Token: 0x04005C68 RID: 23656
		private static bool mayhem1down;

		// Token: 0x04005C69 RID: 23657
		private static bool mayhem2down;

		// Token: 0x04005C6A RID: 23658
		private static bool mayhem3down;

		// Token: 0x02000D4E RID: 3406
		// (Invoke) Token: 0x060063D4 RID: 25556
		public delegate void ItemPickupEvent(Player player, short itemId, int count);

		// Token: 0x02000D4F RID: 3407
		// (Invoke) Token: 0x060063D8 RID: 25560
		public delegate void ItemCraftEvent(short itemId, int count);

		// Token: 0x02000D50 RID: 3408
		// (Invoke) Token: 0x060063DC RID: 25564
		public delegate void TileDestroyedEvent(Player player, ushort tileId);

		// Token: 0x02000D51 RID: 3409
		// (Invoke) Token: 0x060063E0 RID: 25568
		public delegate void NPCKilledEvent(Player player, short npcId);

		// Token: 0x02000D52 RID: 3410
		// (Invoke) Token: 0x060063E4 RID: 25572
		public delegate void ProgressionEventEvent(int eventID);

		// Token: 0x02000D53 RID: 3411
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007B7C RID: 31612
			public static Action<Player> <0>__OnPlayerEnteredWorld;
		}
	}
}
