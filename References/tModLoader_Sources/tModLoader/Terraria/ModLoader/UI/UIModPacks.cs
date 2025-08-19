using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zip;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.Localization;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Core;
using Terraria.Social.Base;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.Utilities;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000253 RID: 595
	internal class UIModPacks : UIState, IHaveBackButtonCommand
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x00518F39 File Offset: 0x00517139
		// (set) Token: 0x06002A15 RID: 10773 RVA: 0x00518F41 File Offset: 0x00517141
		public UIState PreviousUIState { get; set; }

		// Token: 0x06002A16 RID: 10774 RVA: 0x00518F4A File Offset: 0x0051714A
		public static string ModPackModsPath(string packName)
		{
			return Path.Combine(UIModPacks.ModPacksDirectory, packName, "Mods");
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x00518F5C File Offset: 0x0051715C
		public static string ModPackConfigPath(string packName)
		{
			return Path.Combine(UIModPacks.ModPacksDirectory, packName, "ModConfigs");
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x00518F70 File Offset: 0x00517170
		public override void OnInitialize()
		{
			UIElement uielement = new UIElement();
			uielement.Width.Percent = 0.8f;
			uielement.MaxWidth = UICommon.MaxPanelWidth;
			uielement.Top.Pixels = 220f;
			uielement.Height.Pixels = -220f;
			uielement.Height.Percent = 1f;
			uielement.HAlign = 0.5f;
			UIElement uIElement = uielement;
			this._uiLoader = new UILoaderAnimatedImage(0.5f, 0.5f, 1f);
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Percent = 1f;
			uipanel.Height.Pixels = -65f;
			uipanel.Height.Percent = 0.9f;
			uipanel.BackgroundColor = UICommon.MainPanelBackground;
			this._scrollPanel = uipanel;
			uIElement.Append(this._scrollPanel);
			UIList uilist = new UIList();
			uilist.Width.Pixels = -25f;
			uilist.Width.Percent = 1f;
			uilist.Height.Percent = 0.9f;
			uilist.ListPadding = 5f;
			this._modPacks = uilist;
			this._scrollPanel.Append(this._modPacks);
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.Height.Percent = 0.9f;
			uiscrollbar.HAlign = 1f;
			UIScrollbar uIScrollbar = uiscrollbar.WithView(100f, 1000f);
			this._scrollPanel.Append(uIScrollbar);
			this._modPacks.SetScrollbar(uIScrollbar);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("tModLoader.ModPacksHeader"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Pixels = -35f;
			uitextPanel.BackgroundColor = UICommon.DefaultUIBlue;
			UITextPanel<LocalizedText> titleTextPanel = uitextPanel.WithPadding(15f);
			uIElement.Append(titleTextPanel);
			UIAutoScaleTextTextPanel<LocalizedText> uiautoScaleTextTextPanel = new UIAutoScaleTextTextPanel<LocalizedText>(Language.GetText("tModLoader.OpenModPackFolder"), 1f, false);
			uiautoScaleTextTextPanel.Width = new StyleDimension(-10f, 0.5f);
			uiautoScaleTextTextPanel.Height.Pixels = 40f;
			uiautoScaleTextTextPanel.VAlign = 0.9f;
			uiautoScaleTextTextPanel.HAlign = 0f;
			uiautoScaleTextTextPanel.Top.Pixels = -20f;
			UIAutoScaleTextTextPanel<LocalizedText> folderButton = uiautoScaleTextTextPanel.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			folderButton.OnLeftClick += this.OpenFolder;
			uIElement.Append(folderButton);
			UIAutoScaleTextTextPanel<LocalizedText> uiautoScaleTextTextPanel2 = new UIAutoScaleTextTextPanel<LocalizedText>(Language.GetText("UI.Back"), 1f, false);
			uiautoScaleTextTextPanel2.Width = new StyleDimension(-10f, 0.5f);
			uiautoScaleTextTextPanel2.Height.Pixels = 40f;
			uiautoScaleTextTextPanel2.VAlign = 1f;
			uiautoScaleTextTextPanel2.HAlign = 0f;
			uiautoScaleTextTextPanel2.Top.Pixels = -20f;
			UIAutoScaleTextTextPanel<LocalizedText> backButton = uiautoScaleTextTextPanel2.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			backButton.OnLeftClick += this.BackClick;
			uIElement.Append(backButton);
			UIAutoScaleTextTextPanel<LocalizedText> saveNewButton = new UIAutoScaleTextTextPanel<LocalizedText>(Language.GetText("tModLoader.ModPacksSaveEnabledAsNewPack"), 1f, false);
			saveNewButton.CopyStyle(backButton);
			saveNewButton.TextColor = Color.Green;
			saveNewButton.VAlign = 1f;
			saveNewButton.HAlign = 1f;
			saveNewButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			UIElement uielement2 = saveNewButton;
			UIElement.MouseEvent value;
			if ((value = UIModPacks.<>O.<0>__SaveNewModList) == null)
			{
				value = (UIModPacks.<>O.<0>__SaveNewModList = new UIElement.MouseEvent(UIModPacks.SaveNewModList));
			}
			uielement2.OnLeftClick += value;
			uIElement.Append(saveNewButton);
			base.Append(uIElement);
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x00519328 File Offset: 0x00517528
		private static UIVirtualKeyboard VirtualKeyboard
		{
			get
			{
				UIVirtualKeyboard result;
				if ((result = UIModPacks._virtualKeyboard) == null)
				{
					string textValue = Language.GetTextValue("tModLoader.ModPacksEnterModPackName");
					string startingText = "";
					UIVirtualKeyboard.KeyboardSubmitEvent submitAction;
					if ((submitAction = UIModPacks.<>O.<1>__SaveModPack) == null)
					{
						submitAction = (UIModPacks.<>O.<1>__SaveModPack = new UIVirtualKeyboard.KeyboardSubmitEvent(UIModPacks.SaveModPack));
					}
					result = (UIModPacks._virtualKeyboard = new UIVirtualKeyboard(textValue, startingText, submitAction, delegate()
					{
						Main.menuMode = 10016;
					}, 0, false));
				}
				return result;
			}
		}

		// Token: 0x06002A1A RID: 10778 RVA: 0x00519394 File Offset: 0x00517594
		private static void SaveNewModList(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIModPacks.VirtualKeyboard.Text = "";
			Main.MenuUI.SetState(UIModPacks.VirtualKeyboard);
			Main.menuMode = 888;
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x005193E4 File Offset: 0x005175E4
		public static void SaveModPack(string filename)
		{
			if (!UIModPacks.IsValidModpackName(filename))
			{
				UIModPacks.VirtualKeyboard.Text = UIModPacks.SanitizeModpackName(filename);
				return;
			}
			string modsPath = UIModPacks.ModPackModsPath(filename);
			UIModPacks.SaveSnapshot(UIModPacks.ModPackConfigPath(filename), modsPath);
			Main.menuMode = 10016;
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x00519427 File Offset: 0x00517627
		private void BackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			((IHaveBackButtonCommand)this).HandleBackButtonUsage();
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x00519444 File Offset: 0x00517644
		private void OpenFolder(UIMouseEvent evt, UIElement listeningElement)
		{
			Utils.OpenFolder(UIModPacks.ModPacksDirectory);
		}

		// Token: 0x06002A1E RID: 10782 RVA: 0x00519450 File Offset: 0x00517650
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x0051945F File Offset: 0x0051765F
		internal static string SanitizeModpackName(string name)
		{
			return UIModPacks.ModPackRegex.Replace(name, string.Empty);
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x00519471 File Offset: 0x00517671
		internal static bool IsValidModpackName(string name)
		{
			return !UIModPacks.ModPackRegex.Match(name).Success && name.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x00519495 File Offset: 0x00517695
		public override void OnDeactivate()
		{
			CancellationTokenSource cts = this._cts;
			if (cts != null)
			{
				cts.Cancel(false);
			}
			CancellationTokenSource cts2 = this._cts;
			if (cts2 != null)
			{
				cts2.Dispose();
			}
			this._cts = null;
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x005194C1 File Offset: 0x005176C1
		public override void OnActivate()
		{
			this._cts = new CancellationTokenSource();
			this._scrollPanel.Append(this._uiLoader);
			this._modPacks.Clear();
			Task.Run(delegate()
			{
				Directory.CreateDirectory(UIModPacks.ModPacksDirectory);
				string[] dirs = Directory.GetDirectories(UIModPacks.ModPacksDirectory, "*", SearchOption.TopDirectoryOnly);
				IEnumerable<string> files = Directory.GetFiles(UIModPacks.ModPacksDirectory, "*.json", SearchOption.TopDirectoryOnly);
				List<UIElement> ModPacksToAdd = new List<UIElement>();
				foreach (string modPackPath in files.Concat(dirs))
				{
					try
					{
						if (!UIModPacks.IsValidModpackName(Path.GetFileNameWithoutExtension(modPackPath)))
						{
							throw new Exception();
						}
						if (Directory.Exists(modPackPath))
						{
							ModPacksToAdd.Add(this.LoadModernModPack(modPackPath));
						}
						else
						{
							ModPacksToAdd.Add(this.LoadLegacyModPack(modPackPath));
						}
					}
					catch
					{
						UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.ModPackMalformed", Path.GetFileName(modPackPath)), 1f, false);
						uiautoScaleTextTextPanel.Width.Percent = 1f;
						uiautoScaleTextTextPanel.Height.Pixels = 50f;
						uiautoScaleTextTextPanel.Height.Percent = 0f;
						UIAutoScaleTextTextPanel<string> badModPackMessage = uiautoScaleTextTextPanel;
						ModPacksToAdd.Add(badModPackMessage);
					}
				}
				Main.QueueMainThreadAction(delegate
				{
					this._modPacks.AddRange(ModPacksToAdd);
					this._scrollPanel.RemoveChild(this._uiLoader);
				});
			});
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x005194FC File Offset: 0x005176FC
		public UIModPackItem LoadModernModPack(string folderPath)
		{
			string[] modPackMods = JsonConvert.DeserializeObject<string[]>(File.ReadAllText(Path.Combine(folderPath, "Mods", "enabled.json")));
			if (modPackMods == null)
			{
				Utils.LogAndConsoleInfoMessage("No contents in enabled.json at: " + folderPath + ". Is this correct?");
				modPackMods = new string[0];
			}
			LocalMod[] localMods = ModOrganizer.FindMods(false);
			return new UIModPackItem(folderPath, modPackMods, false, localMods);
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x00519554 File Offset: 0x00517754
		public UIModPackItem LoadLegacyModPack(string jsonPath)
		{
			string[] modPackMods = JsonConvert.DeserializeObject<string[]>(File.ReadAllText(jsonPath));
			LocalMod[] localMods = ModOrganizer.FindMods(false);
			return new UIModPackItem(Path.GetFileNameWithoutExtension(jsonPath), modPackMods, true, localMods);
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x00519584 File Offset: 0x00517784
		public static void SaveSnapshot(string configsPath, string modsPath)
		{
			if (!Directory.Exists(ConfigManager.ModConfigPath))
			{
				Directory.CreateDirectory(ConfigManager.ModConfigPath);
			}
			Directory.CreateDirectory(configsPath);
			Directory.CreateDirectory(modsPath);
			Directory.EnumerateFiles(ConfigManager.ModConfigPath);
			File.Copy(Path.Combine(ModOrganizer.modPath, "enabled.json"), Path.Combine(modsPath, "enabled.json"), true);
			File.WriteAllText(Path.Combine(modsPath, "tmlversion.txt"), BuildInfo.tMLVersion.ToString());
			List<string> workshopIds = new List<string>();
			foreach (Mod mod in ModLoader.Mods)
			{
				if (mod.File != null)
				{
					FoundWorkshopEntryInfo info;
					if (ModOrganizer.TryReadManifest(ModOrganizer.GetParentDir(mod.File.path), out info))
					{
						workshopIds.Add(info.workshopEntryId.ToString());
					}
					if (mod.File.path != Path.Combine(modsPath, mod.Name + ".tmod"))
					{
						File.Copy(mod.File.path, Path.Combine(modsPath, mod.Name + ".tmod"), true);
					}
				}
			}
			File.WriteAllLines(Path.Combine(modsPath, "install.txt"), workshopIds);
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x005196B4 File Offset: 0x005178B4
		public static void ExportSnapshot(string modPackName)
		{
			string instancePath = Path.Combine(Directory.GetCurrentDirectory(), modPackName);
			Directory.CreateDirectory(instancePath);
			Directory.CreateDirectory(Path.Combine(instancePath, "SaveData"));
			string sourcePath = UIModPacks.ModPackModsPath(modPackName);
			string configPath = ConfigManager.ModConfigPath;
			FileUtilities.CopyFolder(sourcePath, Path.Combine(instancePath, "SaveData", "Mods"));
			FileUtilities.CopyFolder(configPath, Path.Combine(instancePath, "SaveData", "ModConfigs"));
			File.WriteAllText(Path.Combine(instancePath, "cli-argsConfig.txt"), "-tmlsavedirectory " + Path.Combine(instancePath, "SaveData") + "\n-steamworkshopfolder none");
			Logging.tML.Info("Exported instance of Frozen Mod Pack " + modPackName + " to " + instancePath);
			Utils.OpenFolder(instancePath);
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x00519768 File Offset: 0x00517968
		public static void ExtractTmlInstall(string instancePath)
		{
			string zipFilePath = Path.Combine(instancePath, "tModLoader.zip");
			using (ZipFile zip = ZipFile.Read(zipFilePath))
			{
				zip.ExtractAll(instancePath);
			}
			File.Delete(zipFilePath);
		}

		// Token: 0x04001AE4 RID: 6884
		internal static readonly Regex ModPackRegex = new Regex("(?:[^a-zA-Z0-9_.-]+)|(?:^(con|prn|aux|nul|com[1-9]|lpt[1-9])$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04001AE5 RID: 6885
		internal static string ModPacksDirectory = Path.Combine(ModLoader.ModPath, "ModPacks");

		// Token: 0x04001AE6 RID: 6886
		private UIList _modPacks;

		// Token: 0x04001AE7 RID: 6887
		private UILoaderAnimatedImage _uiLoader;

		// Token: 0x04001AE8 RID: 6888
		private UIPanel _scrollPanel;

		// Token: 0x04001AE9 RID: 6889
		private CancellationTokenSource _cts;

		// Token: 0x04001AEB RID: 6891
		private static UIVirtualKeyboard _virtualKeyboard;

		// Token: 0x02000A0B RID: 2571
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006C3A RID: 27706
			public static UIElement.MouseEvent <0>__SaveNewModList;

			// Token: 0x04006C3B RID: 27707
			public static UIVirtualKeyboard.KeyboardSubmitEvent <1>__SaveModPack;
		}
	}
}
