using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200036E RID: 878
	public class UIWorkshopImportWorldListItem : AWorldListItem
	{
		// Token: 0x06002832 RID: 10290 RVA: 0x00588B48 File Offset: 0x00586D48
		public UIWorkshopImportWorldListItem(UIState ownerState, WorldFileData data, int orderInList)
		{
			this._ownerState = ownerState;
			this._orderInList = orderInList;
			this._data = data;
			this.LoadTextures();
			this.InitializeAppearance();
			this._worldIcon = base.GetIconElement();
			this._worldIcon.Left.Set(4f, 0f);
			this._worldIcon.OnLeftDoubleClick += this.ImportButtonClick_ImportWorldToLocalFiles;
			base.Append(this._worldIcon);
			float num = 4f;
			UIImageButton uiimageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay", 1));
			uiimageButton.VAlign = 1f;
			uiimageButton.Left.Set(num, 0f);
			uiimageButton.OnLeftClick += this.ImportButtonClick_ImportWorldToLocalFiles;
			base.OnLeftDoubleClick += this.ImportButtonClick_ImportWorldToLocalFiles;
			uiimageButton.OnMouseOver += this.PlayMouseOver;
			uiimageButton.OnMouseOut += this.ButtonMouseOut;
			base.Append(uiimageButton);
			num += 24f;
			this._buttonLabel = new UIText("", 1f, false);
			this._buttonLabel.VAlign = 1f;
			this._buttonLabel.Left.Set(num, 0f);
			this._buttonLabel.Top.Set(-3f, 0f);
			base.Append(this._buttonLabel);
			uiimageButton.SetSnapPoint("Import", orderInList, null, null);
		}

		// Token: 0x06002833 RID: 10291 RVA: 0x00588CD4 File Offset: 0x00586ED4
		private void LoadTextures()
		{
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", 1);
			this._workshopIconTexture = TextureAssets.Extra[243];
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x00588D13 File Offset: 0x00586F13
		private void InitializeAppearance()
		{
			this.Height.Set(96f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x00588D50 File Offset: 0x00586F50
		private void SetColorsToHovered()
		{
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x00588D7A File Offset: 0x00586F7A
		private void SetColorsToNotHovered()
		{
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x00588DB8 File Offset: 0x00586FB8
		private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Import"));
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x00588DCF File Offset: 0x00586FCF
		private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText("");
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x00588DE4 File Offset: 0x00586FE4
		private void ImportButtonClick_ImportWorldToLocalFiles(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement != evt.Target)
			{
				return;
			}
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Language.GetTextValue("Workshop.EnterNewNameForImportedWorld"), this._data.Name, new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoToMainMenu), 0, true);
			uivirtualKeyboard.SetMaxInputLength(27);
			uivirtualKeyboard.Text = this._data.Name;
			Main.MenuUI.SetState(uivirtualKeyboard);
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x00588E70 File Offset: 0x00587070
		private void OnFinishedSettingName(string name)
		{
			string newDisplayName = name.Trim();
			if (SocialAPI.Workshop != null)
			{
				SocialAPI.Workshop.ImportDownloadedWorldToLocalSaves(this._data, null, newDisplayName);
			}
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x0058257D File Offset: 0x0058077D
		private void GoToMainMenu()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = 0;
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x00588EA0 File Offset: 0x005870A0
		public override int CompareTo(object obj)
		{
			UIWorkshopImportWorldListItem uiworkshopImportWorldListItem = obj as UIWorkshopImportWorldListItem;
			if (uiworkshopImportWorldListItem != null)
			{
				return this._orderInList.CompareTo(uiworkshopImportWorldListItem._orderInList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x00588ED0 File Offset: 0x005870D0
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColorsToHovered();
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x00588EDF File Offset: 0x005870DF
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColorsToNotHovered();
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00588EF0 File Offset: 0x005870F0
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(this._innerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x00588FE0 File Offset: 0x005871E0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._worldIcon.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color color = this._data.IsValid ? Color.White : Color.Gray;
			string worldName = this._data.GetWorldName(true);
			if (worldName != null)
			{
				Utils.DrawBorderString(spriteBatch, worldName, new Vector2(num + 6f, dimensions.Y - 2f), color, 1f, 0f, 0f, -1);
			}
			spriteBatch.Draw(this._workshopIconTexture.Value, new Vector2(base.GetDimensions().X + base.GetDimensions().Width - (float)this._workshopIconTexture.Width() - 3f, base.GetDimensions().Y + 2f), new Rectangle?(this._workshopIconTexture.Frame(1, 1, 0, 0, 0, 0)), Color.White);
			spriteBatch.Draw(this._dividerTexture.Value, new Vector2(num, innerDimensions.Y + 21f), null, Color.White, 0f, Vector2.Zero, new Vector2((base.GetDimensions().X + base.GetDimensions().Width - num) / 8f, 1f), SpriteEffects.None, 0f);
			Vector2 vector = new Vector2(num + 6f, innerDimensions.Y + 29f);
			float num2 = 100f;
			this.DrawPanel(spriteBatch, vector, num2);
			string text = "";
			Color white = Color.White;
			base.GetDifficulty(out text, out white);
			float x = FontAssets.MouseText.Value.MeasureString(text).X;
			float x2 = num2 * 0.5f - x * 0.5f;
			Utils.DrawBorderString(spriteBatch, text, vector + new Vector2(x2, 3f), white, 1f, 0f, 0f, -1);
			vector.X += num2 + 5f;
			if (this._data._worldSizeName != null)
			{
				float num3 = 150f;
				if (!GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive)
				{
					num3 += 40f;
				}
				this.DrawPanel(spriteBatch, vector, num3);
				string textValue = Language.GetTextValue("UI.WorldSizeFormat", this._data.WorldSizeName);
				float x3 = FontAssets.MouseText.Value.MeasureString(textValue).X;
				float x4 = num3 * 0.5f - x3 * 0.5f;
				Utils.DrawBorderString(spriteBatch, textValue, vector + new Vector2(x4, 3f), Color.White, 1f, 0f, 0f, -1);
				vector.X += num3 + 5f;
			}
			float num4 = innerDimensions.X + innerDimensions.Width - vector.X;
			this.DrawPanel(spriteBatch, vector, num4);
			string arg;
			if (GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive)
			{
				arg = this._data.CreationTime.ToString("d MMMM yyyy");
			}
			else
			{
				arg = this._data.CreationTime.ToShortDateString();
			}
			string textValue2 = Language.GetTextValue("UI.WorldCreatedFormat", arg);
			float x5 = FontAssets.MouseText.Value.MeasureString(textValue2).X;
			float x6 = num4 * 0.5f - x5 * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue2, vector + new Vector2(x6, 3f), Color.White, 1f, 0f, 0f, -1);
			vector.X += num4 + 5f;
		}

		// Token: 0x04004B57 RID: 19287
		private Asset<Texture2D> _dividerTexture;

		// Token: 0x04004B58 RID: 19288
		private Asset<Texture2D> _workshopIconTexture;

		// Token: 0x04004B59 RID: 19289
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04004B5A RID: 19290
		private UIElement _worldIcon;

		// Token: 0x04004B5B RID: 19291
		private UIText _buttonLabel;

		// Token: 0x04004B5C RID: 19292
		private Asset<Texture2D> _buttonImportTexture;

		// Token: 0x04004B5D RID: 19293
		private int _orderInList;

		// Token: 0x04004B5E RID: 19294
		public UIState _ownerState;
	}
}
