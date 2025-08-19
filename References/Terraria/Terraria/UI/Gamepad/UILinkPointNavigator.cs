using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameInput;

namespace Terraria.UI.Gamepad
{
	// Token: 0x020000A5 RID: 165
	public class UILinkPointNavigator
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06001362 RID: 4962 RVA: 0x0049EDC0 File Offset: 0x0049CFC0
		public static int CurrentPoint
		{
			get
			{
				return UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].CurrentPoint;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0049EDD8 File Offset: 0x0049CFD8
		public static bool Available
		{
			get
			{
				return Main.playerInventory || Main.ingameOptionsWindow || Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1 || Main.mapFullscreen || Main.clothesWindow || Main.MenuUI.IsVisible || Main.InGameUI.IsVisible;
			}
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0049EE3F File Offset: 0x0049D03F
		public static void SuggestUsage(int PointID)
		{
			if (!UILinkPointNavigator.Points.ContainsKey(PointID))
			{
				return;
			}
			UILinkPointNavigator._suggestedPointID = new int?(PointID);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0049EE5C File Offset: 0x0049D05C
		public static void ConsumeSuggestion()
		{
			if (UILinkPointNavigator._suggestedPointID == null)
			{
				return;
			}
			int value = UILinkPointNavigator._suggestedPointID.Value;
			UILinkPointNavigator.ClearSuggestion();
			UILinkPointNavigator.CurrentPage = UILinkPointNavigator.Points[value].Page;
			UILinkPointNavigator.OverridePoint = value;
			UILinkPointNavigator.ProcessChanges();
			PlayerInput.Triggers.Current.UsedMovementKey = true;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0049EEB6 File Offset: 0x0049D0B6
		public static void ClearSuggestion()
		{
			UILinkPointNavigator._suggestedPointID = null;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0049EEC4 File Offset: 0x0049D0C4
		public static void GoToDefaultPage(int specialFlag = 0)
		{
			TileEntity tileEntity = Main.LocalPlayer.tileEntityAnchor.GetTileEntity();
			if (Main.MenuUI.IsVisible)
			{
				UILinkPointNavigator.CurrentPage = 1004;
				return;
			}
			if (Main.InGameUI.IsVisible || specialFlag == 1)
			{
				UILinkPointNavigator.CurrentPage = 1004;
				return;
			}
			if (Main.gameMenu)
			{
				UILinkPointNavigator.CurrentPage = 1000;
				return;
			}
			if (Main.ingameOptionsWindow)
			{
				UILinkPointNavigator.CurrentPage = 1001;
				return;
			}
			if (Main.CreativeMenu.Enabled)
			{
				UILinkPointNavigator.CurrentPage = 1005;
				return;
			}
			if (Main.hairWindow)
			{
				UILinkPointNavigator.CurrentPage = 12;
				return;
			}
			if (Main.clothesWindow)
			{
				UILinkPointNavigator.CurrentPage = 15;
				return;
			}
			if (Main.npcShop != 0)
			{
				UILinkPointNavigator.CurrentPage = 13;
				return;
			}
			if (Main.InGuideCraftMenu)
			{
				UILinkPointNavigator.CurrentPage = 0;
				return;
			}
			if (Main.InReforgeMenu)
			{
				UILinkPointNavigator.CurrentPage = 0;
				return;
			}
			if (Main.player[Main.myPlayer].chest != -1)
			{
				UILinkPointNavigator.CurrentPage = 4;
				return;
			}
			if (tileEntity is TEDisplayDoll)
			{
				UILinkPointNavigator.CurrentPage = 20;
				return;
			}
			if (tileEntity is TEHatRack)
			{
				UILinkPointNavigator.CurrentPage = 21;
				return;
			}
			if (Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1)
			{
				UILinkPointNavigator.CurrentPage = 1003;
				return;
			}
			UILinkPointNavigator.CurrentPage = 0;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0049F008 File Offset: 0x0049D208
		public static void Update()
		{
			bool inUse = UILinkPointNavigator.InUse;
			UILinkPointNavigator.InUse = false;
			bool flag = true;
			if (flag)
			{
				InputMode currentInputMode = PlayerInput.CurrentInputMode;
				if (currentInputMode <= InputMode.Mouse && !Main.gameMenu)
				{
					flag = false;
				}
			}
			if (flag && PlayerInput.NavigatorRebindingLock > 0)
			{
				flag = false;
			}
			if (flag && !Main.gameMenu && !PlayerInput.UsingGamepadUI)
			{
				flag = false;
			}
			if (flag && !Main.gameMenu && PlayerInput.InBuildingMode)
			{
				flag = false;
			}
			if (flag && !Main.gameMenu && !UILinkPointNavigator.Available)
			{
				flag = false;
			}
			bool flag2 = false;
			UILinkPage uilinkPage;
			if (!UILinkPointNavigator.Pages.TryGetValue(UILinkPointNavigator.CurrentPage, out uilinkPage))
			{
				flag2 = true;
			}
			else if (!uilinkPage.IsValid())
			{
				flag2 = true;
			}
			if (flag2)
			{
				UILinkPointNavigator.GoToDefaultPage(0);
				UILinkPointNavigator.ProcessChanges();
				flag = false;
			}
			if (inUse != flag)
			{
				if (!flag)
				{
					uilinkPage.Leave();
					UILinkPointNavigator.GoToDefaultPage(0);
					UILinkPointNavigator.ProcessChanges();
				}
				else
				{
					UILinkPointNavigator.GoToDefaultPage(0);
					UILinkPointNavigator.ProcessChanges();
					UILinkPointNavigator.ConsumeSuggestion();
					uilinkPage.Enter();
				}
				if (flag)
				{
					if (!PlayerInput.SteamDeckIsUsed || PlayerInput.PreventCursorModeSwappingToGamepad)
					{
						Main.player[Main.myPlayer].releaseInventory = false;
					}
					Main.player[Main.myPlayer].releaseUseTile = false;
					PlayerInput.LockGamepadTileUseButton = true;
				}
				if (!Main.gameMenu)
				{
					if (flag)
					{
						PlayerInput.NavigatorCachePosition();
					}
					else
					{
						PlayerInput.NavigatorUnCachePosition();
					}
				}
			}
			UILinkPointNavigator.ClearSuggestion();
			if (!flag)
			{
				return;
			}
			UILinkPointNavigator.InUse = true;
			UILinkPointNavigator.OverridePoint = -1;
			if (UILinkPointNavigator.PageLeftCD > 0)
			{
				UILinkPointNavigator.PageLeftCD--;
			}
			if (UILinkPointNavigator.PageRightCD > 0)
			{
				UILinkPointNavigator.PageRightCD--;
			}
			Vector2 navigatorDirections = PlayerInput.Triggers.Current.GetNavigatorDirections();
			object obj = PlayerInput.Triggers.Current.HotbarMinus && !PlayerInput.Triggers.Current.HotbarPlus;
			bool flag3 = PlayerInput.Triggers.Current.HotbarPlus && !PlayerInput.Triggers.Current.HotbarMinus;
			object obj2 = obj;
			if (obj2 == null)
			{
				UILinkPointNavigator.PageLeftCD = 0;
			}
			if (!flag3)
			{
				UILinkPointNavigator.PageRightCD = 0;
			}
			object obj3 = obj2 != null && UILinkPointNavigator.PageLeftCD == 0;
			flag3 = (flag3 && UILinkPointNavigator.PageRightCD == 0);
			if (UILinkPointNavigator.LastInput.X != navigatorDirections.X)
			{
				UILinkPointNavigator.XCooldown = 0;
			}
			if (UILinkPointNavigator.LastInput.Y != navigatorDirections.Y)
			{
				UILinkPointNavigator.YCooldown = 0;
			}
			if (UILinkPointNavigator.XCooldown > 0)
			{
				UILinkPointNavigator.XCooldown--;
			}
			if (UILinkPointNavigator.YCooldown > 0)
			{
				UILinkPointNavigator.YCooldown--;
			}
			UILinkPointNavigator.LastInput = navigatorDirections;
			object obj4 = obj3;
			if (obj4 != null)
			{
				UILinkPointNavigator.PageLeftCD = 16;
			}
			if (flag3)
			{
				UILinkPointNavigator.PageRightCD = 16;
			}
			UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].Update();
			int num = 10;
			if (!Main.gameMenu && Main.playerInventory && !Main.ingameOptionsWindow && !Main.inFancyUI && (UILinkPointNavigator.CurrentPage == 0 || UILinkPointNavigator.CurrentPage == 4 || UILinkPointNavigator.CurrentPage == 2 || UILinkPointNavigator.CurrentPage == 1 || UILinkPointNavigator.CurrentPage == 20 || UILinkPointNavigator.CurrentPage == 21))
			{
				num = PlayerInput.CurrentProfile.InventoryMoveCD;
			}
			if (navigatorDirections.X == -1f && UILinkPointNavigator.XCooldown == 0)
			{
				UILinkPointNavigator.XCooldown = num;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelLeft();
			}
			if (navigatorDirections.X == 1f && UILinkPointNavigator.XCooldown == 0)
			{
				UILinkPointNavigator.XCooldown = num;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelRight();
			}
			if (navigatorDirections.Y == -1f && UILinkPointNavigator.YCooldown == 0)
			{
				UILinkPointNavigator.YCooldown = num;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelUp();
			}
			if (navigatorDirections.Y == 1f && UILinkPointNavigator.YCooldown == 0)
			{
				UILinkPointNavigator.YCooldown = num;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelDown();
			}
			UILinkPointNavigator.XCooldown = (UILinkPointNavigator.YCooldown = Math.Max(UILinkPointNavigator.XCooldown, UILinkPointNavigator.YCooldown));
			if (obj4 != null)
			{
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].SwapPageLeft();
			}
			if (flag3)
			{
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].SwapPageRight();
			}
			if (PlayerInput.Triggers.Current.UsedMovementKey)
			{
				Vector2 position = UILinkPointNavigator.Points[UILinkPointNavigator.CurrentPoint].Position;
				Vector2 value = new Vector2((float)PlayerInput.MouseX, (float)PlayerInput.MouseY);
				float amount = 0.3f;
				if (PlayerInput.InvisibleGamepadInMenus)
				{
					amount = 1f;
				}
				Vector2 vector = Vector2.Lerp(value, position, amount);
				if (Main.gameMenu)
				{
					if (Math.Abs(vector.X - position.X) <= 5f)
					{
						vector.X = position.X;
					}
					if (Math.Abs(vector.Y - position.Y) <= 5f)
					{
						vector.Y = position.Y;
					}
				}
				PlayerInput.MouseX = (int)vector.X;
				PlayerInput.MouseY = (int)vector.Y;
			}
			UILinkPointNavigator.ResetFlagsEnd();
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0049F4C2 File Offset: 0x0049D6C2
		public static void ResetFlagsEnd()
		{
			UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 0;
			UILinkPointNavigator.Shortcuts.BackButtonLock = false;
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 0;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0049F4D8 File Offset: 0x0049D6D8
		public static string GetInstructions()
		{
			UILinkPage uilinkPage = UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage];
			UILinkPoint uilinkPoint = UILinkPointNavigator.Points[UILinkPointNavigator.CurrentPoint];
			if (UILinkPointNavigator._suggestedPointID != null)
			{
				UILinkPointNavigator.SwapToSuggestion();
				uilinkPoint = UILinkPointNavigator.Points[UILinkPointNavigator._suggestedPointID.Value];
				uilinkPage = UILinkPointNavigator.Pages[uilinkPoint.Page];
				UILinkPointNavigator.CurrentPage = uilinkPage.ID;
				uilinkPage.CurrentPoint = UILinkPointNavigator._suggestedPointID.Value;
			}
			string text = uilinkPage.SpecialInteractions();
			if ((PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Gamepad && PlayerInput.Triggers.Current.UsedMovementKey && UILinkPointNavigator.InUse) || UILinkPointNavigator._suggestedPointID != null)
			{
				string text2 = uilinkPoint.SpecialInteractions();
				if (!string.IsNullOrEmpty(text2))
				{
					if (string.IsNullOrEmpty(text))
					{
						return text2;
					}
					text = text + "   " + text2;
				}
			}
			UILinkPointNavigator.ConsumeSuggestionSwap();
			return text;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0049F5B9 File Offset: 0x0049D7B9
		public static void SwapToSuggestion()
		{
			UILinkPointNavigator._preSuggestionPoint = new int?(UILinkPointNavigator.CurrentPoint);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0049F5CC File Offset: 0x0049D7CC
		public static void ConsumeSuggestionSwap()
		{
			if (UILinkPointNavigator._preSuggestionPoint != null)
			{
				int value = UILinkPointNavigator._preSuggestionPoint.Value;
				UILinkPointNavigator.CurrentPage = UILinkPointNavigator.Points[value].Page;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].CurrentPoint = value;
			}
			UILinkPointNavigator._preSuggestionPoint = null;
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0049F625 File Offset: 0x0049D825
		public static void ForceMovementCooldown(int time)
		{
			UILinkPointNavigator.LastInput = PlayerInput.Triggers.Current.GetNavigatorDirections();
			UILinkPointNavigator.XCooldown = time;
			UILinkPointNavigator.YCooldown = time;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0049F647 File Offset: 0x0049D847
		public static void SetPosition(int ID, Vector2 Position)
		{
			UILinkPointNavigator.Points[ID].Position = Position * Main.UIScale;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0049F664 File Offset: 0x0049D864
		public static void RegisterPage(UILinkPage page, int ID, bool automatedDefault = true)
		{
			if (automatedDefault)
			{
				page.DefaultPoint = page.LinkMap.Keys.First<int>();
			}
			page.CurrentPoint = page.DefaultPoint;
			page.ID = ID;
			UILinkPointNavigator.Pages.Add(page.ID, page);
			foreach (KeyValuePair<int, UILinkPoint> keyValuePair in page.LinkMap)
			{
				keyValuePair.Value.SetPage(ID);
				UILinkPointNavigator.Points.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0049F714 File Offset: 0x0049D914
		public static void ChangePage(int PageID)
		{
			if (UILinkPointNavigator.Pages.ContainsKey(PageID) && UILinkPointNavigator.Pages[PageID].CanEnter())
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				UILinkPointNavigator.CurrentPage = PageID;
				UILinkPointNavigator.ProcessChanges();
			}
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0049F760 File Offset: 0x0049D960
		public static void ChangePoint(int PointID)
		{
			if (UILinkPointNavigator.Points.ContainsKey(PointID))
			{
				UILinkPointNavigator.CurrentPage = UILinkPointNavigator.Points[PointID].Page;
				UILinkPointNavigator.OverridePoint = PointID;
				UILinkPointNavigator.ProcessChanges();
			}
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0049F790 File Offset: 0x0049D990
		public static void ProcessChanges()
		{
			UILinkPage uilinkPage = UILinkPointNavigator.Pages[UILinkPointNavigator.OldPage];
			if (UILinkPointNavigator.OldPage != UILinkPointNavigator.CurrentPage)
			{
				uilinkPage.Leave();
				if (!UILinkPointNavigator.Pages.TryGetValue(UILinkPointNavigator.CurrentPage, out uilinkPage))
				{
					UILinkPointNavigator.GoToDefaultPage(0);
					UILinkPointNavigator.ProcessChanges();
					UILinkPointNavigator.OverridePoint = -1;
				}
				uilinkPage.CurrentPoint = uilinkPage.DefaultPoint;
				uilinkPage.Enter();
				uilinkPage.Update();
				UILinkPointNavigator.OldPage = UILinkPointNavigator.CurrentPage;
			}
			if (UILinkPointNavigator.OverridePoint != -1 && uilinkPage.LinkMap.ContainsKey(UILinkPointNavigator.OverridePoint))
			{
				uilinkPage.CurrentPoint = UILinkPointNavigator.OverridePoint;
			}
		}

		// Token: 0x0400119C RID: 4508
		public static Dictionary<int, UILinkPage> Pages = new Dictionary<int, UILinkPage>();

		// Token: 0x0400119D RID: 4509
		public static Dictionary<int, UILinkPoint> Points = new Dictionary<int, UILinkPoint>();

		// Token: 0x0400119E RID: 4510
		public static int CurrentPage = 1000;

		// Token: 0x0400119F RID: 4511
		public static int OldPage = 1000;

		// Token: 0x040011A0 RID: 4512
		private static int XCooldown;

		// Token: 0x040011A1 RID: 4513
		private static int YCooldown;

		// Token: 0x040011A2 RID: 4514
		private static Vector2 LastInput;

		// Token: 0x040011A3 RID: 4515
		private static int PageLeftCD;

		// Token: 0x040011A4 RID: 4516
		private static int PageRightCD;

		// Token: 0x040011A5 RID: 4517
		public static bool InUse;

		// Token: 0x040011A6 RID: 4518
		public static int OverridePoint = -1;

		// Token: 0x040011A7 RID: 4519
		private static int? _suggestedPointID;

		// Token: 0x040011A8 RID: 4520
		private static int? _preSuggestionPoint;

		// Token: 0x02000550 RID: 1360
		public static class Shortcuts
		{
			// Token: 0x040058AC RID: 22700
			public static int NPCS_IconsPerColumn = 100;

			// Token: 0x040058AD RID: 22701
			public static int NPCS_IconsTotal = 0;

			// Token: 0x040058AE RID: 22702
			public static int NPCS_LastHovered = -2;

			// Token: 0x040058AF RID: 22703
			public static bool NPCS_IconsDisplay = false;

			// Token: 0x040058B0 RID: 22704
			public static int CRAFT_IconsPerRow = 100;

			// Token: 0x040058B1 RID: 22705
			public static int CRAFT_IconsPerColumn = 100;

			// Token: 0x040058B2 RID: 22706
			public static int CRAFT_CurrentIngredientsCount = 0;

			// Token: 0x040058B3 RID: 22707
			public static int CRAFT_CurrentRecipeBig = 0;

			// Token: 0x040058B4 RID: 22708
			public static int CRAFT_CurrentRecipeSmall = 0;

			// Token: 0x040058B5 RID: 22709
			public static bool NPCCHAT_ButtonsLeft = false;

			// Token: 0x040058B6 RID: 22710
			public static bool NPCCHAT_ButtonsMiddle = false;

			// Token: 0x040058B7 RID: 22711
			public static bool NPCCHAT_ButtonsRight = false;

			// Token: 0x040058B8 RID: 22712
			public static bool NPCCHAT_ButtonsRight2 = false;

			// Token: 0x040058B9 RID: 22713
			public static int INGAMEOPTIONS_BUTTONS_LEFT = 0;

			// Token: 0x040058BA RID: 22714
			public static int INGAMEOPTIONS_BUTTONS_RIGHT = 0;

			// Token: 0x040058BB RID: 22715
			public static bool CREATIVE_ItemSlotShouldHighlightAsSelected = false;

			// Token: 0x040058BC RID: 22716
			public static int OPTIONS_BUTTON_SPECIALFEATURE;

			// Token: 0x040058BD RID: 22717
			public static int BackButtonCommand;

			// Token: 0x040058BE RID: 22718
			public static bool BackButtonInUse = false;

			// Token: 0x040058BF RID: 22719
			public static bool BackButtonLock;

			// Token: 0x040058C0 RID: 22720
			public static int FANCYUI_HIGHEST_INDEX = 1;

			// Token: 0x040058C1 RID: 22721
			public static int FANCYUI_SPECIAL_INSTRUCTIONS = 0;

			// Token: 0x040058C2 RID: 22722
			public static int INFOACCCOUNT = 0;

			// Token: 0x040058C3 RID: 22723
			public static int BUILDERACCCOUNT = 0;

			// Token: 0x040058C4 RID: 22724
			public static int BUFFS_PER_COLUMN = 0;

			// Token: 0x040058C5 RID: 22725
			public static int BUFFS_DRAWN = 0;

			// Token: 0x040058C6 RID: 22726
			public static int INV_MOVE_OPTION_CD = 0;
		}
	}
}
