using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.Social.Base;
using Terraria.Social.Steam;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000252 RID: 594
	internal class UIModPackItem : UIPanel
	{
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060029FF RID: 10751 RVA: 0x00517891 File Offset: 0x00515A91
		private bool IsLocalModPack
		{
			get
			{
				return ModOrganizer.ModPackActive == this._filepath;
			}
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x005178A4 File Offset: 0x00515AA4
		public UIModPackItem(string name, string[] mods, bool legacy, IEnumerable<LocalMod> localMods)
		{
			this._legacy = legacy;
			this._filename = (this._legacy ? name : Path.GetFileNameWithoutExtension(name));
			this._filepath = name;
			this._numModsEnabled = 0;
			this._numModsDisabled = 0;
			this._mods = mods;
			this._numMods = mods.Length;
			for (int i = 0; i < mods.Length; i++)
			{
				string mod = mods[i];
				LocalMod localMod = localMods.SingleOrDefault((LocalMod m) => m.Name == mod);
				if (localMod != null)
				{
					if (localMod.Enabled)
					{
						this._numModsEnabled++;
					}
					else
					{
						this._numModsDisabled++;
					}
				}
				else
				{
					this._missing.Add(mod);
				}
			}
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			if (this.IsLocalModPack)
			{
				this.BackgroundColor = Color.MediumPurple * 0.7f;
			}
			this._dividerTexture = UICommon.DividerTexture;
			this._innerPanelTexture = UICommon.InnerPanelTexture;
			this.Height.Pixels = (float)(this._legacy ? 126 : 210);
			this.Width.Percent = 1f;
			base.SetPadding(6f);
			UIText uitext = new UIText(this._filename, 1f, false);
			uitext.Left.Pixels = 10f;
			uitext.Top.Pixels = 5f;
			this._modName = uitext;
			base.Append(this._modName);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.ModPackViewList"), 1f, false);
			uiautoScaleTextTextPanel.Width.Pixels = 100f;
			uiautoScaleTextTextPanel.Height.Pixels = 36f;
			uiautoScaleTextTextPanel.Left.Pixels = 407f;
			uiautoScaleTextTextPanel.Top.Pixels = 40f;
			UIAutoScaleTextTextPanel<string> viewListButton = uiautoScaleTextTextPanel.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			viewListButton.PaddingTop -= 2f;
			viewListButton.PaddingBottom -= 2f;
			UIElement uielement = viewListButton;
			UIElement.MouseEvent value;
			if ((value = UIModPackItem.<>O.<0>__ViewListInfo) == null)
			{
				value = (UIModPackItem.<>O.<0>__ViewListInfo = new UIElement.MouseEvent(UIModPackItem.ViewListInfo));
			}
			uielement.OnLeftClick += value;
			base.Append(viewListButton);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel2 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.ModPackEnableThisList"), 1f, false);
			uiautoScaleTextTextPanel2.Width.Pixels = 151f;
			uiautoScaleTextTextPanel2.Height.Pixels = 36f;
			uiautoScaleTextTextPanel2.Left.Pixels = 248f;
			uiautoScaleTextTextPanel2.Top.Pixels = 40f;
			this._enableListButton = uiautoScaleTextTextPanel2.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._enableListButton.PaddingTop -= 2f;
			this._enableListButton.PaddingBottom -= 2f;
			UIElement enableListButton = this._enableListButton;
			UIElement.MouseEvent value2;
			if ((value2 = UIModPackItem.<>O.<1>__EnableList) == null)
			{
				value2 = (UIModPackItem.<>O.<1>__EnableList = new UIElement.MouseEvent(UIModPackItem.EnableList));
			}
			enableListButton.OnLeftClick += value2;
			base.Append(this._enableListButton);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel3 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.ModPackEnableOnlyThisList"), 1f, false);
			uiautoScaleTextTextPanel3.Width.Pixels = 190f;
			uiautoScaleTextTextPanel3.Height.Pixels = 36f;
			uiautoScaleTextTextPanel3.Left.Pixels = 50f;
			uiautoScaleTextTextPanel3.Top.Pixels = 40f;
			this._enableListOnlyButton = uiautoScaleTextTextPanel3.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._enableListOnlyButton.PaddingTop -= 2f;
			this._enableListOnlyButton.PaddingBottom -= 2f;
			UIElement enableListOnlyButton = this._enableListOnlyButton;
			UIElement.MouseEvent value3;
			if ((value3 = UIModPackItem.<>O.<2>__EnabledListOnly) == null)
			{
				value3 = (UIModPackItem.<>O.<2>__EnabledListOnly = new UIElement.MouseEvent(UIModPackItem.EnabledListOnly));
			}
			enableListOnlyButton.OnLeftClick += value3;
			base.Append(this._enableListOnlyButton);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel4 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.ModPackViewModsInModBrowser"), 1f, false);
			uiautoScaleTextTextPanel4.Width.Pixels = 246f;
			uiautoScaleTextTextPanel4.Height.Pixels = 36f;
			uiautoScaleTextTextPanel4.Left.Pixels = 50f;
			uiautoScaleTextTextPanel4.Top.Pixels = 80f;
			this._viewInModBrowserButton = uiautoScaleTextTextPanel4.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._viewInModBrowserButton.PaddingTop -= 2f;
			this._viewInModBrowserButton.PaddingBottom -= 2f;
			UIElement viewInModBrowserButton = this._viewInModBrowserButton;
			UIElement.MouseEvent value4;
			if ((value4 = UIModPackItem.<>O.<3>__DownloadMissingMods) == null)
			{
				value4 = (UIModPackItem.<>O.<3>__DownloadMissingMods = new UIElement.MouseEvent(UIModPackItem.DownloadMissingMods));
			}
			viewInModBrowserButton.OnLeftClick += value4;
			base.Append(this._viewInModBrowserButton);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel5 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.ModPackUpdateListWithEnabled"), 1f, false);
			uiautoScaleTextTextPanel5.Width.Pixels = 225f;
			uiautoScaleTextTextPanel5.Height.Pixels = 36f;
			uiautoScaleTextTextPanel5.Left.Pixels = 304f;
			uiautoScaleTextTextPanel5.Top.Pixels = 80f;
			this._updateListWithEnabledButton = uiautoScaleTextTextPanel5.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._updateListWithEnabledButton.PaddingTop -= 2f;
			this._updateListWithEnabledButton.PaddingBottom -= 2f;
			UIElement updateListWithEnabledButton = this._updateListWithEnabledButton;
			UIElement.MouseEvent value5;
			if ((value5 = UIModPackItem.<>O.<4>__UpdateModPack) == null)
			{
				value5 = (UIModPackItem.<>O.<4>__UpdateModPack = new UIElement.MouseEvent(UIModPackItem.UpdateModPack));
			}
			updateListWithEnabledButton.OnLeftClick += value5;
			base.Append(this._updateListWithEnabledButton);
			UIImageButton uiimageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete"));
			uiimageButton.Top.Pixels = 40f;
			this._deleteButton = uiimageButton;
			this._deleteButton.OnLeftClick += this.DeleteButtonClick;
			this.AddOrRemoveChild(this._deleteButton, !this.IsLocalModPack);
			UIImageButton uiimageButton2 = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete"));
			uiimageButton2.Top.Pixels = 40f;
			this._fakeDeleteButton = uiimageButton2;
			this._fakeDeleteButton.SetVisibility(0.4f, 0.4f);
			this.AddOrRemoveChild(this._fakeDeleteButton, this.IsLocalModPack);
			if (this._legacy)
			{
				return;
			}
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel6 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.InstallPackLocal"), 1f, false);
			uiautoScaleTextTextPanel6.Width.Pixels = 225f;
			uiautoScaleTextTextPanel6.Height.Pixels = 36f;
			uiautoScaleTextTextPanel6.Left.Pixels = 50f;
			uiautoScaleTextTextPanel6.Top.Pixels = 120f;
			this._importFromPackLocalButton = uiautoScaleTextTextPanel6.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._importFromPackLocalButton.PaddingTop -= 2f;
			this._importFromPackLocalButton.PaddingBottom -= 2f;
			UIElement importFromPackLocalButton = this._importFromPackLocalButton;
			UIElement.MouseEvent value6;
			if ((value6 = UIModPackItem.<>O.<5>__ImportModPackLocal) == null)
			{
				value6 = (UIModPackItem.<>O.<5>__ImportModPackLocal = new UIElement.MouseEvent(UIModPackItem.ImportModPackLocal));
			}
			importFromPackLocalButton.OnLeftClick += value6;
			base.Append(this._importFromPackLocalButton);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel7 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.RemovePackLocal"), 1f, false);
			uiautoScaleTextTextPanel7.Width.Pixels = 225f;
			uiautoScaleTextTextPanel7.Height.Pixels = 36f;
			uiautoScaleTextTextPanel7.Left.Pixels = 280f;
			uiautoScaleTextTextPanel7.Top.Pixels = 120f;
			this._removePackLocalButton = uiautoScaleTextTextPanel7.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._removePackLocalButton.PaddingTop -= 2f;
			this._removePackLocalButton.PaddingBottom -= 2f;
			UIElement removePackLocalButton = this._removePackLocalButton;
			UIElement.MouseEvent value7;
			if ((value7 = UIModPackItem.<>O.<6>__RemoveModPackLocal) == null)
			{
				value7 = (UIModPackItem.<>O.<6>__RemoveModPackLocal = new UIElement.MouseEvent(UIModPackItem.RemoveModPackLocal));
			}
			removePackLocalButton.OnLeftClick += value7;
			base.Append(this._removePackLocalButton);
			UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel8 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.ExportPackInstance"), 1f, false);
			uiautoScaleTextTextPanel8.Width.Pixels = 200f;
			uiautoScaleTextTextPanel8.Height.Pixels = 36f;
			uiautoScaleTextTextPanel8.Left.Pixels = 10f;
			uiautoScaleTextTextPanel8.Top.Pixels = 160f;
			this._exportPackInstanceButton = uiautoScaleTextTextPanel8.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._exportPackInstanceButton.PaddingTop -= 2f;
			this._exportPackInstanceButton.PaddingBottom -= 2f;
			UIElement exportPackInstanceButton = this._exportPackInstanceButton;
			UIElement.MouseEvent value8;
			if ((value8 = UIModPackItem.<>O.<7>__ExportInstance) == null)
			{
				value8 = (UIModPackItem.<>O.<7>__ExportInstance = new UIElement.MouseEvent(UIModPackItem.ExportInstance));
			}
			exportPackInstanceButton.OnLeftClick += value8;
			base.Append(this._exportPackInstanceButton);
			if (Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), this._filename)))
			{
				UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel9 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.DeletePackInstance"), 1f, false);
				uiautoScaleTextTextPanel9.Width.Pixels = 140f;
				uiautoScaleTextTextPanel9.Height.Pixels = 36f;
				uiautoScaleTextTextPanel9.Left.Pixels = 370f;
				uiautoScaleTextTextPanel9.Top.Pixels = 160f;
				this._removePackInstanceButton = uiautoScaleTextTextPanel9.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
				this._removePackInstanceButton.PaddingTop -= 2f;
				this._removePackInstanceButton.PaddingBottom -= 2f;
				UIElement removePackInstanceButton = this._removePackInstanceButton;
				UIElement.MouseEvent value9;
				if ((value9 = UIModPackItem.<>O.<8>__DeleteInstance) == null)
				{
					value9 = (UIModPackItem.<>O.<8>__DeleteInstance = new UIElement.MouseEvent(UIModPackItem.DeleteInstance));
				}
				removePackInstanceButton.OnLeftClick += value9;
				base.Append(this._removePackInstanceButton);
			}
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x0051831C File Offset: 0x0051651C
		public override void Draw(SpriteBatch spriteBatch)
		{
			this._tooltip = null;
			base.Draw(spriteBatch);
			if (!string.IsNullOrEmpty(this._tooltip))
			{
				UICommon.TooltipMouseText(this._tooltip);
			}
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x00518344 File Offset: 0x00516544
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(this._innerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), 0, 0f);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x00518434 File Offset: 0x00516634
		private void DrawEnabledText(SpriteBatch spriteBatch, Vector2 drawPos)
		{
			string text = Language.GetTextValue("tModLoader.ModPackModsAvailableStatus", new object[]
			{
				this._numMods,
				this._numModsEnabled,
				this._numModsDisabled,
				this._missing.Count
			});
			Color color = (this._missing.Count > 0) ? Color.Red : ((this._numModsDisabled > 0) ? Color.Yellow : ((ModLoader.EnabledMods.Count > this._mods.Count<string>()) ? Color.LimeGreen : Color.Green));
			Utils.DrawBorderString(spriteBatch, text, drawPos, color, 1f, 0f, 0f, -1);
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x005184F4 File Offset: 0x005166F4
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			Vector2 drawPos;
			drawPos..ctor(innerDimensions.X + 5f, innerDimensions.Y + 30f);
			spriteBatch.Draw(this._dividerTexture.Value, drawPos, null, Color.White, 0f, Vector2.Zero, new Vector2((innerDimensions.Width - 10f) / 8f, 1f), 0, 0f);
			drawPos..ctor(innerDimensions.X + innerDimensions.Width - 355f, innerDimensions.Y);
			this.DrawPanel(spriteBatch, drawPos, 350f);
			this.DrawEnabledText(spriteBatch, drawPos + new Vector2(10f, 5f));
			UIAutoScaleTextTextPanel<string> enableListOnlyButton = this._enableListOnlyButton;
			if (enableListOnlyButton != null && enableListOnlyButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("tModLoader.ModPackEnableOnlyThisListDesc");
				return;
			}
			UIAutoScaleTextTextPanel<string> enableListButton = this._enableListButton;
			if (enableListButton != null && enableListButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("tModLoader.ModPackEnableThisListDesc");
				return;
			}
			UIAutoScaleTextTextPanel<string> exportPackInstanceButton = this._exportPackInstanceButton;
			if (exportPackInstanceButton != null && exportPackInstanceButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("tModLoader.ExportPackInstanceDesc");
				return;
			}
			UIAutoScaleTextTextPanel<string> removePackInstanceButton = this._removePackInstanceButton;
			if (removePackInstanceButton != null && removePackInstanceButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("tModLoader.DeletePackInstanceDesc");
				return;
			}
			UIAutoScaleTextTextPanel<string> playInstanceButton = this._playInstanceButton;
			if (playInstanceButton != null && playInstanceButton.IsMouseHovering)
			{
				this._tooltip = "Play tModLoader using InstallDirectory/<ModPackName>";
				return;
			}
			UIAutoScaleTextTextPanel<string> importFromPackLocalButton = this._importFromPackLocalButton;
			if (importFromPackLocalButton != null && importFromPackLocalButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("tModLoader.InstallPackLocalDesc");
				return;
			}
			UIAutoScaleTextTextPanel<string> removePackLocalButton = this._removePackLocalButton;
			if (removePackLocalButton != null && removePackLocalButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("tModLoader.RemovePackLocalDesc");
				return;
			}
			UIAutoScaleTextTextPanel<string> viewInModBrowserButton = this._viewInModBrowserButton;
			if (viewInModBrowserButton != null && viewInModBrowserButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("tModLoader.ModPackViewModsInModBrowserDesc");
				return;
			}
			UIAutoScaleTextTextPanel<string> updateListWithEnabledButton = this._updateListWithEnabledButton;
			if (updateListWithEnabledButton != null && updateListWithEnabledButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("tModLoader.ModPackUpdateListWithEnabledDesc");
				return;
			}
			UIImageButton deleteButton = this._deleteButton;
			if (deleteButton != null && deleteButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("tModLoader.ModPackDelete");
				return;
			}
			UIImageButton fakeDeleteButton = this._fakeDeleteButton;
			if (fakeDeleteButton != null && fakeDeleteButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("tModLoader.ModPackDisableToDelete");
			}
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x00518754 File Offset: 0x00516954
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			if (Path.GetFileNameWithoutExtension(ModOrganizer.ModPackActive) == this._filename)
			{
				this.BackgroundColor = Color.MediumPurple * 0.4f;
			}
			else
			{
				this.BackgroundColor = UICommon.DefaultUIBlue;
			}
			this.BorderColor = new Color(89, 116, 213);
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x005187B8 File Offset: 0x005169B8
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			if (Path.GetFileNameWithoutExtension(ModOrganizer.ModPackActive) == this._filename)
			{
				this.BackgroundColor = Color.MediumPurple * 0.7f;
			}
			else
			{
				this.BackgroundColor = UICommon.DefaultUIBlueMouseOver;
			}
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x00518824 File Offset: 0x00516A24
		private void DeleteButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this.IsLocalModPack)
			{
				Logging.tML.Warn("Tried to delete active modpack somehow");
				return;
			}
			UIModPackItem modPackItem = (UIModPackItem)listeningElement.Parent;
			if (this._legacy)
			{
				string path = UIModPacks.ModPacksDirectory + Path.DirectorySeparatorChar.ToString() + modPackItem._filename + ".json";
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			else
			{
				string path2 = Path.Combine(UIModPacks.ModPacksDirectory, this._filename);
				if (Directory.Exists(path2))
				{
					Directory.Delete(path2, true);
				}
			}
			Logging.tML.Info("Deleted Mod Pack " + modPackItem._filename);
			Interface.modPacksMenu.OnDeactivate();
			Interface.modPacksMenu.OnActivate();
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x005188DC File Offset: 0x00516ADC
		private static void EnableList(UIMouseEvent evt, UIElement listeningElement)
		{
			UIModPackItem modListItem = (UIModPackItem)listeningElement.Parent;
			foreach (LocalMod mod in ModOrganizer.FindMods(false))
			{
				mod.Enabled = (mod.Enabled || modListItem._mods.Contains(mod.Name));
			}
			if (modListItem._missing.Count > 0)
			{
				Interface.infoMessage.Show(Language.GetTextValue("tModLoader.ModPackModsMissing", string.Join("\n", modListItem._missing)), 10016, null, "", null, null);
			}
			Logging.tML.Info("Enabled Collection of mods defined in  Mod Pack " + modListItem._filename);
			ModLoader.OnSuccessfulLoad = (Action)Delegate.Combine(ModLoader.OnSuccessfulLoad, new Action(delegate()
			{
				Main.menuMode = 10016;
			}));
			ModLoader.Reload();
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x005189C4 File Offset: 0x00516BC4
		private List<ModPubId_t> GetModPackBrowserIds()
		{
			if (!this._legacy)
			{
				return Array.ConvertAll<string, ModPubId_t>(File.ReadAllLines(Path.Combine(UIModPacks.ModPackModsPath(this._filename), "install.txt")), (string x) => new ModPubId_t
				{
					m_ModPubId = x
				}).ToList<ModPubId_t>();
			}
			List<string> modIds;
			if (!WorkshopHelper.TryGetGroupPublishIdsByInternalName(new QueryParameters
			{
				searchModSlugs = this._mods
			}, out modIds))
			{
				return new List<ModPubId_t>();
			}
			List<ModPubId_t> output = new List<ModPubId_t>();
			foreach (string item in modIds)
			{
				if (item != "0")
				{
					output.Add(new ModPubId_t
					{
						m_ModPubId = item
					});
				}
			}
			return output;
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x00518AAC File Offset: 0x00516CAC
		private static void DownloadMissingMods(UIMouseEvent evt, UIElement listeningElement)
		{
			UIModPackItem modpack = (UIModPackItem)listeningElement.Parent;
			Interface.modBrowser.Activate();
			Interface.modBrowser.FilterTextBox.Text = "";
			Interface.modBrowser.SpecialModPackFilter = modpack.GetModPackBrowserIds();
			Interface.modBrowser.SpecialModPackFilterTitle = Language.GetTextValue("tModLoader.MBFilterModlist");
			Interface.modBrowser.UpdateFilterMode = UpdateFilter.All;
			Interface.modBrowser.ModSideFilterMode = ModSideFilter.All;
			Interface.modBrowser.ResetTagFilters();
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			Interface.modBrowser.PreviousUIState = Interface.modPacksMenu;
			Main.menuMode = 10007;
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x00518B58 File Offset: 0x00516D58
		private static void EnabledListOnly(UIMouseEvent evt, UIElement listeningElement)
		{
			UIModPackItem modpack = (UIModPackItem)listeningElement.Parent;
			ModLoader.DisableAllMods();
			UIModPackItem.EnableList(evt, listeningElement);
			Logging.tML.Info("Enabled only mods defined in Collection " + modpack._filename);
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00518B98 File Offset: 0x00516D98
		private static void UpdateModPack(UIMouseEvent evt, UIElement listeningElement)
		{
			UIModPackItem modpack = (UIModPackItem)listeningElement.Parent;
			UIModPacks.SaveModPack(modpack._filename);
			if (modpack._filepath == ModOrganizer.ModPackActive)
			{
				ModLoader.DisableAllMods();
				Logging.tML.Info("Cleaning up removed tmods " + modpack._filename);
				ModLoader.OnSuccessfulLoad = (Action)Delegate.Combine(ModLoader.OnSuccessfulLoad, new Action(delegate()
				{
					foreach (string file2 in Directory.EnumerateFiles(UIModPacks.ModPackModsPath(modpack._filename), "*.tmod"))
					{
						if (!modpack._mods.Contains(Path.GetFileNameWithoutExtension(file2)))
						{
							File.Delete(file2);
						}
					}
					UIModPackItem.EnableList(evt, listeningElement);
				}));
				ModLoader.Reload();
			}
			else
			{
				foreach (string file in Directory.EnumerateFiles(UIModPacks.ModPackModsPath(modpack._filename), "*.tmod"))
				{
					if (!modpack._mods.Contains(Path.GetFileNameWithoutExtension(file)))
					{
						File.Delete(file);
					}
				}
			}
			Interface.modPacksMenu.OnDeactivate();
			Interface.modPacksMenu.OnActivate();
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x00518CC0 File Offset: 0x00516EC0
		private static void ImportModPackLocal(UIMouseEvent evt, UIElement listeningElement)
		{
			UIModPackItem modpack = (UIModPackItem)listeningElement.Parent;
			ModOrganizer.ModPackActive = modpack._filepath;
			Main.SaveSettings();
			Logging.tML.Info("Enabled Frozen Mod Pack " + modpack._filename);
			UIModPackItem.EnabledListOnly(evt, listeningElement);
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x00518D0C File Offset: 0x00516F0C
		private static void RemoveModPackLocal(UIMouseEvent evt, UIElement listeningElement)
		{
			UIModPackItem modpack = (UIModPackItem)listeningElement.Parent;
			ModOrganizer.ModPackActive = null;
			Main.SaveSettings();
			ModLoader.DisableAllMods();
			Logging.tML.Info("Disabled Frozen Mod Pack " + modpack._filename);
			ModLoader.OnSuccessfulLoad = (Action)Delegate.Combine(ModLoader.OnSuccessfulLoad, new Action(delegate()
			{
				Main.menuMode = 10016;
			}));
			ModLoader.Reload();
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x00518D88 File Offset: 0x00516F88
		private static void ExportInstance(UIMouseEvent evt, UIElement listeningElement)
		{
			UIModPacks.ExportSnapshot(((UIModPackItem)listeningElement.Parent)._filename);
			Interface.modPacksMenu.OnDeactivate();
			Interface.modPacksMenu.OnActivate();
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x00518DB4 File Offset: 0x00516FB4
		private static void PlayInstance(UIMouseEvent evt, UIElement listeningElement)
		{
			UIModPackItem modpack = (UIModPackItem)listeningElement.Parent;
			string launchScript = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), modpack._filename), Platform.IsWindows ? "start-tModLoader.bat" : "start-tModLoader.sh");
			Process.Start(new ProcessStartInfo
			{
				FileName = launchScript,
				UseShellExecute = true
			});
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x00518E10 File Offset: 0x00517010
		private static void DeleteInstance(UIMouseEvent evt, UIElement listeningElement)
		{
			UIModPackItem modpack = (UIModPackItem)listeningElement.Parent;
			Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), modpack._filename), true);
			Interface.modPacksMenu.OnDeactivate();
			Interface.modPacksMenu.OnActivate();
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x00518E54 File Offset: 0x00517054
		private static void ViewListInfo(UIMouseEvent evt, UIElement listeningElement)
		{
			UIModPackItem modListItem = (UIModPackItem)listeningElement.Parent;
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			string message = "";
			foreach (string mod in modListItem._mods)
			{
				message = message + mod + (modListItem._missing.Contains(mod) ? Language.GetTextValue("tModLoader.ModPackMissing") : (ModLoader.IsEnabled(mod) ? "" : Language.GetTextValue("tModLoader.ModPackDisabled"))) + "\n";
			}
			Interface.infoMessage.Show(Language.GetTextValue("tModLoader.ModPackModsContained", message), 10016, null, "", null, null);
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x00518F08 File Offset: 0x00517108
		public override int CompareTo(object obj)
		{
			UIModPackItem item = obj as UIModPackItem;
			if (item == null)
			{
				return base.CompareTo(obj);
			}
			return string.Compare(this._filename, item._filename, StringComparison.Ordinal);
		}

		// Token: 0x04001ACD RID: 6861
		private readonly Asset<Texture2D> _dividerTexture;

		// Token: 0x04001ACE RID: 6862
		private readonly Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04001ACF RID: 6863
		private readonly UIText _modName;

		// Token: 0x04001AD0 RID: 6864
		private readonly string[] _mods;

		// Token: 0x04001AD1 RID: 6865
		private readonly List<string> _missing = new List<string>();

		// Token: 0x04001AD2 RID: 6866
		private readonly int _numMods;

		// Token: 0x04001AD3 RID: 6867
		private readonly int _numModsEnabled;

		// Token: 0x04001AD4 RID: 6868
		private readonly int _numModsDisabled;

		// Token: 0x04001AD5 RID: 6869
		private readonly UIAutoScaleTextTextPanel<string> _enableListButton;

		// Token: 0x04001AD6 RID: 6870
		private readonly UIAutoScaleTextTextPanel<string> _enableListOnlyButton;

		// Token: 0x04001AD7 RID: 6871
		private readonly UIAutoScaleTextTextPanel<string> _viewInModBrowserButton;

		// Token: 0x04001AD8 RID: 6872
		private readonly UIAutoScaleTextTextPanel<string> _updateListWithEnabledButton;

		// Token: 0x04001AD9 RID: 6873
		private readonly UIAutoScaleTextTextPanel<string> _playInstanceButton;

		// Token: 0x04001ADA RID: 6874
		private readonly UIAutoScaleTextTextPanel<string> _exportPackInstanceButton;

		// Token: 0x04001ADB RID: 6875
		private readonly UIAutoScaleTextTextPanel<string> _removePackInstanceButton;

		// Token: 0x04001ADC RID: 6876
		private readonly UIAutoScaleTextTextPanel<string> _importFromPackLocalButton;

		// Token: 0x04001ADD RID: 6877
		private readonly UIAutoScaleTextTextPanel<string> _removePackLocalButton;

		// Token: 0x04001ADE RID: 6878
		private readonly UIImageButton _deleteButton;

		// Token: 0x04001ADF RID: 6879
		private readonly UIImageButton _fakeDeleteButton;

		// Token: 0x04001AE0 RID: 6880
		private readonly string _filename;

		// Token: 0x04001AE1 RID: 6881
		private readonly string _filepath;

		// Token: 0x04001AE2 RID: 6882
		private readonly bool _legacy;

		// Token: 0x04001AE3 RID: 6883
		private string _tooltip;

		// Token: 0x02000A07 RID: 2567
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006C29 RID: 27689
			public static UIElement.MouseEvent <0>__ViewListInfo;

			// Token: 0x04006C2A RID: 27690
			public static UIElement.MouseEvent <1>__EnableList;

			// Token: 0x04006C2B RID: 27691
			public static UIElement.MouseEvent <2>__EnabledListOnly;

			// Token: 0x04006C2C RID: 27692
			public static UIElement.MouseEvent <3>__DownloadMissingMods;

			// Token: 0x04006C2D RID: 27693
			public static UIElement.MouseEvent <4>__UpdateModPack;

			// Token: 0x04006C2E RID: 27694
			public static UIElement.MouseEvent <5>__ImportModPackLocal;

			// Token: 0x04006C2F RID: 27695
			public static UIElement.MouseEvent <6>__RemoveModPackLocal;

			// Token: 0x04006C30 RID: 27696
			public static UIElement.MouseEvent <7>__ExportInstance;

			// Token: 0x04006C31 RID: 27697
			public static UIElement.MouseEvent <8>__DeleteInstance;
		}
	}
}
