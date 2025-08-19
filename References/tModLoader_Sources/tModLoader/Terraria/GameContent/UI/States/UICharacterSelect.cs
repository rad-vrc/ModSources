using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.OS;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.Utilities;

namespace Terraria.GameContent.UI.States
{
	// Token: 0x020004D8 RID: 1240
	public class UICharacterSelect : UIState
	{
		// Token: 0x06003B8D RID: 15245 RVA: 0x005B5514 File Offset: 0x005B3714
		public override void OnInitialize()
		{
			UIElement uIElement = new UIElement();
			uIElement.Width.Set(0f, 0.8f);
			uIElement.MaxWidth.Set(650f, 0f);
			uIElement.Top.Set(220f, 0f);
			uIElement.Height.Set(-220f, 1f);
			uIElement.HAlign = 0.5f;
			UIPanel uIPanel = new UIPanel();
			uIPanel.Width.Set(0f, 1f);
			uIPanel.Height.Set(-110f, 1f);
			uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
			this._containerPanel = uIPanel;
			uIElement.Append(uIPanel);
			this._playerList = new UIList();
			this._playerList.Width.Set(0f, 1f);
			this._playerList.Height.Set(0f, 1f);
			this._playerList.ListPadding = 5f;
			uIPanel.Append(this._playerList);
			this._scrollbar = new UIScrollbar();
			this._scrollbar.SetView(100f, 1000f);
			this._scrollbar.Height.Set(0f, 1f);
			this._scrollbar.HAlign = 1f;
			this._playerList.SetScrollbar(this._scrollbar);
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.SelectPlayer"), 0.8f, true);
			uITextPanel.HAlign = 0.5f;
			uITextPanel.Top.Set(-40f, 0f);
			uITextPanel.SetPadding(15f);
			uITextPanel.BackgroundColor = new Color(73, 94, 171);
			uIElement.Append(uITextPanel);
			UITextPanel<LocalizedText> uITextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uITextPanel2.Width.Set(-10f, 0.5f);
			uITextPanel2.Height.Set(50f, 0f);
			uITextPanel2.VAlign = 1f;
			uITextPanel2.Top.Set(-45f, 0f);
			uITextPanel2.OnMouseOver += this.FadedMouseOver;
			uITextPanel2.OnMouseOut += this.FadedMouseOut;
			uITextPanel2.OnLeftClick += this.GoBackClick;
			uITextPanel2.SetSnapPoint("Back", 0, null, null);
			uIElement.Append(uITextPanel2);
			this._backPanel = uITextPanel2;
			UITextPanel<LocalizedText> uITextPanel3 = new UITextPanel<LocalizedText>(Language.GetText("UI.New"), 0.7f, true);
			uITextPanel3.CopyStyle(uITextPanel2);
			uITextPanel3.HAlign = 1f;
			uITextPanel3.OnMouseOver += this.FadedMouseOver;
			uITextPanel3.OnMouseOut += this.FadedMouseOut;
			uITextPanel3.OnLeftClick += this.NewCharacterClick;
			uIElement.Append(uITextPanel3);
			uITextPanel2.SetSnapPoint("New", 0, null, null);
			this._newPanel = uITextPanel3;
			base.Append(uIElement);
			this.InitializeMigrationPanel();
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x005B5858 File Offset: 0x005B3A58
		public override void Recalculate()
		{
			if (this._scrollbar != null)
			{
				if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
				{
					this._containerPanel.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._playerList.Width.Set(0f, 1f);
				}
				else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
				{
					this._containerPanel.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._playerList.Width.Set(-25f, 1f);
				}
			}
			base.Recalculate();
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x005B5906 File Offset: 0x005B3B06
		private void NewCharacterClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.PendingPlayer = new Player();
			Main.menuMode = 888;
			Main.MenuUI.SetState(new UICharacterCreation(Main.PendingPlayer));
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x005B5945 File Offset: 0x005B3B45
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = 0;
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x005B5964 File Offset: 0x005B3B64
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x005B59B9 File Offset: 0x005B3BB9
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x005B59F8 File Offset: 0x005B3BF8
		public override void OnActivate()
		{
			Main.LoadPlayers();
			Main.ActivePlayerFileData = new PlayerFileData();
			this.ActivateMigrationPanel();
			this.UpdatePlayersList();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3000 + ((this._playerList.Count == 0) ? 1 : 2));
			}
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x005B5A38 File Offset: 0x005B3C38
		private void UpdatePlayersList()
		{
			this._playerList.Clear();
			List<PlayerFileData> list = new List<PlayerFileData>(Main.PlayerList);
			list.Sort(delegate(PlayerFileData x, PlayerFileData y)
			{
				if (x.IsFavorite && !y.IsFavorite)
				{
					return -1;
				}
				if (!x.IsFavorite && y.IsFavorite)
				{
					return 1;
				}
				if (x.Name.CompareTo(y.Name) == 0)
				{
					return x.GetFileName(true).CompareTo(y.GetFileName(true));
				}
				return x.Name.CompareTo(y.Name);
			});
			int num = 0;
			foreach (PlayerFileData item in list)
			{
				this._playerList.Add(new UICharacterListItem(item, num++));
			}
			this._playerList.Add(UICharacterSelect._migrationPanel);
			if (list.Count == 0)
			{
				this.AddAutomaticPlayerMigrationButtons();
			}
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x005B5AF0 File Offset: 0x005B3CF0
		public override void Draw(SpriteBatch spriteBatch)
		{
			if (this.skipDraw)
			{
				this.skipDraw = false;
				return;
			}
			if (this.UpdateFavoritesCache())
			{
				this.skipDraw = true;
				Main.MenuUI.Draw(spriteBatch, new GameTime());
			}
			base.Draw(spriteBatch);
			this.SetupGamepadPoints(spriteBatch);
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x005B5B30 File Offset: 0x005B3D30
		private bool UpdateFavoritesCache()
		{
			List<PlayerFileData> list = new List<PlayerFileData>(Main.PlayerList);
			list.Sort(delegate(PlayerFileData x, PlayerFileData y)
			{
				if (x.IsFavorite && !y.IsFavorite)
				{
					return -1;
				}
				if (!x.IsFavorite && y.IsFavorite)
				{
					return 1;
				}
				if (x.Name.CompareTo(y.Name) == 0)
				{
					return x.GetFileName(true).CompareTo(y.GetFileName(true));
				}
				return x.Name.CompareTo(y.Name);
			});
			bool flag = false;
			if (!flag && list.Count != this.favoritesCache.Count)
			{
				flag = true;
			}
			if (!flag)
			{
				for (int i = 0; i < this.favoritesCache.Count; i++)
				{
					Tuple<string, bool> tuple = this.favoritesCache[i];
					if (!(list[i].Name == tuple.Item1) || list[i].IsFavorite != tuple.Item2)
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				this.favoritesCache.Clear();
				foreach (PlayerFileData item in list)
				{
					this.favoritesCache.Add(Tuple.Create<string, bool>(item.Name, item.IsFavorite));
				}
				this.UpdatePlayersList();
			}
			return flag;
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x005B5C50 File Offset: 0x005B3E50
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 1;
			int num = 3000;
			UILinkPointNavigator.SetPosition(num, this._backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			UILinkPointNavigator.SetPosition(num + 1, this._newPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
			int num2 = num;
			UILinkPoint uILinkPoint = UILinkPointNavigator.Points[num2];
			uILinkPoint.Unlink();
			uILinkPoint.Right = num2 + 1;
			num2 = num + 1;
			uILinkPoint = UILinkPointNavigator.Points[num2];
			uILinkPoint.Unlink();
			uILinkPoint.Left = num2 - 1;
			float num3 = 1f / Main.UIScale;
			Rectangle clippingRectangle = this._containerPanel.GetClippingRectangle(spriteBatch);
			Vector2 minimum = clippingRectangle.TopLeft() * num3;
			Vector2 maximum = clippingRectangle.BottomRight() * num3;
			List<SnapPoint> snapPoints = this.GetSnapPoints();
			for (int i = 0; i < snapPoints.Count; i++)
			{
				if (!snapPoints[i].Position.Between(minimum, maximum))
				{
					snapPoints.Remove(snapPoints[i]);
					i--;
				}
			}
			int num4 = 5;
			SnapPoint[,] array = new SnapPoint[this._playerList.Count, num4];
			foreach (SnapPoint item in from a in snapPoints
			where a.Name == "Play"
			select a)
			{
				array[item.Id, 0] = item;
			}
			foreach (SnapPoint item2 in from a in snapPoints
			where a.Name == "Favorite"
			select a)
			{
				array[item2.Id, 1] = item2;
			}
			foreach (SnapPoint item3 in from a in snapPoints
			where a.Name == "Cloud"
			select a)
			{
				array[item3.Id, 2] = item3;
			}
			foreach (SnapPoint item4 in from a in snapPoints
			where a.Name == "Rename"
			select a)
			{
				array[item4.Id, 3] = item4;
			}
			foreach (SnapPoint item5 in from a in snapPoints
			where a.Name == "Delete"
			select a)
			{
				array[item5.Id, 4] = item5;
			}
			num2 = num + 2;
			int[] array2 = new int[this._playerList.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = -1;
			}
			for (int k = 0; k < num4; k++)
			{
				int num5 = -1;
				for (int l = 0; l < array.GetLength(0); l++)
				{
					if (array[l, k] != null)
					{
						uILinkPoint = UILinkPointNavigator.Points[num2];
						uILinkPoint.Unlink();
						UILinkPointNavigator.SetPosition(num2, array[l, k].Position);
						if (num5 != -1)
						{
							uILinkPoint.Up = num5;
							UILinkPointNavigator.Points[num5].Down = num2;
						}
						if (array2[l] != -1)
						{
							uILinkPoint.Left = array2[l];
							UILinkPointNavigator.Points[array2[l]].Right = num2;
						}
						uILinkPoint.Down = num;
						if (k == 0)
						{
							UILinkPointNavigator.Points[num].Up = (UILinkPointNavigator.Points[num + 1].Up = num2);
						}
						num5 = num2;
						array2[l] = num2;
						UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = num2;
						num2++;
					}
				}
			}
			if (PlayerInput.UsingGamepadUI && this._playerList.Count == 0 && UILinkPointNavigator.CurrentPoint > 3001)
			{
				UILinkPointNavigator.ChangePoint(3001);
			}
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x005B6104 File Offset: 0x005B4304
		private void InitializeMigrationPanel()
		{
			UICharacterSelect._migrationPanel = new UIExpandablePanel();
			UICharacterSelect._migrationPanel.OnExpanded += this._migrationPanel_OnExpanded;
			UIText playerMigrationPanelTitle = new UIText(Language.GetTextValue("tModLoader.MigrateIndividualPlayersHeader"), 1f, false);
			playerMigrationPanelTitle.Top.Set(4f, 0f);
			UICharacterSelect._migrationPanel.Append(playerMigrationPanelTitle);
			UICharacterSelect.migratePlayerList = new NestedUIList();
			UICharacterSelect.migratePlayerList.Width.Set(-22f, 1f);
			UICharacterSelect.migratePlayerList.Left.Set(0f, 0f);
			UICharacterSelect.migratePlayerList.Top.Set(30f, 0f);
			UICharacterSelect.migratePlayerList.MinHeight.Set(300f, 0f);
			UICharacterSelect.migratePlayerList.ListPadding = 5f;
			UICharacterSelect._migrationPanel.VisibleWhenExpanded.Add(UICharacterSelect.migratePlayerList);
			UIScrollbar scrollbar = new UIScrollbar();
			scrollbar.SetView(100f, 1000f);
			scrollbar.Height.Set(-42f, 1f);
			scrollbar.Top.Set(36f, 0f);
			UIScrollbar uiscrollbar = scrollbar;
			uiscrollbar.Left.Pixels = uiscrollbar.Left.Pixels - 0f;
			scrollbar.HAlign = 1f;
			UICharacterSelect.migratePlayerList.SetScrollbar(scrollbar);
			UICharacterSelect._migrationPanel.VisibleWhenExpanded.Add(scrollbar);
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x005B6272 File Offset: 0x005B4472
		private void ActivateMigrationPanel()
		{
			UICharacterSelect.migratePlayerList.Clear();
			UICharacterSelect.migratablePlayersLoaded = false;
			UICharacterSelect._migrationPanel.Collapse();
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x005B628E File Offset: 0x005B448E
		private void _migrationPanel_OnExpanded()
		{
			if (UICharacterSelect.migratablePlayersLoaded)
			{
				return;
			}
			UICharacterSelect.migratablePlayersLoaded = true;
			this.LoadMigratablePlayers();
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x005B62A4 File Offset: 0x005B44A4
		private void LoadMigratablePlayers()
		{
			ValueTuple<string, string, int>[] otherPaths = FileUtilities.GetAlternateSavePathFiles("Players");
			BuildInfo.BuildPurpose purpose = BuildInfo.Purpose;
			int j;
			if (purpose != BuildInfo.BuildPurpose.Preview)
			{
				if (purpose == BuildInfo.BuildPurpose.Stable)
				{
					j = 1;
				}
				else
				{
					j = 3;
				}
			}
			else
			{
				j = 2;
			}
			int currentStabilityLevel = j;
			ValueTuple<string, string, int>[] array = otherPaths;
			for (j = 0; j < array.Length; j++)
			{
				ValueTuple<string, string, int> valueTuple = array[j];
				string otherSaveFolderPath = valueTuple.Item1;
				string message = valueTuple.Item2;
				int stabilityLevel = valueTuple.Item3;
				if (stabilityLevel != currentStabilityLevel && Directory.Exists(otherSaveFolderPath))
				{
					string[] files = Directory.GetFiles(otherSaveFolderPath, "*.plr");
					int num2 = Math.Min(1000, files.Length);
					for (int i = 0; i < num2; i++)
					{
						string playerInThisPlayersPath = Path.Combine(Main.PlayerPath, Path.GetFileName(files[i]));
						if (!File.Exists(playerInThisPlayersPath) || !(File.GetLastWriteTime(playerInThisPlayersPath) == File.GetLastWriteTime(files[i])))
						{
							PlayerFileData fileData = Player.GetFileData(files[i], false);
							if (fileData != null)
							{
								UIPanel migrateIndividualPlayerPanel = new UIPanel();
								migrateIndividualPlayerPanel.Width.Set(0f, 1f);
								migrateIndividualPlayerPanel.Height.Set(50f, 0f);
								float left = 0f;
								if (stabilityLevel > currentStabilityLevel)
								{
									UIHoverImage uihoverImage = new UIHoverImage(UICommon.ButtonErrorTexture, Language.GetTextValue("tModLoader.PlayerFromNewerTModMightNotWork"));
									uihoverImage.UseTooltipMouseText = true;
									uihoverImage.Left.Pixels = left;
									uihoverImage.Top.Pixels = 3f;
									UIHoverImage warningImage = uihoverImage;
									migrateIndividualPlayerPanel.Append(warningImage);
									left += warningImage.Width.Pixels + 6f;
								}
								PlayerFileData playerWithSameName = Main.PlayerList.FirstOrDefault((PlayerFileData x) => x.Name == fileData.Name);
								if (playerWithSameName != null)
								{
									UIHoverImage uihoverImage2 = new UIHoverImage(UICommon.ButtonExclamationTexture, Language.GetTextValue("tModLoader.PlayerWithThisNameExistsWillBeOverwritten"));
									uihoverImage2.UseTooltipMouseText = true;
									uihoverImage2.Left.Pixels = left;
									uihoverImage2.Top.Pixels = 3f;
									UIHoverImage warningImage2 = uihoverImage2;
									migrateIndividualPlayerPanel.Append(warningImage2);
									left += warningImage2.Width.Pixels + 6f;
									if (File.GetLastWriteTime(playerWithSameName.Path) > File.GetLastWriteTime(files[i]))
									{
										UIHoverImage uihoverImage3 = new UIHoverImage(UICommon.ButtonExclamationTexture, Language.GetTextValue("tModLoader.ExistingPlayerPlayedMoreRecently"));
										uihoverImage3.UseTooltipMouseText = true;
										uihoverImage3.Left.Pixels = left;
										uihoverImage3.Top.Pixels = 3f;
										warningImage2 = uihoverImage3;
										migrateIndividualPlayerPanel.Append(warningImage2);
										left += warningImage2.Width.Pixels + 6f;
									}
								}
								UIText migrateIndividualPlayerText = new UIText(string.Format(message, fileData.Name), 1f, false);
								migrateIndividualPlayerText.Width.Set(-left, 1f);
								migrateIndividualPlayerText.Left.Set(left, 0f);
								migrateIndividualPlayerText.Height.Set(0f, 1f);
								Action <>9__2;
								migrateIndividualPlayerText.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
								{
									if (UICharacterSelect._currentlyMigratingFiles)
									{
										return;
									}
									UICharacterSelect._currentlyMigratingFiles = true;
									migrateIndividualPlayerText.SetText(Language.GetText("tModLoader.MigratingWorldsText"));
									TaskFactory factory = Task.Factory;
									Action action;
									if ((action = <>9__2) == null)
									{
										action = (<>9__2 = delegate()
										{
											UICharacterSelect.ExecuteIndividualPlayerMigration(fileData, otherSaveFolderPath);
										});
									}
									factory.StartNew(action, TaskCreationOptions.PreferFairness);
								};
								migrateIndividualPlayerPanel.Append(migrateIndividualPlayerText);
								UICharacterSelect.migratePlayerList.Add(migrateIndividualPlayerPanel);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x005B6608 File Offset: 0x005B4808
		private static void ExecuteIndividualPlayerMigration(PlayerFileData fileData, string otherSaveFolderPath)
		{
			try
			{
				string playerFileName = Path.GetFileNameWithoutExtension(fileData.Path);
				foreach (string path in from s in Directory.GetFiles(Main.PlayerPath, playerFileName + ".*")
				where s.EndsWith(".plr") || s.EndsWith(".tplr") || s.EndsWith(".bak")
				select s)
				{
					File.Delete(path);
				}
				string existingPlayerMapPath = Path.Combine(Main.PlayerPath, Path.GetFileNameWithoutExtension(fileData.Path));
				if (Directory.Exists(existingPlayerMapPath))
				{
					Directory.Delete(existingPlayerMapPath, true);
				}
				foreach (string otherPlayerFile in from s in Directory.GetFiles(otherSaveFolderPath, playerFileName + ".*")
				where s.EndsWith(".plr") || s.EndsWith(".tplr") || s.EndsWith(".bak")
				select s)
				{
					File.Copy(otherPlayerFile, Path.Combine(Main.PlayerPath, Path.GetFileName(otherPlayerFile)), true);
				}
				string playerMapPath = Path.Combine(otherSaveFolderPath, Path.GetFileNameWithoutExtension(fileData.Path));
				if (Directory.Exists(playerMapPath))
				{
					FileUtilities.CopyFolder(playerMapPath, existingPlayerMapPath);
				}
			}
			catch (Exception e)
			{
				Logging.tML.Error(Language.GetText("tModLoader.MigratePlayersException"), e);
			}
			UICharacterSelect._currentlyMigratingFiles = false;
			Main.menuMode = 1;
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x005B67B0 File Offset: 0x005B49B0
		private void AddAutomaticPlayerMigrationButtons()
		{
			string vanillaPlayersPath = Path.Combine(Platform.Get<IPathService>().GetStoragePath("Terraria"), "Players");
			if (!Directory.Exists(vanillaPlayersPath) || !Directory.GetFiles(vanillaPlayersPath, "*.plr").Any<string>())
			{
				return;
			}
			UIPanel autoMigrateButton = new UIPanel();
			autoMigrateButton.Width.Set(0f, 1f);
			autoMigrateButton.Height.Set(50f, 0f);
			UIText migrateText = new UIText((!UICharacterSelect._currentlyMigratingFiles) ? Language.GetText("tModLoader.MigratePlayersText") : Language.GetText("tModLoader.MigratingWorldsText"), 1f, false);
			Action <>9__2;
			autoMigrateButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				if (UICharacterSelect._currentlyMigratingFiles)
				{
					return;
				}
				UICharacterSelect._currentlyMigratingFiles = true;
				migrateText.SetText(Language.GetText("tModLoader.MigratingWorldsText"));
				TaskFactory factory = Task.Factory;
				Action action;
				if ((action = <>9__2) == null)
				{
					action = (<>9__2 = delegate()
					{
						UICharacterSelect.ExecuteAutomaticPlayerMigration(vanillaPlayersPath);
					});
				}
				factory.StartNew(action, TaskCreationOptions.PreferFairness);
			};
			autoMigrateButton.Append(migrateText);
			this._playerList.Add(autoMigrateButton);
			UIText noPlayersMessage = new UIText(Language.GetTextValue("tModLoader.MigratePlayersMessage", Program.SaveFolderName), 1f, false);
			noPlayersMessage.Width.Set(0f, 1f);
			noPlayersMessage.Height.Set(300f, 0f);
			noPlayersMessage.MarginTop = 20f;
			noPlayersMessage.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				Utils.OpenFolder(Main.PlayerPath);
				Utils.OpenFolder(vanillaPlayersPath);
			};
			this._playerList.Add(noPlayersMessage);
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x005B6900 File Offset: 0x005B4B00
		private static void ExecuteAutomaticPlayerMigration(string vanillaPlayersPath)
		{
			foreach (string file in from s in Directory.GetFiles(vanillaPlayersPath, "*.*")
			where s.EndsWith(".plr") || s.EndsWith(".tplr") || s.EndsWith(".bak")
			select s)
			{
				File.Copy(file, Path.Combine(Main.PlayerPath, Path.GetFileName(file)), true);
			}
			string[] directories = Directory.GetDirectories(vanillaPlayersPath);
			for (int i = 0; i < directories.Length; i++)
			{
				IEnumerable<string> mapFiles = from s in Directory.GetFiles(directories[i], "*.*")
				where s.EndsWith(".map") || s.EndsWith(".tmap")
				select s;
				try
				{
					foreach (string mapFile in mapFiles)
					{
						string mapFileDir = Path.Combine(Main.PlayerPath, Directory.GetParent(mapFile).Name);
						Directory.CreateDirectory(mapFileDir);
						File.Copy(mapFile, Path.Combine(mapFileDir, Path.GetFileName(mapFile)), true);
					}
				}
				catch (Exception e)
				{
					Logging.tML.Error(Language.GetText("tModLoader.MigratePlayersException"), e);
				}
			}
			UICharacterSelect._currentlyMigratingFiles = false;
			Main.menuMode = 1;
		}

		// Token: 0x04005520 RID: 21792
		internal UIList _playerList;

		// Token: 0x04005521 RID: 21793
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x04005522 RID: 21794
		private UITextPanel<LocalizedText> _newPanel;

		// Token: 0x04005523 RID: 21795
		private UIPanel _containerPanel;

		// Token: 0x04005524 RID: 21796
		private UIScrollbar _scrollbar;

		// Token: 0x04005525 RID: 21797
		private bool _isScrollbarAttached;

		// Token: 0x04005526 RID: 21798
		private List<Tuple<string, bool>> favoritesCache = new List<Tuple<string, bool>>();

		// Token: 0x04005527 RID: 21799
		private bool skipDraw;

		// Token: 0x04005528 RID: 21800
		private static bool _currentlyMigratingFiles;

		// Token: 0x04005529 RID: 21801
		private static UIExpandablePanel _migrationPanel;

		// Token: 0x0400552A RID: 21802
		private static NestedUIList migratePlayerList;

		// Token: 0x0400552B RID: 21803
		private static bool migratablePlayersLoaded;
	}
}
