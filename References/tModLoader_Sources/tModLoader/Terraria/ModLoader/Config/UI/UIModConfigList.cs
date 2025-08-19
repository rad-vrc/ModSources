using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.ModLoader.Config.UI
{
	// Token: 0x020003C2 RID: 962
	internal class UIModConfigList : UIState
	{
		// Token: 0x0600330E RID: 13070 RVA: 0x005480E8 File Offset: 0x005462E8
		public override void OnInitialize()
		{
			UIElement uielement = new UIElement();
			uielement.Width.Percent = 0.8f;
			uielement.MaxWidth.Pixels = 800f;
			uielement.MaxWidth.Percent = 0f;
			uielement.Top.Pixels = 220f;
			uielement.Height.Pixels = -220f;
			uielement.Height.Percent = 1f;
			uielement.HAlign = 0.5f;
			this.uIElement = uielement;
			base.Append(this.uIElement);
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Percent = 1f;
			uipanel.Height.Pixels = -110f;
			uipanel.Height.Percent = 1f;
			uipanel.BackgroundColor = UICommon.MainPanelBackground;
			this.uIPanel = uipanel;
			this.uIElement.Append(this.uIPanel);
			UITextPanel<LocalizedText> uitextPanel = new UITextPanel<LocalizedText>(Language.GetText("tModLoader.ModConfiguration"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Pixels = -35f;
			uitextPanel.BackgroundColor = UICommon.DefaultUIBlue;
			UITextPanel<LocalizedText> uIHeaderTextPanel = uitextPanel.WithPadding(15f);
			this.uIElement.Append(uIHeaderTextPanel);
			UIPanel uipanel2 = new UIPanel();
			uipanel2.Width.Pixels = this.uIPanel.PaddingTop / -2f;
			uipanel2.Width.Percent = 0.5f;
			uipanel2.Height.Percent = 1f;
			UIPanel modListPanel = uipanel2;
			this.uIPanel.Append(modListPanel);
			UIPanel uipanel3 = new UIPanel();
			uipanel3.Width.Pixels = this.uIPanel.PaddingTop / -2f;
			uipanel3.Width.Percent = 0.5f;
			uipanel3.Height.Percent = 1f;
			uipanel3.HAlign = 1f;
			UIPanel configListPanel = uipanel3;
			this.uIPanel.Append(configListPanel);
			float headerHeight = 35f;
			UIText uitext = new UIText(Language.GetText("tModLoader.MenuMods"), 0.5f, true);
			uitext.Top.Pixels = 5f;
			uitext.Left.Pixels = 12.5f;
			uitext.HAlign = 0.5f;
			UIText modListHeader = uitext;
			modListPanel.Append(modListHeader);
			UIText uitext2 = new UIText(Language.GetText("tModLoader.ModConfigs"), 0.5f, true);
			uitext2.Top.Pixels = 5f;
			uitext2.Left.Pixels = -12.5f;
			uitext2.HAlign = 0.5f;
			UIText configListHeader = uitext2;
			configListPanel.Append(configListHeader);
			UIList uilist = new UIList();
			uilist.Top.Pixels = headerHeight;
			uilist.Width.Pixels = -25f;
			uilist.Width.Percent = 1f;
			uilist.Height.Pixels = -headerHeight;
			uilist.Height.Percent = 1f;
			uilist.ListPadding = 5f;
			uilist.HAlign = 1f;
			uilist.ManualSortMethod = delegate(List<UIElement> list)
			{
			};
			this.modList = uilist;
			modListPanel.Append(this.modList);
			UIList uilist2 = new UIList();
			uilist2.Top.Pixels = headerHeight;
			uilist2.Width.Pixels = -25f;
			uilist2.Width.Percent = 1f;
			uilist2.Height.Pixels = -headerHeight;
			uilist2.Height.Percent = 1f;
			uilist2.ListPadding = 5f;
			uilist2.HAlign = 0f;
			uilist2.ManualSortMethod = delegate(List<UIElement> list)
			{
			};
			this.configList = uilist2;
			configListPanel.Append(this.configList);
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.Top.Pixels = headerHeight;
			uiscrollbar.Height.Pixels = -headerHeight;
			uiscrollbar.Height.Percent = 1f;
			UIScrollbar modListScrollbar = uiscrollbar;
			modListScrollbar.SetView(100f, 1000f);
			this.modList.SetScrollbar(modListScrollbar);
			modListPanel.Append(modListScrollbar);
			UIScrollbar uiscrollbar2 = new UIScrollbar();
			uiscrollbar2.Top.Pixels = headerHeight;
			uiscrollbar2.Height.Pixels = -headerHeight;
			uiscrollbar2.Height.Percent = 1f;
			uiscrollbar2.HAlign = 1f;
			UIScrollbar configListScrollbar = uiscrollbar2;
			configListScrollbar.SetView(100f, 1000f);
			this.configList.SetScrollbar(configListScrollbar);
			configListPanel.Append(configListScrollbar);
			UITextPanel<LocalizedText> uitextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uitextPanel2.Width.Pixels = -10f;
			uitextPanel2.Width.Percent = 0.5f;
			uitextPanel2.Height.Pixels = 50f;
			uitextPanel2.Top.Pixels = -45f;
			uitextPanel2.VAlign = 1f;
			uitextPanel2.HAlign = 0.5f;
			this.backButton = uitextPanel2.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this.backButton.OnLeftClick += delegate(UIMouseEvent _, UIElement _)
			{
				SoundEngine.PlaySound(SoundID.MenuClose, null, null);
				if (Main.gameMenu)
				{
					Main.menuMode = 10000;
					return;
				}
				IngameFancyUI.Close();
			};
			this.uIElement.Append(this.backButton);
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x00548627 File Offset: 0x00546827
		internal void Unload()
		{
			UIList uilist = this.modList;
			if (uilist != null)
			{
				uilist.Clear();
			}
			UIList uilist2 = this.configList;
			if (uilist2 != null)
			{
				uilist2.Clear();
			}
			this.selectedMod = null;
			this.ModToSelectOnOpen = null;
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x0054865C File Offset: 0x0054685C
		public override void OnActivate()
		{
			UIList uilist = this.modList;
			if (uilist != null)
			{
				uilist.Clear();
			}
			UIList uilist2 = this.configList;
			if (uilist2 != null)
			{
				uilist2.Clear();
			}
			this.selectedMod = null;
			if (this.ModToSelectOnOpen != null)
			{
				this.selectedMod = this.ModToSelectOnOpen;
				this.ModToSelectOnOpen = null;
			}
			this.PopulateMods();
			if (this.selectedMod != null)
			{
				this.PopulateConfigs();
			}
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x005486C4 File Offset: 0x005468C4
		private void PopulateMods()
		{
			UIList uilist = this.modList;
			if (uilist != null)
			{
				uilist.Clear();
			}
			List<Mod> list = ModLoader.Mods.ToList<Mod>();
			list.Sort((Mod x, Mod y) => x.DisplayNameClean.CompareTo(y.DisplayNameClean));
			using (List<Mod>.Enumerator enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Mod mod = enumerator.Current;
					List<ModConfig> list2;
					if (ConfigManager.Configs.TryGetValue(mod, out list2))
					{
						UIButton<string> uibutton = new UIButton<string>(mod.DisplayName, 1f, false);
						uibutton.MaxWidth.Percent = 0.95f;
						uibutton.HAlign = 0.5f;
						uibutton.ScalePanel = true;
						uibutton.AltPanelColor = new Color?(UICommon.MainPanelBackground);
						uibutton.AltHoverPanelColor = new Color?(UICommon.MainPanelBackground * 1.25f);
						uibutton.UseAltColors = (() => this.selectedMod != mod);
						uibutton.ClickSound = new SoundStyle?(SoundID.MenuTick);
						UIButton<string> modPanel = uibutton;
						modPanel.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
						{
							this.selectedMod = mod;
							this.PopulateConfigs();
						};
						this.modList.Add(modPanel);
					}
					else if (!(mod.Name == "ModLoader"))
					{
						UIButton<string> uibutton2 = new UIButton<string>(mod.DisplayName, 1f, false);
						uibutton2.MaxWidth.Percent = 0.95f;
						uibutton2.HAlign = 0.5f;
						uibutton2.ScalePanel = true;
						uibutton2.BackgroundColor = Color.Gray;
						uibutton2.HoverPanelColor = Color.Gray;
						uibutton2.HoverBorderColor = Color.Black;
						uibutton2.TooltipText = true;
						uibutton2.HoverText = Language.GetTextValue("tModLoader.ModConfigModLoaderButNoConfigs");
						UIButton<string> modPanel2 = uibutton2;
						this.modList.Add(modPanel2);
					}
				}
			}
		}

		// Token: 0x06003312 RID: 13074 RVA: 0x005488C4 File Offset: 0x00546AC4
		private void PopulateConfigs()
		{
			UIList uilist = this.configList;
			if (uilist != null)
			{
				uilist.Clear();
			}
			List<ModConfig> configs;
			if (this.selectedMod == null || !ConfigManager.Configs.TryGetValue(this.selectedMod, out configs))
			{
				return;
			}
			using (List<ModConfig>.Enumerator enumerator = (from x in configs
			orderby Utils.CleanChatTags(x.DisplayName.Value)
			select x).ToList<ModConfig>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					UIModConfigList.<>c__DisplayClass11_0 CS$<>8__locals1 = new UIModConfigList.<>c__DisplayClass11_0();
					CS$<>8__locals1.<>4__this = this;
					CS$<>8__locals1.config = enumerator.Current;
					float indicatorOffset = 24f;
					UIButton<LocalizedText> uibutton = new UIButton<LocalizedText>(CS$<>8__locals1.config.DisplayName, 1f, false);
					uibutton.MaxWidth.Percent = 0.95f;
					uibutton.HAlign = 0.5f;
					uibutton.ScalePanel = true;
					uibutton.UseInnerDimensions = true;
					uibutton.ClickSound = new SoundStyle?(SoundID.MenuOpen);
					UIButton<LocalizedText> configPanel = uibutton;
					configPanel.PaddingRight += indicatorOffset;
					configPanel.OnLeftClick += delegate(UIMouseEvent evt, UIElement listeningElement)
					{
						Interface.modConfig.SetMod(CS$<>8__locals1.<>4__this.selectedMod, CS$<>8__locals1.config, false, null, null, true);
						if (Main.gameMenu)
						{
							Main.menuMode = 10024;
							return;
						}
						Main.InGameUI.SetState(Interface.modConfig);
					};
					this.configList.Add(configPanel);
					Asset<Texture2D> indicatorTexture = UICommon.ConfigSideIndicatorTexture;
					Rectangle indicatorFrame = indicatorTexture.Frame(2, 1, (CS$<>8__locals1.config.Mode == ConfigScope.ServerSide) ? 1 : 0, 0, 0, 0);
					Color serverColor = Colors.RarityRed;
					Color clientColor = Colors.RarityCyan;
					UIImageFramed sideIndicator = new UIImageFramed(indicatorTexture, indicatorFrame)
					{
						VAlign = 0.5f,
						HAlign = 1f,
						Color = Color.White,
						MarginRight = -indicatorOffset - 4f,
						MarginTop = -4f
					};
					sideIndicator.OnUpdate += delegate(UIElement affectedElement)
					{
						if (sideIndicator.IsMouseHovering)
						{
							string colorCode = (CS$<>8__locals1.config.Mode == ConfigScope.ServerSide) ? serverColor.Hex3() : clientColor.Hex3();
							string hoverText = Language.GetTextValue((CS$<>8__locals1.config.Mode == ConfigScope.ServerSide) ? "tModLoader.ModConfigServerSide" : "tModLoader.ModConfigClientSide");
							Main instance = Main.instance;
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
							defaultInterpolatedStringHandler.AppendLiteral("[c/");
							defaultInterpolatedStringHandler.AppendFormatted(colorCode);
							defaultInterpolatedStringHandler.AppendLiteral(":");
							defaultInterpolatedStringHandler.AppendFormatted(hoverText);
							defaultInterpolatedStringHandler.AppendLiteral("]");
							instance.MouseText(defaultInterpolatedStringHandler.ToStringAndClear(), 0, 0, -1, -1, -1, -1, 0);
						}
					};
					configPanel.Append(sideIndicator);
				}
			}
		}

		// Token: 0x06003313 RID: 13075 RVA: 0x00548AD4 File Offset: 0x00546CD4
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 100;
			UILinkPointNavigator.Shortcuts.BackButtonGoto = 10000;
		}

		// Token: 0x04001DE5 RID: 7653
		public Mod ModToSelectOnOpen;

		// Token: 0x04001DE6 RID: 7654
		private Mod selectedMod;

		// Token: 0x04001DE7 RID: 7655
		private UIElement uIElement;

		// Token: 0x04001DE8 RID: 7656
		private UIPanel uIPanel;

		// Token: 0x04001DE9 RID: 7657
		private UITextPanel<LocalizedText> backButton;

		// Token: 0x04001DEA RID: 7658
		private UIList modList;

		// Token: 0x04001DEB RID: 7659
		private UIList configList;
	}
}
