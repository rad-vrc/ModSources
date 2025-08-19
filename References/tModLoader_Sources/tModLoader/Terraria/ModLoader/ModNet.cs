using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using log4net;
using Newtonsoft.Json;
using Terraria.Localization;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI;
using Terraria.Net;
using Terraria.Net.Sockets;
using Terraria.UI;

namespace Terraria.ModLoader
{
	// Token: 0x020001B8 RID: 440
	public static class ModNet
	{
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x004E5C2B File Offset: 0x004E3E2B
		// (set) Token: 0x060021D5 RID: 8661 RVA: 0x004E5C32 File Offset: 0x004E3E32
		[Obsolete("No longer supported")]
		public static bool AllowVanillaClients { get; internal set; }

		// Token: 0x060021D6 RID: 8662 RVA: 0x004E5C3A File Offset: 0x004E3E3A
		public static bool IsModdedClient(int i)
		{
			return ModNet.isModdedClient[i];
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x004E5C43 File Offset: 0x004E3E43
		public static Mod GetMod(int netID)
		{
			if (netID < 0 || netID >= ModNet.netMods.Length)
			{
				return null;
			}
			return ModNet.netMods[netID];
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060021D8 RID: 8664 RVA: 0x004E5C5C File Offset: 0x004E3E5C
		public static int NetModCount
		{
			get
			{
				return ModNet.netMods.Length;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x004E5C65 File Offset: 0x004E3E65
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x004E5C6C File Offset: 0x004E3E6C
		internal static INetDiagnosticsUI ModNetDiagnosticsUI { get; private set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060021DB RID: 8667 RVA: 0x004E5C74 File Offset: 0x004E3E74
		[Nullable(2)]
		private static Version StableNetVersion { [NullableContext(2)] get; } = (!BuildInfo.IsStable && !BuildInfo.IsPreview) ? null : ((ModNet.IncompatiblePatchVersion.MajorMinor() == BuildInfo.tMLVersion.MajorMinor()) ? ModNet.IncompatiblePatchVersion : BuildInfo.tMLVersion.MajorMinorBuild());

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060021DC RID: 8668 RVA: 0x004E5C7B File Offset: 0x004E3E7B
		internal static string NetVersionString { get; }

		// Token: 0x060021DD RID: 8669 RVA: 0x004E5C84 File Offset: 0x004E3E84
		static ModNet()
		{
			string versionedName = BuildInfo.versionedName;
			string str;
			if (!(ModNet.StableNetVersion != null))
			{
				str = "";
			}
			else
			{
				string str2 = "!";
				Version stableNetVersion = ModNet.StableNetVersion;
				str = str2 + ((stableNetVersion != null) ? stableNetVersion.ToString() : null);
			}
			ModNet.NetVersionString = versionedName + str;
			ModNet.NetReloadKeepAliveTimer = new Stopwatch();
			ModNet.ReadUnderflowBypass = false;
			ModNet.DetailedLogging = Program.LaunchParameters.ContainsKey("-detailednetlog");
			ModNet.NetLog = LogManager.GetLogger("Network");
			if (Main.dedServ && ModNet.StableNetVersion != null)
			{
				ILog tML = Logging.tML;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Network compatibility version is ");
				defaultInterpolatedStringHandler.AppendFormatted<Version>(ModNet.StableNetVersion);
				tML.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
			}
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x004E5DD8 File Offset: 0x004E3FD8
		internal static bool IsClientCompatible(string clientVersion, out bool isModded, out string kickMsg)
		{
			kickMsg = null;
			isModded = clientVersion.StartsWith("tModLoader");
			if (clientVersion == ModNet.NetVersionString)
			{
				return true;
			}
			string[] split = clientVersion.Split('!', StringSplitOptions.None);
			Version netVer;
			if (ModNet.StableNetVersion != null && split.Length == 2 && Version.TryParse(split[1], out netVer) && netVer == ModNet.StableNetVersion)
			{
				Logging.tML.Debug("Client has " + split[0] + ", assuming net compatibility");
				return true;
			}
			kickMsg = (isModded ? ("You are on " + split[0] + ", server is on " + BuildInfo.versionedName) : "You cannot connect to a tModLoader Server with an unmodded client");
			return false;
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x004E5E80 File Offset: 0x004E4080
		internal static void AssignNetIDs()
		{
			ModNet.netMods = (from mod in ModLoader.Mods
			where mod.Side != ModSide.Server
			select mod).ToArray<Mod>();
			short i = 0;
			while ((int)i < ModNet.netMods.Length)
			{
				ModNet.netMods[(int)i].netID = i;
				i += 1;
			}
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x004E5EE0 File Offset: 0x004E40E0
		internal static void Unload()
		{
			ModNet.netMods = null;
			ModNet.SetModNetDiagnosticsUI(ModLoader.Mods);
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x004E5EF4 File Offset: 0x004E40F4
		internal static void SyncMods(int clientIndex)
		{
			ModPacket p = new ModPacket(251, 256);
			List<Mod> syncMods = (from mod in ModLoader.Mods
			where mod.Side == ModSide.Both
			select mod).ToList<Mod>();
			ModNet.AddNoSyncDeps(syncMods);
			p.Write(syncMods.Count);
			foreach (Mod mod2 in syncMods)
			{
				p.Write(mod2.Name);
				p.Write(mod2.Version.ToString());
				p.Write(mod2.File.Hash);
				ModNet.SendServerConfigs(p, mod2);
			}
			p.Send(clientIndex, -1);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x004E5FCC File Offset: 0x004E41CC
		private static void AddNoSyncDeps(List<Mod> syncMods)
		{
			Queue<Mod> queue = new Queue<Mod>(from m in syncMods
			where m.Side == ModSide.Both
			select m);
			while (queue.Count > 0)
			{
				foreach (Mod dep in AssemblyManager.GetDependencies(queue.Dequeue()))
				{
					if (dep.Side == ModSide.NoSync && !syncMods.Contains(dep))
					{
						syncMods.Add(dep);
						queue.Enqueue(dep);
					}
				}
			}
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x004E6070 File Offset: 0x004E4270
		private static void SendServerConfigs(ModPacket p, Mod mod)
		{
			List<ModConfig> configs;
			if (!ConfigManager.Configs.TryGetValue(mod, out configs))
			{
				p.Write(0);
				return;
			}
			ModConfig[] serverConfigs = (from x in configs
			where x.Mode == ConfigScope.ServerSide
			select x).ToArray<ModConfig>();
			p.Write(serverConfigs.Length);
			foreach (ModConfig config in serverConfigs)
			{
				string json = JsonConvert.SerializeObject(config, ConfigManager.serializerSettingsCompact);
				ILog tML = Logging.tML;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 3);
				defaultInterpolatedStringHandler.AppendLiteral("Sending Server Config ");
				defaultInterpolatedStringHandler.AppendFormatted(config.Mod.Name);
				defaultInterpolatedStringHandler.AppendLiteral(":");
				defaultInterpolatedStringHandler.AppendFormatted(config.Name);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(json);
				tML.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
				p.Write(config.Name);
				p.Write(json);
			}
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x004E6170 File Offset: 0x004E4370
		internal static void SyncClientMods(BinaryReader reader)
		{
			bool needsReload;
			if (!ModNet.SyncClientMods(reader, out needsReload))
			{
				return;
			}
			if (ModNet.downloadQueue.Count > 0)
			{
				ModNet.DownloadNextMod();
				return;
			}
			ModNet.OnModsDownloaded(needsReload);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x004E61A4 File Offset: 0x004E43A4
		internal static bool SyncClientMods(BinaryReader reader, out bool needsReload)
		{
			Main.statusText = Language.GetTextValue("tModLoader.MPSyncingMods");
			Mod[] clientMods = ModLoader.Mods;
			LocalMod[] modFiles = ModOrganizer.FindAllMods();
			needsReload = false;
			ModNet.SyncModHeaders.Clear();
			ModNet.downloadQueue.Clear();
			ModNet.pendingConfigs.Clear();
			List<ModNet.ModHeader> blockedList = new List<ModNet.ModHeader>();
			List<ReloadRequiredExplanation> reloadRequiredExplanationEntries = new List<ReloadRequiredExplanation>();
			List<string> toEnable = new List<string>();
			int i = reader.ReadInt32();
			for (int j = 0; j < i; j++)
			{
				ModNet.ModHeader header = new ModNet.ModHeader(reader.ReadString(), new Version(reader.ReadString()), reader.ReadBytes(20));
				ModNet.SyncModHeaders.Add(header);
				int configCount = reader.ReadInt32();
				for (int c = 0; c < configCount; c++)
				{
					ModNet.pendingConfigs.Add(new ModNet.NetConfig(header.name, reader.ReadString(), reader.ReadString()));
				}
				Mod clientMod = clientMods.SingleOrDefault((Mod m) => m.Name == header.name);
				if (clientMod == null || !header.Matches(clientMod.File))
				{
					needsReload = true;
					LocalMod matching = modFiles.FirstOrDefault((LocalMod mod) => header.Matches(mod.modFile));
					if (matching != null)
					{
						reloadRequiredExplanationEntries.Add(ModNet.MakeEnableOrVersionSwitchExplanation(header, clientMod, matching));
						if (clientMod == null)
						{
							toEnable.Add(matching.Name);
						}
					}
					else if (ModNet.downloadModsFromServers)
					{
						ModNet.downloadQueue.Enqueue(header);
						reloadRequiredExplanationEntries.Add(ModNet.MakeDownloadModExplanation(modFiles, header, clientMod));
					}
					else
					{
						blockedList.Add(header);
					}
				}
			}
			Logging.tML.Debug("Server mods:\n\t\t" + string.Join<ModNet.ModHeader>("\n\t\t", ModNet.SyncModHeaders));
			Logging.tML.Debug("Download queue: " + string.Join<ModNet.ModHeader>(", ", ModNet.downloadQueue));
			if (ModNet.pendingConfigs.Any<ModNet.NetConfig>())
			{
				Logging.tML.Debug("Configs:\n\t\t" + string.Join<ModNet.NetConfig>("\n\t\t", ModNet.pendingConfigs));
			}
			IEnumerable<Mod> clientSideMods = from x in clientMods
			where x.Side == ModSide.Client
			select x;
			if (clientSideMods.Any<Mod>())
			{
				Logging.tML.Debug("Client Side mods: " + string.Join(", ", from x in clientSideMods
				select x.Name + " (" + x.DisplayNameClean + ")"));
			}
			IEnumerable<string> toDisable = (from m in clientMods
			where m.Side == ModSide.Both
			select m.Name).Except(from h in ModNet.SyncModHeaders
			select h.name);
			using (IEnumerator<string> enumerator = toDisable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string name = enumerator.Current;
					needsReload = true;
					reloadRequiredExplanationEntries.Add(new ReloadRequiredExplanation(4, name, (from mod in modFiles
					where mod.Name == name
					orderby mod.Version descending
					select mod).FirstOrDefault<LocalMod>(), Language.GetTextValue("tModLoader.ReloadRequiredExplanationDisable", "FFA07A")));
				}
			}
			if (blockedList.Count > 0)
			{
				string msg = Language.GetTextValue("tModLoader.MPServerModsCantDownload");
				msg += (ModNet.downloadModsFromServers ? Language.GetTextValue("tModLoader.MPServerModsCantDownloadReasonSigned") : Language.GetTextValue("tModLoader.MPServerModsCantDownloadReasonAutomaticDownloadDisabled"));
				msg = msg + ".\n" + Language.GetTextValue("tModLoader.MPServerModsCantDownloadChangeSettingsHint") + "\n";
				foreach (ModNet.ModHeader mod2 in blockedList)
				{
					string str = msg;
					string str2 = "\n    ";
					ModNet.ModHeader modHeader = mod2;
					msg = str + str2 + ((modHeader != null) ? modHeader.ToString() : null);
				}
				Logging.tML.Warn(msg);
				Interface.errorMessage.Show(msg, 0, null, "", false, false, null);
				return false;
			}
			List<Mod> modsWithRequiredConfigReload;
			if (ModNet.AnyModNeedsReloadFromNetConfigsCheckOnly(out modsWithRequiredConfigReload))
			{
				needsReload = true;
				using (List<Mod>.Enumerator enumerator3 = modsWithRequiredConfigReload.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						Mod modWithChangedConfig = enumerator3.Current;
						reloadRequiredExplanationEntries.Add(new ReloadRequiredExplanation(5, modWithChangedConfig.Name, modFiles.FirstOrDefault((LocalMod mod) => mod.Name == modWithChangedConfig.Name), Language.GetTextValue("tModLoader.ReloadRequiredExplanationConfigChanged", "DDA0DD")));
					}
				}
			}
			if (needsReload)
			{
				ModNet.NetReloadKeepAliveTimer.Restart();
				string continueButtonText = Language.GetTextValue("tModLoader." + ((ModNet.downloadQueue.Count > 0) ? "ReloadRequiredDownloadAndContinue" : "ReloadRequiredReloadAndContinue"));
				Interface.serverModsDifferMessage.Show(Language.GetTextValue("tModLoader.ReloadRequiredServerJoinMessage", continueButtonText), 0, null, continueButtonText, delegate
				{
					foreach (string modName in toDisable)
					{
						ModLoader.DisableMod(modName);
					}
					foreach (string modName2 in toEnable)
					{
						ModLoader.EnableMod(modName2);
					}
					if (ModNet.downloadQueue.Count > 0)
					{
						ModNet.DownloadNextMod();
						return;
					}
					ModNet.OnModsDownloaded(true);
				}, Language.GetTextValue("tModLoader.ModConfigBack"), delegate
				{
					Netplay.InvalidateAllOngoingIPSetAttempts();
					Netplay.Disconnect = true;
					Netplay.Connection.Socket.Close();
					if (Main.tServer != null)
					{
						try
						{
							Main.tServer.Kill();
							Main.tServer = null;
						}
						catch
						{
						}
					}
				}, reloadRequiredExplanationEntries);
				return false;
			}
			foreach (ModNet.NetConfig pendingConfig in ModNet.pendingConfigs)
			{
				JsonConvert.PopulateObject(pendingConfig.json, ConfigManager.GetConfig(pendingConfig), ConfigManager.serializerSettingsCompact);
			}
			foreach (ModNet.NetConfig netConfig in ModNet.pendingConfigs)
			{
				ConfigManager.GetConfig(netConfig).OnChanged();
			}
			return true;
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x004E67FC File Offset: 0x004E49FC
		private static ReloadRequiredExplanation MakeEnableOrVersionSwitchExplanation(ModNet.ModHeader header, Mod clientMod, LocalMod matching)
		{
			if (clientMod == null)
			{
				return new ReloadRequiredExplanation(3, header.name, matching, Language.GetTextValue("tModLoader.ReloadRequiredExplanationEnable", "98FB98"));
			}
			if (clientMod.Version > header.version)
			{
				return new ReloadRequiredExplanation(2, header.name, matching, Language.GetTextValue("tModLoader.ReloadRequiredExplanationSwitchVersionDowngrade", "FFFACD", header.version, clientMod.Version));
			}
			return new ReloadRequiredExplanation(2, header.name, matching, Language.GetTextValue("tModLoader.ReloadRequiredExplanationSwitchVersionUpgrade", "FFFACD", header.version, clientMod.Version));
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x004E6890 File Offset: 0x004E4A90
		private static ReloadRequiredExplanation MakeDownloadModExplanation(LocalMod[] modFiles, ModNet.ModHeader header, Mod clientMod)
		{
			LocalMod localMod = modFiles.FirstOrDefault((LocalMod mod) => header.MatchesNameAndVersion(mod.modFile));
			if (localMod != null)
			{
				return new ReloadRequiredExplanation(1, header.name, localMod, Language.GetTextValue("tModLoader.ReloadRequiredExplanationDownloadHashDiffers", "00BFFF", header.version, string.Concat(from b in RuntimeHelpers.GetSubArray<byte>(header.hash, Range.EndAt(4))
				select b.ToString("x2"))));
			}
			LocalMod localModMatchingNameOnly = (from mod in modFiles
			where mod.Name == header.name
			orderby mod.Version descending
			select mod).FirstOrDefault<LocalMod>();
			if (localModMatchingNameOnly == null)
			{
				return new ReloadRequiredExplanation(1, header.name, null, Language.GetTextValue("tModLoader.ReloadRequiredExplanationDownload", "00BFFF", header.version));
			}
			if (clientMod == null)
			{
				return new ReloadRequiredExplanation(1, header.name, localModMatchingNameOnly, Language.GetTextValue("tModLoader.ReloadRequiredExplanationDownload", "00BFFF", header.version));
			}
			if (clientMod.Version > header.version)
			{
				bool downgradeIsTemporary = true;
				if (!(from mod in modFiles
				where mod.Name == header.name && mod.location == ModLocation.Workshop
				select mod).Any<LocalMod>())
				{
					downgradeIsTemporary = false;
				}
				return new ReloadRequiredExplanation(1, header.name, localModMatchingNameOnly, Language.GetTextValue(downgradeIsTemporary ? "tModLoader.ReloadRequiredExplanationDownloadDowngradeTemporary" : "tModLoader.ReloadRequiredExplanationDownloadDowngrade", "00BFFF", header.version, clientMod.Version));
			}
			return new ReloadRequiredExplanation(1, header.name, localModMatchingNameOnly, Language.GetTextValue("tModLoader.ReloadRequiredExplanationDownloadUpgrade", "00BFFF", header.version, clientMod.Version));
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x004E6A74 File Offset: 0x004E4C74
		internal static bool AnyModNeedsReloadFromNetConfigsCheckOnly(out List<Mod> modsWithRequiredConfigReload)
		{
			modsWithRequiredConfigReload = new List<Mod>();
			using (List<ModNet.NetConfig>.Enumerator enumerator = ModNet.pendingConfigs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ModNet.NetConfig pendingConfig = enumerator.Current;
					if (!modsWithRequiredConfigReload.Any((Mod x) => x.Name == pendingConfig.modname) && ModLoader.HasMod(pendingConfig.modname))
					{
						ModConfig currentConfigClone = ConfigManager.GeneratePopulatedClone(ConfigManager.GetConfig(pendingConfig));
						JsonConvert.PopulateObject(pendingConfig.json, currentConfigClone, ConfigManager.serializerSettingsCompact);
						Mod mod = ModLoader.GetMod(pendingConfig.modname);
						ModConfig loadTimeConfig = ConfigManager.GetLoadTimeConfig(mod, pendingConfig.configname);
						if (loadTimeConfig.NeedsReload(currentConfigClone))
						{
							modsWithRequiredConfigReload.Add(mod);
							ILog tML = Logging.tML;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(51, 3);
							defaultInterpolatedStringHandler.AppendFormatted(pendingConfig.modname);
							defaultInterpolatedStringHandler.AppendLiteral(":");
							defaultInterpolatedStringHandler.AppendFormatted(pendingConfig.configname);
							defaultInterpolatedStringHandler.AppendLiteral(" (");
							defaultInterpolatedStringHandler.AppendFormatted<LocalizedText>(loadTimeConfig.DisplayName);
							defaultInterpolatedStringHandler.AppendLiteral(") would require a mod reload to join this server");
							tML.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
						}
					}
				}
			}
			return modsWithRequiredConfigReload.Any<Mod>();
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x004E6BE8 File Offset: 0x004E4DE8
		private static void DownloadNextMod()
		{
			ModNet.downloadingMod = ModNet.downloadQueue.Dequeue();
			ModNet.downloadingFile = null;
			ModPacket modPacket = new ModPacket(252, 256);
			modPacket.Write(ModNet.downloadingMod.name);
			modPacket.Send(-1, -1);
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x004E6C28 File Offset: 0x004E4E28
		internal static void SendMod(string modName, int toClient)
		{
			Mod mod = ModLoader.GetMod(modName);
			if (mod.Side == ModSide.Server)
			{
				return;
			}
			FileStream fs = File.OpenRead(mod.File.path);
			ModPacket modPacket = new ModPacket(252, 256);
			modPacket.Write(mod.DisplayName);
			modPacket.Write(fs.Length);
			modPacket.Send(toClient, -1);
			byte[] buf = new byte[16384];
			int count;
			while ((count = fs.Read(buf, 0, buf.Length)) > 0)
			{
				ModPacket modPacket2 = new ModPacket(252, 16387);
				modPacket2.Write(buf, 0, count);
				modPacket2.Send(toClient, -1);
			}
			fs.Close();
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x004E6CC8 File Offset: 0x004E4EC8
		internal static void ReceiveMod(BinaryReader reader)
		{
			if (ModNet.downloadingMod == null)
			{
				return;
			}
			try
			{
				if (ModNet.downloadingFile == null)
				{
					UIProgress progress = Interface.progress;
					string displayText = reader.ReadString();
					int gotoMenu = 0;
					Action cancel;
					if ((cancel = ModNet.<>O.<0>__CancelDownload) == null)
					{
						cancel = (ModNet.<>O.<0>__CancelDownload = new Action(ModNet.CancelDownload));
					}
					progress.Show(displayText, gotoMenu, cancel);
					Mod mod;
					if (ModLoader.TryGetMod(ModNet.downloadingMod.name, out mod))
					{
						mod.Close();
					}
					ModNet.downloadingLength = reader.ReadInt64();
					ILog tML = Logging.tML;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(20, 2);
					defaultInterpolatedStringHandler.AppendLiteral("Downloading: ");
					defaultInterpolatedStringHandler.AppendFormatted(ModNet.downloadingMod.name);
					defaultInterpolatedStringHandler.AppendLiteral(" ");
					defaultInterpolatedStringHandler.AppendFormatted<long>(ModNet.downloadingLength);
					defaultInterpolatedStringHandler.AppendLiteral(" bytes");
					tML.Debug(defaultInterpolatedStringHandler.ToStringAndClear());
					ModNet.downloadingFile = new FileStream(ModNet.downloadingMod.path, FileMode.Create);
				}
				else
				{
					byte[] bytes = reader.ReadBytes((int)Math.Min(ModNet.downloadingLength - ModNet.downloadingFile.Position, 16384L));
					ModNet.downloadingFile.Write(bytes, 0, bytes.Length);
					Interface.progress.Progress = (float)ModNet.downloadingFile.Position / (float)ModNet.downloadingLength;
					if (ModNet.downloadingFile.Position == ModNet.downloadingLength)
					{
						ModNet.downloadingFile.Close();
						ModCompile.recentlyBuiltModCheckTimeCutoff = DateTime.Now + TimeSpan.FromSeconds(10.0);
						TmodFile mod2 = new TmodFile(ModNet.downloadingMod.path, null, null);
						using (mod2.Open())
						{
						}
						if (!ModNet.downloadingMod.Matches(mod2))
						{
							throw new Exception(Language.GetTextValue("tModLoader.MPErrorModHashMismatch"));
						}
						ModLoader.EnableMod(mod2.Name);
						if (ModNet.downloadQueue.Count > 0)
						{
							ModNet.DownloadNextMod();
						}
						else
						{
							ModNet.OnModsDownloaded(true);
						}
					}
				}
			}
			catch (Exception e)
			{
				try
				{
					FileStream fileStream = ModNet.downloadingFile;
					if (fileStream != null)
					{
						fileStream.Close();
					}
					File.Delete(ModNet.downloadingMod.path);
				}
				catch (Exception exc2)
				{
					Logging.tML.Error("Unknown error during mod sync", exc2);
				}
				string msg = Language.GetTextValue("tModLoader.MPErrorModDownloadError", ModNet.downloadingMod.name);
				Logging.tML.Error(msg, e);
				UIErrorMessage errorMessage = Interface.errorMessage;
				string str = msg;
				Exception ex = e;
				errorMessage.Show(str + ((ex != null) ? ex.ToString() : null), 0, null, "", false, false, null);
				Netplay.Disconnect = true;
				ModNet.downloadingMod = null;
			}
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x004E6F84 File Offset: 0x004E5184
		private static void CancelDownload()
		{
			try
			{
				FileStream fileStream = ModNet.downloadingFile;
				if (fileStream != null)
				{
					fileStream.Close();
				}
				File.Delete(ModNet.downloadingMod.path);
			}
			catch
			{
			}
			ModNet.downloadingMod = null;
			Netplay.Disconnect = true;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x004E6FD4 File Offset: 0x004E51D4
		private static void OnModsDownloaded(bool needsReload)
		{
			if (needsReload)
			{
				Main.netMode = 0;
				ModLoader.OnSuccessfulLoad = ModNet.NetReload();
				ModLoader.Reload();
				return;
			}
			Main.netMode = 1;
			ModNet.downloadingMod = null;
			ModNet.netMods = null;
			Mod[] mods = ModLoader.Mods;
			for (int i = 0; i < mods.Length; i++)
			{
				mods[i].netID = -1;
			}
			new ModPacket(251, 256).Send(-1, -1);
		}

		// Token: 0x060021EE RID: 8686 RVA: 0x004E7040 File Offset: 0x004E5240
		internal static Action NetReload()
		{
			string path = Main.ActivePlayerFileData.Path;
			bool isCloudSave = Main.ActivePlayerFileData.IsCloudSave;
			ModNet.NetReloadActive = true;
			ModNet.NetReloadKeepAliveTimer.Restart();
			return delegate()
			{
				ModNet.NetReloadActive = false;
				Player.GetFileData(path, isCloudSave).SetAsActive();
				Main.player[Main.myPlayer].hostile = false;
				Main.clientPlayer = Main.player[Main.myPlayer].clientClone();
				if (!Netplay.Connection.Socket.IsConnected())
				{
					Main.menuMode = 15;
					Logging.tML.Error("Disconnected from server during reload.");
					Main.statusText = "Disconnected from server during reload.";
					return;
				}
				Main.menuMode = 10;
				Main.statusText = "Reload complete, joining...";
				ModNet.OnModsDownloaded(false);
			};
		}

		// Token: 0x060021EF RID: 8687 RVA: 0x004E7090 File Offset: 0x004E5290
		internal static bool ServerRequiresDifferentVersion(LocalMod mod)
		{
			return ModNet.NetReloadActive && ModNet.SyncModHeaders.Any((ModNet.ModHeader h) => h.name == mod.Name && !h.Matches(mod.modFile));
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x004E70CC File Offset: 0x004E52CC
		internal static void SendNetIDs(int toClient)
		{
			ModPacket p = new ModPacket(250, 256);
			p.Write(ModNet.netMods.Length);
			foreach (Mod mod in ModNet.netMods)
			{
				p.Write(mod.Name);
			}
			p.Write(Player.MaxBuffs);
			p.Send(toClient, -1);
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x004E7130 File Offset: 0x004E5330
		private static void ReadNetIDs(BinaryReader reader)
		{
			Mod[] mods = ModLoader.Mods;
			List<Mod> list = new List<Mod>();
			int i = reader.ReadInt32();
			short j = 0;
			while ((int)j < i)
			{
				string name = reader.ReadString();
				Mod mod2 = mods.SingleOrDefault((Mod m) => m.Name == name);
				list.Add(mod2);
				if (mod2 != null)
				{
					mod2.netID = j;
				}
				j += 1;
			}
			ModNet.netMods = list.ToArray();
			ModNet.SetModNetDiagnosticsUI(from mod in ModNet.netMods
			where mod != null
			select mod);
			int serverMaxBuffs = reader.ReadInt32();
			if (serverMaxBuffs != Player.MaxBuffs)
			{
				Netplay.Disconnect = true;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(93, 2);
				defaultInterpolatedStringHandler.AppendLiteral("The server expects Player.MaxBuffs of ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(serverMaxBuffs);
				defaultInterpolatedStringHandler.AppendLiteral("\nbut this client reports ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(Player.MaxBuffs);
				defaultInterpolatedStringHandler.AppendLiteral(".\nSome mod is behaving poorly.");
				Main.statusText = defaultInterpolatedStringHandler.ToStringAndClear();
			}
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x004E723C File Offset: 0x004E543C
		internal static void HandleModPacket(BinaryReader reader, int whoAmI, int length)
		{
			if (ModNet.netMods == null)
			{
				ModNet.ReadNetIDs(reader);
				return;
			}
			short id = (ModNet.NetModCount < 256) ? ((short)reader.ReadByte()) : reader.ReadInt16();
			int start = (int)reader.BaseStream.Position;
			int actualLength = length - 1 - ((ModNet.NetModCount < 256) ? 1 : 2);
			try
			{
				ModNet.ReadUnderflowBypass = false;
				Mod mod = ModNet.GetMod((int)id);
				if (mod != null)
				{
					mod.HandlePacket(reader, whoAmI);
				}
				if (!ModNet.ReadUnderflowBypass && reader.BaseStream.Position - (long)start != (long)actualLength)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(52, 3);
					defaultInterpolatedStringHandler.AppendLiteral("Read underflow ");
					defaultInterpolatedStringHandler.AppendFormatted<long>(reader.BaseStream.Position - (long)start);
					defaultInterpolatedStringHandler.AppendLiteral(" of ");
					defaultInterpolatedStringHandler.AppendFormatted<int>(actualLength);
					defaultInterpolatedStringHandler.AppendLiteral(" bytes caused by ");
					Mod mod2 = ModNet.GetMod((int)id);
					defaultInterpolatedStringHandler.AppendFormatted(((mod2 != null) ? mod2.Name : null) ?? "Unknown mod");
					defaultInterpolatedStringHandler.AppendLiteral(" in HandlePacket");
					throw new IOException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
			}
			catch
			{
			}
			if (Main.netMode == 1 && id >= 0)
			{
				ModNet.ModNetDiagnosticsUI.CountReadMessage((int)id, length);
			}
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x004E7380 File Offset: 0x004E5580
		internal static void SetModNetDiagnosticsUI(IEnumerable<Mod> mods)
		{
			INetDiagnosticsUI modNetDiagnosticsUI;
			if (!Main.dedServ)
			{
				INetDiagnosticsUI netDiagnosticsUI = new UIModNetDiagnostics(mods);
				modNetDiagnosticsUI = netDiagnosticsUI;
			}
			else
			{
				INetDiagnosticsUI netDiagnosticsUI = new EmptyDiagnosticsUI();
				modNetDiagnosticsUI = netDiagnosticsUI;
			}
			ModNet.ModNetDiagnosticsUI = modNetDiagnosticsUI;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x004E73AA File Offset: 0x004E55AA
		internal static bool HijackGetData(ref byte messageType, ref BinaryReader reader, int playerNumber)
		{
			return ModNet.netMods != null && SystemLoader.HijackGetData(ref messageType, ref reader, playerNumber);
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x004E73C0 File Offset: 0x004E55C0
		internal static bool HijackSendData(int whoAmI, int msgType, int remoteClient, int ignoreClient, NetworkText text, int number, float number2, float number3, float number4, int number5, int number6, int number7)
		{
			return SystemLoader.HijackSendData(whoAmI, msgType, remoteClient, ignoreClient, text, number, number2, number3, number4, number5, number6, number7);
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x004E73E6 File Offset: 0x004E55E6
		private static ILog NetLog { get; }

		// Token: 0x060021F7 RID: 8695 RVA: 0x004E73F0 File Offset: 0x004E55F0
		private static string Identifier(int whoAmI)
		{
			if (!Main.dedServ)
			{
				return "";
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			if (whoAmI >= 0 && whoAmI < 256)
			{
				RemoteClient client = Netplay.Clients[whoAmI];
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 3);
				defaultInterpolatedStringHandler.AppendLiteral("[");
				defaultInterpolatedStringHandler.AppendFormatted<int>(whoAmI);
				defaultInterpolatedStringHandler.AppendLiteral("][");
				ISocket socket = client.Socket;
				string value;
				if (socket == null)
				{
					value = null;
				}
				else
				{
					RemoteAddress remoteAddress = socket.GetRemoteAddress();
					value = ((remoteAddress != null) ? remoteAddress.GetFriendlyName() : null);
				}
				defaultInterpolatedStringHandler.AppendFormatted(value);
				defaultInterpolatedStringHandler.AppendLiteral(" (");
				defaultInterpolatedStringHandler.AppendFormatted(client.Name);
				defaultInterpolatedStringHandler.AppendLiteral(")] ");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}
			if (whoAmI == -1)
			{
				return "[*] ";
			}
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 1);
			defaultInterpolatedStringHandler.AppendLiteral("[");
			defaultInterpolatedStringHandler.AppendFormatted<int>(whoAmI);
			defaultInterpolatedStringHandler.AppendLiteral("] ");
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x004E74DC File Offset: 0x004E56DC
		private static string Identifier(RemoteAddress addr)
		{
			if (!Main.dedServ || addr == null)
			{
				return "";
			}
			RemoteClient client = Netplay.Clients.SingleOrDefault(delegate(RemoteClient c)
			{
				ISocket socket = c.Socket;
				return ((socket != null) ? socket.GetRemoteAddress() : null) == addr;
			});
			if (client != null)
			{
				return ModNet.Identifier(client.Id);
			}
			return "[" + addr.GetFriendlyName() + "] ";
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x004E754B File Offset: 0x004E574B
		public static void Log(int whoAmI, string s)
		{
			ModNet.Log(ModNet.Identifier(whoAmI) + s);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x004E755E File Offset: 0x004E575E
		public static void Log(RemoteAddress addr, string s)
		{
			ModNet.Log(ModNet.Identifier(addr) + s);
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x004E7571 File Offset: 0x004E5771
		public static void Log(string s)
		{
			ModNet.NetLog.Info(s);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x004E757E File Offset: 0x004E577E
		public static void Warn(int whoAmI, string s)
		{
			ModNet.Warn(ModNet.Identifier(whoAmI) + s);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x004E7591 File Offset: 0x004E5791
		public static void Warn(RemoteAddress addr, string s)
		{
			ModNet.Warn(ModNet.Identifier(addr) + s);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x004E75A4 File Offset: 0x004E57A4
		public static void Warn(string s)
		{
			ModNet.NetLog.Warn(s);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x004E75B1 File Offset: 0x004E57B1
		public static void Debug(int whoAmI, string s)
		{
			ModNet.Debug(ModNet.Identifier(whoAmI) + s);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x004E75C4 File Offset: 0x004E57C4
		public static void Debug(RemoteAddress addr, string s)
		{
			ModNet.Debug(ModNet.Identifier(addr) + s);
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x004E75D7 File Offset: 0x004E57D7
		public static void Debug(string s)
		{
			if (ModNet.DetailedLogging)
			{
				ModNet.NetLog.Info(s);
			}
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x004E75EB File Offset: 0x004E57EB
		public static void Error(int whoAmI, string s, Exception e = null)
		{
			ModNet.Error(ModNet.Identifier(whoAmI) + s, e);
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x004E75FF File Offset: 0x004E57FF
		public static void Error(RemoteAddress addr, string s, Exception e = null)
		{
			ModNet.Error(ModNet.Identifier(addr) + s, e);
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x004E7613 File Offset: 0x004E5813
		public static void Error(string s, Exception e = null)
		{
			ModNet.NetLog.Error(s, e);
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x004E7624 File Offset: 0x004E5824
		public static void LogSend(int toClient, int ignoreClient, string s, int len)
		{
			if (!ModNet.DetailedLogging)
			{
				return;
			}
			string str = s;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
			defaultInterpolatedStringHandler.AppendLiteral(", ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(len);
			s = str + defaultInterpolatedStringHandler.ToStringAndClear();
			if (ignoreClient != -1)
			{
				string str2 = s;
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
				defaultInterpolatedStringHandler.AppendLiteral(", ignore: ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(ignoreClient);
				s = str2 + defaultInterpolatedStringHandler.ToStringAndClear();
			}
			ModNet.Debug(toClient, s);
		}

		// Token: 0x040016ED RID: 5869
		internal static bool downloadModsFromServers = true;

		// Token: 0x040016EE RID: 5870
		internal static bool[] isModdedClient = new bool[256];

		// Token: 0x040016EF RID: 5871
		private static Mod[] netMods;

		// Token: 0x040016F0 RID: 5872
		internal static bool ShouldDrawModNetDiagnosticsUI = false;

		// Token: 0x040016F2 RID: 5874
		private static List<ModNet.ModHeader> SyncModHeaders = new List<ModNet.ModHeader>();

		// Token: 0x040016F3 RID: 5875
		private static Queue<ModNet.ModHeader> downloadQueue = new Queue<ModNet.ModHeader>();

		// Token: 0x040016F4 RID: 5876
		internal static List<ModNet.NetConfig> pendingConfigs = new List<ModNet.NetConfig>();

		// Token: 0x040016F5 RID: 5877
		private static ModNet.ModHeader downloadingMod;

		// Token: 0x040016F6 RID: 5878
		private static FileStream downloadingFile;

		// Token: 0x040016F7 RID: 5879
		private static long downloadingLength;

		/// <summary>
		/// Update every time a change is pushed to stable which is incompatible between server and clients. Ignored if not updated each month.
		/// </summary>
		// Token: 0x040016F8 RID: 5880
		private static Version IncompatiblePatchVersion = new Version(2022, 1, 1, 1);

		// Token: 0x040016FB RID: 5883
		internal const int CHUNK_SIZE = 16384;

		// Token: 0x040016FC RID: 5884
		internal static bool NetReloadActive;

		// Token: 0x040016FD RID: 5885
		internal static Stopwatch NetReloadKeepAliveTimer;

		// Token: 0x040016FE RID: 5886
		internal static bool ReadUnderflowBypass;

		// Token: 0x040016FF RID: 5887
		public static bool DetailedLogging;

		// Token: 0x0200092F RID: 2351
		internal class ModHeader
		{
			// Token: 0x060053D3 RID: 21459 RVA: 0x0069A4B4 File Offset: 0x006986B4
			public ModHeader(string name, Version version, byte[] hash)
			{
				this.name = name;
				this.version = version;
				this.hash = hash;
				this.path = Path.Combine(ModLoader.ModPath, name + ".tmod");
			}

			// Token: 0x060053D4 RID: 21460 RVA: 0x0069A4EC File Offset: 0x006986EC
			public bool Matches(TmodFile mod)
			{
				return this.name == mod.Name && this.version == mod.Version && this.hash.SequenceEqual(mod.Hash);
			}

			// Token: 0x060053D5 RID: 21461 RVA: 0x0069A527 File Offset: 0x00698727
			public bool MatchesNameAndVersion(TmodFile mod)
			{
				return this.name == mod.Name && this.version == mod.Version;
			}

			// Token: 0x060053D6 RID: 21462 RVA: 0x0069A550 File Offset: 0x00698750
			public override string ToString()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
				defaultInterpolatedStringHandler.AppendFormatted(this.name);
				defaultInterpolatedStringHandler.AppendLiteral(" v");
				defaultInterpolatedStringHandler.AppendFormatted<Version>(this.version);
				defaultInterpolatedStringHandler.AppendLiteral("[");
				defaultInterpolatedStringHandler.AppendFormatted(string.Concat(from b in RuntimeHelpers.GetSubArray<byte>(this.hash, Range.EndAt(4))
				select b.ToString("x2")));
				defaultInterpolatedStringHandler.AppendLiteral("]");
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}

			// Token: 0x04006B00 RID: 27392
			public string name;

			// Token: 0x04006B01 RID: 27393
			public Version version;

			// Token: 0x04006B02 RID: 27394
			public byte[] hash;

			// Token: 0x04006B03 RID: 27395
			public string path;
		}

		// Token: 0x02000930 RID: 2352
		internal class NetConfig
		{
			// Token: 0x060053D7 RID: 21463 RVA: 0x0069A5F1 File Offset: 0x006987F1
			public NetConfig(string modname, string configname, string json)
			{
				this.modname = modname;
				this.configname = configname;
				this.json = json;
			}

			// Token: 0x060053D8 RID: 21464 RVA: 0x0069A610 File Offset: 0x00698810
			public override string ToString()
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 3);
				defaultInterpolatedStringHandler.AppendFormatted(this.modname);
				defaultInterpolatedStringHandler.AppendLiteral(":");
				defaultInterpolatedStringHandler.AppendFormatted(this.configname);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(this.json);
				return defaultInterpolatedStringHandler.ToStringAndClear();
			}

			// Token: 0x04006B04 RID: 27396
			public string modname;

			// Token: 0x04006B05 RID: 27397
			public string configname;

			// Token: 0x04006B06 RID: 27398
			public string json;
		}

		// Token: 0x02000931 RID: 2353
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006B07 RID: 27399
			public static Action <0>__CancelDownload;
		}
	}
}
