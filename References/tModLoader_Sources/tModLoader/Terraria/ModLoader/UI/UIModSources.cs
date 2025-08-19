using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200025B RID: 603
	internal class UIModSources : UIState, IHaveBackButtonCommand
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06002A6A RID: 10858 RVA: 0x0051C0EC File Offset: 0x0051A2EC
		// (set) Token: 0x06002A6B RID: 10859 RVA: 0x0051C0F4 File Offset: 0x0051A2F4
		public UIState PreviousUIState { get; set; }

		// Token: 0x06002A6C RID: 10860 RVA: 0x0051C100 File Offset: 0x0051A300
		public override void OnInitialize()
		{
			UIElement uielement = new UIElement();
			uielement.Width.Percent = 0.8f;
			uielement.MaxWidth = UICommon.MaxPanelWidth;
			uielement.Top.Pixels = 220f;
			uielement.Height.Pixels = -220f;
			uielement.Height.Percent = 1f;
			uielement.HAlign = 0.5f;
			this._uIElement = uielement;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Percent = 1f;
			uipanel.Height.Pixels = -65f;
			uipanel.Height.Percent = 1f;
			uipanel.BackgroundColor = UICommon.MainPanelBackground;
			uipanel.PaddingTop = 0f;
			this._uIPanel = uipanel;
			this._uIElement.Append(this._uIPanel);
			this._uiLoader = new UILoaderAnimatedImage(0.5f, 0.5f, 1f);
			UIElement uielement2 = new UIElement();
			uielement2.Width.Percent = 1f;
			uielement2.Height.Pixels = 82f;
			uielement2.Top.Pixels = 10f;
			UIElement upperMenuContainer = uielement2;
			UIPanel uipanel2 = new UIPanel();
			uipanel2.Top.Percent = 0f;
			uipanel2.Left.Pixels = -135f;
			uipanel2.Left.Percent = 1f;
			uipanel2.Width.Pixels = 135f;
			uipanel2.Height.Pixels = 32f;
			UIPanel filterTextBoxBackground = uipanel2;
			filterTextBoxBackground.OnRightClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.filterTextBox.Text = "";
			};
			upperMenuContainer.Append(filterTextBoxBackground);
			UIInputTextField uiinputTextField = new UIInputTextField(Language.GetTextValue("tModLoader.ModsTypeToSearch"));
			uiinputTextField.Top.Pixels = 5f;
			uiinputTextField.Left.Pixels = -125f;
			uiinputTextField.Left.Percent = 1f;
			uiinputTextField.Width.Pixels = 120f;
			uiinputTextField.Height.Pixels = 20f;
			this.filterTextBox = uiinputTextField;
			this.filterTextBox.OnRightClick += delegate(UIMouseEvent a, UIElement b)
			{
				this.filterTextBox.Text = "";
			};
			this.filterTextBox.OnTextChange += delegate(object a, EventArgs b)
			{
				this._updateNeeded = true;
			};
			upperMenuContainer.Append(this.filterTextBox);
			this._uIPanel.Append(upperMenuContainer);
			UIList uilist = new UIList();
			uilist.Width.Pixels = -25f;
			uilist.Width.Percent = 1f;
			uilist.Height.Pixels = -134f;
			uilist.Height.Percent = 1f;
			uilist.Top.Pixels = 134f;
			uilist.ListPadding = 5f;
			this._modList = uilist;
			this._uIPanel.Append(this._modList);
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.Height.Pixels = -134f;
			uiscrollbar.Height.Percent = 1f;
			uiscrollbar.Top.Pixels = 134f;
			uiscrollbar.HAlign = 1f;
			UIScrollbar uIScrollbar = uiscrollbar.WithView(100f, 1000f);
			this._uIPanel.Append(uIScrollbar);
			this._modList.SetScrollbar(uIScrollbar);
			UITextPanel<string> uitextPanel = new UITextPanel<string>(Language.GetTextValue("tModLoader.MenuModSources"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Pixels = -35f;
			uitextPanel.BackgroundColor = UICommon.DefaultUIBlue;
			UITextPanel<string> uIHeaderTextPanel = uitextPanel.WithPadding(15f);
			this._uIElement.Append(uIHeaderTextPanel);
			UIPanel uipanel3 = new UIPanel();
			uipanel3.Width.Percent = 1f;
			uipanel3.Height.Pixels = 78f;
			uipanel3.Top.Pixels = 46f;
			this._links = uipanel3;
			this._links.SetPadding(8f);
			this._uIPanel.Append(this._links);
			this.AddLink(Language.GetText("tModLoader.VersionUpgrade"), 0.5f, 0f, "https://github.com/tModLoader/tModLoader/wiki/Update-Migration-Guide");
			this.AddLink(Language.GetText("tModLoader.WikiLink"), 0f, 0.5f, "https://github.com/tModLoader/tModLoader/wiki/");
			string exampleModBranch = BuildInfo.IsStable ? "stable" : (BuildInfo.IsPreview ? "preview" : "1.4.4");
			this.AddLink(Language.GetText("tModLoader.ExampleModLink"), 1f, 0.5f, "https://github.com/tModLoader/tModLoader/tree/" + exampleModBranch + "/ExampleMod");
			string docsURL = BuildInfo.IsStable ? "stable" : "preview";
			this.AddLink(Language.GetText("tModLoader.DocumentationLink"), 0f, 1f, "https://docs.tmodloader.net/docs/" + docsURL + "/annotated.html");
			this.AddLink(Language.GetText("tModLoader.DiscordLink"), 1f, 1f, "https://tmodloader.net/discord");
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.MSBuildAll"), 1f, false);
			uiautoScaleTextTextPanel.Width.Pixels = -10f;
			uiautoScaleTextTextPanel.Width.Percent = 0.33333334f;
			uiautoScaleTextTextPanel.Height.Pixels = 40f;
			uiautoScaleTextTextPanel.VAlign = 1f;
			uiautoScaleTextTextPanel.Top.Pixels = -65f;
			UIAutoScaleTextTextPanel<string> buttonBA = uiautoScaleTextTextPanel;
			buttonBA.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			buttonBA.OnLeftClick += this.BuildMods;
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel2 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.MSBuildReloadAll"), 1f, false);
			uiautoScaleTextTextPanel2.CopyStyle(buttonBA);
			uiautoScaleTextTextPanel2.HAlign = 0.5f;
			uiautoScaleTextTextPanel2.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			uiautoScaleTextTextPanel2.OnLeftClick += this.BuildAndReload;
			UIAutoScaleTextTextPanel<string> buttonCreateMod = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.MSCreateMod"), 1f, false);
			buttonCreateMod.CopyStyle(buttonBA);
			buttonCreateMod.HAlign = 1f;
			buttonCreateMod.Top.Pixels = -20f;
			buttonCreateMod.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			buttonCreateMod.OnLeftClick += this.ButtonCreateMod_OnClick;
			this._uIElement.Append(buttonCreateMod);
			UIAutoScaleTextTextPanel<string> buttonB = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("UI.Back"), 1f, false);
			buttonB.CopyStyle(buttonBA);
			buttonB.Top.Pixels = -20f;
			buttonB.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			buttonB.OnLeftClick += this.BackClick;
			this._uIElement.Append(buttonB);
			UIAutoScaleTextTextPanel<string> buttonOS = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.MSOpenSources"), 1f, false);
			buttonOS.CopyStyle(buttonB);
			buttonOS.HAlign = 0.5f;
			buttonOS.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			buttonOS.OnLeftClick += this.OpenSources;
			this._uIElement.Append(buttonOS);
			base.Append(this._uIElement);
		}

		// Token: 0x06002A6D RID: 10861 RVA: 0x0051C858 File Offset: 0x0051AA58
		private void AddLink(LocalizedText text, float hAlign, float vAlign, string url)
		{
			UIText link = new UIText(text, 1f, false)
			{
				TextColor = Color.White,
				HAlign = hAlign,
				VAlign = vAlign
			};
			link.OnMouseOver += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(SoundID.MenuTick, null, null);
				link.TextColor = Main.OurFavoriteColor;
			};
			link.OnMouseOut += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				link.TextColor = Color.White;
			};
			link.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
			{
				SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
				Utils.OpenToURL(url);
			};
			this._links.Append(link);
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x0051C8F4 File Offset: 0x0051AAF4
		private void ButtonCreateMod_OnClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = 10025;
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x0051C915 File Offset: 0x0051AB15
		private void BackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			((IHaveBackButtonCommand)this).HandleBackButtonUsage();
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x0051C920 File Offset: 0x0051AB20
		private void OpenSources(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			try
			{
				Directory.CreateDirectory(ModCompile.ModSourcePath);
				Utils.OpenFolder(ModCompile.ModSourcePath);
			}
			catch (Exception e)
			{
				Logging.tML.Error(e);
			}
		}

		// Token: 0x06002A71 RID: 10865 RVA: 0x0051C978 File Offset: 0x0051AB78
		private void BuildMods(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			if (this._modList.Count > 0)
			{
				Interface.buildMod.BuildAll(false);
			}
		}

		// Token: 0x06002A72 RID: 10866 RVA: 0x0051C9A8 File Offset: 0x0051ABA8
		private void BuildAndReload(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			if (this._modList.Count > 0)
			{
				Interface.buildMod.BuildAll(true);
			}
		}

		// Token: 0x06002A73 RID: 10867 RVA: 0x0051C9D8 File Offset: 0x0051ABD8
		public override void Draw(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
			base.Draw(spriteBatch);
		}

		// Token: 0x06002A74 RID: 10868 RVA: 0x0051C9E8 File Offset: 0x0051ABE8
		public override void OnActivate()
		{
			this._cts = new CancellationTokenSource();
			Main.clrInput();
			ModCompile.UpdateReferencesFolder();
			this._uIPanel.Append(this._uiLoader);
			this._modList.Clear();
			this._items.Clear();
			if (this.ShowInfoMessages())
			{
				return;
			}
			this.Populate();
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x0051CA40 File Offset: 0x0051AC40
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
			this.modListViewPosition = this._modList.ViewPosition;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x0051CA80 File Offset: 0x0051AC80
		private bool ShowInfoMessages()
		{
			if (!ModLoader.SeenFirstLaunchModderWelcomeMessage)
			{
				this.ShowWelcomeMessage("tModLoader.MSFirstLaunchModderWelcomeMessage", "tModLoader.ViewOnGitHub", "https://github.com/tModLoader/tModLoader/wiki/tModLoader-guide-for-developers", 10001, null);
				ModLoader.SeenFirstLaunchModderWelcomeMessage = true;
				Main.SaveSettings();
				return true;
			}
			if (!UIModSources.IsCompatibleDotnetSdkAvailable())
			{
				if (UIModSources.IsRunningInSandbox())
				{
					Utils.ShowFancyErrorMessage(Language.GetTextValue("tModLoader.DevModsInSandbox"), 888, this.PreviousUIState);
				}
				else
				{
					this.ShowWelcomeMessage("tModLoader.MSNetSDKNotFound", "tModLoader.DownloadNetSDK", "https://github.com/tModLoader/tModLoader/wiki/tModLoader-guide-for-developers#net-sdk", 888, this.PreviousUIState);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x0051CB0C File Offset: 0x0051AD0C
		private void ShowWelcomeMessage(string messageKey, string altButtonTextKey, string url, int gotoMenu = 10001, UIState state = null)
		{
			string dotnetVersion = Environment.Version.MajorMinor().ToString();
			Interface.infoMessage.Show(Language.GetTextValue(messageKey, dotnetVersion), gotoMenu, state, Language.GetTextValue(altButtonTextKey, dotnetVersion), delegate
			{
				Utils.OpenToURL(url);
			}, null);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x0051CB60 File Offset: 0x0051AD60
		private static string GetCommandToFindPathOfExecutable()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				return "where";
			}
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
			{
				return "which";
			}
			Logging.tML.Debug("Getting command for finding path of the executable failed due to an unsupported operating system");
			return null;
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x0051CBB9 File Offset: 0x0051ADB9
		private static IEnumerable<string> GetPossibleSystemDotnetPaths()
		{
			string cmd = UIModSources.GetCommandToFindPathOfExecutable();
			if (cmd != null)
			{
				yield return ModCompile.StartOnHost(new ProcessStartInfo
				{
					FileName = cmd,
					Arguments = "dotnet",
					UseShellExecute = false,
					RedirectStandardOutput = true
				}).StandardOutput.ReadToEnd().Split("\n", StringSplitOptions.None)[0].Trim();
			}
			string pathsFile = "/etc/paths.d/dotnet";
			if (File.Exists(pathsFile))
			{
				string contents = File.ReadAllText(pathsFile).Trim();
				Logging.tML.Debug("Reading " + pathsFile + ": " + contents);
				yield return contents + "/dotnet";
			}
			string dotnetRoot = Environment.GetEnvironmentVariable("DOTNET_ROOT");
			if (dotnetRoot != null)
			{
				Logging.tML.Debug("Found env var DOTNET_ROOT: " + dotnetRoot);
				yield return dotnetRoot + "/dotnet";
			}
			yield return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".dotnet", "dotnet");
			yield return "/usr/bin/dotnet";
			yield break;
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x0051CBC4 File Offset: 0x0051ADC4
		private static string GetSystemDotnetPath()
		{
			try
			{
				IEnumerable<string> possibleSystemDotnetPaths = UIModSources.GetPossibleSystemDotnetPaths();
				Func<string, bool> predicate;
				if ((predicate = UIModSources.<>O.<0>__DoesDotnetWork) == null)
				{
					predicate = (UIModSources.<>O.<0>__DoesDotnetWork = new Func<string, bool>(UIModSources.DoesDotnetWork));
				}
				string path = possibleSystemDotnetPaths.FirstOrDefault(predicate);
				if (path != null)
				{
					Logging.tML.Debug("System dotnet install located at: " + path);
					return path;
				}
			}
			catch (Exception)
			{
			}
			Logging.tML.Debug("Finding dotnet on PATH failed");
			return null;
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x0051CC3C File Offset: 0x0051AE3C
		private static bool IsCompatibleDotnetSdkAvailable()
		{
			if (UIModSources.dotnetSDKFound)
			{
				return true;
			}
			try
			{
				string dotnetFilename = UIModSources.GetSystemDotnetPath() ?? "dotnet";
				string output = ModCompile.StartOnHost(new ProcessStartInfo
				{
					FileName = dotnetFilename,
					Arguments = "--list-sdks",
					UseShellExecute = false,
					RedirectStandardOutput = true
				}).StandardOutput.ReadToEnd().Trim();
				Logging.tML.Info("\n" + output);
				if (Platform.IsWindows && dotnetFilename.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86)))
				{
					Logging.tML.Warn("Building mods requires the 64 bit dotnet SDK to be installed, but the 32 bit dotnet SDK was found on the PATH. It is likely that you accidentally installed the 32 bit dotnet SDK and it is taking priority. This will prevent you from debugging or building mods in Visual Studio or any other IDE. To fix this, follow the instructions at https://github.com/tModLoader/tModLoader/wiki/tModLoader-guide-for-developers#net-sdk");
				}
				foreach (string line in output.Split('\n', StringSplitOptions.None))
				{
					if (new Version(new Regex("([0-9.]+).*").Match(line).Groups[1].Value).Major == Environment.Version.Major)
					{
						UIModSources.dotnetSDKFound = true;
						return true;
					}
				}
			}
			catch (Exception e)
			{
				Logging.tML.Debug("'dotnet --list-sdks' check failed: ", e);
			}
			return UIModSources.dotnetSDKFound;
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x0051CD64 File Offset: 0x0051AF64
		private static bool IsRunningInSandbox()
		{
			if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("FLATPAK_SANDBOX_DIR")))
			{
				Logging.tML.Debug("Flatpak sandbox detected");
				return true;
			}
			return false;
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x0051CD8C File Offset: 0x0051AF8C
		private static bool DoesDotnetWork(string path)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				return false;
			}
			try
			{
				Process proc = ModCompile.StartOnHost(new ProcessStartInfo
				{
					FileName = path,
					Arguments = "--version"
				});
				if (proc == null)
				{
					return false;
				}
				proc.WaitForExit();
				if (proc.ExitCode == 0)
				{
					return true;
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x0051CDF4 File Offset: 0x0051AFF4
		internal void Populate()
		{
			Task.Run(delegate()
			{
				LocalMod[] modFiles = ModOrganizer.FindAllMods();
				string[] array = ModCompile.FindModSources();
				for (int i = 0; i < array.Length; i++)
				{
					string sourcePath = array[i];
					string modName = Path.GetFileName(sourcePath);
					LocalMod builtMod = (from m in modFiles
					where m.Name == modName
					where m.location == ModLocation.Local
					orderby m.Version descending
					select m).FirstOrDefault<LocalMod>();
					if (builtMod != null && !string.IsNullOrWhiteSpace(builtMod.properties.modSource) && builtMod.properties.modSource != sourcePath)
					{
						builtMod = null;
					}
					this._items.Add(new UIModSourceItem(sourcePath, builtMod, this._cts.Token));
				}
				this._updateNeeded = true;
			});
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x0051CE08 File Offset: 0x0051B008
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			UIModBrowser.PageUpDownSupport(this._modList);
			if (!this._updateNeeded)
			{
				return;
			}
			this._updateNeeded = false;
			this._uIPanel.RemoveChild(this._uiLoader);
			this._modList.Clear();
			string filter = this.filterTextBox.Text;
			this._modList.AddRange(from item in this._items
			where filter.Length <= 0 || item.modName.IndexOf(filter, StringComparison.OrdinalIgnoreCase) != -1
			select item);
			if (this._items.Count == 0)
			{
				UIPanel firstModGuidePanel = new UIPanel
				{
					Width = new StyleDimension(0f, 1f),
					Height = new StyleDimension(180f, 0f)
				};
				firstModGuidePanel.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					Utils.OpenToURL("https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide");
				};
				UIText firstModGuideText = new UIText(Language.GetTextValue("tModLoader.MSNoModSourcesLinkToBasicModdingGuide"), 1f, false)
				{
					IsWrapped = true,
					WrappedTextBottomPadding = 0f,
					Width = StyleDimension.Fill,
					TextOriginX = 0f,
					VAlign = 0.5f
				};
				firstModGuidePanel.Append(firstModGuideText);
				this._modList.Add(firstModGuidePanel);
			}
			this.Recalculate();
			this._modList.ViewPosition = this.modListViewPosition;
		}

		// Token: 0x04001B27 RID: 6951
		private readonly List<UIModSourceItem> _items = new List<UIModSourceItem>();

		// Token: 0x04001B28 RID: 6952
		private UIList _modList;

		// Token: 0x04001B29 RID: 6953
		private float modListViewPosition;

		// Token: 0x04001B2A RID: 6954
		private bool _updateNeeded;

		// Token: 0x04001B2B RID: 6955
		private UIElement _uIElement;

		// Token: 0x04001B2C RID: 6956
		private UIPanel _uIPanel;

		// Token: 0x04001B2D RID: 6957
		private UIInputTextField filterTextBox;

		// Token: 0x04001B2E RID: 6958
		private UILoaderAnimatedImage _uiLoader;

		// Token: 0x04001B2F RID: 6959
		private UIElement _links;

		// Token: 0x04001B30 RID: 6960
		private CancellationTokenSource _cts;

		// Token: 0x04001B31 RID: 6961
		private static bool dotnetSDKFound;

		// Token: 0x02000A16 RID: 2582
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006C4E RID: 27726
			public static Func<string, bool> <0>__DoesDotnetWork;
		}
	}
}
