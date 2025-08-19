using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Engine;
using Terraria.ModLoader.UI;
using Terraria.Social;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.GameContent.UI.Elements
{
	// Token: 0x0200050C RID: 1292
	public class UICharacterListItem : UIPanel
	{
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06003E26 RID: 15910 RVA: 0x005CFBF0 File Offset: 0x005CDDF0
		public PlayerFileData Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06003E27 RID: 15911 RVA: 0x005CFBF8 File Offset: 0x005CDDF8
		public bool IsFavorite
		{
			get
			{
				return this._data.IsFavorite;
			}
		}

		// Token: 0x06003E28 RID: 15912 RVA: 0x005CFC08 File Offset: 0x005CDE08
		public UICharacterListItem(PlayerFileData data, int snapPointIndex)
		{
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider");
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground");
			this._buttonCloudActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudActive");
			this._buttonCloudInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudInactive");
			this._buttonFavoriteActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteActive");
			this._buttonFavoriteInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteInactive");
			this._buttonPlayTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay");
			this._buttonRenameTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonRename");
			this._buttonDeleteTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete");
			this.InitializeTmlFields(data);
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
			UIImageButton uIImageButton = new UIImageButton(this._buttonPlayTexture);
			uIImageButton.VAlign = 1f;
			uIImageButton.Left.Set(num, 0f);
			uIImageButton.OnLeftClick += this.PlayGame;
			uIImageButton.OnMouseOver += this.PlayMouseOver;
			uIImageButton.OnMouseOut += this.ButtonMouseOut;
			base.Append(uIImageButton);
			num += 24f;
			UIImageButton uIImageButton2 = new UIImageButton(this._data.IsFavorite ? this._buttonFavoriteActiveTexture : this._buttonFavoriteInactiveTexture);
			uIImageButton2.VAlign = 1f;
			uIImageButton2.Left.Set(num, 0f);
			uIImageButton2.OnLeftClick += this.FavoriteButtonClick;
			uIImageButton2.OnMouseOver += this.FavoriteMouseOver;
			uIImageButton2.OnMouseOut += this.ButtonMouseOut;
			uIImageButton2.SetVisibility(1f, this._data.IsFavorite ? 0.8f : 0.4f);
			base.Append(uIImageButton2);
			num += 24f;
			if (SocialAPI.Cloud != null)
			{
				UIImageButton uIImageButton3 = new UIImageButton(this._data.IsCloudSave ? this._buttonCloudActiveTexture : this._buttonCloudInactiveTexture);
				uIImageButton3.VAlign = 1f;
				uIImageButton3.Left.Set(num, 0f);
				uIImageButton3.OnLeftClick += this.CloudButtonClick;
				uIImageButton3.OnMouseOver += this.CloudMouseOver;
				uIImageButton3.OnMouseOut += this.ButtonMouseOut;
				base.Append(uIImageButton3);
				uIImageButton3.SetSnapPoint("Cloud", snapPointIndex, null, null);
				num += 24f;
			}
			UIImageButton uIImageButton4 = new UIImageButton(this._buttonRenameTexture);
			uIImageButton4.VAlign = 1f;
			uIImageButton4.Left.Set(num, 0f);
			uIImageButton4.OnLeftClick += this.RenameButtonClick;
			uIImageButton4.OnMouseOver += this.RenameMouseOver;
			uIImageButton4.OnMouseOut += this.ButtonMouseOut;
			base.Append(uIImageButton4);
			num += 24f;
			UIImageButton uIImageButton5 = new UIImageButton(this._buttonDeleteTexture)
			{
				VAlign = 1f,
				HAlign = 1f
			};
			if (!this._data.IsFavorite)
			{
				uIImageButton5.OnLeftClick += this.DeleteButtonClick;
			}
			uIImageButton5.OnMouseOver += this.DeleteMouseOver;
			uIImageButton5.OnMouseOut += this.DeleteMouseOut;
			this._deleteButton = uIImageButton5;
			base.Append(uIImageButton5);
			num += 4f;
			this.AddTmlElements(data);
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
			uIImageButton.SetSnapPoint("Play", snapPointIndex, null, null);
			uIImageButton2.SetSnapPoint("Favorite", snapPointIndex, null, null);
			uIImageButton4.SetSnapPoint("Rename", snapPointIndex, null, null);
			uIImageButton5.SetSnapPoint("Delete", snapPointIndex, null, null);
		}

		// Token: 0x06003E29 RID: 15913 RVA: 0x005D01BC File Offset: 0x005CE3BC
		private void RenameMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Rename"));
		}

		// Token: 0x06003E2A RID: 15914 RVA: 0x005D01D3 File Offset: 0x005CE3D3
		private void FavoriteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsFavorite)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.Unfavorite"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.Favorite"));
		}

		// Token: 0x06003E2B RID: 15915 RVA: 0x005D0210 File Offset: 0x005CE410
		private void CloudMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsCloudSave)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.MoveOffCloud"));
				return;
			}
			if (!Steam.CheckSteamCloudStorageSufficient(this._fileSize))
			{
				this._buttonLabel.SetText(Language.GetTextValue("tModLoader.CloudWarning"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.MoveToCloud"));
		}

		// Token: 0x06003E2C RID: 15916 RVA: 0x005D0278 File Offset: 0x005CE478
		private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Play"));
		}

		// Token: 0x06003E2D RID: 15917 RVA: 0x005D028F File Offset: 0x005CE48F
		private void DeleteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsFavorite)
			{
				this._deleteButtonLabel.SetText(Language.GetTextValue("UI.CannotDeleteFavorited"));
				return;
			}
			this._deleteButtonLabel.SetText(Language.GetTextValue("UI.Delete"));
		}

		// Token: 0x06003E2E RID: 15918 RVA: 0x005D02C9 File Offset: 0x005CE4C9
		private void DeleteMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._deleteButtonLabel.SetText("");
		}

		// Token: 0x06003E2F RID: 15919 RVA: 0x005D02DB File Offset: 0x005CE4DB
		private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText("");
		}

		// Token: 0x06003E30 RID: 15920 RVA: 0x005D02F0 File Offset: 0x005CE4F0
		private void RenameButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uIVirtualKeyboard = new UIVirtualKeyboard(Lang.menu[45].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 0, true);
			uIVirtualKeyboard.SetMaxInputLength(20);
			Main.MenuUI.SetState(uIVirtualKeyboard);
			UIList uIList = base.Parent.Parent as UIList;
			if (uIList != null)
			{
				uIList.UpdateOrder();
			}
		}

		// Token: 0x06003E31 RID: 15921 RVA: 0x005D0378 File Offset: 0x005CE578
		private void OnFinishedSettingName(string name)
		{
			string newName = name.Trim();
			Main.menuMode = 10;
			this._data.Rename(newName);
			Main.OpenCharacterSelectUI();
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x005D03A4 File Offset: 0x005CE5A4
		private void GoBackHere()
		{
			Main.OpenCharacterSelectUI();
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x005D03AC File Offset: 0x005CE5AC
		private void CloudButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsCloudSave)
			{
				this._data.MoveToLocal();
			}
			else
			{
				Steam.RecalculateAvailableSteamCloudStorage();
				if (!Steam.CheckSteamCloudStorageSufficient(this._fileSize))
				{
					return;
				}
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

		// Token: 0x06003E34 RID: 15924 RVA: 0x005D0454 File Offset: 0x005CE654
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

		// Token: 0x06003E35 RID: 15925 RVA: 0x005D04AB File Offset: 0x005CE6AB
		private void PlayGame(UIMouseEvent evt, UIElement listeningElement)
		{
			if (listeningElement == evt.Target && this._data.Player.loadStatus == 0)
			{
				Main.SelectPlayer(this._data);
			}
		}

		// Token: 0x06003E36 RID: 15926 RVA: 0x005D04D4 File Offset: 0x005CE6D4
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
			UIList uIList = base.Parent.Parent as UIList;
			if (uIList != null)
			{
				uIList.UpdateOrder();
			}
		}

		// Token: 0x06003E37 RID: 15927 RVA: 0x005D05C8 File Offset: 0x005CE7C8
		public override int CompareTo(object obj)
		{
			UICharacterListItem uICharacterListItem = obj as UICharacterListItem;
			if (uICharacterListItem == null)
			{
				return base.CompareTo(obj);
			}
			if (this.IsFavorite && !uICharacterListItem.IsFavorite)
			{
				return -1;
			}
			if (!this.IsFavorite && uICharacterListItem.IsFavorite)
			{
				return 1;
			}
			if (this._data.Name.CompareTo(uICharacterListItem._data.Name) != 0)
			{
				return this._data.Name.CompareTo(uICharacterListItem._data.Name);
			}
			return this._data.GetFileName(true).CompareTo(uICharacterListItem._data.GetFileName(true));
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x005D0661 File Offset: 0x005CE861
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.BackgroundColor = new Color(73, 94, 171);
			this.BorderColor = new Color(89, 116, 213);
			this._playerPanel.SetAnimated(true);
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x005D06A0 File Offset: 0x005CE8A0
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.BackgroundColor = new Color(63, 82, 151) * 0.7f;
			this.BorderColor = new Color(89, 116, 213) * 0.7f;
			this._playerPanel.SetAnimated(false);
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x005D06FC File Offset: 0x005CE8FC
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(this._innerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), 0, 0f);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x005D07EC File Offset: 0x005CE9EC
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
			spriteBatch.Draw(this._dividerTexture.Value, new Vector2(num, innerDimensions.Y + 21f), null, Color.White, 0f, Vector2.Zero, new Vector2((base.GetDimensions().X + base.GetDimensions().Width - num) / 8f, 1f), 0, 0f);
			Vector2 vector;
			vector..ctor(num + 6f, innerDimensions.Y + 29f);
			float num2 = 200f;
			Vector2 vector2 = vector;
			this.DrawPanel(spriteBatch, vector2, num2);
			spriteBatch.Draw(TextureAssets.Heart.Value, vector2 + new Vector2(5f, 2f), Color.White);
			vector2.X += 10f + (float)TextureAssets.Heart.Width();
			Utils.DrawBorderString(spriteBatch, this._data.Player.statLifeMax2.ToString() + Language.GetTextValue("GameUI.PlayerLifeMax"), vector2 + new Vector2(0f, 3f), Color.White, 1f, 0f, 0f, -1);
			vector2.X += 65f;
			spriteBatch.Draw(TextureAssets.Mana.Value, vector2 + new Vector2(5f, 2f), Color.White);
			vector2.X += 10f + (float)TextureAssets.Mana.Width();
			Utils.DrawBorderString(spriteBatch, this._data.Player.statManaMax2.ToString() + Language.GetTextValue("GameUI.PlayerManaMax"), vector2 + new Vector2(0f, 3f), Color.White, 1f, 0f, 0f, -1);
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
			string text3 = ((num5 < 10) ? "0" : "") + num5.ToString() + playTime.ToString("\\:mm\\:ss");
			vector4 += new Vector2(num4 * 0.5f - FontAssets.MouseText.Value.MeasureString(text3).X * 0.5f, 3f);
			Utils.DrawBorderString(spriteBatch, text3, vector4, Color.White, 1f, 0f, 0f, -1);
		}

		// Token: 0x06003E3C RID: 15932 RVA: 0x005D0C88 File Offset: 0x005CEE88
		private void InitializeTmlFields(PlayerFileData data)
		{
			this._errorTexture = UICommon.ButtonErrorTexture;
			this._configTexture = UICommon.ButtonConfigTexture;
			this._fileSize = (ulong)((long)FileUtilities.GetFileSize(data.Path, data.IsCloudSave));
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x005D0CB8 File Offset: 0x005CEEB8
		private void AddTmlElements(PlayerFileData data)
		{
			this.warningLabel = new UIText("", 1f, false)
			{
				VAlign = 0f,
				HAlign = 1f
			};
			float topRightButtonsLeftPixels = 0f;
			this.warningLabel.Top.Set(3f, 0f);
			base.Append(this.warningLabel);
			if (data.Player.usedMods != null)
			{
				string[] currentModNames = (from m in ModLoader.Mods
				select m.Name).ToArray<string>();
				IEnumerable<string> source = data.Player.usedMods.Except(currentModNames);
				Func<string, string> selector;
				if ((selector = UICharacterListItem.<>O.<0>__GetDisplayNameCleanFromLocalModsOrDefaultToModName) == null)
				{
					selector = (UICharacterListItem.<>O.<0>__GetDisplayNameCleanFromLocalModsOrDefaultToModName = new Func<string, string>(ModOrganizer.GetDisplayNameCleanFromLocalModsOrDefaultToModName));
				}
				List<string> missingMods = source.Select(selector).ToList<string>();
				IEnumerable<string> source2 = currentModNames.Except(new string[]
				{
					"ModLoader"
				}).Except(data.Player.usedMods);
				Func<string, string> selector2;
				if ((selector2 = UICharacterListItem.<>O.<0>__GetDisplayNameCleanFromLocalModsOrDefaultToModName) == null)
				{
					selector2 = (UICharacterListItem.<>O.<0>__GetDisplayNameCleanFromLocalModsOrDefaultToModName = new Func<string, string>(ModOrganizer.GetDisplayNameCleanFromLocalModsOrDefaultToModName));
				}
				List<string> newMods = source2.Select(selector2).ToList<string>();
				bool checkModPack = Path.GetFileNameWithoutExtension(ModOrganizer.ModPackActive) != data.Player.modPack;
				if (checkModPack || missingMods.Count > 0 || newMods.Count > 0)
				{
					UICharacterListItem.<>c__DisplayClass43_0 CS$<>8__locals1 = new UICharacterListItem.<>c__DisplayClass43_0();
					CS$<>8__locals1.<>4__this = this;
					UIImageButton modListWarning = new UIImageButton(this._errorTexture)
					{
						VAlign = 0f,
						HAlign = 1f,
						Top = new StyleDimension(-2f, 0f),
						Left = new StyleDimension(topRightButtonsLeftPixels, 0f)
					};
					topRightButtonsLeftPixels -= 24f;
					StringBuilder fullSB = new StringBuilder(Language.GetTextValue("tModLoader.ModsDifferentSinceLastPlay"));
					CS$<>8__locals1.shortSB = new StringBuilder();
					if (checkModPack)
					{
						string pack = data.Player.modPack;
						if (string.IsNullOrEmpty(pack))
						{
							pack = "None";
						}
						CS$<>8__locals1.shortSB.Append(CS$<>8__locals1.<AddTmlElements>g__Separator|1() + Language.GetTextValue("tModLoader.ModPackMismatch", pack));
						fullSB.Append("\n" + Language.GetTextValue("tModLoader.ModPackMismatch", pack));
					}
					if (missingMods.Count > 0)
					{
						CS$<>8__locals1.shortSB.Append(CS$<>8__locals1.<AddTmlElements>g__Separator|1() + ((missingMods.Count > 1) ? Language.GetTextValue("tModLoader.MissingXMods", missingMods.Count) : Language.GetTextValue("tModLoader.Missing1Mod")));
						fullSB.Append("\n" + Language.GetTextValue("tModLoader.MissingModsListing", string.Join("\n", from x in missingMods
						select "- " + x)));
					}
					if (newMods.Count > 0)
					{
						CS$<>8__locals1.shortSB.Append(CS$<>8__locals1.<AddTmlElements>g__Separator|1() + ((newMods.Count > 1) ? Language.GetTextValue("tModLoader.NewXMods", newMods.Count) : Language.GetTextValue("tModLoader.New1Mod")));
						fullSB.Append("\n" + Language.GetTextValue("tModLoader.NewModsListing", string.Join("\n", from x in newMods
						select "- " + x)));
					}
					if (CS$<>8__locals1.shortSB.Length != 0)
					{
						CS$<>8__locals1.shortSB.Append('.');
					}
					CS$<>8__locals1.warning = CS$<>8__locals1.shortSB.ToString();
					CS$<>8__locals1.fullWarning = fullSB.ToString();
					modListWarning.OnMouseOver += delegate(UIMouseEvent a, UIElement b)
					{
						CS$<>8__locals1.<>4__this.warningLabel.SetText(CS$<>8__locals1.warning);
					};
					modListWarning.OnMouseOut += delegate(UIMouseEvent a, UIElement b)
					{
						this.warningLabel.SetText("");
					};
					modListWarning.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
					{
						Interface.infoMessage.Show(CS$<>8__locals1.fullWarning, 888, Main._characterSelectMenu, "", null, null);
					};
					base.Append(modListWarning);
				}
			}
			if (data.customDataFail != null)
			{
				UIImageButton errorButton = new UIImageButton(this._errorTexture)
				{
					VAlign = 0f,
					HAlign = 1f,
					Top = new StyleDimension(-2f, 0f),
					Left = new StyleDimension(topRightButtonsLeftPixels, 0f)
				};
				topRightButtonsLeftPixels -= 24f;
				errorButton.OnLeftClick += this.ErrorButtonClick;
				errorButton.OnMouseOver += this.ErrorMouseOver;
				errorButton.OnMouseOut += delegate(UIMouseEvent a, UIElement b)
				{
					this.warningLabel.SetText("");
				};
				base.Append(errorButton);
			}
			if (data.Player.ModSaveErrors.Any<KeyValuePair<string, string>>())
			{
				UIImageButton errorButton2 = new UIImageButton(this._errorTexture)
				{
					VAlign = 0f,
					HAlign = 1f,
					Top = new StyleDimension(-2f, 0f),
					Left = new StyleDimension(topRightButtonsLeftPixels, 0f)
				};
				topRightButtonsLeftPixels -= 24f;
				errorButton2.OnLeftClick += this.SaveErrorButtonClick;
				errorButton2.OnMouseOver += this.SaveErrorMouseOver;
				errorButton2.OnMouseOut += delegate(UIMouseEvent a, UIElement b)
				{
					this.warningLabel.SetText("");
				};
				base.Append(errorButton2);
			}
			this.warningLabel.Left.Set(topRightButtonsLeftPixels - 6f, 0f);
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x005D1208 File Offset: 0x005CF408
		private void ErrorMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this.warningLabel.SetText(this._data.customDataFail.modName + " Error");
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x005D122F File Offset: 0x005CF42F
		private void SaveErrorMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this.warningLabel.SetText(Language.GetTextValue("tModLoader.ViewSaveErrorMessage"));
		}

		// Token: 0x06003E40 RID: 15936 RVA: 0x005D1246 File Offset: 0x005CF446
		private void ConfigMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText("Edit Player Config");
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x005D1258 File Offset: 0x005CF458
		private void ErrorButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			UIInfoMessage infoMessage = Interface.infoMessage;
			string textValue = Language.GetTextValue("tModLoader.PlayerCustomDataFail");
			string str = "\n\n";
			Exception innerException = this._data.customDataFail.InnerException;
			infoMessage.Show(textValue + str + ((innerException != null) ? innerException.ToString() : null), 888, Main._characterSelectMenu, "", null, null);
		}

		// Token: 0x06003E42 RID: 15938 RVA: 0x005D12B0 File Offset: 0x005CF4B0
		private void SaveErrorButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			string message = Utils.CreateSaveErrorMessage("tModLoader.PlayerCustomDataSaveFail", this._data.Player.ModSaveErrors, true).ToString();
			Interface.infoMessage.Show(message, 888, Main._characterSelectMenu, "", null, null);
		}

		// Token: 0x06003E43 RID: 15939 RVA: 0x005D12FA File Offset: 0x005CF4FA
		private void ConfigButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
		}

		// Token: 0x040056C4 RID: 22212
		private PlayerFileData _data;

		// Token: 0x040056C5 RID: 22213
		private Asset<Texture2D> _dividerTexture;

		// Token: 0x040056C6 RID: 22214
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x040056C7 RID: 22215
		private UICharacter _playerPanel;

		// Token: 0x040056C8 RID: 22216
		private UIText _buttonLabel;

		// Token: 0x040056C9 RID: 22217
		private UIText _deleteButtonLabel;

		// Token: 0x040056CA RID: 22218
		private Asset<Texture2D> _buttonCloudActiveTexture;

		// Token: 0x040056CB RID: 22219
		private Asset<Texture2D> _buttonCloudInactiveTexture;

		// Token: 0x040056CC RID: 22220
		private Asset<Texture2D> _buttonFavoriteActiveTexture;

		// Token: 0x040056CD RID: 22221
		private Asset<Texture2D> _buttonFavoriteInactiveTexture;

		// Token: 0x040056CE RID: 22222
		private Asset<Texture2D> _buttonPlayTexture;

		// Token: 0x040056CF RID: 22223
		private Asset<Texture2D> _buttonRenameTexture;

		// Token: 0x040056D0 RID: 22224
		private Asset<Texture2D> _buttonDeleteTexture;

		// Token: 0x040056D1 RID: 22225
		private UIImageButton _deleteButton;

		// Token: 0x040056D2 RID: 22226
		private Asset<Texture2D> _errorTexture;

		// Token: 0x040056D3 RID: 22227
		private Asset<Texture2D> _configTexture;

		// Token: 0x040056D4 RID: 22228
		private ulong _fileSize;

		// Token: 0x040056D5 RID: 22229
		private UIText warningLabel;

		// Token: 0x02000C14 RID: 3092
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007861 RID: 30817
			public static Func<string, string> <0>__GetDisplayNameCleanFromLocalModsOrDefaultToModName;
		}
	}
}
