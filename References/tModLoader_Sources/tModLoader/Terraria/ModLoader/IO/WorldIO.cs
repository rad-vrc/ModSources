using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;
using Microsoft.Xna.Framework;
using ReLogic.OS;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.Exceptions;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria.ModLoader.IO
{
	// Token: 0x0200029D RID: 669
	internal static class WorldIO
	{
		// Token: 0x06002CB0 RID: 11440 RVA: 0x00528F4C File Offset: 0x0052714C
		internal static void Save(string path, bool isCloudSave)
		{
			path = Path.ChangeExtension(path, ".twld");
			if (FileUtilities.Exists(path, isCloudSave))
			{
				FileUtilities.Copy(path, path + ".bak", isCloudSave, true);
			}
			Main.ActiveWorldFileData.ModSaveErrors.Clear();
			TagCompound header = WorldIO.SaveHeader();
			TagCompound tagCompound = new TagCompound();
			tagCompound["chests"] = WorldIO.SaveChestInventory();
			tagCompound["tiles"] = TileIO.SaveBasics();
			tagCompound["containers"] = TileIO.SaveContainers();
			tagCompound["npcs"] = WorldIO.SaveNPCs();
			tagCompound["tileEntities"] = TileIO.SaveTileEntities();
			tagCompound["killCounts"] = WorldIO.SaveNPCKillCounts();
			tagCompound["bestiaryKills"] = WorldIO.SaveNPCBestiaryKills();
			tagCompound["bestiarySights"] = WorldIO.SaveNPCBestiarySights();
			tagCompound["bestiaryChats"] = WorldIO.SaveNPCBestiaryChats();
			tagCompound["anglerQuest"] = WorldIO.SaveAnglerQuest();
			tagCompound["townManager"] = WorldIO.SaveTownManager();
			tagCompound["modData"] = WorldIO.SaveModData();
			tagCompound["alteredVanillaFields"] = WorldIO.SaveAlteredVanillaFields();
			TagCompound body = tagCompound;
			TagCompound saveModDataErrors = new TagCompound();
			foreach (KeyValuePair<string, string> error in Main.ActiveWorldFileData.ModSaveErrors)
			{
				saveModDataErrors[error.Key] = error.Value;
			}
			header["saveModDataErrors"] = saveModDataErrors;
			TagCompound tagCompound2 = new TagCompound();
			tagCompound2["0header"] = header;
			TagCompound tag = tagCompound2;
			foreach (KeyValuePair<string, object> bodyTag in body)
			{
				tag[bodyTag.Key] = bodyTag.Value;
			}
			FileUtilities.WriteTagCompound(path, isCloudSave, tag);
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x00529140 File Offset: 0x00527340
		internal static void Load(string path, bool isCloudSave)
		{
			WorldIO.customDataFail = null;
			path = Path.ChangeExtension(path, ".twld");
			if (!FileUtilities.Exists(path, isCloudSave))
			{
				return;
			}
			byte[] buf = FileUtilities.ReadAllBytes(path, isCloudSave);
			if (buf[0] != 31 || buf[1] != 139)
			{
				throw new IOException(Path.GetFileName(path) + ":: File Corrupted during Last Save Step. Aborting... ERROR: Missing NBT Header");
			}
			TagCompound tag = TagIO.FromStream(buf.ToMemoryStream(false), true);
			TileIO.LoadBasics(tag.GetCompound("tiles"));
			TileIO.LoadContainers(tag.GetCompound("containers"));
			WorldIO.LoadNPCs(tag.GetList<TagCompound>("npcs"));
			try
			{
				TileIO.LoadTileEntities(tag.GetList<TagCompound>("tileEntities"));
			}
			catch (CustomModDataException ex)
			{
				WorldIO.customDataFail = ex;
				throw;
			}
			WorldIO.LoadChestInventory(tag.GetList<TagCompound>("chests"));
			WorldIO.LoadNPCKillCounts(tag.GetList<TagCompound>("killCounts"));
			WorldIO.LoadNPCBestiaryKills(tag.GetList<TagCompound>("bestiaryKills"));
			WorldIO.LoadNPCBestiarySights(tag.GetList<TagCompound>("bestiarySights"));
			WorldIO.LoadNPCBestiaryChats(tag.GetList<TagCompound>("bestiaryChats"));
			WorldIO.LoadAnglerQuest(tag.GetCompound("anglerQuest"));
			WorldIO.LoadTownManager(tag.GetList<TagCompound>("townManager"));
			try
			{
				WorldIO.LoadModData(tag.GetList<TagCompound>("modData"));
			}
			catch (CustomModDataException ex2)
			{
				WorldIO.customDataFail = ex2;
				throw;
			}
			WorldIO.LoadAlteredVanillaFields(tag.GetCompound("alteredVanillaFields"));
			if (Main.ActiveWorldFileData.ModSaveErrors.Any<KeyValuePair<string, string>>())
			{
				Utils.LogAndConsoleInfoMessage(Utils.CreateSaveErrorMessage("tModLoader.WorldCustomDataSaveFail", Main.ActiveWorldFileData.ModSaveErrors, false).ToString());
			}
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x005292D8 File Offset: 0x005274D8
		internal static List<TagCompound> SaveChestInventory()
		{
			List<TagCompound> list = new List<TagCompound>();
			for (int i = 0; i < 8000; i++)
			{
				Chest chest = Main.chest[i];
				if (chest != null)
				{
					List<TagCompound> itemTagListModded = PlayerIO.SaveInventory(chest.item);
					if (itemTagListModded != null)
					{
						TagCompound tagCompound = new TagCompound();
						tagCompound["items"] = itemTagListModded;
						tagCompound["x"] = chest.x;
						tagCompound["y"] = chest.y;
						TagCompound tag = tagCompound;
						list.Add(tag);
					}
				}
			}
			return list;
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x00529360 File Offset: 0x00527560
		internal static void LoadChestInventory(IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				int cID = Chest.FindChest(tag.GetInt("x"), tag.GetInt("y"));
				if (cID >= 0)
				{
					PlayerIO.LoadInventory(Main.chest[cID].item, tag.GetList<TagCompound>("items"));
				}
			}
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x005293E0 File Offset: 0x005275E0
		internal static List<TagCompound> SaveNPCs()
		{
			List<TagCompound> list = new List<TagCompound>();
			TagCompound data = new TagCompound();
			for (int index = 0; index < Main.maxNPCs; index++)
			{
				NPC npc = Main.npc[index];
				if (npc.active && NPCLoader.SavesAndLoads(npc))
				{
					List<TagCompound> globalData = new List<TagCompound>();
					foreach (GlobalNPC g in NPCLoader.HookSaveData.Enumerate(npc))
					{
						UnloadedGlobalNPC unloadedGlobalNPC = g as UnloadedGlobalNPC;
						if (unloadedGlobalNPC != null)
						{
							globalData.AddRange(unloadedGlobalNPC.data);
						}
						else
						{
							g.SaveData(npc, data);
							if (data.Count != 0)
							{
								List<TagCompound> list2 = globalData;
								TagCompound tagCompound = new TagCompound();
								tagCompound["mod"] = g.Mod.Name;
								tagCompound["name"] = g.Name;
								tagCompound["data"] = data;
								list2.Add(tagCompound);
								data = new TagCompound();
							}
						}
					}
					TagCompound tag;
					if (NPCLoader.IsModNPC(npc))
					{
						npc.ModNPC.SaveData(data);
						TagCompound tagCompound2 = new TagCompound();
						tagCompound2["mod"] = npc.ModNPC.Mod.Name;
						tagCompound2["name"] = npc.ModNPC.Name;
						tag = tagCompound2;
						if (data.Count != 0)
						{
							tag["data"] = data;
							data = new TagCompound();
						}
						if (npc.townNPC)
						{
							tag["displayName"] = npc.GivenName;
							tag["homeless"] = npc.homeless;
							tag["homeTileX"] = npc.homeTileX;
							tag["homeTileY"] = npc.homeTileY;
							tag["isShimmered"] = NPC.ShimmeredTownNPCs[npc.type];
							tag["npcTownVariationIndex"] = npc.townNpcVariationIndex;
						}
					}
					else
					{
						if (globalData.Count == 0)
						{
							goto IL_268;
						}
						TagCompound tagCompound3 = new TagCompound();
						tagCompound3["mod"] = "Terraria";
						tagCompound3["name"] = NPCID.Search.GetName(npc.type);
						tag = tagCompound3;
					}
					tag["x"] = npc.position.X;
					tag["y"] = npc.position.Y;
					tag["globalData"] = globalData;
					list.Add(tag);
				}
				IL_268:;
			}
			return list;
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x00529668 File Offset: 0x00527868
		internal static void LoadNPCs(IList<TagCompound> list)
		{
			if (list == null)
			{
				return;
			}
			int nextFreeNPC = 0;
			foreach (TagCompound tag in list)
			{
				NPC npc = null;
				while (nextFreeNPC < Main.maxNPCs && Main.npc[nextFreeNPC].active)
				{
					nextFreeNPC++;
				}
				if (tag.GetString("mod") == "Terraria")
				{
					int npcId = NPCID.Search.GetId(tag.GetString("name"));
					float x = tag.GetFloat("x");
					float y = tag.GetFloat("y");
					int index;
					for (index = 0; index < Main.maxNPCs; index++)
					{
						npc = Main.npc[index];
						if (npc.active && npc.type == npcId && npc.position.X == x && npc.position.Y == y)
						{
							break;
						}
					}
					if (index == Main.maxNPCs)
					{
						if (nextFreeNPC == Main.maxNPCs)
						{
							ModContent.GetInstance<UnloadedSystem>().unloadedNPCs.Add(tag);
							continue;
						}
						npc = Main.npc[nextFreeNPC];
						npc.SetDefaults(npcId, default(NPCSpawnParams));
						npc.position = new Vector2(x, y);
					}
				}
				else
				{
					ModNPC modNpc;
					if (!ModContent.TryFind<ModNPC>(tag.GetString("mod"), tag.GetString("name"), out modNpc))
					{
						ModContent.GetInstance<UnloadedSystem>().unloadedNPCs.Add(tag);
						continue;
					}
					if (nextFreeNPC == Main.maxNPCs)
					{
						ModContent.GetInstance<UnloadedSystem>().unloadedNPCs.Add(tag);
						continue;
					}
					npc = Main.npc[nextFreeNPC];
					npc.SetDefaults(modNpc.Type, default(NPCSpawnParams));
					npc.position.X = tag.GetFloat("x");
					npc.position.Y = tag.GetFloat("y");
					if (npc.townNPC)
					{
						npc.GivenName = tag.GetString("displayName");
						npc.homeless = tag.GetBool("homeless");
						npc.homeTileX = tag.GetInt("homeTileX");
						npc.homeTileY = tag.GetInt("homeTileY");
						NPC.ShimmeredTownNPCs[modNpc.Type] = tag.GetBool("isShimmered");
						npc.townNpcVariationIndex = tag.GetInt("npcTownVariationIndex");
					}
					if (tag.ContainsKey("data"))
					{
						npc.ModNPC.LoadData((TagCompound)tag["data"]);
					}
				}
				WorldIO.LoadGlobals(npc, tag.GetList<TagCompound>("globalData"));
			}
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x00529918 File Offset: 0x00527B18
		private static void LoadGlobals(NPC npc, IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				GlobalNPC globalNPCBase;
				GlobalNPC globalNPC;
				if (ModContent.TryFind<GlobalNPC>(tag.GetString("mod"), tag.GetString("name"), out globalNPCBase) && npc.TryGetGlobalNPC<GlobalNPC>(globalNPCBase, out globalNPC))
				{
					try
					{
						globalNPC.LoadData(npc, tag.GetCompound("data"));
						continue;
					}
					catch (Exception inner)
					{
						throw new CustomModDataException(globalNPC.Mod, "Error in reading custom player data for " + tag.GetString("mod"), inner);
					}
				}
				npc.GetGlobalNPC<UnloadedGlobalNPC>().data.Add(tag);
			}
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x005299DC File Offset: 0x00527BDC
		internal static List<TagCompound> SaveNPCKillCounts()
		{
			List<TagCompound> list = new List<TagCompound>();
			for (int type = (int)NPCID.Count; type < NPCLoader.NPCCount; type++)
			{
				int killCount = NPC.killCount[type];
				if (killCount > 0)
				{
					ModNPC modNPC = NPCLoader.GetNPC(type);
					List<TagCompound> list2 = list;
					TagCompound tagCompound = new TagCompound();
					tagCompound["mod"] = modNPC.Mod.Name;
					tagCompound["name"] = modNPC.Name;
					tagCompound["count"] = killCount;
					list2.Add(tagCompound);
				}
			}
			return list;
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x00529A5C File Offset: 0x00527C5C
		internal static void LoadNPCKillCounts(IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				ModNPC modNpc;
				if (ModContent.TryFind<ModNPC>(tag.GetString("mod"), tag.GetString("name"), out modNpc))
				{
					NPC.killCount[modNpc.Type] = tag.GetInt("count");
				}
				else
				{
					ModContent.GetInstance<UnloadedSystem>().unloadedKillCounts.Add(tag);
				}
			}
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x00529AE8 File Offset: 0x00527CE8
		internal static List<TagCompound> SaveNPCBestiaryKills()
		{
			List<TagCompound> list = new List<TagCompound>();
			for (int type = (int)NPCID.Count; type < NPCLoader.NPCCount; type++)
			{
				int killCount = Main.BestiaryTracker.Kills.GetKillCount(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[type]);
				if (killCount > 0)
				{
					ModNPC modNPC = NPCLoader.GetNPC(type);
					List<TagCompound> list2 = list;
					TagCompound tagCompound = new TagCompound();
					tagCompound["mod"] = modNPC.Mod.Name;
					tagCompound["name"] = modNPC.Name;
					tagCompound["count"] = killCount;
					list2.Add(tagCompound);
				}
			}
			return list;
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x00529B7C File Offset: 0x00527D7C
		internal static void LoadNPCBestiaryKills(IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				ModNPC modNpc;
				if (ModContent.TryFind<ModNPC>(tag.GetString("mod"), tag.GetString("name"), out modNpc))
				{
					string persistentId = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[modNpc.Type];
					Main.BestiaryTracker.Kills.SetKillCountDirectly(persistentId, tag.GetInt("count"));
				}
				else
				{
					ModContent.GetInstance<UnloadedSystem>().unloadedBestiaryKills.Add(tag);
				}
			}
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x00529C1C File Offset: 0x00527E1C
		internal static List<TagCompound> SaveNPCBestiarySights()
		{
			List<TagCompound> list = new List<TagCompound>();
			for (int type = (int)NPCID.Count; type < NPCLoader.NPCCount; type++)
			{
				if (Main.BestiaryTracker.Sights.GetWasNearbyBefore(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[type]))
				{
					ModNPC modNPC = NPCLoader.GetNPC(type);
					List<TagCompound> list2 = list;
					TagCompound tagCompound = new TagCompound();
					tagCompound["mod"] = modNPC.Mod.Name;
					tagCompound["name"] = modNPC.Name;
					list2.Add(tagCompound);
				}
			}
			return list;
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x00529C9C File Offset: 0x00527E9C
		internal static void LoadNPCBestiarySights(IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				ModNPC modNpc;
				if (ModContent.TryFind<ModNPC>(tag.GetString("mod"), tag.GetString("name"), out modNpc))
				{
					string persistentId = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[modNpc.Type];
					Main.BestiaryTracker.Sights.SetWasSeenDirectly(persistentId);
				}
				else
				{
					ModContent.GetInstance<UnloadedSystem>().unloadedBestiarySights.Add(tag);
				}
			}
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x00529D30 File Offset: 0x00527F30
		internal static List<TagCompound> SaveNPCBestiaryChats()
		{
			List<TagCompound> list = new List<TagCompound>();
			for (int type = (int)NPCID.Count; type < NPCLoader.NPCCount; type++)
			{
				if (Main.BestiaryTracker.Chats.GetWasChatWith(ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[type]))
				{
					ModNPC modNPC = NPCLoader.GetNPC(type);
					List<TagCompound> list2 = list;
					TagCompound tagCompound = new TagCompound();
					tagCompound["mod"] = modNPC.Mod.Name;
					tagCompound["name"] = modNPC.Name;
					list2.Add(tagCompound);
				}
			}
			return list;
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x00529DB0 File Offset: 0x00527FB0
		internal static void LoadNPCBestiaryChats(IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				ModNPC modNpc;
				if (ModContent.TryFind<ModNPC>(tag.GetString("mod"), tag.GetString("name"), out modNpc))
				{
					string persistentId = ContentSamples.NpcBestiaryCreditIdsByNpcNetIds[modNpc.Type];
					Main.BestiaryTracker.Chats.SetWasChatWithDirectly(persistentId);
				}
				else
				{
					ModContent.GetInstance<UnloadedSystem>().unloadedBestiaryChats.Add(tag);
				}
			}
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x00529E44 File Offset: 0x00528044
		internal static TagCompound SaveAnglerQuest()
		{
			if (Main.anglerQuest < ItemLoader.vanillaQuestFishCount)
			{
				return null;
			}
			ModItem modItem = ItemLoader.GetItem(Main.anglerQuestItemNetIDs[Main.anglerQuest]);
			TagCompound tagCompound = new TagCompound();
			tagCompound["mod"] = modItem.Mod.Name;
			tagCompound["itemName"] = modItem.Name;
			return tagCompound;
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x00529E9C File Offset: 0x0052809C
		internal static void LoadAnglerQuest(TagCompound tag)
		{
			if (!tag.ContainsKey("mod"))
			{
				return;
			}
			ModItem modItem;
			if (ModContent.TryFind<ModItem>(tag.GetString("mod"), tag.GetString("itemName"), out modItem))
			{
				for (int i = 0; i < Main.anglerQuestItemNetIDs.Length; i++)
				{
					if (Main.anglerQuestItemNetIDs[i] == modItem.Type)
					{
						Main.anglerQuest = i;
						return;
					}
				}
			}
			Main.AnglerQuestSwap();
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x00529F04 File Offset: 0x00528104
		internal static List<TagCompound> SaveTownManager()
		{
			List<TagCompound> list = new List<TagCompound>();
			foreach (Tuple<int, Point> pair in WorldGen.TownManager._roomLocationPairs)
			{
				if (pair.Item1 >= (int)NPCID.Count)
				{
					ModNPC npc = NPCLoader.GetNPC(pair.Item1);
					TagCompound tagCompound = new TagCompound();
					tagCompound["mod"] = npc.Mod.Name;
					tagCompound["name"] = npc.Name;
					tagCompound["x"] = pair.Item2.X;
					tagCompound["y"] = pair.Item2.Y;
					TagCompound tag = tagCompound;
					list.Add(tag);
				}
			}
			return list;
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x00529FE8 File Offset: 0x005281E8
		internal static void LoadTownManager(IList<TagCompound> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (TagCompound tag in list)
			{
				ModNPC modNpc;
				if (ModContent.TryFind<ModNPC>(tag.GetString("mod"), tag.GetString("name"), out modNpc))
				{
					Point location;
					location..ctor(tag.GetInt("x"), tag.GetInt("y"));
					WorldGen.TownManager._roomLocationPairs.Add(Tuple.Create<int, Point>(modNpc.Type, location));
					WorldGen.TownManager._hasRoom[modNpc.Type] = true;
				}
			}
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x0052A098 File Offset: 0x00528298
		internal static List<TagCompound> SaveModData()
		{
			List<TagCompound> list = new List<TagCompound>();
			TagCompound saveData = new TagCompound();
			foreach (ModSystem system in SystemLoader.Systems)
			{
				try
				{
					system.SaveWorldData(saveData);
				}
				catch (Exception e)
				{
					Utils.HandleSaveErrorMessageLogging(NetworkText.FromKey("tModLoader.SaveWorldDataExceptionWarning", new object[]
					{
						system.Name,
						system.Mod.Name,
						"\n\n" + e.ToString()
					}), true);
					Main.ActiveWorldFileData.ModSaveErrors[system.FullName + ".SaveWorldData"] = e.ToString();
					saveData = new TagCompound();
					continue;
				}
				if (saveData.Count != 0)
				{
					List<TagCompound> list2 = list;
					TagCompound tagCompound = new TagCompound();
					tagCompound["mod"] = system.Mod.Name;
					tagCompound["name"] = system.Name;
					tagCompound["data"] = saveData;
					list2.Add(tagCompound);
					saveData = new TagCompound();
				}
			}
			return list;
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x0052A1C8 File Offset: 0x005283C8
		internal static void LoadModData(IList<TagCompound> list)
		{
			foreach (TagCompound tag in list)
			{
				ModSystem system;
				if (ModContent.TryFind<ModSystem>(tag.GetString("mod"), tag.GetString("name"), out system))
				{
					try
					{
						system.LoadWorldData(tag.GetCompound("data"));
						continue;
					}
					catch (Exception e)
					{
						throw new CustomModDataException(system.Mod, "Error in reading custom world data for " + system.Mod.Name, e);
					}
				}
				ModContent.GetInstance<UnloadedSystem>().data.Add(tag);
			}
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x0052A27C File Offset: 0x0052847C
		internal static TagCompound SaveAlteredVanillaFields()
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound["timeCultists"] = CultistRitual.delay;
			tagCompound["timeRain"] = Main.rainTime;
			tagCompound["timeSandstorm"] = Sandstorm.TimeLeft;
			return tagCompound;
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x0052A2CD File Offset: 0x005284CD
		internal static void LoadAlteredVanillaFields(TagCompound compound)
		{
			CultistRitual.delay = compound.GetDouble("timeCultists");
			Main.rainTime = compound.GetDouble("timeRain");
			Sandstorm.TimeLeft = compound.GetDouble("timeSandstorm");
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x0052A300 File Offset: 0x00528500
		public unsafe static void SendModData(BinaryWriter writer)
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookNetSend.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ModSystem system = *readOnlySpan[i];
				writer.SafeWrite(delegate(BinaryWriter w)
				{
					system.NetSend(w);
				});
			}
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x0052A354 File Offset: 0x00528554
		public unsafe static void ReceiveModData(BinaryReader reader)
		{
			ReadOnlySpan<ModSystem> readOnlySpan = SystemLoader.HookNetReceive.Enumerate();
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				ModSystem system = *readOnlySpan[i];
				try
				{
					reader.SafeRead(delegate(BinaryReader r)
					{
						system.NetReceive(r);
					});
				}
				catch (IOException e)
				{
					Logging.tML.Error(e.ToString());
					ILog tML = Logging.tML;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(49, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Above IOException error caused by ");
					defaultInterpolatedStringHandler.AppendFormatted(system.Name);
					defaultInterpolatedStringHandler.AppendLiteral(" from the ");
					defaultInterpolatedStringHandler.AppendFormatted(system.Mod.Name);
					defaultInterpolatedStringHandler.AppendLiteral(" mod.");
					tML.Error(defaultInterpolatedStringHandler.ToStringAndClear());
				}
			}
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x0052A438 File Offset: 0x00528638
		public unsafe static void ValidateSigns()
		{
			for (int i = 0; i < Main.sign.Length; i++)
			{
				if (Main.sign[i] != null)
				{
					Tile tile = Main.tile[Main.sign[i].x, Main.sign[i].y];
					if (!tile.active() || !Main.tileSign[(int)(*tile.type)])
					{
						Main.sign[i] = null;
					}
				}
			}
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x0052A4A4 File Offset: 0x005286A4
		internal static void MoveToCloud(string localPath, string cloudPath)
		{
			localPath = Path.ChangeExtension(localPath, ".twld");
			cloudPath = Path.ChangeExtension(cloudPath, ".twld");
			if (File.Exists(localPath))
			{
				FileUtilities.MoveToCloud(localPath, cloudPath);
			}
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x0052A4D0 File Offset: 0x005286D0
		internal static void MoveToLocal(string cloudPath, string localPath)
		{
			cloudPath = Path.ChangeExtension(cloudPath, ".twld");
			localPath = Path.ChangeExtension(localPath, ".twld");
			if (FileUtilities.Exists(cloudPath, true))
			{
				FileUtilities.MoveToLocal(cloudPath, localPath);
			}
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x0052A4FD File Offset: 0x005286FD
		internal static void LoadBackup(string path, bool cloudSave)
		{
			path = Path.ChangeExtension(path, ".twld");
			if (FileUtilities.Exists(path + ".bak", cloudSave))
			{
				FileUtilities.Move(path + ".bak", path, cloudSave, true, false);
			}
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x0052A534 File Offset: 0x00528734
		internal static void LoadDedServBackup(string path, bool cloudSave)
		{
			path = Path.ChangeExtension(path, ".twld");
			if (FileUtilities.Exists(path, cloudSave))
			{
				FileUtilities.Copy(path, path + ".bad", cloudSave, true);
			}
			if (FileUtilities.Exists(path + ".bak", cloudSave))
			{
				FileUtilities.Copy(path + ".bak", path, cloudSave, true);
				FileUtilities.Delete(path + ".bak", cloudSave, false);
			}
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x0052A5A4 File Offset: 0x005287A4
		internal static void RevertDedServBackup(string path, bool cloudSave)
		{
			path = Path.ChangeExtension(path, ".twld");
			if (FileUtilities.Exists(path, cloudSave))
			{
				FileUtilities.Copy(path, path + ".bak", cloudSave, true);
			}
			if (FileUtilities.Exists(path + ".bad", cloudSave))
			{
				FileUtilities.Copy(path + ".bad", path, cloudSave, true);
				FileUtilities.Delete(path + ".bad", cloudSave, false);
			}
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x0052A614 File Offset: 0x00528814
		internal static void EraseWorld(string path, bool cloudSave)
		{
			path = Path.ChangeExtension(path, ".twld");
			if (!cloudSave)
			{
				Platform.Get<IPathService>().MoveToRecycleBin(path);
				Platform.Get<IPathService>().MoveToRecycleBin(path + ".bak");
				return;
			}
			if (SocialAPI.Cloud != null)
			{
				SocialAPI.Cloud.Delete(path);
			}
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x0052A668 File Offset: 0x00528868
		private static TagCompound SaveHeader()
		{
			TagCompound tagCompound = new TagCompound();
			tagCompound["modHeaders"] = WorldIO.SaveModHeaders();
			tagCompound["usedMods"] = WorldIO.SaveUsedMods();
			tagCompound["usedModPack"] = WorldIO.SaveUsedModPack();
			tagCompound["generatedWithMods"] = WorldIO.SaveGeneratedWithMods();
			return tagCompound;
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x0052A6BC File Offset: 0x005288BC
		private static TagCompound SaveModHeaders()
		{
			TagCompound modHeaders = new TagCompound();
			TagCompound saveData = new TagCompound();
			foreach (ModSystem system in SystemLoader.Systems)
			{
				try
				{
					system.SaveWorldHeader(saveData);
				}
				catch (Exception e)
				{
					Utils.HandleSaveErrorMessageLogging(NetworkText.FromKey("tModLoader.SaveWorldHeaderExceptionWarning", new object[]
					{
						system.Name,
						system.Mod.Name
					}), true);
					Main.ActiveWorldFileData.ModSaveErrors[system.FullName + ".SaveWorldHeader"] = e.ToString();
					saveData = new TagCompound();
					continue;
				}
				if (saveData.Count != 0)
				{
					modHeaders[system.FullName] = saveData;
					saveData = new TagCompound();
				}
			}
			foreach (KeyValuePair<string, TagCompound> entry in Main.ActiveWorldFileData.ModHeaders)
			{
				ModSystem modSystem;
				if (!ModContent.TryFind<ModSystem>(entry.Key, out modSystem))
				{
					modHeaders[entry.Key] = entry.Value;
				}
			}
			return modHeaders;
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x0052A80C File Offset: 0x00528A0C
		internal static void ReadWorldHeader(WorldFileData data)
		{
			string path = Path.ChangeExtension(data.Path, ".twld");
			bool isCloudSave = data.IsCloudSave;
			if (!FileUtilities.Exists(path, isCloudSave))
			{
				return;
			}
			try
			{
				using (Stream stream = isCloudSave ? new MemoryStream(SocialAPI.Cloud.Read(path)) : new FileStream(path, FileMode.Open))
				{
					using (BinaryReader reader = new BigEndianReader(new GZipStream(stream, CompressionMode.Decompress)))
					{
						if (reader.ReadByte() != 10)
						{
							throw new IOException("Root tag not a TagCompound");
						}
						TagIO.ReadTagImpl(8, reader);
						if (reader.ReadByte() == 10)
						{
							if (!((string)TagIO.ReadTagImpl(8, reader) != "0header"))
							{
								WorldIO.LoadWorldHeader(data, (TagCompound)TagIO.ReadTagImpl(10, reader));
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				ILog tML = Logging.tML;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Error reading .twld header from: ");
				defaultInterpolatedStringHandler.AppendFormatted(path);
				defaultInterpolatedStringHandler.AppendLiteral(" (IsCloudSave=");
				defaultInterpolatedStringHandler.AppendFormatted<bool>(isCloudSave);
				defaultInterpolatedStringHandler.AppendLiteral(")");
				tML.Warn(defaultInterpolatedStringHandler.ToStringAndClear(), ex);
			}
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x0052A95C File Offset: 0x00528B5C
		private static void LoadWorldHeader(WorldFileData data, TagCompound tag)
		{
			WorldIO.LoadModHeaders(data, tag);
			WorldIO.LoadUsedMods(data, tag.GetList<string>("usedMods"));
			WorldIO.LoadUsedModPack(data, tag.GetString("usedModPack"));
			if (tag.ContainsKey("generatedWithMods"))
			{
				WorldIO.LoadGeneratedWithMods(data, tag.GetCompound("generatedWithMods"));
			}
			WorldIO.LoadErrors(data, tag.GetCompound("saveModDataErrors"));
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x0052A9C4 File Offset: 0x00528BC4
		private static void LoadErrors(WorldFileData data, TagCompound tagCompound)
		{
			foreach (KeyValuePair<string, object> entry in tagCompound)
			{
				data.ModSaveErrors[entry.Key] = (string)entry.Value;
			}
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x0052AA24 File Offset: 0x00528C24
		private static void LoadModHeaders(WorldFileData data, TagCompound tag)
		{
			data.ModHeaders = new Dictionary<string, TagCompound>();
			foreach (KeyValuePair<string, object> entry in tag.GetCompound("modHeaders"))
			{
				string fullname = entry.Key;
				ModSystem system;
				if (ModContent.TryFind<ModSystem>(fullname, out system))
				{
					fullname = system.FullName;
				}
				data.ModHeaders[fullname] = (TagCompound)entry.Value;
			}
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x0052AAAC File Offset: 0x00528CAC
		internal static void LoadUsedMods(WorldFileData data, IList<string> usedMods)
		{
			data.usedMods = usedMods;
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x0052AAB8 File Offset: 0x00528CB8
		internal static List<string> SaveUsedMods()
		{
			return (from m in ModLoader.Mods
			select m.Name).Except(new string[]
			{
				"ModLoader"
			}).ToList<string>();
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x0052AB06 File Offset: 0x00528D06
		internal static void LoadUsedModPack(WorldFileData data, string modpack)
		{
			data.modPack = (string.IsNullOrEmpty(modpack) ? null : modpack);
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x0052AB1A File Offset: 0x00528D1A
		internal static string SaveUsedModPack()
		{
			return Path.GetFileNameWithoutExtension(ModOrganizer.ModPackActive);
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x0052AB28 File Offset: 0x00528D28
		internal static void LoadGeneratedWithMods(WorldFileData data, TagCompound tag)
		{
			data.modVersionsDuringWorldGen = new Dictionary<string, Version>();
			foreach (KeyValuePair<string, object> item in tag)
			{
				data.modVersionsDuringWorldGen[item.Key] = new Version((string)item.Value);
			}
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x0052AB98 File Offset: 0x00528D98
		internal static TagCompound SaveGeneratedWithMods()
		{
			if (Main.ActiveWorldFileData.modVersionsDuringWorldGen == null)
			{
				return null;
			}
			TagCompound tag = new TagCompound();
			foreach (KeyValuePair<string, Version> item in Main.ActiveWorldFileData.modVersionsDuringWorldGen)
			{
				tag[item.Key] = item.Value;
			}
			return tag;
		}

		/// <summary> Contains modded error messages from the world load attempt. </summary>
		// Token: 0x04001C0F RID: 7183
		public static CustomModDataException customDataFail;
	}
}
