using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria.IO
{
	// Token: 0x020000D8 RID: 216
	public class PlayerFileData : FileData
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x004AE999 File Offset: 0x004ACB99
		// (set) Token: 0x060014DE RID: 5342 RVA: 0x004AE9A1 File Offset: 0x004ACBA1
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

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x004AE9BE File Offset: 0x004ACBBE
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x004AE9C6 File Offset: 0x004ACBC6
		public bool ServerSideCharacter { get; private set; }

		// Token: 0x060014E1 RID: 5345 RVA: 0x004AE9CF File Offset: 0x004ACBCF
		public PlayerFileData() : base("Player")
		{
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x004AE9F2 File Offset: 0x004ACBF2
		public PlayerFileData(string path, bool cloudSave) : base("Player", path, cloudSave)
		{
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x004AEA18 File Offset: 0x004ACC18
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

		// Token: 0x060014E4 RID: 5348 RVA: 0x004AEA91 File Offset: 0x004ACC91
		public override void SetAsActive()
		{
			Main.ActivePlayerFileData = this;
			Main.player[Main.myPlayer] = this.Player;
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x004AEAAA File Offset: 0x004ACCAA
		public void MarkAsServerSide()
		{
			this.ServerSideCharacter = true;
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x004AEAB4 File Offset: 0x004ACCB4
		public override void MoveToCloud()
		{
			if (base.IsCloudSave || SocialAPI.Cloud == null)
			{
				return;
			}
			string playerPathFromName = Main.GetPlayerPathFromName(this.Name, true);
			if (FileUtilities.MoveToCloud(base.Path, playerPathFromName))
			{
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
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x004AEBA8 File Offset: 0x004ACDA8
		public override void MoveToLocal()
		{
			if (!base.IsCloudSave || SocialAPI.Cloud == null)
			{
				return;
			}
			string playerPathFromName = Main.GetPlayerPathFromName(this.Name, false);
			if (FileUtilities.MoveToLocal(base.Path, playerPathFromName))
			{
				string fileName = base.GetFileName(false);
				string mapPath = System.IO.Path.Combine(Main.CloudPlayerPath, fileName);
				foreach (string text in (from path in SocialAPI.Cloud.GetFiles().ToList<string>()
				where this.MapBelongsToPath(mapPath, path)
				select path).ToList<string>())
				{
					string localPath = System.IO.Path.Combine(Main.PlayerPath, fileName, FileUtilities.GetFileName(text, true));
					FileUtilities.MoveToLocal(text, localPath);
				}
				Main.CloudFavoritesData.ClearEntry(this);
				this._isCloudSave = false;
				this._path = playerPathFromName;
				Main.LocalFavoriteData.SaveFavorite(this);
			}
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x004AECAC File Offset: 0x004ACEAC
		private bool MapBelongsToPath(string mapPath, string filePath)
		{
			if (!filePath.EndsWith(".map", StringComparison.CurrentCultureIgnoreCase))
			{
				return false;
			}
			string value = mapPath.Replace('\\', '/');
			return filePath.StartsWith(value, StringComparison.CurrentCultureIgnoreCase);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x004AECDC File Offset: 0x004ACEDC
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

		// Token: 0x060014EA RID: 5354 RVA: 0x004AED29 File Offset: 0x004ACF29
		public void StartPlayTimer()
		{
			if (this._isTimerActive)
			{
				return;
			}
			this._isTimerActive = true;
			if (!this._timer.IsRunning)
			{
				this._timer.Start();
			}
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x004AED53 File Offset: 0x004ACF53
		public void PausePlayTimer()
		{
			this.StopPlayTimer();
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x004AED5B File Offset: 0x004ACF5B
		public TimeSpan GetPlayTime()
		{
			if (this._timer.IsRunning)
			{
				return this._playTime + this._timer.Elapsed;
			}
			return this._playTime;
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x004AED88 File Offset: 0x004ACF88
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

		// Token: 0x060014EE RID: 5358 RVA: 0x004AEDD4 File Offset: 0x004ACFD4
		public void StopPlayTimer()
		{
			if (!this._isTimerActive)
			{
				return;
			}
			this._isTimerActive = false;
			if (this._timer.IsRunning)
			{
				this._playTime += this._timer.Elapsed;
				this._timer.Reset();
			}
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x004AEE25 File Offset: 0x004AD025
		public void SetPlayTime(TimeSpan time)
		{
			this._playTime = time;
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x004AEE2E File Offset: 0x004AD02E
		public void Rename(string newName)
		{
			if (this.Player != null)
			{
				this.Player.name = newName.Trim();
			}
			Player.SavePlayer(this, false);
		}

		// Token: 0x0400126B RID: 4715
		private Player _player;

		// Token: 0x0400126C RID: 4716
		private TimeSpan _playTime = TimeSpan.Zero;

		// Token: 0x0400126D RID: 4717
		private readonly Stopwatch _timer = new Stopwatch();

		// Token: 0x0400126E RID: 4718
		private bool _isTimerActive;
	}
}
