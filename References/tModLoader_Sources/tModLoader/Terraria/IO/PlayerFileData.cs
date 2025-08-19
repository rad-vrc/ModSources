using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Terraria.ModLoader.Exceptions;
using Terraria.ModLoader.IO;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria.IO
{
	// Token: 0x020003E0 RID: 992
	public class PlayerFileData : FileData
	{
		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x060033FF RID: 13311 RVA: 0x005563F7 File Offset: 0x005545F7
		// (set) Token: 0x06003400 RID: 13312 RVA: 0x005563FF File Offset: 0x005545FF
		public Player Player
		{
			get
			{
				return this._player;
			}
			set
			{
				this._player = value;
				if (value != null)
				{
					this.Name = this._player.name;
				}
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x0055641C File Offset: 0x0055461C
		// (set) Token: 0x06003402 RID: 13314 RVA: 0x00556424 File Offset: 0x00554624
		public bool ServerSideCharacter { get; private set; }

		// Token: 0x06003403 RID: 13315 RVA: 0x0055642D File Offset: 0x0055462D
		public PlayerFileData() : base("Player")
		{
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x00556450 File Offset: 0x00554650
		public PlayerFileData(string path, bool cloudSave) : base("Player", path, cloudSave)
		{
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x00556478 File Offset: 0x00554678
		public static PlayerFileData CreateAndSave(Player player)
		{
			PlayerFileData playerFileData = new PlayerFileData();
			playerFileData.Metadata = FileMetadata.FromCurrentSettings(FileType.Player);
			playerFileData.Player = player;
			playerFileData._isCloudSave = (SocialAPI.Cloud != null && SocialAPI.Cloud.EnabledByDefault);
			playerFileData._path = Main.GetPlayerPathFromName(player.name, playerFileData.IsCloudSave);
			(playerFileData.IsCloudSave ? Main.CloudFavoritesData : Main.LocalFavoriteData).ClearEntry(playerFileData);
			Player.SavePlayer(playerFileData, true);
			return playerFileData;
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x005564F1 File Offset: 0x005546F1
		public override void SetAsActive()
		{
			Main.ActivePlayerFileData = this;
			Main.player[Main.myPlayer] = this.Player;
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x0055650A File Offset: 0x0055470A
		public void MarkAsServerSide()
		{
			this.ServerSideCharacter = true;
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x00556514 File Offset: 0x00554714
		public override void MoveToCloud()
		{
			if (base.IsCloudSave || SocialAPI.Cloud == null)
			{
				return;
			}
			string playerPathFromName = Main.GetPlayerPathFromName(this.Name, true);
			if (!FileUtilities.MoveToCloud(base.Path, playerPathFromName))
			{
				return;
			}
			PlayerIO.MoveToCloud(base.Path, playerPathFromName);
			string fileName = base.GetFileName(false);
			string path = Main.PlayerPath + System.IO.Path.DirectorySeparatorChar.ToString() + fileName + System.IO.Path.DirectorySeparatorChar.ToString();
			if (Directory.Exists(path))
			{
				string[] files = Directory.GetFiles(path);
				for (int i = 0; i < files.Length; i++)
				{
					string cloudPath = string.Concat(new string[]
					{
						Main.CloudPlayerPath,
						"/",
						fileName,
						"/",
						FileUtilities.GetFileName(files[i], true)
					});
					FileUtilities.MoveToCloud(files[i], cloudPath);
				}
			}
			Main.LocalFavoriteData.ClearEntry(this);
			this._isCloudSave = true;
			this._path = playerPathFromName;
			Main.CloudFavoritesData.SaveFavorite(this);
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x00556608 File Offset: 0x00554808
		public override void MoveToLocal()
		{
			if (!base.IsCloudSave || SocialAPI.Cloud == null)
			{
				return;
			}
			string playerPathFromName = Main.GetPlayerPathFromName(this.Name, false);
			if (!FileUtilities.MoveToLocal(base.Path, playerPathFromName))
			{
				return;
			}
			PlayerIO.MoveToLocal(base.Path, playerPathFromName);
			string fileName = base.GetFileName(false);
			string mapPath = System.IO.Path.Combine(Main.CloudPlayerPath, fileName);
			IEnumerable<string> source = SocialAPI.Cloud.GetFiles().ToList<string>();
			Func<string, bool> <>9__0;
			Func<string, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((string path) => this.MapBelongsToPath(mapPath, path)));
			}
			foreach (string item in source.Where(predicate).ToList<string>())
			{
				string localPath = System.IO.Path.Combine(Main.PlayerPath, fileName, FileUtilities.GetFileName(item, true));
				FileUtilities.MoveToLocal(item, localPath);
			}
			Main.CloudFavoritesData.ClearEntry(this);
			this._isCloudSave = false;
			this._path = playerPathFromName;
			Main.LocalFavoriteData.SaveFavorite(this);
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x0055672C File Offset: 0x0055492C
		private bool MapBelongsToPath(string mapPath, string filePath)
		{
			if (!filePath.EndsWith(".map", StringComparison.CurrentCultureIgnoreCase) && !filePath.EndsWith(".tmap", StringComparison.CurrentCultureIgnoreCase))
			{
				return false;
			}
			string value = mapPath.Replace('\\', '/');
			return filePath.StartsWith(value, StringComparison.CurrentCultureIgnoreCase);
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x0055676C File Offset: 0x0055496C
		public void UpdatePlayTimer()
		{
			bool flag = Main.gamePaused && !Main.hasFocus;
			bool flag2 = Main.instance.IsActive && !flag;
			if (Main.gameMenu)
			{
				flag2 = false;
			}
			if (flag2)
			{
				this.StartPlayTimer();
				return;
			}
			this.PausePlayTimer();
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x005567B9 File Offset: 0x005549B9
		public void StartPlayTimer()
		{
			if (!this._isTimerActive)
			{
				this._isTimerActive = true;
				if (!this._timer.IsRunning)
				{
					this._timer.Start();
				}
			}
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x005567E2 File Offset: 0x005549E2
		public void PausePlayTimer()
		{
			this.StopPlayTimer();
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x005567EA File Offset: 0x005549EA
		public TimeSpan GetPlayTime()
		{
			if (this._timer.IsRunning)
			{
				return this._playTime + this._timer.Elapsed;
			}
			return this._playTime;
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x00556818 File Offset: 0x00554A18
		public void UpdatePlayTimerAndKeepState()
		{
			bool isRunning = this._timer.IsRunning;
			this._playTime += this._timer.Elapsed;
			this._timer.Reset();
			if (isRunning)
			{
				this._timer.Start();
			}
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x00556864 File Offset: 0x00554A64
		public void StopPlayTimer()
		{
			if (this._isTimerActive)
			{
				this._isTimerActive = false;
				if (this._timer.IsRunning)
				{
					this._playTime += this._timer.Elapsed;
					this._timer.Reset();
				}
			}
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x005568B4 File Offset: 0x00554AB4
		public void SetPlayTime(TimeSpan time)
		{
			this._playTime = time;
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x005568BD File Offset: 0x00554ABD
		public void Rename(string newName)
		{
			if (this.Player != null)
			{
				this.Player.name = newName.Trim();
			}
			Player.SavePlayer(this, false);
		}

		// Token: 0x04001E73 RID: 7795
		private Player _player;

		// Token: 0x04001E74 RID: 7796
		private TimeSpan _playTime = TimeSpan.Zero;

		// Token: 0x04001E75 RID: 7797
		private readonly Stopwatch _timer = new Stopwatch();

		// Token: 0x04001E76 RID: 7798
		private bool _isTimerActive;

		// Token: 0x04001E77 RID: 7799
		public CustomModDataException customDataFail;
	}
}
