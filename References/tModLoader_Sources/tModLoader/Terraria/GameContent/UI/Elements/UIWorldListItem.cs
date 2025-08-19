using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
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
	// Token: 0x0200053C RID: 1340
	public class UIWorldListItem : AWorldListItem
	{
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06003FC1 RID: 16321 RVA: 0x005DB90D File Offset: 0x005D9B0D
		public bool IsFavorite
		{
			get
			{
				return this._data.IsFavorite;
			}
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x005DB91C File Offset: 0x005D9B1C
		public UIWorldListItem(WorldFileData data, int orderInList, bool canBePlayed)
		{
			this._orderInList = orderInList;
			this._data = data;
			this._canBePlayed = canBePlayed;
			this.InitializeTmlFields(data);
			this.LoadTextures();
			this.InitializeAppearance();
			this._worldIcon = base.GetIconElement();
			this._worldIcon.OnLeftDoubleClick += this.PlayGame;
			base.Append(this._worldIcon);
			if (this._data.DefeatedMoonlord)
			{
				UIImage element = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/IconCompletion"))
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
			UIImageButton uIImageButton = new UIImageButton(this._buttonPlayTexture);
			uIImageButton.VAlign = 1f;
			uIImageButton.Left.Set(num, 0f);
			uIImageButton.OnLeftClick += this.PlayGame;
			base.OnLeftDoubleClick += this.PlayGame;
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
				uIImageButton3.SetSnapPoint("Cloud", orderInList, null, null);
				base.Append(uIImageButton3);
				num += 24f;
			}
			if (this._data.WorldGeneratorVersion != 0UL)
			{
				UIImageButton uIImageButton4 = new UIImageButton(this._buttonSeedTexture);
				uIImageButton4.VAlign = 1f;
				uIImageButton4.Left.Set(num, 0f);
				uIImageButton4.OnLeftClick += this.SeedButtonClick;
				uIImageButton4.OnMouseOver += this.SeedMouseOver;
				uIImageButton4.OnMouseOut += this.ButtonMouseOut;
				uIImageButton4.SetSnapPoint("Seed", orderInList, null, null);
				base.Append(uIImageButton4);
				num += 24f;
			}
			this.AddTmlElements(data, ref num);
			UIImageButton uIImageButton5 = new UIImageButton(this._buttonRenameTexture);
			uIImageButton5.VAlign = 1f;
			uIImageButton5.Left.Set(num, 0f);
			uIImageButton5.OnLeftClick += this.RenameButtonClick;
			uIImageButton5.OnMouseOver += this.RenameMouseOver;
			uIImageButton5.OnMouseOut += this.ButtonMouseOut;
			uIImageButton5.SetSnapPoint("Rename", orderInList, null, null);
			base.Append(uIImageButton5);
			num += 24f;
			UIImageButton uIImageButton6 = new UIImageButton(this._buttonDeleteTexture)
			{
				VAlign = 1f,
				HAlign = 1f
			};
			if (!this._data.IsFavorite)
			{
				uIImageButton6.OnLeftClick += this.DeleteButtonClick;
			}
			uIImageButton6.OnMouseOver += this.DeleteMouseOver;
			uIImageButton6.OnMouseOut += this.DeleteMouseOut;
			this._deleteButton = uIImageButton6;
			base.Append(uIImageButton6);
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
			uIImageButton.SetSnapPoint("Play", orderInList, null, null);
			uIImageButton2.SetSnapPoint("Favorite", orderInList, null, null);
			uIImageButton5.SetSnapPoint("Rename", orderInList, null, null);
			uIImageButton6.SetSnapPoint("Delete", orderInList, null, null);
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x005DBEF4 File Offset: 0x005DA0F4
		private void LoadTextures()
		{
			this._dividerTexture = Main.Assets.Request<Texture2D>("Images/UI/Divider");
			this._innerPanelTexture = Main.Assets.Request<Texture2D>("Images/UI/InnerPanelBackground");
			this._buttonCloudActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudActive");
			this._buttonCloudInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonCloudInactive");
			this._buttonFavoriteActiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteActive");
			this._buttonFavoriteInactiveTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonFavoriteInactive");
			this._buttonPlayTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonPlay");
			this._buttonSeedTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonSeed");
			this._buttonRenameTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonRename");
			this._buttonDeleteTexture = Main.Assets.Request<Texture2D>("Images/UI/ButtonDelete");
			this.LoadTmlTextures();
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x005DBFD9 File Offset: 0x005DA1D9
		private void InitializeAppearance()
		{
			this.Height.Set(96f, 0f);
			this.Width.Set(0f, 1f);
			base.SetPadding(6f);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x005DC018 File Offset: 0x005DA218
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

		// Token: 0x06003FC6 RID: 16326 RVA: 0x005DC0A4 File Offset: 0x005DA2A4
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

		// Token: 0x06003FC7 RID: 16327 RVA: 0x005DC143 File Offset: 0x005DA343
		private void RenameMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Rename"));
		}

		// Token: 0x06003FC8 RID: 16328 RVA: 0x005DC15A File Offset: 0x005DA35A
		private void FavoriteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsFavorite)
			{
				this._buttonLabel.SetText(Language.GetTextValue("UI.Unfavorite"));
				return;
			}
			this._buttonLabel.SetText(Language.GetTextValue("UI.Favorite"));
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x005DC194 File Offset: 0x005DA394
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

		// Token: 0x06003FCA RID: 16330 RVA: 0x005DC1FC File Offset: 0x005DA3FC
		private void PlayMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.Play"));
		}

		// Token: 0x06003FCB RID: 16331 RVA: 0x005DC213 File Offset: 0x005DA413
		private void SeedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText(Language.GetTextValue("UI.CopySeed", this._data.GetFullSeedText(true)));
		}

		// Token: 0x06003FCC RID: 16332 RVA: 0x005DC236 File Offset: 0x005DA436
		private void DeleteMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			if (this._data.IsFavorite)
			{
				this._deleteButtonLabel.SetText(Language.GetTextValue("UI.CannotDeleteFavorited"));
				return;
			}
			this._deleteButtonLabel.SetText(Language.GetTextValue("UI.Delete"));
		}

		// Token: 0x06003FCD RID: 16333 RVA: 0x005DC270 File Offset: 0x005DA470
		private void DeleteMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._deleteButtonLabel.SetText("");
		}

		// Token: 0x06003FCE RID: 16334 RVA: 0x005DC282 File Offset: 0x005DA482
		private void ButtonMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText("");
		}

		// Token: 0x06003FCF RID: 16335 RVA: 0x005DC294 File Offset: 0x005DA494
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

		// Token: 0x06003FD0 RID: 16336 RVA: 0x005DC33C File Offset: 0x005DA53C
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

		// Token: 0x06003FD1 RID: 16337 RVA: 0x005DC394 File Offset: 0x005DA594
		private void PlayGame(UIMouseEvent evt, UIElement listeningElement)
		{
			if (!this._data.IsValid)
			{
				return;
			}
			if (listeningElement == evt.Target && !this.TryMovingToRejectionMenuIfNeeded(this._data.GameMode))
			{
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
		}

		// Token: 0x06003FD2 RID: 16338 RVA: 0x005DC43C File Offset: 0x005DA63C
		private bool TryMovingToRejectionMenuIfNeeded(int worldGameMode)
		{
			GameModeData value;
			if (!Main.RegisteredGameModes.TryGetValue(worldGameMode, out value))
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.statusText = Language.GetTextValue("UI.WorldCannotBeLoadedBecauseItHasAnInvalidGameMode");
				Main.menuMode = 1000000;
				return true;
			}
			if (this._canBePlayed)
			{
				return false;
			}
			bool flag = Main.ActivePlayerFileData.Player.difficulty == 3;
			bool isJourneyMode = value.IsJourneyMode;
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
			ModSystem rejector;
			if (!SystemLoader.CanWorldBePlayed(Main.ActivePlayerFileData, this._data, out rejector))
			{
				SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
				Main.statusText = rejector.WorldCanBePlayedRejectionMessage(Main.ActivePlayerFileData, this._data);
				Main.menuMode = 1000000;
				return true;
			}
			return false;
		}

		// Token: 0x06003FD3 RID: 16339 RVA: 0x005DC568 File Offset: 0x005DA768
		private void RenameButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.clrInput();
			UIVirtualKeyboard uIVirtualKeyboard = new UIVirtualKeyboard(Lang.menu[48].Value, "", new UIVirtualKeyboard.KeyboardSubmitEvent(this.OnFinishedSettingName), new Action(this.GoBackHere), 0, true);
			uIVirtualKeyboard.SetMaxInputLength(27);
			Main.MenuUI.SetState(uIVirtualKeyboard);
			UIList uIList = base.Parent.Parent as UIList;
			if (uIList != null)
			{
				uIList.UpdateOrder();
			}
		}

		// Token: 0x06003FD4 RID: 16340 RVA: 0x005DC5F0 File Offset: 0x005DA7F0
		private void OnFinishedSettingName(string name)
		{
			string newDisplayName = name.Trim();
			Main.menuMode = 10;
			this._data.Rename(newDisplayName);
		}

		// Token: 0x06003FD5 RID: 16341 RVA: 0x005DC617 File Offset: 0x005DA817
		private void GoBackHere()
		{
			Main.GoToWorldSelect();
		}

		// Token: 0x06003FD6 RID: 16342 RVA: 0x005DC620 File Offset: 0x005DA820
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

		// Token: 0x06003FD7 RID: 16343 RVA: 0x005DC712 File Offset: 0x005DA912
		private void SeedButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
			Platform.Get<IClipboard>().Value = this._data.GetFullSeedText(false);
			this._buttonLabel.SetText(Language.GetTextValue("UI.SeedCopied"));
		}

		// Token: 0x06003FD8 RID: 16344 RVA: 0x005DC740 File Offset: 0x005DA940
		public override int CompareTo(object obj)
		{
			UIWorldListItem uIWorldListItem = obj as UIWorldListItem;
			if (uIWorldListItem != null)
			{
				return this._orderInList.CompareTo(uIWorldListItem._orderInList);
			}
			if (obj is UIPanel)
			{
				return -1;
			}
			return base.CompareTo(obj);
		}

		// Token: 0x06003FD9 RID: 16345 RVA: 0x005DC77A File Offset: 0x005DA97A
		public override void MouseOver(UIMouseEvent evt)
		{
			base.MouseOver(evt);
			this.SetColorsToHovered();
		}

		// Token: 0x06003FDA RID: 16346 RVA: 0x005DC789 File Offset: 0x005DA989
		public override void MouseOut(UIMouseEvent evt)
		{
			base.MouseOut(evt);
			this.SetColorsToNotHovered();
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x005DC798 File Offset: 0x005DA998
		private void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
		{
			spriteBatch.Draw(this._innerPanelTexture.Value, position, new Rectangle?(new Rectangle(0, 0, 8, this._innerPanelTexture.Height())), Color.White);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + 8f, position.Y), new Rectangle?(new Rectangle(8, 0, 8, this._innerPanelTexture.Height())), Color.White, 0f, Vector2.Zero, new Vector2((width - 16f) / 8f, 1f), 0, 0f);
			spriteBatch.Draw(this._innerPanelTexture.Value, new Vector2(position.X + width - 8f, position.Y), new Rectangle?(new Rectangle(16, 0, 8, this._innerPanelTexture.Height())), Color.White);
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x005DC888 File Offset: 0x005DAA88
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			base.DrawSelf(spriteBatch);
			CalculatedStyle innerDimensions = base.GetInnerDimensions();
			CalculatedStyle dimensions = this._worldIcon.GetDimensions();
			float num = dimensions.X + dimensions.Width;
			Color color = this._data.IsValid ? Color.White : Color.Gray;
			string worldName = this._data.GetWorldName(true);
			Utils.DrawBorderString(spriteBatch, worldName, new Vector2(num + 6f, dimensions.Y - 2f), color, 1f, 0f, 0f, -1);
			spriteBatch.Draw(this._dividerTexture.Value, new Vector2(num, innerDimensions.Y + 21f), null, Color.White, 0f, Vector2.Zero, new Vector2((base.GetDimensions().X + base.GetDimensions().Width - num) / 8f, 1f), 0, 0f);
			Vector2 vector;
			vector..ctor(num + 6f, innerDimensions.Y + 29f);
			float num2 = 100f;
			this.DrawPanel(spriteBatch, vector, num2);
			string expertText;
			Color gameModeColor;
			base.GetDifficulty(out expertText, out gameModeColor);
			float x = FontAssets.MouseText.Value.MeasureString(expertText).X;
			float x2 = num2 * 0.5f - x * 0.5f;
			Utils.DrawBorderString(spriteBatch, expertText, vector + new Vector2(x2, 3f), gameModeColor, 1f, 0f, 0f, -1);
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
			string arg = (!GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive) ? this._data.CreationTime.ToShortDateString() : this._data.CreationTime.ToString("d MMMM yyyy");
			string textValue2 = Language.GetTextValue("UI.WorldCreatedFormat", arg);
			float x5 = FontAssets.MouseText.Value.MeasureString(textValue2).X;
			float x6 = num4 * 0.5f - x5 * 0.5f;
			Utils.DrawBorderString(spriteBatch, textValue2, vector + new Vector2(x6, 3f), Color.White, 1f, 0f, 0f, -1);
			vector.X += num4 + 5f;
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x005DCBA8 File Offset: 0x005DADA8
		private void InitializeTmlFields(WorldFileData data)
		{
			this._fileSize = (ulong)((long)FileUtilities.GetFileSize(data.Path, data.IsCloudSave));
		}

		// Token: 0x06003FDE RID: 16350 RVA: 0x005DCBC2 File Offset: 0x005DADC2
		private void LoadTmlTextures()
		{
			this._configTexture = UICommon.ButtonConfigTexture;
		}

		// Token: 0x06003FDF RID: 16351 RVA: 0x005DCBD0 File Offset: 0x005DADD0
		private void AddTmlElements(WorldFileData data, ref float offset)
		{
			this.warningLabel = new UIText("", 1f, false)
			{
				VAlign = 0f,
				HAlign = 1f
			};
			float topRightButtonsLeftPixels = 0f;
			this.warningLabel.Top.Set(3f, 0f);
			base.Append(this.warningLabel);
			if (data.usedMods != null)
			{
				string[] currentModNames = (from m in ModLoader.Mods
				select m.Name).ToArray<string>();
				IEnumerable<string> source = data.usedMods.Except(currentModNames);
				Func<string, string> selector;
				if ((selector = UIWorldListItem.<>O.<0>__GetDisplayNameCleanFromLocalModsOrDefaultToModName) == null)
				{
					selector = (UIWorldListItem.<>O.<0>__GetDisplayNameCleanFromLocalModsOrDefaultToModName = new Func<string, string>(ModOrganizer.GetDisplayNameCleanFromLocalModsOrDefaultToModName));
				}
				List<string> missingMods = source.Select(selector).ToList<string>();
				IEnumerable<string> source2 = currentModNames.Except(new string[]
				{
					"ModLoader"
				}).Except(data.usedMods);
				Func<string, string> selector2;
				if ((selector2 = UIWorldListItem.<>O.<0>__GetDisplayNameCleanFromLocalModsOrDefaultToModName) == null)
				{
					selector2 = (UIWorldListItem.<>O.<0>__GetDisplayNameCleanFromLocalModsOrDefaultToModName = new Func<string, string>(ModOrganizer.GetDisplayNameCleanFromLocalModsOrDefaultToModName));
				}
				List<string> newMods = source2.Select(selector2).ToList<string>();
				bool checkModPack = Path.GetFileNameWithoutExtension(ModOrganizer.ModPackActive) != data.modPack;
				if (checkModPack || missingMods.Count > 0 || newMods.Count > 0)
				{
					UIWorldListItem.<>c__DisplayClass50_0 CS$<>8__locals1 = new UIWorldListItem.<>c__DisplayClass50_0();
					CS$<>8__locals1.<>4__this = this;
					UIImageButton modListWarning = new UIImageButton(UICommon.ButtonErrorTexture)
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
						string pack = data.modPack;
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
						Interface.infoMessage.Show(CS$<>8__locals1.fullWarning, 888, Main._worldSelectMenu, "", null, null);
					};
					base.Append(modListWarning);
				}
			}
			if (data.ModSaveErrors.Any<KeyValuePair<string, string>>())
			{
				UIImageButton modSaveErrorWarning = new UIImageButton(UICommon.ButtonErrorTexture)
				{
					VAlign = 0f,
					HAlign = 1f,
					Top = new StyleDimension(-2f, 0f),
					Left = new StyleDimension(topRightButtonsLeftPixels, 0f)
				};
				topRightButtonsLeftPixels -= 24f;
				string warning = Language.GetTextValue("tModLoader.ViewSaveErrorMessage");
				string fullError = Utils.CreateSaveErrorMessage("tModLoader.WorldCustomDataSaveFail", data.ModSaveErrors, true).ToString();
				modSaveErrorWarning.OnMouseOver += delegate(UIMouseEvent a, UIElement b)
				{
					this.warningLabel.SetText(warning);
				};
				modSaveErrorWarning.OnMouseOut += delegate(UIMouseEvent a, UIElement b)
				{
					this.warningLabel.SetText("");
				};
				modSaveErrorWarning.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
				{
					Interface.infoMessage.Show(fullError, 888, Main._worldSelectMenu, "", null, null);
				};
				base.Append(modSaveErrorWarning);
			}
			this.warningLabel.Left.Set(topRightButtonsLeftPixels - 6f, 0f);
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x005DD0A2 File Offset: 0x005DB2A2
		internal static Action PlayReload()
		{
			string path = Main.ActivePlayerFileData.Path;
			bool isCloudSave = Main.ActivePlayerFileData.IsCloudSave;
			return delegate()
			{
				Player.GetFileData(path, isCloudSave).SetAsActive();
				WorldGen.playWorld();
			};
		}

		// Token: 0x06003FE1 RID: 16353 RVA: 0x005DD0D4 File Offset: 0x005DB2D4
		private void ConfigMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			this._buttonLabel.SetText("Edit World Config");
		}

		// Token: 0x06003FE2 RID: 16354 RVA: 0x005DD0E6 File Offset: 0x005DB2E6
		private void ConfigButtonClick(UIMouseEvent evt, UIElement listeningElement)
		{
		}

		// Token: 0x0400581E RID: 22558
		private Asset<Texture2D> _dividerTexture;

		// Token: 0x0400581F RID: 22559
		private Asset<Texture2D> _innerPanelTexture;

		// Token: 0x04005820 RID: 22560
		private UIElement _worldIcon;

		// Token: 0x04005821 RID: 22561
		private UIText _buttonLabel;

		// Token: 0x04005822 RID: 22562
		private UIText _deleteButtonLabel;

		// Token: 0x04005823 RID: 22563
		private Asset<Texture2D> _buttonCloudActiveTexture;

		// Token: 0x04005824 RID: 22564
		private Asset<Texture2D> _buttonCloudInactiveTexture;

		// Token: 0x04005825 RID: 22565
		private Asset<Texture2D> _buttonFavoriteActiveTexture;

		// Token: 0x04005826 RID: 22566
		private Asset<Texture2D> _buttonFavoriteInactiveTexture;

		// Token: 0x04005827 RID: 22567
		private Asset<Texture2D> _buttonPlayTexture;

		// Token: 0x04005828 RID: 22568
		private Asset<Texture2D> _buttonSeedTexture;

		// Token: 0x04005829 RID: 22569
		private Asset<Texture2D> _buttonRenameTexture;

		// Token: 0x0400582A RID: 22570
		private Asset<Texture2D> _buttonDeleteTexture;

		// Token: 0x0400582B RID: 22571
		private UIImageButton _deleteButton;

		// Token: 0x0400582C RID: 22572
		private int _orderInList;

		// Token: 0x0400582D RID: 22573
		private bool _canBePlayed;

		// Token: 0x0400582E RID: 22574
		private ulong _fileSize;

		// Token: 0x0400582F RID: 22575
		private Asset<Texture2D> _configTexture;

		// Token: 0x04005830 RID: 22576
		private UIText warningLabel;

		// Token: 0x02000C21 RID: 3105
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04007882 RID: 30850
			public static Func<string, string> <0>__GetDisplayNameCleanFromLocalModsOrDefaultToModName;
		}
	}
}
