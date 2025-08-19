using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using ReLogic.Content;
using ReLogic.OS;
using Terraria.Initializers;
using Terraria.Localization;
using Terraria.ModLoader.Assets;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.Engine;
using Terraria.ModLoader.Exceptions;
using Terraria.ModLoader.UI;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.Social.Steam;

namespace Terraria.ModLoader
{
	/// <summary>
	/// This serves as the central class which loads mods. It contains many static fields and methods related to mods and their contents.
	/// </summary>
	// Token: 0x020001B4 RID: 436
	public static class ModLoader
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600217F RID: 8575 RVA: 0x004E48EC File Offset: 0x004E2AEC
		public static string versionedName
		{
			get
			{
				if (BuildInfo.Purpose == BuildInfo.BuildPurpose.Stable)
				{
					return BuildInfo.versionedName;
				}
				return BuildInfo.versionedNameDevFriendly;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x004E4904 File Offset: 0x004E2B04
		public static string CompressedPlatformRepresentation
		{
			get
			{
				return (Platform.IsWindows ? "w" : (Platform.IsLinux ? "l" : "m")) + ((InstallVerifier.DistributionPlatform == DistributionPlatform.GoG) ? "g" : "s") + "c";
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06002181 RID: 8577 RVA: 0x004E4950 File Offset: 0x004E2B50
		public static string ModPath
		{
			get
			{
				return ModOrganizer.modPath;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x004E4957 File Offset: 0x004E2B57
		// (set) Token: 0x06002183 RID: 8579 RVA: 0x004E495E File Offset: 0x004E2B5E
		public static Mod[] Mods { get; private set; } = new Mod[0];

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x004E4966 File Offset: 0x004E2B66
		// (set) Token: 0x06002185 RID: 8581 RVA: 0x004E496D File Offset: 0x004E2B6D
		internal static AssetRepository ManifestAssets { get; set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x004E4975 File Offset: 0x004E2B75
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x004E497C File Offset: 0x004E2B7C
		internal static AssemblyResourcesContentSource ManifestContentSource { get; set; }

		/// <summary> Gets the instance of the Mod with the specified name. This will throw an exception if the mod cannot be found so it should only be used for mods known to be enabled, such as a strong mod dependency.
		/// <para /> Use <see cref="M:Terraria.ModLoader.ModLoader.TryGetMod(System.String,Terraria.ModLoader.Mod@)" /> instead if the mod might not be enabled. </summary>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException" />
		// Token: 0x06002188 RID: 8584 RVA: 0x004E4984 File Offset: 0x004E2B84
		public static Mod GetMod(string name)
		{
			return ModLoader.modsByName[name];
		}

		/// <summary> Safely attempts to get the instance of the Mod with the specified name. </summary>
		/// <returns> Whether or not the requested instance has been found. </returns>
		// Token: 0x06002189 RID: 8585 RVA: 0x004E4991 File Offset: 0x004E2B91
		public static bool TryGetMod(string name, out Mod result)
		{
			return ModLoader.modsByName.TryGetValue(name, out result);
		}

		/// <summary> Safely checks whether or not a mod with the specified internal name is currently loaded. </summary>
		/// <returns> Whether or not a mod with the provided internal name has been found. </returns>
		// Token: 0x0600218A RID: 8586 RVA: 0x004E499F File Offset: 0x004E2B9F
		public static bool HasMod(string name)
		{
			return ModLoader.modsByName.ContainsKey(name);
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x004E49AC File Offset: 0x004E2BAC
		internal static void EngineInit()
		{
			FileAssociationSupport.UpdateFileAssociation();
			FolderShortcutSupport.UpdateFolderShortcuts();
			MonoModHooks.Initialize();
			FNAFixes.Init();
			LoaderManager.AutoLoad();
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x004E49C8 File Offset: 0x004E2BC8
		internal static void PrepareAssets()
		{
			ModLoader.ManifestContentSource = new AssemblyResourcesContentSource(Assembly.GetExecutingAssembly(), null, new string[]
			{
				"Terraria/ModLoader/Templates/"
			});
			AssetRepository assetRepository = new AssetRepository(AssetInitializer.assetReaderCollection, new AssemblyResourcesContentSource[]
			{
				ModLoader.ManifestContentSource
			});
			FailedToLoadAssetCustomAction assetLoadFailHandler;
			if ((assetLoadFailHandler = ModLoader.<>O.<0>__OnceFailedLoadingAnAsset) == null)
			{
				assetLoadFailHandler = (ModLoader.<>O.<0>__OnceFailedLoadingAnAsset = new FailedToLoadAssetCustomAction(Main.OnceFailedLoadingAnAsset));
			}
			assetRepository.AssetLoadFailHandler = assetLoadFailHandler;
			ModLoader.ManifestAssets = assetRepository;
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x004E4A31 File Offset: 0x004E2C31
		internal static void BeginLoad(CancellationToken token)
		{
			Task.Run(delegate()
			{
				ModLoader.Load(token);
			});
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x004E4A50 File Offset: 0x004E2C50
		private static void Load(CancellationToken token = default(CancellationToken))
		{
			if (ModLoader.isLoading)
			{
				throw new Exception("Load called twice");
			}
			ModLoader.isLoading = true;
			if (!ModLoader.Unload())
			{
				return;
			}
			ModLoader.<>c__DisplayClass49_0 CS$<>8__locals1;
			CS$<>8__locals1.availableMods = ModOrganizer.FindMods(true);
			try
			{
				Stopwatch sw = Stopwatch.StartNew();
				List<Mod> list = AssemblyManager.InstantiateMods(ModOrganizer.SelectAndSortMods(CS$<>8__locals1.availableMods, token), token);
				list.Insert(0, new ModLoaderMod());
				ModLoader.Mods = list.ToArray();
				foreach (Mod mod in ModLoader.Mods)
				{
					ModLoader.modsByName[mod.Name] = mod;
				}
				ModContent.Load(token);
				ILog tML = Logging.tML;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(24, 1);
				defaultInterpolatedStringHandler.AppendLiteral("Mod Load Completed in ");
				defaultInterpolatedStringHandler.AppendFormatted<long>(sw.ElapsedMilliseconds);
				defaultInterpolatedStringHandler.AppendLiteral("ms");
				tML.Info(defaultInterpolatedStringHandler.ToStringAndClear());
				if (ModLoader.OnSuccessfulLoad != null)
				{
					ModLoader.OnSuccessfulLoad();
				}
				else
				{
					Main.menuMode = 0;
				}
			}
			catch when (endfilter(token.IsCancellationRequested > false))
			{
				ModLoader.skipLoad = true;
				ModLoader.OnSuccessfulLoad = (Action)Delegate.Combine(ModLoader.OnSuccessfulLoad, new Action(delegate()
				{
					Main.menuMode = 10000;
				}));
				ModLoader.isLoading = false;
				ModLoader.Load(default(CancellationToken));
			}
			catch (Exception e)
			{
				List<string> responsibleMods = new List<string>();
				if (e.Data.Contains("mod"))
				{
					responsibleMods.Add((string)e.Data["mod"]);
				}
				if (e.Data.Contains("mods"))
				{
					responsibleMods.AddRange((IEnumerable<string>)e.Data["mods"]);
				}
				responsibleMods.Remove("ModLoader");
				string stackMod;
				if (responsibleMods.Count == 0 && AssemblyManager.FirstModInStackTrace(new StackTrace(e), out stackMod))
				{
					responsibleMods.Add(stackMod);
				}
				string msg = Language.GetTextValue("tModLoader.LoadError", string.Join(", ", responsibleMods));
				if (responsibleMods.Count == 1)
				{
					LocalMod mod2 = CS$<>8__locals1.availableMods.FirstOrDefault((LocalMod m) => m.Name == responsibleMods[0]);
					if (mod2 != null)
					{
						string str = msg;
						DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 1);
						defaultInterpolatedStringHandler.AppendLiteral(" v");
						defaultInterpolatedStringHandler.AppendFormatted<Version>(mod2.Version);
						msg = str + defaultInterpolatedStringHandler.ToStringAndClear();
					}
					if (mod2 != null && mod2.tModLoaderVersion.MajorMinorBuild() != BuildInfo.tMLVersion.MajorMinorBuild())
					{
						msg = msg + "\n" + Language.GetTextValue("tModLoader.LoadErrorVersionMessage", mod2.tModLoaderVersion, ModLoader.versionedName);
					}
					else if (mod2 != null)
					{
						SteamedWraps.QueueForceValidateSteamInstall();
					}
					if (e is JITException)
					{
						msg = msg + "\nThe mod will need to be updated to match the current tModLoader version, or may be incompatible with the version of some of your other mods. Click the '" + Language.GetTextValue("tModLoader.OpenWebHelp") + "' button to learn more.";
					}
				}
				if (responsibleMods.Count > 0)
				{
					msg = msg + "\n" + Language.GetTextValue("tModLoader.LoadErrorDisabled");
				}
				else
				{
					msg = msg + "\n" + Language.GetTextValue("tModLoader.LoadErrorCulpritUnknown");
				}
				ReflectionTypeLoadException reflectionTypeLoadException = e as ReflectionTypeLoadException;
				if (reflectionTypeLoadException != null)
				{
					msg = msg + "\n\n" + string.Join("\n", from x in reflectionTypeLoadException.LoaderExceptions
					select x.Message);
				}
				if (e.Data.Contains("contentType"))
				{
					Type contentType = e.Data["contentType"] as Type;
					if (contentType != null)
					{
						msg = msg + "\n" + Language.GetTextValue("tModLoader.LoadErrorContentType", contentType.FullName);
					}
				}
				foreach (string mod3 in responsibleMods)
				{
					ModLoader.<Load>g__DisableModAndDependents|49_2(mod3, ref CS$<>8__locals1);
				}
				Logging.tML.Error(msg, e);
				ModLoader.isLoading = false;
				ModLoader.DisplayLoadError(msg, e, e.Data.Contains("fatal"), responsibleMods.Count == 0);
			}
			finally
			{
				ModLoader.isLoading = false;
				ModLoader.OnSuccessfulLoad = null;
				ModLoader.skipLoad = false;
				ModNet.NetReloadActive = false;
			}
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x004E4F30 File Offset: 0x004E3130
		internal static void Reload()
		{
			if (Main.dedServ)
			{
				ModLoader.Load(default(CancellationToken));
				return;
			}
			Main.menuMode = 10002;
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x004E4F60 File Offset: 0x004E3160
		internal static bool Unload()
		{
			bool result;
			try
			{
				IReadOnlyList<WeakReference<Mod>> weakModRefs = ModLoader.GetWeakModRefs();
				ModLoader.Mods_Unload();
				ModLoader.WarnModsStillLoaded(weakModRefs);
				result = true;
			}
			catch (Exception e)
			{
				string msg = Language.GetTextValue("tModLoader.UnloadError");
				if (e.Data.Contains("mod"))
				{
					msg = msg + "\n" + Language.GetTextValue("tModLoader.DefensiveUnload", e.Data["mod"]);
				}
				Logging.tML.Fatal(msg, e);
				ModLoader.DisplayLoadError(msg, e, true, false);
				result = false;
			}
			return result;
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x004E4FF0 File Offset: 0x004E31F0
		internal static bool IsUnloadedModStillAlive(string name)
		{
			return AssemblyManager.OldLoadContexts().Contains(name);
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x004E4FFD File Offset: 0x004E31FD
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static WeakReference<Mod>[] GetWeakModRefs()
		{
			return (from x in ModLoader.Mods
			select new WeakReference<Mod>(x)).ToArray<WeakReference<Mod>>();
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x004E5030 File Offset: 0x004E3230
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void Mods_Unload()
		{
			Interface.loadMods.SetLoadStage("tModLoader.MSUnloading", ModLoader.Mods.Length);
			WorldGen.clearWorld();
			ModContent.UnloadModContent();
			ModLoader.Mods = new Mod[0];
			ModLoader.modsByName.Clear();
			ModContent.Unload();
			MemoryTracking.Clear();
			Thread.MemoryBarrier();
			AssemblyManager.Unload();
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x004E5088 File Offset: 0x004E3288
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void WarnModsStillLoaded(IReadOnlyList<WeakReference<Mod>> weakModRefs)
		{
			using (IEnumerator<string> enumerator = AssemblyManager.OldLoadContexts().Distinct<string>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string alcName = enumerator.Current;
					if (weakModRefs.Any(delegate(WeakReference<Mod> modRef)
					{
						Mod mod;
						return modRef.TryGetTarget(out mod) && mod.Name == alcName;
					}))
					{
						Logging.tML.WarnFormat(alcName + " mod class still using memory. Some content references have probably not been cleared. Use a heap dump to figure out why.", Array.Empty<object>());
					}
					else
					{
						Logging.tML.WarnFormat(alcName + " AssemblyLoadContext still using memory. Some classes are being held by Terraria or another mod. Use a heap dump to figure out why.", Array.Empty<object>());
					}
				}
			}
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x004E5134 File Offset: 0x004E3334
		private static void DisplayLoadError(string msg, Exception e, bool fatal, bool continueIsRetry = false)
		{
			msg = msg + "\n\n" + (e.Data.Contains("hideStackTrace") ? e.Message : e.ToString());
			if (!Main.dedServ)
			{
				string HelpLink = e.HelpLink;
				if (HelpLink == null)
				{
					MultipleException multipleException = e as MultipleException;
					if (multipleException != null)
					{
						HelpLink = (from x in multipleException.InnerExceptions
						where x.HelpLink != null
						select x.HelpLink).FirstOrDefault<string>();
					}
				}
				Interface.errorMessage.Show(msg, fatal ? -1 : 10006, null, HelpLink, continueIsRetry, !fatal, null);
				return;
			}
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(msg);
			Console.ResetColor();
			if (fatal)
			{
				Console.WriteLine("Press any key to exit...");
				Console.ReadKey();
				Environment.Exit(-1);
				return;
			}
			ModLoader.Reload();
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x004E5230 File Offset: 0x004E3430
		public static bool IsSignedBy(TmodFile mod, string xmlPublicKey)
		{
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter();
			AsymmetricAlgorithm v = AsymmetricAlgorithm.Create("RSA");
			rsapkcs1SignatureDeformatter.SetHashAlgorithm("SHA1");
			v.FromXmlString(xmlPublicKey);
			rsapkcs1SignatureDeformatter.SetKey(v);
			return rsapkcs1SignatureDeformatter.VerifySignature(mod.Hash, mod.Signature);
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06002197 RID: 8599 RVA: 0x004E5277 File Offset: 0x004E3477
		internal static HashSet<string> EnabledMods
		{
			get
			{
				HashSet<string> result;
				if ((result = ModLoader._enabledMods) == null)
				{
					result = (ModLoader._enabledMods = ModOrganizer.LoadEnabledMods());
				}
				return result;
			}
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x004E528D File Offset: 0x004E348D
		internal static bool IsEnabled(string modName)
		{
			return ModLoader.EnabledMods.Contains(modName);
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x004E529A File Offset: 0x004E349A
		internal static void EnableMod(string modName)
		{
			ModLoader.SetModEnabled(modName, true);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x004E52A3 File Offset: 0x004E34A3
		internal static void DisableMod(string modName)
		{
			ModLoader.SetModEnabled(modName, false);
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x004E52AC File Offset: 0x004E34AC
		internal static void SetModEnabled(string modName, bool active)
		{
			if (active == ModLoader.IsEnabled(modName))
			{
				return;
			}
			Logging.tML.Info((active ? "Enabling" : "Disabling") + " Mod: " + modName);
			if (active)
			{
				ModLoader.EnabledMods.Add(modName);
			}
			else
			{
				ModLoader.EnabledMods.Remove(modName);
			}
			ModOrganizer.SaveEnabledMods();
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x004E5309 File Offset: 0x004E3509
		internal static void DisableAllMods()
		{
			Logging.tML.InfoFormat("Disabling All Mods: " + string.Join(", ", ModLoader.EnabledMods), Array.Empty<object>());
			ModLoader.EnabledMods.Clear();
			ModOrganizer.SaveEnabledMods();
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x004E5344 File Offset: 0x004E3544
		internal static void SaveConfiguration()
		{
			Main.Configuration.Put("ModBrowserPassphrase", ModLoader.modBrowserPassphrase);
			Main.Configuration.Put("DownloadModsFromServers", ModNet.downloadModsFromServers);
			Main.Configuration.Put("AutomaticallyReloadAndEnableModsLeavingModBrowser", ModLoader.autoReloadAndEnableModsLeavingModBrowser);
			Main.Configuration.Put("AutomaticallyReloadRequiredModsLeavingModsScreen", ModLoader.autoReloadRequiredModsLeavingModsScreen);
			Main.Configuration.Put("RemoveForcedMinimumZoom", ModLoader.removeForcedMinimumZoom);
			Main.Configuration.Put("attackSpeedScalingTooltipVisibility".ToUpperInvariant(), ModLoader.attackSpeedScalingTooltipVisibility);
			Main.Configuration.Put("AvoidGithub", UIModBrowser.AvoidGithub);
			Main.Configuration.Put("AvoidImgur", UIModBrowser.AvoidImgur);
			Main.Configuration.Put("EarlyAutoUpdate", UIModBrowser.EarlyAutoUpdate);
			Main.Configuration.Put("ShowModMenuNotifications", ModLoader.notifyNewMainMenuThemes);
			Main.Configuration.Put("ShowNewUpdatedModsInfo", ModLoader.showNewUpdatedModsInfo);
			Main.Configuration.Put("ShowConfirmationWindowWhenEnableDisableAllMods", ModLoader.showConfirmationWindowWhenEnableDisableAllMods);
			Main.Configuration.Put("LastSelectedModMenu", MenuLoader.LastSelectedModMenu);
			Main.Configuration.Put("KnownMenuThemes", MenuLoader.KnownMenuSaveString);
			Main.Configuration.Put("BossBarStyle", BossBarLoader.lastSelectedStyle);
			Main.Configuration.Put("SeenFirstLaunchModderWelcomeMessage", ModLoader.SeenFirstLaunchModderWelcomeMessage);
			Main.Configuration.Put("LastLaunchedTModLoaderVersion", BuildInfo.tMLVersion.ToString());
			Main.Configuration.Put("BetaUpgradeWelcomed144", ModLoader.BetaUpgradeWelcomed144);
			Main.Configuration.Put("LastLaunchedTModLoaderAlphaSha", (BuildInfo.IsPreview && BuildInfo.CommitSHA != "unknown") ? BuildInfo.CommitSHA : ModLoader.LastLaunchedTModLoaderAlphaSha);
			Main.Configuration.Put("LastPreviewFreezeNotificationSeen", ModLoader.LastPreviewFreezeNotificationSeen.ToString());
			Main.Configuration.Put("ModPackActive", ModOrganizer.ModPackActive);
			Main.Configuration.Put("LatestNewsTimestamp", ModLoader.LatestNewsTimestamp);
			Main.Configuration.Put("WarnedFamilyShareDontShowAgain", ModLoader.WarnedFamilyShareDontShowAgain);
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x004E5598 File Offset: 0x004E3798
		internal static void LoadConfiguration()
		{
			Main.Configuration.Get<string>("ModBrowserPassphrase", ref ModLoader.modBrowserPassphrase);
			Main.Configuration.Get<bool>("DownloadModsFromServers", ref ModNet.downloadModsFromServers);
			Main.Configuration.Get<bool>("AutomaticallyReloadAndEnableModsLeavingModBrowser", ref ModLoader.autoReloadAndEnableModsLeavingModBrowser);
			Main.Configuration.Get<bool>("AutomaticallyReloadRequiredModsLeavingModsScreen", ref ModLoader.autoReloadRequiredModsLeavingModsScreen);
			Main.Configuration.Get<bool>("RemoveForcedMinimumZoom", ref ModLoader.removeForcedMinimumZoom);
			Main.Configuration.Get<int>("attackSpeedScalingTooltipVisibility".ToUpperInvariant(), ref ModLoader.attackSpeedScalingTooltipVisibility);
			Main.Configuration.Get<bool>("AvoidGithub", ref UIModBrowser.AvoidGithub);
			Main.Configuration.Get<bool>("AvoidImgur", ref UIModBrowser.AvoidImgur);
			Main.Configuration.Get<bool>("EarlyAutoUpdate", ref UIModBrowser.EarlyAutoUpdate);
			Main.Configuration.Get<bool>("ShowModMenuNotifications", ref ModLoader.notifyNewMainMenuThemes);
			Main.Configuration.Get<bool>("ShowConfirmationWindowWhenEnableDisableAllMods", ref ModLoader.showConfirmationWindowWhenEnableDisableAllMods);
			Main.Configuration.Get<bool>("ShowNewUpdatedModsInfo", ref ModLoader.showNewUpdatedModsInfo);
			Main.Configuration.Get<string>("LastSelectedModMenu", ref MenuLoader.LastSelectedModMenu);
			Main.Configuration.Get<string>("KnownMenuThemes", ref MenuLoader.KnownMenuSaveString);
			Main.Configuration.Get<string>("BossBarStyle", ref BossBarLoader.lastSelectedStyle);
			Main.Configuration.Get<bool>("SeenFirstLaunchModderWelcomeMessage", ref ModLoader.SeenFirstLaunchModderWelcomeMessage);
			Main.Configuration.Get<string>("ModPackActive", ref ModOrganizer.ModPackActive);
			ModLoader.LastLaunchedTModLoaderVersion = new Version(Main.Configuration.Get<string>("LastLaunchedTModLoaderVersion", "0.0"));
			Main.Configuration.Get<bool>("BetaUpgradeWelcomed144", ref ModLoader.BetaUpgradeWelcomed144);
			Main.Configuration.Get<string>("LastLaunchedTModLoaderAlphaSha", ref ModLoader.LastLaunchedTModLoaderAlphaSha);
			ModLoader.LastPreviewFreezeNotificationSeen = new Version(Main.Configuration.Get<string>("LastPreviewFreezeNotificationSeen", "0.0"));
			Main.Configuration.Get<int>("LatestNewsTimestamp", ref ModLoader.LatestNewsTimestamp);
			Main.Configuration.Get<bool>("WarnedFamilyShareDontShowAgain", ref ModLoader.WarnedFamilyShareDontShowAgain);
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x004E578A File Offset: 0x004E398A
		internal static void MigrateSettings()
		{
			if (BuildInfo.IsPreview && ModLoader.LastLaunchedTModLoaderVersion != BuildInfo.tMLVersion)
			{
				ModLoader.ShowWhatsNew = true;
			}
			if (ModLoader.LastLaunchedTModLoaderVersion == new Version(0, 0))
			{
				ModLoader.ShowFirstLaunchWelcomeMessage = true;
			}
		}

		/// <summary>
		/// Allows type inference on T and F
		/// </summary>
		// Token: 0x060021A0 RID: 8608 RVA: 0x004E57C4 File Offset: 0x004E39C4
		internal static void BuildGlobalHook<T, F>(ref F[] list, IList<T> providers, Expression<Func<T, F>> expr) where F : Delegate
		{
			LoaderUtils.MethodOverrideQuery<T> query = expr.ToOverrideQuery<T, F>();
			list = (from t in providers.Where(new Func<T, bool>(query.HasOverride))
			select (F)((object)query.Binder(t))).ToArray<F>();
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x004E5874 File Offset: 0x004E3A74
		[CompilerGenerated]
		internal static void <Load>g__DisableModAndDependents|49_2(string mod, ref ModLoader.<>c__DisplayClass49_0 A_1)
		{
			ModLoader.DisableMod(mod);
			Func<string, bool> <>9__6;
			foreach (string mod2 in from m in A_1.availableMods.Where(delegate(LocalMod m)
			{
				if (ModLoader.IsEnabled(m.Name))
				{
					IEnumerable<string> source = m.properties.RefNames(false);
					Func<string, bool> predicate;
					if ((predicate = <>9__6) == null)
					{
						predicate = (<>9__6 = ((string refName) => refName.Equals(mod)));
					}
					return source.Any(predicate);
				}
				return false;
			})
			select m.Name)
			{
				ModLoader.<Load>g__DisableModAndDependents|49_2(mod2, ref A_1);
			}
		}

		// Token: 0x040016CA RID: 5834
		public static Version LastLaunchedTModLoaderVersion;

		// Token: 0x040016CB RID: 5835
		public static string LastLaunchedTModLoaderAlphaSha;

		// Token: 0x040016CC RID: 5836
		public static bool ShowWhatsNew;

		// Token: 0x040016CD RID: 5837
		public static bool PreviewFreezeNotification;

		// Token: 0x040016CE RID: 5838
		public static bool DownloadedDependenciesOnStartup;

		// Token: 0x040016CF RID: 5839
		public static bool ShowFirstLaunchWelcomeMessage;

		// Token: 0x040016D0 RID: 5840
		public static bool SeenFirstLaunchModderWelcomeMessage;

		// Token: 0x040016D1 RID: 5841
		public static bool WarnedFamilyShare;

		// Token: 0x040016D2 RID: 5842
		public static bool WarnedFamilyShareDontShowAgain;

		// Token: 0x040016D3 RID: 5843
		public static Version LastPreviewFreezeNotificationSeen;

		// Token: 0x040016D4 RID: 5844
		public static int LatestNewsTimestamp;

		// Token: 0x040016D5 RID: 5845
		public static bool BetaUpgradeWelcomed144;

		// Token: 0x040016D6 RID: 5846
		private static readonly IDictionary<string, Mod> modsByName = new Dictionary<string, Mod>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040016D7 RID: 5847
		internal static readonly string modBrowserPublicKey = "<RSAKeyValue><Modulus>oCZObovrqLjlgTXY/BKy72dRZhoaA6nWRSGuA+aAIzlvtcxkBK5uKev3DZzIj0X51dE/qgRS3OHkcrukqvrdKdsuluu0JmQXCv+m7sDYjPQ0E6rN4nYQhgfRn2kfSvKYWGefp+kqmMF9xoAq666YNGVoERPm3j99vA+6EIwKaeqLB24MrNMO/TIf9ysb0SSxoV8pC/5P/N6ViIOk3adSnrgGbXnFkNQwD0qsgOWDks8jbYyrxUFMc4rFmZ8lZKhikVR+AisQtPGUs3ruVh4EWbiZGM2NOkhOCOM4k1hsdBOyX2gUliD0yjK5tiU3LBqkxoi2t342hWAkNNb4ZxLotw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";

		// Token: 0x040016D8 RID: 5848
		internal static string modBrowserPassphrase = "";

		// Token: 0x040016D9 RID: 5849
		internal static bool autoReloadAndEnableModsLeavingModBrowser = true;

		// Token: 0x040016DA RID: 5850
		internal static bool autoReloadRequiredModsLeavingModsScreen = true;

		// Token: 0x040016DB RID: 5851
		internal static bool removeForcedMinimumZoom;

		// Token: 0x040016DC RID: 5852
		internal static int attackSpeedScalingTooltipVisibility = 1;

		// Token: 0x040016DD RID: 5853
		internal static bool notifyNewMainMenuThemes = true;

		// Token: 0x040016DE RID: 5854
		internal static bool showNewUpdatedModsInfo = true;

		// Token: 0x040016DF RID: 5855
		internal static bool showConfirmationWindowWhenEnableDisableAllMods = true;

		// Token: 0x040016E0 RID: 5856
		internal static bool skipLoad;

		// Token: 0x040016E1 RID: 5857
		internal static Action OnSuccessfulLoad;

		// Token: 0x040016E2 RID: 5858
		internal static bool isLoading;

		/// <summary>A cached list of enabled mods (not necessarily currently loaded or even installed), mirroring the enabled.json file.</summary>
		// Token: 0x040016E6 RID: 5862
		private static HashSet<string> _enabledMods;

		// Token: 0x02000924 RID: 2340
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006AEF RID: 27375
			public static FailedToLoadAssetCustomAction <0>__OnceFailedLoadingAnAsset;
		}
	}
}
