using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.GameInput;

namespace Terraria.UI.Gamepad
{
	// Token: 0x020000BA RID: 186
	public class GamepadMainMenuHandler
	{
		// Token: 0x06001631 RID: 5681 RVA: 0x004B2C3C File Offset: 0x004B0E3C
		public static void Update()
		{
			if (!GamepadMainMenuHandler.CanRun)
			{
				UILinkPage uilinkPage = UILinkPointNavigator.Pages[1000];
				uilinkPage.CurrentPoint = uilinkPage.DefaultPoint;
				Vector2 vector = new Vector2((float)Math.Cos((double)(Main.GlobalTimeWrappedHourly * 6.2831855f)), (float)Math.Sin((double)(Main.GlobalTimeWrappedHourly * 6.2831855f * 2f))) * new Vector2(30f, 15f) + Vector2.UnitY * 20f;
				UILinkPointNavigator.SetPosition(2000, new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / 2f + vector);
				return;
			}
			if (!Main.gameMenu || Main.MenuUI.IsVisible || GamepadMainMenuHandler.LastDrew != Main.menuMode)
			{
				return;
			}
			int lastMainMenu = GamepadMainMenuHandler.LastMainMenu;
			GamepadMainMenuHandler.LastMainMenu = Main.menuMode;
			switch (Main.menuMode)
			{
			case 17:
			case 18:
			case 19:
			case 21:
			case 22:
			case 23:
			case 24:
			case 26:
				if (GamepadMainMenuHandler.MenuItemPositions.Count >= 4)
				{
					Vector2 item = GamepadMainMenuHandler.MenuItemPositions[3];
					GamepadMainMenuHandler.MenuItemPositions.RemoveAt(3);
					if (Main.menuMode == 17)
					{
						GamepadMainMenuHandler.MenuItemPositions.Insert(0, item);
					}
				}
				break;
			case 28:
				if (GamepadMainMenuHandler.MenuItemPositions.Count >= 3)
				{
					GamepadMainMenuHandler.MenuItemPositions.RemoveAt(1);
				}
				break;
			}
			UILinkPage uILinkPage2 = UILinkPointNavigator.Pages[1000];
			if (lastMainMenu != Main.menuMode)
			{
				uILinkPage2.CurrentPoint = uILinkPage2.DefaultPoint;
			}
			for (int i = 0; i < GamepadMainMenuHandler.MenuItemPositions.Count; i++)
			{
				Vector2 position = GamepadMainMenuHandler.MenuItemPositions[i] * Main.UIScale;
				if (i == 0 && lastMainMenu != GamepadMainMenuHandler.LastMainMenu && PlayerInput.UsingGamepad && Main.InvisibleCursorForGamepad)
				{
					Main.mouseX = (PlayerInput.MouseX = (int)position.X);
					Main.mouseY = (PlayerInput.MouseY = (int)position.Y);
					Main.menuFocus = -1;
				}
				UILinkPoint uILinkPoint = uILinkPage2.LinkMap[2000 + i];
				uILinkPoint.Position = position;
				if (i == 0)
				{
					uILinkPoint.Up = -1;
				}
				else
				{
					uILinkPoint.Up = 2000 + i - 1;
				}
				uILinkPoint.Left = -3;
				uILinkPoint.Right = -4;
				if (i == GamepadMainMenuHandler.MenuItemPositions.Count - 1)
				{
					uILinkPoint.Down = -2;
				}
				else
				{
					uILinkPoint.Down = 2000 + i + 1;
				}
				if (GamepadMainMenuHandler.MoveCursorOnNextRun)
				{
					GamepadMainMenuHandler.MoveCursorOnNextRun = false;
					UILinkPointNavigator.ChangePoint(uILinkPoint.ID);
				}
			}
			GamepadMainMenuHandler.MenuItemPositions.Clear();
		}

		// Token: 0x0400116E RID: 4462
		public static int LastMainMenu = -1;

		// Token: 0x0400116F RID: 4463
		public static List<Vector2> MenuItemPositions = new List<Vector2>(20);

		// Token: 0x04001170 RID: 4464
		public static int LastDrew = -1;

		// Token: 0x04001171 RID: 4465
		public static bool CanRun = false;

		// Token: 0x04001172 RID: 4466
		public static bool MoveCursorOnNextRun = false;
	}
}
