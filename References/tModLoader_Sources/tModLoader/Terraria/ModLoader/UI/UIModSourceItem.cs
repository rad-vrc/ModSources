using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using Steamworks;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.Social.Steam;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x0200025A RID: 602
	internal class UIModSourceItem : UIPanel
	{
		// Token: 0x06002A54 RID: 10836 RVA: 0x0051B13C File Offset: 0x0051933C
		public UIModSourceItem(string mod, LocalMod builtMod, CancellationToken modSourcesToken)
		{
			this._mod = mod;
			this._modSourcesToken = modSourcesToken;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			this._dividerTexture = UICommon.DividerTexture;
			this.Height.Pixels = 90f;
			this.Width.Percent = 1f;
			base.SetPadding(6f);
			string addendum = Path.GetFileName(mod).Contains(" ") ? ("  [c/FF0000:" + Language.GetTextValue("tModLoader.MSModSourcesCantHaveSpaces") + "]") : "";
			this.modName = Path.GetFileName(mod);
			UIText uitext = new UIText(this.modName + addendum, 1f, false);
			uitext.Left.Pixels = 10f;
			uitext.Top.Pixels = 5f;
			this._modName = uitext;
			base.Append(this._modName);
			if (builtMod != null)
			{
				string lastBuildTimeMessage = TimeHelper.HumanTimeSpanString(builtMod.lastModified, true);
				UIText uitext2 = new UIText(lastBuildTimeMessage, 1f, false);
				uitext2.HAlign = 1f;
				uitext2.Left.Pixels = -110f;
				uitext2.Top.Pixels = 5f;
				this._lastBuildTime = uitext2;
				TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - builtMod.lastModified.Ticks);
				double delta = Math.Abs(ts.TotalSeconds);
				UIText lastBuildTime = this._lastBuildTime;
				Color textColor;
				if (delta >= 300.0)
				{
					if (delta >= 3600.0)
					{
						if (delta >= 2592000.0)
						{
							textColor = Color.Red;
						}
						else
						{
							textColor = Color.Orange;
						}
					}
					else
					{
						textColor = Color.Yellow;
					}
				}
				else
				{
					textColor = Color.Green;
				}
				lastBuildTime.TextColor = textColor;
				base.Append(this._lastBuildTime);
			}
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.MSBuild"), 1f, false);
			uiautoScaleTextTextPanel.Width.Pixels = 100f;
			uiautoScaleTextTextPanel.Height.Pixels = 36f;
			uiautoScaleTextTextPanel.Left.Pixels = 10f;
			uiautoScaleTextTextPanel.Top.Pixels = 40f;
			UIAutoScaleTextTextPanel<string> buildButton = uiautoScaleTextTextPanel.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			buildButton.PaddingTop -= 2f;
			buildButton.PaddingBottom -= 2f;
			buildButton.OnLeftClick += this.BuildMod;
			base.Append(buildButton);
			UIAutoScaleTextTextPanel<string> buildReloadButton = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.MSBuildReload"), 1f, false);
			buildReloadButton.CopyStyle(buildButton);
			buildReloadButton.Width.Pixels = 200f;
			buildReloadButton.Left.Pixels = 150f;
			buildReloadButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			buildReloadButton.OnLeftClick += this.BuildAndReload;
			base.Append(buildReloadButton);
			this._builtMod = builtMod;
			if (builtMod != null && LocalizationLoader.changedMods.Contains(this.modName))
			{
				this.needRebuildButton = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.MSRebuildRequired"), 1f, false);
				this.needRebuildButton.CopyStyle(buildReloadButton);
				this.needRebuildButton.Width.Pixels = 180f;
				this.needRebuildButton.Left.Pixels = 360f;
				this.needRebuildButton.BackgroundColor = Color.Red;
				base.Append(this.needRebuildButton);
			}
			else if (builtMod != null)
			{
				UIAutoScaleTextTextPanel<string> publishButton = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.MSPublish"), 1f, false);
				publishButton.CopyStyle(buildReloadButton);
				publishButton.Width.Pixels = 100f;
				publishButton.Left.Pixels = 390f;
				publishButton.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
				if (builtMod.properties.side == ModSide.Server)
				{
					publishButton.OnLeftClick += this.PublishServerSideMod;
					base.Append(publishButton);
				}
				else if (builtMod.Enabled)
				{
					publishButton.OnLeftClick += this.PublishMod;
					base.Append(publishButton);
				}
			}
			base.OnLeftDoubleClick += this.BuildAndReload;
			string modFolderName = Path.GetFileName(this._mod);
			string csprojFile = Path.Combine(this._mod, modFolderName + ".csproj");
			if (File.Exists(csprojFile))
			{
				UIHoverImage uihoverImage = new UIHoverImage(UICommon.CopyCodeButtonTexture, Language.GetTextValue("tModLoader.MSOpenCSProj"));
				uihoverImage.RemoveFloatingPointsFromDrawPosition = true;
				uihoverImage.UseTooltipMouseText = true;
				uihoverImage.Left.Pixels = (float)this.contextButtonsLeft;
				uihoverImage.Left.Percent = 1f;
				uihoverImage.Top.Pixels = 4f;
				UIHoverImage openCSProjButton = uihoverImage;
				openCSProjButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					if (Platform.IsWindows)
					{
						Process.Start(new ProcessStartInfo("explorer", csprojFile)
						{
							UseShellExecute = true
						});
						return;
					}
					Process.Start(new ProcessStartInfo(csprojFile)
					{
						UseShellExecute = true
					});
				};
				base.Append(openCSProjButton);
				this.contextButtonsLeft -= 26;
			}
			if (File.Exists(csprojFile))
			{
				bool customModSourceFolder = builtMod != null && !string.IsNullOrWhiteSpace(builtMod.properties.modSource) && builtMod.properties.modSource != Path.Combine(ModCompile.ModSourcePath, this.modName);
				UIHoverImage uihoverImage2 = new UIHoverImage(customModSourceFolder ? UICommon.ButtonOpenFolderCustom : UICommon.ButtonOpenFolder, customModSourceFolder ? Language.GetTextValue("tModLoader.MSOpenCustomSourceFolder", builtMod.properties.modSource) : Lang.inter[110].Value);
				uihoverImage2.RemoveFloatingPointsFromDrawPosition = true;
				uihoverImage2.UseTooltipMouseText = true;
				uihoverImage2.Left.Pixels = (float)this.contextButtonsLeft;
				uihoverImage2.Left.Percent = 1f;
				uihoverImage2.Top.Pixels = 4f;
				UIHoverImage openFolderButton = uihoverImage2;
				openFolderButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					Utils.OpenFolder(this._mod);
				};
				base.Append(openFolderButton);
				this.contextButtonsLeft -= 26;
			}
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x0051B788 File Offset: 0x00519988
		protected override void DrawChildren(SpriteBatch spriteBatch)
		{
			base.DrawChildren(spriteBatch);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = this.needRebuildButton;
			if (uiautoScaleTextTextPanel != null && uiautoScaleTextTextPanel.IsMouseHovering)
			{
				UICommon.TooltipMouseText(Language.GetTextValue("tModLoader.MSLocalizationFilesChangedCantPublish"));
			}
			UIText lastBuildTime = this._lastBuildTime;
			if (lastBuildTime != null && lastBuildTime.IsMouseHovering)
			{
				UICommon.TooltipMouseText(Language.GetTextValue("tModLoader.MSLastBuilt", TimeHelper.HumanTimeSpanString(this._builtMod.lastModified, true)));
			}
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x0051B7F4 File Offset: 0x005199F4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			Vector2 drawPos;
			drawPos..ctor(innerDimensions.X + 5f, innerDimensions.Y + 30f);
			spriteBatch.Draw(this._dividerTexture.Value, drawPos, null, Color.White, 0f, Vector2.Zero, new Vector2((innerDimensions.Width - 10f) / 8f, 1f), 0, 0f);
			if (!this._upgradePotentialChecked)
			{
				this._upgradePotentialChecked = true;
				this.StartUpgradeTasks();
			}
			Task<string[]> task = this.langFileTask;
			if (task != null && task.IsCompleted)
			{
				string[] result = this.langFileTask.Result;
				if (result.Length != 0)
				{
					this.AddLangFileUpgradeButton(result);
				}
				this.langFileTask = null;
			}
			Task<bool> task2 = this.sourceUpgradeTask;
			if (task2 != null && task2.IsCompleted)
			{
				try
				{
					if (this.sourceUpgradeTask.GetAwaiter().GetResult())
					{
						this.AddCsProjUpgradeButton();
					}
					else
					{
						this.AddModPorterButton();
					}
				}
				catch (Exception e)
				{
					this.AddErrorButton(e);
				}
				finally
				{
					this.sourceUpgradeTask = null;
				}
			}
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x0051B930 File Offset: 0x00519B30
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.BackgroundColor = UICommon.DefaultUIBlue;
			this.BorderColor = new Color(89, 116, 213);
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x0051B958 File Offset: 0x00519B58
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x0051B9A8 File Offset: 0x00519BA8
		public override int CompareTo(object obj)
		{
			UIModSourceItem uIModSourceItem = obj as UIModSourceItem;
			if (uIModSourceItem == null)
			{
				return base.CompareTo(obj);
			}
			if (uIModSourceItem._builtMod == null && this._builtMod == null)
			{
				return this._modName.Text.CompareTo(uIModSourceItem._modName.Text);
			}
			if (uIModSourceItem._builtMod == null)
			{
				return -1;
			}
			if (this._builtMod == null)
			{
				return 1;
			}
			return uIModSourceItem._builtMod.lastModified.CompareTo(this._builtMod.lastModified);
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x0051BA22 File Offset: 0x00519C22
		private void BuildMod(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Interface.buildMod.Build(this._mod, false);
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x0051BA4A File Offset: 0x00519C4A
		private void BuildAndReload(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Interface.buildMod.Build(this._mod, true);
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x0051BA74 File Offset: 0x00519C74
		private void PublishMod(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			try
			{
				Mod mod;
				if (!SteamedWraps.SteamClient)
				{
					Utils.ShowFancyErrorMessage(Language.GetTextValue("tModLoader.SteamPublishingLimit"), 10001, null);
				}
				else if (!ModLoader.TryGetMod(this._builtMod.Name, out mod))
				{
					if (!this._builtMod.Enabled)
					{
						this._builtMod.Enabled = true;
					}
					Main.menuMode = 10006;
					ModLoader.OnSuccessfulLoad = (Action)Delegate.Combine(ModLoader.OnSuccessfulLoad, new Action(delegate()
					{
						Main.QueueMainThreadAction(delegate
						{
							this.PublishMod(null, null);
						});
					}));
				}
				else
				{
					string icon = Path.Combine(this._mod, "icon_workshop.png");
					if (!File.Exists(icon))
					{
						icon = Path.Combine(this._mod, "icon.png");
					}
					WorkshopHelper.PublishMod(this._builtMod, icon);
				}
			}
			catch (WebException e)
			{
				UIModBrowser.LogModBrowserException(e, 10001);
			}
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x0051BB64 File Offset: 0x00519D64
		private void PublishServerSideMod(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			try
			{
				if (!SteamedWraps.SteamClient)
				{
					Utils.ShowFancyErrorMessage(Language.GetTextValue("tModLoader.SteamPublishingLimit"), 10001, null);
				}
				else
				{
					Process.Start(new ProcessStartInfo
					{
						UseShellExecute = true,
						FileName = Process.GetCurrentProcess().MainModule.FileName,
						Arguments = "tModLoader.dll -server -steam -publish " + this._builtMod.modFile.path.Remove(this._builtMod.modFile.path.LastIndexOf(".tmod"))
					}).WaitForExit();
				}
			}
			catch (WebException e)
			{
				UIModBrowser.LogModBrowserException(e, 10001);
			}
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x0051BC2C File Offset: 0x00519E2C
		internal static void PublishModCommandLine(string modName)
		{
			try
			{
				TmodFile modFile = new TmodFile(Path.Combine(ModLoader.ModPath, modName + ".tmod"), null, null);
				LocalMod localMod;
				using (modFile.Open())
				{
					localMod = new LocalMod(ModLocation.Local, modFile);
				}
				string icon = Path.Combine(localMod.properties.modSource, "icon_workshop.png");
				if (!File.Exists(icon))
				{
					icon = Path.Combine(localMod.properties.modSource, "icon.png");
				}
				WorkshopHelper.PublishMod(localMod, icon);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Something went wrong with command line mod publishing.");
				Console.WriteLine(ex.ToString());
				SteamAPI.Shutdown();
				Environment.Exit(1);
			}
			Console.WriteLine("exiting ");
			SteamAPI.Shutdown();
			Environment.Exit(0);
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x0051BD00 File Offset: 0x00519F00
		private void StartUpgradeTasks()
		{
			this.langFileTask = Task.Run<string[]>(() => Directory.GetFiles(this._mod, "*.lang", SearchOption.AllDirectories), this._modSourcesToken);
			this.sourceUpgradeTask = Task.Run<bool>(() => SourceManagement.SourceUpgradeNeeded(this._mod), this._modSourcesToken);
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x0051BD3C File Offset: 0x00519F3C
		private void AddLangFileUpgradeButton(string[] result)
		{
			UIModSourceItem.<>c__DisplayClass25_0 CS$<>8__locals1 = new UIModSourceItem.<>c__DisplayClass25_0();
			CS$<>8__locals1.result = result;
			CS$<>8__locals1.<>4__this = this;
			Asset<Texture2D> icon = UICommon.ButtonUpgradeLang;
			UIModSourceItem.<>c__DisplayClass25_0 CS$<>8__locals2 = CS$<>8__locals1;
			UIHoverImage uihoverImage = new UIHoverImage(icon, Language.GetTextValue("tModLoader.MSUpgradeLangFiles"));
			uihoverImage.RemoveFloatingPointsFromDrawPosition = true;
			uihoverImage.UseTooltipMouseText = true;
			uihoverImage.Left.Pixels = (float)this.contextButtonsLeft;
			uihoverImage.Left.Percent = 1f;
			uihoverImage.Top.Pixels = 4f;
			CS$<>8__locals2.upgradeLangFilesButton = uihoverImage;
			CS$<>8__locals1.upgradeLangFilesButton.OnLeftClick += delegate(UIMouseEvent s, UIElement e)
			{
				string[] result2 = CS$<>8__locals1.result;
				for (int i = 0; i < result2.Length; i++)
				{
					LocalizationLoader.UpgradeLangFile(result2[i], CS$<>8__locals1.<>4__this.modName);
				}
				CS$<>8__locals1.upgradeLangFilesButton.Remove();
				SoundEngine.PlaySound(SoundID.MenuTick, null, null);
			};
			base.Append(CS$<>8__locals1.upgradeLangFilesButton);
			this.contextButtonsLeft -= 26;
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x0051BDEC File Offset: 0x00519FEC
		private void AddCsProjUpgradeButton()
		{
			UIModSourceItem.<>c__DisplayClass26_0 CS$<>8__locals1 = new UIModSourceItem.<>c__DisplayClass26_0();
			CS$<>8__locals1.<>4__this = this;
			Asset<Texture2D> icon = UICommon.ButtonUpgradeCsproj;
			UIModSourceItem.<>c__DisplayClass26_0 CS$<>8__locals2 = CS$<>8__locals1;
			UIHoverImage uihoverImage = new UIHoverImage(icon, Language.GetTextValue("tModLoader.MSUpgradeCSProj"));
			uihoverImage.RemoveFloatingPointsFromDrawPosition = true;
			uihoverImage.UseTooltipMouseText = true;
			uihoverImage.Left.Pixels = (float)this.contextButtonsLeft;
			uihoverImage.Left.Percent = 1f;
			uihoverImage.Top.Pixels = 4f;
			CS$<>8__locals2.upgradeCSProjButton = uihoverImage;
			CS$<>8__locals1.upgradeCSProjButton.OnLeftClick += delegate(UIMouseEvent s, UIElement e)
			{
				SourceManagement.UpgradeSource(CS$<>8__locals1.<>4__this._mod);
				SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
				Main.menuMode = 10001;
				CS$<>8__locals1.upgradeCSProjButton.Remove();
				CS$<>8__locals1.<>4__this.AddModPorterButton();
			};
			base.Append(CS$<>8__locals1.upgradeCSProjButton);
			this.contextButtonsLeft -= 26;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x0051BE94 File Offset: 0x0051A094
		private void AddModPorterButton()
		{
			UIHoverImage uihoverImage = new UIHoverImage(UICommon.ButtonRunTModPorter, Language.GetTextValue("tModLoader.MSPortToLatest"));
			uihoverImage.RemoveFloatingPointsFromDrawPosition = true;
			uihoverImage.UseTooltipMouseText = true;
			uihoverImage.Left.Pixels = (float)this.contextButtonsLeft;
			uihoverImage.Left.Percent = 1f;
			uihoverImage.Top.Pixels = 4f;
			UIHoverImage portModButton = uihoverImage;
			portModButton.OnLeftClick += delegate(UIMouseEvent s, UIElement e)
			{
				string modFolderName = Path.GetFileName(this._mod);
				string csprojFile = Path.Combine(this._mod, modFolderName + ".csproj");
				string args = "\"" + csprojFile + "\"";
				string tMLPath = Path.GetDirectoryName(Path.GetFullPath(Assembly.GetExecutingAssembly().Location));
				string porterPath = Path.Combine(tMLPath, "tModPorter", Platform.IsWindows ? "tModPorter.bat" : "tModPorter.sh");
				ProcessStartInfo porterInfo = new ProcessStartInfo
				{
					FileName = porterPath,
					Arguments = args,
					WorkingDirectory = tMLPath,
					UseShellExecute = true
				};
				try
				{
					ModCompile.StartOnHost(porterInfo);
				}
				catch (Exception ex)
				{
					Logging.tML.Error("Failed to start tModPorter", ex);
				}
			};
			base.Append(portModButton);
			this.contextButtonsLeft -= 26;
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x0051BF20 File Offset: 0x0051A120
		private void AddErrorButton(Exception e)
		{
			UIHoverImage uihoverImage = new UIHoverImage(UICommon.ButtonErrorTexture, Language.GetTextValue("tModLoader.MSSourceIssue"));
			uihoverImage.RemoveFloatingPointsFromDrawPosition = true;
			uihoverImage.UseTooltipMouseText = true;
			uihoverImage.Left.Pixels = (float)this.contextButtonsLeft;
			uihoverImage.Left.Percent = 1f;
			uihoverImage.Top.Pixels = 4f;
			UIHoverImage modSaveErrorWarning = uihoverImage;
			string fullError = Language.GetTextValue("tModLoader.MSSourceIssueMessage", this.modName, "\n\n" + e.ToString());
			modSaveErrorWarning.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				Interface.infoMessage.Show(fullError, 888, Interface.modSources, "", null, null);
			};
			base.Append(modSaveErrorWarning);
			this.contextButtonsLeft -= 26;
		}

		// Token: 0x04001B19 RID: 6937
		private readonly string _mod;

		// Token: 0x04001B1A RID: 6938
		internal readonly string modName;

		// Token: 0x04001B1B RID: 6939
		private readonly Asset<Texture2D> _dividerTexture;

		// Token: 0x04001B1C RID: 6940
		private readonly UIText _modName;

		// Token: 0x04001B1D RID: 6941
		private readonly UIText _lastBuildTime;

		// Token: 0x04001B1E RID: 6942
		private readonly UIAutoScaleTextTextPanel<string> needRebuildButton;

		// Token: 0x04001B1F RID: 6943
		private readonly LocalMod _builtMod;

		// Token: 0x04001B20 RID: 6944
		private bool _upgradePotentialChecked;

		// Token: 0x04001B21 RID: 6945
		private Stopwatch uploadTimer;

		// Token: 0x04001B22 RID: 6946
		private int contextButtonsLeft = -26;

		// Token: 0x04001B23 RID: 6947
		private Task<string[]> langFileTask;

		// Token: 0x04001B24 RID: 6948
		private Task<bool> sourceUpgradeTask;

		// Token: 0x04001B25 RID: 6949
		private CancellationToken _modSourcesToken;
	}
}
