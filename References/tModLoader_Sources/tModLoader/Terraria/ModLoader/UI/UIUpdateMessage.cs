using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Ionic.Zip;
using Microsoft.Xna.Framework;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader.UI.DownloadManager;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.Social;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000261 RID: 609
	internal class UIUpdateMessage : UIState
	{
		// Token: 0x06002AC0 RID: 10944 RVA: 0x0051E490 File Offset: 0x0051C690
		public override void OnInitialize()
		{
			UIElement uielement = new UIElement();
			uielement.Width.Percent = 0.8f;
			uielement.Top.Pixels = 200f;
			uielement.Height.Pixels = -240f;
			uielement.Height.Percent = 1f;
			uielement.HAlign = 0.5f;
			this._area = uielement;
			this._message.Width.Percent = 1f;
			this._message.Height.Percent = 0.8f;
			this._message.HAlign = 0.5f;
			this._area.Append(this._message);
			UITextPanel<string> uitextPanel = new UITextPanel<string>("Ignore", 0.7f, true);
			uitextPanel.Width.Pixels = -10f;
			uitextPanel.Width.Percent = 0.33333334f;
			uitextPanel.Height.Pixels = 50f;
			uitextPanel.VAlign = 1f;
			uitextPanel.Top.Pixels = -30f;
			UITextPanel<string> button = uitextPanel;
			button.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			button.OnLeftClick += this.IgnoreClick;
			this._area.Append(button);
			UITextPanel<string> button2 = new UITextPanel<string>("Download", 0.7f, true);
			button2.CopyStyle(button);
			button2.HAlign = 0.5f;
			button2.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			button2.OnLeftClick += this.OpenURL;
			this._area.Append(button2);
			if (Platform.IsWindows && SocialAPI.Mode != SocialMode.Steam)
			{
				this._autoUpdateButton = new UITextPanel<string>("Auto Update", 0.7f, true);
				this._autoUpdateButton.CopyStyle(button);
				this._autoUpdateButton.HAlign = 1f;
				this._autoUpdateButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
				this._autoUpdateButton.OnLeftClick += this.AutoUpdate;
			}
			base.Append(this._area);
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x0051E6E6 File Offset: 0x0051C8E6
		public override void OnActivate()
		{
			base.OnActivate();
			if (Platform.IsWindows && SocialAPI.Mode != SocialMode.Steam && UIModBrowser.PlatformSupportsTls12)
			{
				this._area.AddOrRemoveChild(this._autoUpdateButton, !string.IsNullOrEmpty(this._autoUpdateUrl));
			}
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x0051E723 File Offset: 0x0051C923
		internal void SetMessage(string text)
		{
			this._message.SetText(text);
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x0051E731 File Offset: 0x0051C931
		internal void SetGotoMenu(int gotoMenu)
		{
			this._gotoMenu = gotoMenu;
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x0051E73A File Offset: 0x0051C93A
		internal void SetURL(string url)
		{
			this._url = url;
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x0051E743 File Offset: 0x0051C943
		internal void SetAutoUpdateURL(string autoUpdateURL)
		{
			this._autoUpdateUrl = autoUpdateURL;
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x0051E74C File Offset: 0x0051C94C
		private void IgnoreClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.menuMode = this._gotoMenu;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x0051E76E File Offset: 0x0051C96E
		private void OpenURL(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Process.Start(this._url);
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x0051E794 File Offset: 0x0051C994
		private void AutoUpdate(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			string installDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			string zipFileName = Path.GetFileName(new Uri(this._autoUpdateUrl).LocalPath);
			string zipFilePath = Path.Combine(installDirectory, zipFileName);
			Logging.tML.Info("AutoUpdate: " + this._autoUpdateUrl + " -> " + zipFilePath);
			DownloadFile downloadFile = new DownloadFile(this._autoUpdateUrl, zipFilePath, "Auto update: " + zipFileName);
			downloadFile.OnComplete += delegate()
			{
				UIUpdateMessage.OnAutoUpdateDownloadComplete(installDirectory, zipFilePath);
			};
			Interface.downloadProgress.gotoMenu = 10007;
			Interface.downloadProgress.HandleDownloads(new DownloadFile[]
			{
				downloadFile
			});
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x0051E870 File Offset: 0x0051CA70
		private static void OnAutoUpdateDownloadComplete(string installDirectory, string zipFilePath)
		{
			try
			{
				string updateScriptName = Platform.IsWindows ? "update.bat" : "update.sh";
				string updateScript = Path.Combine(installDirectory, updateScriptName);
				Logging.tML.Info("Writing Script: " + updateScriptName);
				using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Terraria.ModLoader.Core." + updateScriptName))
				{
					using (FileStream fs = File.OpenWrite(updateScript))
					{
						stream.CopyTo(fs);
					}
				}
				if (Platform.IsWindows)
				{
					string extractDir = Path.Combine(installDirectory, "tModLoader_Update");
					if (Directory.Exists(extractDir))
					{
						Directory.Delete(extractDir, true);
					}
					Directory.CreateDirectory(extractDir);
					Logging.tML.Info("Extracting: " + zipFilePath + " -> " + extractDir);
					using (ZipFile zip = ZipFile.Read(zipFilePath))
					{
						zip.ExtractAll(extractDir);
					}
					File.Delete(zipFilePath);
					string executableName = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
					Logging.tML.Info("Renaming Terraria.exe -> " + executableName);
					File.Move(Path.Combine(extractDir, "Terraria.exe"), Path.Combine(extractDir, executableName));
					Process.Start(updateScript, "\"" + executableName + "\" tModLoader_Update");
				}
				else
				{
					Process.Start("bash", "-c 'chmod a+x \"" + updateScript + "\"'").WaitForExit();
					string fileName = updateScript;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler.AppendFormatted<int>(Process.GetCurrentProcess().Id);
					defaultInterpolatedStringHandler.AppendLiteral(" \"");
					defaultInterpolatedStringHandler.AppendFormatted(zipFilePath);
					defaultInterpolatedStringHandler.AppendLiteral("\"");
					Process.Start(fileName, defaultInterpolatedStringHandler.ToStringAndClear());
				}
				Logging.tML.Info("AutoUpdate script started. Exiting");
				Interface.downloadProgress.gotoMenu = 10026;
				Main.menuMode = 10026;
			}
			catch (Exception e)
			{
				Logging.tML.Error("Problem during autoupdate", e);
			}
		}

		// Token: 0x04001B4F RID: 6991
		private readonly UIMessageBox _message = new UIMessageBox("");

		// Token: 0x04001B50 RID: 6992
		private UIElement _area;

		// Token: 0x04001B51 RID: 6993
		private UITextPanel<string> _autoUpdateButton;

		// Token: 0x04001B52 RID: 6994
		private int _gotoMenu;

		// Token: 0x04001B53 RID: 6995
		private string _url;

		// Token: 0x04001B54 RID: 6996
		private string _autoUpdateUrl;
	}
}
