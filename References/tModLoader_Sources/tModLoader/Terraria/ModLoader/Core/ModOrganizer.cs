using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json;
using Steamworks;
using Terraria.Localization;
using Terraria.ModLoader.Exceptions;
using Terraria.ModLoader.UI;
using Terraria.Social.Base;
using Terraria.Social.Steam;

namespace Terraria.ModLoader.Core
{
	/// <summary>
	/// Responsible for sorting, dependency verification and organizing which mods to load
	/// </summary>
	// Token: 0x02000364 RID: 868
	internal static class ModOrganizer
	{
		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06003019 RID: 12313 RVA: 0x005396C0 File Offset: 0x005378C0
		// (remove) Token: 0x0600301A RID: 12314 RVA: 0x005396F4 File Offset: 0x005378F4
		internal static event ModOrganizer.LocalModsChangedDelegate OnLocalModsChanged;

		// Token: 0x14000054 RID: 84
		// (add) Token: 0x0600301B RID: 12315 RVA: 0x00539728 File Offset: 0x00537928
		// (remove) Token: 0x0600301C RID: 12316 RVA: 0x0053975C File Offset: 0x0053795C
		internal static event ModOrganizer.LocalModsChangedDelegate PostLocalModsChanged;

		// Token: 0x0600301D RID: 12317 RVA: 0x0053978F File Offset: 0x0053798F
		internal static void LocalModsChanged(HashSet<string> modSlugs, bool isDeletion)
		{
			ModOrganizer.LocalModsChangedDelegate onLocalModsChanged = ModOrganizer.OnLocalModsChanged;
			if (onLocalModsChanged != null)
			{
				onLocalModsChanged(modSlugs, isDeletion);
			}
			ModOrganizer.LocalModsChangedDelegate postLocalModsChanged = ModOrganizer.PostLocalModsChanged;
			if (postLocalModsChanged == null)
			{
				return;
			}
			postLocalModsChanged(modSlugs, isDeletion);
		}

		/// <summary>Mods in workshop folders, not in dev folder or modpacks</summary>
		// Token: 0x0600301E RID: 12318 RVA: 0x005397B4 File Offset: 0x005379B4
		internal static IReadOnlyList<LocalMod> FindWorkshopMods()
		{
			return ModOrganizer.SelectVersionsToLoad(from m in ModOrganizer.FindAllMods()
			where m.location == ModLocation.Workshop
			select m, true).ToArray<LocalMod>();
		}

		/// <summary>Mods from any location, using the default internal priority logic</summary>
		// Token: 0x0600301F RID: 12319 RVA: 0x005397EA File Offset: 0x005379EA
		internal static LocalMod[] FindMods(bool logDuplicates = false)
		{
			return ModOrganizer.SelectVersionsToLoad(ModOrganizer.FindAllMods(), !logDuplicates).ToArray<LocalMod>();
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x005397FF File Offset: 0x005379FF
		internal static IEnumerable<LocalMod> RecheckVersionsToLoad()
		{
			return ModOrganizer.SelectVersionsToLoad(ModOrganizer.AllFoundMods, true);
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x0053980C File Offset: 0x00537A0C
		// (set) Token: 0x06003022 RID: 12322 RVA: 0x00539813 File Offset: 0x00537A13
		internal static Dictionary<string, LocalMod> LoadableModVersions { get; private set; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x0053981B File Offset: 0x00537A1B
		// (set) Token: 0x06003024 RID: 12324 RVA: 0x00539822 File Offset: 0x00537A22
		internal static LocalMod[] AllFoundMods { get; private set; }

		// Token: 0x06003025 RID: 12325 RVA: 0x0053982C File Offset: 0x00537A2C
		internal static LocalMod[] FindAllMods()
		{
			Logging.tML.Info("Finding Mods...");
			Directory.CreateDirectory(ModLoader.ModPath);
			ModOrganizer.DeleteTemporaryFiles();
			List<LocalMod> mods = new List<LocalMod>();
			if (!string.IsNullOrEmpty(ModOrganizer.ModPackActive))
			{
				if (Directory.Exists(ModOrganizer.ModPackActive))
				{
					Logging.tML.Info("Loading Mods from active modpack: " + ModOrganizer.ModPackActive);
					mods.AddRange(ModOrganizer.ReadModFiles(ModLocation.Modpack, Directory.GetFiles(ModOrganizer.ModPackActive, "*.tmod", SearchOption.AllDirectories)));
				}
				else
				{
					Logging.tML.Warn("Active modpack missing, deactivating: " + ModOrganizer.ModPackActive);
					ModOrganizer.ModPackActive = null;
				}
			}
			mods.AddRange(ModOrganizer.ReadModFiles(ModLocation.Local, Directory.GetFiles(ModOrganizer.modPath, "*.tmod", SearchOption.TopDirectoryOnly)));
			ModOrganizer.WorkshopFileFinder.Refresh(new WorkshopIssueReporter());
			foreach (string repo in ModOrganizer.WorkshopFileFinder.ModPaths)
			{
				mods.AddRange(ModOrganizer.ReadModFiles(ModLocation.Workshop, Directory.GetFiles(repo, "*.tmod", SearchOption.AllDirectories)));
			}
			return ModOrganizer.AllFoundMods = mods.ToArray();
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x00539960 File Offset: 0x00537B60
		private static IEnumerable<LocalMod> SelectVersionsToLoad(IEnumerable<LocalMod> mods, bool quiet = false)
		{
			return from m in mods
			group m by m.Name into g
			orderby g.Key
			select g into m
			select ModOrganizer.SelectVersionToLoad(m, quiet) into m
			where m != null
			select m;
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x005399F8 File Offset: 0x00537BF8
		private static LocalMod SelectVersionToLoad(IEnumerable<LocalMod> versions, bool quiet = false)
		{
			ModOrganizer.<>c__DisplayClass30_0 CS$<>8__locals1 = new ModOrganizer.<>c__DisplayClass30_0();
			CS$<>8__locals1.quiet = quiet;
			CS$<>8__locals1.list = versions.ToList<LocalMod>();
			ModOrganizer.<>c__DisplayClass30_0 CS$<>8__locals2 = CS$<>8__locals1;
			Func<LocalMod, bool> condition;
			if ((condition = ModOrganizer.<>O.<0>__ServerRequiresDifferentVersion) == null)
			{
				condition = (ModOrganizer.<>O.<0>__ServerRequiresDifferentVersion = new Func<LocalMod, bool>(ModNet.ServerRequiresDifferentVersion));
			}
			CS$<>8__locals2.<SelectVersionToLoad>g__FilterOut|0(condition, "connecting to a server which requires a different version");
			CS$<>8__locals1.<SelectVersionToLoad>g__FilterOut|0((LocalMod m) => SocialBrowserModule.GetBrowserVersionNumber(m.tModLoaderVersion) != SocialBrowserModule.GetBrowserVersionNumber(BuildInfo.tMLVersion), "mod is for a different Terraria version/LTS release stream");
			CS$<>8__locals1.<SelectVersionToLoad>g__FilterOut|0((LocalMod m) => SocialBrowserModule.GetBrowserVersionNumber(m.tModLoaderVersion).Contains("Transitive"), "The tML version is transitional with no distribution or mod browser support");
			ModOrganizer.<>c__DisplayClass30_0 CS$<>8__locals3 = CS$<>8__locals1;
			Func<LocalMod, bool> condition2;
			if ((condition2 = ModOrganizer.<>O.<1>__SkipModForPreviewNotPlayable) == null)
			{
				condition2 = (ModOrganizer.<>O.<1>__SkipModForPreviewNotPlayable = new Func<LocalMod, bool>(ModOrganizer.SkipModForPreviewNotPlayable));
			}
			CS$<>8__locals3.<SelectVersionToLoad>g__FilterOut|0(condition2, "preview early-access disabled");
			CS$<>8__locals1.<SelectVersionToLoad>g__FilterOut|0((LocalMod m) => BuildInfo.tMLVersion < m.tModLoaderVersion.MajorMinor(), "mod is for a newer tML monthly release");
			CS$<>8__locals1.<SelectVersionToLoad>g__OrderByDescending|1<bool>((LocalMod m) => m.location == ModLocation.Modpack, "a frozen copy is present in the active modpack");
			CS$<>8__locals1.<SelectVersionToLoad>g__OrderByDescending|1<Version>((LocalMod m) => m.Version, "a newer version exists");
			CS$<>8__locals1.<SelectVersionToLoad>g__OrderByDescending|1<Version>((LocalMod m) => m.tModLoaderVersion, "a matching version for a newer tModLoader exists");
			CS$<>8__locals1.<SelectVersionToLoad>g__FilterOut|0((LocalMod m) => m.location != ModLocation.Workshop && CS$<>8__locals1.list.Any((LocalMod m2) => m2.location == ModLocation.Workshop && m2.modFile.Hash == m.modFile.Hash), "an identical copy exists in the workshop folder");
			CS$<>8__locals1.<SelectVersionToLoad>g__OrderByDescending|1<bool>((LocalMod m) => m.location == ModLocation.Local, "a local copy with the same version (but different hash) exists");
			CS$<>8__locals1.<SelectVersionToLoad>g__OrderByDescending|1<bool>((LocalMod m) => Path.GetFileNameWithoutExtension(m.modFile.path) == m.Name, "this .tmod has been renamed");
			CS$<>8__locals1.selected = CS$<>8__locals1.list.FirstOrDefault<LocalMod>();
			CS$<>8__locals1.<SelectVersionToLoad>g__FilterOut|0((LocalMod v) => v != CS$<>8__locals1.selected, "Logic Error, multiple versions remain. One was randomly selected");
			if (!CS$<>8__locals1.quiet)
			{
				Logging.tML.Debug("Selected " + CS$<>8__locals1.selected.DetailedInfo + ".");
			}
			return CS$<>8__locals1.selected;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x00539C2B File Offset: 0x00537E2B
		private static IEnumerable<LocalMod> ReadModFiles(ModLocation location, IEnumerable<string> fileNames)
		{
			foreach (string fileName in fileNames)
			{
				LocalMod mod;
				if (ModOrganizer.TryReadLocalMod(location, fileName, out mod))
				{
					yield return mod;
				}
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x00539C44 File Offset: 0x00537E44
		private static bool TryReadLocalMod(ModLocation location, string fileName, out LocalMod mod)
		{
			DateTime lastModified = File.GetLastWriteTime(fileName);
			if (ModOrganizer.modsDirCache.TryGetValue(fileName, out mod) && mod.lastModified == lastModified)
			{
				return true;
			}
			try
			{
				TmodFile modFile = new TmodFile(fileName, null, null);
				using (modFile.Open())
				{
					mod = new LocalMod(location, modFile)
					{
						lastModified = lastModified
					};
				}
			}
			catch (Exception e)
			{
				if (ModOrganizer.readFailures.Add(fileName))
				{
					Logging.tML.Warn("Failed to read " + fileName, e);
				}
				return false;
			}
			ModOrganizer.modsDirCache[fileName] = mod;
			return true;
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x00539CFC File Offset: 0x00537EFC
		internal static bool ApplyPreviewChecks(LocalMod mod)
		{
			return BuildInfo.IsPreview && mod.location == ModLocation.Workshop;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x00539D10 File Offset: 0x00537F10
		internal static bool CheckStableBuildOnPreview(LocalMod mod)
		{
			return ModOrganizer.ApplyPreviewChecks(mod) && mod.properties.buildVersion.MajorMinor() <= BuildInfo.stableVersion.MajorMinor();
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x00539D3B File Offset: 0x00537F3B
		internal static bool SkipModForPreviewNotPlayable(LocalMod mod)
		{
			return ModOrganizer.ApplyPreviewChecks(mod) && !mod.properties.playableOnPreview && mod.properties.buildVersion.MajorMinor() > BuildInfo.stableVersion;
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x00539D6E File Offset: 0x00537F6E
		internal static bool IsModFromSteam(string modPath)
		{
			return modPath.Contains(Path.Combine(new string[]
			{
				"workshop"
			}), StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x00539D8C File Offset: 0x00537F8C
		internal static HashSet<string> IdentifyMissingWorkshopDependencies()
		{
			IReadOnlyList<LocalMod> mods = ModOrganizer.FindWorkshopMods();
			string[] installedSlugs = (from s in mods
			select s.Name).ToArray<string>();
			HashSet<string> missingModSlugs = new HashSet<string>();
			Func<string, bool> <>9__3;
			foreach (LocalMod localMod in from m in mods
			where m.properties.modReferences.Length != 0
			select m)
			{
				IEnumerable<string> source = localMod.properties.modReferences.Select((BuildProperties.ModReference dep) => dep.mod);
				Func<string, bool> predicate;
				if ((predicate = <>9__3) == null)
				{
					predicate = (<>9__3 = ((string slug) => !installedSlugs.Contains(slug)));
				}
				IEnumerable<string> missingDeps = source.Where(predicate);
				missingModSlugs.UnionWith(missingDeps);
			}
			return missingModSlugs;
		}

		/// <summary>
		/// Returns changes based on last time <see cref="M:Terraria.ModLoader.Core.ModOrganizer.SaveLastLaunchedMods" /> was called. Can be null if no changes.
		/// </summary>
		// Token: 0x0600302F RID: 12335 RVA: 0x00539E94 File Offset: 0x00538094
		internal static string DetectModChangesForInfoMessage(out IEnumerable<string> removedMods)
		{
			if (!ModLoader.showNewUpdatedModsInfo || !File.Exists(ModOrganizer.lastLaunchedModsFilePath))
			{
				removedMods = Array.Empty<string>();
				return null;
			}
			Dictionary<string, LocalMod> currMods = ModOrganizer.FindWorkshopMods().ToDictionary((LocalMod mod) => mod.Name, (LocalMod mod) => mod);
			Logging.tML.Info("3 most recently changed workshop mods: " + string.Join(", ", (from x in currMods
			orderby x.Value.lastModified descending
			select x).Take(3).Select(delegate(KeyValuePair<string, LocalMod> x)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 3);
				defaultInterpolatedStringHandler.AppendFormatted(x.Value.Name);
				defaultInterpolatedStringHandler.AppendLiteral(" v");
				defaultInterpolatedStringHandler.AppendFormatted<Version>(x.Value.Version);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted<DateTime>(x.Value.lastModified, "d");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			})));
			string result;
			try
			{
				IEnumerable<string> enumerable = File.ReadLines(ModOrganizer.lastLaunchedModsFilePath);
				Dictionary<string, Version> lastMods = new Dictionary<string, Version>();
				foreach (string text in enumerable)
				{
					string[] parts = text.Split(' ', StringSplitOptions.None);
					if (parts.Length == 2)
					{
						string name5 = parts[0];
						string versionString = parts[1];
						lastMods.Add(name5, new Version(versionString));
					}
				}
				List<string> newMods = new List<string>();
				List<string> updatedMods = new List<string>();
				StringBuilder messages = new StringBuilder();
				removedMods = from name in lastMods.Keys
				where !currMods.ContainsKey(name)
				select name;
				foreach (KeyValuePair<string, LocalMod> item in currMods)
				{
					string name2 = item.Key;
					Version version = item.Value.Version;
					Version lastVersion;
					if (!lastMods.ContainsKey(name2))
					{
						newMods.Add(name2);
						ModOrganizer.modsThatUpdatedSinceLastLaunch.Add(new ValueTuple<string, Version>(name2, null));
					}
					else if (lastMods.TryGetValue(name2, out lastVersion) && lastVersion < version)
					{
						updatedMods.Add(name2);
						ModOrganizer.modsThatUpdatedSinceLastLaunch.Add(new ValueTuple<string, Version>(name2, lastVersion));
					}
				}
				if (removedMods.Count<string>() > 0)
				{
					messages.Append(Language.GetTextValue("tModLoader.ShowRemovedModsInfoMessageUpdatedMods"));
					foreach (string name3 in removedMods)
					{
						StringBuilder stringBuilder = messages;
						StringBuilder stringBuilder2 = stringBuilder;
						StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(16, 1, stringBuilder);
						appendInterpolatedStringHandler.AppendLiteral("\n  ");
						appendInterpolatedStringHandler.AppendFormatted(name3);
						appendInterpolatedStringHandler.AppendLiteral(" was removed.");
						stringBuilder2.Append(ref appendInterpolatedStringHandler);
					}
				}
				if (newMods.Count > 0)
				{
					messages.Append(Language.GetTextValue("tModLoader.ShowNewUpdatedModsInfoMessageNewMods"));
					foreach (string newMod in newMods)
					{
						StringBuilder stringBuilder = messages;
						StringBuilder stringBuilder3 = stringBuilder;
						StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(6, 2, stringBuilder);
						appendInterpolatedStringHandler.AppendLiteral("\n  ");
						appendInterpolatedStringHandler.AppendFormatted(newMod);
						appendInterpolatedStringHandler.AppendLiteral(" (");
						appendInterpolatedStringHandler.AppendFormatted(currMods[newMod].DisplayName);
						appendInterpolatedStringHandler.AppendLiteral(")");
						stringBuilder3.Append(ref appendInterpolatedStringHandler);
					}
				}
				if (updatedMods.Count > 0)
				{
					messages.Append(Language.GetTextValue("tModLoader.ShowNewUpdatedModsInfoMessageUpdatedMods"));
					foreach (string name4 in updatedMods)
					{
						string displayName = currMods[name4].DisplayName;
						Version lastVersion2 = lastMods[name4];
						Version currVersion = currMods[name4].properties.version;
						StringBuilder stringBuilder = messages;
						StringBuilder stringBuilder4 = stringBuilder;
						StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(13, 4, stringBuilder);
						appendInterpolatedStringHandler.AppendLiteral("\n  ");
						appendInterpolatedStringHandler.AppendFormatted(name4);
						appendInterpolatedStringHandler.AppendLiteral(" (");
						appendInterpolatedStringHandler.AppendFormatted(displayName);
						appendInterpolatedStringHandler.AppendLiteral(") v");
						appendInterpolatedStringHandler.AppendFormatted<Version>(lastVersion2);
						appendInterpolatedStringHandler.AppendLiteral(" -> v");
						appendInterpolatedStringHandler.AppendFormatted<Version>(currVersion);
						stringBuilder4.Append(ref appendInterpolatedStringHandler);
					}
				}
				result = ((messages.Length > 0) ? messages.ToString() : null);
			}
			catch
			{
				removedMods = Array.Empty<string>();
				result = null;
			}
			return result;
		}

		/// <summary>
		/// Collects local mod status and saves it to a file.
		/// </summary>
		// Token: 0x06003030 RID: 12336 RVA: 0x0053A390 File Offset: 0x00538590
		internal static void SaveLastLaunchedMods()
		{
			if (Main.dedServ)
			{
				return;
			}
			if (!ModLoader.showNewUpdatedModsInfo)
			{
				return;
			}
			IEnumerable<LocalMod> enumerable = ModOrganizer.FindWorkshopMods();
			StringBuilder fileText = new StringBuilder();
			foreach (LocalMod mod in enumerable)
			{
				StringBuilder stringBuilder = fileText;
				StringBuilder stringBuilder2 = stringBuilder;
				StringBuilder.AppendInterpolatedStringHandler appendInterpolatedStringHandler = new StringBuilder.AppendInterpolatedStringHandler(2, 2, stringBuilder);
				appendInterpolatedStringHandler.AppendFormatted(mod.Name);
				appendInterpolatedStringHandler.AppendLiteral(" ");
				appendInterpolatedStringHandler.AppendFormatted<Version>(mod.Version);
				appendInterpolatedStringHandler.AppendLiteral("\n");
				stringBuilder2.Append(ref appendInterpolatedStringHandler);
			}
			File.WriteAllText(ModOrganizer.lastLaunchedModsFilePath, fileText.ToString());
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x0053A444 File Offset: 0x00538644
		private static void DeleteTemporaryFiles()
		{
			foreach (string path in ModOrganizer.GetTemporaryFiles())
			{
				Logging.tML.Info("Cleaning up leftover temporary file " + Path.GetFileName(path));
				try
				{
					File.Delete(path);
				}
				catch (Exception e)
				{
					Logging.tML.Error("Could not delete leftover temporary file " + Path.GetFileName(path), e);
				}
			}
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x0053A4D8 File Offset: 0x005386D8
		private static IEnumerable<string> GetTemporaryFiles()
		{
			return Directory.GetFiles(ModOrganizer.modPath, "*.tmp", SearchOption.TopDirectoryOnly).Union(Directory.GetFiles(ModOrganizer.modPath, "temporaryDownload.tmod", SearchOption.TopDirectoryOnly));
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x0053A4FF File Offset: 0x005386FF
		internal static bool LoadSide(ModSide side)
		{
			return side != (Main.dedServ ? ModSide.Client : ModSide.Server);
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x0053A514 File Offset: 0x00538714
		internal static List<LocalMod> SelectAndSortMods(IEnumerable<LocalMod> availableMods, CancellationToken token)
		{
			List<string> missing = ModLoader.EnabledMods.Except(from mod in availableMods
			select mod.Name).ToList<string>();
			if (missing.Any<string>())
			{
				Logging.tML.Info("Missing previously enabled mods: " + string.Join(", ", missing));
				foreach (string name in missing)
				{
					ModLoader.EnabledMods.Remove(name);
				}
				ModOrganizer.SaveEnabledMods();
			}
			if ((Main.instance.IsActive && Main.oldKeyState.PressingShift()) || ModLoader.skipLoad || token.IsCancellationRequested)
			{
				ModLoader.skipLoad = false;
				Interface.loadMods.SetLoadStage("tModLoader.CancellingLoading", -1);
				return new List<LocalMod>();
			}
			ModOrganizer.CommandLineModPackOverride(availableMods);
			Interface.loadMods.SetLoadStage("tModLoader.MSFinding", -1);
			foreach (LocalMod mod2 in ModOrganizer.GetModsToLoad(availableMods))
			{
				ModOrganizer.EnableWithDeps(mod2, availableMods);
			}
			ModOrganizer.SaveEnabledMods();
			List<LocalMod> modsToLoad = ModOrganizer.GetModsToLoad(availableMods);
			List<LocalMod> result;
			try
			{
				ModOrganizer.EnsureRecentlyBuildModsAreLoading(modsToLoad);
				ModOrganizer.EnsureDependenciesExist(modsToLoad, false);
				ModOrganizer.EnsureTargetVersionsMet(modsToLoad);
				ModOrganizer.EnsureHashesAreValid(modsToLoad);
				result = ModOrganizer.Sort(modsToLoad);
			}
			catch (ModSortingException e)
			{
				e.Data["mods"] = (from m in e.errored
				select m.Name).ToArray<string>();
				e.Data["hideStackTrace"] = true;
				throw;
			}
			return result;
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x0053A6FC File Offset: 0x005388FC
		private static List<LocalMod> GetModsToLoad(IEnumerable<LocalMod> availableMods)
		{
			List<LocalMod> list = (from mod in availableMods
			where mod.Enabled && ModOrganizer.LoadSide(mod.properties.side)
			select mod).ToList<LocalMod>();
			ModOrganizer.VerifyNames(list);
			return list;
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x0053A730 File Offset: 0x00538930
		private static void CommandLineModPackOverride(IEnumerable<LocalMod> mods)
		{
			if (string.IsNullOrWhiteSpace(ModOrganizer.commandLineModPack))
			{
				return;
			}
			if (!ModOrganizer.commandLineModPack.EndsWith(".json"))
			{
				ModOrganizer.commandLineModPack += ".json";
			}
			string filePath = Path.Combine(UIModPacks.ModPacksDirectory, ModOrganizer.commandLineModPack);
			try
			{
				Directory.CreateDirectory(UIModPacks.ModPacksDirectory);
				Logging.ServerConsoleLine(Language.GetTextValue("tModLoader.LoadingSpecifiedModPack", ModOrganizer.commandLineModPack));
				HashSet<string> modSet = JsonConvert.DeserializeObject<HashSet<string>>(File.ReadAllText(filePath));
				foreach (LocalMod mod in mods)
				{
					mod.Enabled = modSet.Contains(mod.Name);
				}
			}
			catch (Exception e)
			{
				throw new Exception((e is FileNotFoundException) ? Language.GetTextValue("tModLoader.ModPackDoesNotExist", filePath) : Language.GetTextValue("tModLoader.ModPackMalformed", ModOrganizer.commandLineModPack), e);
			}
			finally
			{
				ModOrganizer.commandLineModPack = null;
			}
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x0053A840 File Offset: 0x00538A40
		internal static void EnableWithDeps(LocalMod mod, IEnumerable<LocalMod> availableMods)
		{
			mod.Enabled = true;
			using (IEnumerator<string> enumerator = mod.properties.RefNames(false).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string depName = enumerator.Current;
					LocalMod dep = availableMods.SingleOrDefault((LocalMod m) => m.Name == depName);
					if (dep != null && !dep.Enabled)
					{
						ModOrganizer.EnableWithDeps(dep, availableMods);
					}
				}
			}
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x0053A8C4 File Offset: 0x00538AC4
		private static void VerifyNames(List<LocalMod> mods)
		{
			HashSet<string> names = new HashSet<string>();
			List<string> errors = new List<string>();
			List<LocalMod> erroredMods = new List<LocalMod>();
			foreach (LocalMod mod in mods)
			{
				if (mod.Name.Length == 0)
				{
					errors.Add(Language.GetTextValue("tModLoader.BuildErrorModNameEmpty"));
				}
				else if (mod.Name.Equals("Terraria", StringComparison.InvariantCultureIgnoreCase))
				{
					errors.Add(Language.GetTextValue("tModLoader.BuildErrorModNamedTerraria"));
				}
				else if (mod.Name.IndexOf('.') >= 0)
				{
					errors.Add(Language.GetTextValue("tModLoader.BuildErrorModNameHasPeriod"));
				}
				else
				{
					if (names.Add(mod.Name))
					{
						continue;
					}
					errors.Add(Language.GetTextValue("tModLoader.BuildErrorTwoModsSameName", mod.Name));
				}
				erroredMods.Add(mod);
			}
			if (erroredMods.Count > 0)
			{
				Exception ex = new Exception(string.Join("\n", errors));
				ex.Data["mods"] = (from m in erroredMods
				select m.Name).ToArray<string>();
				throw ex;
			}
		}

		// Token: 0x06003039 RID: 12345 RVA: 0x0053AA10 File Offset: 0x00538C10
		private static void EnsureRecentlyBuildModsAreLoading(List<LocalMod> mods)
		{
			using (List<LocalMod>.Enumerator enumerator = mods.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					LocalMod mod = enumerator.Current;
					LocalMod localMod = ModOrganizer.AllFoundMods.SingleOrDefault((LocalMod x) => x.Name == mod.Name && x.location == ModLocation.Local && Path.GetFileNameWithoutExtension(x.modFile.path) == mod.Name);
					if (localMod != null && localMod != mod && localMod.lastModified.CompareTo(mod.lastModified) > 0 && localMod.lastModified.CompareTo(ModCompile.recentlyBuiltModCheckTimeCutoff) > 0)
					{
						Exception ex = new Exception(Language.GetTextValue("tModLoader.LoadErrorRecentlyBuiltLocalModWithLowerVersion" + mod.location.ToString(), localMod.Name, localMod.Version, mod.Version));
						ex.Data["mod"] = mod.Name;
						ex.Data["hideStackTrace"] = true;
						throw ex;
					}
				}
			}
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x0053AB38 File Offset: 0x00538D38
		internal static void EnsureDependenciesExist(ICollection<LocalMod> mods, bool includeWeak)
		{
			Dictionary<string, LocalMod> nameMap = mods.ToDictionary((LocalMod mod) => mod.Name);
			HashSet<LocalMod> errored = new HashSet<LocalMod>();
			StringBuilder errorLog = new StringBuilder();
			foreach (LocalMod mod2 in mods)
			{
				foreach (string depName in mod2.properties.RefNames(includeWeak))
				{
					if (!nameMap.ContainsKey(depName))
					{
						errored.Add(mod2);
						errorLog.AppendLine(Language.GetTextValue("tModLoader.LoadErrorDependencyMissing", depName, mod2));
					}
				}
			}
			if (errored.Count > 0)
			{
				throw new ModSortingException(errored, errorLog.ToString());
			}
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x0053AC2C File Offset: 0x00538E2C
		internal static void EnsureTargetVersionsMet(ICollection<LocalMod> mods)
		{
			Dictionary<string, LocalMod> nameMap = mods.ToDictionary((LocalMod mod) => mod.Name);
			HashSet<LocalMod> errored = new HashSet<LocalMod>();
			StringBuilder errorLog = new StringBuilder();
			foreach (LocalMod mod2 in mods)
			{
				foreach (BuildProperties.ModReference dep in mod2.properties.Refs(true))
				{
					LocalMod inst;
					if (!(dep.target == null) && nameMap.TryGetValue(dep.mod, out inst))
					{
						if (inst.Version < dep.target)
						{
							errored.Add(mod2);
							errorLog.AppendLine(Language.GetTextValue("tModLoader.LoadErrorDependencyVersionTooLow", new object[]
							{
								mod2,
								dep.target,
								dep.mod,
								inst.Version
							}));
						}
						else if (inst.Version.Major != dep.target.Major)
						{
							errored.Add(mod2);
							errorLog.AppendLine(Language.GetTextValue("tModLoader.LoadErrorMajorVersionMismatch", new object[]
							{
								mod2,
								dep.target,
								dep.mod,
								inst.Version
							}));
						}
					}
				}
			}
			if (errored.Count > 0)
			{
				throw new ModSortingException(errored, errorLog.ToString());
			}
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x0053ADFC File Offset: 0x00538FFC
		internal static void EnsureHashesAreValid(ICollection<LocalMod> mods)
		{
			HashSet<LocalMod> errored = new HashSet<LocalMod>();
			StringBuilder errorLog = new StringBuilder();
			foreach (LocalMod mod in mods)
			{
				if (!mod.modFile.VerifyHash())
				{
					errored.Add(mod);
					errorLog.AppendLine(Language.GetTextValue("tModLoader.LoadErrorHashMismatchCorruptedWithModName", mod));
				}
			}
			if (errored.Count > 0)
			{
				throw new ModSortingException(errored, errorLog.ToString());
			}
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x0053AE88 File Offset: 0x00539088
		internal static void EnsureSyncedDependencyStability(TopoSort<LocalMod> synced, TopoSort<LocalMod> full)
		{
			HashSet<LocalMod> errored = new HashSet<LocalMod>();
			StringBuilder errorLog = new StringBuilder();
			foreach (LocalMod mod in synced.list)
			{
				List<List<LocalMod>> chains = new List<List<LocalMod>>();
				Action<LocalMod, Stack<LocalMod>> FindChains = null;
				FindChains = delegate(LocalMod search, Stack<LocalMod> stack)
				{
					stack.Push(search);
					if (search.properties.side == ModSide.Both && stack.Count > 1)
					{
						if (stack.Count > 2)
						{
							chains.Add(stack.Reverse<LocalMod>().ToList<LocalMod>());
						}
					}
					else
					{
						foreach (LocalMod dep in full.Dependencies(search))
						{
							FindChains(dep, stack);
						}
					}
					stack.Pop();
				};
				FindChains(mod, new Stack<LocalMod>());
				if (chains.Count != 0)
				{
					ISet<LocalMod> syncedDependencies = synced.AllDependencies(mod);
					foreach (List<LocalMod> chain in chains)
					{
						if (!syncedDependencies.Contains(chain.Last<LocalMod>()))
						{
							errored.Add(mod);
							StringBuilder stringBuilder = errorLog;
							string[] array = new string[5];
							int num = 0;
							LocalMod localMod = mod;
							array[num] = ((localMod != null) ? localMod.ToString() : null);
							array[1] = " indirectly depends on ";
							int num2 = 2;
							LocalMod localMod2 = chain.Last<LocalMod>();
							array[num2] = ((localMod2 != null) ? localMod2.ToString() : null);
							array[3] = " via ";
							array[4] = string.Join<LocalMod>(" -> ", chain);
							stringBuilder.AppendLine(string.Concat(array));
						}
					}
				}
			}
			if (errored.Count > 0)
			{
				errorLog.AppendLine("Some of these mods may not exist on both client and server. Add a direct sort entries or weak references.");
				throw new ModSortingException(errored, errorLog.ToString());
			}
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x0053B040 File Offset: 0x00539240
		private static TopoSort<LocalMod> BuildSort(ICollection<LocalMod> mods)
		{
			Dictionary<string, LocalMod> nameMap = mods.ToDictionary((LocalMod mod) => mod.Name);
			Func<string, LocalMod> <>9__3;
			Func<string, LocalMod> <>9__4;
			return new TopoSort<LocalMod>(mods, delegate(LocalMod mod)
			{
				IEnumerable<string> source = mod.properties.sortAfter.Where(new Func<string, bool>(nameMap.ContainsKey));
				Func<string, LocalMod> selector;
				if ((selector = <>9__3) == null)
				{
					selector = (<>9__3 = ((string name) => nameMap[name]));
				}
				return source.Select(selector);
			}, delegate(LocalMod mod)
			{
				IEnumerable<string> source = mod.properties.sortBefore.Where(new Func<string, bool>(nameMap.ContainsKey));
				Func<string, LocalMod> selector;
				if ((selector = <>9__4) == null)
				{
					selector = (<>9__4 = ((string name) => nameMap[name]));
				}
				return source.Select(selector);
			});
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x0053B09C File Offset: 0x0053929C
		internal static List<LocalMod> Sort(ICollection<LocalMod> mods)
		{
			List<LocalMod> list = (from mod in mods
			orderby mod.Name
			select mod).ToList<LocalMod>();
			TopoSort<LocalMod> syncedSort = ModOrganizer.BuildSort((from mod in list
			where mod.properties.side == ModSide.Both
			select mod).ToList<LocalMod>());
			TopoSort<LocalMod> fullSort = ModOrganizer.BuildSort(list);
			ModOrganizer.EnsureSyncedDependencyStability(syncedSort, fullSort);
			List<LocalMod> result;
			try
			{
				List<LocalMod> syncedList = syncedSort.Sort();
				for (int i = 1; i < syncedList.Count; i++)
				{
					fullSort.AddEntry(syncedList[i - 1], syncedList[i]);
				}
				result = fullSort.Sort();
			}
			catch (TopoSort<LocalMod>.SortingException e)
			{
				throw new ModSortingException(e.set, e.Message);
			}
			return result;
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x0053B170 File Offset: 0x00539370
		internal static void SaveEnabledMods()
		{
			Directory.CreateDirectory(ModLoader.ModPath);
			string json = JsonConvert.SerializeObject(ModLoader.EnabledMods, 1);
			File.WriteAllText(Path.Combine(ModOrganizer.modPath, "enabled.json"), json);
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x0053B1AC File Offset: 0x005393AC
		internal static HashSet<string> LoadEnabledMods()
		{
			HashSet<string> result;
			try
			{
				string path = Path.Combine(ModOrganizer.modPath, "enabled.json");
				if (!File.Exists(path))
				{
					Logging.tML.Warn("Did not find enabled.json file");
					result = new HashSet<string>();
				}
				else
				{
					result = (JsonConvert.DeserializeObject<HashSet<string>>(File.ReadAllText(path)) ?? new HashSet<string>());
				}
			}
			catch (Exception e)
			{
				Logging.tML.Warn("Unknown error occurred when trying to read enabled.json", e);
				result = new HashSet<string>();
			}
			return result;
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x0053B22C File Offset: 0x0053942C
		internal static string GetActiveTmodInRepo(string repo)
		{
			IEnumerable<ValueTuple<string, Version, bool>> information = from t in ModOrganizer.AnalyzeWorkshopTmods(repo)
			where !SocialBrowserModule.GetBrowserVersionNumber(t.Item2).Contains("Transitive")
			select t;
			if (information == null || information.Count<ValueTuple<string, Version, bool>>() == 0)
			{
				Logging.tML.Warn("Unexpectedly missing .tMods in Workshop Folder " + repo);
				return null;
			}
			ValueTuple<string, Version, bool> recommendedTmod = (from t in information
			where t.Item2 <= BuildInfo.tMLVersion
			orderby t.Item2 descending
			select t).FirstOrDefault<ValueTuple<string, Version, bool>>();
			ValueTuple<string, Version, bool> valueTuple = recommendedTmod;
			if (valueTuple.Item1 == null && valueTuple.Item2 == null && !valueTuple.Item3)
			{
				Logging.tML.Warn("No .tMods found for this version in Workshop Folder " + repo + ". Defaulting to show newest");
				return (from t in information
				orderby t.Item2 descending
				select t).First<ValueTuple<string, Version, bool>>().Item1;
			}
			return recommendedTmod.Item1;
		}

		/// <summary>
		/// Must Be called AFTER the new files are added to the publishing repo.
		/// Assumes one .tmod per YYYY.XX folder in the publishing repo
		/// </summary>
		/// <param name="repo"></param>
		// Token: 0x06003043 RID: 12355 RVA: 0x0053B34C File Offset: 0x0053954C
		internal static void CleanupOldPublish(string repo)
		{
			if (BuildInfo.IsPreview)
			{
				ModOrganizer.RemoveSkippablePreview(repo);
			}
			if (Directory.GetFiles(repo, "*.tmod", SearchOption.AllDirectories).Length <= 3)
			{
				return;
			}
			List<ValueTuple<string, Version, bool>> information = ModOrganizer.AnalyzeWorkshopTmods(repo);
			if (information == null || information.Count<ValueTuple<string, Version, bool>>() <= 3)
			{
				return;
			}
			foreach (ValueTuple<string, int> requirement in new ValueTuple<string, int>[]
			{
				new ValueTuple<string, int>("1.4.3", 1),
				new ValueTuple<string, int>("1.4.4", 3),
				new ValueTuple<string, int>("1.3", 1),
				new ValueTuple<string, int>("1.4.4-Transitive", 0)
			})
			{
				foreach (ValueTuple<string, Version, bool> item in ModOrganizer.GetOrderedTmodWorkshopInfoForVersion(information, requirement.Item1).Skip(requirement.Item2))
				{
					if (item.Item3)
					{
						Directory.Delete(Path.GetDirectoryName(item.Item1), true);
					}
					else
					{
						File.Delete(item.Item1);
					}
				}
			}
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x0053B46C File Offset: 0x0053966C
		[return: TupleElementNames(new string[]
		{
			"file",
			"tModVersion",
			"isInFolder"
		})]
		internal static IOrderedEnumerable<ValueTuple<string, Version, bool>> GetOrderedTmodWorkshopInfoForVersion([TupleElementNames(new string[]
		{
			"file",
			"tModVersion",
			"isInFolder"
		})] List<ValueTuple<string, Version, bool>> information, string tmlVersion)
		{
			return from t in information
			where SocialBrowserModule.GetBrowserVersionNumber(t.Item2) == tmlVersion
			orderby t.Item2 descending
			select t;
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x0053B4BC File Offset: 0x005396BC
		[return: TupleElementNames(new string[]
		{
			"file",
			"tModVersion",
			"isInFolder"
		})]
		internal static List<ValueTuple<string, Version, bool>> AnalyzeWorkshopTmods(string repo)
		{
			string[] files = Directory.GetFiles(repo, "*.tmod", SearchOption.AllDirectories);
			List<ValueTuple<string, Version, bool>> information = new List<ValueTuple<string, Version, bool>>();
			foreach (string filename in files)
			{
				Match match = ModOrganizer.PublishFolderMetadata.Match(filename);
				if (match.Success)
				{
					information.Add(new ValueTuple<string, Version, bool>(filename, new Version(match.Groups[1].Value), true));
				}
				else
				{
					information.Add(new ValueTuple<string, Version, bool>(filename, new Version(0, 12), false));
				}
			}
			return information;
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x0053B540 File Offset: 0x00539740
		private static void RemoveSkippablePreview(string repo)
		{
			foreach (string filename in Directory.GetFiles(repo, "*.tmod", SearchOption.AllDirectories))
			{
				Match match = ModOrganizer.PublishFolderMetadata.Match(filename);
				if (match.Success)
				{
					Version checkVersion = new Version(match.Groups[1].Value);
					if (checkVersion > BuildInfo.stableVersion && checkVersion < BuildInfo.tMLVersion.MajorMinor())
					{
						Directory.Delete(Path.GetDirectoryName(filename), true);
					}
				}
			}
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x0053B5C5 File Offset: 0x005397C5
		internal static bool CanDeleteFrom(ModLocation location)
		{
			return location == ModLocation.Local || location == ModLocation.Workshop;
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x0053B5D0 File Offset: 0x005397D0
		internal static void DeleteMod(LocalMod tmod)
		{
			if (tmod.location == ModLocation.Local)
			{
				File.Delete(tmod.modFile.path);
			}
			else
			{
				if (tmod.location != ModLocation.Workshop)
				{
					throw new InvalidOperationException("Cannot delete mod from " + tmod.location.ToString());
				}
				string parentDir = ModOrganizer.GetParentDir(tmod.modFile.path);
				FoundWorkshopEntryInfo info;
				if (!ModOrganizer.TryReadManifest(parentDir, out info))
				{
					Logging.tML.Error("Failed to read manifest for workshop mod " + tmod.Name + " @ " + parentDir);
					return;
				}
				SteamedWraps.UninstallWorkshopItem(new PublishedFileId_t(info.workshopEntryId), parentDir);
			}
			ModOrganizer.LocalModsChanged(new HashSet<string>
			{
				tmod.Name
			}, true);
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x0053B68B File Offset: 0x0053988B
		internal static bool TryReadManifest(string parentDir, out FoundWorkshopEntryInfo info)
		{
			info = null;
			return ModOrganizer.IsModFromSteam(parentDir) && AWorkshopEntry.TryReadingManifest(parentDir + Path.DirectorySeparatorChar.ToString() + "workshop.json", out info);
		}

		// Token: 0x0600304A RID: 12362 RVA: 0x0053B6B8 File Offset: 0x005398B8
		internal static string GetParentDir(string tmodPath)
		{
			string parentDir = Directory.GetParent(tmodPath).ToString();
			if (!ModOrganizer.IsModFromSteam(parentDir))
			{
				return parentDir;
			}
			if (ModOrganizer.PublishFolderMetadata.Match(parentDir + Path.DirectorySeparatorChar.ToString()).Success)
			{
				parentDir = Directory.GetParent(parentDir).ToString();
			}
			return parentDir;
		}

		// Token: 0x0600304B RID: 12363 RVA: 0x0053B70C File Offset: 0x0053990C
		internal static string GetDisplayNameCleanFromLocalModsOrDefaultToModName(string modname)
		{
			IEnumerable<LocalMod> localMods = from m in ModOrganizer.AllFoundMods
			where string.Equals(modname, m.Name)
			select m;
			if (!localMods.Any<LocalMod>())
			{
				return modname;
			}
			return localMods.FirstOrDefault<LocalMod>().DisplayNameClean;
		}

		// Token: 0x04001CF3 RID: 7411
		internal static string modPath = Path.Combine(Main.SavePath, "Mods");

		// Token: 0x04001CF4 RID: 7412
		internal static string commandLineModPack;

		// Token: 0x04001CF5 RID: 7413
		private static Dictionary<string, LocalMod> modsDirCache = new Dictionary<string, LocalMod>();

		// Token: 0x04001CF6 RID: 7414
		private static HashSet<string> readFailures = new HashSet<string>();

		// Token: 0x04001CF7 RID: 7415
		internal static string lastLaunchedModsFilePath = Path.Combine(Main.SavePath, "LastLaunchedMods.txt");

		// Token: 0x04001CF8 RID: 7416
		[TupleElementNames(new string[]
		{
			"ModName",
			"previousVersion"
		})]
		internal static List<ValueTuple<string, Version>> modsThatUpdatedSinceLastLaunch = new List<ValueTuple<string, Version>>();

		// Token: 0x04001CF9 RID: 7417
		internal static WorkshopHelper.UGCBased.Downloader WorkshopFileFinder = new WorkshopHelper.UGCBased.Downloader();

		// Token: 0x04001CFA RID: 7418
		internal static string ModPackActive = null;

		// Token: 0x04001CFD RID: 7421
		private static readonly Regex PublishFolderMetadata = new Regex("[/|\\\\]([0-9]{4}[.][0-9]{1,2})[/|\\\\]");

		// Token: 0x02000AC8 RID: 2760
		// (Invoke) Token: 0x06005A32 RID: 23090
		internal delegate void LocalModsChangedDelegate(HashSet<string> modSlugs, bool isDeletion);

		// Token: 0x02000AC9 RID: 2761
		private enum SearchFolders
		{

		}

		// Token: 0x02000ACA RID: 2762
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006E16 RID: 28182
			public static Func<LocalMod, bool> <0>__ServerRequiresDifferentVersion;

			// Token: 0x04006E17 RID: 28183
			public static Func<LocalMod, bool> <1>__SkipModForPreviewNotPlayable;
		}
	}
}
