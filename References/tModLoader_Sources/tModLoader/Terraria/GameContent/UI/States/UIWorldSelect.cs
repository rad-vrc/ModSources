using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
	// Token: 0x020004E8 RID: 1256
	public class UIWorldSelect : UIState
	{
		// Token: 0x06003CF5 RID: 15605 RVA: 0x005C5FC4 File Offset: 0x005C41C4
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
			uIElement.Append(uIPanel);
			this._containerPanel = uIPanel;
			this._worldList = new UIList();
			this._worldList.Width.Set(0f, 1f);
			this._worldList.Height.Set(0f, 1f);
			this._worldList.ListPadding = 5f;
			uIPanel.Append(this._worldList);
			this._scrollbar = new UIScrollbar();
			this._scrollbar.SetView(100f, 1000f);
			this._scrollbar.Height.Set(0f, 1f);
			this._scrollbar.HAlign = 1f;
			this._worldList.SetScrollbar(this._scrollbar);
			UITextPanel<LocalizedText> uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.SelectWorld"), 0.8f, true);
			uITextPanel.HAlign = 0.5f;
			uITextPanel.Top.Set(-40f, 0f);
			uITextPanel.SetPadding(15f);
			uITextPanel.BackgroundColor = new Color(73, 94, 171);
			uIElement.Append(uITextPanel);
			UITextPanel<LocalizedText> uITextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, true);
			uITextPanel2.Width.Set(-10f, 0.5f);
			uITextPanel2.Height.Set(50f, 0f);
			uITextPanel2.VAlign = 1f;
			uITextPanel2.HAlign = 0f;
			uITextPanel2.Top.Set(-45f, 0f);
			uITextPanel2.OnMouseOver += this.FadedMouseOver;
			uITextPanel2.OnMouseOut += this.FadedMouseOut;
			uITextPanel2.OnLeftClick += this.GoBackClick;
			uIElement.Append(uITextPanel2);
			this._backPanel = uITextPanel2;
			UITextPanel<LocalizedText> uITextPanel3 = new UITextPanel<LocalizedText>(Language.GetText("UI.New"), 0.7f, true);
			uITextPanel3.CopyStyle(uITextPanel2);
			uITextPanel3.HAlign = 1f;
			uITextPanel3.OnMouseOver += this.FadedMouseOver;
			uITextPanel3.OnMouseOut += this.FadedMouseOut;
			uITextPanel3.OnLeftClick += this.NewWorldClick;
			uIElement.Append(uITextPanel3);
			this._newPanel = uITextPanel3;
			base.Append(uIElement);
			this.InitializeMigrationPanel();
		}

		// Token: 0x06003CF6 RID: 15606 RVA: 0x005C62D4 File Offset: 0x005C44D4
		public override void Recalculate()
		{
			if (this._scrollbar != null)
			{
				if (this._isScrollbarAttached && !this._scrollbar.CanScroll)
				{
					this._containerPanel.RemoveChild(this._scrollbar);
					this._isScrollbarAttached = false;
					this._worldList.Width.Set(0f, 1f);
				}
				else if (!this._isScrollbarAttached && this._scrollbar.CanScroll)
				{
					this._containerPanel.Append(this._scrollbar);
					this._isScrollbarAttached = true;
					this._worldList.Width.Set(-25f, 1f);
				}
			}
			base.Recalculate();
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x005C6384 File Offset: 0x005C4584
		private void NewWorldClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(10, -1, -1, 1, 1f, 0f);
			Main.newWorldName = Lang.gen[57].Value + " " + (Main.WorldList.Count + 1).ToString();
			Main.menuMode = 888;
			Main.MenuUI.SetState(new UIWorldCreation());
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x005C63EF File Offset: 0x005C45EF
		private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
			Main.menuMode = ((!Main.menuMultiplayer) ? 1 : 12);
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x005C6418 File Offset: 0x005C4618
		private void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
		{
			SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
			((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
			((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x005C646D File Offset: 0x005C466D
		private void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
		{
			((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.7f;
			((UIPanel)evt.Target).BorderColor = Color.Black;
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x005C64AC File Offset: 0x005C46AC
		public override void OnActivate()
		{
			Main.LoadWorlds();
			this.ActivateMigrationPanel();
			this.UpdateWorldsList();
			if (PlayerInput.UsingGamepadUI)
			{
				UILinkPointNavigator.ChangePoint(3000 + ((this._worldList.Count == 0) ? 1 : 2));
			}
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x005C64E4 File Offset: 0x005C46E4
		private void UpdateWorldsList()
		{
			this._worldList.Clear();
			IEnumerable<WorldFileData> source = new List<WorldFileData>(Main.WorldList);
			Func<WorldFileData, bool> keySelector;
			if ((keySelector = UIWorldSelect.<>O.<0>__CanWorldBePlayed) == null)
			{
				keySelector = (UIWorldSelect.<>O.<0>__CanWorldBePlayed = new Func<WorldFileData, bool>(UIWorldSelect.CanWorldBePlayed));
			}
			IOrderedEnumerable<WorldFileData> orderedEnumerable = source.OrderByDescending(keySelector).ThenByDescending((WorldFileData x) => x.IsFavorite).ThenBy((WorldFileData x) => x.Name).ThenBy((WorldFileData x) => x.GetFileName(true));
			int num = 0;
			foreach (WorldFileData item in orderedEnumerable)
			{
				this._worldList.Add(new UIWorldListItem(item, num++, UIWorldSelect.CanWorldBePlayed(item)));
			}
			this._worldList.Add(UIWorldSelect._migrationPanel);
			if (!orderedEnumerable.Any<WorldFileData>())
			{
				this.AddAutomaticWorldMigrationButtons();
			}
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x005C6604 File Offset: 0x005C4804
		internal static bool CanWorldBePlayed(WorldFileData file)
		{
			bool flag2 = Main.ActivePlayerFileData.Player.difficulty == 3;
			bool flag = file.GameMode == 3;
			ModSystem modSystem;
			return flag2 == flag && SystemLoader.CanWorldBePlayed(Main.ActivePlayerFileData, file, out modSystem);
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x005C663F File Offset: 0x005C483F
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

		// Token: 0x06003CFF RID: 15615 RVA: 0x005C6680 File Offset: 0x005C4880
		private bool UpdateFavoritesCache()
		{
			List<WorldFileData> list = new List<WorldFileData>(Main.WorldList);
			list.Sort(delegate(WorldFileData x, WorldFileData y)
			{
				if (x.IsFavorite && !y.IsFavorite)
				{
					return -1;
				}
				if (!x.IsFavorite && y.IsFavorite)
				{
					return 1;
				}
				if (x.Name == null)
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
				foreach (WorldFileData item in list)
				{
					this.favoritesCache.Add(Tuple.Create<string, bool>(item.Name, item.IsFavorite));
				}
				this.UpdateWorldsList();
			}
			return flag;
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x005C67A0 File Offset: 0x005C49A0
		private void SetupGamepadPoints(SpriteBatch spriteBatch)
		{
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 2;
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
			SnapPoint[,] array = new SnapPoint[this._worldList.Count, 6];
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
			where a.Name == "Seed"
			select a)
			{
				array[item4.Id, 3] = item4;
			}
			foreach (SnapPoint item5 in from a in snapPoints
			where a.Name == "Rename"
			select a)
			{
				array[item5.Id, 4] = item5;
			}
			foreach (SnapPoint item6 in from a in snapPoints
			where a.Name == "Delete"
			select a)
			{
				array[item6.Id, 5] = item6;
			}
			num2 = num + 2;
			int[] array2 = new int[this._worldList.Count];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = -1;
			}
			for (int k = 0; k < array.GetLength(1); k++)
			{
				int num4 = -1;
				for (int l = 0; l < array.GetLength(0); l++)
				{
					if (array[l, k] != null)
					{
						uILinkPoint = UILinkPointNavigator.Points[num2];
						uILinkPoint.Unlink();
						UILinkPointNavigator.SetPosition(num2, array[l, k].Position);
						if (num4 != -1)
						{
							uILinkPoint.Up = num4;
							UILinkPointNavigator.Points[num4].Down = num2;
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
						num4 = num2;
						array2[l] = num2;
						UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = num2;
						num2++;
					}
				}
			}
			if (PlayerInput.UsingGamepadUI && this._worldList.Count == 0 && UILinkPointNavigator.CurrentPoint > 3001)
			{
				UILinkPointNavigator.ChangePoint(3001);
			}
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x005C6CC4 File Offset: 0x005C4EC4
		private void InitializeMigrationPanel()
		{
			UIWorldSelect._migrationPanel = new UIExpandablePanel();
			UIWorldSelect._migrationPanel.OnExpanded += this._migrationPanel_OnExpanded;
			UIText playerMigrationPanelTitle = new UIText(Language.GetTextValue("tModLoader.MigrateIndividualWorldsHeader"), 1f, false);
			playerMigrationPanelTitle.Top.Set(4f, 0f);
			UIWorldSelect._migrationPanel.Append(playerMigrationPanelTitle);
			UIWorldSelect.migrateWorldList = new NestedUIList();
			UIWorldSelect.migrateWorldList.Width.Set(-22f, 1f);
			UIWorldSelect.migrateWorldList.Left.Set(0f, 0f);
			UIWorldSelect.migrateWorldList.Top.Set(30f, 0f);
			UIWorldSelect.migrateWorldList.MinHeight.Set(300f, 0f);
			UIWorldSelect.migrateWorldList.ListPadding = 5f;
			UIWorldSelect._migrationPanel.VisibleWhenExpanded.Add(UIWorldSelect.migrateWorldList);
			UIScrollbar scrollbar = new UIScrollbar();
			scrollbar.SetView(100f, 1000f);
			scrollbar.Height.Set(-42f, 1f);
			scrollbar.Top.Set(36f, 0f);
			UIScrollbar uiscrollbar = scrollbar;
			uiscrollbar.Left.Pixels = uiscrollbar.Left.Pixels - 0f;
			scrollbar.HAlign = 1f;
			UIWorldSelect.migrateWorldList.SetScrollbar(scrollbar);
			UIWorldSelect._migrationPanel.VisibleWhenExpanded.Add(scrollbar);
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x005C6E32 File Offset: 0x005C5032
		private void ActivateMigrationPanel()
		{
			UIWorldSelect.migrateWorldList.Clear();
			UIWorldSelect.migratableWorldsLoaded = false;
			UIWorldSelect._migrationPanel.Collapse();
		}

		// Token: 0x06003D03 RID: 15619 RVA: 0x005C6E4E File Offset: 0x005C504E
		private void _migrationPanel_OnExpanded()
		{
			if (UIWorldSelect.migratableWorldsLoaded)
			{
				return;
			}
			UIWorldSelect.migratableWorldsLoaded = true;
			this.LoadMigratableWorlds();
		}

		// Token: 0x06003D04 RID: 15620 RVA: 0x005C6E64 File Offset: 0x005C5064
		private void LoadMigratableWorlds()
		{
			ValueTuple<string, string, int>[] otherPaths = FileUtilities.GetAlternateSavePathFiles("Worlds");
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
					string[] files = Directory.GetFiles(otherSaveFolderPath, "*.wld");
					int num2 = Math.Min(1000, files.Length);
					for (int i = 0; i < num2; i++)
					{
						string worldInThisWorldsPath = Path.Combine(Main.WorldPath, Path.GetFileName(files[i]));
						if (!File.Exists(worldInThisWorldsPath) || !(File.GetLastWriteTime(worldInThisWorldsPath) == File.GetLastWriteTime(files[i])))
						{
							WorldFileData fileData = WorldFile.GetAllMetadata(files[i], false);
							if (fileData != null)
							{
								UIPanel migrateIndividualWorldPanel = new UIPanel();
								migrateIndividualWorldPanel.Width.Set(0f, 1f);
								migrateIndividualWorldPanel.Height.Set(50f, 0f);
								float left = 0f;
								if (stabilityLevel > currentStabilityLevel)
								{
									UIHoverImage uihoverImage = new UIHoverImage(UICommon.ButtonErrorTexture, Language.GetTextValue("tModLoader.WorldFromNewerTModMightNotWork"));
									uihoverImage.UseTooltipMouseText = true;
									uihoverImage.Left.Pixels = left;
									uihoverImage.Top.Pixels = 3f;
									UIHoverImage warningImage = uihoverImage;
									migrateIndividualWorldPanel.Append(warningImage);
									left += warningImage.Width.Pixels + 6f;
								}
								WorldFileData worldWithSameName = Main.WorldList.FirstOrDefault((WorldFileData x) => x.Name == fileData.Name);
								if (worldWithSameName != null)
								{
									UIHoverImage uihoverImage2 = new UIHoverImage(UICommon.ButtonExclamationTexture, Language.GetTextValue("tModLoader.WorldWithThisNameExistsWillBeOverwritten"));
									uihoverImage2.UseTooltipMouseText = true;
									uihoverImage2.Left.Pixels = left;
									uihoverImage2.Top.Pixels = 3f;
									UIHoverImage warningImage2 = uihoverImage2;
									migrateIndividualWorldPanel.Append(warningImage2);
									left += warningImage2.Width.Pixels + 6f;
									if (File.GetLastWriteTime(worldWithSameName.Path) > File.GetLastWriteTime(files[i]))
									{
										UIHoverImage uihoverImage3 = new UIHoverImage(UICommon.ButtonExclamationTexture, Language.GetTextValue("tModLoader.ExistingWorldPlayedMoreRecently"));
										uihoverImage3.UseTooltipMouseText = true;
										uihoverImage3.Left.Pixels = left;
										uihoverImage3.Top.Pixels = 3f;
										warningImage2 = uihoverImage3;
										migrateIndividualWorldPanel.Append(warningImage2);
										left += warningImage2.Width.Pixels + 6f;
									}
								}
								UIText migrateIndividualWorldText = new UIText(string.Format(message, fileData.Name), 1f, false);
								migrateIndividualWorldText.Width.Set(-left, 1f);
								migrateIndividualWorldText.Left.Set(left, 0f);
								migrateIndividualWorldText.Height.Set(0f, 1f);
								Action <>9__2;
								migrateIndividualWorldText.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
								{
									if (UIWorldSelect._currentlyMigratingFiles)
									{
										return;
									}
									UIWorldSelect._currentlyMigratingFiles = true;
									migrateIndividualWorldText.SetText(Language.GetText("tModLoader.MigratingWorldsText"));
									TaskFactory factory = Task.Factory;
									Action action;
									if ((action = <>9__2) == null)
									{
										action = (<>9__2 = delegate()
										{
											UIWorldSelect.ExecuteIndividualWorldMigration(fileData, otherSaveFolderPath);
										});
									}
									factory.StartNew(action, TaskCreationOptions.PreferFairness);
								};
								migrateIndividualWorldPanel.Append(migrateIndividualWorldText);
								UIWorldSelect.migrateWorldList.Add(migrateIndividualWorldPanel);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003D05 RID: 15621 RVA: 0x005C71C8 File Offset: 0x005C53C8
		private static void ExecuteIndividualWorldMigration(WorldFileData fileData, string otherSaveFolderPath)
		{
			try
			{
				string worldFileName = Path.GetFileNameWithoutExtension(fileData.Path);
				foreach (string path in from s in Directory.GetFiles(Main.WorldPath, worldFileName + ".*")
				where s.EndsWith(".wld") || s.EndsWith(".twld") || s.EndsWith(".bak")
				select s)
				{
					File.Delete(path);
				}
				foreach (string otherWorldFile in from s in Directory.GetFiles(otherSaveFolderPath, worldFileName + ".*")
				where s.EndsWith(".wld") || s.EndsWith(".twld") || s.EndsWith(".bak")
				select s)
				{
					File.Copy(otherWorldFile, Path.Combine(Main.WorldPath, Path.GetFileName(otherWorldFile)), true);
				}
			}
			catch (Exception e)
			{
				Logging.tML.Error(Language.GetText("tModLoader.MigratePlayersException"), e);
			}
			UIWorldSelect._currentlyMigratingFiles = false;
			Main.menuMode = 6;
		}

		// Token: 0x06003D06 RID: 15622 RVA: 0x005C7300 File Offset: 0x005C5500
		private void AddAutomaticWorldMigrationButtons()
		{
			string vanillaWorldsPath = Path.Combine(Platform.Get<IPathService>().GetStoragePath("Terraria"), "Worlds");
			if (!Directory.Exists(vanillaWorldsPath) || !Directory.GetFiles(vanillaWorldsPath, "*.wld").Any<string>())
			{
				return;
			}
			UIPanel autoMigrateButton = new UIPanel();
			autoMigrateButton.Width.Set(0f, 1f);
			autoMigrateButton.Height.Set(50f, 0f);
			UIText migrateText = new UIText((!UIWorldSelect._currentlyMigratingFiles) ? Language.GetText("tModLoader.MigrateWorldsText") : Language.GetText("tModLoader.MigratingWorldsText"), 1f, false);
			Action <>9__2;
			autoMigrateButton.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				if (UIWorldSelect._currentlyMigratingFiles)
				{
					return;
				}
				UIWorldSelect._currentlyMigratingFiles = true;
				migrateText.SetText(Language.GetText("tModLoader.MigratingWorldsText"));
				TaskFactory factory = Task.Factory;
				Action action;
				if ((action = <>9__2) == null)
				{
					action = (<>9__2 = delegate()
					{
						UIWorldSelect.ExecuteAutomaticWorldMigration(vanillaWorldsPath);
					});
				}
				factory.StartNew(action, TaskCreationOptions.PreferFairness);
			};
			autoMigrateButton.Append(migrateText);
			this._worldList.Add(autoMigrateButton);
			UIText noWorldsMessage = new UIText(Language.GetTextValue("tModLoader.MigrateWorldsMessage", Program.SaveFolderName), 1f, false);
			noWorldsMessage.Width.Set(0f, 1f);
			noWorldsMessage.Height.Set(300f, 0f);
			noWorldsMessage.MarginTop = 20f;
			noWorldsMessage.OnLeftClick += delegate(UIMouseEvent a, UIElement b)
			{
				Utils.OpenFolder(Main.WorldPath);
				Utils.OpenFolder(vanillaWorldsPath);
			};
			this._worldList.Add(noWorldsMessage);
		}

		// Token: 0x06003D07 RID: 15623 RVA: 0x005C7450 File Offset: 0x005C5650
		private static void ExecuteAutomaticWorldMigration(string vanillaWorldsPath)
		{
			foreach (string file in from s in Directory.GetFiles(vanillaWorldsPath, "*.*")
			where s.EndsWith(".wld") || s.EndsWith(".twld") || s.EndsWith(".bak")
			select s)
			{
				File.Copy(file, Path.Combine(Main.WorldPath, Path.GetFileName(file)), true);
			}
			UIWorldSelect._currentlyMigratingFiles = false;
			Main.menuMode = 6;
		}

		// Token: 0x040055EB RID: 21995
		private UIList _worldList;

		// Token: 0x040055EC RID: 21996
		private UITextPanel<LocalizedText> _backPanel;

		// Token: 0x040055ED RID: 21997
		private UITextPanel<LocalizedText> _newPanel;

		// Token: 0x040055EE RID: 21998
		private UITextPanel<LocalizedText> _workshopPanel;

		// Token: 0x040055EF RID: 21999
		private UIPanel _containerPanel;

		// Token: 0x040055F0 RID: 22000
		private UIScrollbar _scrollbar;

		// Token: 0x040055F1 RID: 22001
		private bool _isScrollbarAttached;

		// Token: 0x040055F2 RID: 22002
		private List<Tuple<string, bool>> favoritesCache = new List<Tuple<string, bool>>();

		// Token: 0x040055F3 RID: 22003
		private bool skipDraw;

		// Token: 0x040055F4 RID: 22004
		private static bool _currentlyMigratingFiles;

		// Token: 0x040055F5 RID: 22005
		private static UIExpandablePanel _migrationPanel;

		// Token: 0x040055F6 RID: 22006
		private static NestedUIList migrateWorldList;

		// Token: 0x040055F7 RID: 22007
		private static bool migratableWorldsLoaded;

		// Token: 0x02000C04 RID: 3076
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400782E RID: 30766
			public static Func<WorldFileData, bool> <0>__CanWorldBePlayed;
		}
	}
}
