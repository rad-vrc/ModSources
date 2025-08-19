using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000260 RID: 608
	internal class UIServerModsDifferMessage : UIState, IHaveBackButtonCommand
	{
		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06002AB6 RID: 10934 RVA: 0x0051DAD0 File Offset: 0x0051BCD0
		// (set) Token: 0x06002AB7 RID: 10935 RVA: 0x0051DAD8 File Offset: 0x0051BCD8
		public UIState PreviousUIState { get; set; }

		// Token: 0x06002AB8 RID: 10936 RVA: 0x0051DAE4 File Offset: 0x0051BCE4
		public override void OnInitialize()
		{
			UIElement uielement = new UIElement();
			uielement.Width.Percent = 0.8f;
			uielement.MaxWidth = UICommon.MaxPanelWidth;
			uielement.Top.Pixels = 200f;
			uielement.Height.Pixels = -200f;
			uielement.Height.Percent = 1f;
			uielement.HAlign = 0.5f;
			this._area = uielement;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Percent = 1f;
			uipanel.Height.Pixels = -100f;
			uipanel.Height.Percent = 1f;
			uipanel.BackgroundColor = UICommon.MainPanelBackground;
			UIPanel uIPanel = uipanel;
			this._area.Append(uIPanel);
			UIPanel uipanel2 = new UIPanel();
			uipanel2.Width.Percent = 1f;
			uipanel2.Height.Pixels = 100f;
			this.messagePanel = uipanel2;
			uIPanel.Append(this.messagePanel);
			UIText uitext = new UIText("", 1f, false);
			uitext.Width.Percent = 1f;
			uitext.Height.Percent = 1f;
			this.message = uitext;
			this.message.IsWrapped = true;
			this.messagePanel.Append(this.message);
			UIScrollbar uiscrollbar = new UIScrollbar();
			uiscrollbar.Height.Pixels = -118f;
			uiscrollbar.Height.Percent = 1f;
			uiscrollbar.Top.Pixels = 112f;
			uiscrollbar.HAlign = 1f;
			UIScrollbar uIScrollbar = uiscrollbar.WithView(100f, 1000f);
			uIPanel.Append(uIScrollbar);
			UIPanel uipanel3 = new UIPanel();
			uipanel3.Top.Pixels = 106f;
			uipanel3.Width.Pixels = -24f;
			uipanel3.Width.Percent = 1f;
			uipanel3.Height.Pixels = -106f;
			uipanel3.Height.Percent = 1f;
			UIPanel modPanel = uipanel3;
			modPanel.SetPadding(6f);
			uIPanel.Append(modPanel);
			UIList uilist = new UIList();
			uilist.Width.Percent = 1f;
			uilist.Height.Percent = 1f;
			uilist.ListPadding = 5f;
			this.modList = uilist;
			this.modList.ManualSortMethod = delegate(List<UIElement> e)
			{
			};
			modPanel.Append(this.modList);
			this.modList.SetScrollbar(uIScrollbar);
			UITextPanel<string> uitextPanel = new UITextPanel<string>("???", 0.7f, true);
			uitextPanel.Width.Pixels = -6f;
			uitextPanel.Width.Percent = 0.33333334f;
			uitextPanel.Height.Pixels = 50f;
			uitextPanel.Left.Percent = 0f;
			uitextPanel.VAlign = 1f;
			uitextPanel.Top.Pixels = -45f;
			this._backButton = uitextPanel.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._backButton.OnLeftClick += this.BackClick;
			this._area.Append(this._backButton);
			UITextPanel<string> uitextPanel2 = new UITextPanel<string>("???", 0.7f, true);
			uitextPanel2.Width.Pixels = -6f;
			uitextPanel2.Width.Percent = 0.6666667f;
			uitextPanel2.Height.Pixels = 50f;
			uitextPanel2.VAlign = 1f;
			uitextPanel2.HAlign = 1f;
			uitextPanel2.Top.Pixels = -45f;
			this._continueButton = uitextPanel2.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			this._continueButton.OnLeftClick += this.ContinueClick;
			this._area.Append(this._continueButton);
			base.Append(this._area);
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x0051DEEC File Offset: 0x0051C0EC
		public override void OnActivate()
		{
			this.message.SetText(this._message);
			this._continueButton.SetText(this._continueButtonText);
			this._backButton.SetText(this._backText);
			this.modList.Clear();
			foreach (ReloadRequiredExplanation entry in this.reloadRequiredExplanationEntries)
			{
				UIPanel panel = new UIPanel();
				panel.SetPadding(6f);
				panel.Width.Set(0f, 1f);
				panel.Height.Set(92f, 0f);
				panel.BackgroundColor = UICommon.DefaultUIBlue;
				LocalMod localMod = entry.localMod;
				UIText uitext = new UIText(((localMod != null) ? localMod.DisplayName : null) ?? entry.mod, 1f, false);
				uitext.Top.Pixels = 2f;
				uitext.Left.Pixels = 85f;
				UIText modName = uitext;
				UIText uitext2 = new UIText(entry.reason, 1f, false);
				uitext2.Top.Pixels = 30f;
				uitext2.Left.Pixels = 112f;
				UIText reason = uitext2;
				Asset<Texture2D> modIcon = Main.Assets.Request<Texture2D>("Images/UI/DefaultResourcePackIcon", 1);
				if (entry.localMod != null)
				{
					try
					{
						using (entry.localMod.modFile.Open())
						{
							using (Stream s = entry.localMod.modFile.GetStream("icon.png", false))
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
						Logging.tML.Error("Unknown error", e);
					}
				}
				UIImage uiimage = new UIImage(modIcon);
				uiimage.Left.Percent = 0f;
				uiimage.Top.Percent = 0f;
				uiimage.Width.Pixels = 80f;
				uiimage.Height.Pixels = 80f;
				uiimage.ScaleToFit = true;
				UIImage modIconImage = uiimage;
				panel.Append(modIconImage);
				panel.Append(modName);
				panel.Append(reason);
				this.modList.Add(panel);
			}
			if (Main.tServer != null)
			{
				UIPanel panel2 = new UIPanel();
				panel2.Width.Set(0f, 1f);
				panel2.Height.Set(130f, 0f);
				panel2.BackgroundColor = Color.Orange;
				UIText uitext3 = new UIText(Language.GetTextValue("tModLoader.ReloadRequiredHostAndPlayModWasDisabledHint"), 1f, false);
				uitext3.Width.Percent = 1f;
				uitext3.Height.Percent = 1f;
				this.message = uitext3;
				this.message.IsWrapped = true;
				this.message.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					Utils.OpenToURL("https://github.com/tModLoader/tModLoader/wiki/Debugging-Multiplayer-Usage-Issues#when-i-join-my-own-server-mods-get-disabled");
				};
				panel2.Append(this.message);
				this.modList.Add(panel2);
			}
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x0051E280 File Offset: 0x0051C480
		internal void Show(string message, int gotoMenu, UIState gotoState = null, string continueButtonText = "", Action continueButtonAction = null, string backButtonText = null, Action backButtonAction = null, List<ReloadRequiredExplanation> reloadRequiredExplanationEntries = null)
		{
			if (!Program.IsMainThread)
			{
				Main.QueueMainThreadAction(delegate
				{
					this.Show(message, gotoMenu, gotoState, continueButtonText, continueButtonAction, backButtonText, backButtonAction, reloadRequiredExplanationEntries);
				});
				return;
			}
			this._message = message;
			this._gotoMenu = gotoMenu;
			this._gotoState = gotoState;
			this._continueButtonText = continueButtonText;
			this._continueButtonAction = continueButtonAction;
			this._backText = backButtonText;
			this._backAction = backButtonAction;
			List<ReloadRequiredExplanation> list = reloadRequiredExplanationEntries;
			List<ReloadRequiredExplanation> list2;
			if (list == null)
			{
				list2 = null;
			}
			else
			{
				list2 = (from x in list
				orderby x.typeOrder, x.mod
				select x).ToList<ReloadRequiredExplanation>();
			}
			this.reloadRequiredExplanationEntries = list2;
			Main.menuMode = 10028;
			Main.MenuUI.SetState(null);
			Main.alreadyGrabbingSunOrMoon = false;
			Logging.tML.Info("ModsDifferMessage: " + message + "\n" + string.Join("\n", reloadRequiredExplanationEntries.Select(delegate(ReloadRequiredExplanation x)
			{
				string str = "    ";
				LocalMod localMod = x.localMod;
				return str + (((localMod != null) ? localMod.DisplayNameClean : null) ?? x.mod) + ": " + Utils.CleanChatTags(x.reason).Replace("\n", " ");
			})));
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x0051E412 File Offset: 0x0051C612
		private void BackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x06002ABC RID: 10940 RVA: 0x0051E41C File Offset: 0x0051C61C
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
			Main.menuMode = this._gotoMenu;
			if (this._gotoState != null)
			{
				Main.MenuUI.SetState(this._gotoState);
			}
			this._backAction();
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x0051E46C File Offset: 0x0051C66C
		private void ContinueClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this._continueButtonAction();
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x0051E479 File Offset: 0x0051C679
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
		}

		// Token: 0x04001B40 RID: 6976
		protected UIElement _area;

		// Token: 0x04001B41 RID: 6977
		private UIPanel messagePanel;

		// Token: 0x04001B42 RID: 6978
		private UIText message;

		// Token: 0x04001B43 RID: 6979
		private UIList modList;

		// Token: 0x04001B44 RID: 6980
		protected UITextPanel<string> _backButton;

		// Token: 0x04001B45 RID: 6981
		private UITextPanel<string> _continueButton;

		// Token: 0x04001B46 RID: 6982
		private UIState _gotoState;

		// Token: 0x04001B47 RID: 6983
		private string _message;

		// Token: 0x04001B48 RID: 6984
		private int _gotoMenu;

		// Token: 0x04001B49 RID: 6985
		private Action _continueButtonAction;

		// Token: 0x04001B4A RID: 6986
		private Action _backAction;

		// Token: 0x04001B4B RID: 6987
		private string _continueButtonText;

		// Token: 0x04001B4C RID: 6988
		private string _backText;

		// Token: 0x04001B4D RID: 6989
		private List<ReloadRequiredExplanation> reloadRequiredExplanationEntries;
	}
}
