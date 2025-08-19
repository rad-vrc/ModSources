using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using ReLogic.Utilities;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace Terraria.IO
{
	// Token: 0x020003E7 RID: 999
	public class WorldFileData : FileData
	{
		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x0055DBC8 File Offset: 0x0055BDC8
		public string SeedText
		{
			get
			{
				return this._seedText;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x0055DBD0 File Offset: 0x0055BDD0
		public int Seed
		{
			get
			{
				return this._seed;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x0055DBD8 File Offset: 0x0055BDD8
		public string WorldSizeName
		{
			get
			{
				return this._worldSizeName.Value;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06003490 RID: 13456 RVA: 0x0055DBE5 File Offset: 0x0055BDE5
		// (set) Token: 0x06003491 RID: 13457 RVA: 0x0055DBF0 File Offset: 0x0055BDF0
		public bool HasCrimson
		{
			get
			{
				return !this.HasCorruption;
			}
			set
			{
				this.HasCorruption = !value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x0055DBFC File Offset: 0x0055BDFC
		public bool HasValidSeed
		{
			get
			{
				return this.WorldGeneratorVersion > 0UL;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x0055DC08 File Offset: 0x0055BE08
		public bool UseGuidAsMapName
		{
			get
			{
				return this.WorldGeneratorVersion >= 777389080577UL;
			}
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x0055DC20 File Offset: 0x0055BE20
		public string GetWorldName(bool allowCropping = false)
		{
			string text = this.Name;
			if (text == null)
			{
				return text;
			}
			if (allowCropping)
			{
				int num = 530;
				text = FontAssets.MouseText.Value.CreateCroppedText(text, (float)num);
			}
			return text;
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x0055DC58 File Offset: 0x0055BE58
		public string GetFullSeedText(bool allowCropping = false)
		{
			int num = 0;
			if (this.WorldSizeX == 4200 && this.WorldSizeY == 1200)
			{
				num = 1;
			}
			if (this.WorldSizeX == 6400 && this.WorldSizeY == 1800)
			{
				num = 2;
			}
			if (this.WorldSizeX == 8400 && this.WorldSizeY == 2400)
			{
				num = 3;
			}
			int num2 = 0;
			if (this.HasCorruption)
			{
				num2 = 1;
			}
			if (this.HasCrimson)
			{
				num2 = 2;
			}
			int num3 = this.GameMode + 1;
			string text = this._seedText;
			if (allowCropping)
			{
				int num4 = 340;
				text = FontAssets.MouseText.Value.CreateCroppedText(text, (float)num4);
			}
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 4);
			defaultInterpolatedStringHandler.AppendFormatted<int>(num);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted<int>(num3);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted<int>(num2);
			defaultInterpolatedStringHandler.AppendLiteral(".");
			defaultInterpolatedStringHandler.AppendFormatted(text);
			return defaultInterpolatedStringHandler.ToStringAndClear();
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x0055DD53 File Offset: 0x0055BF53
		public WorldFileData() : base("World")
		{
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x0055DD8F File Offset: 0x0055BF8F
		public WorldFileData(string path, bool cloudSave) : base("World", path, cloudSave)
		{
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x0055DDCD File Offset: 0x0055BFCD
		public override void SetAsActive()
		{
			Main.ActiveWorldFileData = this;
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x0055DDD8 File Offset: 0x0055BFD8
		public void SetWorldSize(int x, int y)
		{
			this.WorldSizeX = x;
			this.WorldSizeY = y;
			if (x == 4200)
			{
				this._worldSizeName = Language.GetText("UI.WorldSizeSmall");
				return;
			}
			if (x == 6400)
			{
				this._worldSizeName = Language.GetText("UI.WorldSizeMedium");
				return;
			}
			if (x != 8400)
			{
				this._worldSizeName = Language.GetText("UI.WorldSizeUnknown");
				return;
			}
			this._worldSizeName = Language.GetText("UI.WorldSizeLarge");
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x0055DE50 File Offset: 0x0055C050
		public static WorldFileData FromInvalidWorld(string path, bool cloudSave)
		{
			WorldFileData worldFileData = new WorldFileData(path, cloudSave);
			worldFileData.GameMode = 0;
			worldFileData.SetSeedToEmpty();
			worldFileData.WorldGeneratorVersion = 0UL;
			worldFileData.Metadata = FileMetadata.FromCurrentSettings(FileType.World);
			worldFileData.SetWorldSize(1, 1);
			worldFileData.HasCorruption = true;
			worldFileData.IsHardMode = false;
			worldFileData.IsValid = false;
			worldFileData.Name = FileUtilities.GetFileName(path, false);
			worldFileData.UniqueId = Guid.Empty;
			if (!cloudSave)
			{
				worldFileData.CreationTime = File.GetCreationTime(path);
			}
			else
			{
				worldFileData.CreationTime = DateTime.Now;
			}
			return worldFileData;
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x0055DED8 File Offset: 0x0055C0D8
		public void SetSeedToEmpty()
		{
			this.SetSeed("");
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x0055DEE8 File Offset: 0x0055C0E8
		public void SetSeed(string seedText)
		{
			this._seedText = seedText;
			WorldGen.currentWorldSeed = seedText;
			if (!int.TryParse(seedText, out this._seed))
			{
				this._seed = Crc32.Calculate(seedText);
			}
			this._seed = ((this._seed == int.MinValue) ? int.MaxValue : Math.Abs(this._seed));
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x0055DF44 File Offset: 0x0055C144
		public void SetSeedToRandom()
		{
			this.SetSeed(new UnifiedRandom().Next().ToString());
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x0055DF6C File Offset: 0x0055C16C
		public override void MoveToCloud()
		{
			if (!base.IsCloudSave)
			{
				string worldPathFromName = Main.GetWorldPathFromName(this.Name, true);
				if (FileUtilities.MoveToCloud(base.Path, worldPathFromName))
				{
					WorldIO.MoveToCloud(base.Path, worldPathFromName);
					Main.LocalFavoriteData.ClearEntry(this);
					this._isCloudSave = true;
					this._path = worldPathFromName;
					Main.CloudFavoritesData.SaveFavorite(this);
				}
			}
		}

		// Token: 0x0600349F RID: 13471 RVA: 0x0055DFCC File Offset: 0x0055C1CC
		public override void MoveToLocal()
		{
			if (base.IsCloudSave)
			{
				string worldPathFromName = Main.GetWorldPathFromName(this.Name, false);
				if (FileUtilities.MoveToLocal(base.Path, worldPathFromName))
				{
					WorldIO.MoveToLocal(base.Path, worldPathFromName);
					Main.CloudFavoritesData.ClearEntry(this);
					this._isCloudSave = false;
					this._path = worldPathFromName;
					Main.LocalFavoriteData.SaveFavorite(this);
				}
			}
		}

		// Token: 0x060034A0 RID: 13472 RVA: 0x0055E02C File Offset: 0x0055C22C
		public void Rename(string newDisplayName)
		{
			if (newDisplayName != null)
			{
				WorldGen.RenameWorld(this, newDisplayName, new Action<string>(this.OnWorldRenameSuccess));
			}
		}

		// Token: 0x060034A1 RID: 13473 RVA: 0x0055E044 File Offset: 0x0055C244
		public void CopyToLocal(string newFileName = null, string newDisplayName = null)
		{
			if (!base.IsCloudSave)
			{
				if (newFileName == null)
				{
					newFileName = Guid.NewGuid().ToString();
				}
				string worldPathFromName = Main.GetWorldPathFromName(newFileName, false);
				FileUtilities.Copy(base.Path, worldPathFromName, false, true);
				this._path = worldPathFromName;
				if (newDisplayName != null)
				{
					WorldGen.RenameWorld(this, newDisplayName, new Action<string>(this.OnWorldRenameSuccess));
				}
			}
		}

		// Token: 0x060034A2 RID: 13474 RVA: 0x0055E0A4 File Offset: 0x0055C2A4
		private void OnWorldRenameSuccess(string newWorldName)
		{
			this.Name = newWorldName;
			Main.DelayedProcesses.Add(this.DelayedGoToTitleScreen());
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x0055E0BD File Offset: 0x0055C2BD
		private IEnumerator DelayedGoToTitleScreen()
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.menuMode = 0;
			yield break;
		}

		/// <summary>
		/// Retrieves the version that the specified mod was at when the world was generated. <see langword="false" /> will be returned for mods that were not enabled when the world was generated.<para />
		/// The feature tracking which mods were used to generate a world was added in v2023.8, so modders should first check <see cref="P:Terraria.IO.WorldFileData.WorldGenModsRecorded" /> to see if the mods used to generate the world were recorded at all.
		/// </summary>
		// Token: 0x060034A4 RID: 13476 RVA: 0x0055E0C5 File Offset: 0x0055C2C5
		public bool TryGetModVersionGeneratedWith(string mod, out Version modVersion)
		{
			return this.modVersionsDuringWorldGen.TryGetValue(mod, out modVersion);
		}

		/// <summary>
		/// If <see langword="true" />, the mods used to generate this world have been saved and their version can be retrieved using <see cref="M:Terraria.IO.WorldFileData.TryGetModVersionGeneratedWith(System.String,System.Version@)" />.<para />
		/// If <see langword="false" />, this world was generated before the feature tracking mods used to generate a world was added (v2023.8) and modders can't determine if a specific mod was enabled when the world was generated.
		/// </summary>
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x060034A5 RID: 13477 RVA: 0x0055E0D4 File Offset: 0x0055C2D4
		public bool WorldGenModsRecorded
		{
			get
			{
				return this.modVersionsDuringWorldGen != null;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x060034A6 RID: 13478 RVA: 0x0055E0DF File Offset: 0x0055C2DF
		// (set) Token: 0x060034A7 RID: 13479 RVA: 0x0055E0E7 File Offset: 0x0055C2E7
		internal Dictionary<string, TagCompound> ModHeaders { get; set; } = new Dictionary<string, TagCompound>();

		// Token: 0x060034A8 RID: 13480 RVA: 0x0055E0F0 File Offset: 0x0055C2F0
		public bool TryGetHeaderData<T>(out TagCompound data) where T : ModSystem
		{
			return this.TryGetHeaderData(ModContent.GetInstance<T>(), out data);
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x0055E103 File Offset: 0x0055C303
		public bool TryGetHeaderData(ModSystem system, out TagCompound data)
		{
			return this.ModHeaders.TryGetValue(system.FullName, out data);
		}

		/// <summary> Contains error messages from ModSystem.SaveWorldData from a previous world save retrieved from the .twld during load or the latest autosave. Will be shown in various places to warn the user. Maps ModSystem.FullName.MethodName to exception string.</summary>
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x060034AA RID: 13482 RVA: 0x0055E117 File Offset: 0x0055C317
		// (set) Token: 0x060034AB RID: 13483 RVA: 0x0055E11F File Offset: 0x0055C31F
		internal Dictionary<string, string> ModSaveErrors { get; set; } = new Dictionary<string, string>();

		// Token: 0x04001EC3 RID: 7875
		private const ulong GUID_IN_WORLD_FILE_VERSION = 777389080577UL;

		// Token: 0x04001EC4 RID: 7876
		public DateTime CreationTime;

		// Token: 0x04001EC5 RID: 7877
		public int WorldSizeX;

		// Token: 0x04001EC6 RID: 7878
		public int WorldSizeY;

		// Token: 0x04001EC7 RID: 7879
		public ulong WorldGeneratorVersion;

		// Token: 0x04001EC8 RID: 7880
		private string _seedText = "";

		// Token: 0x04001EC9 RID: 7881
		private int _seed;

		// Token: 0x04001ECA RID: 7882
		public bool IsValid = true;

		// Token: 0x04001ECB RID: 7883
		public Guid UniqueId;

		// Token: 0x04001ECC RID: 7884
		public LocalizedText _worldSizeName;

		// Token: 0x04001ECD RID: 7885
		public int GameMode;

		// Token: 0x04001ECE RID: 7886
		public bool DrunkWorld;

		// Token: 0x04001ECF RID: 7887
		public bool NotTheBees;

		// Token: 0x04001ED0 RID: 7888
		public bool ForTheWorthy;

		// Token: 0x04001ED1 RID: 7889
		public bool Anniversary;

		// Token: 0x04001ED2 RID: 7890
		public bool DontStarve;

		// Token: 0x04001ED3 RID: 7891
		public bool RemixWorld;

		// Token: 0x04001ED4 RID: 7892
		public bool NoTrapsWorld;

		// Token: 0x04001ED5 RID: 7893
		public bool ZenithWorld;

		// Token: 0x04001ED6 RID: 7894
		public bool HasCorruption = true;

		// Token: 0x04001ED7 RID: 7895
		public bool IsHardMode;

		// Token: 0x04001ED8 RID: 7896
		public bool DefeatedMoonlord;

		// Token: 0x04001ED9 RID: 7897
		internal IList<string> usedMods;

		// Token: 0x04001EDA RID: 7898
		internal string modPack;

		// Token: 0x04001EDB RID: 7899
		internal Dictionary<string, Version> modVersionsDuringWorldGen;
	}
}
