using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.Social;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x02000395 RID: 917
	public class UIWorldListItem : AWorldListItem
	{
		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x00592425 File Offset: 0x00590625
		public bool IsFavorite
		{
			get
			{
				return this._data.IsFavorite;
			}
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x00592434 File Offset: 0x00590634
		public UIWorldListItem(WorldFileData data, int orderInList, bool canBePlayed)
		{
			this._orderInList = orderInList;
			this._data = data;
			this._canBePlayed = canBePlayed;
			this.LoadTextures();
			this.InitializeAppearance();
			this._worldIcon = base.GetIconElement();
			this._worldIcon.OnLeftDoubleClick += this.PlayGame;
			base.Append(this._worldIcon);
			if (this._data.DefeatedMoonlord)
			{
				UIImage element = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/IconCompletion", 1))
				{
					HAlign = 0.5f,
					VAlign = 0.5f,
					Top = new StyleDimension(-10f, 0f),
					Left = new StyleDimension(-3f, 0f),
					IgnoresMouseInteraction = true
				};
				this._worldIcon.Append(element);
			}
			float num = 4f;
			UIImageButton uiimageButton = new UIImageButton(this._buttonPlayTexture);
			uiimageButton.VAlign = 1f;
			uiimageButton.Left.Set(num, 0f);
			uiimageButton.OnLeftClick += this.PlayGame;
			base.OnLeftDoubleClick += this.PlayGame;
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
				uiimageButton3.SetSnapPoint("Cloud", orderInList, null, null);
				base.Append(uiimageButton3);
				num += 24f;
			}
			if (this._data.WorldGeneratorVersion != 0UL)
			{
				UIImageButton uiimageButton4 = new UIImageButton(this._buttonSeedTexture);
				uiimageButton4.VAlign = 1f;
				uiimageButton4.Left.Set(num, 0f);
				uiimageButton4.OnLeftClick += this.SeedButtonClick;
				uiimageButton4.OnMouseOver += this.SeedMouseOver;
				uiimageButton4.OnMouseOut += this.ButtonMouseOut;
				uiimageButton4.SetSnapPoint("Seed", orderInList, null, null);
				base.Append(uiimageButton4);
				num += 24f;
			}
			UIImageButton uiimageButton5 = new UIImageButton(this._buttonRenameTexture);
			uiimageButton5.VAlign = 1f;
			uiimageButton5.Left.Set(num, 0f);
			uiimageButton5.OnLeftClick += this.RenameButtonClick;
			uiimageButton5.OnMouseOver += this.RenameMouseOver;
			uiimageButton5.OnMouseOut += this.ButtonMouseOut;
			uiimageButton5.SetSnapPoint("Rename", orderInList, null, null);
			base.Append(uiimageButton5);
			num += 24f;
			UIImageButton uiimageButton6 = new UIImageButton(this._buttonDeleteTexture);
			uiimageButton6.VAlign = 1f;
			uiimageButton6.HAlign = 1f;
			if (!this._data.IsFavorite)
			{
				uiimageButton6.OnLeftClick += this.DeleteButtonClick;
			}
			uiimageButton6.OnMouseOver += this.DeleteMouseOver;
			uiimageButton6.OnMouseOut += this.DeleteMouseOut;
			this._deleteButton = uiimageButton6;
			base.Append(uiimageButton6);
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
			uiimageButton.SetSnapPoint("Play", orderInList, null, null);
			uiimageButton2.SetSnapPoint("Favorite", orderInList, null, null);
			uiimageButton5.SetSnapPoint("Rename", orderInList, null, null);
			uiimageButton6.SetSnapPoint("Delete", orderInList, null, null);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x005929FC File Offset: 0x00590BFC
		private void LoadTextures()
		{
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider", 1);
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground", 1);
			this._buttonCloudActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudActive", 1);
			this._buttonCloudInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudInactive", 1);
			this._buttonFavoriteActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteActive", 1);
			this._buttonFavoriteInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteInactive", 1);
			this._buttonPlayTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay", 1);
			this._buttonSeedTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonSeed", 1);
			this._buttonRenameTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonRename", 1);
			this._buttonDeleteTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete", 1);
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x00592AE5 File Offset: 0x00590CE5
		private void InitializeAppearance()
		{
			this.Height.Set(96f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x00592B24 File Offset: 0x00590D24
		private void SetColorsToHovered()
		{
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
			if (!this._canBePlayed)
			{
				this.BorderColor = new Color(150, 150, 150) * 1f;
				this.BackgroundColor = Color.Lerp(this.BackgroundColor, new Color(120, 120, 120), 0.5f) * 1f;
			}
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x00592BB0 File Offset: 0x00590DB0
		private void SetColorsToNotHovered()
		{
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			if (!this._canBePlayed)
			{
				this.BorderColor = new Color(127, 127, 127) * 0.7f;
				this.BackgroundColor = Color.Lerp(new Color(63, 82, 151), new Color(80, 80, 80), 0.5f) * 0.7f;
			}
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x00592C4F File Offset: 0x00590E4F
		private void RenameMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Rename"));
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x00592C66 File Offset: 0x00590E66
		private void FavoriteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsFavorite)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.Unfavorite"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.Favorite"));
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x00592CA0 File Offset: 0x00590EA0
		private void CloudMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsCloudSave)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.MoveOffCloud"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.MoveToCloud"));
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x00592CDA File Offset: 0x00590EDA
		private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Play"));
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x00592CF1 File Offset: 0x00590EF1
		private void SeedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.CopySeed", this._data.GetFullSeedText(true)));
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x00592D14 File Offset: 0x00590F14
		private void DeleteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsFavorite)
			{
				this._deleteButtonLabel.SetText(Language.GetTextValue("UI.CannotDeleteFavorited"));
				return;
			}
			this._deleteButtonLabel.SetText(Language.GetTextValue("UI.Delete"));
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x00592D4E File Offset: 0x00590F4E
		private void DeleteMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._deleteButtonLabel.SetText("");
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x00592D60 File Offset: 0x00590F60
		private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText("");
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x00592D74 File Offset: 0x00590F74
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

		// Token: 0x06002971 RID: 10609 RVA: 0x00592E0C File Offset: 0x0059100C
		private void DeleteButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			for (int i = 0; i < Main.WorldList.Count; i++)
			{
				if (Main.WorldList[i] == this._data)
				{
					SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
					Main.selectedWorld = i;
					Main.menuMode = 9;
					return;
				}
			}
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x00592E64 File Offset: 0x00591064
		private void PlayGame(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement != evt.Target)
			{
				return;
			}
			if (this.TryMovingToRejectionMenuIfNeeded(this._data.GameMode))
			{
				return;
			}
			this._data.SetAsActive();
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			Main.GetInputText("", false);
			if (Main.menuMultiplayer && SocialAPI.Network != null)
			{
				Main.menuMode = 889;
			}
			else if (Main.menuMultiplayer)
			{
				Main.menuMode = 30;
			}
			else
			{
				Main.menuMode = 10;
			}
			if (!Main.menuMultiplayer)
			{
				WorldGen.playWorld();
			}
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x00592F00 File Offset: 0x00591100
		private bool TryMovingToRejectionMenuIfNeeded(int worldGameMode)
		{
			GameModeData gameModeData;
			if (!Main.RegisteredGameModes.TryGetValue(worldGameMode, out gameModeData))
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.statusText = Language.GetTextValue("UI.WorldCannotBeLoadedBecauseItHasAnInvalidGameMode");
				Main.menuMode = 1000000;
				return true;
			}
			bool flag = Main.ActivePlayerFileData.Player.difficulty == 3;
			bool isJourneyMode = gameModeData.IsJourneyMode;
			if (flag && !isJourneyMode)
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.statusText = Language.GetTextValue("UI.PlayerIsCreativeAndWorldIsNotCreative");
				Main.menuMode = 1000000;
				return true;
			}
			if (!flag && isJourneyMode)
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.statusText = Language.GetTextValue("UI.PlayerIsNotCreativeAndWorldIsCreative");
				Main.menuMode = 1000000;
				return true;
			}
			return false;
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x00592FD8 File Offset: 0x005911D8
		private void RenameButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uivirtualKeyboard = new UIVirtualKeyboard(Lang.menu[48].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 0, true);
			uivirtualKeyboard.SetMaxInputLength(27);
			Main.MenuUI.SetState(uivirtualKeyboard);
			UIList uilist = base.Parent.Parent as UIList;
			if (uilist != null)
			{
				uilist.UpdateOrder();
			}
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x00593060 File Offset: 0x00591260
		private void OnFinishedSettingName(string name)
		{
			string newDisplayName = name.Trim();
			Main.menuMode = 10;
			this._data.Rename(newDisplayName);
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x00593087 File Offset: 0x00591287
		private void GoBackHere()
		{
			Main.GoToWorldSelect();
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x00593090 File Offset: 0x00591290
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

		// Token: 0x06002978 RID: 10616 RVA: 0x00593182 File Offset: 0x00591382
		private void SeedButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Platform.Get<IClipboard>().Value = this._data.GetFullSeedText(false);
			this._buttonLabel.SetText(Language.GetTextValue("UI.SeedCopied"));
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x005931B0 File Offset: 0x005913B0
		public override int CompareTo(object obj)
		{
			UIWorldListItem uiworldListItem = obj as UIWorldListItem;
			if (uiworldListItem != null)
			{
				return this._orderInList.CompareTo(uiworldListItem._orderInList);
			}
			return base.CompareTo(obj);
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x005931E0 File Offset: 0x005913E0
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColorsToHovered();
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x005931EF File Offset: 0x005913EF
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColorsToNotHovered();
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x00593200 File Offset: 0x00591400
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(this._innerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), SpriteEffects.None, 0f);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x005932F0 File Offset: 0x005914F0
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._worldIcon.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color color = this._data.IsValid ? Color.White : Color.Gray;
			string worldName = this._data.GetWorldName(true);
			Utils.DrawBorderString(spriteBatch, worldName, new Vector2(num + 6f, dimensions.Y - 2f), color, 1f, 0f, 0f, -1);
			spriteBatch.Draw(this._dividerTexture.Value, new Vector2(num, innerDimensions.Y + 21f), null, Color.White, 0f, Vector2.Zero, new Vector2((base.GetDimensions().X + base.GetDimensions().Width - num) / 8f, 1f), SpriteEffects.None, 0f);
			Vector2 vector = new Vector2(num + 6f, innerDimensions.Y + 29f);
			float num2 = 100f;
			this.DrawPanel(spriteBatch, vector, num2);
			string text;
			Color color2;
			base.GetDifficulty(out text, out color2);
			float x = FontAssets.MouseText.Value.MeasureString(text).X;
			float x2 = num2 * 0.5f - x * 0.5f;
			Utils.DrawBorderString(spriteBatch, text, vector + new Vector2(x2, 3f), color2, 1f, 0f, 0f, -1);
			vector.X += num2 + 5f;
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

		// Token: 0x04004C7D RID: 19581
		private Asset<Texture2D> _dividerTexture;

		// Token: 0x04004C7E RID: 19582
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04004C7F RID: 19583
		private UIElement _worldIcon;

		// Token: 0x04004C80 RID: 19584
		private UIText _buttonLabel;

		// Token: 0x04004C81 RID: 19585
		private UIText _deleteButtonLabel;

		// Token: 0x04004C82 RID: 19586
		private Asset<Texture2D> _buttonCloudActiveTexture;

		// Token: 0x04004C83 RID: 19587
		private Asset<Texture2D> _buttonCloudInactiveTexture;

		// Token: 0x04004C84 RID: 19588
		private Asset<Texture2D> _buttonFavoriteActiveTexture;

		// Token: 0x04004C85 RID: 19589
		private Asset<Texture2D> _buttonFavoriteInactiveTexture;

		// Token: 0x04004C86 RID: 19590
		private Asset<Texture2D> _buttonPlayTexture;

		// Token: 0x04004C87 RID: 19591
		private Asset<Texture2D> _buttonSeedTexture;

		// Token: 0x04004C88 RID: 19592
		private Asset<Texture2D> _buttonRenameTexture;

		// Token: 0x04004C89 RID: 19593
		private Asset<Texture2D> _buttonDeleteTexture;

		// Token: 0x04004C8A RID: 19594
		private UIImageButton _deleteButton;

		// Token: 0x04004C8B RID: 19595
		private int _orderInList;

		// Token: 0x04004C8C RID: 19596
		private bool _canBePlayed;
	}
}
