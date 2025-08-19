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
	// Token: 0x020000BF RID: 191
	public class UILinkPointNavigator
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x0600165D RID: 5725 RVA: 0x004B35EF File Offset: 0x004B17EF
		public static int CurrentPoint
		{
			get
			{
				return UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].CurrentPoint;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x0600165E RID: 5726 RVA: 0x004B3608 File Offset: 0x004B1808
		public static bool Available
		{
			get
			{
				return Main.playerInventory || Main.ingameOptionsWindow || Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1 || Main.mapFullscreen || Main.clothesWindow || Main.MenuUI.IsVisible || Main.InGameUI.IsVisible;
			}
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x004B366F File Offset: 0x004B186F
		public static void SuggestUsage(int PointID)
		{
			if (UILinkPointNavigator.Points.ContainsKey(PointID))
			{
				UILinkPointNavigator._suggestedPointID = new int?(PointID);
			}
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x004B368C File Offset: 0x004B188C
		public static void ConsumeSuggestion()
		{
			if (UILinkPointNavigator._suggestedPointID != null)
			{
				int value = UILinkPointNavigator._suggestedPointID.Value;
				UILinkPointNavigator.ClearSuggestion();
				UILinkPointNavigator.CurrentPage = UILinkPointNavigator.Points[value].Page;
				UILinkPointNavigator.OverridePoint = value;
				UILinkPointNavigator.ProcessChanges();
				PlayerInput.Triggers.Current.UsedMovementKey = true;
			}
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x004B36E5 File Offset: 0x004B18E5
		public static void ClearSuggestion()
		{
			UILinkPointNavigator._suggestedPointID = null;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x004B36F4 File Offset: 0x004B18F4
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

		// Token: 0x06001663 RID: 5731 RVA: 0x004B3838 File Offset: 0x004B1A38
		public static void Update()
		{
			bool inUse = UILinkPointNavigator.InUse;
			UILinkPointNavigator.InUse = false;
			bool flag = true;
			if (flag && PlayerInput.CurrentInputMode <= InputMode.Mouse && !Main.gameMenu)
			{
				flag = false;
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
			UILinkPage value;
			if (!UILinkPointNavigator.Pages.TryGetValue(UILinkPointNavigator.CurrentPage, out value))
			{
				flag2 = true;
			}
			else if (!value.IsValid())
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
					value.Leave();
					UILinkPointNavigator.GoToDefaultPage(0);
					UILinkPointNavigator.ProcessChanges();
				}
				else
				{
					UILinkPointNavigator.GoToDefaultPage(0);
					UILinkPointNavigator.ProcessChanges();
					UILinkPointNavigator.ConsumeSuggestion();
					value.Enter();
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
			int num3 = 10;
			if (!Main.gameMenu && Main.playerInventory && !Main.ingameOptionsWindow && !Main.inFancyUI && (UILinkPointNavigator.CurrentPage == 0 || UILinkPointNavigator.CurrentPage == 4 || UILinkPointNavigator.CurrentPage == 2 || UILinkPointNavigator.CurrentPage == 1 || UILinkPointNavigator.CurrentPage == 20 || UILinkPointNavigator.CurrentPage == 21))
			{
				num3 = PlayerInput.CurrentProfile.InventoryMoveCD;
			}
			if (navigatorDirections.X == -1f && UILinkPointNavigator.XCooldown == 0)
			{
				UILinkPointNavigator.XCooldown = num3;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelLeft();
			}
			if (navigatorDirections.X == 1f && UILinkPointNavigator.XCooldown == 0)
			{
				UILinkPointNavigator.XCooldown = num3;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelRight();
			}
			if (navigatorDirections.Y == -1f && UILinkPointNavigator.YCooldown == 0)
			{
				UILinkPointNavigator.YCooldown = num3;
				UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage].TravelUp();
			}
			if (navigatorDirections.Y == 1f && UILinkPointNavigator.YCooldown == 0)
			{
				UILinkPointNavigator.YCooldown = num3;
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
				Vector2 vector2 = new Vector2((float)PlayerInput.MouseX, (float)PlayerInput.MouseY);
				float amount = 0.3f;
				if (PlayerInput.InvisibleGamepadInMenus)
				{
					amount = 1f;
				}
				Vector2 vector = Vector2.Lerp(vector2, position, amount);
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

		// Token: 0x06001664 RID: 5732 RVA: 0x004B3CE4 File Offset: 0x004B1EE4
		public static void ResetFlagsEnd()
		{
			UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE = 0;
			UILinkPointNavigator.Shortcuts.BackButtonLock = false;
			UILinkPointNavigator.Shortcuts.BackButtonCommand = 0;
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x004B3CF8 File Offset: 0x004B1EF8
		public static string GetInstructions()
		{
			UILinkPage uILinkPage = UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage];
			UILinkPoint uILinkPoint = UILinkPointNavigator.Points[UILinkPointNavigator.CurrentPoint];
			if (UILinkPointNavigator._suggestedPointID != null)
			{
				UILinkPointNavigator.SwapToSuggestion();
				uILinkPoint = UILinkPointNavigator.Points[UILinkPointNavigator._suggestedPointID.Value];
				uILinkPage = UILinkPointNavigator.Pages[uILinkPoint.Page];
				UILinkPointNavigator.CurrentPage = uILinkPage.ID;
				uILinkPage.CurrentPoint = UILinkPointNavigator._suggestedPointID.Value;
			}
			string text = uILinkPage.SpecialInteractions();
			if ((PlayerInput.SettingsForUI.CurrentCursorMode == CursorMode.Gamepad && PlayerInput.Triggers.Current.UsedMovementKey && UILinkPointNavigator.InUse) || UILinkPointNavigator._suggestedPointID != null)
			{
				string text2 = uILinkPoint.SpecialInteractions();
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

		// Token: 0x06001666 RID: 5734 RVA: 0x004B3DD6 File Offset: 0x004B1FD6
		public static void SwapToSuggestion()
		{
			UILinkPointNavigator._preSuggestionPoint = new int?(UILinkPointNavigator.CurrentPoint);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x004B3DE8 File Offset: 0x004B1FE8
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

		// Token: 0x06001668 RID: 5736 RVA: 0x004B3E41 File Offset: 0x004B2041
		public static void ForceMovementCooldown(int time)
		{
			UILinkPointNavigator.LastInput = PlayerInput.Triggers.Current.GetNavigatorDirections();
			UILinkPointNavigator.XCooldown = time;
			UILinkPointNavigator.YCooldown = time;
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x004B3E63 File Offset: 0x004B2063
		public static void SetPosition(int ID, Vector2 Position)
		{
			UILinkPointNavigator.Points[ID].Position = Position * Main.UIScale;
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x004B3E80 File Offset: 0x004B2080
		public static void RegisterPage(UILinkPage page, int ID, bool automatedDefault = true)
		{
			if (automatedDefault)
			{
				page.DefaultPoint = page.LinkMap.Keys.First<int>();
			}
			page.CurrentPoint = page.DefaultPoint;
			page.ID = ID;
			UILinkPointNavigator.Pages.Add(page.ID, page);
			foreach (KeyValuePair<int, UILinkPoint> item in page.LinkMap)
			{
				item.Value.SetPage(ID);
				UILinkPointNavigator.Points.Add(item.Key, item.Value);
			}
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x004B3F30 File Offset: 0x004B2130
		public static void ChangePage(int PageID)
		{
			if (UILinkPointNavigator.Pages.ContainsKey(PageID) && UILinkPointNavigator.Pages[PageID].CanEnter())
			{
				SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				UILinkPointNavigator.CurrentPage = PageID;
				UILinkPointNavigator.ProcessChanges();
			}
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x004B3F7C File Offset: 0x004B217C
		public static void ChangePoint(int PointID)
		{
			if (UILinkPointNavigator.Points.ContainsKey(PointID))
			{
				UILinkPointNavigator.CurrentPage = UILinkPointNavigator.Points[PointID].Page;
				UILinkPointNavigator.OverridePoint = PointID;
				UILinkPointNavigator.ProcessChanges();
			}
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x004B3FAC File Offset: 0x004B21AC
		public static void ProcessChanges()
		{
			UILinkPage value = UILinkPointNavigator.Pages[UILinkPointNavigator.OldPage];
			if (UILinkPointNavigator.OldPage != UILinkPointNavigator.CurrentPage)
			{
				value.Leave();
				if (!UILinkPointNavigator.Pages.TryGetValue(UILinkPointNavigator.CurrentPage, out value))
				{
					UILinkPointNavigator.GoToDefaultPage(0);
					UILinkPointNavigator.ProcessChanges();
					UILinkPointNavigator.OverridePoint = -1;
				}
				value.CurrentPoint = value.DefaultPoint;
				value.Enter();
				value.Update();
				UILinkPointNavigator.OldPage = UILinkPointNavigator.CurrentPage;
			}
			if (UILinkPointNavigator.OverridePoint != -1 && value.LinkMap.ContainsKey(UILinkPointNavigator.OverridePoint))
			{
				value.CurrentPoint = UILinkPointNavigator.OverridePoint;
			}
		}

		// Token: 0x0400128D RID: 4749
		public static Dictionary<int, UILinkPage> Pages = new Dictionary<int, UILinkPage>();

		// Token: 0x0400128E RID: 4750
		public static Dictionary<int, UILinkPoint> Points = new Dictionary<int, UILinkPoint>();

		// Token: 0x0400128F RID: 4751
		public static int CurrentPage = 1000;

		// Token: 0x04001290 RID: 4752
		public static int OldPage = 1000;

		// Token: 0x04001291 RID: 4753
		private static int XCooldown;

		// Token: 0x04001292 RID: 4754
		private static int YCooldown;

		// Token: 0x04001293 RID: 4755
		private static Vector2 LastInput;

		// Token: 0x04001294 RID: 4756
		private static int PageLeftCD;

		// Token: 0x04001295 RID: 4757
		private static int PageRightCD;

		// Token: 0x04001296 RID: 4758
		public static bool InUse;

		// Token: 0x04001297 RID: 4759
		public static int OverridePoint = -1;

		// Token: 0x04001298 RID: 4760
		private static int? _suggestedPointID;

		// Token: 0x04001299 RID: 4761
		private static int? _preSuggestionPoint;

		// Token: 0x02000878 RID: 2168
		public static class Shortcuts
		{
			// Token: 0x040069A9 RID: 27049
			public static int NPCS_IconsPerColumn = 100;

			// Token: 0x040069AA RID: 27050
			public static int NPCS_IconsTotal = 0;

			// Token: 0x040069AB RID: 27051
			public static int NPCS_LastHovered = -2;

			// Token: 0x040069AC RID: 27052
			public static bool NPCS_IconsDisplay = false;

			// Token: 0x040069AD RID: 27053
			public static int CRAFT_IconsPerRow = 100;

			// Token: 0x040069AE RID: 27054
			public static int CRAFT_IconsPerColumn = 100;

			// Token: 0x040069AF RID: 27055
			public static int CRAFT_CurrentIngredientsCount = 0;

			// Token: 0x040069B0 RID: 27056
			public static int CRAFT_CurrentRecipeBig = 0;

			// Token: 0x040069B1 RID: 27057
			public static int CRAFT_CurrentRecipeSmall = 0;

			// Token: 0x040069B2 RID: 27058
			public static bool NPCCHAT_ButtonsLeft = false;

			// Token: 0x040069B3 RID: 27059
			public static bool NPCCHAT_ButtonsMiddle = false;

			// Token: 0x040069B4 RID: 27060
			public static bool NPCCHAT_ButtonsRight = false;

			// Token: 0x040069B5 RID: 27061
			public static bool NPCCHAT_ButtonsRight2 = false;

			// Token: 0x040069B6 RID: 27062
			public static int INGAMEOPTIONS_BUTTONS_LEFT = 0;

			// Token: 0x040069B7 RID: 27063
			public static int INGAMEOPTIONS_BUTTONS_RIGHT = 0;

			// Token: 0x040069B8 RID: 27064
			public static bool CREATIVE_ItemSlotShouldHighlightAsSelected = false;

			// Token: 0x040069B9 RID: 27065
			public static int OPTIONS_BUTTON_SPECIALFEATURE;

			// Token: 0x040069BA RID: 27066
			public static int BackButtonCommand;

			// Token: 0x040069BB RID: 27067
			public static bool BackButtonInUse = false;

			// Token: 0x040069BC RID: 27068
			public static bool BackButtonLock;

			// Token: 0x040069BD RID: 27069
			public static int BackButtonGoto = 0;

			// Token: 0x040069BE RID: 27070
			public static int FANCYUI_HIGHEST_INDEX = 1;

			// Token: 0x040069BF RID: 27071
			public static int FANCYUI_SPECIAL_INSTRUCTIONS = 0;

			// Token: 0x040069C0 RID: 27072
			public static int INFOACCCOUNT = 0;

			// Token: 0x040069C1 RID: 27073
			public static int BUILDERACCCOUNT = 0;

			// Token: 0x040069C2 RID: 27074
			public static int BUFFS_PER_COLUMN = 0;

			// Token: 0x040069C3 RID: 27075
			public static int BUFFS_DRAWN = 0;

			// Token: 0x040069C4 RID: 27076
			public static int INV_MOVE_OPTION_CD = 0;
		}
	}
}
