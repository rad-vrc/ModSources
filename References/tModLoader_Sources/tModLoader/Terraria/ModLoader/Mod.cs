using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using log4net;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Content.Sources;
using ReLogic.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Assets;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Exceptions;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Terraria.ModLoader
{
	/// <summary>
	/// Mod is an abstract class that you will override. It serves as a central place from which the mod's contents are stored. It provides methods for you to use or override.
	/// </summary>
	// Token: 0x02000198 RID: 408
	public class Mod
	{
		/// <summary>
		/// The TmodFile object created when tModLoader reads this mod.
		/// </summary>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x004E19DF File Offset: 0x004DFBDF
		// (set) Token: 0x06001F59 RID: 8025 RVA: 0x004E19E7 File Offset: 0x004DFBE7
		internal TmodFile File { get; set; }

		/// <summary>
		/// The assembly code this is loaded when tModLoader loads this mod. <br />
		/// Do NOT call <see cref="M:System.Reflection.Assembly.GetTypes" /> on this as it will error out if the mod uses the <see cref="T:Terraria.ModLoader.ExtendsFromModAttribute" /> attribute to inherit from weakly referenced mods. Use <see cref="M:Terraria.ModLoader.Core.AssemblyManager.GetLoadableTypes(System.Reflection.Assembly)" /> instead.
		/// </summary>
		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x004E19F0 File Offset: 0x004DFBF0
		// (set) Token: 0x06001F5B RID: 8027 RVA: 0x004E19F8 File Offset: 0x004DFBF8
		public Assembly Code { get; internal set; }

		/// <summary>
		/// A logger with this mod's name for easy logging.
		/// </summary>
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001F5C RID: 8028 RVA: 0x004E1A01 File Offset: 0x004DFC01
		// (set) Token: 0x06001F5D RID: 8029 RVA: 0x004E1A09 File Offset: 0x004DFC09
		public ILog Logger { get; internal set; }

		/// <summary>
		/// Stores the name of the mod. This name serves as the mod's identification, and also helps with saving everything your mod adds. By default this returns the name of the folder that contains all your code and stuff.
		/// </summary>
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001F5E RID: 8030 RVA: 0x004E1A12 File Offset: 0x004DFC12
		public virtual string Name
		{
			get
			{
				return this.File.Name;
			}
		}

		/// <summary>
		/// The version of tModLoader that was being used when this mod was built.
		/// </summary>
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001F5F RID: 8031 RVA: 0x004E1A1F File Offset: 0x004DFC1F
		// (set) Token: 0x06001F60 RID: 8032 RVA: 0x004E1A27 File Offset: 0x004DFC27
		public Version TModLoaderVersion { get; internal set; }

		/// <summary>
		/// This version number of this mod.
		/// </summary>
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x004E1A30 File Offset: 0x004DFC30
		public virtual Version Version
		{
			get
			{
				return this.File.Version;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001F62 RID: 8034 RVA: 0x004E1A3D File Offset: 0x004DFC3D
		// (set) Token: 0x06001F63 RID: 8035 RVA: 0x004E1A45 File Offset: 0x004DFC45
		public List<string> TranslationForMods { get; internal set; }

		/// <summary>
		/// The path to the source folder the mod was built from.
		/// </summary>
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x004E1A4E File Offset: 0x004DFC4E
		// (set) Token: 0x06001F65 RID: 8037 RVA: 0x004E1A56 File Offset: 0x004DFC56
		public string SourceFolder { get; internal set; }

		/// <summary>
		/// Whether or not this mod will autoload content by default. Autoloading content means you do not need to manually add content through methods.
		/// </summary>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06001F66 RID: 8038 RVA: 0x004E1A5F File Offset: 0x004DFC5F
		// (set) Token: 0x06001F67 RID: 8039 RVA: 0x004E1A67 File Offset: 0x004DFC67
		public bool ContentAutoloadingEnabled { get; set; } = true;

		/// <summary>
		/// Whether or not this mod will automatically add images in the "Gores" folder as gores to the game, along with any <see cref="T:Terraria.ModLoader.ModGore" /> classes that share names with the images. This means you do not need to manually call <see cref="M:Terraria.ModLoader.GoreLoader.AddGoreFromTexture``1(Terraria.ModLoader.Mod,System.String)" />.
		/// </summary>
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x004E1A70 File Offset: 0x004DFC70
		// (set) Token: 0x06001F69 RID: 8041 RVA: 0x004E1A78 File Offset: 0x004DFC78
		public bool GoreAutoloadingEnabled { get; set; } = true;

		/// <summary>
		/// Whether or not this mod will automatically add images in the "Clouds" folder as clouds to the game. This means you do not need to manually call <see cref="M:Terraria.ModLoader.CloudLoader.AddCloudFromTexture(Terraria.ModLoader.Mod,System.String,System.Single,System.Boolean)" /> or make a <see cref="T:Terraria.ModLoader.ModCloud" /> class to add them to the game, but they will have the default spawn chance and be counted as normal clouds if autoloaded in this manner. Defaults to true.
		/// </summary>
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001F6A RID: 8042 RVA: 0x004E1A81 File Offset: 0x004DFC81
		// (set) Token: 0x06001F6B RID: 8043 RVA: 0x004E1A89 File Offset: 0x004DFC89
		public bool CloudAutoloadingEnabled { get; set; } = true;

		/// <summary>
		/// Whether or not this mod will automatically add music to the game. All supported audio files in a folder or subfolder of a folder named "Music" will be autoloaded as music.
		/// </summary>
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001F6C RID: 8044 RVA: 0x004E1A92 File Offset: 0x004DFC92
		// (set) Token: 0x06001F6D RID: 8045 RVA: 0x004E1A9A File Offset: 0x004DFC9A
		public bool MusicAutoloadingEnabled { get; set; } = true;

		/// <summary>
		/// Whether or not all music loaded by this mod will automatically have <see cref="F:Terraria.ID.MusicID.Sets.SkipsVolumeRemap" /> set to true.
		/// </summary>
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001F6E RID: 8046 RVA: 0x004E1AA3 File Offset: 0x004DFCA3
		// (set) Token: 0x06001F6F RID: 8047 RVA: 0x004E1AAB File Offset: 0x004DFCAB
		public bool MusicSkipsVolumeRemap { get; set; }

		/// <summary>
		/// Whether or not this mod will automatically add images in the "Backgrounds" folder as background textures to the game. This means you do not need to manually call <see cref="M:Terraria.ModLoader.BackgroundTextureLoader.AddBackgroundTexture(Terraria.ModLoader.Mod,System.String)" />.
		/// </summary>
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001F70 RID: 8048 RVA: 0x004E1AB4 File Offset: 0x004DFCB4
		// (set) Token: 0x06001F71 RID: 8049 RVA: 0x004E1ABC File Offset: 0x004DFCBC
		public bool BackgroundAutoloadingEnabled { get; set; } = true;

		/// <summary>
		/// The ModSide that controls how this mod is synced between client and server.
		/// </summary>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06001F72 RID: 8050 RVA: 0x004E1AC5 File Offset: 0x004DFCC5
		// (set) Token: 0x06001F73 RID: 8051 RVA: 0x004E1ACD File Offset: 0x004DFCCD
		public ModSide Side { get; internal set; }

		/// <summary>
		/// The display name of this mod in the Mods menu.
		/// </summary>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x004E1AD6 File Offset: 0x004DFCD6
		// (set) Token: 0x06001F75 RID: 8053 RVA: 0x004E1ADE File Offset: 0x004DFCDE
		public string DisplayName { get; internal set; }

		/// <summary>
		/// Same as DisplayName, but chat tags are removed. This can be used for more readable logging and console output. It is also useful for code that searches or filters by mod name.
		/// </summary>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x004E1AE8 File Offset: 0x004DFCE8
		public string DisplayNameClean
		{
			get
			{
				string result;
				if ((result = this.displayNameClean) == null)
				{
					result = (this.displayNameClean = Utils.CleanChatTags(this.DisplayName));
				}
				return result;
			}
		}

		/// <summary>
		/// Provides access to assets (textures, sounds, shaders, etc) contained within this mod. The main usage is to call the <see cref="M:ReLogic.Content.AssetRepository.Request``1(System.String)" /> method to retrieve an Asset&lt;T&gt; instance:
		/// <code>Asset&lt;Texture2D&gt; balloonTexture = Mod.Assets.Request&lt;Texture2D&gt;("Content/Items/Armor/SimpleAccessory_Balloon");</code>
		/// Do not include the mod name in the Request method call, the path supplied should not include the mod name. This is different from using <see cref="M:Terraria.ModLoader.ModContent.Request``1(System.String,ReLogic.Content.AssetRequestMode)" /> where the mod name is required.
		/// <para /> Read the <see href="https://github.com/tModLoader/tModLoader/wiki/Assets">Assets guide on the wiki</see> for more information.
		/// </summary>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x004E1B13 File Offset: 0x004DFD13
		// (set) Token: 0x06001F78 RID: 8056 RVA: 0x004E1B1B File Offset: 0x004DFD1B
		public AssetRepository Assets { get; private set; }

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001F79 RID: 8057 RVA: 0x004E1B24 File Offset: 0x004DFD24
		// (set) Token: 0x06001F7A RID: 8058 RVA: 0x004E1B2C File Offset: 0x004DFD2C
		public IContentSource RootContentSource { get; private set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001F7B RID: 8059 RVA: 0x004E1B35 File Offset: 0x004DFD35
		public short NetID
		{
			get
			{
				return this.netID;
			}
		}

		/// <summary> If true, this mod has a <see cref="P:Terraria.ModLoader.Mod.NetID" /> assigned. This is mainly useful for checking if a <see cref="F:Terraria.ModLoader.ModSide.NoSync" /> mod is present on the server from a client to determine if a <see cref="T:Terraria.ModLoader.ModPacket" /> can be sent to the server or not. </summary>
		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x004E1B3D File Offset: 0x004DFD3D
		public bool IsNetSynced
		{
			get
			{
				return this.netID >= 0;
			}
		}

		/// <inheritdoc cref="T:Terraria.ModLoader.PreJITFilter" />
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x004E1B4B File Offset: 0x004DFD4B
		// (set) Token: 0x06001F7E RID: 8062 RVA: 0x004E1B53 File Offset: 0x004DFD53
		public PreJITFilter PreJITFilter { get; protected set; } = new PreJITFilter();

		// Token: 0x06001F7F RID: 8063 RVA: 0x004E1B5C File Offset: 0x004DFD5C
		public Mod()
		{
			this.Content = new ContentCache(this);
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x004E1BC8 File Offset: 0x004DFDC8
		internal void AutoloadConfig()
		{
			if (this.Code == null)
			{
				return;
			}
			foreach (Type type2 in from type in AssemblyManager.GetLoadableTypes(this.Code)
			orderby type.FullName
			select type)
			{
				if (!type2.IsAbstract && type2.IsSubclassOf(typeof(ModConfig)))
				{
					ModConfig mc = (ModConfig)Activator.CreateInstance(type2, true);
					if (mc.Mode == ConfigScope.ServerSide && (this.Side == ModSide.Client || this.Side == ModSide.NoSync))
					{
						throw new Exception("The ModConfig " + mc.Name + " can't be loaded because the config is ServerSide but this Mods ModSide isn't Both or Server");
					}
					if (mc.Mode == ConfigScope.ClientSide && this.Side == ModSide.Server)
					{
						throw new Exception("The ModConfig " + mc.Name + " can't be loaded because the config is ClientSide but this Mods ModSide is Server");
					}
					mc.Mod = this;
					string name = type2.Name;
					if (mc.Autoload(ref name))
					{
						this.AddConfig(name, mc);
					}
				}
			}
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x004E1CFC File Offset: 0x004DFEFC
		public void AddConfig(string name, ModConfig mc)
		{
			mc.Name = name;
			mc.Mod = this;
			ConfigManager.Add(mc);
			ContentInstance.Register(mc);
			ModTypeLookup<ModConfig>.Register(mc);
		}

		/// <summary> Call this to manually add a content instance of the specified type (with a parameterless constructor) to the game. </summary>
		/// <returns> true if the instance was successfully added </returns>
		// Token: 0x06001F82 RID: 8066 RVA: 0x004E1D1E File Offset: 0x004DFF1E
		public bool AddContent<T>() where T : ILoadable, new()
		{
			return this.AddContent(Activator.CreateInstance<T>());
		}

		/// <summary> Call this to manually add the given content instance to the game. </summary>
		/// <param name="instance"> The content instance to add </param>
		/// <returns> true if the instance was successfully added </returns>
		// Token: 0x06001F83 RID: 8067 RVA: 0x004E1D30 File Offset: 0x004DFF30
		public bool AddContent(ILoadable instance)
		{
			if (!this.loading)
			{
				throw new Exception(Language.GetTextValue("tModLoader.LoadErrorNotLoading"));
			}
			if (!instance.IsLoadingEnabled(this))
			{
				return false;
			}
			instance.Load(this);
			this.Content.Add(instance);
			ContentInstance.Register(instance);
			return true;
		}

		/// <summary>
		/// Returns all registered content instances that are added by this mod.
		/// <br />This only includes the 'template' instance for each piece of content, not all the clones/new instances which get added to Items/Players/NPCs etc. as the game is played
		/// </summary>
		// Token: 0x06001F84 RID: 8068 RVA: 0x004E1D6F File Offset: 0x004DFF6F
		public IEnumerable<ILoadable> GetContent()
		{
			return this.Content.GetContent();
		}

		/// <summary>
		/// Returns all registered content instances that derive from the provided type that are added by this mod.
		/// <br />This only includes the 'template' instance for each piece of content, not all the clones/new instances which get added to Items/Players/NPCs etc. as the game is played
		/// </summary>
		// Token: 0x06001F85 RID: 8069 RVA: 0x004E1D7C File Offset: 0x004DFF7C
		public IEnumerable<T> GetContent<T>() where T : ILoadable
		{
			return this.Content.GetContent<T>();
		}

		/// <summary> Attempts to find the template instance from this mod with the specified name (not the clone/new instance which gets added to Items/Players/NPCs etc. as the game is played). Caching the result is recommended.<para />This will throw exceptions on failure. </summary>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException" />
		// Token: 0x06001F86 RID: 8070 RVA: 0x004E1D89 File Offset: 0x004DFF89
		public T Find<T>(string name) where T : IModType
		{
			return ModContent.Find<T>(this.Name, name);
		}

		/// <summary> Safely attempts to find the template instance from this mod with the specified name (not the clone/new instance which gets added to Items/Players/NPCs etc. as the game is played). Caching the result is recommended. </summary>
		/// <returns> Whether or not the requested instance has been found. </returns>
		// Token: 0x06001F87 RID: 8071 RVA: 0x004E1D97 File Offset: 0x004DFF97
		public bool TryFind<T>(string name, out T value) where T : IModType
		{
			return ModContent.TryFind<T>(this.Name, name, out value);
		}

		/// <summary>
		/// Creates a localization key following the pattern of "Mods.{ModName}.{suffix}". Use this with <see cref="M:Terraria.Localization.Language.GetOrRegister(System.String,System.Func{System.String})" /> to retrieve a <see cref="T:Terraria.Localization.LocalizedText" /> for custom localization keys. Alternatively <see cref="M:Terraria.ModLoader.Mod.GetLocalization(System.String,System.Func{System.String})" /> can be used directly instead. Custom localization keys need to be registered during the mod loading process to appear automatically in the localization files.
		/// </summary>
		/// <param name="suffix"></param>
		/// <returns></returns>
		// Token: 0x06001F88 RID: 8072 RVA: 0x004E1DA6 File Offset: 0x004DFFA6
		public string GetLocalizationKey(string suffix)
		{
			return "Mods." + this.Name + "." + suffix;
		}

		/// <summary>
		/// Returns a <see cref="T:Terraria.Localization.LocalizedText" /> for this Mod with the provided <paramref name="suffix" />. The suffix will be used to generate a key by providing it to <see cref="M:Terraria.ModLoader.Mod.GetLocalizationKey(System.String)" />.
		/// <br />If no existing localization exists for the key, it will be defined so it can be exported to a matching mod localization file.
		/// </summary>
		/// <param name="suffix"></param>
		/// <param name="makeDefaultValue">A factory method for creating the default value, used to update localization files with missing entries</param>
		/// <returns></returns>
		// Token: 0x06001F89 RID: 8073 RVA: 0x004E1DBE File Offset: 0x004DFFBE
		public LocalizedText GetLocalization(string suffix, Func<string> makeDefaultValue = null)
		{
			return Language.GetOrRegister(this.GetLocalizationKey(suffix), makeDefaultValue);
		}

		/// <summary>
		/// Assigns a head texture to the given town NPC type.
		/// </summary>
		/// <param name="npcType">Type of the NPC.</param>
		/// <param name="texture">The texture.</param>
		/// <returns>The boss head texture slot</returns>
		/// <exception cref="T:Terraria.ModLoader.Exceptions.MissingResourceException"></exception>
		// Token: 0x06001F8A RID: 8074 RVA: 0x004E1DD0 File Offset: 0x004DFFD0
		public int AddNPCHeadTexture(int npcType, string texture)
		{
			if (!this.loading)
			{
				throw new Exception(Language.GetTextValue("tModLoader.LoadErrorNotLoading"));
			}
			int slot = NPCHeadLoader.ReserveHeadSlot();
			NPCHeadLoader.heads[texture] = slot;
			NPCHeadID.Search.Add(texture, slot);
			if (!Main.dedServ)
			{
				ModContent.Request<Texture2D>(texture, 2);
			}
			NPCHeadLoader.npcToHead[npcType] = slot;
			return slot;
		}

		/// <summary>
		/// Assigns a head texture that can be used by NPCs on the map.
		/// </summary>
		/// <param name="texture">The texture.</param>
		/// <param name="npcType">An optional npc id for NPCID.Sets.BossHeadTextures</param>
		/// <returns>The boss head texture slot</returns>
		// Token: 0x06001F8B RID: 8075 RVA: 0x004E1E30 File Offset: 0x004E0030
		public int AddBossHeadTexture(string texture, int npcType = -1)
		{
			if (!this.loading)
			{
				throw new Exception(Language.GetTextValue("tModLoader.LoadErrorNotLoading"));
			}
			int slot = NPCHeadLoader.ReserveBossHeadSlot(texture);
			NPCHeadLoader.bossHeads[texture] = slot;
			ModContent.Request<Texture2D>(texture, 2);
			if (npcType >= 0)
			{
				NPCHeadLoader.npcToBossHead[npcType] = slot;
			}
			return slot;
		}

		/// <summary>
		/// Retrieves the names of every file packaged into this mod.
		/// Note that this includes extensions, and for images the extension will always be <c>.rawimg</c>.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06001F8C RID: 8076 RVA: 0x004E1E81 File Offset: 0x004E0081
		public List<string> GetFileNames()
		{
			TmodFile file = this.File;
			if (file == null)
			{
				return null;
			}
			return file.GetFileNames();
		}

		/// <summary>
		/// Retrieves the contents of a file packaged within the .tmod file as a byte array. Should be used mainly for non-<see cref="T:ReLogic.Content.Asset`1" /> files. The <paramref name="name" /> should be in the format of "Folders/FileNameWithExtension" starting from your mod's source code folder. Returns null if the file does not exist within the mod.
		/// <para /> A typical usage of this might be to load a text file containing structured data included within your mod. Make sure the txt file is UTF8 encoded and use the following to retrieve file's text contents: <c>string pointsFileContents = Encoding.UTF8.GetString(Mod.GetFileBytes("data/points.txt"));</c>
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		// Token: 0x06001F8D RID: 8077 RVA: 0x004E1E94 File Offset: 0x004E0094
		public byte[] GetFileBytes(string name)
		{
			TmodFile file = this.File;
			if (file == null)
			{
				return null;
			}
			return file.GetBytes(name);
		}

		/// <summary>
		/// Retrieve contents of files within the .tmod file.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="newFileStream"></param>
		/// <returns></returns>
		// Token: 0x06001F8E RID: 8078 RVA: 0x004E1EA8 File Offset: 0x004E00A8
		public Stream GetFileStream(string name, bool newFileStream = false)
		{
			TmodFile file = this.File;
			if (file == null)
			{
				return null;
			}
			return file.GetStream(name, newFileStream);
		}

		/// <summary>
		/// Returns whether or not a file with the specified name exists. Note that this includes file extension and the folder path: "Folders/FileNameWithExtension"
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		// Token: 0x06001F8F RID: 8079 RVA: 0x004E1EBD File Offset: 0x004E00BD
		public bool FileExists(string name)
		{
			return this.File != null && this.File.HasFile(name);
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x004E1ED5 File Offset: 0x004E00D5
		public bool HasAsset(string assetName)
		{
			return this.RootContentSource.HasAsset(assetName);
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x004E1EE3 File Offset: 0x004E00E3
		public bool RequestAssetIfExists<T>(string assetName, out Asset<T> asset) where T : class
		{
			if (!this.HasAsset(assetName))
			{
				asset = null;
				return false;
			}
			asset = this.Assets.Request<T>(assetName);
			return true;
		}

		/// <summary>
		/// Used for weak inter-mod communication. This allows you to interact with other mods without having to reference their types or namespaces, provided that they have implemented this method.<br />
		/// The <see href="https://github.com/tModLoader/tModLoader/wiki/Expert-Cross-Mod-Content">Expert Cross Mod Content Guide</see> explains how to use this hook to implement and utilize cross-mod capabilities.
		/// </summary>
		// Token: 0x06001F92 RID: 8082 RVA: 0x004E1F02 File Offset: 0x004E0102
		public virtual object Call(params object[] args)
		{
			return null;
		}

		/// <summary>
		/// Creates a <see cref="T:Terraria.ModLoader.ModPacket" /> object that you can write to and then send between servers and clients.
		/// </summary>
		/// <param name="capacity">The capacity.</param>
		/// <returns></returns>
		/// <exception cref="T:System.Exception">Cannot get packet for " + Name + " because it does not exist on the other side</exception>
		// Token: 0x06001F93 RID: 8083 RVA: 0x004E1F08 File Offset: 0x004E0108
		public ModPacket GetPacket(int capacity = 256)
		{
			if (this.netID >= 0)
			{
				ModPacket p = new ModPacket(250, capacity + 5);
				if (ModNet.NetModCount < 256)
				{
					p.Write((byte)this.netID);
				}
				else
				{
					p.Write(this.netID);
				}
				p.netID = this.netID;
				return p;
			}
			if (Main.netMode == 0)
			{
				throw new Exception("GetPacket should only be called during multiplayer");
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(125, 2);
			defaultInterpolatedStringHandler.AppendLiteral("Cannot get packet for ");
			defaultInterpolatedStringHandler.AppendFormatted(this.Name);
			defaultInterpolatedStringHandler.AppendLiteral(" because it does not exist on the ");
			defaultInterpolatedStringHandler.AppendFormatted(Main.dedServ ? "client" : "server");
			defaultInterpolatedStringHandler.AppendLiteral(". GetPacket should not be called for server-side or client-side mods.");
			throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
		}

		// Token: 0x06001F94 RID: 8084 RVA: 0x004E1FD4 File Offset: 0x004E01D4
		public ModConfig GetConfig(string name)
		{
			List<ModConfig> configs;
			if (ConfigManager.Configs.TryGetValue(this, out configs))
			{
				return configs.Single((ModConfig x) => x.Name == name);
			}
			return null;
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x004E2011 File Offset: 0x004E0211
		[Obsolete("Use Recipe.Create", true)]
		public Recipe CreateRecipe(int result, int amount = 1)
		{
			return Recipe.Create(result, amount);
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x004E201A File Offset: 0x004E021A
		[Obsolete("Use Recipe.Clone", true)]
		public Recipe CloneRecipe(Recipe recipe)
		{
			return recipe.Clone();
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x004E2022 File Offset: 0x004E0222
		public virtual IContentSource CreateDefaultContentSource()
		{
			return new TModContentSource(this.File);
		}

		/// <summary>
		/// Override this method to run code after all content has been autoloaded. Here additional content can be manually loaded and Mod-wide tasks and setup can be done. For organization, it may be more suitable to split some things into various <see cref="M:Terraria.ModLoader.ModType.Load" /> methods, such as in <see cref="T:Terraria.ModLoader.ModSystem" /> classes, instead of doing everything here. <br />
		/// Beware that mod content has not finished loading here, things like ModContent lookup tables or ID Sets are not fully populated. Use <see cref="M:Terraria.ModLoader.Mod.PostSetupContent" /> for any logic that needs to act on all content being fully loaded.
		/// </summary>
		// Token: 0x06001F98 RID: 8088 RVA: 0x004E202F File Offset: 0x004E022F
		public virtual void Load()
		{
		}

		/// <summary>
		/// Allows you to load things in your mod after its content has been setup (arrays have been resized to fit the content, etc).
		/// </summary>
		// Token: 0x06001F99 RID: 8089 RVA: 0x004E2031 File Offset: 0x004E0231
		public virtual void PostSetupContent()
		{
		}

		/// <summary>
		/// This is called whenever this mod is unloaded from the game. Use it to undo changes that you've made in Load that aren't automatically handled (for example, modifying the texture of a vanilla item). Mods are guaranteed to be unloaded in the reverse order they were loaded in.
		/// </summary>
		// Token: 0x06001F9A RID: 8090 RVA: 0x004E2033 File Offset: 0x004E0233
		public virtual void Unload()
		{
		}

		/// <summary>
		/// The amount of extra buff slots this mod desires for Players. This value is checked after Mod.Load but before Mod.PostSetupContent. The actual number of buffs the player can use will be 22 plus the max value of all enabled mods. In-game use Player.MaxBuffs to check the maximum number of buffs.
		/// </summary>
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x004E2035 File Offset: 0x004E0235
		public virtual uint ExtraPlayerBuffSlots { get; }

		/// <summary>
		/// Override this method to add recipe groups to this mod. You must add recipe groups by calling the RecipeGroup.RegisterGroup method here. A recipe group is a set of items that can be used interchangeably in the same recipe.
		/// </summary>
		// Token: 0x06001F9C RID: 8092 RVA: 0x004E203D File Offset: 0x004E023D
		[Obsolete("Use ModSystem.AddRecipeGroups", true)]
		public virtual void AddRecipeGroups()
		{
		}

		/// <summary>
		/// Override this method to add recipes to the game. It is recommended that you do so through instances of Recipe, since it provides methods that simplify recipe creation.
		/// </summary>
		// Token: 0x06001F9D RID: 8093 RVA: 0x004E203F File Offset: 0x004E023F
		[Obsolete("Use ModSystem.AddRecipes", true)]
		public virtual void AddRecipes()
		{
		}

		/// <summary>
		/// This provides a hook into the mod-loading process immediately after recipes have been added. You can use this to edit recipes added by other mods.
		/// </summary>
		// Token: 0x06001F9E RID: 8094 RVA: 0x004E2041 File Offset: 0x004E0241
		[Obsolete("Use ModSystem.PostAddRecipes", true)]
		public virtual void PostAddRecipes()
		{
		}

		/// <summary>
		/// Close is called before Unload, and may be called at any time when mod unloading is imminent (such as when downloading an update, or recompiling)
		/// Use this to release any additional file handles, or stop streaming music.
		/// Make sure to call `base.Close()` at the end
		/// May be called multiple times before Unload
		/// </summary>
		// Token: 0x06001F9F RID: 8095 RVA: 0x004E2044 File Offset: 0x004E0244
		public virtual void Close()
		{
			MusicLoader.CloseModStreams(this);
			IDisposable disposable = this.fileHandle;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			if (this.File != null && this.File.IsOpen)
			{
				throw new IOException("TModFile has open handles: " + this.File.path);
			}
		}

		/// <summary>
		/// Called whenever a net message / packet pertaining to this mod is received from a client (if this is a server) or the server (if this is a client). whoAmI is the ID of whomever sent the packet (equivalent to the Main.myPlayer of the sender), and reader is used to read the binary data of the packet. <para />
		/// Note that many packets are sent from a client to the server and then relayed to the remaining clients. The whoAmI when the packet arrives at the remaining clients will be the servers <see cref="F:Terraria.Main.myPlayer" />, not the original clients <see cref="F:Terraria.Main.myPlayer" />. For packets only sent from a client to the server, relying on <paramref name="whoAmI" /> to identify the clients player is fine, but for packets that are relayed, the clients player index will need to be part of the packet itself to correctly identify the client that sent the original packet. Use <c>packet.Write((byte) Main.myPlayer);</c> to write and <c>int player = reader.ReadByte();</c> to read. <para />
		/// The <see cref="M:Terraria.ModLoader.ModSystem.HijackGetData(System.Byte@,System.IO.BinaryReader@,System.Int32)" /> hook can be used to intercept any packet used by Terraria.
		/// </summary>
		/// <param name="reader">The reader.</param>
		/// <param name="whoAmI">The player the message is from. Only relevant for server code. For clients it will always be 255, the server. For the server it will be the whoAmI of the client.</param>
		// Token: 0x06001FA0 RID: 8096 RVA: 0x004E2098 File Offset: 0x004E0298
		public virtual void HandlePacket(BinaryReader reader, int whoAmI)
		{
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06001FA1 RID: 8097 RVA: 0x004E209A File Offset: 0x004E029A
		// (set) Token: 0x06001FA2 RID: 8098 RVA: 0x004E20A2 File Offset: 0x004E02A2
		internal ContentCache Content { get; private set; }

		// Token: 0x06001FA3 RID: 8099 RVA: 0x004E20AB File Offset: 0x004E02AB
		internal void SetupContent()
		{
			LoaderUtils.ForEachAndAggregateExceptions<ModType>(this.GetContent<ModType>(), delegate(ModType e)
			{
				e.SetupContent();
			});
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x004E20D8 File Offset: 0x004E02D8
		internal void UnloadContent()
		{
			SystemLoader.OnModUnload(this);
			this.Unload();
			foreach (ILoadable loadable in this.GetContent().Reverse<ILoadable>())
			{
				loadable.Unload();
			}
			this.Content.Clear();
			this.Content = null;
			this.equipTextures.Clear();
			AssetRepository assets = this.Assets;
			if (assets == null)
			{
				return;
			}
			assets.Dispose();
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x004E2160 File Offset: 0x004E0360
		internal void Autoload()
		{
			if (this.Code == null)
			{
				return;
			}
			LocalizationLoader.Autoload(this);
			this.ModSourceBestiaryInfoElement = new ModSourceBestiaryInfoElement(this, this.DisplayName);
			if (this.ContentAutoloadingEnabled)
			{
				LoaderUtils.ForEachAndAggregateExceptions<Type>((from t in AssemblyManager.GetLoadableTypes(this.Code)
				where !t.IsAbstract && !t.ContainsGenericParameters
				where t.IsAssignableTo(typeof(ILoadable))
				where t.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, Type.EmptyTypes) != null
				where AutoloadAttribute.GetValue(t).NeedsAutoloading
				select t).OrderBy((Type type) => type.FullName, StringComparer.InvariantCulture), delegate(Type t)
				{
					this.AddContent((ILoadable)Activator.CreateInstance(t, true));
				});
			}
			if (Main.dedServ)
			{
				return;
			}
			if (this.GoreAutoloadingEnabled)
			{
				GoreLoader.AutoloadGores(this);
			}
			if (this.CloudAutoloadingEnabled)
			{
				CloudLoader.AutoloadClouds(this);
			}
			if (this.MusicAutoloadingEnabled)
			{
				MusicLoader.AutoloadMusic(this);
			}
			if (this.BackgroundAutoloadingEnabled)
			{
				BackgroundTextureLoader.AutoloadBackgrounds(this);
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x004E22B4 File Offset: 0x004E04B4
		internal void PrepareAssets()
		{
			TmodFile file = this.File;
			this.fileHandle = ((file != null) ? file.Open() : null);
			this.RootContentSource = this.CreateDefaultContentSource();
			this.Assets = new AssetRepository(XnaExtensions.Get<AssetReaderCollection>(Main.instance.Services), new IContentSource[]
			{
				this.RootContentSource
			})
			{
				AssetLoadFailHandler = new FailedToLoadAssetCustomAction(this.OnceFailedLoadingAnAsset)
			};
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x004E2320 File Offset: 0x004E0520
		internal void TransferAllAssets()
		{
			Interface.loadMods.SubProgressText = Language.GetTextValue("tModLoader.MSFinishingResourceLoading");
			this.Assets.TransferAllAssets();
			this.initialTransferComplete = true;
			LoaderUtils.RethrowAggregatedExceptions(this.AssetExceptions);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x004E2354 File Offset: 0x004E0554
		internal void OnceFailedLoadingAnAsset(string assetPath, Exception e)
		{
			if (this.initialTransferComplete)
			{
				Logging.Terraria.Error("Failed to load asset: \"" + assetPath + "\"", e);
				FancyErrorPrinter.ShowFailedToLoadAssetError(e, assetPath);
				return;
			}
			if (e is AssetLoadException)
			{
				IEnumerable<string> enumerable = this.RootContentSource.EnumerateAssets().ToList<string>();
				List<string> cleanKeys = new List<string>();
				foreach (string key in enumerable)
				{
					string keyWithoutExtension = key.Substring(0, key.LastIndexOf("."));
					string extension = this.RootContentSource.GetExtension(keyWithoutExtension);
					if (extension != null)
					{
						cleanKeys.Add(key.Substring(0, key.LastIndexOf(extension)));
					}
				}
				List<string> reasons = new List<string>();
				this.RootContentSource.Rejections.TryGetRejections(reasons);
				MissingResourceException MissingResourceException = new MissingResourceException(reasons, assetPath.Replace("\\", "/"), cleanKeys);
				this.AssetExceptions.Add(MissingResourceException);
				return;
			}
			this.AssetExceptions.Add(e);
		}

		// Token: 0x04001693 RID: 5779
		private string displayNameClean;

		// Token: 0x04001696 RID: 5782
		internal short netID = -1;

		// Token: 0x04001697 RID: 5783
		private IDisposable fileHandle;

		// Token: 0x04001698 RID: 5784
		public ModSourceBestiaryInfoElement ModSourceBestiaryInfoElement;

		// Token: 0x0400169B RID: 5787
		internal bool loading;

		// Token: 0x0400169C RID: 5788
		internal readonly IDictionary<Tuple<string, EquipType>, EquipTexture> equipTextures = new Dictionary<Tuple<string, EquipType>, EquipTexture>();

		// Token: 0x0400169E RID: 5790
		internal bool initialTransferComplete;

		// Token: 0x0400169F RID: 5791
		internal List<Exception> AssetExceptions = new List<Exception>();
	}
}
