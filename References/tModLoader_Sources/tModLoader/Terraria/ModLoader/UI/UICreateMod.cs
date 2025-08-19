using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Core;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.ModLoader.UI
{
	// Token: 0x02000241 RID: 577
	public class UICreateMod : UIState, IHaveBackButtonCommand
	{
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x0051182E File Offset: 0x0050FA2E
		// (set) Token: 0x0600295A RID: 10586 RVA: 0x00511836 File Offset: 0x0050FA36
		public UIState PreviousUIState { get; set; }

		// Token: 0x0600295B RID: 10587 RVA: 0x00511840 File Offset: 0x0050FA40
		public override void OnInitialize()
		{
			UICreateMod.<>c__DisplayClass10_0 CS$<>8__locals1 = new UICreateMod.<>c__DisplayClass10_0();
			CS$<>8__locals1.<>4__this = this;
			UIElement uielement = new UIElement();
			uielement.Width.Percent = 0.8f;
			uielement.MaxWidth = UICommon.MaxPanelWidth;
			uielement.Top.Pixels = 220f;
			uielement.Height.Pixels = -220f;
			uielement.Height.Percent = 1f;
			uielement.HAlign = 0.5f;
			this._baseElement = uielement;
			base.Append(this._baseElement);
			UICreateMod.<>c__DisplayClass10_0 CS$<>8__locals2 = CS$<>8__locals1;
			UIPanel uipanel = new UIPanel();
			uipanel.Width.Percent = 1f;
			uipanel.Height.Pixels = -110f;
			uipanel.Height.Percent = 1f;
			uipanel.BackgroundColor = UICommon.MainPanelBackground;
			uipanel.PaddingTop = 0f;
			CS$<>8__locals2.mainPanel = uipanel;
			this._baseElement.Append(CS$<>8__locals1.mainPanel);
			UITextPanel<string> uitextPanel = new UITextPanel<string>(Language.GetTextValue("tModLoader.MSCreateMod"), 0.8f, true);
			uitextPanel.HAlign = 0.5f;
			uitextPanel.Top.Pixels = -35f;
			uitextPanel.BackgroundColor = UICommon.DefaultUIBlue;
			UITextPanel<string> uITextPanel = uitextPanel.WithPadding(15f);
			this._baseElement.Append(uITextPanel);
			UITextPanel<string> uitextPanel2 = new UITextPanel<string>(Language.GetTextValue(""), 1f, false);
			uitextPanel2.Width.Percent = 1f;
			uitextPanel2.Height.Pixels = 25f;
			uitextPanel2.VAlign = 1f;
			uitextPanel2.Top.Pixels = -20f;
			this._messagePanel = uitextPanel2;
			UITextPanel<string> uitextPanel3 = new UITextPanel<string>(Language.GetTextValue("UI.Back"), 1f, false);
			uitextPanel3.Width.Pixels = -10f;
			uitextPanel3.Width.Percent = 0.5f;
			uitextPanel3.Height.Pixels = 25f;
			uitextPanel3.VAlign = 1f;
			uitextPanel3.Top.Pixels = -65f;
			UITextPanel<string> buttonBack = uitextPanel3.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			buttonBack.OnLeftClick += this.BackClick;
			this._baseElement.Append(buttonBack);
			UITextPanel<string> buttonCreate = new UITextPanel<string>(Language.GetTextValue("LegacyMenu.28"), 1f, false);
			buttonCreate.CopyStyle(buttonBack);
			buttonCreate.HAlign = 1f;
			buttonCreate.WithFadedMouseOver(default(Color), default(Color), default(Color), default(Color));
			buttonCreate.OnLeftClick += this.OKClick;
			this._baseElement.Append(buttonCreate);
			CS$<>8__locals1.top = 16f;
			this._modName = CS$<>8__locals1.<OnInitialize>g__createAndAppendTextInputWithLabel|5(Language.GetTextValue("tModLoader.CreateModName"), Language.GetTextValue("tModLoader.ModConfigTypeHere"));
			this._modName.OnTextChange += delegate(object a, EventArgs b)
			{
				CS$<>8__locals1.<>4__this._modName.SetText(CS$<>8__locals1.<>4__this._modName.CurrentString.Replace(" ", ""));
			};
			this._modDisplayName = CS$<>8__locals1.<OnInitialize>g__createAndAppendTextInputWithLabel|5(Language.GetTextValue("tModLoader.CreateModDisplayName"), Language.GetTextValue("tModLoader.ModConfigTypeHere"));
			this._modAuthor = CS$<>8__locals1.<OnInitialize>g__createAndAppendTextInputWithLabel|5(Language.GetTextValue("tModLoader.CreateModAuthor"), Language.GetTextValue("tModLoader.ModConfigTypeHere"));
			this._basicSword = CS$<>8__locals1.<OnInitialize>g__createAndAppendTextInputWithLabel|5(Language.GetTextValue("tModLoader.CreateModBasicSword"), Language.GetTextValue("tModLoader.CreateModBasicSwordHint"));
			this._modName.OnTab += delegate(object a, EventArgs b)
			{
				CS$<>8__locals1.<>4__this._modDisplayName.Focused = true;
			};
			this._modDisplayName.OnTab += delegate(object a, EventArgs b)
			{
				CS$<>8__locals1.<>4__this._modAuthor.Focused = true;
			};
			this._modAuthor.OnTab += delegate(object a, EventArgs b)
			{
				CS$<>8__locals1.<>4__this._basicSword.Focused = true;
			};
			this._basicSword.OnTab += delegate(object a, EventArgs b)
			{
				CS$<>8__locals1.<>4__this._modName.Focused = true;
			};
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x00511BF0 File Offset: 0x0050FDF0
		public override void OnActivate()
		{
			base.OnActivate();
			this._modName.SetText("");
			this._basicSword.SetText("");
			this._modDisplayName.SetText("");
			this._modAuthor.SetText("");
			this._messagePanel.SetText("");
			this._modName.Focused = true;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x00511C60 File Offset: 0x0050FE60
		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (this.lastKnownMessage != this._messagePanel.Text)
			{
				this.lastKnownMessage = this._messagePanel.Text;
				if (string.IsNullOrEmpty(this._messagePanel.Text))
				{
					this._baseElement.RemoveChild(this._messagePanel);
					return;
				}
				this._baseElement.Append(this._messagePanel);
			}
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x00511CD2 File Offset: 0x0050FED2
		private void BackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this.HandleBackButtonUsage();
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x00511CDC File Offset: 0x0050FEDC
		public void HandleBackButtonUsage()
		{
			SoundEngine.PlaySound(SoundID.MenuClose, null, null);
			Main.menuMode = 10001;
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x00511D08 File Offset: 0x0050FF08
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x00511D18 File Offset: 0x0050FF18
		private void OKClick(UIMouseEvent evt, UIElement listeningElement)
		{
			try
			{
				string modNameTrimmed = this._modName.CurrentString.Trim();
				string basicSwordTrimmed = this._basicSword.CurrentString.Trim();
				string sourceFolder = Path.Combine(ModCompile.ModSourcePath, modNameTrimmed);
				CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
				if (Directory.Exists(sourceFolder))
				{
					this._messagePanel.SetText(Language.GetTextValue("tModLoader.CreateModFolderAlreadyExists"));
				}
				else if (!provider.IsValidIdentifier(modNameTrimmed))
				{
					this._messagePanel.SetText(Language.GetTextValue("tModLoader.CreateModNameInvalid"));
				}
				else if (modNameTrimmed.Equals("Mod", StringComparison.InvariantCultureIgnoreCase) || modNameTrimmed.Equals("ModLoader", StringComparison.InvariantCultureIgnoreCase) || modNameTrimmed.Equals("tModLoader", StringComparison.InvariantCultureIgnoreCase))
				{
					this._messagePanel.SetText(Language.GetTextValue("tModLoader.CreateModNameReserved"));
				}
				else if (!string.IsNullOrEmpty(basicSwordTrimmed) && !provider.IsValidIdentifier(basicSwordTrimmed))
				{
					this._messagePanel.SetText(Language.GetTextValue("tModLoader.CreateModBasicSwordInvalid"));
				}
				else if (string.IsNullOrWhiteSpace(this._modDisplayName.CurrentString))
				{
					this._messagePanel.SetText(Language.GetTextValue("tModLoader.CreateModDisplayNameEmpty"));
				}
				else if (string.IsNullOrWhiteSpace(this._modAuthor.CurrentString))
				{
					this._messagePanel.SetText(Language.GetTextValue("tModLoader.CreateModAuthorEmpty"));
				}
				else
				{
					Directory.CreateDirectory(sourceFolder);
					SourceManagement.WriteModTemplate(sourceFolder, this.GetModTemplateArguments());
					Utils.OpenFolder(sourceFolder);
					SoundEngine.PlaySound(SoundID.MenuOpen, null, null);
					Main.menuMode = 10001;
				}
			}
			catch (Exception e)
			{
				Logging.tML.Error(e);
				this._messagePanel.SetText("There was an issue. Check client.log");
			}
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x00511EDC File Offset: 0x005100DC
		private SourceManagement.TemplateParameters GetModTemplateArguments()
		{
			SourceManagement.TemplateParameters templateParameters = new SourceManagement.TemplateParameters();
			templateParameters.ModName = this._modName.CurrentString.Trim();
			templateParameters.ModDisplayName = this._modDisplayName.CurrentString;
			templateParameters.ModAuthor = this._modAuthor.CurrentString.Trim();
			templateParameters.ModVersion = "0.1";
			templateParameters.ItemName = this._basicSword.CurrentString.Trim();
			templateParameters.ItemDisplayName = Regex.Replace(templateParameters.ItemName, "([A-Z])", " $1").Trim();
			return templateParameters;
		}

		// Token: 0x04001A35 RID: 6709
		private UIElement _baseElement;

		// Token: 0x04001A36 RID: 6710
		private UITextPanel<string> _messagePanel;

		// Token: 0x04001A37 RID: 6711
		private UIFocusInputTextField _modName;

		// Token: 0x04001A38 RID: 6712
		private UIFocusInputTextField _modDisplayName;

		// Token: 0x04001A39 RID: 6713
		private UIFocusInputTextField _modAuthor;

		// Token: 0x04001A3A RID: 6714
		private UIFocusInputTextField _basicSword;

		// Token: 0x04001A3C RID: 6716
		private string lastKnownMessage = "";
	}
}
