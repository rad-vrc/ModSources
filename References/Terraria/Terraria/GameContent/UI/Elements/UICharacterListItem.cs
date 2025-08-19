using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000389 RID: 905
	public class UICharacterListItem : UIPanel
	{
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060028E2 RID: 10466 RVA: 0x0058F548 File Offset: 0x0058D748
		public bool IsFavorite
		{
			get
			{
				return this._data.IsFavorite;
			}
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x0058F558 File Offset: 0x0058D758
		public UICharacterListItem(PlayerFileData data, int snapPointIndex)
		{
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", 1);
			this._buttonCloudActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudActive", 1);
			this._buttonCloudInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudInactive", 1);
			this._buttonFavoriteActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteActive", 1);
			this._buttonFavoriteInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteInactive", 1);
			this._buttonPlayTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay", 1);
			this._buttonRenameTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonRename", 1);
			this._buttonDeleteTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete", 1);
			this.Height.Set(96f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this._data = data;
			this._playerPanel = new UICharacter(data.Player, false, true, 1f, true);
			this._playerPanel.Left.Set(4f, 0f);
			this._playerPanel.OnLeftDoubleClick += this.PlayGame;
			base.OnLeftDoubleClick += this.PlayGame;
			base.Append(this._playerPanel);
			float num = 4f;
			UIImageButton uiimageButton = new UIImageButton(this._buttonPlayTexture);
			uiimageButton.VAlign = 1f;
			uiimageButton.Left.Set(num, 0f);
			uiimageButton.OnLeftClick += this.PlayGame;
			uiimageButton.OnMouseOver += this.PlayMouseOver;
			uiimageButton.OnMouseOut += this.ButtonMouseOut;
			base.Append(uiimageButton);
			num += 24f;
			UIImageButton uiimageButton2 = new UIImageButton(this._data.IsFavorite ? this._buttonFavoriteActiveTexture : this._buttonFavoriteInactiveTexture);
			uiimageButton2.VAlign = 1f;
			uiimageButton2.Left.Set(num, 0f);
			uiimageButton2.OnLeftClick += this.FavoriteButtonClick;
			uiimageButton2.OnMouseOver += this.FavoriteMouseOver;
			uiimageButton2.OnMouseOut += this.ButtonMouseOut;
			uiimageButton2.SetVisibility(1f, this._data.IsFavorite ? 0.8f : 0.4f);
			base.Append(uiimageButton2);
			num += 24f;
			if (SocialAPI.Cloud != null)
			{
				UIImageButton uiimageButton3 = new UIImageButton(this._data.IsCloudSave ? this._buttonCloudActiveTexture : this._buttonCloudInactiveTexture);
				uiimageButton3.VAlign = 1f;
				uiimageButton3.Left.Set(num, 0f);
				uiimageButton3.OnLeftClick += this.CloudButtonClick;
				uiimageButton3.OnMouseOver += this.CloudMouseOver;
				uiimageButton3.OnMouseOut += this.ButtonMouseOut;
				base.Append(uiimageButton3);
				uiimageButton3.SetSnapPoint("Cloud", snapPointIndex, null, null);
				num += 24f;
			}
			UIImageButton uiimageButton4 = new UIImageButton(this._buttonRenameTexture);
			uiimageButton4.VAlign = 1f;
			uiimageButton4.Left.Set(num, 0f);
			uiimageButton4.OnLeftClick += this.RenameButtonClick;
			uiimageButton4.OnMouseOver += this.RenameMouseOver;
			uiimageButton4.OnMouseOut += this.ButtonMouseOut;
			base.Append(uiimageButton4);
			num += 24f;
			UIImageButton uiimageButton5 = new UIImageButton(this._buttonDeleteTexture);
			uiimageButton5.VAlign = 1f;
			uiimageButton5.HAlign = 1f;
			if (!this._data.IsFavorite)
			{
				uiimageButton5.OnLeftClick += this.DeleteButtonClick;
			}
			uiimageButton5.OnMouseOver += this.DeleteMouseOver;
			uiimageButton5.OnMouseOut += this.DeleteMouseOut;
			this._deleteButton = uiimageButton5;
			base.Append(uiimageButton5);
			num += 4f;
			this._buttonLabel = new UIText("", 1f, false);
			this._buttonLabel.VAlign = 1f;
			this._buttonLabel.Left.Set(num, 0f);
			this._buttonLabel.Top.Set(-3f, 0f);
			base.Append(this._buttonLabel);
			this._deleteButtonLabel = new UIText("", 1f, false);
			this._deleteButtonLabel.VAlign = 1f;
			this._deleteButtonLabel.HAlign = 1f;
			this._deleteButtonLabel.Left.Set(-30f, 0f);
			this._deleteButtonLabel.Top.Set(-3f, 0f);
			base.Append(this._deleteButtonLabel);
			uiimageButton.SetSnapPoint("Play", snapPointIndex, null, null);
			uiimageButton2.SetSnapPoint("Favorite", snapPointIndex, null, null);
			uiimageButton4.SetSnapPoint("Rename", snapPointIndex, null, null);
			uiimageButton5.SetSnapPoint("Delete", snapPointIndex, null, null);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x0058FB09 File Offset: 0x0058DD09
		private void RenameMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Rename"));
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x0058FB20 File Offset: 0x0058DD20
		private void FavoriteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsFavorite)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.Unfavorite"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.Favorite"));
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x0058FB5A File Offset: 0x0058DD5A
		private void CloudMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsCloudSave)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.MoveOffCloud"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.MoveToCloud"));
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0058FB94 File Offset: 0x0058DD94
		private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Play"));
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0058FBAB File Offset: 0x0058DDAB
		private void DeleteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsFavorite)
			{
				this._deleteButtonLabel.SetText(Language.GetTextValue("UI.CannotDeleteFavorited"));
				return;
			}
			this._deleteButtonLabel.SetText(Language.GetTextValue("UI.Delete"));
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x0058FBE5 File Offset: 0x0058DDE5
		private void DeleteMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._deleteButtonLabel.SetText("");
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0058FBF7 File Offset: 0x0058DDF7
		private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText("");
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x0058FC0C File Offset: 0x0058DE0C
		private void RenameButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Lang.menu[45].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 0, true);
			uivirtualKeyboard.SetMaxInputLength(20);
			Main.MenuUI.SetState(uivirtualKeyboard);
			UIList uilist = base.Parent.Parent as UIList;
			if (uilist != null)
			{
				uilist.UpdateOrder();
			}
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x0058FC94 File Offset: 0x0058DE94
		private void OnFinishedSettingName(string name)
		{
			string newName = name.Trim();
			Main.menuMode = 10;
			this._data.Rename(newName);
			Main.OpenCharacterSelectUI();
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x0058FCC0 File Offset: 0x0058DEC0
		private void GoBackHere()
		{
			Main.OpenCharacterSelectUI();
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x0058FCC8 File Offset: 0x0058DEC8
		private void CloudButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsCloudSave)
			{
				this._data.MoveToLocal();
			}
			else
			{
				this._data.MoveToCloud();
			}
			((UIImageButton)evt.Target).SetImage(this._data.IsCloudSave ? this._buttonCloudActiveTexture : this._buttonCloudInactiveTexture);
			if (this._data.IsCloudSave)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.MoveOffCloud"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.MoveToCloud"));
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x0058FD60 File Offset: 0x0058DF60
		private void DeleteButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			for (int i = 0; i < Main.PlayerList.Count; i++)
			{
				if (Main.PlayerList[i] == this._data)
				{
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
					Main.selectedPlayer = i;
					Main.menuMode = 5;
					return;
				}
			}
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x0058FDB7 File Offset: 0x0058DFB7
		private void PlayGame(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement != evt.Target)
			{
				return;
			}
			if (this._data.Player.loadStatus == 0)
			{
				Main.SelectPlayer(this._data);
			}
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x0058FDE0 File Offset: 0x0058DFE0
		private void FavoriteButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			this._data.ToggleFavorite();
			((UIImageButton)evt.Target).SetImage(this._data.IsFavorite ? this._buttonFavoriteActiveTexture : this._buttonFavoriteInactiveTexture);
			((UIImageButton)evt.Target).SetVisibility(1f, this._data.IsFavorite ? 0.8f : 0.4f);
			if (this._data.IsFavorite)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.Unfavorite"));
				this._deleteButton.OnLeftClick -= this.DeleteButtonClick;
			}
			else
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.Favorite"));
				this._deleteButton.OnLeftClick += this.DeleteButtonClick;
			}
			UIList uilist = base.Parent.Parent as UIList;
			if (uilist != null)
			{
				uilist.UpdateOrder();
			}
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x0058FED4 File Offset: 0x0058E0D4
		public override int CompareTo(object obj)
		{
			UICharacterListItem uicharacterListItem = obj as UICharacterListItem;
			if (uicharacterListItem == null)
			{
				return base.CompareTo(obj);
			}
			if (this.IsFavorite && !uicharacterListItem.IsFavorite)
			{
				return -1;
			}
			if (!this.IsFavorite && uicharacterListItem.IsFavorite)
			{
				return 1;
			}
			if (this._data.Name.CompareTo(uicharacterListItem._data.Name) != 0)
			{
				return this._data.Name.CompareTo(uicharacterListItem._data.Name);
			}
			return this._data.GetFileName(true).CompareTo(uicharacterListItem._data.GetFileName(true));
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x0058FF6D File Offset: 0x0058E16D
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
			this._playerPanel.SetAnimated(true);
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x0058FFAC File Offset: 0x0058E1AC
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			this._playerPanel.SetAnimated(false);
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x00590008 File Offset: 0x0058E208
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(this._innerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x005900F8 File Offset: 0x0058E2F8
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._playerPanel.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color color = Color.White;
			string text = this._data.Name;
			if (this._data.Player.loadStatus != 0)
			{
				color = Color.Gray;
				string name = StatusID.Search.GetName(this._data.Player.loadStatus);
				text = "(" + name + ") " + text;
			}
			Utils.DrawBorderString(spriteBatch, text, new Vector2(num + 6f, dimensions.Y - 2f), color, 1f, 0f, 0f, -1);
			spriteBatch.Draw(this._dividerTexture.Value, new Vector2(num, innerDimensions.Y + 21f), null, Color.White, 0f, Vector2.Zero, new Vector2((base.GetDimensions().X + base.GetDimensions().Width - num) / 8f, 1f), SpriteEffects.None, 0f);
			Vector2 vector = new Vector2(num + 6f, innerDimensions.Y + 29f);
			float num2 = 200f;
			Vector2 vector2 = vector;
			this.DrawPanel(spriteBatch, vector2, num2);
			spriteBatch.Draw(TextureAssets.Heart.Value, vector2 + new Vector2(5f, 2f), Color.White);
			vector2.X += 10f + (float)TextureAssets.Heart.Width();
			Utils.DrawBorderString(spriteBatch, this._data.Player.statLifeMax + Language.GetTextValue("GameUI.PlayerLifeMax"), vector2 + new Vector2(0f, 3f), Color.White, 1f, 0f, 0f, -1);
			vector2.X += 65f;
			spriteBatch.Draw(TextureAssets.Mana.Value, vector2 + new Vector2(5f, 2f), Color.White);
			vector2.X += 10f + (float)TextureAssets.Mana.Width();
			Utils.DrawBorderString(spriteBatch, this._data.Player.statManaMax + Language.GetTextValue("GameUI.PlayerManaMax"), vector2 + new Vector2(0f, 3f), Color.White, 1f, 0f, 0f, -1);
			vector.X += num2 + 5f;
			Vector2 vector3 = vector;
			float num3 = 140f;
			if (GameCulture.FromCultureName(GameCulture.CultureName.Russian).IsActive)
			{
				num3 = 180f;
			}
			this.DrawPanel(spriteBatch, vector3, num3);
			string text2 = "";
			Color color2 = Color.White;
			switch (this._data.Player.difficulty)
			{
			case 0:
				text2 = Language.GetTextValue("UI.Softcore");
				break;
			case 1:
				text2 = Language.GetTextValue("UI.Mediumcore");
				color2 = Main.mcColor;
				break;
			case 2:
				text2 = Language.GetTextValue("UI.Hardcore");
				color2 = Main.hcColor;
				break;
			case 3:
				text2 = Language.GetTextValue("UI.Creative");
				color2 = Main.creativeModeColor;
				break;
			}
			vector3 += new Vector2(num3 * 0.5f - FontAssets.MouseText.Value.MeasureString(text2).X * 0.5f, 3f);
			Utils.DrawBorderString(spriteBatch, text2, vector3, color2, 1f, 0f, 0f, -1);
			vector.X += num3 + 5f;
			Vector2 vector4 = vector;
			float num4 = innerDimensions.X + innerDimensions.Width - vector4.X;
			this.DrawPanel(spriteBatch, vector4, num4);
			TimeSpan playTime = this._data.GetPlayTime();
			int num5 = playTime.Days * 24 + playTime.Hours;
			string text3 = ((num5 < 10) ? "0" : "") + num5 + playTime.ToString("\\:mm\\:ss");
			vector4 += new Vector2(num4 * 0.5f - FontAssets.MouseText.Value.MeasureString(text3).X * 0.5f, 3f);
			Utils.DrawBorderString(spriteBatch, text3, vector4, Color.White, 1f, 0f, 0f, -1);
		}

		// Token: 0x04004C29 RID: 19497
		private PlayerFileData _data;

		// Token: 0x04004C2A RID: 19498
		private Asset<Texture2D> _dividerTexture;

		// Token: 0x04004C2B RID: 19499
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04004C2C RID: 19500
		private UICharacter _playerPanel;

		// Token: 0x04004C2D RID: 19501
		private UIText _buttonLabel;

		// Token: 0x04004C2E RID: 19502
		private UIText _deleteButtonLabel;

		// Token: 0x04004C2F RID: 19503
		private Asset<Texture2D> _buttonCloudActiveTexture;

		// Token: 0x04004C30 RID: 19504
		private Asset<Texture2D> _buttonCloudInactiveTexture;

		// Token: 0x04004C31 RID: 19505
		private Asset<Texture2D> _buttonFavoriteActiveTexture;

		// Token: 0x04004C32 RID: 19506
		private Asset<Texture2D> _buttonFavoriteInactiveTexture;

		// Token: 0x04004C33 RID: 19507
		private Asset<Texture2D> _buttonPlayTexture;

		// Token: 0x04004C34 RID: 19508
		private Asset<Texture2D> _buttonRenameTexture;

		// Token: 0x04004C35 RID: 19509
		private Asset<Texture2D> _buttonDeleteTexture;

		// Token: 0x04004C36 RID: 19510
		private UIImageButton _deleteButton;
	}
}
