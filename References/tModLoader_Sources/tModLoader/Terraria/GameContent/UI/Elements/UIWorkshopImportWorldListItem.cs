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
	// Token: 0x02000538 RID: 1336
	public class UIWorkshopImportWorldListItem : AWorldListItem
	{
		// Token: 0x06003F97 RID: 16279 RVA: 0x005DA094 File Offset: 0x005D8294
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
			UIImageButton uIImageButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay"));
			uIImageButton.VAlign = 1f;
			uIImageButton.Left.Set(num, 0f);
			uIImageButton.OnLeftClick += this.ImportButtonClick_ImportWorldToLocalFiles;
			base.OnLeftDoubleClick += this.ImportButtonClick_ImportWorldToLocalFiles;
			uIImageButton.OnMouseOver += this.PlayMouseOver;
			uIImageButton.OnMouseOut += this.ButtonMouseOut;
			base.Append(uIImageButton);
			num += 24f;
			this._buttonLabel = new UIText("", 1f, false);
			this._buttonLabel.VAlign = 1f;
			this._buttonLabel.Left.Set(num, 0f);
			this._buttonLabel.Top.Set(-3f, 0f);
			base.Append(this._buttonLabel);
			uIImageButton.SetSnapPoint("Import", orderInList, null, null);
		}

		// Token: 0x06003F98 RID: 16280 RVA: 0x005DA21F File Offset: 0x005D841F
		private void LoadTextures()
		{
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider");
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground");
			this._workshopIconTexture = TextureAssets.Extra[243];
		}

		// Token: 0x06003F99 RID: 16281 RVA: 0x005DA25C File Offset: 0x005D845C
		private void InitializeAppearance()
		{
			this.Height.Set(96f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06003F9A RID: 16282 RVA: 0x005DA299 File Offset: 0x005D8499
		private void SetColorsToHovered()
		{
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
		}

		// Token: 0x06003F9B RID: 16283 RVA: 0x005DA2C3 File Offset: 0x005D84C3
		private void SetColorsToNotHovered()
		{
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
		}

		// Token: 0x06003F9C RID: 16284 RVA: 0x005DA301 File Offset: 0x005D8501
		private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Import"));
		}

		// Token: 0x06003F9D RID: 16285 RVA: 0x005DA318 File Offset: 0x005D8518
		private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText("");
		}

		// Token: 0x06003F9E RID: 16286 RVA: 0x005DA32C File Offset: 0x005D852C
		private void ImportButtonClick_ImportWorldToLocalFiles(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement == evt.Target)
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.clrInput();
				UIVirtualKeyboard uIVirtualKeyboard = new UIVirtualKeyboard(Language.GetTextValue("Workshop.EnterNewNameForImportedWorld"), this._data.Name, new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoToMainMenu), 0, true);
				uIVirtualKeyboard.SetMaxInputLength(27);
				uIVirtualKeyboard.Text = this._data.Name;
				Main.MenuUI.SetState(uIVirtualKeyboard);
			}
		}

		// Token: 0x06003F9F RID: 16287 RVA: 0x005DA3B8 File Offset: 0x005D85B8
		private void OnFinishedSettingName(string name)
		{
			string newDisplayName = name.Trim();
			if (SocialAPI.Workshop != null)
			{
				SocialAPI.Workshop.ImportDownloadedWorldToLocalSaves(this._data, null, newDisplayName);
			}
		}

		// Token: 0x06003FA0 RID: 16288 RVA: 0x005DA3E5 File Offset: 0x005D85E5
		private void GoToMainMenu()
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = 0;
		}

		// Token: 0x06003FA1 RID: 16289 RVA: 0x005DA404 File Offset: 0x005D8604
		public override int CompareTo(object obj)
		{
			UIWorkshopImportWorldListItem uIWorkshopImportWorldListItem = obj as UIWorkshopImportWorldListItem;
			if (uIWorkshopImportWorldListItem != null)
			{
				return this._orderInList.CompareTo(uIWorkshopImportWorldListItem._orderInList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x06003FA2 RID: 16290 RVA: 0x005DA434 File Offset: 0x005D8634
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColorsToHovered();
		}

		// Token: 0x06003FA3 RID: 16291 RVA: 0x005DA443 File Offset: 0x005D8643
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06003FA4 RID: 16292 RVA: 0x005DA454 File Offset: 0x005D8654
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(this._innerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), 0, 0f);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
		}

		// Token: 0x06003FA5 RID: 16293 RVA: 0x005DA544 File Offset: 0x005D8744
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
			spriteBatch.Draw(this._dividerTexture.Value, new Vector2(num, innerDimensions.Y + 21f), null, Color.White, 0f, Vector2.Zero, new Vector2((base.GetDimensions().X + base.GetDimensions().Width - num) / 8f, 1f), 0, 0f);
			Vector2 vector;
			vector..ctor(num + 6f, innerDimensions.Y + 29f);
			float num2 = 100f;
			this.DrawPanel(spriteBatch, vector, num2);
			string expertText = "";
			Color gameModeColor = Color.White;
			base.GetDifficulty(out expertText, out gameModeColor);
			float x = FontAssets.MouseText.Value.MeasureString(expertText).X;
			float x2 = num2 * 0.5f - x * 0.5f;
			Utils.DrawBorderString(spriteBatch, expertText, vector + new Vector2(x2, 3f), gameModeColor, 1f, 0f, 0f, -1);
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
			string arg = (!GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive) ? this._data.CreationTime.ToShortDateString() : this._data.CreationTime.ToString("d MMMM yyyy");
			string textValue2 = Language.GetTextValue("UI.WorldCreatedFormat", arg);
			float x5 = FontAssets.MouseText.Value.MeasureString(textValue2).X;
			float x6 = num4 * 0.5f - x5 * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue2, vector + new Vector2(x6, 3f), Color.White, 1f, 0f, 0f, -1);
			vector.X += num4 + 5f;
		}

		// Token: 0x040057F2 RID: 22514
		private Asset<Texture2D> _dividerTexture;

		// Token: 0x040057F3 RID: 22515
		private Asset<Texture2D> _workshopIconTexture;

		// Token: 0x040057F4 RID: 22516
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x040057F5 RID: 22517
		private UIElement _worldIcon;

		// Token: 0x040057F6 RID: 22518
		private UIText _buttonLabel;

		// Token: 0x040057F7 RID: 22519
		private Asset<Texture2D> _buttonImportTexture;

		// Token: 0x040057F8 RID: 22520
		private int _orderInList;

		// Token: 0x040057F9 RID: 22521
		public UIState _ownerState;
	}
}
