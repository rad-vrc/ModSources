using System;
using System.Collections;
using System.IO;
using ReLogic.Utilities;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.Utilities;

namespace Terraria.IO
{
	// Token: 0x020000D9 RID: 217
	public class WorldFileData : FileData
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x004AEE50 File Offset: 0x004AD050
		public string SeedText
		{
			get
			{
				return this._seedText;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x004AEE58 File Offset: 0x004AD058
		public int Seed
		{
			get
			{
				return this._seed;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x004AEE60 File Offset: 0x004AD060
		public string WorldSizeName
		{
			get
			{
				return this._worldSizeName.Value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x004AEE6D File Offset: 0x004AD06D
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x004AEE78 File Offset: 0x004AD078
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

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x004AEE84 File Offset: 0x004AD084
		public bool HasValidSeed
		{
			get
			{
				return this.WorldGeneratorVersion > 0UL;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x004AEE90 File Offset: 0x004AD090
		public bool UseGuidAsMapName
		{
			get
			{
				return this.WorldGeneratorVersion >= 777389080577UL;
			}
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x004AEEA8 File Offset: 0x004AD0A8
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

		// Token: 0x060014F9 RID: 5369 RVA: 0x004AEEE0 File Offset: 0x004AD0E0
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
			return string.Format("{0}.{1}.{2}.{3}", new object[]
			{
				num,
				num3,
				num2,
				text
			});
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x004AEFB6 File Offset: 0x004AD1B6
		public WorldFileData() : base("World")
		{
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x004AEFDC File Offset: 0x004AD1DC
		public WorldFileData(string path, bool cloudSave) : base("World", path, cloudSave)
		{
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x004AF004 File Offset: 0x004AD204
		public override void SetAsActive()
		{
			Main.ActiveWorldFileData = this;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x004AF00C File Offset: 0x004AD20C
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

		// Token: 0x060014FE RID: 5374 RVA: 0x004AF084 File Offset: 0x004AD284
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

		// Token: 0x060014FF RID: 5375 RVA: 0x004AF10C File Offset: 0x004AD30C
		public void SetSeedToEmpty()
		{
			this.SetSeed("");
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x004AF11C File Offset: 0x004AD31C
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

		// Token: 0x06001501 RID: 5377 RVA: 0x004AF178 File Offset: 0x004AD378
		public void SetSeedToRandom()
		{
			this.SetSeed(new UnifiedRandom().Next().ToString());
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x004AF1A0 File Offset: 0x004AD3A0
		public override void MoveToCloud()
		{
			if (base.IsCloudSave)
			{
				return;
			}
			string worldPathFromName = Main.GetWorldPathFromName(this.Name, true);
			if (FileUtilities.MoveToCloud(base.Path, worldPathFromName))
			{
				Main.LocalFavoriteData.ClearEntry(this);
				this._isCloudSave = true;
				this._path = worldPathFromName;
				Main.CloudFavoritesData.SaveFavorite(this);
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x004AF1F8 File Offset: 0x004AD3F8
		public override void MoveToLocal()
		{
			if (!base.IsCloudSave)
			{
				return;
			}
			string worldPathFromName = Main.GetWorldPathFromName(this.Name, false);
			if (FileUtilities.MoveToLocal(base.Path, worldPathFromName))
			{
				Main.CloudFavoritesData.ClearEntry(this);
				this._isCloudSave = false;
				this._path = worldPathFromName;
				Main.LocalFavoriteData.SaveFavorite(this);
			}
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x004AF24D File Offset: 0x004AD44D
		public void Rename(string newDisplayName)
		{
			if (newDisplayName == null)
			{
				return;
			}
			WorldGen.RenameWorld(this, newDisplayName, new Action<string>(this.OnWorldRenameSuccess));
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x004AF268 File Offset: 0x004AD468
		public void CopyToLocal(string newFileName = null, string newDisplayName = null)
		{
			if (base.IsCloudSave)
			{
				return;
			}
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

		// Token: 0x06001506 RID: 5382 RVA: 0x004AF2C9 File Offset: 0x004AD4C9
		private void OnWorldRenameSuccess(string newWorldName)
		{
			this.Name = newWorldName;
			Main.DelayedProcesses.Add(this.DelayedGoToTitleScreen());
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x004AF2E2 File Offset: 0x004AD4E2
		private IEnumerator DelayedGoToTitleScreen()
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.menuMode = 0;
			yield break;
		}

		// Token: 0x04001270 RID: 4720
		private const ulong GUID_IN_WORLD_FILE_VERSION = 777389080577UL;

		// Token: 0x04001271 RID: 4721
		public DateTime CreationTime;

		// Token: 0x04001272 RID: 4722
		public int WorldSizeX;

		// Token: 0x04001273 RID: 4723
		public int WorldSizeY;

		// Token: 0x04001274 RID: 4724
		public ulong WorldGeneratorVersion;

		// Token: 0x04001275 RID: 4725
		private string _seedText = "";

		// Token: 0x04001276 RID: 4726
		private int _seed;

		// Token: 0x04001277 RID: 4727
		public bool IsValid = true;

		// Token: 0x04001278 RID: 4728
		public Guid UniqueId;

		// Token: 0x04001279 RID: 4729
		public LocalizedText _worldSizeName;

		// Token: 0x0400127A RID: 4730
		public int GameMode;

		// Token: 0x0400127B RID: 4731
		public bool DrunkWorld;

		// Token: 0x0400127C RID: 4732
		public bool NotTheBees;

		// Token: 0x0400127D RID: 4733
		public bool ForTheWorthy;

		// Token: 0x0400127E RID: 4734
		public bool Anniversary;

		// Token: 0x0400127F RID: 4735
		public bool DontStarve;

		// Token: 0x04001280 RID: 4736
		public bool RemixWorld;

		// Token: 0x04001281 RID: 4737
		public bool NoTrapsWorld;

		// Token: 0x04001282 RID: 4738
		public bool ZenithWorld;

		// Token: 0x04001283 RID: 4739
		public bool HasCorruption = true;

		// Token: 0x04001284 RID: 4740
		public bool IsHardMode;

		// Token: 0x04001285 RID: 4741
		public bool DefeatedMoonlord;
	}
}
