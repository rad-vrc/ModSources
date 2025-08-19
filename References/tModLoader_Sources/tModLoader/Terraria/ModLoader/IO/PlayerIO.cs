using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria.GameContent.Creative;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.Engine;
using Terraria.ModLoader.Exceptions;
using Terraria.Utilities;

namespace Terraria.ModLoader.IO
{
	// Token: 0x02000286 RID: 646
	internal static class PlayerIO
	{
		// Token: 0x06002BF1 RID: 11249 RVA: 0x00524F92 File Offset: 0x00523192
		internal static void WriteByteVanillaHairDye(int hairDye, BinaryWriter writer)
		{
			writer.Write((byte)((hairDye > EffectsTracker.vanillaHairShaderCount) ? 0 : hairDye));
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x00524FA7 File Offset: 0x005231A7
		internal static void WriteVanillaHair(int hair, BinaryWriter writer)
		{
			writer.Write((hair >= HairID.Count) ? 0 : hair);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x00524FBB File Offset: 0x005231BB
		internal static void Save(TagCompound tag, string path, bool isCloudSave)
		{
			path = Path.ChangeExtension(path, ".tplr");
			if (FileUtilities.Exists(path, isCloudSave))
			{
				FileUtilities.Copy(path, path + ".bak", isCloudSave, true);
			}
			FileUtilities.WriteTagCompound(path, isCloudSave, tag);
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x00524FF0 File Offset: 0x005231F0
		internal static TagCompound SaveData(Player player)
		{
			Item[] _temporaryItemSlots = new Item[]
			{
				Main.mouseItem,
				Main.CreativeMenu.GetItemByIndex(0),
				Main.guideItem,
				Main.reforgeItem
			};
			TagCompound tagCompound = new TagCompound();
			tagCompound["armor"] = PlayerIO.SaveInventory(player.armor);
			tagCompound["dye"] = PlayerIO.SaveInventory(player.dye);
			tagCompound["loadouts"] = PlayerIO.SaveLoadouts(player.Loadouts);
			tagCompound["inventory"] = PlayerIO.SaveInventory(player.inventory);
			tagCompound["miscEquips"] = PlayerIO.SaveInventory(player.miscEquips);
			tagCompound["miscDyes"] = PlayerIO.SaveInventory(player.miscDyes);
			tagCompound["bank"] = PlayerIO.SaveInventory(player.bank.item);
			tagCompound["bank2"] = PlayerIO.SaveInventory(player.bank2.item);
			tagCompound["bank3"] = PlayerIO.SaveInventory(player.bank3.item);
			tagCompound["bank4"] = PlayerIO.SaveInventory(player.bank4.item);
			tagCompound["temporaryItemSlots"] = PlayerIO.SaveInventory(_temporaryItemSlots);
			tagCompound["hairDye"] = PlayerIO.SaveHairDye(player.hairDye);
			tagCompound["research"] = PlayerIO.SaveResearch(player);
			tagCompound["modData"] = PlayerIO.SaveModData(player);
			tagCompound["modBuffs"] = PlayerIO.SaveModBuffs(player);
			tagCompound["infoDisplays"] = PlayerIO.SaveInfoDisplays(player);
			tagCompound["builderToggles"] = PlayerIO.SaveBuilderToggles(player);
			tagCompound["usedMods"] = PlayerIO.SaveUsedMods(player);
			tagCompound["usedModPack"] = PlayerIO.SaveUsedModPack(player);
			tagCompound["hair"] = PlayerIO.SaveHair(player.hair);
			return tagCompound;
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x005251D4 File Offset: 0x005233D4
		internal static void Load(Player player, TagCompound tag)
		{
			PlayerIO.LoadInventory(player.armor, tag.GetList<TagCompound>("armor"));
			PlayerIO.LoadInventory(player.dye, tag.GetList<TagCompound>("dye"));
			PlayerIO.LoadLoadouts(player.Loadouts, tag.GetCompound("loadouts"));
			PlayerIO.LoadInventory(player.inventory, tag.GetList<TagCompound>("inventory"));
			PlayerIO.LoadInventory(player.miscEquips, tag.GetList<TagCompound>("miscEquips"));
			PlayerIO.LoadInventory(player.miscDyes, tag.GetList<TagCompound>("miscDyes"));
			PlayerIO.LoadInventory(player.bank.item, tag.GetList<TagCompound>("bank"));
			PlayerIO.LoadInventory(player.bank2.item, tag.GetList<TagCompound>("bank2"));
			PlayerIO.LoadInventory(player.bank3.item, tag.GetList<TagCompound>("bank3"));
			PlayerIO.LoadInventory(player.bank4.item, tag.GetList<TagCompound>("bank4"));
			PlayerIO.LoadInventory(player._temporaryItemSlots, tag.GetList<TagCompound>("temporaryItemSlots"));
			PlayerIO.LoadHairDye(player, tag.GetString("hairDye"));
			PlayerIO.LoadResearch(player, tag.GetList<TagCompound>("research"));
			PlayerIO.LoadModData(player, tag.GetList<TagCompound>("modData"));
			PlayerIO.LoadModBuffs(player, tag.GetList<TagCompound>("modBuffs"));
			PlayerIO.LoadInfoDisplays(player, tag.GetList<string>("infoDisplays"));
			PlayerIO.LoadBuilderToggles(player, tag.GetList<TagCompound>("builderToggles"));
			PlayerIO.LoadUsedMods(player, tag.GetList<string>("usedMods"));
			PlayerIO.LoadUsedModPack(player, tag.GetString("usedModPack"));
			PlayerIO.LoadHair(player, tag.GetString("hair"));
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x00525380 File Offset: 0x00523580
		internal static byte[] ReadDataBytes(string path, bool isCloudSave)
		{
			path = Path.ChangeExtension(path, ".tplr");
			if (!FileUtilities.Exists(path, isCloudSave))
			{
				return null;
			}
			return FileUtilities.ReadAllBytes(path, isCloudSave);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x005253A4 File Offset: 0x005235A4
		public static List<TagCompound> SaveInventory(Item[] inv)
		{
			List<TagCompound> list = new List<TagCompound>();
			for (int i = 0; i < inv.Length; i++)
			{
				List<TagCompound> globalData = ItemIO.SaveGlobals(inv[i]);
				if (globalData != null || ItemLoader.NeedsModSaving(inv[i]))
				{
					TagCompound tag = ItemIO.Save(inv[i], globalData);
					if (tag.Count != 0)
					{
						tag.Set("slot", (short)i, false);
						list.Add(tag);
					}
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x00525414 File Offset: 0x00523614
		public static void LoadInventory(Item[] inv, IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				inv[(int)tag.GetShort("slot")] = ItemIO.Load(tag);
			}
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x00525468 File Offset: 0x00523668
		public static List<TagCompound> SaveResearch(Player player)
		{
			List<TagCompound> list = new List<TagCompound>();
			foreach (KeyValuePair<int, int> item in new Dictionary<int, int>(player.creativeTracker.ItemSacrifices._sacrificesCountByItemIdCache))
			{
				ModItem modItem = ItemLoader.GetItem(item.Key);
				if (modItem != null)
				{
					TagCompound tagCompound = new TagCompound();
					tagCompound["mod"] = modItem.Mod.Name;
					tagCompound["name"] = modItem.Name;
					tagCompound["sacrificeCount"] = item.Value;
					TagCompound tag = tagCompound;
					list.Add(tag);
				}
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x00525534 File Offset: 0x00523734
		public static void LoadResearch(Player player, IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				if (tag.ContainsKey("mod") && tag.ContainsKey("name"))
				{
					string @string = tag.GetString("mod");
					string modItemName = tag.GetString("name");
					ModItem modItem;
					if (ModContent.TryFind<ModItem>(@string, modItemName, out modItem))
					{
						int netId = modItem.Type;
						string persistentId = ContentSamples.ItemPersistentIdsByNetIds[netId];
						int sacrificeCount = tag.GetInt("sacrificeCount");
						ItemsSacrificedUnlocksTracker itemSacrifices = player.creativeTracker.ItemSacrifices;
						itemSacrifices._sacrificeCountByItemPersistentId[persistentId] = sacrificeCount;
						itemSacrifices._sacrificesCountByItemIdCache[netId] = sacrificeCount;
					}
					else
					{
						player.GetModPlayer<UnloadedPlayer>().unloadedResearch.Add(tag);
					}
				}
			}
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x00525618 File Offset: 0x00523818
		public static string SaveHairDye(int hairDye)
		{
			if (hairDye <= EffectsTracker.vanillaHairShaderCount)
			{
				return "";
			}
			return ItemLoader.GetItem(GameShaders.Hair._reverseShaderLookupDictionary[hairDye]).FullName;
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x00525644 File Offset: 0x00523844
		public static void LoadHairDye(Player player, string hairDyeItemName)
		{
			if (hairDyeItemName == "")
			{
				return;
			}
			ModItem modItem;
			if (ModContent.TryFind<ModItem>(hairDyeItemName, out modItem))
			{
				player.hairDye = (int)((byte)GameShaders.Hair.GetShaderIdFromItemId(modItem.Type));
			}
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x00525680 File Offset: 0x00523880
		public static string SaveHair(int hair)
		{
			if (hair < HairID.Count)
			{
				return "";
			}
			return HairLoader.GetHair(hair).FullName;
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x0052569C File Offset: 0x0052389C
		public static void LoadHair(Player player, string hairName)
		{
			if (hairName == "")
			{
				return;
			}
			ModHair modHair;
			if (ModContent.TryFind<ModHair>(hairName, out modHair))
			{
				player.hair = modHair.Type;
			}
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x005256D0 File Offset: 0x005238D0
		internal static List<TagCompound> SaveModData(Player player)
		{
			List<TagCompound> list = new List<TagCompound>();
			TagCompound saveData = new TagCompound();
			ModPlayer[] modPlayers = player.modPlayers;
			int i = 0;
			while (i < modPlayers.Length)
			{
				ModPlayer modPlayer = modPlayers[i];
				try
				{
					modPlayer.SaveData(saveData);
				}
				catch (Exception e)
				{
					Utils.HandleSaveErrorMessageLogging(NetworkText.FromKey("tModLoader.SavePlayerDataExceptionWarning", new object[]
					{
						modPlayer.Name,
						modPlayer.Mod.Name,
						"\n\n" + e.ToString()
					}), false);
					List<TagCompound> list2 = list;
					TagCompound tagCompound = new TagCompound();
					tagCompound["mod"] = modPlayer.Mod.Name;
					tagCompound["name"] = modPlayer.Name;
					tagCompound["error"] = e.ToString();
					list2.Add(tagCompound);
					saveData = new TagCompound();
					goto IL_10D;
				}
				goto IL_BF;
				IL_10D:
				i++;
				continue;
				IL_BF:
				if (saveData.Count != 0)
				{
					List<TagCompound> list3 = list;
					TagCompound tagCompound2 = new TagCompound();
					tagCompound2["mod"] = modPlayer.Mod.Name;
					tagCompound2["name"] = modPlayer.Name;
					tagCompound2["data"] = saveData;
					list3.Add(tagCompound2);
					saveData = new TagCompound();
					goto IL_10D;
				}
				goto IL_10D;
			}
			return list;
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x00525808 File Offset: 0x00523A08
		internal static void LoadModData(Player player, IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				string modName = tag.GetString("mod");
				string modPlayerName = tag.GetString("name");
				string errorMessage;
				if (tag.TryGet<string>("error", out errorMessage))
				{
					player.ModSaveErrors[modName + "/" + modPlayerName + ".SaveData"] = errorMessage;
				}
				else
				{
					ModPlayer modPlayerBase;
					if (ModContent.TryFind<ModPlayer>(modName, modPlayerName, out modPlayerBase))
					{
						ModPlayer modPlayer = player.GetModPlayer<ModPlayer>(modPlayerBase);
						try
						{
							modPlayer.LoadData(tag.GetCompound("data"));
							continue;
						}
						catch (Exception e)
						{
							Mod mod = modPlayer.Mod;
							throw new CustomModDataException(mod, "Error in reading custom player data for " + mod.Name, e);
						}
					}
					player.GetModPlayer<UnloadedPlayer>().data.Add(tag);
				}
			}
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x00525904 File Offset: 0x00523B04
		internal static List<TagCompound> SaveModBuffs(Player player)
		{
			List<TagCompound> list = new List<TagCompound>();
			for (int i = 0; i < Player.MaxBuffs; i++)
			{
				int buff = player.buffType[i];
				if (buff != 0 && !Main.buffNoSave[buff])
				{
					if (BuffLoader.IsModBuff(buff))
					{
						ModBuff modBuff = BuffLoader.GetBuff(buff);
						List<TagCompound> list2 = list;
						TagCompound tagCompound = new TagCompound();
						tagCompound["mod"] = modBuff.Mod.Name;
						tagCompound["name"] = modBuff.Name;
						tagCompound["time"] = player.buffTime[i];
						list2.Add(tagCompound);
					}
					else
					{
						List<TagCompound> list3 = list;
						TagCompound tagCompound2 = new TagCompound();
						tagCompound2["mod"] = "Terraria";
						tagCompound2["id"] = buff;
						tagCompound2["time"] = player.buffTime[i];
						list3.Add(tagCompound2);
					}
				}
			}
			return list;
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x005259E8 File Offset: 0x00523BE8
		internal static void LoadModBuffs(Player player, IList<TagCompound> list)
		{
			int buffCount = Player.MaxBuffs;
			while (buffCount > 0 && player.buffType[buffCount - 1] == 0)
			{
				buffCount--;
			}
			if (buffCount == 0)
			{
				foreach (TagCompound tag in list)
				{
					if (buffCount == Player.MaxBuffs)
					{
						return;
					}
					string modName = tag.GetString("mod");
					ModBuff buff;
					int type = (modName == "Terraria") ? tag.GetInt("id") : (ModContent.TryFind<ModBuff>(modName, tag.GetString("name"), out buff) ? buff.Type : 0);
					if (type > 0)
					{
						player.buffType[buffCount] = type;
						player.buffTime[buffCount] = tag.GetInt("time");
						buffCount++;
					}
				}
				return;
			}
			foreach (TagCompound tag2 in list.Reverse<TagCompound>())
			{
				ModBuff buff2;
				if (ModContent.TryFind<ModBuff>(tag2.GetString("mod"), tag2.GetString("name"), out buff2))
				{
					int index = Math.Min((int)tag2.GetByte("index"), buffCount);
					Array.Copy(player.buffType, index, player.buffType, index + 1, Player.MaxBuffs - index - 1);
					Array.Copy(player.buffTime, index, player.buffTime, index + 1, Player.MaxBuffs - index - 1);
					player.buffType[index] = buff2.Type;
					player.buffTime[index] = tag2.GetInt("time");
				}
			}
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x00525BA4 File Offset: 0x00523DA4
		internal static List<string> SaveInfoDisplays(Player player)
		{
			List<string> hidden = new List<string>();
			for (int i = 0; i < InfoDisplayLoader.InfoDisplays.Count; i++)
			{
				if (!(InfoDisplayLoader.InfoDisplays[i] is VanillaInfoDisplay) && player.hideInfo[i])
				{
					hidden.Add(InfoDisplayLoader.InfoDisplays[i].FullName);
				}
			}
			return hidden;
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x00525C00 File Offset: 0x00523E00
		internal static void LoadInfoDisplays(Player player, IList<string> hidden)
		{
			for (int i = 0; i < InfoDisplayLoader.InfoDisplays.Count; i++)
			{
				if (!(InfoDisplayLoader.InfoDisplays[i] is VanillaInfoDisplay) && hidden.Contains(InfoDisplayLoader.InfoDisplays[i].FullName))
				{
					player.hideInfo[i] = true;
				}
			}
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x00525C58 File Offset: 0x00523E58
		internal static List<TagCompound> SaveBuilderToggles(Player player)
		{
			return (from x in BuilderToggleLoader.BuilderToggles
			where !(x is VanillaBuilderToggle)
			select x).Select(delegate(BuilderToggle x)
			{
				TagCompound tagCompound = new TagCompound();
				tagCompound["fullName"] = x.FullName;
				tagCompound["currentState"] = player.builderAccStatus[x.Type];
				return tagCompound;
			}).ToList<TagCompound>();
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x00525CB4 File Offset: 0x00523EB4
		internal static void LoadBuilderToggles(Player player, IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				string fullname = tag.GetString("fullName");
				int entryIndex = BuilderToggleLoader.BuilderToggles.FindIndex((BuilderToggle x) => x.FullName == fullname);
				if (entryIndex != -1)
				{
					player.builderAccStatus[entryIndex] = tag.GetInt("currentState");
				}
			}
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x00525D3C File Offset: 0x00523F3C
		internal static void LoadUsedMods(Player player, IList<string> usedMods)
		{
			player.usedMods = usedMods;
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00525D48 File Offset: 0x00523F48
		internal static List<string> SaveUsedMods(Player player)
		{
			return (from m in ModLoader.Mods
			select m.Name).Except(new string[]
			{
				"ModLoader"
			}).ToList<string>();
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x00525D96 File Offset: 0x00523F96
		internal static void LoadUsedModPack(Player player, string modpack)
		{
			player.modPack = (string.IsNullOrEmpty(modpack) ? null : modpack);
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x00525DAA File Offset: 0x00523FAA
		internal static string SaveUsedModPack(Player player)
		{
			return Path.GetFileNameWithoutExtension(ModOrganizer.ModPackActive);
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x00525DB8 File Offset: 0x00523FB8
		internal static TagCompound SaveLoadouts(EquipmentLoadout[] equipLoadouts)
		{
			TagCompound loadouts = new TagCompound();
			for (int i = 0; i < equipLoadouts.Length; i++)
			{
				TagCompound tagCompound = loadouts;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
				defaultInterpolatedStringHandler.AppendLiteral("loadout");
				defaultInterpolatedStringHandler.AppendFormatted<int>(i);
				defaultInterpolatedStringHandler.AppendLiteral("Armor");
				tagCompound[defaultInterpolatedStringHandler.ToStringAndClear()] = PlayerIO.SaveInventory(equipLoadouts[i].Armor);
				TagCompound tagCompound2 = loadouts;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
				defaultInterpolatedStringHandler.AppendLiteral("loadout");
				defaultInterpolatedStringHandler.AppendFormatted<int>(i);
				defaultInterpolatedStringHandler.AppendLiteral("Dye");
				tagCompound2[defaultInterpolatedStringHandler.ToStringAndClear()] = PlayerIO.SaveInventory(equipLoadouts[i].Dye);
			}
			return loadouts;
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x00525E68 File Offset: 0x00524068
		internal static void LoadLoadouts(EquipmentLoadout[] loadouts, TagCompound loadoutTag)
		{
			for (int i = 0; i < loadouts.Length; i++)
			{
				Item[] armor = loadouts[i].Armor;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(12, 1);
				defaultInterpolatedStringHandler.AppendLiteral("loadout");
				defaultInterpolatedStringHandler.AppendFormatted<int>(i);
				defaultInterpolatedStringHandler.AppendLiteral("Armor");
				PlayerIO.LoadInventory(armor, loadoutTag.GetList<TagCompound>(defaultInterpolatedStringHandler.ToStringAndClear()));
				Item[] dye = loadouts[i].Dye;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
				defaultInterpolatedStringHandler.AppendLiteral("loadout");
				defaultInterpolatedStringHandler.AppendFormatted<int>(i);
				defaultInterpolatedStringHandler.AppendLiteral("Dye");
				PlayerIO.LoadInventory(dye, loadoutTag.GetList<TagCompound>(defaultInterpolatedStringHandler.ToStringAndClear()));
			}
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x00525F11 File Offset: 0x00524111
		internal static void MoveToCloud(string localPath, string cloudPath)
		{
			localPath = Path.ChangeExtension(localPath, ".tplr");
			cloudPath = Path.ChangeExtension(cloudPath, ".tplr");
			if (File.Exists(localPath))
			{
				FileUtilities.MoveToCloud(localPath, cloudPath);
			}
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x00525F3D File Offset: 0x0052413D
		internal static void MoveToLocal(string cloudPath, string localPath)
		{
			cloudPath = Path.ChangeExtension(cloudPath, ".tplr");
			localPath = Path.ChangeExtension(localPath, ".tplr");
			if (FileUtilities.Exists(cloudPath, true))
			{
				FileUtilities.MoveToLocal(cloudPath, localPath);
			}
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x00525F6A File Offset: 0x0052416A
		internal static void LoadBackup(string path, bool cloudSave)
		{
			path = Path.ChangeExtension(path, ".tplr");
			if (FileUtilities.Exists(path + ".bak", cloudSave))
			{
				FileUtilities.Move(path + ".bak", path, cloudSave, true, false);
			}
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x00525FA0 File Offset: 0x005241A0
		internal static void ErasePlayer(string path, bool cloudSave)
		{
			path = Path.ChangeExtension(path, ".tplr");
			try
			{
				FileUtilities.Delete(path, cloudSave, false);
				FileUtilities.Delete(path + ".bak", cloudSave, false);
			}
			catch
			{
			}
		}
	}
}
