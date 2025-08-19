using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria
{
	// Token: 0x02000035 RID: 53
	public class Lang
	{
		// Token: 0x0600030A RID: 778 RVA: 0x000887F0 File Offset: 0x000869F0
		public static string GetMapObjectName(int id)
		{
			if (Lang._mapLegendCache != null)
			{
				return Lang._mapLegendCache[id].Value;
			}
			return string.Empty;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00088810 File Offset: 0x00086A10
		public static object CreateDialogSubstitutionObject(NPC npc = null)
		{
			return new
			{
				Nurse = NPC.GetFirstNPCNameOrNull(18),
				Merchant = NPC.GetFirstNPCNameOrNull(17),
				ArmsDealer = NPC.GetFirstNPCNameOrNull(19),
				Dryad = NPC.GetFirstNPCNameOrNull(20),
				Demolitionist = NPC.GetFirstNPCNameOrNull(38),
				Clothier = NPC.GetFirstNPCNameOrNull(54),
				Guide = NPC.GetFirstNPCNameOrNull(22),
				Wizard = NPC.GetFirstNPCNameOrNull(108),
				GoblinTinkerer = NPC.GetFirstNPCNameOrNull(107),
				Mechanic = NPC.GetFirstNPCNameOrNull(124),
				Truffle = NPC.GetFirstNPCNameOrNull(160),
				Steampunker = NPC.GetFirstNPCNameOrNull(178),
				DyeTrader = NPC.GetFirstNPCNameOrNull(207),
				PartyGirl = NPC.GetFirstNPCNameOrNull(208),
				Cyborg = NPC.GetFirstNPCNameOrNull(209),
				Painter = NPC.GetFirstNPCNameOrNull(227),
				WitchDoctor = NPC.GetFirstNPCNameOrNull(228),
				Pirate = NPC.GetFirstNPCNameOrNull(229),
				Stylist = NPC.GetFirstNPCNameOrNull(353),
				TravelingMerchant = NPC.GetFirstNPCNameOrNull(368),
				Angler = NPC.GetFirstNPCNameOrNull(369),
				Bartender = NPC.GetFirstNPCNameOrNull(550),
				WorldName = Main.worldName,
				Day = Main.dayTime,
				BloodMoon = Main.bloodMoon,
				Eclipse = Main.eclipse,
				MoonLordDefeated = NPC.downedMoonlord,
				GolemDefeated = NPC.downedGolemBoss,
				DukeFishronDefeated = NPC.downedFishron,
				FrostLegionDefeated = NPC.downedFrost,
				MartiansDefeated = NPC.downedMartians,
				PumpkingDefeated = NPC.downedHalloweenKing,
				IceQueenDefeated = NPC.downedChristmasIceQueen,
				HardMode = Main.hardMode,
				Homeless = (npc != null && npc.homeless),
				InventoryKey = Main.cInv,
				PlayerName = Main.player[Main.myPlayer].name,
				GolfGuy = NPC.GetFirstNPCNameOrNull(588),
				TaxCollector = NPC.GetFirstNPCNameOrNull(441),
				Rain = Main.raining,
				Graveyard = Main.LocalPlayer.ZoneGraveyard,
				AnglerCompletedQuestsCount = Main.LocalPlayer.anglerQuestsFinished,
				TotalDeathsCount = Main.LocalPlayer.numberOfDeathsPVE,
				WorldEvilStone = (WorldGen.crimson ? Language.GetTextValue("Misc.Crimstone") : Language.GetTextValue("Misc.Ebonstone")),
				InputTriggerUI_BuildFromInventory = PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "QuickMount"),
				InputTriggerUI_SellItem = PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "SmartSelect"),
				InputTriggerUI_Trash = PlayerInput.GenerateInputTag_ForCurrentGamemode_WithHacks(false, "SmartSelect"),
				InputTriggerUI_FavoriteItem = PlayerInput.GenerateInputTag_ForCurrentGamemode_WithHacks(false, "SmartCursor"),
				InputTrigger_QuickEquip = PlayerInput.GenerateInputTag_ForCurrentGamemode(false, "Grapple"),
				InputTrigger_LockOn = PlayerInput.GenerateInputTag_ForCurrentGamemode(true, "LockOn"),
				InputTrigger_RadialQuickbar = PlayerInput.GenerateInputTag_ForCurrentGamemode(true, "HotbarMinus"),
				InputTrigger_RadialHotbar = PlayerInput.GenerateInputTag_ForCurrentGamemode(true, "HotbarPlus"),
				InputTrigger_SmartCursor = PlayerInput.GenerateInputTag_ForCurrentGamemode(true, "SmartCursor"),
				InputTrigger_UseOrAttack = PlayerInput.GenerateInputTag_ForCurrentGamemode(true, "MouseLeft"),
				InputTrigger_InteractWithTile = PlayerInput.GenerateInputTag_ForCurrentGamemode(true, "MouseRight"),
				InputTrigger_SmartSelect = PlayerInput.GenerateInputTag_ForCurrentGamemode(true, "SmartSelect")
			};
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00088A15 File Offset: 0x00086C15
		[Old("dialog is deprecated. Please use Language.GetText instead.")]
		public static string dialog(int l, bool english = false)
		{
			return Language.GetTextValueWith("LegacyDialog." + l.ToString(), Lang.CreateDialogSubstitutionObject(null));
		}

		/// <summary> Gets the display name of the provided npc type (<see cref="F:Terraria.NPC.type" />) as a string. </summary>
		// Token: 0x0600030D RID: 781 RVA: 0x00088A33 File Offset: 0x00086C33
		public static string GetNPCNameValue(int netID)
		{
			return Lang.GetNPCName(netID).Value;
		}

		/// <summary> Gets the display name of the provided npc type (<see cref="F:Terraria.NPC.type" />) as a <see cref="T:Terraria.Localization.LocalizedText" />. </summary>
		// Token: 0x0600030E RID: 782 RVA: 0x00088A40 File Offset: 0x00086C40
		public static LocalizedText GetNPCName(int netID)
		{
			if (netID > 0 && netID < Lang._npcNameCache.Length)
			{
				return Lang._npcNameCache[netID];
			}
			if (netID < 0 && -netID - 1 < Lang._negativeNpcNameCache.Length)
			{
				return Lang._negativeNpcNameCache[-netID - 1];
			}
			return LocalizedText.Empty;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00088A79 File Offset: 0x00086C79
		public static ItemTooltip GetTooltip(int itemId)
		{
			return Lang._itemTooltipCache[itemId];
		}

		/// <summary> Gets the display name of the provided item type (<see cref="F:Terraria.Item.type" />) as a <see cref="T:Terraria.Localization.LocalizedText" />. <para /> Note that modded item entries are not retrievable until after mod loading is complete. </summary>
		// Token: 0x06000310 RID: 784 RVA: 0x00088A82 File Offset: 0x00086C82
		public static LocalizedText GetItemName(int id)
		{
			if (id < 0)
			{
				id = (int)ItemID.FromNetId((short)id);
			}
			if (id > 0 && id < Lang._itemNameCache.Length && Lang._itemNameCache[id] != null)
			{
				return Lang._itemNameCache[id];
			}
			return LocalizedText.Empty;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00088AB5 File Offset: 0x00086CB5
		public static LocalizedText GetEmojiName(int id)
		{
			if (id >= 0 && id < EmoteBubbleLoader.EmoteBubbleCount && Lang._emojiNameCache[id] != null)
			{
				return Lang._emojiNameCache[id];
			}
			return LocalizedText.Empty;
		}

		/// <summary> Gets the display name of the provided item type (<see cref="F:Terraria.Item.type" />) as a string. </summary>
		// Token: 0x06000312 RID: 786 RVA: 0x00088AD9 File Offset: 0x00086CD9
		public static string GetItemNameValue(int id)
		{
			return Lang.GetItemName(id).Value;
		}

		/// <summary> Gets the display name of the provided buff type (<see cref="T:Terraria.ID.BuffID" />) as a string. </summary>
		// Token: 0x06000313 RID: 787 RVA: 0x00088AE6 File Offset: 0x00086CE6
		public static string GetBuffName(int id)
		{
			return Lang._buffNameCache[id].Value;
		}

		/// <summary> Gets the description of the provided buff type (<see cref="T:Terraria.ID.BuffID" />) as a string. </summary>
		// Token: 0x06000314 RID: 788 RVA: 0x00088AF4 File Offset: 0x00086CF4
		public static string GetBuffDescription(int id)
		{
			return Lang._buffDescriptionCache[id].Value;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00088B04 File Offset: 0x00086D04
		public static string GetDryadWorldStatusDialog(out bool worldIsEntirelyPure)
		{
			worldIsEntirelyPure = false;
			int tGood = (int)WorldGen.tGood;
			int tEvil = (int)WorldGen.tEvil;
			int tBlood = (int)WorldGen.tBlood;
			string text;
			if (tGood > 0 && tEvil > 0 && tBlood > 0)
			{
				text = Language.GetTextValue("DryadSpecialText.WorldStatusAll", new object[]
				{
					Main.worldName,
					tGood,
					tEvil,
					tBlood
				});
			}
			else if (tGood > 0 && tEvil > 0)
			{
				text = Language.GetTextValue("DryadSpecialText.WorldStatusHallowCorrupt", Main.worldName, tGood, tEvil);
			}
			else if (tGood > 0 && tBlood > 0)
			{
				text = Language.GetTextValue("DryadSpecialText.WorldStatusHallowCrimson", Main.worldName, tGood, tBlood);
			}
			else if (tEvil > 0 && tBlood > 0)
			{
				text = Language.GetTextValue("DryadSpecialText.WorldStatusCorruptCrimson", Main.worldName, tEvil, tBlood);
			}
			else if (tEvil > 0)
			{
				text = Language.GetTextValue("DryadSpecialText.WorldStatusCorrupt", Main.worldName, tEvil);
			}
			else if (tBlood > 0)
			{
				text = Language.GetTextValue("DryadSpecialText.WorldStatusCrimson", Main.worldName, tBlood);
			}
			else
			{
				if (tGood <= 0)
				{
					text = Language.GetTextValue("DryadSpecialText.WorldStatusPure", Main.worldName);
					worldIsEntirelyPure = true;
					return text;
				}
				text = Language.GetTextValue("DryadSpecialText.WorldStatusHallow", Main.worldName, tGood);
			}
			string arg = ((double)tGood * 1.2 >= (double)(tEvil + tBlood) && (double)tGood * 0.8 <= (double)(tEvil + tBlood)) ? Language.GetTextValue("DryadSpecialText.WorldDescriptionBalanced") : ((tGood >= tEvil + tBlood) ? Language.GetTextValue("DryadSpecialText.WorldDescriptionFairyTale") : ((tEvil + tBlood > tGood + 20) ? Language.GetTextValue("DryadSpecialText.WorldDescriptionGrim") : ((tEvil + tBlood <= 5) ? Language.GetTextValue("DryadSpecialText.WorldDescriptionClose") : Language.GetTextValue("DryadSpecialText.WorldDescriptionWork"))));
			return text + " " + arg;
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00088CD2 File Offset: 0x00086ED2
		public static string GetRandomGameTitle()
		{
			return Language.RandomFromCategory("GameTitle", null).Value;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00088CE4 File Offset: 0x00086EE4
		public static string DyeTraderQuestChat(bool gotDye = false)
		{
			object obj = Lang.CreateDialogSubstitutionObject(null);
			LocalizedText[] array = Language.FindAll(Lang.CreateDialogFilter(gotDye ? "DyeTraderSpecialText.HasPlant" : "DyeTraderSpecialText.NoPlant", obj));
			return array[Main.rand.Next(array.Length)].FormatWith(obj);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00088D28 File Offset: 0x00086F28
		public static string AnglerQuestCountChat(NPC npc)
		{
			object obj = Lang.CreateDialogSubstitutionObject(npc);
			return Language.SelectRandom(Lang.CreateDialogFilter("AnglerQuestChatter.", obj), null).FormatWith(obj);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00088D54 File Offset: 0x00086F54
		public static string BartenderHelpText(NPC npc)
		{
			object obj = Lang.CreateDialogSubstitutionObject(npc);
			Player player = Main.player[Main.myPlayer];
			if (player.bartenderQuestLog == 0)
			{
				player.bartenderQuestLog++;
				Item item = new Item();
				item.SetDefaults(3817);
				item.stack = 10;
				item.position = player.Center;
				item = player.GetItem(player.whoAmI, item, GetItemSettings.NPCEntityToPlayerInventorySettings);
				if (item.stack > 0)
				{
					int number = Item.NewItem(new EntitySource_Gift(npc, null), (int)player.position.X, (int)player.position.Y, player.width, player.height, item.type, item.stack, false, 0, true, false);
					if (Main.netMode == 1)
					{
						NetMessage.SendData(21, -1, -1, null, number, 1f, 0f, 0f, 0, 0, 0);
					}
				}
				return Language.GetTextValueWith("BartenderSpecialText.FirstHelp", obj);
			}
			LocalizedText[] array = Language.FindAll(Lang.CreateDialogFilter("BartenderHelpText.", obj));
			if (Main.BartenderHelpTextIndex >= array.Length)
			{
				Main.BartenderHelpTextIndex = 0;
			}
			return array[Main.BartenderHelpTextIndex++].FormatWith(obj);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00088E78 File Offset: 0x00087078
		public static string BartenderChat(NPC npc)
		{
			object obj = Lang.CreateDialogSubstitutionObject(npc);
			if (Main.rand.Next(5) == 0)
			{
				return Language.GetTextValueWith(DD2Event.DownedInvasionT3 ? "BartenderSpecialText.AfterDD2Tier3" : (DD2Event.DownedInvasionT2 ? "BartenderSpecialText.AfterDD2Tier2" : ((!DD2Event.DownedInvasionT1) ? "BartenderSpecialText.BeforeDD2Tier1" : "BartenderSpecialText.AfterDD2Tier1")), obj);
			}
			return Language.SelectRandom(Lang.CreateDialogFilter("BartenderChatter.", obj), null).FormatWith(obj);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00088EE8 File Offset: 0x000870E8
		public static string GolferChat(NPC npc)
		{
			object obj = Lang.CreateDialogSubstitutionObject(npc);
			return Language.SelectRandom(Lang.CreateDialogFilter("GolferChatter.", obj), null).FormatWith(obj);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00088F14 File Offset: 0x00087114
		public static string BestiaryGirlChat(NPC npc)
		{
			object obj = Lang.CreateDialogSubstitutionObject(npc);
			string startsWith = "BestiaryGirlChatter.";
			if (npc.ShouldBestiaryGirlBeLycantrope())
			{
				startsWith = "BestiaryGirlLycantropeChatter.";
			}
			return Language.SelectRandom(Lang.CreateDialogFilter(startsWith, obj), null).FormatWith(obj);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00088F50 File Offset: 0x00087150
		public static string PrincessChat(NPC npc)
		{
			object obj = Lang.CreateDialogSubstitutionObject(npc);
			return Language.SelectRandom(Lang.CreateDialogFilter("PrincessChatter.", obj), null).FormatWith(obj);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00088F7C File Offset: 0x0008717C
		public static string CatChat(NPC npc)
		{
			object obj = Lang.CreateDialogSubstitutionObject(npc);
			return Language.SelectRandom(Lang.CreateDialogFilter("CatChatter.", obj), null).FormatWith(obj);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00088FA8 File Offset: 0x000871A8
		public static string DogChat(NPC npc)
		{
			object obj = Lang.CreateDialogSubstitutionObject(npc);
			return Language.SelectRandom(Lang.CreateDialogFilter("DogChatter.", obj), null).FormatWith(obj);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00088FD4 File Offset: 0x000871D4
		public static string BunnyChat(NPC npc)
		{
			object obj = Lang.CreateDialogSubstitutionObject(npc);
			return Language.SelectRandom(Lang.CreateDialogFilter("BunnyChatter.", obj), null).FormatWith(obj);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00089000 File Offset: 0x00087200
		public static string SlimeChat(NPC npc)
		{
			object obj = Lang.CreateDialogSubstitutionObject(npc);
			string text = "Blue";
			switch (npc.type)
			{
			case 670:
				text = "Blue";
				break;
			case 678:
				text = "Green";
				break;
			case 679:
				text = "Old";
				break;
			case 680:
				text = "Purple";
				break;
			case 681:
				text = "Rainbow";
				break;
			case 682:
				text = "Red";
				break;
			case 683:
				text = "Yellow";
				break;
			case 684:
				text = "Copper";
				break;
			}
			return Language.SelectRandom(Lang.CreateDialogFilter("Slime" + text + "Chatter.", obj), null).FormatWith(obj);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000890CB File Offset: 0x000872CB
		public static string GetNPCHouseBannerText(NPC npc, int bannerStyle)
		{
			if (bannerStyle == 1)
			{
				return Language.GetTextValue("Game.ReservedForNPC", npc.FullName);
			}
			return npc.FullName;
		}

		/// <summary>
		/// Filters LocalizedText objects by checking if their key starts with the provided <paramref name="startsWith" /> parameter and if the conditions embedded into the LocalizedText are all satisfied by the data in <paramref name="substitutions" />. See <see cref="M:Terraria.Localization.LocalizedText.CanFormatWith(System.Object)" /> for more information about conditions embedded in localization text. <br /><br />
		/// When passed into <see cref="M:Terraria.Localization.Language.FindAll(Terraria.Localization.LanguageSearchFilter)" />, all LocalizedText with keys starting with <paramref name="startsWith" /> that satisfy <paramref name="substitutions" /> will be returned. When passed into <see cref="M:Terraria.Localization.Language.SelectRandom(Terraria.Localization.LanguageSearchFilter,Terraria.Utilities.UnifiedRandom)" />, a single random LocalizedText with key starting with <paramref name="startsWith" /> that satisfies <paramref name="substitutions" /> will be returned.<br /><br />
		/// </summary>
		/// <param name="startsWith"></param>
		/// <param name="substitutions"></param>
		/// <returns></returns>
		// Token: 0x06000323 RID: 803 RVA: 0x000890E8 File Offset: 0x000872E8
		public static LanguageSearchFilter CreateDialogFilter(string startsWith, object substitutions)
		{
			return (string key, LocalizedText text) => key.StartsWith(startsWith) && text.CanFormatWith(substitutions);
		}

		/// <summary>
		/// Filters LocalizedText objects by checking if their key starts with the provided <paramref name="startsWith" /> parameter.<br /><br />
		/// When passed into <see cref="M:Terraria.Localization.Language.FindAll(Terraria.Localization.LanguageSearchFilter)" />, all LocalizedText with keys starting with <paramref name="startsWith" /> will be returned. When passed into <see cref="M:Terraria.Localization.Language.SelectRandom(Terraria.Localization.LanguageSearchFilter,Terraria.Utilities.UnifiedRandom)" />, a single random LocalizedText with key starting with <paramref name="startsWith" /> will be returned.<br /><br />
		/// </summary>
		/// <param name="startsWith"></param>
		/// <returns></returns>
		// Token: 0x06000324 RID: 804 RVA: 0x00089108 File Offset: 0x00087308
		public static LanguageSearchFilter CreateDialogFilter(string startsWith)
		{
			return (string key, LocalizedText text) => key.StartsWith(startsWith);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00089124 File Offset: 0x00087324
		public static string AnglerQuestChat(bool turnIn = false)
		{
			object obj = Lang.CreateDialogSubstitutionObject(null);
			if (turnIn)
			{
				return Language.SelectRandom(Lang.CreateDialogFilter("AnglerQuestText.TurnIn_", obj), null).FormatWith(obj);
			}
			if (Main.anglerQuestFinished)
			{
				return Language.SelectRandom(Lang.CreateDialogFilter("AnglerQuestText.NoQuest_", obj), null).FormatWith(obj);
			}
			int id = Main.npcChatCornerItem = Main.anglerQuestItemNetIDs[Main.anglerQuest];
			string modText = ItemLoader.AnglerChat(id);
			if (modText != null)
			{
				return modText;
			}
			return Language.GetTextValueWith("AnglerQuestText.Quest_" + ItemID.Search.GetName(id), obj);
		}

		/// <summary> Gets the display name of the provided projectile type (<see cref="F:Terraria.Projectile.type" />) as a <see cref="T:Terraria.Localization.LocalizedText" />. </summary>
		// Token: 0x06000326 RID: 806 RVA: 0x000891AB File Offset: 0x000873AB
		public static LocalizedText GetProjectileName(int type)
		{
			if (type >= 0 && type < Lang._projectileNameCache.Length && Lang._projectileNameCache[type] != null)
			{
				return Lang._projectileNameCache[type];
			}
			return LocalizedText.Empty;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000891D4 File Offset: 0x000873D4
		private static void FillNameCacheArray<IdClass, IdType>(string category, LocalizedText[] nameCache, bool leaveMissingEntriesBlank = false) where IdType : IConvertible
		{
			for (int i = 0; i < nameCache.Length; i++)
			{
				nameCache[i] = LocalizedText.Empty;
			}
			(from f in typeof(IdClass).GetFields(BindingFlags.Static | BindingFlags.Public)
			where f.FieldType == typeof(IdType)
			select f).ToList<FieldInfo>().ForEach(delegate(FieldInfo field)
			{
				long num = Convert.ToInt64((IdType)((object)field.GetValue(null)));
				if (num >= 0L && num < (long)nameCache.Length)
				{
					nameCache[(int)(checked((IntPtr)num))] = ((!leaveMissingEntriesBlank || Language.Exists(category + "." + field.Name)) ? Language.GetText(category + "." + field.Name) : LocalizedText.Empty);
					return;
				}
				if (field.Name == "None")
				{
					nameCache[(int)(checked((IntPtr)num))] = LocalizedText.Empty;
				}
			});
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00089268 File Offset: 0x00087468
		public static void InitializeLegacyLocalization()
		{
			Lang.FillNameCacheArray<PrefixID, int>("Prefix", Lang.prefix, false);
			for (int i = 0; i < Lang.gen.Length; i++)
			{
				Lang.gen[i] = Language.GetText("LegacyWorldGen." + i.ToString());
			}
			for (int j = 0; j < Lang.menu.Length; j++)
			{
				Lang.menu[j] = Language.GetText("LegacyMenu." + j.ToString());
			}
			for (int k = 0; k < Lang.inter.Length; k++)
			{
				Lang.inter[k] = Language.GetText("LegacyInterface." + k.ToString());
			}
			for (int l = 0; l < Lang.misc.Length; l++)
			{
				Lang.misc[l] = Language.GetText("LegacyMisc." + l.ToString());
			}
			for (int m = 0; m < Lang.mp.Length; m++)
			{
				Lang.mp[m] = Language.GetText("LegacyMultiplayer." + m.ToString());
			}
			for (int n = 0; n < Lang.tip.Length; n++)
			{
				Lang.tip[n] = Language.GetText("LegacyTooltip." + n.ToString());
			}
			for (int num = 0; num < Lang.chestType.Length; num++)
			{
				Lang.chestType[num] = Language.GetText("LegacyChestType." + num.ToString());
			}
			for (int num2 = 0; num2 < Lang.chestType2.Length; num2++)
			{
				Lang.chestType2[num2] = Language.GetText("LegacyChestType2." + num2.ToString());
			}
			for (int num3 = 0; num3 < Lang.dresserType.Length; num3++)
			{
				Lang.dresserType[num3] = Language.GetText("LegacyDresserType." + num3.ToString());
			}
			Lang.FillNameCacheArray<ItemID, short>("ItemName", Lang._itemNameCache, false);
			Lang.FillNameCacheArray<ProjectileID, short>("ProjectileName", Lang._projectileNameCache, false);
			Lang.FillNameCacheArray<NPCID, short>("NPCName", Lang._npcNameCache, false);
			Lang.FillNameCacheArray<BuffID, int>("BuffName", Lang._buffNameCache, false);
			Lang.FillNameCacheArray<BuffID, int>("BuffDescription", Lang._buffDescriptionCache, false);
			Lang.FillNameCacheArray<EmoteID, int>("EmojiName", Lang._emojiNameCache, true);
			for (int num4 = -65; num4 < 0; num4++)
			{
				Lang._negativeNpcNameCache[-num4 - 1] = Lang._npcNameCache[NPCID.FromNetId(num4)];
			}
			Lang._negativeNpcNameCache[0] = Language.GetText("NPCName.Slimeling");
			Lang._negativeNpcNameCache[1] = Language.GetText("NPCName.Slimer2");
			Lang._negativeNpcNameCache[2] = Language.GetText("NPCName.GreenSlime");
			Lang._negativeNpcNameCache[3] = Language.GetText("NPCName.Pinky");
			Lang._negativeNpcNameCache[4] = Language.GetText("NPCName.BabySlime");
			Lang._negativeNpcNameCache[5] = Language.GetText("NPCName.BlackSlime");
			Lang._negativeNpcNameCache[6] = Language.GetText("NPCName.PurpleSlime");
			Lang._negativeNpcNameCache[7] = Language.GetText("NPCName.RedSlime");
			Lang._negativeNpcNameCache[8] = Language.GetText("NPCName.YellowSlime");
			Lang._negativeNpcNameCache[9] = Language.GetText("NPCName.JungleSlime");
			Lang._negativeNpcNameCache[53] = Language.GetText("NPCName.SmallRainZombie");
			Lang._negativeNpcNameCache[54] = Language.GetText("NPCName.BigRainZombie");
			TooltipProcessor processor;
			if ((processor = Lang.<>O.<0>__SupportGlyphs) == null)
			{
				processor = (Lang.<>O.<0>__SupportGlyphs = new TooltipProcessor(Lang.SupportGlyphs));
			}
			ItemTooltip.AddGlobalProcessor(processor);
			for (int num5 = 0; num5 < Lang._itemTooltipCache.Length; num5++)
			{
				Lang._itemTooltipCache[num5] = ItemTooltip.None;
			}
			(from f in typeof(ItemID).GetFields(BindingFlags.Static | BindingFlags.Public)
			where f.FieldType == typeof(short)
			select f).ToList<FieldInfo>().ForEach(delegate(FieldInfo field)
			{
				short num6 = (short)field.GetValue(null);
				if (num6 > 0 && (int)num6 < Lang._itemTooltipCache.Length)
				{
					Lang._itemTooltipCache[(int)num6] = ItemTooltip.FromLanguageKey("ItemTooltip." + field.Name);
				}
			});
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00089640 File Offset: 0x00087840
		public static string SupportGlyphs(string tooltip)
		{
			if (tooltip.Contains("<right>"))
			{
				InputMode inputMode = InputMode.XBoxGamepad;
				if (PlayerInput.UsingGamepad)
				{
					inputMode = InputMode.XBoxGamepadUI;
				}
				if (inputMode == InputMode.XBoxGamepadUI)
				{
					KeyConfiguration keyConfiguration = PlayerInput.CurrentProfile.InputModes[inputMode];
					string text = PlayerInput.BuildCommand("", true, new List<string>[]
					{
						keyConfiguration.KeyStatus["MouseRight"]
					});
					text = text.Replace(": ", "");
					tooltip = tooltip.Replace("<right>", text);
				}
				else
				{
					tooltip = tooltip.Replace("<right>", Language.GetTextValue("Controls.RightClick"));
				}
			}
			if (tooltip.Contains("<left>"))
			{
				InputMode inputMode2 = InputMode.XBoxGamepad;
				if (PlayerInput.UsingGamepad)
				{
					inputMode2 = InputMode.XBoxGamepadUI;
				}
				if (inputMode2 == InputMode.XBoxGamepadUI)
				{
					KeyConfiguration keyConfiguration2 = PlayerInput.CurrentProfile.InputModes[inputMode2];
					string text2 = PlayerInput.BuildCommand("", true, new List<string>[]
					{
						keyConfiguration2.KeyStatus["MouseLeft"]
					});
					text2 = text2.Replace(": ", "");
					tooltip = tooltip.Replace("<left>", text2);
				}
				else
				{
					tooltip = tooltip.Replace("<left>", Language.GetTextValue("Controls.LeftClick"));
				}
			}
			return tooltip;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0008976C File Offset: 0x0008796C
		public static void BuildMapAtlas()
		{
			if (Main.dedServ)
			{
				return;
			}
			Lang._mapLegendCache = new MapLegend(MapHelper.LookupCount());
			for (int i = 0; i < Lang._mapLegendCache.Length; i++)
			{
				Lang._mapLegendCache[i] = LocalizedText.Empty;
			}
			for (int j = 0; j < TileID.Sets.IsATreeTrunk.Length; j++)
			{
				if (TileID.Sets.IsATreeTrunk[j])
				{
					Lang._mapLegendCache[MapHelper.TileToLookup(j, 0)] = Language.GetText("MapObject.Tree");
				}
			}
			Lang._mapLegendCache[MapHelper.TileToLookup(4, 0)] = Lang._itemNameCache[8];
			Lang._mapLegendCache[MapHelper.TileToLookup(4, 1)] = Lang._itemNameCache[8];
			Lang._mapLegendCache[MapHelper.TileToLookup(6, 0)] = Language.GetText("MapObject.Iron");
			Lang._mapLegendCache[MapHelper.TileToLookup(7, 0)] = Language.GetText("MapObject.Copper");
			Lang._mapLegendCache[MapHelper.TileToLookup(8, 0)] = Language.GetText("MapObject.Gold");
			Lang._mapLegendCache[MapHelper.TileToLookup(9, 0)] = Language.GetText("MapObject.Silver");
			Lang._mapLegendCache[MapHelper.TileToLookup(10, 0)] = Language.GetText("MapObject.Door");
			Lang._mapLegendCache[MapHelper.TileToLookup(11, 0)] = Language.GetText("MapObject.Door");
			Lang._mapLegendCache[MapHelper.TileToLookup(12, 0)] = Lang._itemNameCache[29];
			Lang._mapLegendCache[MapHelper.TileToLookup(665, 0)] = Lang._itemNameCache[29];
			Lang._mapLegendCache[MapHelper.TileToLookup(639, 0)] = Lang._itemNameCache[109];
			Lang._mapLegendCache[MapHelper.TileToLookup(630, 0)] = Lang._itemNameCache[5137];
			Lang._mapLegendCache[MapHelper.TileToLookup(631, 0)] = Lang._itemNameCache[5138];
			Lang._mapLegendCache[MapHelper.TileToLookup(13, 0)] = Lang._itemNameCache[31];
			Lang._mapLegendCache[MapHelper.TileToLookup(14, 0)] = Language.GetText("MapObject.Table");
			Lang._mapLegendCache[MapHelper.TileToLookup(469, 0)] = Language.GetText("MapObject.Table");
			Lang._mapLegendCache[MapHelper.TileToLookup(486, 0)] = Lang._itemNameCache[4063];
			Lang._mapLegendCache[MapHelper.TileToLookup(487, 0)] = Lang._itemNameCache[4064];
			Lang._mapLegendCache[MapHelper.TileToLookup(487, 1)] = Lang._itemNameCache[4065];
			Lang._mapLegendCache[MapHelper.TileToLookup(489, 0)] = Lang._itemNameCache[4074];
			Lang._mapLegendCache[MapHelper.TileToLookup(490, 0)] = Lang._itemNameCache[4075];
			Lang._mapLegendCache[MapHelper.TileToLookup(15, 0)] = Language.GetText("MapObject.Chair");
			Lang._mapLegendCache[MapHelper.TileToLookup(15, 1)] = Language.GetText("MapObject.Toilet");
			Lang._mapLegendCache[MapHelper.TileToLookup(16, 0)] = Language.GetText("MapObject.Anvil");
			Lang._mapLegendCache[MapHelper.TileToLookup(17, 0)] = Lang._itemNameCache[33];
			Lang._mapLegendCache[MapHelper.TileToLookup(18, 0)] = Lang._itemNameCache[36];
			Lang._mapLegendCache[MapHelper.TileToLookup(20, 0)] = Language.GetText("MapObject.Sapling");
			Lang._mapLegendCache[MapHelper.TileToLookup(590, 0)] = Language.GetText("MapObject.Sapling");
			Lang._mapLegendCache[MapHelper.TileToLookup(595, 0)] = Language.GetText("MapObject.Sapling");
			Lang._mapLegendCache[MapHelper.TileToLookup(615, 0)] = Language.GetText("MapObject.Sapling");
			Lang._mapLegendCache[MapHelper.TileToLookup(21, 0)] = Lang._itemNameCache[48];
			Lang._mapLegendCache[MapHelper.TileToLookup(467, 0)] = Lang._itemNameCache[48];
			Lang._mapLegendCache[MapHelper.TileToLookup(22, 0)] = Language.GetText("MapObject.Demonite");
			Lang._mapLegendCache[MapHelper.TileToLookup(26, 0)] = Language.GetText("MapObject.DemonAltar");
			Lang._mapLegendCache[MapHelper.TileToLookup(26, 1)] = Language.GetText("MapObject.CrimsonAltar");
			Lang._mapLegendCache[MapHelper.TileToLookup(27, 0)] = Lang._itemNameCache[63];
			Lang._mapLegendCache[MapHelper.TileToLookup(407, 0)] = Language.GetText("MapObject.Fossil");
			Lang._mapLegendCache[MapHelper.TileToLookup(412, 0)] = Lang._itemNameCache[3549];
			Lang._mapLegendCache[MapHelper.TileToLookup(441, 0)] = Lang._itemNameCache[48];
			Lang._mapLegendCache[MapHelper.TileToLookup(468, 0)] = Lang._itemNameCache[48];
			Lang._mapLegendCache[MapHelper.TileToLookup(404, 0)] = Language.GetText("MapObject.DesertFossil");
			Lang._mapLegendCache[MapHelper.TileToLookup(654, 0)] = Lang._itemNameCache[5327];
			for (int k = 0; k < 9; k++)
			{
				Lang._mapLegendCache[MapHelper.TileToLookup(28, k)] = Language.GetText("MapObject.Pot");
				Lang._mapLegendCache[MapHelper.TileToLookup(653, k)] = Language.GetText("MapObject.Pot");
			}
			Lang._mapLegendCache[MapHelper.TileToLookup(37, 0)] = Lang._itemNameCache[116];
			Lang._mapLegendCache[MapHelper.TileToLookup(29, 0)] = Lang._itemNameCache[87];
			Lang._mapLegendCache[MapHelper.TileToLookup(31, 0)] = Lang._itemNameCache[115];
			Lang._mapLegendCache[MapHelper.TileToLookup(31, 1)] = Lang._itemNameCache[3062];
			Lang._mapLegendCache[MapHelper.TileToLookup(32, 0)] = Language.GetText("MapObject.Thorns");
			Lang._mapLegendCache[MapHelper.TileToLookup(33, 0)] = Lang._itemNameCache[105];
			Lang._mapLegendCache[MapHelper.TileToLookup(34, 0)] = Language.GetText("MapObject.Chandelier");
			Lang._mapLegendCache[MapHelper.TileToLookup(35, 0)] = Lang._itemNameCache[1813];
			Lang._mapLegendCache[MapHelper.TileToLookup(36, 0)] = Lang._itemNameCache[1869];
			Lang._mapLegendCache[MapHelper.TileToLookup(476, 0)] = Lang._itemNameCache[4040];
			Lang._mapLegendCache[MapHelper.TileToLookup(42, 0)] = Language.GetText("MapObject.Lantern");
			Lang._mapLegendCache[MapHelper.TileToLookup(48, 0)] = Lang._itemNameCache[147];
			Lang._mapLegendCache[MapHelper.TileToLookup(49, 0)] = Lang._itemNameCache[148];
			Lang._mapLegendCache[MapHelper.TileToLookup(50, 0)] = Lang._itemNameCache[149];
			Lang._mapLegendCache[MapHelper.TileToLookup(51, 0)] = Language.GetText("MapObject.Web");
			Lang._mapLegendCache[MapHelper.TileToLookup(55, 0)] = Lang._itemNameCache[171];
			Lang._mapLegendCache[MapHelper.TileToLookup(454, 0)] = Lang._itemNameCache[3746];
			Lang._mapLegendCache[MapHelper.TileToLookup(455, 0)] = Lang._itemNameCache[3747];
			Lang._mapLegendCache[MapHelper.TileToLookup(452, 0)] = Lang._itemNameCache[3742];
			Lang._mapLegendCache[MapHelper.TileToLookup(456, 0)] = Lang._itemNameCache[3748];
			Lang._mapLegendCache[MapHelper.TileToLookup(453, 0)] = Lang._itemNameCache[3744];
			Lang._mapLegendCache[MapHelper.TileToLookup(453, 1)] = Lang._itemNameCache[3745];
			Lang._mapLegendCache[MapHelper.TileToLookup(453, 2)] = Lang._itemNameCache[3743];
			Lang._mapLegendCache[MapHelper.TileToLookup(573, 0)] = Lang._itemNameCache[4710];
			Lang._mapLegendCache[MapHelper.TileToLookup(63, 0)] = Lang._itemNameCache[177];
			Lang._mapLegendCache[MapHelper.TileToLookup(64, 0)] = Lang._itemNameCache[178];
			Lang._mapLegendCache[MapHelper.TileToLookup(65, 0)] = Lang._itemNameCache[179];
			Lang._mapLegendCache[MapHelper.TileToLookup(66, 0)] = Lang._itemNameCache[180];
			Lang._mapLegendCache[MapHelper.TileToLookup(67, 0)] = Lang._itemNameCache[181];
			Lang._mapLegendCache[MapHelper.TileToLookup(68, 0)] = Lang._itemNameCache[182];
			Lang._mapLegendCache[MapHelper.TileToLookup(566, 0)] = Lang._itemNameCache[999];
			Lang._mapLegendCache[MapHelper.TileToLookup(69, 0)] = Language.GetText("MapObject.Thorn");
			Lang._mapLegendCache[MapHelper.TileToLookup(72, 0)] = Language.GetText("MapObject.GiantMushroom");
			Lang._mapLegendCache[MapHelper.TileToLookup(77, 0)] = Lang._itemNameCache[221];
			Lang._mapLegendCache[MapHelper.TileToLookup(78, 0)] = Lang._itemNameCache[222];
			Lang._mapLegendCache[MapHelper.TileToLookup(79, 0)] = Lang._itemNameCache[224];
			Lang._mapLegendCache[MapHelper.TileToLookup(80, 0)] = Lang._itemNameCache[276];
			Lang._mapLegendCache[MapHelper.TileToLookup(81, 0)] = Lang._itemNameCache[275];
			Lang._mapLegendCache[MapHelper.TileToLookup(82, 0)] = Lang._itemNameCache[313];
			Lang._mapLegendCache[MapHelper.TileToLookup(82, 1)] = Lang._itemNameCache[314];
			Lang._mapLegendCache[MapHelper.TileToLookup(82, 2)] = Lang._itemNameCache[315];
			Lang._mapLegendCache[MapHelper.TileToLookup(82, 3)] = Lang._itemNameCache[316];
			Lang._mapLegendCache[MapHelper.TileToLookup(82, 4)] = Lang._itemNameCache[317];
			Lang._mapLegendCache[MapHelper.TileToLookup(82, 5)] = Lang._itemNameCache[318];
			Lang._mapLegendCache[MapHelper.TileToLookup(82, 6)] = Lang._itemNameCache[2358];
			Lang._mapLegendCache[MapHelper.TileToLookup(83, 0)] = Lang._itemNameCache[313];
			Lang._mapLegendCache[MapHelper.TileToLookup(83, 1)] = Lang._itemNameCache[314];
			Lang._mapLegendCache[MapHelper.TileToLookup(83, 2)] = Lang._itemNameCache[315];
			Lang._mapLegendCache[MapHelper.TileToLookup(83, 3)] = Lang._itemNameCache[316];
			Lang._mapLegendCache[MapHelper.TileToLookup(83, 4)] = Lang._itemNameCache[317];
			Lang._mapLegendCache[MapHelper.TileToLookup(83, 5)] = Lang._itemNameCache[318];
			Lang._mapLegendCache[MapHelper.TileToLookup(83, 6)] = Lang._itemNameCache[2358];
			Lang._mapLegendCache[MapHelper.TileToLookup(84, 0)] = Lang._itemNameCache[313];
			Lang._mapLegendCache[MapHelper.TileToLookup(84, 1)] = Lang._itemNameCache[314];
			Lang._mapLegendCache[MapHelper.TileToLookup(84, 2)] = Lang._itemNameCache[315];
			Lang._mapLegendCache[MapHelper.TileToLookup(84, 3)] = Lang._itemNameCache[316];
			Lang._mapLegendCache[MapHelper.TileToLookup(84, 4)] = Lang._itemNameCache[317];
			Lang._mapLegendCache[MapHelper.TileToLookup(84, 5)] = Lang._itemNameCache[318];
			Lang._mapLegendCache[MapHelper.TileToLookup(84, 6)] = Lang._itemNameCache[2358];
			Lang._mapLegendCache[MapHelper.TileToLookup(85, 0)] = Lang._itemNameCache[321];
			Lang._mapLegendCache[MapHelper.TileToLookup(86, 0)] = Lang._itemNameCache[332];
			Lang._mapLegendCache[MapHelper.TileToLookup(87, 0)] = Lang._itemNameCache[333];
			Lang._mapLegendCache[MapHelper.TileToLookup(88, 0)] = Lang._itemNameCache[334];
			Lang._mapLegendCache[MapHelper.TileToLookup(89, 0)] = Lang._itemNameCache[335];
			Lang._mapLegendCache[MapHelper.TileToLookup(89, 1)] = Lang._itemNameCache[2397];
			Lang._mapLegendCache[MapHelper.TileToLookup(89, 2)] = Lang._itemNameCache[4993];
			Lang._mapLegendCache[MapHelper.TileToLookup(90, 0)] = Lang._itemNameCache[336];
			Lang._mapLegendCache[MapHelper.TileToLookup(91, 0)] = Language.GetText("MapObject.Banner");
			Lang._mapLegendCache[MapHelper.TileToLookup(92, 0)] = Lang._itemNameCache[341];
			Lang._mapLegendCache[MapHelper.TileToLookup(93, 0)] = Language.GetText("MapObject.FloorLamp");
			Lang._mapLegendCache[MapHelper.TileToLookup(94, 0)] = Lang._itemNameCache[352];
			Lang._mapLegendCache[MapHelper.TileToLookup(95, 0)] = Lang._itemNameCache[344];
			Lang._mapLegendCache[MapHelper.TileToLookup(96, 0)] = Lang._itemNameCache[345];
			Lang._mapLegendCache[MapHelper.TileToLookup(97, 0)] = Lang._itemNameCache[346];
			Lang._mapLegendCache[MapHelper.TileToLookup(98, 0)] = Lang._itemNameCache[347];
			Lang._mapLegendCache[MapHelper.TileToLookup(100, 0)] = Lang._itemNameCache[349];
			Lang._mapLegendCache[MapHelper.TileToLookup(101, 0)] = Lang._itemNameCache[354];
			Lang._mapLegendCache[MapHelper.TileToLookup(102, 0)] = Lang._itemNameCache[355];
			Lang._mapLegendCache[MapHelper.TileToLookup(103, 0)] = Lang._itemNameCache[356];
			Lang._mapLegendCache[MapHelper.TileToLookup(104, 0)] = Lang._itemNameCache[359];
			Lang._mapLegendCache[MapHelper.TileToLookup(105, 0)] = Language.GetText("MapObject.Statue");
			Lang._mapLegendCache[MapHelper.TileToLookup(105, 2)] = Language.GetText("MapObject.Vase");
			Lang._mapLegendCache[MapHelper.TileToLookup(106, 0)] = Lang._itemNameCache[363];
			Lang._mapLegendCache[MapHelper.TileToLookup(107, 0)] = Language.GetText("MapObject.Cobalt");
			Lang._mapLegendCache[MapHelper.TileToLookup(108, 0)] = Language.GetText("MapObject.Mythril");
			Lang._mapLegendCache[MapHelper.TileToLookup(111, 0)] = Language.GetText("MapObject.Adamantite");
			Lang._mapLegendCache[MapHelper.TileToLookup(114, 0)] = Lang._itemNameCache[398];
			Lang._mapLegendCache[MapHelper.TileToLookup(125, 0)] = Lang._itemNameCache[487];
			Lang._mapLegendCache[MapHelper.TileToLookup(128, 0)] = Lang._itemNameCache[498];
			Lang._mapLegendCache[MapHelper.TileToLookup(129, 0)] = Lang._itemNameCache[502];
			Lang._mapLegendCache[MapHelper.TileToLookup(129, 1)] = Lang._itemNameCache[4988];
			Lang._mapLegendCache[MapHelper.TileToLookup(132, 0)] = Lang._itemNameCache[513];
			Lang._mapLegendCache[MapHelper.TileToLookup(411, 0)] = Lang._itemNameCache[3545];
			Lang._mapLegendCache[MapHelper.TileToLookup(133, 0)] = Lang._itemNameCache[524];
			Lang._mapLegendCache[MapHelper.TileToLookup(133, 1)] = Lang._itemNameCache[1221];
			Lang._mapLegendCache[MapHelper.TileToLookup(134, 0)] = Lang._itemNameCache[525];
			Lang._mapLegendCache[MapHelper.TileToLookup(134, 1)] = Lang._itemNameCache[1220];
			Lang._mapLegendCache[MapHelper.TileToLookup(136, 0)] = Lang._itemNameCache[538];
			Lang._mapLegendCache[MapHelper.TileToLookup(137, 0)] = Language.GetText("MapObject.Trap");
			Lang._mapLegendCache[MapHelper.TileToLookup(138, 0)] = Lang._itemNameCache[540];
			Lang._mapLegendCache[MapHelper.TileToLookup(139, 0)] = Lang._itemNameCache[576];
			Lang._mapLegendCache[MapHelper.TileToLookup(142, 0)] = Lang._itemNameCache[581];
			Lang._mapLegendCache[MapHelper.TileToLookup(143, 0)] = Lang._itemNameCache[582];
			Lang._mapLegendCache[MapHelper.TileToLookup(144, 0)] = Language.GetText("MapObject.Timer");
			Lang._mapLegendCache[MapHelper.TileToLookup(149, 0)] = Language.GetText("MapObject.ChristmasLight");
			Lang._mapLegendCache[MapHelper.TileToLookup(166, 0)] = Language.GetText("MapObject.Tin");
			Lang._mapLegendCache[MapHelper.TileToLookup(167, 0)] = Language.GetText("MapObject.Lead");
			Lang._mapLegendCache[MapHelper.TileToLookup(168, 0)] = Language.GetText("MapObject.Tungsten");
			Lang._mapLegendCache[MapHelper.TileToLookup(169, 0)] = Language.GetText("MapObject.Platinum");
			Lang._mapLegendCache[MapHelper.TileToLookup(170, 0)] = Language.GetText("MapObject.PineTree");
			Lang._mapLegendCache[MapHelper.TileToLookup(171, 0)] = Lang._itemNameCache[1873];
			Lang._mapLegendCache[MapHelper.TileToLookup(172, 0)] = Language.GetText("MapObject.Sink");
			Lang._mapLegendCache[MapHelper.TileToLookup(173, 0)] = Lang._itemNameCache[349];
			Lang._mapLegendCache[MapHelper.TileToLookup(174, 0)] = Lang._itemNameCache[713];
			Lang._mapLegendCache[MapHelper.TileToLookup(178, 0)] = Lang._itemNameCache[181];
			Lang._mapLegendCache[MapHelper.TileToLookup(178, 1)] = Lang._itemNameCache[180];
			Lang._mapLegendCache[MapHelper.TileToLookup(178, 2)] = Lang._itemNameCache[177];
			Lang._mapLegendCache[MapHelper.TileToLookup(178, 3)] = Lang._itemNameCache[179];
			Lang._mapLegendCache[MapHelper.TileToLookup(178, 4)] = Lang._itemNameCache[178];
			Lang._mapLegendCache[MapHelper.TileToLookup(178, 5)] = Lang._itemNameCache[182];
			Lang._mapLegendCache[MapHelper.TileToLookup(178, 6)] = Lang._itemNameCache[999];
			Lang._mapLegendCache[MapHelper.TileToLookup(191, 0)] = Language.GetText("MapObject.LivingWood");
			Lang._mapLegendCache[MapHelper.TileToLookup(204, 0)] = Language.GetText("MapObject.Crimtane");
			Lang._mapLegendCache[MapHelper.TileToLookup(207, 0)] = Language.GetText("MapObject.WaterFountain");
			Lang._mapLegendCache[MapHelper.TileToLookup(209, 0)] = Lang._itemNameCache[928];
			Lang._mapLegendCache[MapHelper.TileToLookup(211, 0)] = Language.GetText("MapObject.Chlorophyte");
			Lang._mapLegendCache[MapHelper.TileToLookup(212, 0)] = Language.GetText("MapObject.Turret");
			Lang._mapLegendCache[MapHelper.TileToLookup(213, 0)] = Lang._itemNameCache[965];
			Lang._mapLegendCache[MapHelper.TileToLookup(214, 0)] = Lang._itemNameCache[85];
			Lang._mapLegendCache[MapHelper.TileToLookup(215, 0)] = Lang._itemNameCache[966];
			Lang._mapLegendCache[MapHelper.TileToLookup(216, 0)] = Language.GetText("MapObject.Rocket");
			Lang._mapLegendCache[MapHelper.TileToLookup(217, 0)] = Lang._itemNameCache[995];
			Lang._mapLegendCache[MapHelper.TileToLookup(218, 0)] = Lang._itemNameCache[996];
			Lang._mapLegendCache[MapHelper.TileToLookup(219, 0)] = Language.GetText("MapObject.SiltExtractinator");
			Lang._mapLegendCache[MapHelper.TileToLookup(642, 0)] = Language.GetText("MapObject.ChlorophyteExtractinator");
			Lang._mapLegendCache[MapHelper.TileToLookup(220, 0)] = Lang._itemNameCache[998];
			Lang._mapLegendCache[MapHelper.TileToLookup(221, 0)] = Language.GetText("MapObject.Palladium");
			Lang._mapLegendCache[MapHelper.TileToLookup(222, 0)] = Language.GetText("MapObject.Orichalcum");
			Lang._mapLegendCache[MapHelper.TileToLookup(223, 0)] = Language.GetText("MapObject.Titanium");
			Lang._mapLegendCache[MapHelper.TileToLookup(227, 0)] = Lang._itemNameCache[1107];
			Lang._mapLegendCache[MapHelper.TileToLookup(227, 1)] = Lang._itemNameCache[1108];
			Lang._mapLegendCache[MapHelper.TileToLookup(227, 2)] = Lang._itemNameCache[1109];
			Lang._mapLegendCache[MapHelper.TileToLookup(227, 3)] = Lang._itemNameCache[1110];
			Lang._mapLegendCache[MapHelper.TileToLookup(227, 4)] = Lang._itemNameCache[1111];
			Lang._mapLegendCache[MapHelper.TileToLookup(227, 5)] = Lang._itemNameCache[1112];
			Lang._mapLegendCache[MapHelper.TileToLookup(227, 6)] = Lang._itemNameCache[1113];
			Lang._mapLegendCache[MapHelper.TileToLookup(227, 7)] = Lang._itemNameCache[1114];
			Lang._mapLegendCache[MapHelper.TileToLookup(227, 8)] = Lang._itemNameCache[3385];
			Lang._mapLegendCache[MapHelper.TileToLookup(227, 9)] = Lang._itemNameCache[3386];
			Lang._mapLegendCache[MapHelper.TileToLookup(227, 10)] = Lang._itemNameCache[3387];
			Lang._mapLegendCache[MapHelper.TileToLookup(227, 11)] = Lang._itemNameCache[3388];
			Lang._mapLegendCache[MapHelper.TileToLookup(228, 0)] = Lang._itemNameCache[1120];
			Lang._mapLegendCache[MapHelper.TileToLookup(231, 0)] = Language.GetText("MapObject.Larva");
			Lang._mapLegendCache[MapHelper.TileToLookup(232, 0)] = Lang._itemNameCache[1150];
			Lang._mapLegendCache[MapHelper.TileToLookup(235, 0)] = Lang._itemNameCache[1263];
			Lang._mapLegendCache[MapHelper.TileToLookup(624, 0)] = Lang._itemNameCache[5114];
			Lang._mapLegendCache[MapHelper.TileToLookup(656, 0)] = Lang._itemNameCache[5333];
			Lang._mapLegendCache[MapHelper.TileToLookup(236, 0)] = Lang._itemNameCache[1291];
			Lang._mapLegendCache[MapHelper.TileToLookup(237, 0)] = Lang._itemNameCache[1292];
			Lang._mapLegendCache[MapHelper.TileToLookup(238, 0)] = Language.GetText("MapObject.PlanterasBulb");
			Lang._mapLegendCache[MapHelper.TileToLookup(239, 0)] = Language.GetText("MapObject.MetalBar");
			Lang._mapLegendCache[MapHelper.TileToLookup(240, 0)] = Language.GetText("MapObject.Trophy");
			Lang._mapLegendCache[MapHelper.TileToLookup(240, 1)] = Language.GetText("MapObject.Painting");
			Lang._mapLegendCache[MapHelper.TileToLookup(240, 2)] = Lang._npcNameCache[21];
			Lang._mapLegendCache[MapHelper.TileToLookup(240, 3)] = Language.GetText("MapObject.ItemRack");
			Lang._mapLegendCache[MapHelper.TileToLookup(240, 4)] = Lang._itemNameCache[2442];
			Lang._mapLegendCache[MapHelper.TileToLookup(241, 0)] = Lang._itemNameCache[1417];
			Lang._mapLegendCache[MapHelper.TileToLookup(242, 0)] = Language.GetText("MapObject.Painting");
			Lang._mapLegendCache[MapHelper.TileToLookup(242, 1)] = Language.GetText("MapObject.AnimalSkin");
			Lang._mapLegendCache[MapHelper.TileToLookup(243, 0)] = Lang._itemNameCache[1430];
			Lang._mapLegendCache[MapHelper.TileToLookup(244, 0)] = Lang._itemNameCache[1449];
			Lang._mapLegendCache[MapHelper.TileToLookup(245, 0)] = Language.GetText("MapObject.Picture");
			Lang._mapLegendCache[MapHelper.TileToLookup(246, 0)] = Language.GetText("MapObject.Picture");
			Lang._mapLegendCache[MapHelper.TileToLookup(247, 0)] = Lang._itemNameCache[1551];
			Lang._mapLegendCache[MapHelper.TileToLookup(254, 0)] = Lang._itemNameCache[1725];
			Lang._mapLegendCache[MapHelper.TileToLookup(269, 0)] = Lang._itemNameCache[1989];
			Lang._mapLegendCache[MapHelper.TileToLookup(475, 0)] = Lang._itemNameCache[3977];
			Lang._mapLegendCache[MapHelper.TileToLookup(597, 0)] = Lang._itemNameCache[4876];
			Lang._mapLegendCache[MapHelper.TileToLookup(597, 1)] = Lang._itemNameCache[4875];
			Lang._mapLegendCache[MapHelper.TileToLookup(597, 2)] = Lang._itemNameCache[4916];
			Lang._mapLegendCache[MapHelper.TileToLookup(597, 3)] = Lang._itemNameCache[4917];
			Lang._mapLegendCache[MapHelper.TileToLookup(597, 4)] = Lang._itemNameCache[4918];
			Lang._mapLegendCache[MapHelper.TileToLookup(597, 5)] = Lang._itemNameCache[4919];
			Lang._mapLegendCache[MapHelper.TileToLookup(597, 6)] = Lang._itemNameCache[4920];
			Lang._mapLegendCache[MapHelper.TileToLookup(597, 7)] = Lang._itemNameCache[4921];
			Lang._mapLegendCache[MapHelper.TileToLookup(597, 8)] = Lang._itemNameCache[4951];
			Lang._mapLegendCache[MapHelper.TileToLookup(617, 0)] = Language.GetText("MapObject.Relic");
			Lang._mapLegendCache[MapHelper.TileToLookup(621, 0)] = Lang._itemNameCache[3750];
			Lang._mapLegendCache[MapHelper.TileToLookup(622, 0)] = Lang._itemNameCache[5008];
			Lang._mapLegendCache[MapHelper.TileToLookup(270, 0)] = Lang._itemNameCache[1993];
			Lang._mapLegendCache[MapHelper.TileToLookup(271, 0)] = Lang._itemNameCache[2005];
			Lang._mapLegendCache[MapHelper.TileToLookup(581, 0)] = Lang._itemNameCache[4848];
			Lang._mapLegendCache[MapHelper.TileToLookup(660, 0)] = Lang._itemNameCache[5351];
			Lang._mapLegendCache[MapHelper.TileToLookup(275, 0)] = Lang._itemNameCache[2162];
			Lang._mapLegendCache[MapHelper.TileToLookup(276, 0)] = Lang._itemNameCache[2163];
			Lang._mapLegendCache[MapHelper.TileToLookup(277, 0)] = Lang._itemNameCache[2164];
			Lang._mapLegendCache[MapHelper.TileToLookup(278, 0)] = Lang._itemNameCache[2165];
			Lang._mapLegendCache[MapHelper.TileToLookup(279, 0)] = Lang._itemNameCache[2166];
			Lang._mapLegendCache[MapHelper.TileToLookup(280, 0)] = Lang._itemNameCache[2167];
			Lang._mapLegendCache[MapHelper.TileToLookup(281, 0)] = Lang._itemNameCache[2168];
			Lang._mapLegendCache[MapHelper.TileToLookup(632, 0)] = Lang._itemNameCache[5213];
			Lang._mapLegendCache[MapHelper.TileToLookup(640, 0)] = Lang._itemNameCache[5301];
			Lang._mapLegendCache[MapHelper.TileToLookup(643, 0)] = Lang._itemNameCache[5314];
			Lang._mapLegendCache[MapHelper.TileToLookup(644, 0)] = Lang._itemNameCache[5315];
			Lang._mapLegendCache[MapHelper.TileToLookup(645, 0)] = Lang._itemNameCache[5316];
			Lang._mapLegendCache[MapHelper.TileToLookup(282, 0)] = Lang._itemNameCache[250];
			Lang._mapLegendCache[MapHelper.TileToLookup(543, 0)] = Lang._itemNameCache[4398];
			Lang._mapLegendCache[MapHelper.TileToLookup(598, 0)] = Lang._itemNameCache[4880];
			Lang._mapLegendCache[MapHelper.TileToLookup(413, 0)] = Lang._itemNameCache[3565];
			Lang._mapLegendCache[MapHelper.TileToLookup(283, 0)] = Lang._itemNameCache[2172];
			Lang._mapLegendCache[MapHelper.TileToLookup(285, 0)] = Lang._itemNameCache[2174];
			Lang._mapLegendCache[MapHelper.TileToLookup(286, 0)] = Lang._itemNameCache[2175];
			Lang._mapLegendCache[MapHelper.TileToLookup(582, 0)] = Lang._itemNameCache[4850];
			Lang._mapLegendCache[MapHelper.TileToLookup(287, 0)] = Lang._itemNameCache[2177];
			Lang._mapLegendCache[MapHelper.TileToLookup(288, 0)] = Lang._itemNameCache[2178];
			Lang._mapLegendCache[MapHelper.TileToLookup(289, 0)] = Lang._itemNameCache[2179];
			Lang._mapLegendCache[MapHelper.TileToLookup(290, 0)] = Lang._itemNameCache[2180];
			Lang._mapLegendCache[MapHelper.TileToLookup(291, 0)] = Lang._itemNameCache[2181];
			Lang._mapLegendCache[MapHelper.TileToLookup(292, 0)] = Lang._itemNameCache[2182];
			Lang._mapLegendCache[MapHelper.TileToLookup(293, 0)] = Lang._itemNameCache[2183];
			Lang._mapLegendCache[MapHelper.TileToLookup(294, 0)] = Lang._itemNameCache[2184];
			Lang._mapLegendCache[MapHelper.TileToLookup(295, 0)] = Lang._itemNameCache[2185];
			Lang._mapLegendCache[MapHelper.TileToLookup(580, 0)] = Lang._itemNameCache[4846];
			Lang._mapLegendCache[MapHelper.TileToLookup(620, 0)] = Lang._itemNameCache[4964];
			Lang._mapLegendCache[MapHelper.TileToLookup(619, 0)] = Lang._itemNameCache[4963];
			Lang._mapLegendCache[MapHelper.TileToLookup(296, 0)] = Lang._itemNameCache[2186];
			Lang._mapLegendCache[MapHelper.TileToLookup(297, 0)] = Lang._itemNameCache[2187];
			Lang._mapLegendCache[MapHelper.TileToLookup(298, 0)] = Lang._itemNameCache[2190];
			Lang._mapLegendCache[MapHelper.TileToLookup(299, 0)] = Lang._itemNameCache[2191];
			Lang._mapLegendCache[MapHelper.TileToLookup(300, 0)] = Lang._itemNameCache[2192];
			Lang._mapLegendCache[MapHelper.TileToLookup(301, 0)] = Lang._itemNameCache[2193];
			Lang._mapLegendCache[MapHelper.TileToLookup(302, 0)] = Lang._itemNameCache[2194];
			Lang._mapLegendCache[MapHelper.TileToLookup(303, 0)] = Lang._itemNameCache[2195];
			Lang._mapLegendCache[MapHelper.TileToLookup(304, 0)] = Lang._itemNameCache[2196];
			Lang._mapLegendCache[MapHelper.TileToLookup(305, 0)] = Lang._itemNameCache[2197];
			Lang._mapLegendCache[MapHelper.TileToLookup(306, 0)] = Lang._itemNameCache[2198];
			Lang._mapLegendCache[MapHelper.TileToLookup(307, 0)] = Lang._itemNameCache[2203];
			Lang._mapLegendCache[MapHelper.TileToLookup(308, 0)] = Lang._itemNameCache[2204];
			Lang._mapLegendCache[MapHelper.TileToLookup(309, 0)] = Lang._itemNameCache[2206];
			Lang._mapLegendCache[MapHelper.TileToLookup(310, 0)] = Lang._itemNameCache[2207];
			Lang._mapLegendCache[MapHelper.TileToLookup(391, 0)] = Lang._itemNameCache[3254];
			Lang._mapLegendCache[MapHelper.TileToLookup(316, 0)] = Lang._itemNameCache[2439];
			Lang._mapLegendCache[MapHelper.TileToLookup(317, 0)] = Lang._itemNameCache[2440];
			Lang._mapLegendCache[MapHelper.TileToLookup(318, 0)] = Lang._itemNameCache[2441];
			Lang._mapLegendCache[MapHelper.TileToLookup(319, 0)] = Lang._itemNameCache[2490];
			Lang._mapLegendCache[MapHelper.TileToLookup(320, 0)] = Lang._itemNameCache[2496];
			Lang._mapLegendCache[MapHelper.TileToLookup(323, 0)] = Language.GetText("MapObject.PalmTree");
			Lang._mapLegendCache[MapHelper.TileToLookup(314, 0)] = Lang._itemNameCache[2340];
			Lang._mapLegendCache[MapHelper.TileToLookup(353, 0)] = Lang._itemNameCache[2996];
			Lang._mapLegendCache[MapHelper.TileToLookup(354, 0)] = Lang._itemNameCache[2999];
			Lang._mapLegendCache[MapHelper.TileToLookup(355, 0)] = Lang._itemNameCache[3000];
			Lang._mapLegendCache[MapHelper.TileToLookup(464, 0)] = Lang._itemNameCache[3814];
			Lang._mapLegendCache[MapHelper.TileToLookup(356, 0)] = Lang._itemNameCache[3064];
			Lang._mapLegendCache[MapHelper.TileToLookup(663, 0)] = Lang._itemNameCache[5381];
			Lang._mapLegendCache[MapHelper.TileToLookup(358, 0)] = Lang._itemNameCache[3070];
			Lang._mapLegendCache[MapHelper.TileToLookup(359, 0)] = Lang._itemNameCache[3071];
			Lang._mapLegendCache[MapHelper.TileToLookup(360, 0)] = Lang._itemNameCache[3072];
			Lang._mapLegendCache[MapHelper.TileToLookup(361, 0)] = Lang._itemNameCache[3073];
			Lang._mapLegendCache[MapHelper.TileToLookup(362, 0)] = Lang._itemNameCache[3074];
			Lang._mapLegendCache[MapHelper.TileToLookup(363, 0)] = Lang._itemNameCache[3075];
			Lang._mapLegendCache[MapHelper.TileToLookup(364, 0)] = Lang._itemNameCache[3076];
			Lang._mapLegendCache[MapHelper.TileToLookup(414, 0)] = Lang._itemNameCache[3566];
			Lang._mapLegendCache[MapHelper.TileToLookup(521, 0)] = Lang._itemNameCache[4327];
			Lang._mapLegendCache[MapHelper.TileToLookup(522, 0)] = Lang._itemNameCache[4328];
			Lang._mapLegendCache[MapHelper.TileToLookup(523, 0)] = Lang._itemNameCache[4329];
			Lang._mapLegendCache[MapHelper.TileToLookup(524, 0)] = Lang._itemNameCache[4330];
			Lang._mapLegendCache[MapHelper.TileToLookup(525, 0)] = Lang._itemNameCache[4331];
			Lang._mapLegendCache[MapHelper.TileToLookup(526, 0)] = Lang._itemNameCache[4332];
			Lang._mapLegendCache[MapHelper.TileToLookup(527, 0)] = Lang._itemNameCache[4333];
			Lang._mapLegendCache[MapHelper.TileToLookup(542, 0)] = Lang._itemNameCache[4396];
			Lang._mapLegendCache[MapHelper.TileToLookup(365, 0)] = Lang._itemNameCache[3077];
			Lang._mapLegendCache[MapHelper.TileToLookup(366, 0)] = Lang._itemNameCache[3078];
			Lang._mapLegendCache[MapHelper.TileToLookup(373, 0)] = Language.GetText("MapObject.DrippingWater");
			Lang._mapLegendCache[MapHelper.TileToLookup(374, 0)] = Language.GetText("MapObject.DrippingLava");
			Lang._mapLegendCache[MapHelper.TileToLookup(375, 0)] = Language.GetText("MapObject.DrippingHoney");
			Lang._mapLegendCache[MapHelper.TileToLookup(461, 0)] = Language.GetText("MapObject.SandFlow");
			Lang._mapLegendCache[MapHelper.TileToLookup(461, 1)] = Language.GetText("MapObject.SandFlow");
			Lang._mapLegendCache[MapHelper.TileToLookup(461, 2)] = Language.GetText("MapObject.SandFlow");
			Lang._mapLegendCache[MapHelper.TileToLookup(461, 3)] = Language.GetText("MapObject.SandFlow");
			Lang._mapLegendCache[MapHelper.TileToLookup(377, 0)] = Lang._itemNameCache[3198];
			Lang._mapLegendCache[MapHelper.TileToLookup(372, 0)] = Lang._itemNameCache[3117];
			Lang._mapLegendCache[MapHelper.TileToLookup(646, 0)] = Lang._itemNameCache[5322];
			Lang._mapLegendCache[MapHelper.TileToLookup(425, 0)] = Lang._itemNameCache[3617];
			Lang._mapLegendCache[MapHelper.TileToLookup(420, 0)] = Lang._itemNameCache[3603];
			Lang._mapLegendCache[MapHelper.TileToLookup(420, 1)] = Lang._itemNameCache[3604];
			Lang._mapLegendCache[MapHelper.TileToLookup(420, 2)] = Lang._itemNameCache[3605];
			Lang._mapLegendCache[MapHelper.TileToLookup(420, 3)] = Lang._itemNameCache[3606];
			Lang._mapLegendCache[MapHelper.TileToLookup(420, 4)] = Lang._itemNameCache[3607];
			Lang._mapLegendCache[MapHelper.TileToLookup(420, 5)] = Lang._itemNameCache[3608];
			Lang._mapLegendCache[MapHelper.TileToLookup(423, 0)] = Lang._itemNameCache[3613];
			Lang._mapLegendCache[MapHelper.TileToLookup(423, 1)] = Lang._itemNameCache[3614];
			Lang._mapLegendCache[MapHelper.TileToLookup(423, 2)] = Lang._itemNameCache[3615];
			Lang._mapLegendCache[MapHelper.TileToLookup(423, 3)] = Lang._itemNameCache[3726];
			Lang._mapLegendCache[MapHelper.TileToLookup(423, 4)] = Lang._itemNameCache[3727];
			Lang._mapLegendCache[MapHelper.TileToLookup(423, 5)] = Lang._itemNameCache[3728];
			Lang._mapLegendCache[MapHelper.TileToLookup(423, 6)] = Lang._itemNameCache[3729];
			Lang._mapLegendCache[MapHelper.TileToLookup(440, 0)] = Lang._itemNameCache[3644];
			Lang._mapLegendCache[MapHelper.TileToLookup(440, 1)] = Lang._itemNameCache[3645];
			Lang._mapLegendCache[MapHelper.TileToLookup(440, 2)] = Lang._itemNameCache[3646];
			Lang._mapLegendCache[MapHelper.TileToLookup(440, 3)] = Lang._itemNameCache[3647];
			Lang._mapLegendCache[MapHelper.TileToLookup(440, 4)] = Lang._itemNameCache[3648];
			Lang._mapLegendCache[MapHelper.TileToLookup(440, 5)] = Lang._itemNameCache[3649];
			Lang._mapLegendCache[MapHelper.TileToLookup(440, 6)] = Lang._itemNameCache[3650];
			Lang._mapLegendCache[MapHelper.TileToLookup(443, 0)] = Lang._itemNameCache[3722];
			Lang._mapLegendCache[MapHelper.TileToLookup(429, 0)] = Lang._itemNameCache[3629];
			Lang._mapLegendCache[MapHelper.TileToLookup(424, 0)] = Lang._itemNameCache[3616];
			Lang._mapLegendCache[MapHelper.TileToLookup(444, 0)] = Language.GetText("MapObject.BeeHive");
			Lang._mapLegendCache[MapHelper.TileToLookup(466, 0)] = Lang._itemNameCache[3816];
			Lang._mapLegendCache[MapHelper.TileToLookup(463, 0)] = Lang._itemNameCache[3813];
			Lang._mapLegendCache[MapHelper.TileToLookup(491, 0)] = Lang._itemNameCache[4076];
			Lang._mapLegendCache[MapHelper.TileToLookup(494, 0)] = Lang._itemNameCache[4089];
			Lang._mapLegendCache[MapHelper.TileToLookup(499, 0)] = Lang._itemNameCache[4142];
			Lang._mapLegendCache[MapHelper.TileToLookup(488, 0)] = Language.GetText("MapObject.FallenLog");
			Lang._mapLegendCache[MapHelper.TileToLookup(505, 0)] = Lang._itemNameCache[4275];
			Lang._mapLegendCache[MapHelper.TileToLookup(521, 0)] = Lang._itemNameCache[4327];
			Lang._mapLegendCache[MapHelper.TileToLookup(522, 0)] = Lang._itemNameCache[4328];
			Lang._mapLegendCache[MapHelper.TileToLookup(523, 0)] = Lang._itemNameCache[4329];
			Lang._mapLegendCache[MapHelper.TileToLookup(524, 0)] = Lang._itemNameCache[4330];
			Lang._mapLegendCache[MapHelper.TileToLookup(525, 0)] = Lang._itemNameCache[4331];
			Lang._mapLegendCache[MapHelper.TileToLookup(526, 0)] = Lang._itemNameCache[4332];
			Lang._mapLegendCache[MapHelper.TileToLookup(527, 0)] = Lang._itemNameCache[4333];
			Lang._mapLegendCache[MapHelper.TileToLookup(531, 0)] = Language.GetText("MapObject.Statue");
			Lang._mapLegendCache[MapHelper.TileToLookup(349, 0)] = Language.GetText("MapObject.Statue");
			Lang._mapLegendCache[MapHelper.TileToLookup(532, 0)] = Lang._itemNameCache[4364];
			Lang._mapLegendCache[MapHelper.TileToLookup(538, 0)] = Lang._itemNameCache[4380];
			Lang._mapLegendCache[MapHelper.TileToLookup(544, 0)] = Lang._itemNameCache[4399];
			Lang._mapLegendCache[MapHelper.TileToLookup(629, 0)] = Lang._itemNameCache[5133];
			Lang._mapLegendCache[MapHelper.TileToLookup(506, 0)] = Lang._itemNameCache[4276];
			Lang._mapLegendCache[MapHelper.TileToLookup(545, 0)] = Lang._itemNameCache[4420];
			Lang._mapLegendCache[MapHelper.TileToLookup(550, 0)] = Lang._itemNameCache[4461];
			Lang._mapLegendCache[MapHelper.TileToLookup(551, 0)] = Lang._itemNameCache[4462];
			Lang._mapLegendCache[MapHelper.TileToLookup(533, 0)] = Lang._itemNameCache[4376];
			Lang._mapLegendCache[MapHelper.TileToLookup(553, 0)] = Lang._itemNameCache[4473];
			Lang._mapLegendCache[MapHelper.TileToLookup(554, 0)] = Lang._itemNameCache[4474];
			Lang._mapLegendCache[MapHelper.TileToLookup(555, 0)] = Lang._itemNameCache[4475];
			Lang._mapLegendCache[MapHelper.TileToLookup(556, 0)] = Lang._itemNameCache[4476];
			Lang._mapLegendCache[MapHelper.TileToLookup(558, 0)] = Lang._itemNameCache[4481];
			Lang._mapLegendCache[MapHelper.TileToLookup(559, 0)] = Lang._itemNameCache[4483];
			Lang._mapLegendCache[MapHelper.TileToLookup(599, 0)] = Lang._itemNameCache[4882];
			Lang._mapLegendCache[MapHelper.TileToLookup(600, 0)] = Lang._itemNameCache[4883];
			Lang._mapLegendCache[MapHelper.TileToLookup(601, 0)] = Lang._itemNameCache[4884];
			Lang._mapLegendCache[MapHelper.TileToLookup(602, 0)] = Lang._itemNameCache[4885];
			Lang._mapLegendCache[MapHelper.TileToLookup(603, 0)] = Lang._itemNameCache[4886];
			Lang._mapLegendCache[MapHelper.TileToLookup(604, 0)] = Lang._itemNameCache[4887];
			Lang._mapLegendCache[MapHelper.TileToLookup(605, 0)] = Lang._itemNameCache[4888];
			Lang._mapLegendCache[MapHelper.TileToLookup(606, 0)] = Lang._itemNameCache[4889];
			Lang._mapLegendCache[MapHelper.TileToLookup(607, 0)] = Lang._itemNameCache[4890];
			Lang._mapLegendCache[MapHelper.TileToLookup(608, 0)] = Lang._itemNameCache[4891];
			Lang._mapLegendCache[MapHelper.TileToLookup(609, 0)] = Lang._itemNameCache[4892];
			Lang._mapLegendCache[MapHelper.TileToLookup(610, 0)] = Lang._itemNameCache[4893];
			Lang._mapLegendCache[MapHelper.TileToLookup(611, 0)] = Lang._itemNameCache[4894];
			Lang._mapLegendCache[MapHelper.TileToLookup(612, 0)] = Lang._itemNameCache[4895];
			Lang._mapLegendCache[MapHelper.TileToLookup(560, 0)] = Lang._itemNameCache[4599];
			Lang._mapLegendCache[MapHelper.TileToLookup(560, 1)] = Lang._itemNameCache[4600];
			Lang._mapLegendCache[MapHelper.TileToLookup(560, 2)] = Lang._itemNameCache[4601];
			Lang._mapLegendCache[MapHelper.TileToLookup(568, 0)] = Lang._itemNameCache[4655];
			Lang._mapLegendCache[MapHelper.TileToLookup(569, 0)] = Lang._itemNameCache[4656];
			Lang._mapLegendCache[MapHelper.TileToLookup(570, 0)] = Lang._itemNameCache[4657];
			Lang._mapLegendCache[MapHelper.TileToLookup(572, 0)] = Lang._itemNameCache[4695];
			Lang._mapLegendCache[MapHelper.TileToLookup(572, 1)] = Lang._itemNameCache[4696];
			Lang._mapLegendCache[MapHelper.TileToLookup(572, 2)] = Lang._itemNameCache[4697];
			Lang._mapLegendCache[MapHelper.TileToLookup(572, 3)] = Lang._itemNameCache[4698];
			Lang._mapLegendCache[MapHelper.TileToLookup(572, 4)] = Lang._itemNameCache[4699];
			Lang._mapLegendCache[MapHelper.TileToLookup(572, 5)] = Lang._itemNameCache[4700];
			Lang._mapLegendCache[MapHelper.TileToLookup(497, 0)] = Language.GetText("MapObject.Toilet");
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0008C960 File Offset: 0x0008AB60
		public static NetworkText CreateDeathMessage(string deadPlayerName, int plr = -1, int npc = -1, int proj = -1, int other = -1, int projType = 0, int plrItemType = 0)
		{
			NetworkText networkText = NetworkText.Empty;
			NetworkText networkText2 = NetworkText.Empty;
			NetworkText networkText3 = NetworkText.Empty;
			NetworkText networkText4 = NetworkText.Empty;
			if (proj >= 0)
			{
				networkText = NetworkText.FromKey(Lang.GetProjectileName(projType).Key, Array.Empty<object>());
			}
			if (npc >= 0)
			{
				networkText2 = Main.npc[npc].GetGivenOrTypeNetName();
			}
			if (plr >= 0 && plr < 255)
			{
				networkText3 = NetworkText.FromLiteral(Main.player[plr].name);
			}
			if (plrItemType >= 0)
			{
				networkText4 = NetworkText.FromKey(Lang.GetItemName(plrItemType).Key, Array.Empty<object>());
			}
			bool flag = networkText != NetworkText.Empty;
			bool flag2 = plr >= 0 && plr < 255;
			bool flag3 = networkText2 != NetworkText.Empty;
			NetworkText result = NetworkText.Empty;
			NetworkText empty = NetworkText.Empty;
			empty = NetworkText.FromKey(Language.RandomFromCategory("DeathTextGeneric", null).Key, new object[]
			{
				deadPlayerName,
				Main.worldName
			});
			if (flag2)
			{
				result = NetworkText.FromKey("DeathSource.Player", new object[]
				{
					empty,
					networkText3,
					flag ? networkText : networkText4
				});
			}
			else if (flag3)
			{
				result = NetworkText.FromKey("DeathSource.NPC", new object[]
				{
					empty,
					networkText2
				});
			}
			else if (flag)
			{
				result = NetworkText.FromKey("DeathSource.Projectile", new object[]
				{
					empty,
					networkText
				});
			}
			else
			{
				switch (other)
				{
				case 0:
					result = NetworkText.FromKey("DeathText.Fell_" + (Main.rand.Next(9) + 1).ToString(), new object[]
					{
						deadPlayerName
					});
					break;
				case 1:
					result = NetworkText.FromKey("DeathText.Drowned_" + (Main.rand.Next(7) + 1).ToString(), new object[]
					{
						deadPlayerName
					});
					break;
				case 2:
					result = NetworkText.FromKey("DeathText.Lava_" + (Main.rand.Next(5) + 1).ToString(), new object[]
					{
						deadPlayerName
					});
					break;
				case 3:
					result = NetworkText.FromKey("DeathText.Default", new object[]
					{
						empty
					});
					break;
				case 4:
					result = NetworkText.FromKey("DeathText.Slain", new object[]
					{
						deadPlayerName
					});
					break;
				case 5:
					result = NetworkText.FromKey("DeathText.Petrified_" + (Main.rand.Next(4) + 1).ToString(), new object[]
					{
						deadPlayerName
					});
					break;
				case 6:
					result = NetworkText.FromKey("DeathText.Stabbed", new object[]
					{
						deadPlayerName
					});
					break;
				case 7:
					result = NetworkText.FromKey("DeathText.Suffocated_" + (Main.rand.Next(2) + 1).ToString(), new object[]
					{
						deadPlayerName
					});
					break;
				case 8:
					result = NetworkText.FromKey("DeathText.Burned_" + (Main.rand.Next(4) + 1).ToString(), new object[]
					{
						deadPlayerName
					});
					break;
				case 9:
					result = NetworkText.FromKey("DeathText.Poisoned", new object[]
					{
						deadPlayerName
					});
					break;
				case 10:
					result = NetworkText.FromKey("DeathText.Electrocuted_" + (Main.rand.Next(4) + 1).ToString(), new object[]
					{
						deadPlayerName
					});
					break;
				case 11:
					result = NetworkText.FromKey("DeathText.TriedToEscape", new object[]
					{
						deadPlayerName
					});
					break;
				case 12:
					result = NetworkText.FromKey("DeathText.WasLicked_" + (Main.rand.Next(2) + 1).ToString(), new object[]
					{
						deadPlayerName
					});
					break;
				case 13:
					result = NetworkText.FromKey("DeathText.Teleport_1", new object[]
					{
						deadPlayerName
					});
					break;
				case 14:
					result = NetworkText.FromKey("DeathText.Teleport_2_Male", new object[]
					{
						deadPlayerName
					});
					break;
				case 15:
					result = NetworkText.FromKey("DeathText.Teleport_2_Female", new object[]
					{
						deadPlayerName
					});
					break;
				case 16:
					result = NetworkText.FromKey("DeathText.Inferno", new object[]
					{
						deadPlayerName
					});
					break;
				case 17:
					result = NetworkText.FromKey("DeathText.DiedInTheDark", new object[]
					{
						deadPlayerName
					});
					break;
				case 18:
					result = NetworkText.FromKey("DeathText.Starved_" + (Main.rand.Next(3) + 1).ToString(), new object[]
					{
						deadPlayerName
					});
					break;
				case 19:
					result = NetworkText.FromKey("DeathText.Space_" + (Main.rand.Next(5) + 1).ToString(), new object[]
					{
						deadPlayerName,
						Main.worldName
					});
					break;
				default:
					if (other != 254)
					{
						if (other == 255)
						{
							result = NetworkText.FromKey("DeathText.Slain", new object[]
							{
								deadPlayerName
							});
						}
					}
					else
					{
						result = NetworkText.Empty;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0008CE88 File Offset: 0x0008B088
		public static NetworkText GetInvasionWaveText(int wave, params short[] npcIds)
		{
			NetworkText[] array = new NetworkText[npcIds.Length + 1];
			for (int i = 0; i < npcIds.Length; i++)
			{
				array[i + 1] = NetworkText.FromKey(Lang.GetNPCName((int)npcIds[i]).Key, Array.Empty<object>());
			}
			if (wave != -1)
			{
				if (wave != 1)
				{
					array[0] = NetworkText.FromKey("Game.Wave", new object[]
					{
						wave
					});
				}
				else
				{
					array[0] = NetworkText.FromKey("Game.FirstWave", Array.Empty<object>());
				}
			}
			else
			{
				array[0] = NetworkText.FromKey("Game.FinalWave", Array.Empty<object>());
			}
			string key = "Game.InvasionWave_Type" + npcIds.Length.ToString();
			object[] array2 = array;
			object[] substitutions = array2;
			return NetworkText.FromKey(key, substitutions);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0008CF3C File Offset: 0x0008B13C
		public static string LocalizedDuration(TimeSpan time, bool abbreviated, bool showAllAvailableUnits)
		{
			string text = "";
			abbreviated |= !GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive;
			if (time.Days > 0)
			{
				int num = time.Days;
				if (!showAllAvailableUnits && time > TimeSpan.FromDays(1.0))
				{
					num++;
				}
				text = text + num.ToString() + (abbreviated ? (" " + Language.GetTextValue("Misc.ShortDays")) : ((num == 1) ? " day" : " days"));
				if (!showAllAvailableUnits)
				{
					return text;
				}
				text += " ";
			}
			if (time.Hours > 0)
			{
				int num2 = time.Hours;
				if (!showAllAvailableUnits && time > TimeSpan.FromHours(1.0))
				{
					num2++;
				}
				text = text + num2.ToString() + (abbreviated ? (" " + Language.GetTextValue("Misc.ShortHours")) : ((num2 == 1) ? " hour" : " hours"));
				if (!showAllAvailableUnits)
				{
					return text;
				}
				text += " ";
			}
			if (time.Minutes > 0)
			{
				int num3 = time.Minutes;
				if (!showAllAvailableUnits && time > TimeSpan.FromMinutes(1.0))
				{
					num3++;
				}
				text = text + num3.ToString() + (abbreviated ? (" " + Language.GetTextValue("Misc.ShortMinutes")) : ((num3 == 1) ? " minute" : " minutes"));
				if (!showAllAvailableUnits)
				{
					return text;
				}
				text += " ";
			}
			return text + time.Seconds.ToString() + (abbreviated ? (" " + Language.GetTextValue("Misc.ShortSeconds")) : ((time.Seconds == 1) ? " second" : " seconds"));
		}

		// Token: 0x040002C1 RID: 705
		[Old("Lang arrays have been replaced with the new Language.GetText system.")]
		public static LocalizedText[] menu = new LocalizedText[254];

		// Token: 0x040002C2 RID: 706
		[Old("Lang arrays have been replaced with the new Language.GetText system.")]
		public static LocalizedText[] gen = new LocalizedText[92];

		// Token: 0x040002C3 RID: 707
		[Old("Lang arrays have been replaced with the new Language.GetText system.")]
		public static LocalizedText[] misc = new LocalizedText[201];

		// Token: 0x040002C4 RID: 708
		[Old("Lang arrays have been replaced with the new Language.GetText system.")]
		public static LocalizedText[] inter = new LocalizedText[129];

		// Token: 0x040002C5 RID: 709
		[Old("Lang arrays have been replaced with the new Language.GetText system.")]
		public static LocalizedText[] tip = new LocalizedText[61];

		// Token: 0x040002C6 RID: 710
		[Old("Lang arrays have been replaced with the new Language.GetText system.")]
		public static LocalizedText[] mp = new LocalizedText[27];

		// Token: 0x040002C7 RID: 711
		[Old("Lang arrays have been replaced with the new Language.GetText system.")]
		public static LocalizedText[] chestType = new LocalizedText[52];

		// Token: 0x040002C8 RID: 712
		[Old("Lang arrays have been replaced with the new Language.GetText system.")]
		public static LocalizedText[] dresserType = new LocalizedText[43];

		// Token: 0x040002C9 RID: 713
		[Old("Lang arrays have been replaced with the new Language.GetText system.")]
		public static LocalizedText[] chestType2 = new LocalizedText[17];

		// Token: 0x040002CA RID: 714
		public static LocalizedText[] prefix = new LocalizedText[PrefixID.Count];

		// Token: 0x040002CB RID: 715
		public static MapLegend _mapLegendCache;

		// Token: 0x040002CC RID: 716
		internal static LocalizedText[] _itemNameCache = new LocalizedText[(int)ItemID.Count];

		// Token: 0x040002CD RID: 717
		internal static LocalizedText[] _projectileNameCache = new LocalizedText[(int)ProjectileID.Count];

		// Token: 0x040002CE RID: 718
		internal static LocalizedText[] _npcNameCache = new LocalizedText[(int)NPCID.Count];

		// Token: 0x040002CF RID: 719
		private static LocalizedText[] _negativeNpcNameCache = new LocalizedText[65];

		// Token: 0x040002D0 RID: 720
		internal static LocalizedText[] _buffNameCache = new LocalizedText[BuffID.Count];

		// Token: 0x040002D1 RID: 721
		internal static LocalizedText[] _buffDescriptionCache = new LocalizedText[BuffID.Count];

		// Token: 0x040002D2 RID: 722
		internal static ItemTooltip[] _itemTooltipCache = new ItemTooltip[(int)ItemID.Count];

		// Token: 0x040002D3 RID: 723
		internal static LocalizedText[] _emojiNameCache = new LocalizedText[EmoteID.Count];

		// Token: 0x02000798 RID: 1944
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040065BA RID: 26042
			public static TooltipProcessor <0>__SupportGlyphs;
		}
	}
}
