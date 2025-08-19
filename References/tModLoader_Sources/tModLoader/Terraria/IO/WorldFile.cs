using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using log4net;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Social;
using Terraria.UI;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.IO
{
	// Token: 0x020003E6 RID: 998
	public class WorldFile
	{
		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06003453 RID: 13395 RVA: 0x00557A58 File Offset: 0x00555C58
		// (remove) Token: 0x06003454 RID: 13396 RVA: 0x00557A8C File Offset: 0x00555C8C
		public static event Action OnWorldLoad;

		// Token: 0x06003455 RID: 13397 RVA: 0x00557AC0 File Offset: 0x00555CC0
		public static void CacheSaveTime()
		{
			WorldFile._hasCache = true;
			WorldFile._cachedDayTime = new bool?(Main.dayTime);
			WorldFile._cachedTime = new double?(Main.time);
			WorldFile._cachedMoonPhase = new int?(Main.moonPhase);
			WorldFile._cachedBloodMoon = new bool?(Main.bloodMoon);
			WorldFile._cachedEclipse = new bool?(Main.eclipse);
			WorldFile._cachedCultistDelay = new int?((int)CultistRitual.delay);
			WorldFile._cachedPartyGenuine = new bool?(BirthdayParty.GenuineParty);
			WorldFile._cachedPartyManual = new bool?(BirthdayParty.ManualParty);
			WorldFile._cachedPartyDaysOnCooldown = new int?(BirthdayParty.PartyDaysOnCooldown);
			WorldFile.CachedCelebratingNPCs.Clear();
			WorldFile.CachedCelebratingNPCs.AddRange(BirthdayParty.CelebratingNPCs);
			WorldFile._cachedSandstormHappening = new bool?(Sandstorm.Happening);
			WorldFile._cachedSandstormTimeLeft = new int?((int)Sandstorm.TimeLeft);
			WorldFile._cachedSandstormSeverity = new float?(Sandstorm.Severity);
			WorldFile._cachedSandstormIntendedSeverity = new float?(Sandstorm.IntendedSeverity);
			WorldFile._cachedLanternNightCooldown = new int?(LanternNight.LanternNightsOnCooldown);
			WorldFile._cachedLanternNightGenuine = new bool?(LanternNight.GenuineLanterns);
			WorldFile._cachedLanternNightManual = new bool?(LanternNight.ManualLanterns);
			WorldFile._cachedLanternNightNextNightIsGenuine = new bool?(LanternNight.NextNightIsLanternNight);
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x00557BF0 File Offset: 0x00555DF0
		public static void SetOngoingToTemps()
		{
			Main.dayTime = WorldFile._tempDayTime;
			Main.time = WorldFile._tempTime;
			Main.moonPhase = WorldFile._tempMoonPhase;
			Main.bloodMoon = WorldFile._tempBloodMoon;
			Main.eclipse = WorldFile._tempEclipse;
			Main.raining = WorldFile._tempRaining;
			Main.rainTime = (double)WorldFile._tempRainTime;
			Main.maxRaining = WorldFile._tempMaxRain;
			Main.cloudAlpha = WorldFile._tempMaxRain;
			CultistRitual.delay = (double)WorldFile._tempCultistDelay;
			BirthdayParty.ManualParty = WorldFile._tempPartyManual;
			BirthdayParty.GenuineParty = WorldFile._tempPartyGenuine;
			BirthdayParty.PartyDaysOnCooldown = WorldFile._tempPartyCooldown;
			BirthdayParty.CelebratingNPCs.Clear();
			BirthdayParty.CelebratingNPCs.AddRange(WorldFile.TempPartyCelebratingNPCs);
			Sandstorm.Happening = WorldFile._tempSandstormHappening;
			Sandstorm.TimeLeft = (double)WorldFile._tempSandstormTimeLeft;
			Sandstorm.Severity = WorldFile._tempSandstormSeverity;
			Sandstorm.IntendedSeverity = WorldFile._tempSandstormIntendedSeverity;
			LanternNight.GenuineLanterns = WorldFile._tempLanternNightGenuine;
			LanternNight.LanternNightsOnCooldown = WorldFile._tempLanternNightCooldown;
			LanternNight.ManualLanterns = WorldFile._tempLanternNightManual;
			LanternNight.NextNightIsLanternNight = WorldFile._tempLanternNightNextNightIsGenuine;
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x00557CEB File Offset: 0x00555EEB
		public static bool IsValidWorld(string file, bool cloudSave)
		{
			return WorldFile.GetFileMetadata(file, cloudSave) != null;
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x00557CF8 File Offset: 0x00555EF8
		public static WorldFileData GetAllMetadata(string file, bool cloudSave)
		{
			if (file == null || (cloudSave && SocialAPI.Cloud == null))
			{
				return null;
			}
			WorldFileData worldFileData = new WorldFileData(file, cloudSave);
			if (!FileUtilities.Exists(file, cloudSave))
			{
				return WorldFileData.FromInvalidWorld(file, cloudSave);
			}
			try
			{
				using (Stream stream = cloudSave ? new MemoryStream(SocialAPI.Cloud.Read(file)) : new FileStream(file, FileMode.Open))
				{
					using (BinaryReader binaryReader = new BinaryReader(stream))
					{
						int num = binaryReader.ReadInt32();
						if (num >= 135)
						{
							worldFileData.Metadata = FileMetadata.Read(binaryReader, FileType.World);
						}
						else
						{
							worldFileData.Metadata = FileMetadata.FromCurrentSettings(FileType.World);
						}
						if (num <= 279)
						{
							binaryReader.ReadInt16();
							stream.Position = (long)binaryReader.ReadInt32();
							worldFileData.Name = binaryReader.ReadString();
							if (num >= 179)
							{
								string seed = (num != 179) ? binaryReader.ReadString() : binaryReader.ReadInt32().ToString();
								worldFileData.SetSeed(seed);
								worldFileData.WorldGeneratorVersion = binaryReader.ReadUInt64();
							}
							else
							{
								worldFileData.SetSeedToEmpty();
								worldFileData.WorldGeneratorVersion = 0UL;
							}
							if (num >= 181)
							{
								worldFileData.UniqueId = new Guid(binaryReader.ReadBytes(16));
							}
							else
							{
								worldFileData.UniqueId = Guid.Empty;
							}
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							int y = binaryReader.ReadInt32();
							int x = binaryReader.ReadInt32();
							worldFileData.SetWorldSize(x, y);
							if (num >= 209)
							{
								worldFileData.GameMode = binaryReader.ReadInt32();
								if (num >= 222)
								{
									worldFileData.DrunkWorld = binaryReader.ReadBoolean();
									if (num >= 227)
									{
										worldFileData.ForTheWorthy = binaryReader.ReadBoolean();
									}
									if (num >= 238)
									{
										worldFileData.Anniversary = binaryReader.ReadBoolean();
									}
									if (num >= 239)
									{
										worldFileData.DontStarve = binaryReader.ReadBoolean();
									}
									if (num >= 241)
									{
										worldFileData.NotTheBees = binaryReader.ReadBoolean();
									}
									if (num >= 249)
									{
										worldFileData.RemixWorld = binaryReader.ReadBoolean();
									}
									if (num >= 266)
									{
										worldFileData.NoTrapsWorld = binaryReader.ReadBoolean();
									}
									if (num >= 267)
									{
										worldFileData.ZenithWorld = binaryReader.ReadBoolean();
									}
									else
									{
										worldFileData.ZenithWorld = (worldFileData.DrunkWorld && worldFileData.RemixWorld);
									}
								}
							}
							else if (num >= 112)
							{
								if (binaryReader.ReadBoolean())
								{
									worldFileData.GameMode = 1;
								}
								else
								{
									worldFileData.GameMode = 0;
								}
							}
							if (num >= 141)
							{
								worldFileData.CreationTime = DateTime.FromBinary(binaryReader.ReadInt64());
							}
							else if (!cloudSave)
							{
								worldFileData.CreationTime = File.GetCreationTime(file);
							}
							else
							{
								worldFileData.CreationTime = DateTime.Now;
							}
							binaryReader.ReadByte();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadDouble();
							binaryReader.ReadDouble();
							binaryReader.ReadDouble();
							binaryReader.ReadBoolean();
							binaryReader.ReadInt32();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							worldFileData.HasCrimson = binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							if (num >= 118)
							{
								binaryReader.ReadBoolean();
							}
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadByte();
							binaryReader.ReadInt32();
							worldFileData.IsHardMode = binaryReader.ReadBoolean();
							if (num >= 257)
							{
								binaryReader.ReadBoolean();
							}
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadDouble();
							if (num >= 118)
							{
								binaryReader.ReadDouble();
							}
							if (num >= 113)
							{
								binaryReader.ReadByte();
							}
							binaryReader.ReadBoolean();
							binaryReader.ReadInt32();
							binaryReader.ReadSingle();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadInt32();
							binaryReader.ReadByte();
							binaryReader.ReadByte();
							binaryReader.ReadByte();
							binaryReader.ReadByte();
							binaryReader.ReadByte();
							binaryReader.ReadByte();
							binaryReader.ReadByte();
							binaryReader.ReadByte();
							binaryReader.ReadInt32();
							binaryReader.ReadInt16();
							binaryReader.ReadSingle();
							if (num < 95)
							{
								return worldFileData;
							}
							for (int num2 = binaryReader.ReadInt32(); num2 > 0; num2--)
							{
								binaryReader.ReadString();
							}
							if (num < 99)
							{
								return worldFileData;
							}
							binaryReader.ReadBoolean();
							if (num < 101)
							{
								return worldFileData;
							}
							binaryReader.ReadInt32();
							if (num < 104)
							{
								return worldFileData;
							}
							binaryReader.ReadBoolean();
							if (num >= 129)
							{
								binaryReader.ReadBoolean();
							}
							if (num >= 201)
							{
								binaryReader.ReadBoolean();
							}
							if (num >= 107)
							{
								binaryReader.ReadInt32();
							}
							if (num >= 108)
							{
								binaryReader.ReadInt32();
							}
							if (num < 109)
							{
								return worldFileData;
							}
							int num3 = (int)binaryReader.ReadInt16();
							for (int i = 0; i < num3; i++)
							{
								binaryReader.ReadInt32();
							}
							if (num < 128)
							{
								return worldFileData;
							}
							binaryReader.ReadBoolean();
							if (num < 131)
							{
								return worldFileData;
							}
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							binaryReader.ReadBoolean();
							worldFileData.DefeatedMoonlord = binaryReader.ReadBoolean();
							WorldIO.ReadWorldHeader(worldFileData);
							return worldFileData;
						}
					}
				}
			}
			catch (Exception e)
			{
				ILog terraria = Logging.Terraria;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(43, 2);
				defaultInterpolatedStringHandler.AppendLiteral("Error reading WorldFileData: ");
				defaultInterpolatedStringHandler.AppendFormatted(file);
				defaultInterpolatedStringHandler.AppendLiteral(", IsCloudSave=");
				defaultInterpolatedStringHandler.AppendFormatted<bool>(cloudSave);
				terraria.Error(defaultInterpolatedStringHandler.ToStringAndClear(), e);
			}
			return null;
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x0055838C File Offset: 0x0055658C
		public static WorldFileData CreateMetadata(string name, bool cloudSave, int GameMode)
		{
			WorldFileData worldFileData = new WorldFileData(Main.GetWorldPathFromName(name, cloudSave), cloudSave);
			if (Main.autoGenFileLocation != null && Main.autoGenFileLocation != "")
			{
				worldFileData = new WorldFileData(Main.autoGenFileLocation, cloudSave);
				Main.autoGenFileLocation = null;
			}
			worldFileData.Name = name;
			worldFileData.GameMode = GameMode;
			worldFileData.CreationTime = DateTime.Now;
			worldFileData.Metadata = FileMetadata.FromCurrentSettings(FileType.World);
			worldFileData.SetFavorite(false, true);
			worldFileData.WorldGeneratorVersion = 1198295875585UL;
			worldFileData.UniqueId = Guid.NewGuid();
			if (Main.DefaultSeed == "")
			{
				worldFileData.SetSeedToRandom();
			}
			else
			{
				worldFileData.SetSeed(Main.DefaultSeed);
			}
			return worldFileData;
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x0055843D File Offset: 0x0055663D
		public static void ResetTemps()
		{
			WorldFile._tempRaining = false;
			WorldFile._tempMaxRain = 0f;
			WorldFile._tempRainTime = 0;
			WorldFile._tempDayTime = true;
			WorldFile._tempBloodMoon = false;
			WorldFile._tempEclipse = false;
			WorldFile._tempMoonPhase = 0;
			Main.anglerWhoFinishedToday.Clear();
			Main.anglerQuestFinished = false;
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x00558480 File Offset: 0x00556680
		public unsafe static void ClearTempTiles()
		{
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					if (*Main.tile[i, j].type == 127 || *Main.tile[i, j].type == 504)
					{
						WorldGen.KillTile(i, j, false, false, false);
					}
				}
			}
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x005584EC File Offset: 0x005566EC
		public static void LoadWorld(bool loadFromCloud)
		{
			Main.lockMenuBGChange = true;
			WorldFile._isWorldOnCloud = loadFromCloud;
			Main.checkXMas();
			Main.checkHalloween();
			bool flag = loadFromCloud && SocialAPI.Cloud != null;
			if (!FileUtilities.Exists(Main.worldPathName, flag) && Main.autoGen)
			{
				if (!flag)
				{
					for (int num = Main.worldPathName.Length - 1; num >= 0; num--)
					{
						if (Main.worldPathName.Substring(num, 1) == (Path.DirectorySeparatorChar.ToString() ?? ""))
						{
							Utils.TryCreatingDirectory(Main.worldPathName.Substring(0, num));
							break;
						}
					}
				}
				WorldGen.clearWorld();
				Main.ActiveWorldFileData = WorldFile.CreateMetadata((Main.worldName == "") ? "World" : Main.worldName, flag, Main.GameMode);
				string text = (Main.AutogenSeedName ?? "").Trim();
				if (text.Length == 0)
				{
					Main.ActiveWorldFileData.SetSeedToRandom();
				}
				else
				{
					Main.ActiveWorldFileData.SetSeed(text);
				}
				UIWorldCreation.ProcessSpecialWorldSeeds(text);
				WorldGen.GenerateWorld(Main.ActiveWorldFileData.Seed, Main.AutogenProgress);
				WorldFile.SaveWorld();
			}
			try
			{
				using (MemoryStream memoryStream = new MemoryStream(FileUtilities.ReadAllBytes(Main.worldPathName, flag)))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						try
						{
							WorldGen.loadFailed = false;
							WorldGen.loadSuccess = false;
							int num2 = WorldFile._versionNumber = binaryReader.ReadInt32();
							if (WorldFile._versionNumber <= 0 || WorldFile._versionNumber > 279)
							{
								WorldGen.loadFailed = true;
								return;
							}
							int num3 = (num2 > 87) ? WorldFile.LoadWorld_Version2(binaryReader) : WorldFile.LoadWorld_Version1_Old_BeforeRelease88(binaryReader);
							if (num2 < 141)
							{
								if (!loadFromCloud)
								{
									Main.ActiveWorldFileData.CreationTime = File.GetCreationTime(Main.worldPathName);
								}
								else
								{
									Main.ActiveWorldFileData.CreationTime = DateTime.Now;
								}
							}
							WorldFile.CheckSavedOreTiers();
							binaryReader.Close();
							memoryStream.Close();
							SystemLoader.OnWorldLoad();
							WorldIO.Load(Main.worldPathName, flag);
							if (num3 != 0)
							{
								WorldGen.loadFailed = true;
							}
							else
							{
								WorldGen.loadSuccess = true;
							}
							if (WorldGen.loadFailed || !WorldGen.loadSuccess)
							{
								return;
							}
							WorldFile.ConvertOldTileEntities();
							WorldFile.ClearTempTiles();
							WorldGen.gen = true;
							GenVars.waterLine = Main.maxTilesY;
							Liquid.QuickWater(2, -1, -1);
							WorldGen.WaterCheck();
							int num4 = 0;
							Liquid.quickSettle = true;
							int num5 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
							float num6 = 0f;
							while (Liquid.numLiquid > 0 && num4 < 100000)
							{
								num4++;
								float num7 = (float)(num5 - (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer)) / (float)num5;
								if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > num5)
								{
									num5 = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
								}
								if (num7 > num6)
								{
									num6 = num7;
								}
								else
								{
									num7 = num6;
								}
								Main.statusText = Lang.gen[27].Value + " " + ((int)(num7 * 100f / 2f + 50f)).ToString() + "%";
								Liquid.UpdateLiquid();
							}
							Liquid.quickSettle = false;
							Main.weatherCounter = WorldGen.genRand.Next(3600, 18000);
							Cloud.resetClouds();
							WorldGen.WaterCheck();
							WorldGen.gen = false;
							NPC.setFireFlyChance();
							if (Main.slimeRainTime > 0.0)
							{
								Main.StartSlimeRain(false);
							}
							NPC.SetWorldSpecificMonstersByWorldID();
						}
						catch (Exception lastThrownLoadException)
						{
							WorldFile.LastThrownLoadException = lastThrownLoadException;
							WorldGen.loadFailed = true;
							WorldGen.loadSuccess = false;
							try
							{
								binaryReader.Close();
								memoryStream.Close();
								return;
							}
							catch
							{
								return;
							}
						}
					}
				}
			}
			catch (Exception lastThrownLoadException2)
			{
				WorldFile.LastThrownLoadException = lastThrownLoadException2;
				WorldGen.loadFailed = true;
				WorldGen.loadSuccess = false;
				return;
			}
			if (WorldFile.OnWorldLoad != null)
			{
				WorldFile.OnWorldLoad();
			}
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x00558910 File Offset: 0x00556B10
		public static void CheckSavedOreTiers()
		{
			if (WorldGen.SavedOreTiers.Copper != -1 && WorldGen.SavedOreTiers.Iron != -1 && WorldGen.SavedOreTiers.Silver != -1 && WorldGen.SavedOreTiers.Gold != -1)
			{
				return;
			}
			int[] array = WorldGen.CountTileTypesInWorld(new int[]
			{
				7,
				166,
				6,
				167,
				9,
				168,
				8,
				169
			});
			for (int i = 0; i < array.Length; i += 2)
			{
				int num = array[i];
				int num2 = array[i + 1];
				switch (i)
				{
				case 0:
					if (num > num2)
					{
						WorldGen.SavedOreTiers.Copper = 7;
					}
					else
					{
						WorldGen.SavedOreTiers.Copper = 166;
					}
					break;
				case 2:
					if (num > num2)
					{
						WorldGen.SavedOreTiers.Iron = 6;
					}
					else
					{
						WorldGen.SavedOreTiers.Iron = 167;
					}
					break;
				case 4:
					if (num > num2)
					{
						WorldGen.SavedOreTiers.Silver = 9;
					}
					else
					{
						WorldGen.SavedOreTiers.Silver = 168;
					}
					break;
				case 6:
					if (num > num2)
					{
						WorldGen.SavedOreTiers.Gold = 8;
					}
					else
					{
						WorldGen.SavedOreTiers.Gold = 169;
					}
					break;
				}
			}
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x005589F8 File Offset: 0x00556BF8
		public static void SaveWorld()
		{
			try
			{
				WorldFile.SaveWorld(WorldFile._isWorldOnCloud, false);
			}
			catch (Exception exception)
			{
				FancyErrorPrinter.ShowFileSavingFailError(exception, Main.WorldPath);
				throw;
			}
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x00558A30 File Offset: 0x00556C30
		public static void SaveWorld(bool useCloudSaving, bool resetTime = false)
		{
			if (useCloudSaving && SocialAPI.Cloud == null)
			{
				return;
			}
			if (Main.worldName == "")
			{
				Main.worldName = "World";
			}
			while (WorldGen.IsGeneratingHardMode)
			{
				Main.statusText = Lang.gen[48].Value;
			}
			if (!Monitor.TryEnter(WorldFile.IOLock))
			{
				return;
			}
			try
			{
				FileUtilities.ProtectedInvoke(delegate
				{
					WorldFile.InternalSaveWorld(useCloudSaving, resetTime);
				});
			}
			finally
			{
				Monitor.Exit(WorldFile.IOLock);
			}
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x00558AD4 File Offset: 0x00556CD4
		private static void InternalSaveWorld(bool useCloudSaving, bool resetTime)
		{
			Utils.TryCreatingDirectory(Main.WorldPath);
			if (Main.skipMenu)
			{
				return;
			}
			if (WorldFile._hasCache)
			{
				WorldFile.SetTempToCache();
			}
			else
			{
				WorldFile.SetTempToOngoing();
			}
			if (resetTime)
			{
				WorldFile.ResetTempsToDayTime();
			}
			if (Main.worldPathName == null)
			{
				return;
			}
			if (!BackupIO.archiveLock)
			{
				BackupIO.World.ArchiveWorld(Main.worldPathName, useCloudSaving);
			}
			new Stopwatch().Start();
			byte[] array;
			int num;
			using (MemoryStream memoryStream = new MemoryStream(7000000))
			{
				using (BinaryWriter writer = new BinaryWriter(memoryStream))
				{
					WorldFile.SaveWorld_Version2(writer);
				}
				array = memoryStream.ToArray();
				num = array.Length;
			}
			byte[] array2 = null;
			if (FileUtilities.Exists(Main.worldPathName, useCloudSaving))
			{
				array2 = FileUtilities.ReadAllBytes(Main.worldPathName, useCloudSaving);
			}
			FileUtilities.Write(Main.worldPathName, array, num, useCloudSaving);
			array = FileUtilities.ReadAllBytes(Main.worldPathName, useCloudSaving);
			string text = null;
			bool validated = true;
			using (MemoryStream input = new MemoryStream(array, 0, num, false))
			{
				using (BinaryReader fileIO = new BinaryReader(input))
				{
					if (!Main.validateSaves || WorldFile.ValidateWorld(fileIO))
					{
						if (array2 != null)
						{
							text = Main.worldPathName + ".bak";
							Main.statusText = Lang.gen[50].Value;
						}
						WorldFile.DoRollingBackups(text);
					}
					else
					{
						Logging.Terraria.Error("ValidateWorld failed. A bug in WorldFile.Save has produced an invalid file. Step through with a debugger to find out where ValidateWorld returns false.");
						validated = false;
						text = Main.worldPathName;
					}
				}
			}
			if (text != null && array2 != null)
			{
				FileUtilities.WriteAllBytes(text, array2, useCloudSaving);
			}
			if (validated)
			{
				Main.statusText = Language.GetTextValue("tModLoader.SavingModdedWorldData");
				WorldIO.Save(Main.worldPathName, useCloudSaving);
			}
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x00558C94 File Offset: 0x00556E94
		private static void DoRollingBackups(string backupWorldWritePath)
		{
			if (Main.WorldRollingBackupsCountToKeep <= 1)
			{
				return;
			}
			int num = Main.WorldRollingBackupsCountToKeep;
			if (num > 9)
			{
				num = 9;
			}
			int num2 = 1;
			for (int i = 1; i < num; i++)
			{
				string path = backupWorldWritePath + i.ToString();
				if (i == 1)
				{
					path = backupWorldWritePath;
				}
				if (!FileUtilities.Exists(path, false))
				{
					break;
				}
				num2 = i + 1;
			}
			for (int num3 = num2 - 1; num3 > 0; num3--)
			{
				string text = backupWorldWritePath + num3.ToString();
				if (num3 == 1)
				{
					text = backupWorldWritePath;
				}
				string destination = backupWorldWritePath + (num3 + 1).ToString();
				if (FileUtilities.Exists(text, false))
				{
					FileUtilities.Move(text, destination, false, true, true);
				}
			}
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x00558D3C File Offset: 0x00556F3C
		private static void ResetTempsToDayTime()
		{
			WorldFile._tempDayTime = true;
			WorldFile._tempTime = 13500.0;
			WorldFile._tempMoonPhase = 0;
			WorldFile._tempBloodMoon = false;
			WorldFile._tempEclipse = false;
			WorldFile._tempCultistDelay = 86400;
			WorldFile._tempPartyManual = false;
			WorldFile._tempPartyGenuine = false;
			if (Main.tenthAnniversaryWorld)
			{
				WorldFile._tempPartyGenuine = true;
			}
			WorldFile._tempPartyCooldown = 0;
			WorldFile.TempPartyCelebratingNPCs.Clear();
			WorldFile._tempSandstormHappening = false;
			WorldFile._tempSandstormTimeLeft = 0;
			WorldFile._tempSandstormSeverity = 0f;
			WorldFile._tempSandstormIntendedSeverity = 0f;
			WorldFile._tempLanternNightCooldown = 0;
			WorldFile._tempLanternNightGenuine = false;
			WorldFile._tempLanternNightManual = false;
			WorldFile._tempLanternNightNextNightIsGenuine = false;
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x00558DDC File Offset: 0x00556FDC
		private static void SetTempToOngoing()
		{
			WorldFile._tempDayTime = Main.dayTime;
			WorldFile._tempTime = Main.time;
			WorldFile._tempMoonPhase = Main.moonPhase;
			WorldFile._tempBloodMoon = Main.bloodMoon;
			WorldFile._tempEclipse = Main.eclipse;
			WorldFile._tempCultistDelay = (int)CultistRitual.delay;
			WorldFile._tempPartyManual = BirthdayParty.ManualParty;
			WorldFile._tempPartyGenuine = BirthdayParty.GenuineParty;
			WorldFile._tempPartyCooldown = BirthdayParty.PartyDaysOnCooldown;
			WorldFile.TempPartyCelebratingNPCs.Clear();
			WorldFile.TempPartyCelebratingNPCs.AddRange(BirthdayParty.CelebratingNPCs);
			WorldFile._tempSandstormHappening = Sandstorm.Happening;
			WorldFile._tempSandstormTimeLeft = (int)Sandstorm.TimeLeft;
			WorldFile._tempSandstormSeverity = Sandstorm.Severity;
			WorldFile._tempSandstormIntendedSeverity = Sandstorm.IntendedSeverity;
			WorldFile._tempRaining = Main.raining;
			WorldFile._tempRainTime = (int)Main.rainTime;
			WorldFile._tempMaxRain = Main.maxRaining;
			WorldFile._tempLanternNightCooldown = LanternNight.LanternNightsOnCooldown;
			WorldFile._tempLanternNightGenuine = LanternNight.GenuineLanterns;
			WorldFile._tempLanternNightManual = LanternNight.ManualLanterns;
			WorldFile._tempLanternNightNextNightIsGenuine = LanternNight.NextNightIsLanternNight;
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x00558ED0 File Offset: 0x005570D0
		private static void SetTempToCache()
		{
			WorldFile._hasCache = false;
			WorldFile._tempDayTime = WorldFile._cachedDayTime.Value;
			WorldFile._tempTime = WorldFile._cachedTime.Value;
			WorldFile._tempMoonPhase = WorldFile._cachedMoonPhase.Value;
			WorldFile._tempBloodMoon = WorldFile._cachedBloodMoon.Value;
			WorldFile._tempEclipse = WorldFile._cachedEclipse.Value;
			WorldFile._tempCultistDelay = WorldFile._cachedCultistDelay.Value;
			WorldFile._tempPartyManual = WorldFile._cachedPartyManual.Value;
			WorldFile._tempPartyGenuine = WorldFile._cachedPartyGenuine.Value;
			WorldFile._tempPartyCooldown = WorldFile._cachedPartyDaysOnCooldown.Value;
			WorldFile.TempPartyCelebratingNPCs.Clear();
			WorldFile.TempPartyCelebratingNPCs.AddRange(WorldFile.CachedCelebratingNPCs);
			WorldFile._tempSandstormHappening = WorldFile._cachedSandstormHappening.Value;
			WorldFile._tempSandstormTimeLeft = WorldFile._cachedSandstormTimeLeft.Value;
			WorldFile._tempSandstormSeverity = WorldFile._cachedSandstormSeverity.Value;
			WorldFile._tempSandstormIntendedSeverity = WorldFile._cachedSandstormIntendedSeverity.Value;
			WorldFile._tempRaining = Main.raining;
			WorldFile._tempRainTime = (int)Main.rainTime;
			WorldFile._tempMaxRain = Main.maxRaining;
			WorldFile._tempLanternNightCooldown = WorldFile._cachedLanternNightCooldown.Value;
			WorldFile._tempLanternNightGenuine = WorldFile._cachedLanternNightGenuine.Value;
			WorldFile._tempLanternNightManual = WorldFile._cachedLanternNightManual.Value;
			WorldFile._tempLanternNightNextNightIsGenuine = WorldFile._cachedLanternNightNextNightIsGenuine.Value;
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x0055901C File Offset: 0x0055721C
		private unsafe static void ConvertOldTileEntities()
		{
			List<Point> list = new List<Point>();
			List<Point> list2 = new List<Point>();
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					Tile tile = Main.tile[i, j];
					if ((*tile.type == 128 || *tile.type == 269) && *tile.frameY == 0 && (*tile.frameX % 100 == 0 || *tile.frameX % 100 == 36))
					{
						list.Add(new Point(i, j));
					}
					if (*tile.type == 334 && *tile.frameY == 0 && *tile.frameX % 54 == 0)
					{
						list2.Add(new Point(i, j));
					}
					if (*tile.type == 49 && (*tile.frameX == -1 || *tile.frameY == -1))
					{
						*tile.frameX = 0;
						*tile.frameY = 0;
					}
				}
			}
			foreach (Point item in list)
			{
				if (WorldGen.InWorld(item.X, item.Y, 5))
				{
					int frameX = (int)(*Main.tile[item.X, item.Y].frameX);
					int frameX2 = (int)(*Main.tile[item.X, item.Y + 1].frameX);
					int frameX3 = (int)(*Main.tile[item.X, item.Y + 2].frameX);
					for (int k = 0; k < 2; k++)
					{
						for (int l = 0; l < 3; l++)
						{
							Tile tile2 = Main.tile[item.X + k, item.Y + l];
							ref short frameX5 = ref tile2.frameX;
							frameX5 %= 100;
							if (*tile2.type == 269)
							{
								ref short frameX6 = ref tile2.frameX;
								frameX6 += 72;
							}
							*tile2.type = 470;
						}
					}
					int num = TEDisplayDoll.Place(item.X, item.Y);
					if (num != -1)
					{
						(TileEntity.ByID[num] as TEDisplayDoll).SetInventoryFromMannequin(frameX, frameX2, frameX3);
					}
				}
			}
			foreach (Point item2 in list2)
			{
				if (WorldGen.InWorld(item2.X, item2.Y, 5))
				{
					bool flag = *Main.tile[item2.X, item2.Y].frameX >= 54;
					short num3 = *Main.tile[item2.X, item2.Y + 1].frameX;
					int frameX4 = (int)(*Main.tile[item2.X + 1, item2.Y + 1].frameX);
					bool flag2 = num3 >= 5000;
					int num2 = (int)(num3 % 5000);
					num2 -= 100;
					int prefix = frameX4 - ((frameX4 >= 25000) ? 25000 : 10000);
					for (int m = 0; m < 3; m++)
					{
						for (int n = 0; n < 3; n++)
						{
							Tile tile3 = Main.tile[item2.X + m, item2.Y + n];
							*tile3.type = 471;
							*tile3.frameX = (short)((flag ? 54 : 0) + m * 18);
							*tile3.frameY = (short)(n * 18);
						}
					}
					if (TEWeaponsRack.Place(item2.X, item2.Y) != -1 && flag2)
					{
						TEWeaponsRack.TryPlacing(item2.X, item2.Y, new Item(num2, 1, prefix), 1);
					}
				}
			}
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x0055946C File Offset: 0x0055766C
		public static void SaveWorld_Version2(BinaryWriter writer)
		{
			int[] pointers = new int[]
			{
				WorldFile.SaveFileFormatHeader(writer),
				WorldFile.SaveWorldHeader(writer),
				WorldFile.SaveWorldTiles(writer),
				WorldFile.SaveChests(writer),
				WorldFile.SaveSigns(writer),
				WorldFile.SaveNPCs(writer),
				WorldFile.SaveTileEntities(writer),
				WorldFile.SaveWeightedPressurePlates(writer),
				WorldFile.SaveTownManager(writer),
				WorldFile.SaveBestiary(writer),
				WorldFile.SaveCreativePowers(writer)
			};
			WorldFile.SaveFooter(writer);
			WorldFile.SaveHeaderPointers(writer, pointers);
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x005594F8 File Offset: 0x005576F8
		public static int SaveFileFormatHeader(BinaryWriter writer)
		{
			ushort count = TileID.Count;
			short num = 11;
			writer.Write(279);
			Main.WorldFileMetadata.IncrementAndWrite(writer);
			writer.Write(num);
			for (int i = 0; i < (int)num; i++)
			{
				writer.Write(0);
			}
			writer.Write(count);
			byte b = 0;
			byte b2 = 1;
			for (int j = 0; j < (int)count; j++)
			{
				if (Main.tileFrameImportant[j])
				{
					b |= b2;
				}
				if (b2 == 128)
				{
					writer.Write(b);
					b = 0;
					b2 = 1;
				}
				else
				{
					b2 = (byte)(b2 << 1);
				}
			}
			if (b2 != 1)
			{
				writer.Write(b);
			}
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x005595A0 File Offset: 0x005577A0
		public static int SaveHeaderPointers(BinaryWriter writer, int[] pointers)
		{
			writer.BaseStream.Position = 0L;
			writer.Write(279);
			writer.BaseStream.Position += 20L;
			writer.Write((short)pointers.Length);
			for (int i = 0; i < pointers.Length; i++)
			{
				writer.Write(pointers[i]);
			}
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x00559608 File Offset: 0x00557808
		public static int SaveWorldHeader(BinaryWriter writer)
		{
			writer.Write(Main.worldName);
			writer.Write(Main.ActiveWorldFileData.SeedText);
			writer.Write(Main.ActiveWorldFileData.WorldGeneratorVersion);
			writer.Write(Main.ActiveWorldFileData.UniqueId.ToByteArray());
			writer.Write(Main.worldID);
			writer.Write((int)Main.leftWorld);
			writer.Write((int)Main.rightWorld);
			writer.Write((int)Main.topWorld);
			writer.Write((int)Main.bottomWorld);
			writer.Write(Main.maxTilesY);
			writer.Write(Main.maxTilesX);
			writer.Write(Main.GameMode);
			writer.Write(Main.drunkWorld);
			writer.Write(Main.getGoodWorld);
			writer.Write(Main.tenthAnniversaryWorld);
			writer.Write(Main.dontStarveWorld);
			writer.Write(Main.notTheBeesWorld);
			writer.Write(Main.remixWorld);
			writer.Write(Main.noTrapsWorld);
			writer.Write(Main.zenithWorld);
			writer.Write(Main.ActiveWorldFileData.CreationTime.ToBinary());
			writer.Write((byte)Main.moonType);
			writer.Write(Main.treeX[0]);
			writer.Write(Main.treeX[1]);
			writer.Write(Main.treeX[2]);
			writer.Write(Main.treeStyle[0]);
			writer.Write(Main.treeStyle[1]);
			writer.Write(Main.treeStyle[2]);
			writer.Write(Main.treeStyle[3]);
			writer.Write(Main.caveBackX[0]);
			writer.Write(Main.caveBackX[1]);
			writer.Write(Main.caveBackX[2]);
			writer.Write(Main.caveBackStyle[0]);
			writer.Write(Main.caveBackStyle[1]);
			writer.Write(Main.caveBackStyle[2]);
			writer.Write(Main.caveBackStyle[3]);
			writer.Write(Main.iceBackStyle);
			writer.Write(Main.jungleBackStyle);
			writer.Write(Main.hellBackStyle);
			writer.Write(Main.spawnTileX);
			writer.Write(Main.spawnTileY);
			writer.Write(Main.worldSurface);
			writer.Write(Main.rockLayer);
			writer.Write(WorldFile._tempTime);
			writer.Write(WorldFile._tempDayTime);
			writer.Write(WorldFile._tempMoonPhase);
			writer.Write(WorldFile._tempBloodMoon);
			writer.Write(WorldFile._tempEclipse);
			writer.Write(Main.dungeonX);
			writer.Write(Main.dungeonY);
			writer.Write(WorldGen.crimson);
			writer.Write(NPC.downedBoss1);
			writer.Write(NPC.downedBoss2);
			writer.Write(NPC.downedBoss3);
			writer.Write(NPC.downedQueenBee);
			writer.Write(NPC.downedMechBoss1);
			writer.Write(NPC.downedMechBoss2);
			writer.Write(NPC.downedMechBoss3);
			writer.Write(NPC.downedMechBossAny);
			writer.Write(NPC.downedPlantBoss);
			writer.Write(NPC.downedGolemBoss);
			writer.Write(NPC.downedSlimeKing);
			writer.Write(NPC.savedGoblin);
			writer.Write(NPC.savedWizard);
			writer.Write(NPC.savedMech);
			writer.Write(NPC.downedGoblins);
			writer.Write(NPC.downedClown);
			writer.Write(NPC.downedFrost);
			writer.Write(NPC.downedPirates);
			writer.Write(WorldGen.shadowOrbSmashed);
			writer.Write(WorldGen.spawnMeteor);
			writer.Write((byte)WorldGen.shadowOrbCount);
			writer.Write(WorldGen.altarCount);
			writer.Write(Main.hardMode);
			writer.Write(Main.afterPartyOfDoom);
			writer.Write(Main.invasionDelay);
			writer.Write(Main.invasionSize);
			writer.Write(Main.invasionType);
			writer.Write(Main.invasionX);
			writer.Write(Main.slimeRainTime);
			writer.Write((byte)Main.sundialCooldown);
			writer.Write(WorldFile._tempRaining);
			writer.Write(WorldFile._tempRainTime);
			writer.Write(WorldFile._tempMaxRain);
			writer.Write(WorldGen.SavedOreTiers.Cobalt);
			writer.Write(WorldGen.SavedOreTiers.Mythril);
			writer.Write(WorldGen.SavedOreTiers.Adamantite);
			writer.Write((byte)WorldGen.treeBG1);
			writer.Write((byte)WorldGen.corruptBG);
			writer.Write((byte)WorldGen.jungleBG);
			writer.Write((byte)WorldGen.snowBG);
			writer.Write((byte)WorldGen.hallowBG);
			writer.Write((byte)WorldGen.crimsonBG);
			writer.Write((byte)WorldGen.desertBG);
			writer.Write((byte)WorldGen.oceanBG);
			writer.Write((int)Main.cloudBGActive);
			writer.Write((short)Main.numClouds);
			writer.Write(Main.windSpeedTarget);
			writer.Write(Main.anglerWhoFinishedToday.Count);
			for (int i = 0; i < Main.anglerWhoFinishedToday.Count; i++)
			{
				writer.Write(Main.anglerWhoFinishedToday[i]);
			}
			writer.Write(NPC.savedAngler);
			writer.Write((Main.anglerQuest < ItemLoader.vanillaQuestFishCount) ? Main.anglerQuest : 0);
			writer.Write(NPC.savedStylist);
			writer.Write(NPC.savedTaxCollector);
			writer.Write(NPC.savedGolfer);
			writer.Write(Main.invasionSizeStart);
			writer.Write(WorldFile._tempCultistDelay);
			writer.Write(NPCID.Count);
			for (int j = 0; j < (int)NPCID.Count; j++)
			{
				writer.Write(NPC.killCount[j]);
			}
			writer.Write(Main.fastForwardTimeToDawn);
			writer.Write(NPC.downedFishron);
			writer.Write(NPC.downedMartians);
			writer.Write(NPC.downedAncientCultist);
			writer.Write(NPC.downedMoonlord);
			writer.Write(NPC.downedHalloweenKing);
			writer.Write(NPC.downedHalloweenTree);
			writer.Write(NPC.downedChristmasIceQueen);
			writer.Write(NPC.downedChristmasSantank);
			writer.Write(NPC.downedChristmasTree);
			writer.Write(NPC.downedTowerSolar);
			writer.Write(NPC.downedTowerVortex);
			writer.Write(NPC.downedTowerNebula);
			writer.Write(NPC.downedTowerStardust);
			writer.Write(NPC.TowerActiveSolar);
			writer.Write(NPC.TowerActiveVortex);
			writer.Write(NPC.TowerActiveNebula);
			writer.Write(NPC.TowerActiveStardust);
			writer.Write(NPC.LunarApocalypseIsUp);
			writer.Write(WorldFile._tempPartyManual);
			writer.Write(WorldFile._tempPartyGenuine);
			writer.Write(WorldFile._tempPartyCooldown);
			writer.Write(WorldFile.TempPartyCelebratingNPCs.Count);
			for (int k = 0; k < WorldFile.TempPartyCelebratingNPCs.Count; k++)
			{
				writer.Write(WorldFile.TempPartyCelebratingNPCs[k]);
			}
			writer.Write(WorldFile._tempSandstormHappening);
			writer.Write(WorldFile._tempSandstormTimeLeft);
			writer.Write(WorldFile._tempSandstormSeverity);
			writer.Write(WorldFile._tempSandstormIntendedSeverity);
			writer.Write(NPC.savedBartender);
			DD2Event.Save(writer);
			writer.Write((byte)WorldGen.mushroomBG);
			writer.Write((byte)WorldGen.underworldBG);
			writer.Write((byte)WorldGen.treeBG2);
			writer.Write((byte)WorldGen.treeBG3);
			writer.Write((byte)WorldGen.treeBG4);
			writer.Write(NPC.combatBookWasUsed);
			writer.Write(WorldFile._tempLanternNightCooldown);
			writer.Write(WorldFile._tempLanternNightGenuine);
			writer.Write(WorldFile._tempLanternNightManual);
			writer.Write(WorldFile._tempLanternNightNextNightIsGenuine);
			WorldGen.TreeTops.Save(writer);
			writer.Write(Main.forceHalloweenForToday);
			writer.Write(Main.forceXMasForToday);
			writer.Write(WorldGen.SavedOreTiers.Copper);
			writer.Write(WorldGen.SavedOreTiers.Iron);
			writer.Write(WorldGen.SavedOreTiers.Silver);
			writer.Write(WorldGen.SavedOreTiers.Gold);
			writer.Write(NPC.boughtCat);
			writer.Write(NPC.boughtDog);
			writer.Write(NPC.boughtBunny);
			writer.Write(NPC.downedEmpressOfLight);
			writer.Write(NPC.downedQueenSlime);
			writer.Write(NPC.downedDeerclops);
			writer.Write(NPC.unlockedSlimeBlueSpawn);
			writer.Write(NPC.unlockedMerchantSpawn);
			writer.Write(NPC.unlockedDemolitionistSpawn);
			writer.Write(NPC.unlockedPartyGirlSpawn);
			writer.Write(NPC.unlockedDyeTraderSpawn);
			writer.Write(NPC.unlockedTruffleSpawn);
			writer.Write(NPC.unlockedArmsDealerSpawn);
			writer.Write(NPC.unlockedNurseSpawn);
			writer.Write(NPC.unlockedPrincessSpawn);
			writer.Write(NPC.combatBookVolumeTwoWasUsed);
			writer.Write(NPC.peddlersSatchelWasUsed);
			writer.Write(NPC.unlockedSlimeGreenSpawn);
			writer.Write(NPC.unlockedSlimeOldSpawn);
			writer.Write(NPC.unlockedSlimePurpleSpawn);
			writer.Write(NPC.unlockedSlimeRainbowSpawn);
			writer.Write(NPC.unlockedSlimeRedSpawn);
			writer.Write(NPC.unlockedSlimeYellowSpawn);
			writer.Write(NPC.unlockedSlimeCopperSpawn);
			writer.Write(Main.fastForwardTimeToDusk);
			writer.Write((byte)Main.moondialCooldown);
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x00559EA0 File Offset: 0x005580A0
		public unsafe static int SaveWorldTiles(BinaryWriter writer)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				float num = (float)i / (float)Main.maxTilesX;
				Main.statusText = Lang.gen[49].Value + " " + ((int)(num * 100f + 1f)).ToString() + "%";
				for (int num2 = 0; num2 < Main.maxTilesY; num2++)
				{
					Tile tile = Main.tile[i, num2];
					int num3 = 4;
					byte b6;
					byte b5;
					byte b4;
					byte b3 = b4 = (b5 = (b6 = 0));
					bool flag = false;
					if (tile.active() && *tile.type < TileID.Count)
					{
						flag = true;
					}
					if (flag)
					{
						b4 |= 2;
						array[num3] = (byte)(*tile.type);
						num3++;
						if (*tile.type > 255)
						{
							array[num3] = (byte)(*tile.type >> 8);
							num3++;
							b4 |= 32;
						}
						if (Main.tileFrameImportant[(int)(*tile.type)])
						{
							short frameX = *tile.frameX;
							TileIO.VanillaSaveFrames(tile, ref frameX);
							array[num3] = (byte)(frameX & 255);
							num3++;
							array[num3] = (byte)(((int)frameX & 65280) >> 8);
							num3++;
							array[num3] = (byte)(*tile.frameY & 255);
							num3++;
							array[num3] = (byte)(((int)(*tile.frameY) & 65280) >> 8);
							num3++;
						}
						if (tile.color() != 0)
						{
							b5 |= 8;
							array[num3] = tile.color();
							num3++;
						}
					}
					if (*tile.wall != 0 && *tile.wall < WallID.Count)
					{
						b4 |= 4;
						array[num3] = (byte)(*tile.wall);
						num3++;
						if (tile.wallColor() != 0)
						{
							b5 |= 16;
							array[num3] = tile.wallColor();
							num3++;
						}
					}
					if (*tile.liquid != 0)
					{
						if (!tile.shimmer())
						{
							b4 = (tile.lava() ? (b4 | 16) : ((!tile.honey()) ? (b4 | 8) : (b4 | 24)));
						}
						else
						{
							b5 |= 128;
							b4 |= 8;
						}
						array[num3] = *tile.liquid;
						num3++;
					}
					if (tile.wire())
					{
						b3 |= 2;
					}
					if (tile.wire2())
					{
						b3 |= 4;
					}
					if (tile.wire3())
					{
						b3 |= 8;
					}
					int num4 = tile.halfBrick() ? 16 : ((tile.slope() != 0) ? ((int)(tile.slope() + 1) << 4) : 0);
					b3 |= (byte)num4;
					if (tile.actuator())
					{
						b5 |= 2;
					}
					if (tile.inActive())
					{
						b5 |= 4;
					}
					if (tile.wire4())
					{
						b5 |= 32;
					}
					if (*tile.wall > 255)
					{
						array[num3] = (byte)(*tile.wall >> 8);
						num3++;
						b5 |= 64;
					}
					if (tile.invisibleBlock())
					{
						b6 |= 2;
					}
					if (tile.invisibleWall())
					{
						b6 |= 4;
					}
					if (tile.fullbrightBlock())
					{
						b6 |= 8;
					}
					if (tile.fullbrightWall())
					{
						b6 |= 16;
					}
					int num5 = 3;
					if (b6 != 0)
					{
						b5 |= 1;
						array[num5] = b6;
						num5--;
					}
					if (b5 != 0)
					{
						b3 |= 1;
						array[num5] = b5;
						num5--;
					}
					if (b3 != 0)
					{
						b4 |= 1;
						array[num5] = b3;
						num5--;
					}
					short num6 = 0;
					int num7 = num2 + 1;
					int num8 = Main.maxTilesY - num2 - 1;
					while (num8 > 0 && tile.isTheSameAs(Main.tile[i, num7]) && TileID.Sets.AllowsSaveCompressionBatching[(int)(*tile.type)])
					{
						num6 += 1;
						num8--;
						num7++;
					}
					num2 += (int)num6;
					if (num6 > 0)
					{
						array[num3] = (byte)(num6 & 255);
						num3++;
						if (num6 > 255)
						{
							b4 |= 128;
							array[num3] = (byte)(((int)num6 & 65280) >> 8);
							num3++;
						}
						else
						{
							b4 |= 64;
						}
					}
					array[num5] = b4;
					writer.Write(array, num5, num3 - num5);
				}
			}
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x0055A30C File Offset: 0x0055850C
		public unsafe static int SaveChests(BinaryWriter writer)
		{
			short num = 0;
			for (int i = 0; i < 8000; i++)
			{
				Chest chest = Main.chest[i];
				if (chest != null)
				{
					bool flag = false;
					for (int j = chest.x; j <= chest.x + 1; j++)
					{
						for (int k = chest.y; k <= chest.y + 1; k++)
						{
							if (j < 0 || k < 0 || j >= Main.maxTilesX || k >= Main.maxTilesY)
							{
								flag = true;
								break;
							}
							Tile tile = Main.tile[j, k];
							if (!tile.active() || !Main.tileContainer[(int)(*tile.type)])
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						Main.chest[i] = null;
					}
					else
					{
						num += 1;
					}
				}
			}
			writer.Write(num);
			writer.Write(40);
			for (int l = 0; l < 8000; l++)
			{
				Chest chest2 = Main.chest[l];
				if (chest2 != null)
				{
					writer.Write(chest2.x);
					writer.Write(chest2.y);
					writer.Write(chest2.name);
					for (int m = 0; m < 40; m++)
					{
						Item item = chest2.item[m];
						if (item == null || ItemID.Sets.ItemsThatShouldNotBeInInventory[item.type] || item.ModItem != null)
						{
							writer.Write(0);
						}
						else
						{
							if (item.stack < 0)
							{
								item.stack = 1;
							}
							writer.Write((short)item.stack);
							if (item.stack > 0)
							{
								writer.Write(item.netID);
								ItemIO.WriteByteVanillaPrefix(item, writer);
							}
						}
					}
				}
			}
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x0055A4C8 File Offset: 0x005586C8
		public static int SaveSigns(BinaryWriter writer)
		{
			short num = 0;
			for (int i = 0; i < 1000; i++)
			{
				Sign sign = Main.sign[i];
				if (sign != null && sign.text != null)
				{
					num += 1;
				}
			}
			writer.Write(num);
			for (int j = 0; j < 1000; j++)
			{
				Sign sign2 = Main.sign[j];
				if (sign2 != null && sign2.text != null)
				{
					writer.Write(sign2.text);
					writer.Write(sign2.x);
					writer.Write(sign2.y);
				}
			}
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x0055A560 File Offset: 0x00558760
		public static int SaveNPCs(BinaryWriter writer)
		{
			bool[] array = RuntimeHelpers.GetSubArray<bool>(NPC.ShimmeredTownNPCs, Range.EndAt((int)NPCID.Count));
			writer.Write(array.Count(true));
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i])
				{
					writer.Write(i);
				}
			}
			for (int j = 0; j < Main.npc.Length; j++)
			{
				NPC nPC = Main.npc[j];
				if (nPC.active && nPC.townNPC && nPC.type != 368 && !NPCLoader.IsModNPC(nPC))
				{
					writer.Write(nPC.active);
					writer.Write(nPC.netID);
					writer.Write(nPC.GivenName);
					writer.Write(nPC.position.X);
					writer.Write(nPC.position.Y);
					writer.Write(nPC.homeless);
					writer.Write(nPC.homeTileX);
					writer.Write(nPC.homeTileY);
					BitsByte bitsByte = 0;
					bitsByte[0] = nPC.townNPC;
					writer.Write(bitsByte);
					if (bitsByte[0])
					{
						writer.Write(nPC.townNpcVariationIndex);
					}
				}
			}
			writer.Write(false);
			for (int k = 0; k < Main.npc.Length; k++)
			{
				NPC nPC2 = Main.npc[k];
				if (nPC2.active && NPCID.Sets.SavesAndLoads[nPC2.type] && !NPCLoader.IsModNPC(nPC2))
				{
					writer.Write(nPC2.active);
					writer.Write(nPC2.netID);
					writer.WriteVector2(nPC2.position);
				}
			}
			writer.Write(false);
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x0055A724 File Offset: 0x00558924
		public static int SaveFooter(BinaryWriter writer)
		{
			writer.Write(true);
			writer.Write(Main.worldName);
			writer.Write(Main.worldID);
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x0055A750 File Offset: 0x00558950
		public static int LoadWorld_Version2(BinaryReader reader)
		{
			reader.BaseStream.Position = 0L;
			bool[] importance;
			int[] positions;
			if (!WorldFile.LoadFileFormatHeader(reader, out importance, out positions))
			{
				return 5;
			}
			if (reader.BaseStream.Position != (long)positions[0])
			{
				return 5;
			}
			WorldFile.LoadHeader(reader);
			if (reader.BaseStream.Position != (long)positions[1])
			{
				return 5;
			}
			WorldFile.LoadWorldTiles(reader, importance);
			if (reader.BaseStream.Position != (long)positions[2])
			{
				return 5;
			}
			WorldFile.LoadChests(reader);
			if (reader.BaseStream.Position != (long)positions[3])
			{
				return 5;
			}
			WorldFile.LoadSigns(reader);
			if (reader.BaseStream.Position != (long)positions[4])
			{
				return 5;
			}
			WorldFile.LoadNPCs(reader);
			if (reader.BaseStream.Position != (long)positions[5])
			{
				return 5;
			}
			if (WorldFile._versionNumber >= 116)
			{
				if (WorldFile._versionNumber < 122)
				{
					WorldFile.LoadDummies(reader);
					if (reader.BaseStream.Position != (long)positions[6])
					{
						return 5;
					}
				}
				else
				{
					WorldFile.LoadTileEntities(reader);
					if (reader.BaseStream.Position != (long)positions[6])
					{
						return 5;
					}
				}
			}
			if (WorldFile._versionNumber >= 170)
			{
				WorldFile.LoadWeightedPressurePlates(reader);
				if (reader.BaseStream.Position != (long)positions[7])
				{
					return 5;
				}
			}
			if (WorldFile._versionNumber >= 189)
			{
				WorldFile.LoadTownManager(reader);
				if (reader.BaseStream.Position != (long)positions[8])
				{
					return 5;
				}
			}
			if (WorldFile._versionNumber >= 210)
			{
				WorldFile.LoadBestiary(reader, WorldFile._versionNumber);
				if (reader.BaseStream.Position != (long)positions[9])
				{
					return 5;
				}
			}
			else
			{
				WorldFile.LoadBestiaryForVersionsBefore210();
			}
			if (WorldFile._versionNumber >= 220)
			{
				WorldFile.LoadCreativePowers(reader, WorldFile._versionNumber);
				if (reader.BaseStream.Position != (long)positions[10])
				{
					return 5;
				}
			}
			WorldFile.LoadWorld_LastMinuteFixes();
			return WorldFile.LoadFooter(reader);
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x0055A8FD File Offset: 0x00558AFD
		private static void LoadWorld_LastMinuteFixes()
		{
			if (WorldFile._versionNumber < 258)
			{
				WorldFile.ConvertIlluminantPaintToNewField();
			}
			WorldFile.FixAgainstExploits();
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x0055A918 File Offset: 0x00558B18
		private static void FixAgainstExploits()
		{
			for (int i = 0; i < 8000; i++)
			{
				Chest chest = Main.chest[i];
				if (chest != null)
				{
					chest.FixLoadedData();
				}
			}
			foreach (TileEntity tileEntity in TileEntity.ByID.Values)
			{
				IFixLoadedData fixLoadedData = tileEntity as IFixLoadedData;
				if (fixLoadedData != null)
				{
					fixLoadedData.FixLoadedData();
				}
			}
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x0055A998 File Offset: 0x00558B98
		public static bool LoadFileFormatHeader(BinaryReader reader, out bool[] importance, out int[] positions)
		{
			importance = null;
			positions = null;
			if ((WorldFile._versionNumber = reader.ReadInt32()) >= 135)
			{
				try
				{
					Main.WorldFileMetadata = FileMetadata.Read(reader, FileType.World);
					goto IL_4E;
				}
				catch (FormatException value)
				{
					Console.WriteLine(Language.GetTextValue("Error.UnableToLoadWorld"));
					Console.WriteLine(value);
					return false;
				}
			}
			Main.WorldFileMetadata = FileMetadata.FromCurrentSettings(FileType.World);
			IL_4E:
			short num = reader.ReadInt16();
			positions = new int[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				positions[i] = reader.ReadInt32();
			}
			ushort num2 = reader.ReadUInt16();
			importance = new bool[(int)num2];
			byte b = 0;
			byte b2 = 128;
			for (int j = 0; j < (int)num2; j++)
			{
				if (b2 == 128)
				{
					b = reader.ReadByte();
					b2 = 1;
				}
				else
				{
					b2 = (byte)(b2 << 1);
				}
				if ((b & b2) == b2)
				{
					importance[j] = true;
				}
			}
			return true;
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x0055AA7C File Offset: 0x00558C7C
		public static void LoadHeader(BinaryReader reader)
		{
			int versionNumber = WorldFile._versionNumber;
			Main.worldName = reader.ReadString();
			if (versionNumber >= 179)
			{
				string seed = (versionNumber != 179) ? reader.ReadString() : reader.ReadInt32().ToString();
				Main.ActiveWorldFileData.SetSeed(seed);
				Main.ActiveWorldFileData.WorldGeneratorVersion = reader.ReadUInt64();
			}
			if (versionNumber >= 181)
			{
				Main.ActiveWorldFileData.UniqueId = new Guid(reader.ReadBytes(16));
			}
			else
			{
				Main.ActiveWorldFileData.UniqueId = Guid.NewGuid();
			}
			Main.worldID = reader.ReadInt32();
			Main.leftWorld = (float)reader.ReadInt32();
			Main.rightWorld = (float)reader.ReadInt32();
			Main.topWorld = (float)reader.ReadInt32();
			Main.bottomWorld = (float)reader.ReadInt32();
			Main.maxTilesY = reader.ReadInt32();
			Main.maxTilesX = reader.ReadInt32();
			WorldGen.clearWorld();
			if (versionNumber >= 209)
			{
				Main.GameMode = reader.ReadInt32();
				if (versionNumber >= 222)
				{
					Main.drunkWorld = reader.ReadBoolean();
				}
				if (versionNumber >= 227)
				{
					Main.getGoodWorld = reader.ReadBoolean();
				}
				if (versionNumber >= 238)
				{
					Main.tenthAnniversaryWorld = reader.ReadBoolean();
				}
				if (versionNumber >= 239)
				{
					Main.dontStarveWorld = reader.ReadBoolean();
				}
				if (versionNumber >= 241)
				{
					Main.notTheBeesWorld = reader.ReadBoolean();
				}
				if (versionNumber >= 249)
				{
					Main.remixWorld = reader.ReadBoolean();
				}
				if (versionNumber >= 266)
				{
					Main.noTrapsWorld = reader.ReadBoolean();
				}
				if (versionNumber >= 267)
				{
					Main.zenithWorld = reader.ReadBoolean();
				}
				else
				{
					Main.zenithWorld = (Main.remixWorld && Main.drunkWorld);
				}
			}
			else
			{
				if (versionNumber >= 112)
				{
					Main.GameMode = ((reader.ReadBoolean() > false) ? 1 : 0);
				}
				else
				{
					Main.GameMode = 0;
				}
				if (versionNumber == 208 && reader.ReadBoolean())
				{
					Main.GameMode = 2;
				}
			}
			if (versionNumber >= 141)
			{
				Main.ActiveWorldFileData.CreationTime = DateTime.FromBinary(reader.ReadInt64());
			}
			Main.moonType = (int)reader.ReadByte();
			Main.treeX[0] = reader.ReadInt32();
			Main.treeX[1] = reader.ReadInt32();
			Main.treeX[2] = reader.ReadInt32();
			Main.treeStyle[0] = reader.ReadInt32();
			Main.treeStyle[1] = reader.ReadInt32();
			Main.treeStyle[2] = reader.ReadInt32();
			Main.treeStyle[3] = reader.ReadInt32();
			Main.caveBackX[0] = reader.ReadInt32();
			Main.caveBackX[1] = reader.ReadInt32();
			Main.caveBackX[2] = reader.ReadInt32();
			Main.caveBackStyle[0] = reader.ReadInt32();
			Main.caveBackStyle[1] = reader.ReadInt32();
			Main.caveBackStyle[2] = reader.ReadInt32();
			Main.caveBackStyle[3] = reader.ReadInt32();
			Main.iceBackStyle = reader.ReadInt32();
			Main.jungleBackStyle = reader.ReadInt32();
			Main.hellBackStyle = reader.ReadInt32();
			Main.spawnTileX = reader.ReadInt32();
			Main.spawnTileY = reader.ReadInt32();
			Main.worldSurface = reader.ReadDouble();
			Main.rockLayer = reader.ReadDouble();
			WorldFile._tempTime = reader.ReadDouble();
			WorldFile._tempDayTime = reader.ReadBoolean();
			WorldFile._tempMoonPhase = reader.ReadInt32();
			WorldFile._tempBloodMoon = reader.ReadBoolean();
			WorldFile._tempEclipse = reader.ReadBoolean();
			Main.eclipse = WorldFile._tempEclipse;
			Main.dungeonX = reader.ReadInt32();
			Main.dungeonY = reader.ReadInt32();
			WorldGen.crimson = reader.ReadBoolean();
			NPC.downedBoss1 = reader.ReadBoolean();
			NPC.downedBoss2 = reader.ReadBoolean();
			NPC.downedBoss3 = reader.ReadBoolean();
			NPC.downedQueenBee = reader.ReadBoolean();
			NPC.downedMechBoss1 = reader.ReadBoolean();
			NPC.downedMechBoss2 = reader.ReadBoolean();
			NPC.downedMechBoss3 = reader.ReadBoolean();
			NPC.downedMechBossAny = reader.ReadBoolean();
			NPC.downedPlantBoss = reader.ReadBoolean();
			NPC.downedGolemBoss = reader.ReadBoolean();
			if (versionNumber >= 118)
			{
				NPC.downedSlimeKing = reader.ReadBoolean();
			}
			NPC.savedGoblin = reader.ReadBoolean();
			NPC.savedWizard = reader.ReadBoolean();
			NPC.savedMech = reader.ReadBoolean();
			NPC.downedGoblins = reader.ReadBoolean();
			NPC.downedClown = reader.ReadBoolean();
			NPC.downedFrost = reader.ReadBoolean();
			NPC.downedPirates = reader.ReadBoolean();
			WorldGen.shadowOrbSmashed = reader.ReadBoolean();
			WorldGen.spawnMeteor = reader.ReadBoolean();
			WorldGen.shadowOrbCount = (int)reader.ReadByte();
			WorldGen.altarCount = reader.ReadInt32();
			Main.hardMode = reader.ReadBoolean();
			if (versionNumber >= 257)
			{
				Main.afterPartyOfDoom = reader.ReadBoolean();
			}
			else
			{
				Main.afterPartyOfDoom = false;
			}
			Main.invasionDelay = reader.ReadInt32();
			Main.invasionSize = reader.ReadInt32();
			Main.invasionType = reader.ReadInt32();
			Main.invasionX = reader.ReadDouble();
			if (versionNumber >= 118)
			{
				Main.slimeRainTime = reader.ReadDouble();
			}
			if (versionNumber >= 113)
			{
				Main.sundialCooldown = (int)reader.ReadByte();
			}
			WorldFile._tempRaining = reader.ReadBoolean();
			WorldFile._tempRainTime = reader.ReadInt32();
			WorldFile._tempMaxRain = reader.ReadSingle();
			WorldGen.SavedOreTiers.Cobalt = reader.ReadInt32();
			WorldGen.SavedOreTiers.Mythril = reader.ReadInt32();
			WorldGen.SavedOreTiers.Adamantite = reader.ReadInt32();
			WorldGen.setBG(0, (int)reader.ReadByte());
			WorldGen.setBG(1, (int)reader.ReadByte());
			WorldGen.setBG(2, (int)reader.ReadByte());
			WorldGen.setBG(3, (int)reader.ReadByte());
			WorldGen.setBG(4, (int)reader.ReadByte());
			WorldGen.setBG(5, (int)reader.ReadByte());
			WorldGen.setBG(6, (int)reader.ReadByte());
			WorldGen.setBG(7, (int)reader.ReadByte());
			Main.cloudBGActive = (float)reader.ReadInt32();
			Main.cloudBGAlpha = (((double)Main.cloudBGActive < 1.0) ? 0f : 1f);
			Main.cloudBGActive = (float)(-(float)WorldGen.genRand.Next(8640, 86400));
			Main.numClouds = (int)reader.ReadInt16();
			Main.windSpeedTarget = reader.ReadSingle();
			Main.windSpeedCurrent = Main.windSpeedTarget;
			if (versionNumber < 95)
			{
				return;
			}
			Main.anglerWhoFinishedToday.Clear();
			for (int num = reader.ReadInt32(); num > 0; num--)
			{
				Main.anglerWhoFinishedToday.Add(reader.ReadString());
			}
			if (versionNumber < 99)
			{
				return;
			}
			NPC.savedAngler = reader.ReadBoolean();
			if (versionNumber < 101)
			{
				return;
			}
			Main.anglerQuest = reader.ReadInt32();
			if (versionNumber < 104)
			{
				return;
			}
			NPC.savedStylist = reader.ReadBoolean();
			if (versionNumber >= 129)
			{
				NPC.savedTaxCollector = reader.ReadBoolean();
			}
			if (versionNumber >= 201)
			{
				NPC.savedGolfer = reader.ReadBoolean();
			}
			if (versionNumber < 107)
			{
				if (Main.invasionType > 0 && Main.invasionSize > 0)
				{
					Main.FakeLoadInvasionStart();
				}
			}
			else
			{
				Main.invasionSizeStart = reader.ReadInt32();
			}
			if (versionNumber < 108)
			{
				WorldFile._tempCultistDelay = 86400;
			}
			else
			{
				WorldFile._tempCultistDelay = reader.ReadInt32();
			}
			if (versionNumber < 109)
			{
				return;
			}
			int num2 = (int)reader.ReadInt16();
			for (int i = 0; i < num2; i++)
			{
				if (i < (int)NPCID.Count)
				{
					NPC.killCount[i] = reader.ReadInt32();
				}
				else
				{
					reader.ReadInt32();
				}
			}
			if (versionNumber < 128)
			{
				return;
			}
			Main.fastForwardTimeToDawn = reader.ReadBoolean();
			if (versionNumber < 131)
			{
				return;
			}
			NPC.downedFishron = reader.ReadBoolean();
			NPC.downedMartians = reader.ReadBoolean();
			NPC.downedAncientCultist = reader.ReadBoolean();
			NPC.downedMoonlord = reader.ReadBoolean();
			NPC.downedHalloweenKing = reader.ReadBoolean();
			NPC.downedHalloweenTree = reader.ReadBoolean();
			NPC.downedChristmasIceQueen = reader.ReadBoolean();
			NPC.downedChristmasSantank = reader.ReadBoolean();
			NPC.downedChristmasTree = reader.ReadBoolean();
			if (versionNumber < 140)
			{
				return;
			}
			NPC.downedTowerSolar = reader.ReadBoolean();
			NPC.downedTowerVortex = reader.ReadBoolean();
			NPC.downedTowerNebula = reader.ReadBoolean();
			NPC.downedTowerStardust = reader.ReadBoolean();
			NPC.TowerActiveSolar = reader.ReadBoolean();
			NPC.TowerActiveVortex = reader.ReadBoolean();
			NPC.TowerActiveNebula = reader.ReadBoolean();
			NPC.TowerActiveStardust = reader.ReadBoolean();
			NPC.LunarApocalypseIsUp = reader.ReadBoolean();
			if (NPC.TowerActiveSolar)
			{
				NPC.ShieldStrengthTowerSolar = NPC.ShieldStrengthTowerMax;
			}
			if (NPC.TowerActiveVortex)
			{
				NPC.ShieldStrengthTowerVortex = NPC.ShieldStrengthTowerMax;
			}
			if (NPC.TowerActiveNebula)
			{
				NPC.ShieldStrengthTowerNebula = NPC.ShieldStrengthTowerMax;
			}
			if (NPC.TowerActiveStardust)
			{
				NPC.ShieldStrengthTowerStardust = NPC.ShieldStrengthTowerMax;
			}
			if (versionNumber < 170)
			{
				WorldFile._tempPartyManual = false;
				WorldFile._tempPartyGenuine = false;
				WorldFile._tempPartyCooldown = 0;
				WorldFile.TempPartyCelebratingNPCs.Clear();
			}
			else
			{
				WorldFile._tempPartyManual = reader.ReadBoolean();
				WorldFile._tempPartyGenuine = reader.ReadBoolean();
				WorldFile._tempPartyCooldown = reader.ReadInt32();
				int num3 = reader.ReadInt32();
				WorldFile.TempPartyCelebratingNPCs.Clear();
				for (int j = 0; j < num3; j++)
				{
					WorldFile.TempPartyCelebratingNPCs.Add(reader.ReadInt32());
				}
			}
			if (versionNumber < 174)
			{
				WorldFile._tempSandstormHappening = false;
				WorldFile._tempSandstormTimeLeft = 0;
				WorldFile._tempSandstormSeverity = 0f;
				WorldFile._tempSandstormIntendedSeverity = 0f;
			}
			else
			{
				WorldFile._tempSandstormHappening = reader.ReadBoolean();
				WorldFile._tempSandstormTimeLeft = reader.ReadInt32();
				WorldFile._tempSandstormSeverity = reader.ReadSingle();
				WorldFile._tempSandstormIntendedSeverity = reader.ReadSingle();
			}
			DD2Event.Load(reader, versionNumber);
			if (versionNumber > 194)
			{
				WorldGen.setBG(8, (int)reader.ReadByte());
			}
			else
			{
				WorldGen.setBG(8, 0);
			}
			if (versionNumber >= 215)
			{
				WorldGen.setBG(9, (int)reader.ReadByte());
			}
			else
			{
				WorldGen.setBG(9, 0);
			}
			if (versionNumber > 195)
			{
				WorldGen.setBG(10, (int)reader.ReadByte());
				WorldGen.setBG(11, (int)reader.ReadByte());
				WorldGen.setBG(12, (int)reader.ReadByte());
			}
			else
			{
				WorldGen.setBG(10, WorldGen.treeBG1);
				WorldGen.setBG(11, WorldGen.treeBG1);
				WorldGen.setBG(12, WorldGen.treeBG1);
			}
			if (versionNumber >= 204)
			{
				NPC.combatBookWasUsed = reader.ReadBoolean();
			}
			if (versionNumber < 207)
			{
				WorldFile._tempLanternNightCooldown = 0;
				WorldFile._tempLanternNightGenuine = false;
				WorldFile._tempLanternNightManual = false;
				WorldFile._tempLanternNightNextNightIsGenuine = false;
			}
			else
			{
				WorldFile._tempLanternNightCooldown = reader.ReadInt32();
				WorldFile._tempLanternNightGenuine = reader.ReadBoolean();
				WorldFile._tempLanternNightManual = reader.ReadBoolean();
				WorldFile._tempLanternNightNextNightIsGenuine = reader.ReadBoolean();
			}
			WorldGen.TreeTops.Load(reader, versionNumber);
			if (versionNumber >= 212)
			{
				Main.forceHalloweenForToday = reader.ReadBoolean();
				Main.forceXMasForToday = reader.ReadBoolean();
			}
			else
			{
				Main.forceHalloweenForToday = false;
				Main.forceXMasForToday = false;
			}
			if (versionNumber >= 216)
			{
				WorldGen.SavedOreTiers.Copper = reader.ReadInt32();
				WorldGen.SavedOreTiers.Iron = reader.ReadInt32();
				WorldGen.SavedOreTiers.Silver = reader.ReadInt32();
				WorldGen.SavedOreTiers.Gold = reader.ReadInt32();
			}
			else
			{
				WorldGen.SavedOreTiers.Copper = -1;
				WorldGen.SavedOreTiers.Iron = -1;
				WorldGen.SavedOreTiers.Silver = -1;
				WorldGen.SavedOreTiers.Gold = -1;
			}
			if (versionNumber >= 217)
			{
				NPC.boughtCat = reader.ReadBoolean();
				NPC.boughtDog = reader.ReadBoolean();
				NPC.boughtBunny = reader.ReadBoolean();
			}
			else
			{
				NPC.boughtCat = false;
				NPC.boughtDog = false;
				NPC.boughtBunny = false;
			}
			if (versionNumber >= 223)
			{
				NPC.downedEmpressOfLight = reader.ReadBoolean();
				NPC.downedQueenSlime = reader.ReadBoolean();
			}
			else
			{
				NPC.downedEmpressOfLight = false;
				NPC.downedQueenSlime = false;
			}
			if (versionNumber >= 240)
			{
				NPC.downedDeerclops = reader.ReadBoolean();
			}
			else
			{
				NPC.downedDeerclops = false;
			}
			if (versionNumber >= 250)
			{
				NPC.unlockedSlimeBlueSpawn = reader.ReadBoolean();
			}
			else
			{
				NPC.unlockedSlimeBlueSpawn = false;
			}
			if (versionNumber >= 251)
			{
				NPC.unlockedMerchantSpawn = reader.ReadBoolean();
				NPC.unlockedDemolitionistSpawn = reader.ReadBoolean();
				NPC.unlockedPartyGirlSpawn = reader.ReadBoolean();
				NPC.unlockedDyeTraderSpawn = reader.ReadBoolean();
				NPC.unlockedTruffleSpawn = reader.ReadBoolean();
				NPC.unlockedArmsDealerSpawn = reader.ReadBoolean();
				NPC.unlockedNurseSpawn = reader.ReadBoolean();
				NPC.unlockedPrincessSpawn = reader.ReadBoolean();
			}
			else
			{
				NPC.unlockedMerchantSpawn = false;
				NPC.unlockedDemolitionistSpawn = false;
				NPC.unlockedPartyGirlSpawn = false;
				NPC.unlockedDyeTraderSpawn = false;
				NPC.unlockedTruffleSpawn = false;
				NPC.unlockedArmsDealerSpawn = false;
				NPC.unlockedNurseSpawn = false;
				NPC.unlockedPrincessSpawn = false;
			}
			if (versionNumber >= 259)
			{
				NPC.combatBookVolumeTwoWasUsed = reader.ReadBoolean();
			}
			else
			{
				NPC.combatBookVolumeTwoWasUsed = false;
			}
			if (versionNumber >= 260)
			{
				NPC.peddlersSatchelWasUsed = reader.ReadBoolean();
			}
			else
			{
				NPC.peddlersSatchelWasUsed = false;
			}
			if (versionNumber >= 261)
			{
				NPC.unlockedSlimeGreenSpawn = reader.ReadBoolean();
				NPC.unlockedSlimeOldSpawn = reader.ReadBoolean();
				NPC.unlockedSlimePurpleSpawn = reader.ReadBoolean();
				NPC.unlockedSlimeRainbowSpawn = reader.ReadBoolean();
				NPC.unlockedSlimeRedSpawn = reader.ReadBoolean();
				NPC.unlockedSlimeYellowSpawn = reader.ReadBoolean();
				NPC.unlockedSlimeCopperSpawn = reader.ReadBoolean();
			}
			else
			{
				NPC.unlockedSlimeGreenSpawn = false;
				NPC.unlockedSlimeOldSpawn = false;
				NPC.unlockedSlimePurpleSpawn = false;
				NPC.unlockedSlimeRainbowSpawn = false;
				NPC.unlockedSlimeRedSpawn = false;
				NPC.unlockedSlimeYellowSpawn = false;
				NPC.unlockedSlimeCopperSpawn = false;
			}
			if (versionNumber >= 264)
			{
				Main.fastForwardTimeToDusk = reader.ReadBoolean();
				Main.moondialCooldown = (int)reader.ReadByte();
			}
			else
			{
				Main.fastForwardTimeToDusk = false;
				Main.moondialCooldown = 0;
			}
			Main.UpdateTimeRate();
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x0055B700 File Offset: 0x00559900
		public unsafe static void LoadWorldTiles(BinaryReader reader, bool[] importance)
		{
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				float num = (float)i / (float)Main.maxTilesX;
				Main.statusText = Lang.gen[51].Value + " " + ((int)((double)num * 100.0 + 1.0)).ToString() + "%";
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					int num2 = -1;
					byte b4;
					byte b3;
					byte b2 = b3 = (b4 = 0);
					Tile tile = Main.tile[i, j];
					byte b5 = reader.ReadByte();
					bool flag = false;
					if ((b5 & 1) == 1)
					{
						flag = true;
						b3 = reader.ReadByte();
					}
					bool flag2 = false;
					if (flag && (b3 & 1) == 1)
					{
						flag2 = true;
						b2 = reader.ReadByte();
					}
					if (flag2 && (b2 & 1) == 1)
					{
						b4 = reader.ReadByte();
					}
					byte b6;
					if ((b5 & 2) == 2)
					{
						tile.active(true);
						if ((b5 & 32) == 32)
						{
							b6 = reader.ReadByte();
							num2 = (int)reader.ReadByte();
							num2 = (num2 << 8 | (int)b6);
						}
						else
						{
							num2 = (int)reader.ReadByte();
						}
						*tile.type = (ushort)num2;
						if (importance[num2])
						{
							*tile.frameX = reader.ReadInt16();
							*tile.frameY = reader.ReadInt16();
							if (*tile.type == 144)
							{
								*tile.frameY = 0;
							}
						}
						else
						{
							*tile.frameX = -1;
							*tile.frameY = -1;
						}
						if ((b2 & 8) == 8)
						{
							tile.color(reader.ReadByte());
						}
					}
					if ((b5 & 4) == 4)
					{
						*tile.wall = (ushort)reader.ReadByte();
						if (*tile.wall >= WallID.Count)
						{
							*tile.wall = 0;
						}
						if ((b2 & 16) == 16)
						{
							tile.wallColor(reader.ReadByte());
						}
					}
					b6 = (byte)((b5 & 24) >> 3);
					if (b6 != 0)
					{
						*tile.liquid = reader.ReadByte();
						if ((b2 & 128) == 128)
						{
							tile.shimmer(true);
						}
						else if (b6 > 1)
						{
							if (b6 == 2)
							{
								tile.lava(true);
							}
							else
							{
								tile.honey(true);
							}
						}
					}
					if (b3 > 1)
					{
						if ((b3 & 2) == 2)
						{
							tile.wire(true);
						}
						if ((b3 & 4) == 4)
						{
							tile.wire2(true);
						}
						if ((b3 & 8) == 8)
						{
							tile.wire3(true);
						}
						b6 = (byte)((b3 & 112) >> 4);
						if (b6 != 0 && (Main.tileSolid[(int)(*tile.type)] || TileID.Sets.NonSolidSaveSlopes[(int)(*tile.type)]))
						{
							if (b6 == 1)
							{
								tile.halfBrick(true);
							}
							else
							{
								tile.slope(b6 - 1);
							}
						}
					}
					if (b2 > 1)
					{
						if ((b2 & 2) == 2)
						{
							tile.actuator(true);
						}
						if ((b2 & 4) == 4)
						{
							tile.inActive(true);
						}
						if ((b2 & 32) == 32)
						{
							tile.wire4(true);
						}
						if ((b2 & 64) == 64)
						{
							b6 = reader.ReadByte();
							*tile.wall = (ushort)((int)b6 << 8 | (int)(*tile.wall));
							if (*tile.wall >= WallID.Count)
							{
								*tile.wall = 0;
							}
						}
					}
					if (b4 > 1)
					{
						if ((b4 & 2) == 2)
						{
							tile.invisibleBlock(true);
						}
						if ((b4 & 4) == 4)
						{
							tile.invisibleWall(true);
						}
						if ((b4 & 8) == 8)
						{
							tile.fullbrightBlock(true);
						}
						if ((b4 & 16) == 16)
						{
							tile.fullbrightWall(true);
						}
					}
					byte b7 = (byte)((b5 & 192) >> 6);
					int num3;
					if (b7 != 0)
					{
						if (b7 != 1)
						{
							num3 = (int)reader.ReadInt16();
						}
						else
						{
							num3 = (int)reader.ReadByte();
						}
					}
					else
					{
						num3 = 0;
					}
					if (num2 != -1)
					{
						if ((double)j <= Main.worldSurface)
						{
							if ((double)(j + num3) <= Main.worldSurface)
							{
								WorldGen.tileCounts[num2] += (num3 + 1) * 5;
							}
							else
							{
								int num4 = (int)(Main.worldSurface - (double)j + 1.0);
								int num5 = num3 + 1 - num4;
								WorldGen.tileCounts[num2] += num4 * 5 + num5;
							}
						}
						else
						{
							WorldGen.tileCounts[num2] += num3 + 1;
						}
					}
					while (num3 > 0)
					{
						j++;
						num3--;
						Tile tile2 = Main.tile[i, j];
						*tile2.Get<LiquidData>() = *tile.Get<LiquidData>();
						*tile2.Get<WallTypeData>() = *tile.Get<WallTypeData>();
						*tile2.Get<TileTypeData>() = *tile.Get<TileTypeData>();
						*tile2.Get<TileWallWireStateData>() = *tile.Get<TileWallWireStateData>();
						*tile2.Get<TileWallBrightnessInvisibilityData>() = *tile.Get<TileWallBrightnessInvisibilityData>();
					}
				}
			}
			WorldGen.AddUpAlignmentCounts(true);
			if (WorldFile._versionNumber < 105)
			{
				WorldGen.FixHearts();
			}
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x0055BBBC File Offset: 0x00559DBC
		public static void LoadChests(BinaryReader reader)
		{
			int num = (int)reader.ReadInt16();
			int num2 = (int)reader.ReadInt16();
			int num3;
			int num4;
			if (num2 < 40)
			{
				num3 = num2;
				num4 = 0;
			}
			else
			{
				num3 = 40;
				num4 = num2 - 40;
			}
			int i;
			for (i = 0; i < num; i++)
			{
				Chest chest = new Chest(false);
				chest.x = reader.ReadInt32();
				chest.y = reader.ReadInt32();
				chest.name = reader.ReadString();
				for (int j = 0; j < num3; j++)
				{
					short num5 = reader.ReadInt16();
					Item item = new Item();
					if (num5 > 0)
					{
						item.netDefaults(reader.ReadInt32());
						item.stack = (int)num5;
						item.Prefix((int)reader.ReadByte());
					}
					else if (num5 < 0)
					{
						item.netDefaults(reader.ReadInt32());
						item.Prefix((int)reader.ReadByte());
						item.stack = 1;
					}
					chest.item[j] = item;
				}
				for (int k = 0; k < num4; k++)
				{
					if (reader.ReadInt16() > 0)
					{
						reader.ReadInt32();
						reader.ReadByte();
					}
				}
				Main.chest[i] = chest;
			}
			List<Point16> list = new List<Point16>();
			for (int l = 0; l < i; l++)
			{
				if (Main.chest[l] != null)
				{
					Point16 item2 = new Point16(Main.chest[l].x, Main.chest[l].y);
					if (list.Contains(item2))
					{
						Main.chest[l] = null;
					}
					else
					{
						list.Add(item2);
					}
				}
			}
			while (i < 8000)
			{
				Main.chest[i] = null;
				i++;
			}
			if (WorldFile._versionNumber < 115)
			{
				WorldFile.FixDresserChests();
			}
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x0055BD68 File Offset: 0x00559F68
		private static void ConvertIlluminantPaintToNewField()
		{
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && tile.color() == 31)
					{
						tile.color(0);
						tile.fullbrightBlock(true);
					}
					if (tile.wallColor() == 31)
					{
						tile.wallColor(0);
						tile.fullbrightWall(true);
					}
				}
			}
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x0055BDE4 File Offset: 0x00559FE4
		public static void LoadSigns(BinaryReader reader)
		{
			short num = reader.ReadInt16();
			int i;
			for (i = 0; i < (int)num; i++)
			{
				string text = reader.ReadString();
				int num2 = reader.ReadInt32();
				int num3 = reader.ReadInt32();
				Tile tile = Main.tile[num2, num3];
				Sign sign = new Sign();
				sign.text = text;
				sign.x = num2;
				sign.y = num3;
				Main.sign[i] = sign;
			}
			List<Point16> list = new List<Point16>();
			for (int j = 0; j < 1000; j++)
			{
				if (Main.sign[j] != null)
				{
					Point16 item = new Point16(Main.sign[j].x, Main.sign[j].y);
					if (list.Contains(item))
					{
						Main.sign[j] = null;
					}
					else
					{
						list.Add(item);
					}
				}
			}
			while (i < 1000)
			{
				Main.sign[i] = null;
				i++;
			}
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x0055BECC File Offset: 0x0055A0CC
		public static void LoadDummies(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				reader.ReadInt16();
				reader.ReadInt16();
			}
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x0055BEFC File Offset: 0x0055A0FC
		public static void LoadNPCs(BinaryReader reader)
		{
			if (WorldFile._versionNumber >= 268)
			{
				int num = reader.ReadInt32();
				while (num-- > 0)
				{
					NPC.ShimmeredTownNPCs[reader.ReadInt32()] = true;
				}
			}
			int num2 = 0;
			bool flag = reader.ReadBoolean();
			while (flag)
			{
				NPC nPC = Main.npc[num2];
				if (WorldFile._versionNumber >= 190)
				{
					nPC.SetDefaults(reader.ReadInt32(), default(NPCSpawnParams));
				}
				else
				{
					nPC.SetDefaults(NPCID.FromLegacyName(reader.ReadString()), default(NPCSpawnParams));
				}
				nPC.GivenName = reader.ReadString();
				nPC.position.X = reader.ReadSingle();
				nPC.position.Y = reader.ReadSingle();
				nPC.homeless = reader.ReadBoolean();
				nPC.homeTileX = reader.ReadInt32();
				nPC.homeTileY = reader.ReadInt32();
				if (WorldFile._versionNumber >= 213 && reader.ReadByte()[0])
				{
					nPC.townNpcVariationIndex = reader.ReadInt32();
				}
				num2++;
				flag = reader.ReadBoolean();
			}
			if (WorldFile._versionNumber < 140)
			{
				return;
			}
			flag = reader.ReadBoolean();
			while (flag)
			{
				NPC nPC2 = Main.npc[num2];
				if (WorldFile._versionNumber >= 190)
				{
					nPC2.SetDefaults(reader.ReadInt32(), default(NPCSpawnParams));
				}
				else
				{
					nPC2.SetDefaults(NPCID.FromLegacyName(reader.ReadString()), default(NPCSpawnParams));
				}
				nPC2.position = reader.ReadVector2();
				num2++;
				flag = reader.ReadBoolean();
			}
			if (WorldFile._versionNumber < 251)
			{
				NPC.unlockedMerchantSpawn = NPC.AnyNPCs(17);
				NPC.unlockedDemolitionistSpawn = NPC.AnyNPCs(38);
				NPC.unlockedPartyGirlSpawn = NPC.AnyNPCs(208);
				NPC.unlockedDyeTraderSpawn = NPC.AnyNPCs(207);
				NPC.unlockedTruffleSpawn = NPC.AnyNPCs(160);
				NPC.unlockedArmsDealerSpawn = NPC.AnyNPCs(19);
				NPC.unlockedNurseSpawn = NPC.AnyNPCs(18);
				NPC.unlockedPrincessSpawn = NPC.AnyNPCs(663);
			}
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x0055C110 File Offset: 0x0055A310
		public static void ValidateLoadNPCs(BinaryReader fileIO)
		{
			int num = fileIO.ReadInt32();
			while (num-- > 0)
			{
				fileIO.ReadInt32();
			}
			bool flag = fileIO.ReadBoolean();
			while (flag)
			{
				fileIO.ReadInt32();
				fileIO.ReadString();
				fileIO.ReadSingle();
				fileIO.ReadSingle();
				fileIO.ReadBoolean();
				fileIO.ReadInt32();
				fileIO.ReadInt32();
				if (fileIO.ReadByte()[0])
				{
					fileIO.ReadInt32();
				}
				flag = fileIO.ReadBoolean();
			}
			flag = fileIO.ReadBoolean();
			while (flag)
			{
				fileIO.ReadInt32();
				fileIO.ReadSingle();
				fileIO.ReadSingle();
				flag = fileIO.ReadBoolean();
			}
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x0055C1BE File Offset: 0x0055A3BE
		public static int LoadFooter(BinaryReader reader)
		{
			if (!reader.ReadBoolean())
			{
				return 6;
			}
			if (reader.ReadString() != Main.worldName)
			{
				return 6;
			}
			if (reader.ReadInt32() != Main.worldID)
			{
				return 6;
			}
			return 0;
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x0055C1F0 File Offset: 0x0055A3F0
		public static bool ValidateWorld(BinaryReader fileIO)
		{
			new Stopwatch().Start();
			bool result2;
			try
			{
				Stream baseStream = fileIO.BaseStream;
				int num = fileIO.ReadInt32();
				if (num == 0 || num > 279)
				{
					result2 = false;
				}
				else
				{
					baseStream.Position = 0L;
					bool[] importance;
					int[] positions;
					if (!WorldFile.LoadFileFormatHeader(fileIO, out importance, out positions))
					{
						result2 = false;
					}
					else
					{
						string text = fileIO.ReadString();
						if (num >= 179)
						{
							if (num == 179)
							{
								fileIO.ReadInt32();
							}
							else
							{
								fileIO.ReadString();
							}
							fileIO.ReadUInt64();
						}
						if (num >= 181)
						{
							fileIO.ReadBytes(16);
						}
						int num2 = fileIO.ReadInt32();
						fileIO.ReadInt32();
						fileIO.ReadInt32();
						fileIO.ReadInt32();
						fileIO.ReadInt32();
						int num3 = fileIO.ReadInt32();
						int num4 = fileIO.ReadInt32();
						baseStream.Position = (long)positions[1];
						for (int i = 0; i < num4; i++)
						{
							float num5 = (float)i / (float)Main.maxTilesX;
							Main.statusText = Lang.gen[73].Value + " " + ((int)(num5 * 100f + 1f)).ToString() + "%";
							for (int num6 = 0; num6 < num3; num6++)
							{
								byte b2;
								byte b = b2 = 0;
								byte b3 = fileIO.ReadByte();
								bool flag = false;
								if ((b3 & 1) == 1)
								{
									flag = true;
									b2 = fileIO.ReadByte();
								}
								bool flag2 = false;
								if (flag && (b2 & 1) == 1)
								{
									flag2 = true;
									b = fileIO.ReadByte();
								}
								if (flag2 && (b & 1) == 1)
								{
									fileIO.ReadByte();
								}
								if ((b3 & 2) == 2)
								{
									int num7;
									if ((b3 & 32) == 32)
									{
										byte b4 = fileIO.ReadByte();
										num7 = (int)fileIO.ReadByte();
										num7 = (num7 << 8 | (int)b4);
									}
									else
									{
										num7 = (int)fileIO.ReadByte();
									}
									if (importance[num7])
									{
										fileIO.ReadInt16();
										fileIO.ReadInt16();
									}
									if ((b & 8) == 8)
									{
										fileIO.ReadByte();
									}
								}
								if ((b3 & 4) == 4)
								{
									fileIO.ReadByte();
									if ((b & 16) == 16)
									{
										fileIO.ReadByte();
									}
								}
								if ((b3 & 24) >> 3 != 0)
								{
									fileIO.ReadByte();
								}
								if ((b & 64) == 64)
								{
									fileIO.ReadByte();
								}
								byte b5 = (byte)((b3 & 192) >> 6);
								int num8;
								if (b5 != 0)
								{
									if (b5 != 1)
									{
										num8 = (int)fileIO.ReadInt16();
									}
									else
									{
										num8 = (int)fileIO.ReadByte();
									}
								}
								else
								{
									num8 = 0;
								}
								num6 += num8;
							}
						}
						if (baseStream.Position != (long)positions[2])
						{
							result2 = false;
						}
						else
						{
							int num9 = (int)fileIO.ReadInt16();
							int num10 = (int)fileIO.ReadInt16();
							for (int j = 0; j < num9; j++)
							{
								fileIO.ReadInt32();
								fileIO.ReadInt32();
								fileIO.ReadString();
								for (int k = 0; k < num10; k++)
								{
									if (fileIO.ReadInt16() > 0)
									{
										fileIO.ReadInt32();
										fileIO.ReadByte();
									}
								}
							}
							if (baseStream.Position != (long)positions[3])
							{
								result2 = false;
							}
							else
							{
								int num11 = (int)fileIO.ReadInt16();
								for (int l = 0; l < num11; l++)
								{
									fileIO.ReadString();
									fileIO.ReadInt32();
									fileIO.ReadInt32();
								}
								if (baseStream.Position != (long)positions[4])
								{
									result2 = false;
								}
								else
								{
									WorldFile.ValidateLoadNPCs(fileIO);
									if (baseStream.Position != (long)positions[5])
									{
										result2 = false;
									}
									else
									{
										if (WorldFile._versionNumber >= 116 && WorldFile._versionNumber <= 121)
										{
											int num12 = fileIO.ReadInt32();
											for (int m = 0; m < num12; m++)
											{
												fileIO.ReadInt16();
												fileIO.ReadInt16();
											}
											if (baseStream.Position != (long)positions[6])
											{
												return false;
											}
										}
										if (WorldFile._versionNumber >= 122)
										{
											int num13 = fileIO.ReadInt32();
											for (int n = 0; n < num13; n++)
											{
												TileEntity.Read(fileIO, false, false);
											}
										}
										if (WorldFile._versionNumber >= 170)
										{
											int num14 = fileIO.ReadInt32();
											for (int num15 = 0; num15 < num14; num15++)
											{
												fileIO.ReadInt64();
											}
										}
										if (WorldFile._versionNumber >= 189)
										{
											int num16 = fileIO.ReadInt32();
											fileIO.ReadBytes(12 * num16);
										}
										if (WorldFile._versionNumber >= 210)
										{
											Main.BestiaryTracker.ValidateWorld(fileIO, WorldFile._versionNumber);
										}
										if (WorldFile._versionNumber >= 220)
										{
											CreativePowerManager.Instance.ValidateWorld(fileIO, WorldFile._versionNumber);
										}
										bool flag3 = fileIO.ReadBoolean();
										string text2 = fileIO.ReadString();
										int num17 = fileIO.ReadInt32();
										bool result = false;
										if (flag3 && (text2 == text || num17 == num2))
										{
											result = true;
										}
										result2 = result;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception value)
			{
				Logging.Terraria.Error("World Validation", value);
				using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
				{
					streamWriter.WriteLine(DateTime.Now);
					streamWriter.WriteLine(value);
					streamWriter.WriteLine("");
				}
				result2 = false;
			}
			return result2;
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x0055C6F0 File Offset: 0x0055A8F0
		public static FileMetadata GetFileMetadata(string file, bool cloudSave)
		{
			if (file == null)
			{
				return null;
			}
			try
			{
				byte[] buffer = null;
				int num;
				if (cloudSave)
				{
					num = ((SocialAPI.Cloud != null) ? 1 : 0);
					if (num != 0)
					{
						int num2 = 24;
						buffer = new byte[num2];
						SocialAPI.Cloud.Read(file, buffer, num2);
					}
				}
				else
				{
					num = 0;
				}
				using (Stream input = (num != 0) ? new MemoryStream(buffer) : new FileStream(file, FileMode.Open))
				{
					using (BinaryReader binaryReader = new BinaryReader(input))
					{
						if (binaryReader.ReadInt32() >= 135)
						{
							return FileMetadata.Read(binaryReader, FileType.World);
						}
						return FileMetadata.FromCurrentSettings(FileType.World);
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x0055C7B4 File Offset: 0x0055A9B4
		private unsafe static void FixDresserChests()
		{
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					Tile tile = Main.tile[i, j];
					if (tile.active() && *tile.type == 88 && *tile.frameX % 54 == 0 && *tile.frameY % 36 == 0)
					{
						Chest.CreateChest(i, j, -1);
					}
				}
			}
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x0055C828 File Offset: 0x0055AA28
		public static int SaveTileEntities(BinaryWriter writer)
		{
			object entityCreationLock = TileEntity.EntityCreationLock;
			lock (entityCreationLock)
			{
				writer.Write(TileEntity.ByID.Count - ModTileEntity.CountInWorld());
				foreach (KeyValuePair<int, TileEntity> item in from pair in TileEntity.ByID
				where (int)pair.Value.type < ModTileEntity.NumVanilla
				select pair)
				{
					TileEntity.Write(writer, item.Value, false, false);
				}
			}
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x0055C8E8 File Offset: 0x0055AAE8
		public static void LoadTileEntities(BinaryReader reader)
		{
			TileEntity.ByID.Clear();
			TileEntity.ByPosition.Clear();
			int num = reader.ReadInt32();
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				TileEntity tileEntity = TileEntity.Read(reader, false, false);
				tileEntity.ID = num2++;
				TileEntity.ByID[tileEntity.ID] = tileEntity;
				TileEntity value;
				if (TileEntity.ByPosition.TryGetValue(tileEntity.Position, out value))
				{
					TileEntity.ByID.Remove(value.ID);
				}
				TileEntity.ByPosition[tileEntity.Position] = tileEntity;
			}
			TileEntity.TileEntitiesNextID = num;
			List<Point16> list = new List<Point16>();
			foreach (KeyValuePair<Point16, TileEntity> item in TileEntity.ByPosition)
			{
				if (!WorldGen.InWorld((int)item.Value.Position.X, (int)item.Value.Position.Y, 1))
				{
					list.Add(item.Value.Position);
				}
				else if (!TileEntity.manager.CheckValidTile((int)item.Value.type, (int)item.Value.Position.X, (int)item.Value.Position.Y))
				{
					list.Add(item.Value.Position);
				}
			}
			try
			{
				foreach (Point16 item2 in list)
				{
					TileEntity tileEntity2 = TileEntity.ByPosition[item2];
					if (TileEntity.ByID.ContainsKey(tileEntity2.ID))
					{
						TileEntity.ByID.Remove(tileEntity2.ID);
					}
					if (TileEntity.ByPosition.ContainsKey(item2))
					{
						TileEntity.ByPosition.Remove(item2);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x0055CAF8 File Offset: 0x0055ACF8
		public static int SaveWeightedPressurePlates(BinaryWriter writer)
		{
			object entityCreationLock = PressurePlateHelper.EntityCreationLock;
			lock (entityCreationLock)
			{
				writer.Write(PressurePlateHelper.PressurePlatesPressed.Count);
				foreach (KeyValuePair<Point, bool[]> item in PressurePlateHelper.PressurePlatesPressed)
				{
					writer.Write(item.Key.X);
					writer.Write(item.Key.Y);
				}
			}
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x0055CBAC File Offset: 0x0055ADAC
		public static void LoadWeightedPressurePlates(BinaryReader reader)
		{
			PressurePlateHelper.Reset();
			PressurePlateHelper.NeedsFirstUpdate = true;
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				Point key;
				key..ctor(reader.ReadInt32(), reader.ReadInt32());
				PressurePlateHelper.PressurePlatesPressed.Add(key, new bool[255]);
			}
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x0055CBFF File Offset: 0x0055ADFF
		public static int SaveTownManager(BinaryWriter writer)
		{
			WorldGen.TownManager.Save(writer);
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x0055CC18 File Offset: 0x0055AE18
		public static void LoadTownManager(BinaryReader reader)
		{
			WorldGen.TownManager.Load(reader);
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x0055CC25 File Offset: 0x0055AE25
		public static int SaveBestiary(BinaryWriter writer)
		{
			Main.BestiaryTracker.Save(writer);
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x0055CC3E File Offset: 0x0055AE3E
		public static void LoadBestiary(BinaryReader reader, int loadVersionNumber)
		{
			Main.BestiaryTracker.Load(reader, loadVersionNumber);
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x0055CC4C File Offset: 0x0055AE4C
		private static void LoadBestiaryForVersionsBefore210()
		{
			Main.BestiaryTracker.FillBasedOnVersionBefore210();
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x0055CC58 File Offset: 0x0055AE58
		public static int SaveCreativePowers(BinaryWriter writer)
		{
			CreativePowerManager.Instance.SaveToWorld(writer);
			return (int)writer.BaseStream.Position;
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x0055CC71 File Offset: 0x0055AE71
		public static void LoadCreativePowers(BinaryReader reader, int loadVersionNumber)
		{
			CreativePowerManager.Instance.LoadFromWorld(reader, loadVersionNumber);
		}

		// Token: 0x0600348A RID: 13450 RVA: 0x0055CC80 File Offset: 0x0055AE80
		private unsafe static int LoadWorld_Version1_Old_BeforeRelease88(BinaryReader fileIO)
		{
			Main.WorldFileMetadata = FileMetadata.FromCurrentSettings(FileType.World);
			int versionNumber = WorldFile._versionNumber;
			if (versionNumber > 279)
			{
				return 1;
			}
			Main.worldName = fileIO.ReadString();
			Main.worldID = fileIO.ReadInt32();
			Main.leftWorld = (float)fileIO.ReadInt32();
			Main.rightWorld = (float)fileIO.ReadInt32();
			Main.topWorld = (float)fileIO.ReadInt32();
			Main.bottomWorld = (float)fileIO.ReadInt32();
			Main.maxTilesY = fileIO.ReadInt32();
			Main.maxTilesX = fileIO.ReadInt32();
			if (versionNumber >= 112)
			{
				Main.GameMode = ((fileIO.ReadBoolean() > false) ? 1 : 0);
			}
			else
			{
				Main.GameMode = 0;
			}
			if (versionNumber >= 63)
			{
				Main.moonType = (int)fileIO.ReadByte();
			}
			else
			{
				WorldGen.RandomizeMoonState(WorldGen.genRand, false);
			}
			WorldGen.clearWorld();
			if (versionNumber >= 44)
			{
				Main.treeX[0] = fileIO.ReadInt32();
				Main.treeX[1] = fileIO.ReadInt32();
				Main.treeX[2] = fileIO.ReadInt32();
				Main.treeStyle[0] = fileIO.ReadInt32();
				Main.treeStyle[1] = fileIO.ReadInt32();
				Main.treeStyle[2] = fileIO.ReadInt32();
				Main.treeStyle[3] = fileIO.ReadInt32();
			}
			if (versionNumber >= 60)
			{
				Main.caveBackX[0] = fileIO.ReadInt32();
				Main.caveBackX[1] = fileIO.ReadInt32();
				Main.caveBackX[2] = fileIO.ReadInt32();
				Main.caveBackStyle[0] = fileIO.ReadInt32();
				Main.caveBackStyle[1] = fileIO.ReadInt32();
				Main.caveBackStyle[2] = fileIO.ReadInt32();
				Main.caveBackStyle[3] = fileIO.ReadInt32();
				Main.iceBackStyle = fileIO.ReadInt32();
				if (versionNumber >= 61)
				{
					Main.jungleBackStyle = fileIO.ReadInt32();
					Main.hellBackStyle = fileIO.ReadInt32();
				}
			}
			else
			{
				WorldGen.RandomizeCaveBackgrounds();
			}
			Main.spawnTileX = fileIO.ReadInt32();
			Main.spawnTileY = fileIO.ReadInt32();
			Main.worldSurface = fileIO.ReadDouble();
			Main.rockLayer = fileIO.ReadDouble();
			WorldFile._tempTime = fileIO.ReadDouble();
			WorldFile._tempDayTime = fileIO.ReadBoolean();
			WorldFile._tempMoonPhase = fileIO.ReadInt32();
			WorldFile._tempBloodMoon = fileIO.ReadBoolean();
			if (versionNumber >= 70)
			{
				WorldFile._tempEclipse = fileIO.ReadBoolean();
				Main.eclipse = WorldFile._tempEclipse;
			}
			Main.dungeonX = fileIO.ReadInt32();
			Main.dungeonY = fileIO.ReadInt32();
			if (versionNumber >= 56)
			{
				WorldGen.crimson = fileIO.ReadBoolean();
			}
			else
			{
				WorldGen.crimson = false;
			}
			NPC.downedBoss1 = fileIO.ReadBoolean();
			NPC.downedBoss2 = fileIO.ReadBoolean();
			NPC.downedBoss3 = fileIO.ReadBoolean();
			if (versionNumber >= 66)
			{
				NPC.downedQueenBee = fileIO.ReadBoolean();
			}
			if (versionNumber >= 44)
			{
				NPC.downedMechBoss1 = fileIO.ReadBoolean();
				NPC.downedMechBoss2 = fileIO.ReadBoolean();
				NPC.downedMechBoss3 = fileIO.ReadBoolean();
				NPC.downedMechBossAny = fileIO.ReadBoolean();
			}
			if (versionNumber >= 64)
			{
				NPC.downedPlantBoss = fileIO.ReadBoolean();
				NPC.downedGolemBoss = fileIO.ReadBoolean();
			}
			if (versionNumber >= 29)
			{
				NPC.savedGoblin = fileIO.ReadBoolean();
				NPC.savedWizard = fileIO.ReadBoolean();
				if (versionNumber >= 34)
				{
					NPC.savedMech = fileIO.ReadBoolean();
					if (versionNumber >= 80)
					{
						NPC.savedStylist = fileIO.ReadBoolean();
					}
				}
				if (versionNumber >= 129)
				{
					NPC.savedTaxCollector = fileIO.ReadBoolean();
				}
				if (versionNumber >= 201)
				{
					NPC.savedGolfer = fileIO.ReadBoolean();
				}
				NPC.downedGoblins = fileIO.ReadBoolean();
			}
			if (versionNumber >= 32)
			{
				NPC.downedClown = fileIO.ReadBoolean();
			}
			if (versionNumber >= 37)
			{
				NPC.downedFrost = fileIO.ReadBoolean();
			}
			if (versionNumber >= 56)
			{
				NPC.downedPirates = fileIO.ReadBoolean();
			}
			WorldGen.shadowOrbSmashed = fileIO.ReadBoolean();
			WorldGen.spawnMeteor = fileIO.ReadBoolean();
			WorldGen.shadowOrbCount = (int)fileIO.ReadByte();
			if (versionNumber >= 23)
			{
				WorldGen.altarCount = fileIO.ReadInt32();
				Main.hardMode = fileIO.ReadBoolean();
			}
			Main.invasionDelay = fileIO.ReadInt32();
			Main.invasionSize = fileIO.ReadInt32();
			Main.invasionType = fileIO.ReadInt32();
			Main.invasionX = fileIO.ReadDouble();
			if (versionNumber >= 113)
			{
				Main.sundialCooldown = (int)fileIO.ReadByte();
			}
			if (versionNumber >= 53)
			{
				WorldFile._tempRaining = fileIO.ReadBoolean();
				WorldFile._tempRainTime = fileIO.ReadInt32();
				WorldFile._tempMaxRain = fileIO.ReadSingle();
			}
			if (versionNumber >= 54)
			{
				WorldGen.SavedOreTiers.Cobalt = fileIO.ReadInt32();
				WorldGen.SavedOreTiers.Mythril = fileIO.ReadInt32();
				WorldGen.SavedOreTiers.Adamantite = fileIO.ReadInt32();
			}
			else if (versionNumber >= 23 && WorldGen.altarCount == 0)
			{
				WorldGen.SavedOreTiers.Cobalt = -1;
				WorldGen.SavedOreTiers.Mythril = -1;
				WorldGen.SavedOreTiers.Adamantite = -1;
			}
			else
			{
				WorldGen.SavedOreTiers.Cobalt = 107;
				WorldGen.SavedOreTiers.Mythril = 108;
				WorldGen.SavedOreTiers.Adamantite = 111;
			}
			int style = 0;
			int style2 = 0;
			int style3 = 0;
			int style4 = 0;
			int style5 = 0;
			int style6 = 0;
			int style7 = 0;
			int style8 = 0;
			int style9 = 0;
			int style10 = 0;
			if (versionNumber >= 55)
			{
				style = (int)fileIO.ReadByte();
				style2 = (int)fileIO.ReadByte();
				style3 = (int)fileIO.ReadByte();
			}
			if (versionNumber >= 60)
			{
				style4 = (int)fileIO.ReadByte();
				style5 = (int)fileIO.ReadByte();
				style6 = (int)fileIO.ReadByte();
				style7 = (int)fileIO.ReadByte();
				style8 = (int)fileIO.ReadByte();
			}
			WorldGen.setBG(0, style);
			WorldGen.setBG(1, style2);
			WorldGen.setBG(2, style3);
			WorldGen.setBG(3, style4);
			WorldGen.setBG(4, style5);
			WorldGen.setBG(5, style6);
			WorldGen.setBG(6, style7);
			WorldGen.setBG(7, style8);
			WorldGen.setBG(8, style9);
			WorldGen.setBG(9, style10);
			WorldGen.setBG(10, style);
			WorldGen.setBG(11, style);
			WorldGen.setBG(12, style);
			if (versionNumber >= 60)
			{
				Main.cloudBGActive = (float)fileIO.ReadInt32();
				if (Main.cloudBGActive >= 1f)
				{
					Main.cloudBGAlpha = 1f;
				}
				else
				{
					Main.cloudBGAlpha = 0f;
				}
			}
			else
			{
				Main.cloudBGActive = (float)(-(float)WorldGen.genRand.Next(8640, 86400));
			}
			if (versionNumber >= 62)
			{
				Main.numClouds = (int)fileIO.ReadInt16();
				Main.windSpeedTarget = fileIO.ReadSingle();
				Main.windSpeedCurrent = Main.windSpeedTarget;
			}
			else
			{
				WorldGen.RandomizeWeather();
			}
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				float num = (float)i / (float)Main.maxTilesX;
				Main.statusText = Lang.gen[51].Value + " " + ((int)(num * 100f + 1f)).ToString() + "%";
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					Tile tile = Main.tile[i, j];
					int num2 = -1;
					tile.active(fileIO.ReadBoolean());
					if (tile.active())
					{
						num2 = (int)((versionNumber <= 77) ? ((ushort)fileIO.ReadByte()) : fileIO.ReadUInt16());
						*tile.type = (ushort)num2;
						if (*tile.type == 127 || *tile.type == 504)
						{
							tile.active(false);
						}
						if (versionNumber < 72 && (*tile.type == 35 || *tile.type == 36 || *tile.type == 170 || *tile.type == 171 || *tile.type == 172))
						{
							*tile.frameX = fileIO.ReadInt16();
							*tile.frameY = fileIO.ReadInt16();
						}
						else if (Main.tileFrameImportant[num2])
						{
							if (versionNumber < 28 && num2 == 4)
							{
								*tile.frameX = 0;
								*tile.frameY = 0;
							}
							else if (versionNumber < 40 && *tile.type == 19)
							{
								*tile.frameX = 0;
								*tile.frameY = 0;
							}
							else if (versionNumber < 195 && *tile.type == 49)
							{
								*tile.frameX = 0;
								*tile.frameY = 0;
							}
							else
							{
								*tile.frameX = fileIO.ReadInt16();
								*tile.frameY = fileIO.ReadInt16();
								if (*tile.type == 144)
								{
									*tile.frameY = 0;
								}
							}
						}
						else
						{
							*tile.frameX = -1;
							*tile.frameY = -1;
						}
						if (versionNumber >= 48 && fileIO.ReadBoolean())
						{
							tile.color(fileIO.ReadByte());
						}
					}
					if (versionNumber <= 25)
					{
						fileIO.ReadBoolean();
					}
					if (fileIO.ReadBoolean())
					{
						*tile.wall = (ushort)fileIO.ReadByte();
						if (*tile.wall >= WallID.Count)
						{
							*tile.wall = 0;
						}
						if (versionNumber >= 48 && fileIO.ReadBoolean())
						{
							tile.wallColor(fileIO.ReadByte());
						}
					}
					if (fileIO.ReadBoolean())
					{
						*tile.liquid = fileIO.ReadByte();
						tile.lava(fileIO.ReadBoolean());
						if (versionNumber >= 51)
						{
							tile.honey(fileIO.ReadBoolean());
						}
					}
					if (versionNumber >= 33)
					{
						tile.wire(fileIO.ReadBoolean());
					}
					if (versionNumber >= 43)
					{
						tile.wire2(fileIO.ReadBoolean());
						tile.wire3(fileIO.ReadBoolean());
					}
					if (versionNumber >= 41)
					{
						tile.halfBrick(fileIO.ReadBoolean());
						if (!Main.tileSolid[(int)(*tile.type)] && !TileID.Sets.NonSolidSaveSlopes[(int)(*tile.type)])
						{
							tile.halfBrick(false);
						}
						if (versionNumber >= 49)
						{
							tile.slope(fileIO.ReadByte());
							if (!Main.tileSolid[(int)(*tile.type)] && !TileID.Sets.NonSolidSaveSlopes[(int)(*tile.type)])
							{
								tile.slope(0);
							}
						}
					}
					if (versionNumber >= 42)
					{
						tile.actuator(fileIO.ReadBoolean());
						tile.inActive(fileIO.ReadBoolean());
					}
					int num3 = 0;
					if (versionNumber >= 25)
					{
						num3 = (int)fileIO.ReadInt16();
					}
					if (num2 != -1)
					{
						if ((double)j <= Main.worldSurface)
						{
							if ((double)(j + num3) <= Main.worldSurface)
							{
								WorldGen.tileCounts[num2] += (num3 + 1) * 5;
							}
							else
							{
								int num4 = (int)(Main.worldSurface - (double)j + 1.0);
								int num5 = num3 + 1 - num4;
								WorldGen.tileCounts[num2] += num4 * 5 + num5;
							}
						}
						else
						{
							WorldGen.tileCounts[num2] += num3 + 1;
						}
					}
					if (num3 > 0)
					{
						for (int k = j + 1; k < j + num3 + 1; k++)
						{
							Main.tile[i, k].CopyFrom(Main.tile[i, j]);
						}
						j += num3;
					}
				}
			}
			WorldGen.AddUpAlignmentCounts(true);
			if (versionNumber < 67)
			{
				WorldGen.FixSunflowers();
			}
			if (versionNumber < 72)
			{
				WorldGen.FixChands();
			}
			int num6 = 40;
			if (versionNumber < 58)
			{
				num6 = 20;
			}
			int num7 = 1000;
			for (int l = 0; l < num7; l++)
			{
				if (fileIO.ReadBoolean())
				{
					Main.chest[l] = new Chest(false);
					Main.chest[l].x = fileIO.ReadInt32();
					Main.chest[l].y = fileIO.ReadInt32();
					if (versionNumber >= 85)
					{
						string text = fileIO.ReadString();
						if (text.Length > 20)
						{
							text = text.Substring(0, 20);
						}
						Main.chest[l].name = text;
					}
					for (int m = 0; m < 40; m++)
					{
						Main.chest[l].item[m] = new Item();
						if (m < num6)
						{
							int num8 = (int)((versionNumber < 59) ? ((short)fileIO.ReadByte()) : fileIO.ReadInt16());
							if (num8 > 0)
							{
								if (versionNumber >= 38)
								{
									Main.chest[l].item[m].netDefaults(fileIO.ReadInt32());
								}
								else
								{
									short defaults = ItemID.FromLegacyName(fileIO.ReadString(), versionNumber);
									Main.chest[l].item[m].SetDefaults((int)defaults);
								}
								Main.chest[l].item[m].stack = num8;
								if (versionNumber >= 36)
								{
									Main.chest[l].item[m].Prefix((int)fileIO.ReadByte());
								}
							}
						}
					}
				}
			}
			for (int n = 0; n < 1000; n++)
			{
				if (fileIO.ReadBoolean())
				{
					string text2 = fileIO.ReadString();
					int num9 = fileIO.ReadInt32();
					int num10 = fileIO.ReadInt32();
					if (Main.tile[num9, num10].active() && (*Main.tile[num9, num10].type == 55 || *Main.tile[num9, num10].type == 85))
					{
						Main.sign[n] = new Sign();
						Main.sign[n].x = num9;
						Main.sign[n].y = num10;
						Main.sign[n].text = text2;
					}
				}
			}
			bool flag = fileIO.ReadBoolean();
			int num11 = 0;
			while (flag)
			{
				if (versionNumber >= 190)
				{
					Main.npc[num11].SetDefaults(fileIO.ReadInt32(), default(NPCSpawnParams));
				}
				else
				{
					Main.npc[num11].SetDefaults(NPCID.FromLegacyName(fileIO.ReadString()), default(NPCSpawnParams));
				}
				if (versionNumber >= 83)
				{
					Main.npc[num11].GivenName = fileIO.ReadString();
				}
				Main.npc[num11].position.X = fileIO.ReadSingle();
				Main.npc[num11].position.Y = fileIO.ReadSingle();
				Main.npc[num11].homeless = fileIO.ReadBoolean();
				Main.npc[num11].homeTileX = fileIO.ReadInt32();
				Main.npc[num11].homeTileY = fileIO.ReadInt32();
				flag = fileIO.ReadBoolean();
				num11++;
			}
			if (versionNumber >= 31 && versionNumber <= 83)
			{
				NPC.setNPCName(fileIO.ReadString(), 17, true);
				NPC.setNPCName(fileIO.ReadString(), 18, true);
				NPC.setNPCName(fileIO.ReadString(), 19, true);
				NPC.setNPCName(fileIO.ReadString(), 20, true);
				NPC.setNPCName(fileIO.ReadString(), 22, true);
				NPC.setNPCName(fileIO.ReadString(), 54, true);
				NPC.setNPCName(fileIO.ReadString(), 38, true);
				NPC.setNPCName(fileIO.ReadString(), 107, true);
				NPC.setNPCName(fileIO.ReadString(), 108, true);
				if (versionNumber >= 35)
				{
					NPC.setNPCName(fileIO.ReadString(), 124, true);
					if (versionNumber >= 65)
					{
						NPC.setNPCName(fileIO.ReadString(), 160, true);
						NPC.setNPCName(fileIO.ReadString(), 178, true);
						NPC.setNPCName(fileIO.ReadString(), 207, true);
						NPC.setNPCName(fileIO.ReadString(), 208, true);
						NPC.setNPCName(fileIO.ReadString(), 209, true);
						NPC.setNPCName(fileIO.ReadString(), 227, true);
						NPC.setNPCName(fileIO.ReadString(), 228, true);
						NPC.setNPCName(fileIO.ReadString(), 229, true);
						if (versionNumber >= 79)
						{
							NPC.setNPCName(fileIO.ReadString(), 353, true);
						}
					}
				}
			}
			if (Main.invasionType > 0 && Main.invasionSize > 0)
			{
				Main.FakeLoadInvasionStart();
			}
			if (versionNumber < 7)
			{
				return 0;
			}
			bool flag2 = fileIO.ReadBoolean();
			string text3 = fileIO.ReadString();
			int num12 = fileIO.ReadInt32();
			if (flag2 && (text3 == Main.worldName || num12 == Main.worldID))
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x04001E96 RID: 7830
		private static readonly object IOLock = new object();

		// Token: 0x04001E97 RID: 7831
		private static double _tempTime = Main.time;

		// Token: 0x04001E98 RID: 7832
		private static bool _tempRaining;

		// Token: 0x04001E99 RID: 7833
		private static float _tempMaxRain;

		// Token: 0x04001E9A RID: 7834
		private static int _tempRainTime;

		// Token: 0x04001E9B RID: 7835
		private static bool _tempDayTime = Main.dayTime;

		// Token: 0x04001E9C RID: 7836
		private static bool _tempBloodMoon = Main.bloodMoon;

		// Token: 0x04001E9D RID: 7837
		private static bool _tempEclipse = Main.eclipse;

		// Token: 0x04001E9E RID: 7838
		private static int _tempMoonPhase = Main.moonPhase;

		// Token: 0x04001E9F RID: 7839
		private static int _tempCultistDelay = (int)CultistRitual.delay;

		// Token: 0x04001EA0 RID: 7840
		private static int _versionNumber;

		// Token: 0x04001EA1 RID: 7841
		private static bool _isWorldOnCloud;

		// Token: 0x04001EA2 RID: 7842
		private static bool _tempPartyGenuine;

		// Token: 0x04001EA3 RID: 7843
		private static bool _tempPartyManual;

		// Token: 0x04001EA4 RID: 7844
		private static int _tempPartyCooldown;

		// Token: 0x04001EA5 RID: 7845
		private static readonly List<int> TempPartyCelebratingNPCs = new List<int>();

		// Token: 0x04001EA6 RID: 7846
		private static bool _hasCache;

		// Token: 0x04001EA7 RID: 7847
		private static bool? _cachedDayTime;

		// Token: 0x04001EA8 RID: 7848
		private static double? _cachedTime;

		// Token: 0x04001EA9 RID: 7849
		private static int? _cachedMoonPhase;

		// Token: 0x04001EAA RID: 7850
		private static bool? _cachedBloodMoon;

		// Token: 0x04001EAB RID: 7851
		private static bool? _cachedEclipse;

		// Token: 0x04001EAC RID: 7852
		private static int? _cachedCultistDelay;

		// Token: 0x04001EAD RID: 7853
		private static bool? _cachedPartyGenuine;

		// Token: 0x04001EAE RID: 7854
		private static bool? _cachedPartyManual;

		// Token: 0x04001EAF RID: 7855
		private static int? _cachedPartyDaysOnCooldown;

		// Token: 0x04001EB0 RID: 7856
		private static readonly List<int> CachedCelebratingNPCs = new List<int>();

		// Token: 0x04001EB1 RID: 7857
		private static bool? _cachedSandstormHappening;

		// Token: 0x04001EB2 RID: 7858
		private static bool _tempSandstormHappening;

		// Token: 0x04001EB3 RID: 7859
		private static int? _cachedSandstormTimeLeft;

		// Token: 0x04001EB4 RID: 7860
		private static int _tempSandstormTimeLeft;

		// Token: 0x04001EB5 RID: 7861
		private static float? _cachedSandstormSeverity;

		// Token: 0x04001EB6 RID: 7862
		private static float _tempSandstormSeverity;

		// Token: 0x04001EB7 RID: 7863
		private static float? _cachedSandstormIntendedSeverity;

		// Token: 0x04001EB8 RID: 7864
		private static float _tempSandstormIntendedSeverity;

		// Token: 0x04001EB9 RID: 7865
		private static bool _tempLanternNightGenuine;

		// Token: 0x04001EBA RID: 7866
		private static bool _tempLanternNightManual;

		// Token: 0x04001EBB RID: 7867
		private static bool _tempLanternNightNextNightIsGenuine;

		// Token: 0x04001EBC RID: 7868
		private static int _tempLanternNightCooldown;

		// Token: 0x04001EBD RID: 7869
		private static bool? _cachedLanternNightGenuine;

		// Token: 0x04001EBE RID: 7870
		private static bool? _cachedLanternNightManual;

		// Token: 0x04001EBF RID: 7871
		private static bool? _cachedLanternNightNextNightIsGenuine;

		// Token: 0x04001EC0 RID: 7872
		private static int? _cachedLanternNightCooldown;

		// Token: 0x04001EC1 RID: 7873
		public static Exception LastThrownLoadException;

		// Token: 0x02000B2B RID: 2859
		public static class TilePacker
		{
			// Token: 0x04006F37 RID: 28471
			public const int Header1_1 = 1;

			// Token: 0x04006F38 RID: 28472
			public const int Header1_2 = 2;

			// Token: 0x04006F39 RID: 28473
			public const int Header1_4 = 4;

			// Token: 0x04006F3A RID: 28474
			public const int Header1_8 = 8;

			// Token: 0x04006F3B RID: 28475
			public const int Header1_10 = 16;

			// Token: 0x04006F3C RID: 28476
			public const int Header1_18 = 24;

			// Token: 0x04006F3D RID: 28477
			public const int Header1_20 = 32;

			// Token: 0x04006F3E RID: 28478
			public const int Header1_40 = 64;

			// Token: 0x04006F3F RID: 28479
			public const int Header1_80 = 128;

			// Token: 0x04006F40 RID: 28480
			public const int Header1_C0 = 192;

			// Token: 0x04006F41 RID: 28481
			public const int Header2_1 = 1;

			// Token: 0x04006F42 RID: 28482
			public const int Header2_2 = 2;

			// Token: 0x04006F43 RID: 28483
			public const int Header2_4 = 4;

			// Token: 0x04006F44 RID: 28484
			public const int Header2_8 = 8;

			// Token: 0x04006F45 RID: 28485
			public const int Header2_10 = 16;

			// Token: 0x04006F46 RID: 28486
			public const int Header2_20 = 32;

			// Token: 0x04006F47 RID: 28487
			public const int Header2_40 = 64;

			// Token: 0x04006F48 RID: 28488
			public const int Header2_70 = 112;

			// Token: 0x04006F49 RID: 28489
			public const int Header2_80 = 128;

			// Token: 0x04006F4A RID: 28490
			public const int Header3_1 = 1;

			// Token: 0x04006F4B RID: 28491
			public const int Header3_2 = 2;

			// Token: 0x04006F4C RID: 28492
			public const int Header3_4 = 4;

			// Token: 0x04006F4D RID: 28493
			public const int Header3_8 = 8;

			// Token: 0x04006F4E RID: 28494
			public const int Header3_10 = 16;

			// Token: 0x04006F4F RID: 28495
			public const int Header3_20 = 32;

			// Token: 0x04006F50 RID: 28496
			public const int Header3_40 = 64;

			// Token: 0x04006F51 RID: 28497
			public const int Header3_80 = 128;

			// Token: 0x04006F52 RID: 28498
			public const int Header4_1 = 1;

			// Token: 0x04006F53 RID: 28499
			public const int Header4_2 = 2;

			// Token: 0x04006F54 RID: 28500
			public const int Header4_4 = 4;

			// Token: 0x04006F55 RID: 28501
			public const int Header4_8 = 8;

			// Token: 0x04006F56 RID: 28502
			public const int Header4_10 = 16;

			// Token: 0x04006F57 RID: 28503
			public const int Header4_20 = 32;

			// Token: 0x04006F58 RID: 28504
			public const int Header4_40 = 64;

			// Token: 0x04006F59 RID: 28505
			public const int Header4_80 = 128;
		}
	}
}
