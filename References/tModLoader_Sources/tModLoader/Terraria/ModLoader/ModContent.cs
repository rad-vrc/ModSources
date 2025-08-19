using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Prefixes;
using Terraria.GameContent.Skies;
using Terraria.GameContent.UI;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Engine;
using Terraria.ModLoader.Exceptions;
using Terraria.ModLoader.IO;
using Terraria.ModLoader.UI;
using Terraria.ModLoader.Utilities;
using Terraria.UI;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Manages content added by mods.
	/// Liaisons between mod content and Terraria's arrays and oversees the Loader classes.
	/// </summary>
	// Token: 0x020001AD RID: 429
	public static class ModContent
	{
		/// <summary> Returns the template instance of the provided content type (not the clone/new instance which gets added to Items/Players/NPCs etc. as the game is played). </summary>
		// Token: 0x0600206E RID: 8302 RVA: 0x004E2FA5 File Offset: 0x004E11A5
		public static T GetInstance<T>() where T : class
		{
			return ContentInstance<T>.Instance;
		}

		/// <summary>
		/// Returns all registered content instances that derive from the provided type and that are added by all currently loaded mods.
		/// <br />This only includes the 'template' instance for each piece of content, not all the clones/new instances which get added to Items/Players/NPCs etc. as the game is played
		/// </summary>
		// Token: 0x0600206F RID: 8303 RVA: 0x004E2FAC File Offset: 0x004E11AC
		public static IEnumerable<T> GetContent<T>() where T : ILoadable
		{
			return ContentCache.GetContentForAllMods<T>();
		}

		/// <summary> Attempts to find the template instance with the specified full name (not the clone/new instance which gets added to Items/Players/NPCs etc. as the game is played). Caching the result is recommended.<para />This will throw exceptions on failure. </summary>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException" />
		// Token: 0x06002070 RID: 8304 RVA: 0x004E2FB3 File Offset: 0x004E11B3
		public static T Find<T>(string fullname) where T : IModType
		{
			return ModTypeLookup<T>.Get(fullname);
		}

		/// <summary> Attempts to find the template instance with the specified name and mod name (not the clone/new instance which gets added to Items/Players/NPCs etc. as the game is played). Caching the result is recommended.<para />This will throw exceptions on failure. </summary>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException" />
		// Token: 0x06002071 RID: 8305 RVA: 0x004E2FBB File Offset: 0x004E11BB
		public static T Find<T>(string modName, string name) where T : IModType
		{
			return ModTypeLookup<T>.Get(modName, name);
		}

		/// <summary> Safely attempts to find the template instance with the specified full name (not the clone/new instance which gets added to Items/Players/NPCs etc. as the game is played). Caching the result is recommended. </summary>
		/// <returns> Whether or not the requested instance has been found. </returns>
		// Token: 0x06002072 RID: 8306 RVA: 0x004E2FC4 File Offset: 0x004E11C4
		public static bool TryFind<T>(string fullname, out T value) where T : IModType
		{
			return ModTypeLookup<T>.TryGetValue(fullname, out value);
		}

		/// <summary> Safely attempts to find the template instance with the specified name and mod name (not the clone/new instance which gets added to Items/Players/NPCs etc. as the game is played). Caching the result is recommended. </summary>
		/// <returns> Whether or not the requested instance has been found. </returns>
		// Token: 0x06002073 RID: 8307 RVA: 0x004E2FCD File Offset: 0x004E11CD
		public static bool TryFind<T>(string modName, string name, out T value) where T : IModType
		{
			return ModTypeLookup<T>.TryGetValue(modName, name, out value);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x004E2FD8 File Offset: 0x004E11D8
		public static void SplitName(string name, out string domain, out string subName)
		{
			int slash = name.IndexOfAny(ModContent.nameSplitters);
			if (slash < 0)
			{
				throw new MissingResourceException(Language.GetTextValue("tModLoader.LoadErrorMissingModQualifier", name));
			}
			domain = name.Substring(0, slash);
			subName = name.Substring(slash + 1);
		}

		/// <summary>
		/// Retrieves the contents of a file packaged within the .tmod file as a byte array. Should be used mainly for non-<see cref="T:ReLogic.Content.Asset`1" /> files. The <paramref name="name" /> should be in the format of "ModFolder/OtherFolders/FileNameWithExtension". Throws an ArgumentException if the mod does not exist. Returns null if the file does not exist.
		/// <para /> A typical usage of this might be to load a text file containing structured data included within your mod. Make sure the txt file is UTF8 encoded and use the following to retrieve file's text contents: <c>string pointsFileContents = Encoding.UTF8.GetString(ModContent.GetFileBytes("MyMod/data/points.txt"));</c>
		/// </summary>
		/// <exception cref="T:Terraria.ModLoader.Exceptions.MissingResourceException"></exception>
		// Token: 0x06002075 RID: 8309 RVA: 0x004E301C File Offset: 0x004E121C
		public static byte[] GetFileBytes(string name)
		{
			string modName;
			string subName;
			ModContent.SplitName(name, out modName, out subName);
			Mod mod;
			if (!ModLoader.TryGetMod(modName, out mod))
			{
				throw new MissingResourceException(Language.GetTextValue("tModLoader.LoadErrorModNotFoundDuringAsset", modName, name));
			}
			return mod.GetFileBytes(subName);
		}

		/// <summary>
		/// Returns whether or not a file with the specified name exists. Note that this includes file extension, the folder path, and must start with the mod name at the start of the path: "ModFolder/OtherFolders/FileNameWithExtension"
		/// </summary>
		// Token: 0x06002076 RID: 8310 RVA: 0x004E3058 File Offset: 0x004E1258
		public static bool FileExists(string name)
		{
			if (!name.Contains('/'))
			{
				return false;
			}
			string modName;
			string subName;
			ModContent.SplitName(name, out modName, out subName);
			Mod mod;
			return ModLoader.TryGetMod(modName, out mod) && mod.FileExists(subName);
		}

		/// <summary>
		/// Gets the asset with the specified name. Throws an Exception if the asset does not exist.
		/// <para />
		/// Modders may wish to use <c>Mod.Assets.Request</c> where the mod name prefix may be omitted for convenience.
		/// <para />
		/// <inheritdoc cref="M:ReLogic.Content.IAssetRepository.Request``1(System.String,ReLogic.Content.AssetRequestMode)" />
		/// </summary>
		/// <param name="name">The path to the asset without extension, including the mod name (or Terraria) for vanilla assets. Eg "ModName/Folder/FileNameWithoutExtension"</param>
		/// <param name="mode">The desired timing for when the asset actually loads. Use ImmediateLoad if you need correct dimensions immediately, such as with UI initialization</param>
		// Token: 0x06002077 RID: 8311 RVA: 0x004E3090 File Offset: 0x004E1290
		public static Asset<T> Request<T>(string name, AssetRequestMode mode = 2) where T : class
		{
			string modName;
			string subName;
			ModContent.SplitName(name, out modName, out subName);
			if (Main.dedServ && Main.Assets == null)
			{
				Main.Assets = new AssetRepository(null, null);
			}
			if (modName == "Terraria")
			{
				return Main.Assets.Request<T>(subName, mode);
			}
			Mod mod;
			if (!ModLoader.TryGetMod(modName, out mod))
			{
				throw new MissingResourceException(Language.GetTextValue("tModLoader.LoadErrorModNotFoundDuringAsset", modName, name));
			}
			return mod.Assets.Request<T>(subName, mode);
		}

		/// <summary>
		/// Returns whether or not a asset with the specified name exists.
		/// Includes the mod name prefix like Request
		/// </summary>
		// Token: 0x06002078 RID: 8312 RVA: 0x004E3104 File Offset: 0x004E1304
		public static bool HasAsset(string name)
		{
			if (Main.dedServ || string.IsNullOrWhiteSpace(name) || !name.Contains('/'))
			{
				return false;
			}
			string modName;
			string subName;
			ModContent.SplitName(name, out modName, out subName);
			if (modName == "Terraria")
			{
				return Main.AssetSourceController.StaticSource.HasAsset(subName);
			}
			Mod mod;
			return ModLoader.TryGetMod(modName, out mod) && mod.RootContentSource.HasAsset(subName);
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x004E316C File Offset: 0x004E136C
		public static bool RequestIfExists<T>(string name, out Asset<T> asset, AssetRequestMode mode = 2) where T : class
		{
			if (!ModContent.HasAsset(name))
			{
				asset = null;
				return false;
			}
			asset = ModContent.Request<T>(name, mode);
			return true;
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.NPCLoader.GetNPC(System.Int32)" />
		// Token: 0x0600207A RID: 8314 RVA: 0x004E3185 File Offset: 0x004E1385
		public static ModNPC GetModNPC(int type)
		{
			return NPCLoader.GetNPC(type);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.NPCHeadLoader.GetBossHeadSlot(System.String)" />
		// Token: 0x0600207B RID: 8315 RVA: 0x004E318D File Offset: 0x004E138D
		public static int GetModBossHeadSlot(string texture)
		{
			return NPCHeadLoader.GetBossHeadSlot(texture);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.NPCHeadLoader.GetHeadSlot(System.String)" />
		// Token: 0x0600207C RID: 8316 RVA: 0x004E3195 File Offset: 0x004E1395
		public static int GetModHeadSlot(string texture)
		{
			return NPCHeadLoader.GetHeadSlot(texture);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ItemLoader.GetItem(System.Int32)" />
		// Token: 0x0600207D RID: 8317 RVA: 0x004E319D File Offset: 0x004E139D
		public static ModItem GetModItem(int type)
		{
			return ItemLoader.GetItem(type);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.DustLoader.GetDust(System.Int32)" />
		// Token: 0x0600207E RID: 8318 RVA: 0x004E31A5 File Offset: 0x004E13A5
		public static ModDust GetModDust(int type)
		{
			return DustLoader.GetDust(type);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.ProjectileLoader.GetProjectile(System.Int32)" />
		// Token: 0x0600207F RID: 8319 RVA: 0x004E31AD File Offset: 0x004E13AD
		public static ModProjectile GetModProjectile(int type)
		{
			return ProjectileLoader.GetProjectile(type);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.BuffLoader.GetBuff(System.Int32)" />
		// Token: 0x06002080 RID: 8320 RVA: 0x004E31B5 File Offset: 0x004E13B5
		public static ModBuff GetModBuff(int type)
		{
			return BuffLoader.GetBuff(type);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.EquipLoader.GetEquipTexture(Terraria.ModLoader.EquipType,System.Int32)" />
		// Token: 0x06002081 RID: 8321 RVA: 0x004E31BD File Offset: 0x004E13BD
		public static EquipTexture GetEquipTexture(EquipType type, int slot)
		{
			return EquipLoader.GetEquipTexture(type, slot);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.MountLoader.GetMount(System.Int32)" />
		// Token: 0x06002082 RID: 8322 RVA: 0x004E31C6 File Offset: 0x004E13C6
		public static ModMount GetModMount(int type)
		{
			return MountLoader.GetMount(type);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.TileLoader.GetTile(System.Int32)" />
		// Token: 0x06002083 RID: 8323 RVA: 0x004E31CE File Offset: 0x004E13CE
		public static ModTile GetModTile(int type)
		{
			return TileLoader.GetTile(type);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.WallLoader.GetWall(System.Int32)" />
		// Token: 0x06002084 RID: 8324 RVA: 0x004E31D6 File Offset: 0x004E13D6
		public static ModWall GetModWall(int type)
		{
			return WallLoader.GetWall(type);
		}

		/// <summary>
		/// Returns the ModWaterStyle with the given ID.
		/// </summary>
		// Token: 0x06002085 RID: 8325 RVA: 0x004E31DE File Offset: 0x004E13DE
		public static ModWaterStyle GetModWaterStyle(int style)
		{
			return LoaderManager.Get<WaterStylesLoader>().Get(style);
		}

		/// <summary>
		/// Returns the ModWaterfallStyle with the given ID.
		/// </summary>
		// Token: 0x06002086 RID: 8326 RVA: 0x004E31EB File Offset: 0x004E13EB
		public static ModWaterfallStyle GetModWaterfallStyle(int style)
		{
			return LoaderManager.Get<WaterFallStylesLoader>().Get(style);
		}

		/// <inheritdoc cref="M:Terraria.ModLoader.BackgroundTextureLoader.GetBackgroundSlot(System.String)" />
		// Token: 0x06002087 RID: 8327 RVA: 0x004E31F8 File Offset: 0x004E13F8
		public static int GetModBackgroundSlot(string texture)
		{
			return BackgroundTextureLoader.GetBackgroundSlot(texture);
		}

		/// <summary>
		/// Returns the ModSurfaceBackgroundStyle object with the given ID.
		/// </summary>
		// Token: 0x06002088 RID: 8328 RVA: 0x004E3200 File Offset: 0x004E1400
		public static ModSurfaceBackgroundStyle GetModSurfaceBackgroundStyle(int style)
		{
			return LoaderManager.Get<SurfaceBackgroundStylesLoader>().Get(style);
		}

		/// <summary>
		/// Returns the ModUndergroundBackgroundStyle object with the given ID.
		/// </summary>
		// Token: 0x06002089 RID: 8329 RVA: 0x004E320D File Offset: 0x004E140D
		public static ModUndergroundBackgroundStyle GetModUndergroundBackgroundStyle(int style)
		{
			return LoaderManager.Get<UndergroundBackgroundStylesLoader>().Get(style);
		}

		/// <summary>
		/// Get the id (type) of a ModGore by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x0600208A RID: 8330 RVA: 0x004E321A File Offset: 0x004E141A
		public static int GoreType<T>() where T : ModGore
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModCloud by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x0600208B RID: 8331 RVA: 0x004E3231 File Offset: 0x004E1431
		public static int CloudType<T>() where T : ModCloud
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModItem by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x0600208C RID: 8332 RVA: 0x004E3248 File Offset: 0x004E1448
		public static int ItemType<T>() where T : ModItem
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModPrefix by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x0600208D RID: 8333 RVA: 0x004E325F File Offset: 0x004E145F
		public static int PrefixType<T>() where T : ModPrefix
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModRarity by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x0600208E RID: 8334 RVA: 0x004E3276 File Offset: 0x004E1476
		public static int RarityType<T>() where T : ModRarity
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModDust by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x0600208F RID: 8335 RVA: 0x004E328D File Offset: 0x004E148D
		public static int DustType<T>() where T : ModDust
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModTile by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x06002090 RID: 8336 RVA: 0x004E32A4 File Offset: 0x004E14A4
		public static int TileType<T>() where T : ModTile
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return (int)t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModPylon by class. Assumes one instance per class.
		/// If nothing is found, returns 0, or the "Forest Pylon" type.
		/// </summary>
		// Token: 0x06002091 RID: 8337 RVA: 0x004E32BB File Offset: 0x004E14BB
		public static TeleportPylonType PylonType<T>() where T : ModPylon
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return TeleportPylonType.SurfacePurity;
			}
			return t.PylonType;
		}

		/// <summary>
		/// Get the id (type) of a ModTileEntity by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x06002092 RID: 8338 RVA: 0x004E32D2 File Offset: 0x004E14D2
		public static int TileEntityType<T>() where T : ModTileEntity
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModWall by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x06002093 RID: 8339 RVA: 0x004E32E9 File Offset: 0x004E14E9
		public static int WallType<T>() where T : ModWall
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return (int)t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModProjectile by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x06002094 RID: 8340 RVA: 0x004E3300 File Offset: 0x004E1500
		public static int ProjectileType<T>() where T : ModProjectile
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModNPC by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x06002095 RID: 8341 RVA: 0x004E3317 File Offset: 0x004E1517
		public static int NPCType<T>() where T : ModNPC
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModBuff by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x06002096 RID: 8342 RVA: 0x004E332E File Offset: 0x004E152E
		public static int BuffType<T>() where T : ModBuff
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModMount by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x06002097 RID: 8343 RVA: 0x004E3345 File Offset: 0x004E1545
		public static int MountType<T>() where T : ModMount
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return t.Type;
		}

		/// <summary>
		/// Get the id (type) of a ModEmoteBubble by class. Assumes one instance per class.
		/// </summary>
		// Token: 0x06002098 RID: 8344 RVA: 0x004E335C File Offset: 0x004E155C
		public static int EmoteBubbleType<T>() where T : ModEmoteBubble
		{
			T t = ModContent.GetInstance<T>();
			if (t == null)
			{
				return 0;
			}
			return t.Type;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x004E3374 File Offset: 0x004E1574
		internal static void Load(CancellationToken token)
		{
			using (CancellationTokenSource parallelCts = new CancellationTokenSource())
			{
				using (new ModContent.ScopedCleanup(new Action(parallelCts.Cancel)))
				{
					Task task = ModContent.JITModsAsync(parallelCts.Token);
					ModContent.CacheVanillaState();
					Interface.loadMods.SetLoadStage("tModLoader.MSLoading", ModLoader.Mods.Length);
					ModContent.LoadModContent(token, delegate(Mod mod)
					{
						ContentInstance.Register(mod);
						mod.loading = true;
						mod.AutoloadConfig();
						mod.PrepareAssets();
						mod.Autoload();
						mod.Load();
						SystemLoader.OnModLoad(mod);
						SystemLoader.EnsureResizeArraysAttributeStaticCtorsRun(mod);
						mod.loading = false;
					});
					ContentCache.contentLoadingFinished = true;
					task.GetAwaiter().GetResult();
					Interface.loadMods.SetLoadStage("tModLoader.MSResizing", -1);
					ModContent.ResizeArrays(false);
					RecipeGroupHelper.CreateRecipeGroupLookups();
					Main.ResourceSetsManager.AddModdedDisplaySets();
					Main.ResourceSetsManager.SetActiveFromOriginalConfigKey();
					Interface.loadMods.SetLoadStage("tModLoader.MSSetupContent", ModLoader.Mods.Length);
					LanguageManager.Instance.ReloadLanguage(false);
					ModContent.LoadModContent(token, delegate(Mod mod)
					{
						mod.SetupContent();
					});
					ContentSamples.Initialize();
					TileLoader.PostSetupContent();
					BuffLoader.PostSetupContent();
					BiomeConversionLoader.PostSetupContent();
					Interface.loadMods.SetLoadStage("tModLoader.MSPostSetupContent", ModLoader.Mods.Length);
					ModContent.LoadModContent(token, delegate(Mod mod)
					{
						mod.PostSetupContent();
						SystemLoader.PostSetupContent(mod);
						mod.TransferAllAssets();
					});
					MemoryTracking.Finish();
					if (Main.dedServ)
					{
						ModNet.AssignNetIDs();
					}
					ModNet.SetModNetDiagnosticsUI(ModLoader.Mods);
					Main.player[255] = new Player();
					BuffLoader.FinishSetup();
					ItemLoader.FinishSetup();
					NPCLoader.FinishSetup();
					PrefixLoader.FinishSetup();
					ProjectileLoader.FinishSetup();
					PylonLoader.FinishSetup();
					TileLoader.FinishSetup();
					WallLoader.FinishSetup();
					EmoteBubbleLoader.FinishSetup();
					MapLoader.FinishSetup();
					PlantLoader.FinishSetup();
					RarityLoader.FinishSetup();
					ConfigManager.FinishSetup();
					SystemLoader.ModifyGameTipVisibility(Main.gameTips.allTips);
					PlayerInput.reinitialize = true;
					ModContent.SetupBestiary();
					NPCShopDatabase.Initialize();
					ModContent.SetupRecipes(token);
					NPCShopDatabase.FinishSetup();
					ContentSamples.RebuildItemCreativeSortingIDsAfterRecipesAreSetUp();
					ItemSorting.SetupWhiteLists();
					LocalizationLoader.FinishSetup();
					MenuLoader.GotoSavedModMenu();
					BossBarLoader.GotoSavedStyle();
					ModOrganizer.SaveLastLaunchedMods();
				}
			}
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x004E35C0 File Offset: 0x004E17C0
		internal static void RunEarlyClassConstructors()
		{
			RuntimeHelpers.RunClassConstructor(typeof(AmmoID.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(DustID.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(MountID.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(NPCHeadID.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(HairID.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(PrefixID.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(PrefixLegacy.ItemSets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.Head.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.Body.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.Legs.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.HandOn.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.HandOff.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.Back.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.Front.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.Shoe.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.Waist.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.Wing.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.Face.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.Beard.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(ArmorIDs.Balloon.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(DamageClass.Sets).TypeHandle);
			RuntimeHelpers.RunClassConstructor(typeof(MusicID.Sets).TypeHandle);
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x004E3788 File Offset: 0x004E1988
		private static Task JITModsAsync(CancellationToken token)
		{
			ModContent.<JITModsAsync>d__47 <JITModsAsync>d__;
			<JITModsAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<JITModsAsync>d__.token = token;
			<JITModsAsync>d__.<>1__state = -1;
			<JITModsAsync>d__.<>t__builder.Start<ModContent.<JITModsAsync>d__47>(ref <JITModsAsync>d__);
			return <JITModsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x004E37CB File Offset: 0x004E19CB
		private static void CacheVanillaState()
		{
			EffectsTracker.CacheVanillaState();
			DamageClassLoader.RegisterDefaultClasses();
			ExtraJumpLoader.RegisterDefaultJumps();
			InfoDisplayLoader.RegisterDefaultDisplays();
			BuilderToggleLoader.RegisterDefaultToggles();
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x004E37E8 File Offset: 0x004E19E8
		private static void LoadModContent(CancellationToken token, Action<Mod> loadAction)
		{
			MemoryTracking.Checkpoint();
			int num = 0;
			foreach (Mod mod in ModLoader.Mods)
			{
				using (new ModContent.TrackCurrentlyLoadingMod(mod.Name))
				{
					token.ThrowIfCancellationRequested();
					Interface.loadMods.SetCurrentMod(num++, mod);
					try
					{
						using (new ModContent.AssetWaitTracker(mod))
						{
							loadAction(mod);
						}
					}
					catch (Exception ex)
					{
						ex.Data["mod"] = mod.Name;
						throw;
					}
					finally
					{
						MemoryTracking.Update(mod.Name);
					}
				}
			}
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x004E38B8 File Offset: 0x004E1AB8
		private static void SetupBestiary()
		{
			BestiaryDatabase bestiaryDatabase = new BestiaryDatabase();
			new BestiaryDatabaseNPCsPopulator().Populate(bestiaryDatabase);
			Main.BestiaryDB = bestiaryDatabase;
			ContentSamples.RebuildBestiarySortingIDsByBestiaryDatabaseContents(bestiaryDatabase);
			ItemDropDatabase itemDropDatabase = new ItemDropDatabase();
			itemDropDatabase.Populate();
			Main.ItemDropsDB = itemDropDatabase;
			bestiaryDatabase.Merge(Main.ItemDropsDB);
			if (!Main.dedServ)
			{
				Main.BestiaryUI = new UIBestiaryTest(Main.BestiaryDB);
			}
			Main.ItemDropSolver = new ItemDropResolver(itemDropDatabase);
			Main.BestiaryTracker = new BestiaryUnlocksTracker();
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x004E3928 File Offset: 0x004E1B28
		private static void SetupRecipes(CancellationToken token)
		{
			Interface.loadMods.SetLoadStage("tModLoader.MSAddingRecipes", -1);
			for (int i = 0; i < Recipe.maxRecipes; i++)
			{
				token.ThrowIfCancellationRequested();
				Main.recipe[i] = new Recipe(null);
			}
			Recipe.numRecipes = 0;
			RecipeGroupHelper.ResetRecipeGroups();
			RecipeLoader.setupRecipes = true;
			Recipe.SetupRecipes();
			RecipeLoader.setupRecipes = false;
			ContentSamples.FixItemsAfterRecipesAreAdded();
			RecipeLoader.PostSetupRecipes();
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x004E3990 File Offset: 0x004E1B90
		internal static void UnloadModContent()
		{
			MenuLoader.Unload();
			CloudLoader.Unload();
			int i = 0;
			foreach (Mod mod in ModLoader.Mods.Reverse<Mod>())
			{
				Interface.loadMods.SetCurrentMod(i++, mod);
				try
				{
					MonoModHooks.RemoveAll(mod);
					mod.Close();
					mod.UnloadContent();
				}
				catch (Exception ex)
				{
					ex.Data["mod"] = mod.Name;
					throw;
				}
			}
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x004E3A30 File Offset: 0x004E1C30
		internal static void Unload()
		{
			MonoModHooks.Clear();
			TypeCaching.Clear();
			ContentCache.Unload();
			ItemLoader.Unload();
			EquipLoader.Unload();
			PrefixLoader.Unload();
			DustLoader.Unload();
			TileLoader.Unload();
			PylonLoader.Unload();
			WallLoader.Unload();
			ProjectileLoader.Unload();
			NPCLoader.Unload();
			NPCHeadLoader.Unload();
			BossBarLoader.Unload();
			PlayerLoader.Unload();
			BuffLoader.Unload();
			MountLoader.Unload();
			RarityLoader.Unload();
			DamageClassLoader.Unload();
			InfoDisplayLoader.Unload();
			BuilderToggleLoader.Unload();
			ExtraJumpLoader.Unload();
			GoreLoader.Unload();
			PlantLoader.UnloadPlants();
			HairLoader.Unload();
			EmoteBubbleLoader.Unload();
			BiomeConversionLoader.Unload();
			ResourceOverlayLoader.Unload();
			ResourceDisplaySetLoader.Unload();
			LoaderManager.Unload();
			GlobalBackgroundStyleLoader.Unload();
			PlayerDrawLayerLoader.Unload();
			MapLayerLoader.Unload();
			SystemLoader.Unload();
			ModContent.ResizeArrays(true);
			for (int i = 0; i < Recipe.maxRecipes; i++)
			{
				Main.recipe[i] = new Recipe(null);
			}
			Recipe.numRecipes = 0;
			RecipeGroupHelper.ResetRecipeGroups();
			Recipe.SetupRecipes();
			TileEntity.manager.Reset();
			MapLoader.UnloadModMap();
			ItemSorting.SetupWhiteLists();
			RecipeLoader.Unload();
			CommandLoader.Unload();
			TagSerializer.Reload();
			ModNet.Unload();
			ConfigManager.Unload();
			CustomCurrencyManager.Initialize();
			EffectsTracker.RemoveModEffects();
			ItemTrader.ChlorophyteExtractinator = ItemTrader.CreateChlorophyteExtractinator();
			Main.gameTips.Reset();
			CreativeItemSacrificesCatalog.Instance.Initialize();
			ContentSamples.CreativeResearchItemPersistentIdOverride.Clear();
			ContentSamples.Initialize();
			ModContent.SetupBestiary();
			LocalizationLoader.Unload();
			ModContent.CleanupModReferences();
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x004E3B90 File Offset: 0x004E1D90
		private static void ResizeArrays(bool unloading = false)
		{
			SetFactory.ResizeArrays(unloading);
			DamageClassLoader.ResizeArrays();
			ExtraJumpLoader.ResizeArrays();
			ItemLoader.ResizeArrays(unloading);
			EquipLoader.ResizeAndFillArrays();
			PrefixLoader.ResizeArrays();
			DustLoader.ResizeArrays();
			TileLoader.ResizeArrays(unloading);
			WallLoader.ResizeArrays(unloading);
			ProjectileLoader.ResizeArrays(unloading);
			NPCLoader.ResizeArrays(unloading);
			NPCHeadLoader.ResizeAndFillArrays();
			MountLoader.ResizeArrays();
			BuffLoader.ResizeArrays();
			PlayerLoader.ResizeArrays();
			PlayerDrawLayerLoader.ResizeArrays();
			MapLayerLoader.ResizeArrays();
			HairLoader.ResizeArrays();
			EmoteBubbleLoader.ResizeArrays();
			BuilderToggleLoader.ResizeArrays();
			BiomeConversionLoader.ResizeArrays();
			SystemLoader.ResizeArrays(unloading);
			if (!Main.dedServ)
			{
				GlobalBackgroundStyleLoader.ResizeAndFillArrays(unloading);
				GoreLoader.ResizeAndFillArrays();
				CloudLoader.ResizeAndFillArrays(unloading);
			}
			LoaderManager.ResizeArrays();
			if (!Main.dedServ)
			{
				SkyManager.Instance["CreditsRoll"] = new CreditsRollSky();
			}
		}

		/// <summary>
		/// Several arrays and other fields hold references to various classes from mods, we need to clean them up to give properly coded mods a chance to be completely free of references
		/// so that they can be collected by the garbage collection. For most things eventually they will be replaced during gameplay, but we want the old instance completely gone quickly.
		/// </summary>
		// Token: 0x060020A3 RID: 8355 RVA: 0x004E3C4C File Offset: 0x004E1E4C
		internal static void CleanupModReferences()
		{
			for (int i = 0; i < Main.player.Length; i++)
			{
				Main.player[i] = new Player();
			}
			Main.dresserInterfaceDummy = null;
			Main.clientPlayer = new Player();
			Main.ActivePlayerFileData = new PlayerFileData();
			UIList playerList = Main._characterSelectMenu._playerList;
			if (playerList != null)
			{
				playerList.Clear();
			}
			Main.PlayerList.Clear();
			if (ItemSlot.singleSlotArray[0] != null)
			{
				ItemSlot.singleSlotArray[0] = new Item();
			}
			WorldGen.ClearGenerationPasses();
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x004E3CCC File Offset: 0x004E1ECC
		public static Stream OpenRead(string assetName, bool newFileStream = false)
		{
			if (!assetName.StartsWith("tmod:"))
			{
				return File.OpenRead(assetName);
			}
			string modName;
			string entryPath;
			ModContent.SplitName(assetName.Substring(5).Replace('\\', '/'), out modName, out entryPath);
			return ModLoader.GetMod(modName).GetFileStream(entryPath, newFileStream);
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x004E3D14 File Offset: 0x004E1F14
		internal static void TransferCompletedAssets()
		{
			if (!ModLoader.isLoading)
			{
				ModContent.DoTransferCompletedAssets();
				return;
			}
			Stopwatch sw = Stopwatch.StartNew();
			while (sw.ElapsedMilliseconds < 15L)
			{
				Func<bool> condition;
				if ((condition = ModContent.<>O.<0>__DoTransferCompletedAssets) == null)
				{
					condition = (ModContent.<>O.<0>__DoTransferCompletedAssets = new Func<bool>(ModContent.DoTransferCompletedAssets));
				}
				if (!SpinWait.SpinUntil(condition, 1))
				{
					break;
				}
			}
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x004E3D64 File Offset: 0x004E1F64
		private static bool DoTransferCompletedAssets()
		{
			bool transferredAnything = false;
			Mod[] mods = ModLoader.Mods;
			for (int i = 0; i < mods.Length; i++)
			{
				AssetRepository assetRepo = mods[i].Assets;
				if (assetRepo != null && !assetRepo.IsDisposed)
				{
					transferredAnything |= assetRepo.TransferCompletedAssets();
				}
			}
			return false;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x004E3DA5 File Offset: 0x004E1FA5
		internal static string CurrentlyLoadingMod
		{
			get
			{
				return ModContent.currentMod ?? "Unknown";
			}
		}

		// Token: 0x040016BB RID: 5819
		private static readonly char[] nameSplitters = new char[]
		{
			'/',
			' ',
			':'
		};

		// Token: 0x040016BC RID: 5820
		[ThreadStatic]
		private static string currentMod = null;

		// Token: 0x0200091D RID: 2333
		private struct ScopedCleanup : IDisposable, IEquatable<ModContent.ScopedCleanup>
		{
			// Token: 0x0600539F RID: 21407 RVA: 0x00699EFE File Offset: 0x006980FE
			public ScopedCleanup(Action Dispose)
			{
				this.Dispose = Dispose;
			}

			// Token: 0x170008DE RID: 2270
			// (get) Token: 0x060053A0 RID: 21408 RVA: 0x00699F07 File Offset: 0x00698107
			// (set) Token: 0x060053A1 RID: 21409 RVA: 0x00699F0F File Offset: 0x0069810F
			public Action Dispose { readonly get; set; }

			// Token: 0x060053A2 RID: 21410 RVA: 0x00699F18 File Offset: 0x00698118
			void IDisposable.Dispose()
			{
				this.Dispose();
			}

			// Token: 0x060053A3 RID: 21411 RVA: 0x00699F28 File Offset: 0x00698128
			[CompilerGenerated]
			public override readonly string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("ScopedCleanup");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x060053A4 RID: 21412 RVA: 0x00699F74 File Offset: 0x00698174
			[CompilerGenerated]
			private readonly bool PrintMembers(StringBuilder builder)
			{
				builder.Append("Dispose = ");
				builder.Append(this.Dispose);
				return true;
			}

			// Token: 0x060053A5 RID: 21413 RVA: 0x00699F90 File Offset: 0x00698190
			[CompilerGenerated]
			public static bool operator !=(ModContent.ScopedCleanup left, ModContent.ScopedCleanup right)
			{
				return !(left == right);
			}

			// Token: 0x060053A6 RID: 21414 RVA: 0x00699F9C File Offset: 0x0069819C
			[CompilerGenerated]
			public static bool operator ==(ModContent.ScopedCleanup left, ModContent.ScopedCleanup right)
			{
				return left.Equals(right);
			}

			// Token: 0x060053A7 RID: 21415 RVA: 0x00699FA6 File Offset: 0x006981A6
			[CompilerGenerated]
			public override readonly int GetHashCode()
			{
				return EqualityComparer<Action>.Default.GetHashCode(this.<Dispose>k__BackingField);
			}

			// Token: 0x060053A8 RID: 21416 RVA: 0x00699FB8 File Offset: 0x006981B8
			[CompilerGenerated]
			public override readonly bool Equals(object obj)
			{
				return obj is ModContent.ScopedCleanup && this.Equals((ModContent.ScopedCleanup)obj);
			}

			// Token: 0x060053A9 RID: 21417 RVA: 0x00699FD0 File Offset: 0x006981D0
			[CompilerGenerated]
			public readonly bool Equals(ModContent.ScopedCleanup other)
			{
				return EqualityComparer<Action>.Default.Equals(this.<Dispose>k__BackingField, other.<Dispose>k__BackingField);
			}

			// Token: 0x060053AA RID: 21418 RVA: 0x00699FE8 File Offset: 0x006981E8
			[CompilerGenerated]
			public readonly void Deconstruct(out Action Dispose)
			{
				Dispose = this.Dispose;
			}
		}

		// Token: 0x0200091E RID: 2334
		private class AssetWaitTracker : IDisposable
		{
			// Token: 0x060053AB RID: 21419 RVA: 0x00699FF2 File Offset: 0x006981F2
			public AssetWaitTracker(Mod mod)
			{
				this.mod = mod;
				AssetRepository.OnBlockingLoadCompleted += this.AddWaitTime;
			}

			// Token: 0x060053AC RID: 21420 RVA: 0x0069A012 File Offset: 0x00698212
			private void AddWaitTime(TimeSpan t)
			{
				this.total += t;
			}

			// Token: 0x060053AD RID: 21421 RVA: 0x0069A028 File Offset: 0x00698228
			public void Dispose()
			{
				AssetRepository.OnBlockingLoadCompleted -= this.AddWaitTime;
				if (this.total > ModContent.AssetWaitTracker.MinReportThreshold)
				{
					ILog tML = Logging.tML;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(84, 4);
					defaultInterpolatedStringHandler.AppendFormatted(this.mod.Name);
					defaultInterpolatedStringHandler.AppendLiteral(" spent ");
					defaultInterpolatedStringHandler.AppendFormatted<int>((int)this.total.TotalMilliseconds);
					defaultInterpolatedStringHandler.AppendLiteral("ms blocking on asset loading. ");
					defaultInterpolatedStringHandler.AppendLiteral("Avoid using ");
					defaultInterpolatedStringHandler.AppendFormatted("AssetRequestMode");
					defaultInterpolatedStringHandler.AppendLiteral(".");
					defaultInterpolatedStringHandler.AppendFormatted("ImmediateLoad");
					defaultInterpolatedStringHandler.AppendLiteral(" during mod loading where possible");
					tML.Warn(defaultInterpolatedStringHandler.ToStringAndClear());
				}
			}

			// Token: 0x04006ADD RID: 27357
			public static readonly TimeSpan MinReportThreshold = TimeSpan.FromMilliseconds(10.0);

			// Token: 0x04006ADE RID: 27358
			private readonly Mod mod;

			// Token: 0x04006ADF RID: 27359
			private TimeSpan total;
		}

		// Token: 0x0200091F RID: 2335
		[CompilerFeatureRequired("RefStructs")]
		public ref struct TrackCurrentlyLoadingMod
		{
			// Token: 0x060053AF RID: 21423 RVA: 0x0069A104 File Offset: 0x00698304
			public TrackCurrentlyLoadingMod(string mod)
			{
				this.prev = ModContent.currentMod;
				ModContent.currentMod = mod;
			}

			// Token: 0x060053B0 RID: 21424 RVA: 0x0069A117 File Offset: 0x00698317
			public void Dispose()
			{
				ModContent.currentMod = this.prev;
			}

			// Token: 0x04006AE0 RID: 27360
			private string prev;
		}

		// Token: 0x02000920 RID: 2336
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006AE1 RID: 27361
			public static Func<bool> <0>__DoTransferCompletedAssets;
		}
	}
}
