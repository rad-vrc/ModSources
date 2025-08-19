using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI.ModBrowser;
using Terraria.Social.Base;
using Terraria.UI;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000250 RID: 592
	internal class UIModItem : UIPanel
	{
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x005156D8 File Offset: 0x005138D8
		private string ToggleModStateText
		{
			get
			{
				if (this._mod.Enabled)
				{
					if (this._modDependents.Any<string>())
					{
						return Language.GetTextValue("tModLoader.ModsDisableAndDependents", this._mod.DisplayName, this._modDependents.Length);
					}
					return Language.GetTextValue("tModLoader.ModsDisable");
				}
				else
				{
					if (this._modDependencies.Any<string>())
					{
						return Language.GetTextValue("tModLoader.ModsEnableAndDependencies", this._mod.DisplayName, this._modDependencies.Length);
					}
					return Language.GetTextValue("tModLoader.ModsEnable");
				}
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x00515767 File Offset: 0x00513967
		public string ModName
		{
			get
			{
				return this._mod.Name;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x00515774 File Offset: 0x00513974
		public bool NeedsReload
		{
			get
			{
				return this._mod.properties.side != ModSide.Server && (this._mod.Enabled != this._loaded || this._configChangesRequireReload);
			}
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x005157A8 File Offset: 0x005139A8
		public UIModItem(LocalMod mod)
		{
			this._mod = mod;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			this.Height.Pixels = 92f;
			this.Width.Percent = 1f;
			base.SetPadding(6f);
			this.DisplayNameClean = this._mod.DisplayNameClean;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x0051581C File Offset: 0x00513A1C
		public override void OnInitialize()
		{
			UIModItem.<>c__DisplayClass37_0 CS$<>8__locals1 = new UIModItem.<>c__DisplayClass37_0();
			CS$<>8__locals1.<>4__this = this;
			base.OnInitialize();
			string displayName = this._mod.DisplayName;
			string str = " v";
			Version version = this._mod.modFile.Version;
			string text = displayName + str + ((version != null) ? version.ToString() : null);
			Asset<Texture2D> modIcon = Main.Assets.Request<Texture2D>("Images/UI/DefaultResourcePackIcon", 1);
			this._modIconAdjust += 85;
			if (this._mod.modFile.HasFile("icon.png"))
			{
				try
				{
					using (this._mod.modFile.Open())
					{
						using (Stream s = this._mod.modFile.GetStream("icon.png", false))
						{
							Asset<Texture2D> iconTexture = Main.Assets.CreateUntracked<Texture2D>(s, ".png", 1);
							if (iconTexture.Width() == 80 && iconTexture.Height() == 80)
							{
								modIcon = iconTexture;
							}
						}
					}
				}
				catch (Exception e)
				{
					Exception e2;
					Logging.tML.Error("Unknown error", e2);
				}
			}
			UIImage uiimage = new UIImage(modIcon);
			uiimage.Left.Percent = 0f;
			uiimage.Top.Percent = 0f;
			uiimage.Width.Pixels = 80f;
			uiimage.Height.Pixels = 80f;
			uiimage.ScaleToFit = true;
			this._modIcon = uiimage;
			base.Append(this._modIcon);
			UIText uitext = new UIText(text, 1f, false);
			uitext.Left = new StyleDimension((float)this._modIconAdjust, 0f);
			uitext.Top.Pixels = 5f;
			this._modName = uitext;
			base.Append(this._modName);
			UIModStateText uimodStateText = new UIModStateText(this._mod.Enabled);
			uimodStateText.Top.Pixels = 40f;
			uimodStateText.Left.Pixels = (float)this._modIconAdjust;
			this._uiModStateText = uimodStateText;
			this._uiModStateText.OnLeftClick += this.ToggleEnabled;
			string updateVersion = null;
			CS$<>8__locals1.updateURL = "https://github.com/tModLoader/tModLoader/wiki/tModLoader-guide-for-players#beta-branches";
			Color updateColor = Color.Orange;
			if (BuildInfo.tMLVersion.MajorMinorBuild() < this._mod.tModLoaderVersion.MajorMinorBuild())
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 1);
				defaultInterpolatedStringHandler.AppendLiteral("v");
				defaultInterpolatedStringHandler.AppendFormatted<Version>(this._mod.tModLoaderVersion);
				updateVersion = defaultInterpolatedStringHandler.ToStringAndClear();
				if (this._mod.tModLoaderVersion.Build == 2)
				{
					updateVersion = "Preview " + updateVersion;
				}
			}
			string modBrowserVersion;
			if (!this.CheckIfPublishedForThisBrowserVersion(out modBrowserVersion))
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
				defaultInterpolatedStringHandler.AppendFormatted(modBrowserVersion);
				defaultInterpolatedStringHandler.AppendLiteral(" v");
				defaultInterpolatedStringHandler.AppendFormatted<Version>(this._mod.tModLoaderVersion);
				updateVersion = defaultInterpolatedStringHandler.ToStringAndClear();
				updateColor = Color.Yellow;
			}
			if (updateVersion != null)
			{
				this.tMLUpdateRequired = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("tModLoader.MBRequiresTMLUpdate", updateVersion), 1f, false).WithFadedMouseOver(updateColor, updateColor * 0.7f, default(Color), default(Color));
				this.tMLUpdateRequired.BackgroundColor = updateColor * 0.7f;
				this.tMLUpdateRequired.Top.Pixels = 40f;
				this.tMLUpdateRequired.Width.Pixels = 280f;
				this.tMLUpdateRequired.Height.Pixels = 36f;
				UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = this.tMLUpdateRequired;
				uiautoScaleTextTextPanel.Left.Pixels = uiautoScaleTextTextPanel.Left.Pixels + (this._uiModStateText.Width.Pixels + this._uiModStateText.Left.Pixels + 5f);
				this.tMLUpdateRequired.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					Utils.OpenToURL(CS$<>8__locals1.updateURL);
				};
				base.Append(this.tMLUpdateRequired);
			}
			else
			{
				base.Append(this._uiModStateText);
			}
			int bottomRightRowOffset = -36;
			UIImage uiimage2 = new UIImage(UICommon.ButtonModInfoTexture);
			uiimage2.RemoveFloatingPointsFromDrawPosition = true;
			uiimage2.Width.Pixels = 36f;
			uiimage2.Height.Pixels = 36f;
			uiimage2.Left.Pixels = (float)bottomRightRowOffset;
			uiimage2.Left.Precent = 1f;
			uiimage2.Top.Pixels = 40f;
			this._moreInfoButton = uiimage2;
			this._moreInfoButton.OnLeftClick += this.ShowMoreInfo;
			base.Append(this._moreInfoButton);
			Mod loadedMod;
			if (ModLoader.TryGetMod(this.ModName, out loadedMod) && ConfigManager.Configs.ContainsKey(loadedMod))
			{
				bottomRightRowOffset -= 36;
				UIImage uiimage3 = new UIImage(UICommon.ButtonModConfigTexture);
				uiimage3.RemoveFloatingPointsFromDrawPosition = true;
				uiimage3.Width.Pixels = 36f;
				uiimage3.Height.Pixels = 36f;
				uiimage3.Left.Pixels = (float)bottomRightRowOffset - 5f;
				uiimage3.Left.Precent = 1f;
				uiimage3.Top.Pixels = 40f;
				this._configButton = uiimage3;
				this._configButton.OnLeftClick += this.OpenConfig;
				base.Append(this._configButton);
				if (ConfigManager.ModNeedsReload(loadedMod))
				{
					this._configChangesRequireReload = true;
				}
			}
			this._modReferences = (from x in this._mod.properties.modReferences
			select x.mod).ToArray<string>();
			CS$<>8__locals1.availableMods = ModOrganizer.RecheckVersionsToLoad();
			this._modRequiresTooltip = "";
			HashSet<string> allDependencies = new HashSet<string>();
			UIModItem.<OnInitialize>g__GetDependencies|37_2(this._mod.Name, allDependencies);
			this._modDependencies = allDependencies.ToArray<string>();
			if (this._modDependencies.Any<string>())
			{
				string refs = string.Join("\n", this._modDependencies.Select(delegate(string x)
				{
					string str2 = "- ";
					UIModItem uimodItem = Interface.modsMenu.FindUIModItem(x);
					return str2 + (((uimodItem != null) ? uimodItem._mod.DisplayName : null) ?? (x + Language.GetTextValue("tModLoader.ModPackMissing")));
				}));
				this._modRequiresTooltip += Language.GetTextValue("tModLoader.ModDependencyTooltip", refs);
			}
			HashSet<string> allDependents = new HashSet<string>();
			CS$<>8__locals1.<OnInitialize>g__GetDependents|3(this._mod.Name, allDependents);
			this._modDependents = allDependents.ToArray<string>();
			if (this._modDependents.Any<string>())
			{
				if (!string.IsNullOrWhiteSpace(this._modRequiresTooltip))
				{
					this._modRequiresTooltip += "\n\n";
				}
				string refs2 = string.Join("\n", this._modDependents.Select(delegate(string x)
				{
					string str2 = "- ";
					UIModItem uimodItem = Interface.modsMenu.FindUIModItem(x);
					return str2 + (((uimodItem != null) ? uimodItem._mod.DisplayName : null) ?? (x + Language.GetTextValue("tModLoader.ModPackMissing")));
				}));
				this._modRequiresTooltip += Language.GetTextValue("tModLoader.ModDependentsTooltip", refs2);
			}
			if (!string.IsNullOrWhiteSpace(this._modRequiresTooltip))
			{
				Asset<Texture2D> icon = UICommon.ButtonDepsTexture;
				UIImage uiimage4 = new UIImage(icon);
				uiimage4.RemoveFloatingPointsFromDrawPosition = true;
				uiimage4.Left = new StyleDimension(this._uiModStateText.Left.Pixels + this._uiModStateText.Width.Pixels + 5f + this.left2ndLine, 0f);
				uiimage4.Top.Pixels = 42.5f;
				this._modReferenceIcon = uiimage4;
				this.left2ndLine += 28f;
				base.Append(this._modReferenceIcon);
			}
			if (this._mod.properties.RefNames(true).Any<string>() && this._mod.properties.translationMod)
			{
				Asset<Texture2D> icon2 = UICommon.ButtonTranslationModTexture;
				UIImage uiimage5 = new UIImage(icon2);
				uiimage5.RemoveFloatingPointsFromDrawPosition = true;
				uiimage5.Left = new StyleDimension(this._uiModStateText.Left.Pixels + this._uiModStateText.Width.Pixels + 5f + this.left2ndLine, 0f);
				uiimage5.Top.Pixels = 42.5f;
				this._translationModIcon = uiimage5;
				this.left2ndLine += 28f;
				base.Append(this._translationModIcon);
			}
			if (BuildInfo.IsDev && ModCompile.DeveloperMode && ModLoader.IsUnloadedModStillAlive(this.ModName))
			{
				UIHoverImage uihoverImage = new UIHoverImage(UICommon.ButtonErrorTexture, Language.GetTextValue("tModLoader.ModDidNotFullyUnloadWarning"));
				uihoverImage.RemoveFloatingPointsFromDrawPosition = true;
				uihoverImage.Left.Pixels = (float)this._modIconAdjust + 5f;
				uihoverImage.Top.Pixels = 3f;
				this._keyImage = uihoverImage;
				base.Append(this._keyImage);
				UIText modName = this._modName;
				modName.Left.Pixels = modName.Left.Pixels + (this._keyImage.Width.Pixels + 10f);
				this._modName.Recalculate();
			}
			if (ModOrganizer.CheckStableBuildOnPreview(this._mod))
			{
				UIHoverImage uihoverImage2 = new UIHoverImage(Main.Assets.Request<Texture2D>(TextureAssets.Item[3999].Name), Language.GetTextValue("tModLoader.ModStableOnPreviewWarning"));
				uihoverImage2.Left.Pixels = 4f;
				uihoverImage2.Left.Percent = 0.2f;
				uihoverImage2.Top.Pixels = 0f;
				uihoverImage2.Top.Percent = 0.5f;
				this._keyImage = uihoverImage2;
				base.Append(this._keyImage);
			}
			Asset<Texture2D> texture;
			switch (this._mod.location)
			{
			case ModLocation.Local:
				texture = UICommon.ModLocationLocalIcon;
				break;
			case ModLocation.Workshop:
				texture = TextureAssets.Extra[243];
				break;
			case ModLocation.Modpack:
				texture = UICommon.ModLocationModPackIcon;
				break;
			default:
				throw new NotImplementedException();
			}
			UIHoverImage uihoverImage3 = new UIHoverImage(texture, Language.GetTextValue("tModLoader.ModFrom" + this._mod.location.ToString()));
			uihoverImage3.RemoveFloatingPointsFromDrawPosition = true;
			uihoverImage3.UseTooltipMouseText = true;
			uihoverImage3.Left.Pixels = -22f;
			uihoverImage3.Left.Percent = 1f;
			UIHoverImage modLocationIcon = uihoverImage3;
			base.Append(modLocationIcon);
			if (loadedMod != null)
			{
				this._loaded = true;
				int[] values = new int[]
				{
					loadedMod.GetContent<ModItem>().Count<ModItem>(),
					loadedMod.GetContent<ModNPC>().Count<ModNPC>(),
					loadedMod.GetContent<ModTile>().Count<ModTile>(),
					loadedMod.GetContent<ModWall>().Count<ModWall>(),
					loadedMod.GetContent<ModBuff>().Count<ModBuff>(),
					loadedMod.GetContent<ModMount>().Count<ModMount>()
				};
				string[] localizationKeys = new string[]
				{
					"ModsXItems",
					"ModsXNPCs",
					"ModsXTiles",
					"ModsXWalls",
					"ModsXBuffs",
					"ModsXMounts"
				};
				int xOffset = -40;
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i] > 0)
					{
						UIHoverImage uihoverImage4 = new UIHoverImage(Main.Assets.Request<Texture2D>(TextureAssets.InfoIcon[i].Name), Language.GetTextValue("tModLoader." + localizationKeys[i], values[i]));
						uihoverImage4.RemoveFloatingPointsFromDrawPosition = true;
						uihoverImage4.Left.Pixels = (float)xOffset;
						uihoverImage4.Left.Percent = 1f;
						this._keyImage = uihoverImage4;
						base.Append(this._keyImage);
						xOffset -= 18;
					}
				}
			}
			base.OnLeftDoubleClick += delegate(UIMouseEvent e, UIElement el)
			{
				if (CS$<>8__locals1.<>4__this.tMLUpdateRequired != null)
				{
					return;
				}
				if (e.Target.GetType() != typeof(UIModStateText))
				{
					CS$<>8__locals1.<>4__this._uiModStateText.LeftClick(e);
				}
			};
			if (!this._loaded && ModOrganizer.CanDeleteFrom(this._mod.location))
			{
				bottomRightRowOffset -= 36;
				UIImage uiimage6 = new UIImage(TextureAssets.Trash);
				uiimage6.RemoveFloatingPointsFromDrawPosition = true;
				uiimage6.Width.Pixels = 36f;
				uiimage6.Height.Pixels = 36f;
				uiimage6.Left.Pixels = (float)bottomRightRowOffset - 5f;
				uiimage6.Left.Precent = 1f;
				uiimage6.Top.Pixels = 42.5f;
				this._deleteModButton = uiimage6;
				this._deleteModButton.OnLeftClick += this.QuickModDelete;
				base.Append(this._deleteModButton);
			}
			ValueTuple<string, Version> oldModVersionData = ModOrganizer.modsThatUpdatedSinceLastLaunch.FirstOrDefault(([TupleElementNames(new string[]
			{
				"ModName",
				"previousVersion"
			})] ValueTuple<string, Version> x) => x.Item1 == CS$<>8__locals1.<>4__this.ModName);
			ValueTuple<string, Version> valueTuple = oldModVersionData;
			if (valueTuple.Item1 != null || valueTuple.Item2 != null)
			{
				this.previousVersionHint = oldModVersionData.Item2;
				Asset<Texture2D> toggleImage = Main.Assets.Request<Texture2D>("Images/UI/Settings_Toggle");
				UIImageFramed uiimageFramed = new UIImageFramed(toggleImage, toggleImage.Frame(2, 1, 1, 0, 0, 0));
				uiimageFramed.Left.Pixels = (float)(this._modName.GetInnerDimensions().ToRectangle().Right + 8);
				uiimageFramed.Left.Percent = 0f;
				uiimageFramed.Top.Pixels = 5f;
				uiimageFramed.Top.Percent = 0f;
				uiimageFramed.Color = ((this.previousVersionHint == null) ? Color.Green : new Color(6, 95, 212));
				this.updatedModDot = uiimageFramed;
				base.Append(this.updatedModDot);
			}
			if (loadedMod != null && this._mod.modFile.path != loadedMod.File.path)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(50, 1);
				defaultInterpolatedStringHandler.AppendLiteral("v");
				defaultInterpolatedStringHandler.AppendFormatted<Version>(loadedMod.Version);
				defaultInterpolatedStringHandler.AppendLiteral(" currently loaded due to multiplayer game session");
				UITextPanel<string> uitextPanel = new UITextPanel<string>(defaultInterpolatedStringHandler.ToStringAndClear(), 1f, false);
				uitextPanel.Left = new StyleDimension(0f, 0f);
				uitextPanel.Width = new StyleDimension(0f, 1f);
				uitextPanel.Height = new StyleDimension(30f, 0f);
				uitextPanel.BackgroundColor = Color.Orange;
				uitextPanel.Top.Pixels = 82f;
				UITextPanel<string> serverDiffMessage = uitextPanel;
				base.Append(serverDiffMessage);
				this.Height.Pixels = 130f;
			}
			this.SetHoverColors(false);
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x0051662C File Offset: 0x0051482C
		public override void Draw(SpriteBatch spriteBatch)
		{
			this._tooltip = null;
			base.Draw(spriteBatch);
			if (!string.IsNullOrEmpty(this._tooltip))
			{
				UICommon.TooltipMouseText(this._tooltip);
			}
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x00516654 File Offset: 0x00514854
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			Vector2 drawPos;
			drawPos..ctor(innerDimensions.X + 5f + (float)this._modIconAdjust, innerDimensions.Y + 30f);
			spriteBatch.Draw(UICommon.DividerTexture.Value, drawPos, null, Color.White, 0f, Vector2.Zero, new Vector2((innerDimensions.Width - 10f - (float)this._modIconAdjust) / 8f, 1f), 0, 0f);
			drawPos..ctor(innerDimensions.X + 10f + (float)this._modIconAdjust, innerDimensions.Y + 45f);
			if (this._mod.properties.side != ModSide.Server && (this._mod.Enabled != this._loaded || this._configChangesRequireReload))
			{
				drawPos += new Vector2(this._uiModStateText.Width.Pixels + this.left2ndLine, 0f);
				Utils.DrawBorderString(spriteBatch, this._configChangesRequireReload ? Language.GetTextValue("tModLoader.ModReloadForced") : Language.GetTextValue("tModLoader.ModReloadRequired"), drawPos, Color.White, 1f, 0f, 0f, -1);
			}
			if (this._mod.properties.side == ModSide.Server)
			{
				drawPos += new Vector2(90f, -2f);
				spriteBatch.Draw(UICommon.ModBrowserIconsTexture.Value, drawPos, new Rectangle?(new Rectangle(170, 102, 32, 32)), Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
				if (new Rectangle((int)drawPos.X, (int)drawPos.Y, 32, 32).Contains(Main.MouseScreen.ToPoint()))
				{
					UICommon.DrawHoverStringInBounds(spriteBatch, Language.GetTextValue("tModLoader.ModIsServerSide"), null);
				}
			}
			UIImage moreInfoButton = this._moreInfoButton;
			if (moreInfoButton != null && moreInfoButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("tModLoader.ModsMoreInfo");
				return;
			}
			UIImage deleteModButton = this._deleteModButton;
			if (deleteModButton != null && deleteModButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("UI.Delete");
				return;
			}
			UIText modName = this._modName;
			if (modName != null && modName.IsMouseHovering)
			{
				LocalMod mod = this._mod;
				if (mod != null && mod.properties.author.Length > 0)
				{
					this._tooltip = Language.GetTextValue("tModLoader.ModsByline", this._mod.properties.author);
					return;
				}
			}
			UIModStateText uiModStateText = this._uiModStateText;
			if (uiModStateText != null && uiModStateText.IsMouseHovering)
			{
				this._tooltip = this.ToggleModStateText;
				return;
			}
			UIImage configButton = this._configButton;
			if (configButton != null && configButton.IsMouseHovering)
			{
				this._tooltip = Language.GetTextValue("tModLoader.ModsOpenConfig");
				return;
			}
			UIImageFramed uiimageFramed = this.updatedModDot;
			if (uiimageFramed != null && uiimageFramed.IsMouseHovering)
			{
				if (this.previousVersionHint == null)
				{
					this._tooltip = Language.GetTextValue("tModLoader.ModAddedSinceLastLaunchMessage");
					return;
				}
				this._tooltip = Language.GetTextValue("tModLoader.ModUpdatedSinceLastLaunchMessage", this.previousVersionHint);
				return;
			}
			else
			{
				UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = this.tMLUpdateRequired;
				if (uiautoScaleTextTextPanel != null && uiautoScaleTextTextPanel.IsMouseHovering)
				{
					this._tooltip = Language.GetTextValue("tModLoader.SwitchVersionInfoButton");
					return;
				}
				UIImage modReferenceIcon = this._modReferenceIcon;
				if (modReferenceIcon != null && modReferenceIcon.IsMouseHovering)
				{
					this._tooltip = this._modRequiresTooltip;
					return;
				}
				UIImage translationModIcon = this._translationModIcon;
				if (translationModIcon != null && translationModIcon.IsMouseHovering)
				{
					string refs = string.Join(", ", this._mod.properties.RefNames(true));
					this._tooltip = Language.GetTextValue("tModLoader.TranslationModTooltip", refs);
				}
				return;
			}
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x00516A0A File Offset: 0x00514C0A
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetHoverColors(true);
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x00516A1A File Offset: 0x00514C1A
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetHoverColors(false);
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x00516A2C File Offset: 0x00514C2C
		private void SetHoverColors(bool hovered)
		{
			this.BorderColor = (hovered ? new Color(89, 116, 213) : (new Color(89, 116, 213) * 0.7f));
			if (this._mod.location == ModLocation.Local)
			{
				this.BackgroundColor = (hovered ? Color.MediumPurple : (Color.MediumPurple * 0.7f));
				return;
			}
			if (this._mod.location == ModLocation.Modpack)
			{
				this.BackgroundColor = (hovered ? Color.SkyBlue : (Color.SkyBlue * 0.7f));
				return;
			}
			this.BackgroundColor = (hovered ? UICommon.DefaultUIBlueMouseOver : UICommon.DefaultUIBlue);
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x00516ADC File Offset: 0x00514CDC
		private void ToggleEnabled(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuTick, null, null);
			this._mod.Enabled = !this._mod.Enabled;
			this.UpdateUIForEnabledChange();
			if (!this._mod.Enabled)
			{
				this.DisableDependents();
				return;
			}
			this.EnableDependencies();
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x00516B37 File Offset: 0x00514D37
		internal void Enable()
		{
			if (this._mod.Enabled)
			{
				return;
			}
			this._mod.Enabled = true;
			this.UpdateUIForEnabledChange();
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x00516B59 File Offset: 0x00514D59
		internal void Disable()
		{
			if (!this._mod.Enabled)
			{
				return;
			}
			this._mod.Enabled = false;
			this.UpdateUIForEnabledChange();
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x00516B7B File Offset: 0x00514D7B
		private void UpdateUIForEnabledChange()
		{
			if (this._mod.Enabled)
			{
				this._uiModStateText.SetEnabled();
				return;
			}
			this._uiModStateText.SetDisabled();
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x00516BA4 File Offset: 0x00514DA4
		internal void EnableDependencies()
		{
			List<string> missingRefs = new List<string>();
			this.EnableDepsRecursive(missingRefs);
			if (missingRefs.Any<string>())
			{
				Interface.infoMessage.Show(Language.GetTextValue("tModLoader.ModDependencyModsNotFound", string.Join(", ", missingRefs)), 10000, null, "", null, null);
			}
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x00516BF4 File Offset: 0x00514DF4
		private void EnableDepsRecursive(List<string> missingRefs)
		{
			foreach (string name in this._modReferences)
			{
				UIModItem dep = Interface.modsMenu.FindUIModItem(name);
				if (dep == null)
				{
					missingRefs.Add(name);
				}
				else
				{
					dep.EnableDepsRecursive(missingRefs);
					dep.Enable();
				}
			}
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x00516C3F File Offset: 0x00514E3F
		private void DisableDependents()
		{
			this.DisableDependentsRecursive();
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x00516C48 File Offset: 0x00514E48
		private void DisableDependentsRecursive()
		{
			foreach (string name in this._modDependents)
			{
				UIModItem dep = Interface.modsMenu.FindUIModItem(name);
				if (dep != null)
				{
					dep.DisableDependentsRecursive();
					dep.Disable();
				}
			}
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x00516C8C File Offset: 0x00514E8C
		internal void ShowMoreInfo(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			Interface.modInfo.Show(this.ModName, this._mod.DisplayName, 10000, this._mod, this._mod.properties.description, this._mod.properties.homepage);
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x00516CF4 File Offset: 0x00514EF4
		internal void OpenConfig(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			Interface.modConfigList.ModToSelectOnOpen = ModLoader.GetMod(this.ModName);
			Main.menuMode = 10027;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x00516D38 File Offset: 0x00514F38
		public override int CompareTo(object obj)
		{
			UIModItem item = obj as UIModItem;
			if (item == null)
			{
				return 1;
			}
			string name = this.DisplayNameClean;
			string othername = item.DisplayNameClean;
			switch (Interface.modsMenu.sortMode)
			{
			case ModsMenuSortMode.RecentlyUpdated:
				return -1 * this._mod.lastModified.CompareTo(item._mod.lastModified);
			case ModsMenuSortMode.DisplayNameAtoZ:
				return string.Compare(name, othername, StringComparison.Ordinal);
			case ModsMenuSortMode.DisplayNameZtoA:
				return -1 * string.Compare(name, othername, StringComparison.Ordinal);
			default:
				return base.CompareTo(obj);
			}
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x00516DB8 File Offset: 0x00514FB8
		public bool PassFilters(UIModsFilterResults filterResults)
		{
			if (Interface.modsMenu.filter.Length > 0)
			{
				if (Interface.modsMenu.searchFilterMode == SearchFilter.Author)
				{
					if (this._mod.properties.author.IndexOf(Interface.modsMenu.filter, StringComparison.OrdinalIgnoreCase) == -1)
					{
						filterResults.filteredBySearch++;
						return false;
					}
				}
				else if (this.DisplayNameClean.IndexOf(Interface.modsMenu.filter, StringComparison.OrdinalIgnoreCase) == -1 && this.ModName.IndexOf(Interface.modsMenu.filter, StringComparison.OrdinalIgnoreCase) == -1)
				{
					filterResults.filteredBySearch++;
					return false;
				}
			}
			if (Interface.modsMenu.modSideFilterMode != ModSideFilter.All && this._mod.properties.side != (ModSide)(Interface.modsMenu.modSideFilterMode - 1))
			{
				filterResults.filteredByModSide++;
				return false;
			}
			switch (Interface.modsMenu.enabledFilterMode)
			{
			default:
				return true;
			case EnabledFilter.EnabledOnly:
				if (!this._mod.Enabled)
				{
					filterResults.filteredByEnabled++;
				}
				return this._mod.Enabled;
			case EnabledFilter.DisabledOnly:
				if (this._mod.Enabled)
				{
					filterResults.filteredByEnabled++;
				}
				return !this._mod.Enabled;
			}
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x00516F08 File Offset: 0x00515108
		private void QuickModDelete(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!Main.keyState.PressingShift())
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				UIImage uiimage = new UIImage(TextureAssets.Extra[190]);
				uiimage.Width.Percent = 1f;
				uiimage.Height.Percent = 1f;
				uiimage.Color = new Color(0, 0, 0, 0);
				uiimage.ScaleToFit = true;
				this._blockInput = uiimage;
				this._blockInput.OnLeftMouseDown += this.CloseDialog;
				Interface.modsMenu.Append(this._blockInput);
				UIPanel uipanel = new UIPanel();
				uipanel.Width.Percent = 0.3f;
				uipanel.Height.Percent = 0.3f;
				uipanel.HAlign = 0.5f;
				uipanel.VAlign = 0.5f;
				uipanel.BackgroundColor = new Color(63, 82, 151);
				uipanel.BorderColor = Color.Black;
				this._deleteModDialog = uipanel;
				this._deleteModDialog.SetPadding(6f);
				Interface.modsMenu.Append(this._deleteModDialog);
				UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("LegacyMenu.104"), 1f, false);
				uiautoScaleTextTextPanel.TextColor = Color.White;
				uiautoScaleTextTextPanel.Width = new StyleDimension(-10f, 0.33333334f);
				uiautoScaleTextTextPanel.Height.Pixels = 40f;
				uiautoScaleTextTextPanel.VAlign = 0.85f;
				uiautoScaleTextTextPanel.HAlign = 0.15f;
				this._dialogYesButton = uiautoScaleTextTextPanel.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
				this._dialogYesButton.OnLeftClick += this.DeleteMod;
				this._deleteModDialog.Append(this._dialogYesButton);
				UIAutoScaleTextTextPanel<string> uiautoScaleTextTextPanel2 = new UIAutoScaleTextTextPanel<string>(Language.GetTextValue("LegacyMenu.105"), 1f, false);
				uiautoScaleTextTextPanel2.TextColor = Color.White;
				uiautoScaleTextTextPanel2.Width = new StyleDimension(-10f, 0.33333334f);
				uiautoScaleTextTextPanel2.Height.Pixels = 40f;
				uiautoScaleTextTextPanel2.VAlign = 0.85f;
				uiautoScaleTextTextPanel2.HAlign = 0.85f;
				this._dialogNoButton = uiautoScaleTextTextPanel2.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
				this._dialogNoButton.OnLeftClick += this.CloseDialog;
				this._deleteModDialog.Append(this._dialogNoButton);
				UIText uitext = new UIText(Language.GetTextValue("tModLoader.DeleteModConfirm"), 1f, false);
				uitext.Width.Percent = 0.75f;
				uitext.HAlign = 0.5f;
				uitext.VAlign = 0.3f;
				uitext.IsWrapped = true;
				this._dialogText = uitext;
				this._deleteModDialog.Append(this._dialogText);
				Interface.modsMenu.Recalculate();
				return;
			}
			this.DeleteMod(evt, listeningElement);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x005171FC File Offset: 0x005153FC
		private void CloseDialog(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(SoundID.MenuClose, null, null);
			UIImage blockInput = this._blockInput;
			if (blockInput != null)
			{
				blockInput.Remove();
			}
			UIPanel deleteModDialog = this._deleteModDialog;
			if (deleteModDialog == null)
			{
				return;
			}
			deleteModDialog.Remove();
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x0051723F File Offset: 0x0051543F
		private void DeleteMod(UIMouseEvent evt, UIElement listeningElement)
		{
			ModOrganizer.DeleteMod(this._mod);
			this.CloseDialog(evt, listeningElement);
			Interface.modsMenu.StoreCurrentScrollPosition();
			Interface.modsMenu.Activate();
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x00517268 File Offset: 0x00515468
		private bool CheckIfPublishedForThisBrowserVersion(out string recommendedModBrowserVersion)
		{
			recommendedModBrowserVersion = SocialBrowserModule.GetBrowserVersionNumber(this._mod.tModLoaderVersion);
			return recommendedModBrowserVersion == SocialBrowserModule.GetBrowserVersionNumber(BuildInfo.tMLVersion);
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x00517290 File Offset: 0x00515490
		[CompilerGenerated]
		internal static void <OnInitialize>g__GetDependencies|37_2(string modName, HashSet<string> allDependencies)
		{
			UIModItem modItem = Interface.modsMenu.FindUIModItem(modName);
			if (modItem == null)
			{
				return;
			}
			foreach (string dependency in (from x in modItem._mod.properties.modReferences
			select x.mod).ToArray<string>())
			{
				if (allDependencies.Add(dependency))
				{
					UIModItem.<OnInitialize>g__GetDependencies|37_2(dependency, allDependencies);
				}
			}
		}

		// Token: 0x04001AA5 RID: 6821
		private const float PADDING = 5f;

		// Token: 0x04001AA6 RID: 6822
		private float left2ndLine;

		// Token: 0x04001AA7 RID: 6823
		private UIImage _moreInfoButton;

		// Token: 0x04001AA8 RID: 6824
		private UIImage _modIcon;

		// Token: 0x04001AA9 RID: 6825
		private UIImageFramed updatedModDot;

		// Token: 0x04001AAA RID: 6826
		private Version previousVersionHint;

		// Token: 0x04001AAB RID: 6827
		private UIHoverImage _keyImage;

		// Token: 0x04001AAC RID: 6828
		private UIImage _configButton;

		// Token: 0x04001AAD RID: 6829
		private UIText _modName;

		// Token: 0x04001AAE RID: 6830
		private UIModStateText _uiModStateText;

		// Token: 0x04001AAF RID: 6831
		internal UIAutoScaleTextTextPanel<string> tMLUpdateRequired;

		// Token: 0x04001AB0 RID: 6832
		private UIImage _modReferenceIcon;

		// Token: 0x04001AB1 RID: 6833
		private UIImage _translationModIcon;

		// Token: 0x04001AB2 RID: 6834
		private UIImage _deleteModButton;

		// Token: 0x04001AB3 RID: 6835
		private UIAutoScaleTextTextPanel<string> _dialogYesButton;

		// Token: 0x04001AB4 RID: 6836
		private UIAutoScaleTextTextPanel<string> _dialogNoButton;

		// Token: 0x04001AB5 RID: 6837
		private UIText _dialogText;

		// Token: 0x04001AB6 RID: 6838
		private UIImage _blockInput;

		// Token: 0x04001AB7 RID: 6839
		private UIPanel _deleteModDialog;

		// Token: 0x04001AB8 RID: 6840
		private readonly LocalMod _mod;

		// Token: 0x04001AB9 RID: 6841
		private bool modFromLocalModFolder;

		// Token: 0x04001ABA RID: 6842
		private bool _configChangesRequireReload;

		// Token: 0x04001ABB RID: 6843
		private bool _loaded;

		// Token: 0x04001ABC RID: 6844
		private int _modIconAdjust;

		// Token: 0x04001ABD RID: 6845
		private string _tooltip;

		// Token: 0x04001ABE RID: 6846
		private string[] _modReferences;

		// Token: 0x04001ABF RID: 6847
		private string[] _modDependents;

		// Token: 0x04001AC0 RID: 6848
		private string[] _modDependencies;

		// Token: 0x04001AC1 RID: 6849
		private string _modRequiresTooltip;

		// Token: 0x04001AC2 RID: 6850
		public readonly string DisplayNameClean;
	}
}
