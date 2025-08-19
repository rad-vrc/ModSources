using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.Initializers
{
	// Token: 0x020000E9 RID: 233
	public class UILinksInitializer
	{
		// Token: 0x06001598 RID: 5528 RVA: 0x004BD4E9 File Offset: 0x004BB6E9
		public static bool NothingMoreImportantThanNPCChat()
		{
			return !Main.hairWindow && Main.npcShop == 0 && Main.player[Main.myPlayer].chest == -1;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x004BD510 File Offset: 0x004BB710
		public static float HandleSliderHorizontalInput(float currentValue, float min, float max, float deadZone = 0.2f, float sensitivity = 0.5f)
		{
			float num = PlayerInput.GamepadThumbstickLeft.X;
			if (num < -deadZone || num > deadZone)
			{
				num = MathHelper.Lerp(0f, sensitivity / 60f, (Math.Abs(num) - deadZone) / (1f - deadZone)) * (float)Math.Sign(num);
			}
			else
			{
				num = 0f;
			}
			return MathHelper.Clamp((currentValue - min) / (max - min) + num, 0f, 1f) * (max - min) + min;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x004BD584 File Offset: 0x004BB784
		public static float HandleSliderVerticalInput(float currentValue, float min, float max, float deadZone = 0.2f, float sensitivity = 0.5f)
		{
			float num = -PlayerInput.GamepadThumbstickLeft.Y;
			if (num < -deadZone || num > deadZone)
			{
				num = MathHelper.Lerp(0f, sensitivity / 60f, (Math.Abs(num) - deadZone) / (1f - deadZone)) * (float)Math.Sign(num);
			}
			else
			{
				num = 0f;
			}
			return MathHelper.Clamp((currentValue - min) / (max - min) + num, 0f, 1f) * (max - min) + min;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0049A1AA File Offset: 0x004983AA
		public static bool CanExecuteInputCommand()
		{
			return PlayerInput.AllowExecutionOfGamepadInstructions;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x004BD5F8 File Offset: 0x004BB7F8
		public static void Load()
		{
			Func<string> value = () => PlayerInput.BuildCommand(Lang.misc[53].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
			});
			UILinkPage uilinkPage = new UILinkPage();
			uilinkPage.UpdateEvent += delegate()
			{
				PlayerInput.GamepadAllowScrolling = true;
			};
			for (int i = 0; i < 20; i++)
			{
				uilinkPage.LinkMap.Add(2000 + i, new UILinkPoint(2000 + i, true, -3, -4, -1, -2));
			}
			uilinkPage.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[53].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
			}) + PlayerInput.BuildCommand(Lang.misc[82].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}));
			uilinkPage.UpdateEvent += delegate()
			{
				bool flag = PlayerInput.Triggers.JustPressed.Inventory;
				if (Main.inputTextEscape)
				{
					Main.inputTextEscape = false;
					flag = true;
				}
				if (UILinksInitializer.CanExecuteInputCommand() && flag)
				{
					UILinksInitializer.FancyExit();
				}
				UILinkPointNavigator.Shortcuts.BackButtonInUse = flag;
				UILinksInitializer.HandleOptionsSpecials();
			};
			uilinkPage.IsValidEvent += (() => Main.gameMenu && !Main.MenuUI.IsVisible);
			uilinkPage.CanEnterEvent += (() => Main.gameMenu && !Main.MenuUI.IsVisible);
			UILinkPointNavigator.RegisterPage(uilinkPage, 1000, true);
			UILinkPage cp2 = new UILinkPage();
			cp2.LinkMap.Add(2500, new UILinkPoint(2500, true, -3, 2501, -1, -2));
			cp2.LinkMap.Add(2501, new UILinkPoint(2501, true, 2500, 2502, -1, -2));
			cp2.LinkMap.Add(2502, new UILinkPoint(2502, true, 2501, 2503, -1, -2));
			cp2.LinkMap.Add(2503, new UILinkPoint(2503, true, 2502, -4, -1, -2));
			cp2.UpdateEvent += delegate()
			{
				cp2.LinkMap[2501].Right = (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight ? 2502 : -4);
				if (cp2.LinkMap[2501].Right == -4 && UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight2)
				{
					cp2.LinkMap[2501].Right = 2503;
				}
				cp2.LinkMap[2502].Right = (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight2 ? 2503 : -4);
				cp2.LinkMap[2503].Left = (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight ? 2502 : 2501);
			};
			cp2.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[53].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
			}) + PlayerInput.BuildCommand(Lang.misc[56].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}));
			cp2.IsValidEvent += (() => (Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1) && UILinksInitializer.NothingMoreImportantThanNPCChat());
			cp2.CanEnterEvent += (() => (Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1) && UILinksInitializer.NothingMoreImportantThanNPCChat());
			cp2.EnterEvent += delegate()
			{
				Main.player[Main.myPlayer].releaseInventory = false;
			};
			cp2.LeaveEvent += delegate()
			{
				Main.npcChatRelease = false;
				Main.player[Main.myPlayer].LockGamepadTileInteractions();
			};
			UILinkPointNavigator.RegisterPage(cp2, 1003, true);
			UILinkPage cp3 = new UILinkPage();
			cp3.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value2 = delegate()
			{
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].inventory, 0, currentPoint);
			};
			Func<string> value3 = () => ItemSlot.GetGamepadInstructions(ref Main.player[Main.myPlayer].trashItem, 6);
			for (int j = 0; j <= 49; j++)
			{
				UILinkPoint uilinkPoint = new UILinkPoint(j, true, j - 1, j + 1, j - 10, j + 10);
				uilinkPoint.OnSpecialInteracts += value2;
				int num = j;
				if (num < 10)
				{
					uilinkPoint.Up = -1;
				}
				if (num >= 40)
				{
					uilinkPoint.Down = -2;
				}
				if (num % 10 == 9)
				{
					uilinkPoint.Right = -4;
				}
				if (num % 10 == 0)
				{
					uilinkPoint.Left = -3;
				}
				cp3.LinkMap.Add(j, uilinkPoint);
			}
			cp3.LinkMap[9].Right = 0;
			cp3.LinkMap[19].Right = 50;
			cp3.LinkMap[29].Right = 51;
			cp3.LinkMap[39].Right = 52;
			cp3.LinkMap[49].Right = 53;
			cp3.LinkMap[0].Left = 9;
			cp3.LinkMap[10].Left = 54;
			cp3.LinkMap[20].Left = 55;
			cp3.LinkMap[30].Left = 56;
			cp3.LinkMap[40].Left = 57;
			cp3.LinkMap.Add(300, new UILinkPoint(300, true, 309, 310, 49, -2));
			cp3.LinkMap.Add(309, new UILinkPoint(309, true, 310, 300, 302, 54));
			cp3.LinkMap.Add(310, new UILinkPoint(310, true, 300, 309, 301, 50));
			cp3.LinkMap.Add(301, new UILinkPoint(301, true, 300, 302, 53, 50));
			cp3.LinkMap.Add(302, new UILinkPoint(302, true, 301, 310, 57, 54));
			cp3.LinkMap.Add(311, new UILinkPoint(311, true, -3, -4, 40, -2));
			cp3.LinkMap[301].OnSpecialInteracts += value;
			cp3.LinkMap[302].OnSpecialInteracts += value;
			cp3.LinkMap[309].OnSpecialInteracts += value;
			cp3.LinkMap[310].OnSpecialInteracts += value;
			cp3.LinkMap[300].OnSpecialInteracts += value3;
			cp3.UpdateEvent += delegate()
			{
				bool inReforgeMenu = Main.InReforgeMenu;
				bool flag = Main.player[Main.myPlayer].chest != -1;
				bool flag2 = Main.npcShop != 0;
				TileEntity tileEntity = Main.LocalPlayer.tileEntityAnchor.GetTileEntity();
				bool flag3 = tileEntity is TEHatRack;
				bool flag4 = tileEntity is TEDisplayDoll;
				for (int num32 = 40; num32 <= 49; num32++)
				{
					if (inReforgeMenu)
					{
						cp3.LinkMap[num32].Down = ((num32 < 45) ? 303 : 304);
					}
					else if (flag)
					{
						cp3.LinkMap[num32].Down = 400 + num32 - 40;
					}
					else if (flag2)
					{
						cp3.LinkMap[num32].Down = 2700 + num32 - 40;
					}
					else if (num32 == 40)
					{
						cp3.LinkMap[num32].Down = 311;
					}
					else
					{
						cp3.LinkMap[num32].Down = -2;
					}
				}
				if (flag4)
				{
					for (int num33 = 40; num33 <= 47; num33++)
					{
						cp3.LinkMap[num33].Down = 5100 + num33 - 40;
					}
				}
				if (flag3)
				{
					for (int num34 = 44; num34 <= 45; num34++)
					{
						cp3.LinkMap[num34].Down = 5000 + num34 - 44;
					}
				}
				if (flag)
				{
					cp3.LinkMap[300].Up = 439;
					cp3.LinkMap[300].Right = -4;
					cp3.LinkMap[300].Left = 309;
					cp3.LinkMap[309].Up = 438;
					cp3.LinkMap[309].Right = 300;
					cp3.LinkMap[309].Left = 310;
					cp3.LinkMap[310].Up = 437;
					cp3.LinkMap[310].Right = 309;
					cp3.LinkMap[310].Left = -3;
				}
				else if (flag2)
				{
					cp3.LinkMap[300].Up = 2739;
					cp3.LinkMap[300].Right = -4;
					cp3.LinkMap[300].Left = 309;
					cp3.LinkMap[309].Up = 2738;
					cp3.LinkMap[309].Right = 300;
					cp3.LinkMap[309].Left = 310;
					cp3.LinkMap[310].Up = 2737;
					cp3.LinkMap[310].Right = 309;
					cp3.LinkMap[310].Left = -3;
				}
				else
				{
					cp3.LinkMap[49].Down = 300;
					cp3.LinkMap[48].Down = 309;
					cp3.LinkMap[47].Down = 310;
					cp3.LinkMap[300].Up = 49;
					cp3.LinkMap[300].Right = 301;
					cp3.LinkMap[300].Left = 309;
					cp3.LinkMap[309].Up = 48;
					cp3.LinkMap[309].Right = 300;
					cp3.LinkMap[309].Left = 310;
					cp3.LinkMap[310].Up = 47;
					cp3.LinkMap[310].Right = 309;
					cp3.LinkMap[310].Left = 302;
				}
				cp3.LinkMap[0].Left = 9;
				cp3.LinkMap[10].Left = 54;
				cp3.LinkMap[20].Left = 55;
				cp3.LinkMap[30].Left = 56;
				cp3.LinkMap[40].Left = 57;
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 0)
				{
					cp3.LinkMap[0].Left = 6000;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 2)
				{
					cp3.LinkMap[10].Left = 6002;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 4)
				{
					cp3.LinkMap[20].Left = 6004;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 6)
				{
					cp3.LinkMap[30].Left = 6006;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 8)
				{
					cp3.LinkMap[40].Left = 6008;
				}
				cp3.PageOnLeft = 9;
				if (Main.CreativeMenu.Enabled)
				{
					cp3.PageOnLeft = 1005;
				}
				if (Main.InReforgeMenu)
				{
					cp3.PageOnLeft = 5;
				}
			};
			cp3.IsValidEvent += (() => Main.playerInventory);
			cp3.PageOnLeft = 9;
			cp3.PageOnRight = 2;
			UILinkPointNavigator.RegisterPage(cp3, 0, true);
			UILinkPage cp4 = new UILinkPage();
			cp4.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value4 = delegate()
			{
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].inventory, 1, currentPoint);
			};
			for (int k = 50; k <= 53; k++)
			{
				UILinkPoint uilinkPoint2 = new UILinkPoint(k, true, -3, -4, k - 1, k + 1);
				uilinkPoint2.OnSpecialInteracts += value4;
				cp4.LinkMap.Add(k, uilinkPoint2);
			}
			cp4.LinkMap[50].Left = 19;
			cp4.LinkMap[51].Left = 29;
			cp4.LinkMap[52].Left = 39;
			cp4.LinkMap[53].Left = 49;
			cp4.LinkMap[50].Right = 54;
			cp4.LinkMap[51].Right = 55;
			cp4.LinkMap[52].Right = 56;
			cp4.LinkMap[53].Right = 57;
			cp4.LinkMap[50].Up = -1;
			cp4.LinkMap[53].Down = -2;
			cp4.UpdateEvent += delegate()
			{
				if (Main.player[Main.myPlayer].chest == -1 && Main.npcShop == 0)
				{
					cp4.LinkMap[50].Up = 301;
					cp4.LinkMap[53].Down = 301;
					return;
				}
				cp4.LinkMap[50].Up = 504;
				cp4.LinkMap[53].Down = 500;
			};
			cp4.IsValidEvent += (() => Main.playerInventory);
			cp4.PageOnLeft = 0;
			cp4.PageOnRight = 2;
			UILinkPointNavigator.RegisterPage(cp4, 1, true);
			UILinkPage cp5 = new UILinkPage();
			cp5.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value5 = delegate()
			{
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].inventory, 2, currentPoint);
			};
			for (int l = 54; l <= 57; l++)
			{
				UILinkPoint uilinkPoint3 = new UILinkPoint(l, true, -3, -4, l - 1, l + 1);
				uilinkPoint3.OnSpecialInteracts += value5;
				cp5.LinkMap.Add(l, uilinkPoint3);
			}
			cp5.LinkMap[54].Left = 50;
			cp5.LinkMap[55].Left = 51;
			cp5.LinkMap[56].Left = 52;
			cp5.LinkMap[57].Left = 53;
			cp5.LinkMap[54].Right = 10;
			cp5.LinkMap[55].Right = 20;
			cp5.LinkMap[56].Right = 30;
			cp5.LinkMap[57].Right = 40;
			cp5.LinkMap[54].Up = -1;
			cp5.LinkMap[57].Down = -2;
			cp5.UpdateEvent += delegate()
			{
				if (Main.player[Main.myPlayer].chest == -1 && Main.npcShop == 0)
				{
					cp5.LinkMap[54].Up = 302;
					cp5.LinkMap[57].Down = 302;
					return;
				}
				cp5.LinkMap[54].Up = 504;
				cp5.LinkMap[57].Down = 500;
			};
			cp5.PageOnLeft = 0;
			cp5.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp5, 2, true);
			UILinkPage cp6 = new UILinkPage();
			cp6.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value6 = delegate()
			{
				int num32 = UILinkPointNavigator.CurrentPoint - 100;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].armor, (num32 < 10) ? 8 : 9, num32);
			};
			Func<string> value7 = delegate()
			{
				int slot = UILinkPointNavigator.CurrentPoint - 120;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].dye, 12, slot);
			};
			for (int m = 100; m <= 119; m++)
			{
				UILinkPoint uilinkPoint4 = new UILinkPoint(m, true, m + 10, m - 10, m - 1, m + 1);
				uilinkPoint4.OnSpecialInteracts += value6;
				int num2 = m - 100;
				if (num2 == 0)
				{
					uilinkPoint4.Up = 305;
				}
				if (num2 == 10)
				{
					uilinkPoint4.Up = 306;
				}
				if (num2 == 9 || num2 == 19)
				{
					uilinkPoint4.Down = -2;
				}
				if (num2 >= 10)
				{
					uilinkPoint4.Left = 120 + num2 % 10;
				}
				else if (num2 >= 3)
				{
					uilinkPoint4.Right = -4;
				}
				else
				{
					uilinkPoint4.Right = 312 + num2;
				}
				cp6.LinkMap.Add(m, uilinkPoint4);
			}
			for (int n = 120; n <= 129; n++)
			{
				UILinkPoint uilinkPoint4 = new UILinkPoint(n, true, -3, -10 + n, n - 1, n + 1);
				uilinkPoint4.OnSpecialInteracts += value7;
				int num3 = n - 120;
				if (num3 == 0)
				{
					uilinkPoint4.Up = 307;
				}
				if (num3 == 9)
				{
					uilinkPoint4.Down = 308;
					uilinkPoint4.Left = 1557;
				}
				if (num3 == 8)
				{
					uilinkPoint4.Left = 1570;
				}
				cp6.LinkMap.Add(n, uilinkPoint4);
			}
			for (int num4 = 312; num4 <= 314; num4++)
			{
				int num5 = num4 - 312;
				UILinkPoint uilinkPoint4 = new UILinkPoint(num4, true, 100 + num5, -4, num4 - 1, num4 + 1);
				if (num5 == 0)
				{
					uilinkPoint4.Up = -1;
				}
				if (num5 == 2)
				{
					uilinkPoint4.Down = -2;
				}
				uilinkPoint4.OnSpecialInteracts += value;
				cp6.LinkMap.Add(num4, uilinkPoint4);
			}
			cp6.IsValidEvent += (() => Main.playerInventory && Main.EquipPage == 0);
			cp6.UpdateEvent += delegate()
			{
				int num32 = 107;
				int amountOfExtraAccessorySlotsToShow = Main.player[Main.myPlayer].GetAmountOfExtraAccessorySlotsToShow();
				for (int num33 = 0; num33 < amountOfExtraAccessorySlotsToShow; num33++)
				{
					cp6.LinkMap[num32 + num33].Down = num32 + num33 + 1;
					cp6.LinkMap[num32 - 100 + 120 + num33].Down = num32 - 100 + 120 + num33 + 1;
					cp6.LinkMap[num32 + 10 + num33].Down = num32 + 10 + num33 + 1;
				}
				cp6.LinkMap[num32 + amountOfExtraAccessorySlotsToShow].Down = 308;
				cp6.LinkMap[num32 + 10 + amountOfExtraAccessorySlotsToShow].Down = 308;
				cp6.LinkMap[num32 - 100 + 120 + amountOfExtraAccessorySlotsToShow].Down = 308;
				bool shouldPVPDraw = Main.ShouldPVPDraw;
				for (int num34 = 120; num34 <= 129; num34++)
				{
					UILinkPoint uilinkPoint17 = cp6.LinkMap[num34];
					int num35 = num34 - 120;
					uilinkPoint17.Left = -3;
					if (num35 == 0)
					{
						uilinkPoint17.Left = (shouldPVPDraw ? 1550 : -3);
					}
					if (num35 == 1)
					{
						uilinkPoint17.Left = (shouldPVPDraw ? 1552 : -3);
					}
					if (num35 == 2)
					{
						uilinkPoint17.Left = (shouldPVPDraw ? 1556 : -3);
					}
					if (num35 == 3)
					{
						uilinkPoint17.Left = ((UILinkPointNavigator.Shortcuts.INFOACCCOUNT >= 1) ? 1558 : -3);
					}
					if (num35 == 4)
					{
						uilinkPoint17.Left = ((UILinkPointNavigator.Shortcuts.INFOACCCOUNT >= 5) ? 1562 : -3);
					}
					if (num35 == 5)
					{
						uilinkPoint17.Left = ((UILinkPointNavigator.Shortcuts.INFOACCCOUNT >= 9) ? 1566 : -3);
					}
				}
				cp6.LinkMap[num32 - 100 + 120 + amountOfExtraAccessorySlotsToShow].Left = 1557;
				cp6.LinkMap[num32 - 100 + 120 + amountOfExtraAccessorySlotsToShow - 1].Left = 1570;
			};
			cp6.PageOnLeft = 8;
			cp6.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp6, 3, true);
			UILinkPage uilinkPage2 = new UILinkPage();
			uilinkPage2.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value8 = delegate()
			{
				int slot = UILinkPointNavigator.CurrentPoint - 400;
				int context = 4;
				Item[] item = Main.player[Main.myPlayer].bank.item;
				switch (Main.player[Main.myPlayer].chest)
				{
				case -5:
					item = Main.player[Main.myPlayer].bank4.item;
					context = 32;
					break;
				case -4:
					item = Main.player[Main.myPlayer].bank3.item;
					break;
				case -3:
					item = Main.player[Main.myPlayer].bank2.item;
					break;
				case -2:
					break;
				case -1:
					return "";
				default:
					item = Main.chest[Main.player[Main.myPlayer].chest].item;
					context = 3;
					break;
				}
				return ItemSlot.GetGamepadInstructions(item, context, slot);
			};
			for (int num6 = 400; num6 <= 439; num6++)
			{
				UILinkPoint uilinkPoint5 = new UILinkPoint(num6, true, num6 - 1, num6 + 1, num6 - 10, num6 + 10);
				uilinkPoint5.OnSpecialInteracts += value8;
				int num7 = num6 - 400;
				if (num7 < 10)
				{
					uilinkPoint5.Up = 40 + num7;
				}
				if (num7 >= 30)
				{
					uilinkPoint5.Down = -2;
				}
				if (num7 % 10 == 9)
				{
					uilinkPoint5.Right = -4;
				}
				if (num7 % 10 == 0)
				{
					uilinkPoint5.Left = -3;
				}
				uilinkPage2.LinkMap.Add(num6, uilinkPoint5);
			}
			uilinkPage2.LinkMap.Add(500, new UILinkPoint(500, true, 409, -4, 53, 501));
			uilinkPage2.LinkMap.Add(501, new UILinkPoint(501, true, 419, -4, 500, 502));
			uilinkPage2.LinkMap.Add(502, new UILinkPoint(502, true, 429, -4, 501, 503));
			uilinkPage2.LinkMap.Add(503, new UILinkPoint(503, true, 439, -4, 502, 505));
			uilinkPage2.LinkMap.Add(505, new UILinkPoint(505, true, 439, -4, 503, 504));
			uilinkPage2.LinkMap.Add(504, new UILinkPoint(504, true, 439, -4, 505, 50));
			uilinkPage2.LinkMap.Add(506, new UILinkPoint(506, true, 439, -4, 505, 50));
			uilinkPage2.LinkMap[500].OnSpecialInteracts += value;
			uilinkPage2.LinkMap[501].OnSpecialInteracts += value;
			uilinkPage2.LinkMap[502].OnSpecialInteracts += value;
			uilinkPage2.LinkMap[503].OnSpecialInteracts += value;
			uilinkPage2.LinkMap[504].OnSpecialInteracts += value;
			uilinkPage2.LinkMap[505].OnSpecialInteracts += value;
			uilinkPage2.LinkMap[506].OnSpecialInteracts += value;
			uilinkPage2.LinkMap[409].Right = 500;
			uilinkPage2.LinkMap[419].Right = 501;
			uilinkPage2.LinkMap[429].Right = 502;
			uilinkPage2.LinkMap[439].Right = 503;
			uilinkPage2.LinkMap[439].Down = 300;
			uilinkPage2.LinkMap[438].Down = 309;
			uilinkPage2.LinkMap[437].Down = 310;
			uilinkPage2.PageOnLeft = 0;
			uilinkPage2.PageOnRight = 0;
			uilinkPage2.DefaultPoint = 400;
			UILinkPointNavigator.RegisterPage(uilinkPage2, 4, false);
			uilinkPage2.IsValidEvent += (() => Main.playerInventory && Main.player[Main.myPlayer].chest != -1);
			UILinkPage uilinkPage3 = new UILinkPage();
			uilinkPage3.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value9 = delegate()
			{
				int slot = UILinkPointNavigator.CurrentPoint - 5100;
				TEDisplayDoll tedisplayDoll = Main.LocalPlayer.tileEntityAnchor.GetTileEntity() as TEDisplayDoll;
				if (tedisplayDoll == null)
				{
					return "";
				}
				return tedisplayDoll.GetItemGamepadInstructions(slot);
			};
			for (int num8 = 5100; num8 <= 5115; num8++)
			{
				UILinkPoint uilinkPoint6 = new UILinkPoint(num8, true, num8 - 1, num8 + 1, num8 - 8, num8 + 8);
				uilinkPoint6.OnSpecialInteracts += value9;
				int num9 = num8 - 5100;
				if (num9 < 8)
				{
					uilinkPoint6.Up = 40 + num9;
				}
				if (num9 >= 8)
				{
					uilinkPoint6.Down = -2;
				}
				if (num9 % 8 == 7)
				{
					uilinkPoint6.Right = -4;
				}
				if (num9 % 8 == 0)
				{
					uilinkPoint6.Left = -3;
				}
				uilinkPage3.LinkMap.Add(num8, uilinkPoint6);
			}
			uilinkPage3.PageOnLeft = 0;
			uilinkPage3.PageOnRight = 0;
			uilinkPage3.DefaultPoint = 5100;
			UILinkPointNavigator.RegisterPage(uilinkPage3, 20, false);
			uilinkPage3.IsValidEvent += (() => Main.playerInventory && Main.LocalPlayer.tileEntityAnchor.GetTileEntity() is TEDisplayDoll);
			UILinkPage uilinkPage4 = new UILinkPage();
			uilinkPage4.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value10 = delegate()
			{
				int slot = UILinkPointNavigator.CurrentPoint - 5000;
				TEHatRack tehatRack = Main.LocalPlayer.tileEntityAnchor.GetTileEntity() as TEHatRack;
				if (tehatRack == null)
				{
					return "";
				}
				return tehatRack.GetItemGamepadInstructions(slot);
			};
			for (int num10 = 5000; num10 <= 5003; num10++)
			{
				UILinkPoint uilinkPoint7 = new UILinkPoint(num10, true, num10 - 1, num10 + 1, num10 - 2, num10 + 2);
				uilinkPoint7.OnSpecialInteracts += value10;
				int num11 = num10 - 5000;
				if (num11 < 2)
				{
					uilinkPoint7.Up = 44 + num11;
				}
				if (num11 >= 2)
				{
					uilinkPoint7.Down = -2;
				}
				if (num11 % 2 == 1)
				{
					uilinkPoint7.Right = -4;
				}
				if (num11 % 2 == 0)
				{
					uilinkPoint7.Left = -3;
				}
				uilinkPage4.LinkMap.Add(num10, uilinkPoint7);
			}
			uilinkPage4.PageOnLeft = 0;
			uilinkPage4.PageOnRight = 0;
			uilinkPage4.DefaultPoint = 5000;
			UILinkPointNavigator.RegisterPage(uilinkPage4, 21, false);
			uilinkPage4.IsValidEvent += (() => Main.playerInventory && Main.LocalPlayer.tileEntityAnchor.GetTileEntity() is TEHatRack);
			UILinkPage uilinkPage5 = new UILinkPage();
			uilinkPage5.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value11 = delegate()
			{
				if (Main.npcShop == 0)
				{
					return "";
				}
				int slot = UILinkPointNavigator.CurrentPoint - 2700;
				return ItemSlot.GetGamepadInstructions(Main.instance.shop[Main.npcShop].item, 15, slot);
			};
			for (int num12 = 2700; num12 <= 2739; num12++)
			{
				UILinkPoint uilinkPoint8 = new UILinkPoint(num12, true, num12 - 1, num12 + 1, num12 - 10, num12 + 10);
				uilinkPoint8.OnSpecialInteracts += value11;
				int num13 = num12 - 2700;
				if (num13 < 10)
				{
					uilinkPoint8.Up = 40 + num13;
				}
				if (num13 >= 30)
				{
					uilinkPoint8.Down = -2;
				}
				if (num13 % 10 == 9)
				{
					uilinkPoint8.Right = -4;
				}
				if (num13 % 10 == 0)
				{
					uilinkPoint8.Left = -3;
				}
				uilinkPage5.LinkMap.Add(num12, uilinkPoint8);
			}
			uilinkPage5.LinkMap[2739].Down = 300;
			uilinkPage5.LinkMap[2738].Down = 309;
			uilinkPage5.LinkMap[2737].Down = 310;
			uilinkPage5.PageOnLeft = 0;
			uilinkPage5.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(uilinkPage5, 13, true);
			uilinkPage5.IsValidEvent += (() => Main.playerInventory && Main.npcShop != 0);
			UILinkPage cp7 = new UILinkPage();
			cp7.LinkMap.Add(303, new UILinkPoint(303, true, 304, 304, 40, -2));
			cp7.LinkMap.Add(304, new UILinkPoint(304, true, 303, 303, 40, -2));
			cp7.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value12 = () => ItemSlot.GetGamepadInstructions(ref Main.reforgeItem, 5);
			cp7.LinkMap[303].OnSpecialInteracts += value12;
			cp7.LinkMap[304].OnSpecialInteracts += (() => Lang.misc[53].Value);
			cp7.UpdateEvent += delegate()
			{
				if (Main.reforgeItem.type > 0)
				{
					cp7.LinkMap[303].Left = (cp7.LinkMap[303].Right = 304);
					return;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp7.CurrentPoint == 304)
				{
					UILinkPointNavigator.ChangePoint(303);
				}
				cp7.LinkMap[303].Left = -3;
				cp7.LinkMap[303].Right = -4;
			};
			cp7.IsValidEvent += (() => Main.playerInventory && Main.InReforgeMenu);
			cp7.PageOnLeft = 0;
			cp7.PageOnRight = 0;
			cp7.EnterEvent += delegate()
			{
				PlayerInput.LockGamepadButtons("MouseLeft");
			};
			UILinkPointNavigator.RegisterPage(cp7, 5, true);
			UILinkPage cp8 = new UILinkPage();
			cp8.OnSpecialInteracts += delegate()
			{
				bool flag = UILinkPointNavigator.CurrentPoint == 600;
				bool flag2 = !flag && WorldGen.IsNPCEvictable(UILinkPointNavigator.Shortcuts.NPCS_LastHovered);
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Grapple)
				{
					Point point = Main.player[Main.myPlayer].Center.ToTileCoordinates();
					if (flag)
					{
						if (WorldGen.MoveTownNPC(point.X, point.Y, -1))
						{
							Main.NewText(Lang.inter[39].Value, byte.MaxValue, 240, 20);
						}
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
					else if (WorldGen.MoveTownNPC(point.X, point.Y, UILinkPointNavigator.Shortcuts.NPCS_LastHovered))
					{
						WorldGen.moveRoom(point.X, point.Y, UILinkPointNavigator.Shortcuts.NPCS_LastHovered);
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					}
					PlayerInput.LockGamepadButtons("Grapple");
					PlayerInput.SettingsForUI.TryRevertingToMouseMode();
				}
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.SmartSelect)
				{
					UILinkPointNavigator.Shortcuts.NPCS_IconsDisplay = !UILinkPointNavigator.Shortcuts.NPCS_IconsDisplay;
					PlayerInput.LockGamepadButtons("SmartSelect");
					PlayerInput.SettingsForUI.TryRevertingToMouseMode();
				}
				if (flag2 && UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.MouseRight)
				{
					WorldGen.kickOut(UILinkPointNavigator.Shortcuts.NPCS_LastHovered);
				}
				return string.Concat(new string[]
				{
					PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
					}),
					PlayerInput.BuildCommand(Lang.misc[64].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
						PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
					}),
					PlayerInput.BuildCommand(Lang.misc[70].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
					}),
					PlayerInput.BuildCommand(Lang.misc[69].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["SmartSelect"]
					}),
					flag2 ? PlayerInput.BuildCommand("Evict", false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
					}) : ""
				});
			};
			for (int num14 = 600; num14 <= 650; num14++)
			{
				UILinkPoint value13 = new UILinkPoint(num14, true, num14 + 10, num14 - 10, num14 - 1, num14 + 1);
				cp8.LinkMap.Add(num14, value13);
			}
			cp8.UpdateEvent += delegate()
			{
				int num32 = UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn;
				if (num32 == 0)
				{
					num32 = 100;
				}
				for (int num33 = 0; num33 < 50; num33++)
				{
					cp8.LinkMap[600 + num33].Up = ((num33 % num32 == 0) ? -1 : (600 + num33 - 1));
					if (cp8.LinkMap[600 + num33].Up == -1)
					{
						if (num33 >= num32 * 2)
						{
							cp8.LinkMap[600 + num33].Up = 307;
						}
						else if (num33 >= num32)
						{
							cp8.LinkMap[600 + num33].Up = 306;
						}
						else
						{
							cp8.LinkMap[600 + num33].Up = 305;
						}
					}
					cp8.LinkMap[600 + num33].Down = (((num33 + 1) % num32 == 0 || num33 == UILinkPointNavigator.Shortcuts.NPCS_IconsTotal - 1) ? 308 : (600 + num33 + 1));
					cp8.LinkMap[600 + num33].Left = ((num33 < UILinkPointNavigator.Shortcuts.NPCS_IconsTotal - num32) ? (600 + num33 + num32) : -3);
					cp8.LinkMap[600 + num33].Right = ((num33 < num32) ? -4 : (600 + num33 - num32));
				}
			};
			cp8.IsValidEvent += (() => Main.playerInventory && Main.EquipPage == 1);
			cp8.PageOnLeft = 8;
			cp8.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp8, 6, true);
			UILinkPage cp9 = new UILinkPage();
			cp9.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value14 = delegate()
			{
				int slot = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 20, slot);
			};
			Func<string> value15 = delegate()
			{
				int slot = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 19, slot);
			};
			Func<string> value16 = delegate()
			{
				int slot = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 18, slot);
			};
			Func<string> value17 = delegate()
			{
				int slot = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 17, slot);
			};
			Func<string> value18 = delegate()
			{
				int slot = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 16, slot);
			};
			Func<string> value19 = delegate()
			{
				int slot = UILinkPointNavigator.CurrentPoint - 185;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscDyes, 33, slot);
			};
			for (int num15 = 180; num15 <= 184; num15++)
			{
				UILinkPoint uilinkPoint9 = new UILinkPoint(num15, true, 185 + num15 - 180, -4, num15 - 1, num15 + 1);
				int num16 = num15 - 180;
				if (num16 == 0)
				{
					uilinkPoint9.Up = 305;
				}
				if (num16 == 4)
				{
					uilinkPoint9.Down = 308;
				}
				cp9.LinkMap.Add(num15, uilinkPoint9);
				switch (num15)
				{
				case 180:
					uilinkPoint9.OnSpecialInteracts += value15;
					break;
				case 181:
					uilinkPoint9.OnSpecialInteracts += value14;
					break;
				case 182:
					uilinkPoint9.OnSpecialInteracts += value16;
					break;
				case 183:
					uilinkPoint9.OnSpecialInteracts += value17;
					break;
				case 184:
					uilinkPoint9.OnSpecialInteracts += value18;
					break;
				}
			}
			for (int num17 = 185; num17 <= 189; num17++)
			{
				UILinkPoint uilinkPoint9 = new UILinkPoint(num17, true, -3, -5 + num17, num17 - 1, num17 + 1);
				uilinkPoint9.OnSpecialInteracts += value19;
				int num18 = num17 - 185;
				if (num18 == 0)
				{
					uilinkPoint9.Up = 306;
				}
				if (num18 == 4)
				{
					uilinkPoint9.Down = 308;
				}
				cp9.LinkMap.Add(num17, uilinkPoint9);
			}
			cp9.UpdateEvent += delegate()
			{
				cp9.LinkMap[184].Down = ((UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0) ? 9000 : 308);
				cp9.LinkMap[189].Down = ((UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0) ? 9000 : 308);
			};
			cp9.IsValidEvent += (() => Main.playerInventory && Main.EquipPage == 2);
			cp9.PageOnLeft = 8;
			cp9.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp9, 7, true);
			UILinkPage cp10 = new UILinkPage();
			cp10.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			cp10.LinkMap.Add(305, new UILinkPoint(305, true, 306, -4, 308, -2));
			cp10.LinkMap.Add(306, new UILinkPoint(306, true, 307, 305, 308, -2));
			cp10.LinkMap.Add(307, new UILinkPoint(307, true, -3, 306, 308, -2));
			cp10.LinkMap.Add(308, new UILinkPoint(308, true, -3, -4, -1, 305));
			cp10.LinkMap[305].OnSpecialInteracts += value;
			cp10.LinkMap[306].OnSpecialInteracts += value;
			cp10.LinkMap[307].OnSpecialInteracts += value;
			cp10.LinkMap[308].OnSpecialInteracts += value;
			cp10.UpdateEvent += delegate()
			{
				switch (Main.EquipPage)
				{
				case 0:
					cp10.LinkMap[305].Down = 100;
					cp10.LinkMap[306].Down = 110;
					cp10.LinkMap[307].Down = 120;
					cp10.LinkMap[308].Up = 108 + Main.player[Main.myPlayer].GetAmountOfExtraAccessorySlotsToShow() - 1;
					break;
				case 1:
				{
					cp10.LinkMap[305].Down = 600;
					cp10.LinkMap[306].Down = ((UILinkPointNavigator.Shortcuts.NPCS_IconsTotal / UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn > 0) ? (600 + UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn) : -2);
					cp10.LinkMap[307].Down = ((UILinkPointNavigator.Shortcuts.NPCS_IconsTotal / UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn > 1) ? (600 + UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn * 2) : -2);
					int num32 = UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn;
					if (num32 == 0)
					{
						num32 = 100;
					}
					if (num32 == 100)
					{
						num32 = UILinkPointNavigator.Shortcuts.NPCS_IconsTotal;
					}
					cp10.LinkMap[308].Up = 600 + num32 - 1;
					break;
				}
				case 2:
					cp10.LinkMap[305].Down = 180;
					cp10.LinkMap[306].Down = 185;
					cp10.LinkMap[307].Down = -2;
					cp10.LinkMap[308].Up = ((UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0) ? 9000 : 184);
					break;
				}
				cp10.PageOnRight = UILinksInitializer.GetCornerWrapPageIdFromRightToLeft();
			};
			cp10.IsValidEvent += (() => Main.playerInventory);
			cp10.PageOnLeft = 0;
			cp10.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(cp10, 8, true);
			UILinkPage cp11 = new UILinkPage();
			cp11.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value20 = () => ItemSlot.GetGamepadInstructions(ref Main.guideItem, 7);
			Func<string> HandleItem2 = delegate()
			{
				if (Main.mouseItem.type < 1)
				{
					return "";
				}
				return ItemSlot.GetGamepadInstructions(ref Main.mouseItem, 22);
			};
			for (int num19 = 1500; num19 < 1550; num19++)
			{
				UILinkPoint uilinkPoint10 = new UILinkPoint(num19, true, num19, num19, -1, -2);
				if (num19 != 1500)
				{
					uilinkPoint10.OnSpecialInteracts += HandleItem2;
				}
				cp11.LinkMap.Add(num19, uilinkPoint10);
			}
			cp11.LinkMap[1500].OnSpecialInteracts += value20;
			cp11.UpdateEvent += delegate()
			{
				int num32 = UILinkPointNavigator.Shortcuts.CRAFT_CurrentIngredientsCount;
				int num33 = num32;
				if (Main.numAvailableRecipes > 0)
				{
					num33 += 2;
				}
				if (num32 < num33)
				{
					num32 = num33;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp11.CurrentPoint > 1500 + num32)
				{
					UILinkPointNavigator.ChangePoint(1500);
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp11.CurrentPoint == 1500 && !Main.InGuideCraftMenu)
				{
					UILinkPointNavigator.ChangePoint(1501);
				}
				for (int num34 = 1; num34 < num32; num34++)
				{
					cp11.LinkMap[1500 + num34].Left = 1500 + num34 - 1;
					cp11.LinkMap[1500 + num34].Right = ((num34 == num32 - 2) ? -4 : (1500 + num34 + 1));
				}
				cp11.LinkMap[1501].Left = -3;
				if (num32 > 0)
				{
					cp11.LinkMap[1500 + num32 - 1].Right = -4;
				}
				cp11.LinkMap[1500].Down = ((num32 >= 2) ? 1502 : -2);
				cp11.LinkMap[1500].Left = ((num32 >= 1) ? 1501 : -3);
				cp11.LinkMap[1502].Up = (Main.InGuideCraftMenu ? 1500 : -1);
			};
			cp11.LinkMap[1501].OnSpecialInteracts += delegate()
			{
				if (Main.InGuideCraftMenu)
				{
					return "";
				}
				string str = "";
				Player player = Main.player[Main.myPlayer];
				bool flag = false;
				Item createItem = Main.recipe[Main.availableRecipe[Main.focusRecipe]].createItem;
				if (Main.mouseItem.type == 0 && createItem.maxStack > 1 && player.ItemSpace(createItem).CanTakeItemToPersonalInventory && !player.HasLockedInventory())
				{
					flag = true;
					if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.Current.Grapple && Main.stackSplit <= 1)
					{
						if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Grapple)
						{
							UILinksInitializer.SomeVarsForUILinkers.SequencedCraftingCurrent = Main.recipe[Main.availableRecipe[Main.focusRecipe]];
						}
						ItemSlot.RefreshStackSplitCooldown();
						Main.preventStackSplitReset = true;
						if (UILinksInitializer.SomeVarsForUILinkers.SequencedCraftingCurrent == Main.recipe[Main.availableRecipe[Main.focusRecipe]])
						{
							Main.CraftItem(Main.recipe[Main.availableRecipe[Main.focusRecipe]]);
							Main.mouseItem = player.GetItem(player.whoAmI, Main.mouseItem, new GetItemSettings(false, true, false, null));
						}
					}
				}
				else if (Main.mouseItem.type > 0 && Main.mouseItem.maxStack == 1 && ItemSlot.Equippable(ref Main.mouseItem, 0))
				{
					str += PlayerInput.BuildCommand(Lang.misc[67].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
					});
					if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Grapple)
					{
						ItemSlot.SwapEquip(ref Main.mouseItem, 0);
						if (Main.player[Main.myPlayer].ItemSpace(Main.mouseItem).CanTakeItemToPersonalInventory)
						{
							Main.mouseItem = player.GetItem(player.whoAmI, Main.mouseItem, GetItemSettings.InventoryUIToInventorySettings);
						}
						PlayerInput.LockGamepadButtons("Grapple");
						PlayerInput.SettingsForUI.TryRevertingToMouseMode();
					}
				}
				bool flag2 = Main.mouseItem.stack <= 0;
				if (flag2 || (Main.mouseItem.type == createItem.type && Main.mouseItem.stack < Main.mouseItem.maxStack))
				{
					if (flag2)
					{
						str += PlayerInput.BuildCommand(Lang.misc[72].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"],
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
						});
					}
					else
					{
						str += PlayerInput.BuildCommand(Lang.misc[72].Value, false, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
						});
					}
				}
				if (!flag2 && Main.mouseItem.type == createItem.type && Main.mouseItem.stack < Main.mouseItem.maxStack)
				{
					str += PlayerInput.BuildCommand(Lang.misc[93].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
					});
				}
				if (flag)
				{
					str += PlayerInput.BuildCommand(Lang.misc[71].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
					});
				}
				return str + HandleItem2();
			};
			cp11.ReachEndEvent += delegate(int current, int next)
			{
				if (current != 1500)
				{
					if (current == 1501)
					{
						if (next == -1)
						{
							if (Main.focusRecipe > 0)
							{
								Main.focusRecipe--;
								return;
							}
						}
						else if (next == -2 && Main.focusRecipe < Main.numAvailableRecipes - 1)
						{
							Main.focusRecipe++;
							return;
						}
					}
					else if (next == -1)
					{
						if (Main.focusRecipe > 0)
						{
							UILinkPointNavigator.ChangePoint(1501);
							Main.focusRecipe--;
							return;
						}
					}
					else if (next == -2 && Main.focusRecipe < Main.numAvailableRecipes - 1)
					{
						UILinkPointNavigator.ChangePoint(1501);
						Main.focusRecipe++;
					}
				}
			};
			cp11.EnterEvent += delegate()
			{
				Main.recBigList = false;
				PlayerInput.LockGamepadButtons("MouseLeft");
			};
			cp11.CanEnterEvent += (() => Main.playerInventory && (Main.numAvailableRecipes > 0 || Main.InGuideCraftMenu));
			cp11.IsValidEvent += (() => Main.playerInventory && (Main.numAvailableRecipes > 0 || Main.InGuideCraftMenu));
			cp11.PageOnLeft = 10;
			cp11.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(cp11, 9, true);
			UILinkPage cp12 = new UILinkPage();
			cp12.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			for (int num20 = 700; num20 < 1500; num20++)
			{
				UILinkPoint uilinkPoint11 = new UILinkPoint(num20, true, num20, num20, num20, num20);
				int IHateLambda = num20;
				uilinkPoint11.OnSpecialInteracts += delegate()
				{
					string text = "";
					bool flag = false;
					Player player = Main.player[Main.myPlayer];
					if (IHateLambda + Main.recStart < Main.numAvailableRecipes)
					{
						int num32 = Main.recStart + IHateLambda - 700;
						if (Main.mouseItem.type == 0 && Main.recipe[Main.availableRecipe[num32]].createItem.maxStack > 1 && player.ItemSpace(Main.recipe[Main.availableRecipe[num32]].createItem).CanTakeItemToPersonalInventory && !player.HasLockedInventory())
						{
							flag = true;
							if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Grapple)
							{
								UILinksInitializer.SomeVarsForUILinkers.SequencedCraftingCurrent = Main.recipe[Main.availableRecipe[num32]];
							}
							if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.Current.Grapple && Main.stackSplit <= 1)
							{
								ItemSlot.RefreshStackSplitCooldown();
								if (UILinksInitializer.SomeVarsForUILinkers.SequencedCraftingCurrent == Main.recipe[Main.availableRecipe[num32]])
								{
									Main.CraftItem(Main.recipe[Main.availableRecipe[num32]]);
									Main.mouseItem = player.GetItem(player.whoAmI, Main.mouseItem, GetItemSettings.InventoryUIToInventorySettings);
								}
							}
						}
					}
					text += PlayerInput.BuildCommand(Lang.misc[73].Value, !flag, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
					});
					if (flag)
					{
						text += PlayerInput.BuildCommand(Lang.misc[71].Value, true, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
						});
					}
					return text;
				};
				cp12.LinkMap.Add(num20, uilinkPoint11);
			}
			cp12.UpdateEvent += delegate()
			{
				int num32 = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerRow;
				int craft_IconsPerColumn = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerColumn;
				if (num32 == 0)
				{
					num32 = 100;
				}
				int num33 = num32 * craft_IconsPerColumn;
				if (num33 > 800)
				{
					num33 = 800;
				}
				if (num33 > Main.numAvailableRecipes)
				{
					num33 = Main.numAvailableRecipes;
				}
				for (int num34 = 0; num34 < num33; num34++)
				{
					cp12.LinkMap[700 + num34].Left = ((num34 % num32 == 0) ? -3 : (700 + num34 - 1));
					cp12.LinkMap[700 + num34].Right = (((num34 + 1) % num32 == 0 || num34 == Main.numAvailableRecipes - 1) ? -4 : (700 + num34 + 1));
					cp12.LinkMap[700 + num34].Down = ((num34 < num33 - num32) ? (700 + num34 + num32) : -2);
					cp12.LinkMap[700 + num34].Up = ((num34 < num32) ? -1 : (700 + num34 - num32));
				}
				cp12.PageOnLeft = UILinksInitializer.GetCornerWrapPageIdFromLeftToRight();
			};
			cp12.ReachEndEvent += delegate(int current, int next)
			{
				int craft_IconsPerRow = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerRow;
				if (next == -1)
				{
					Main.recStart -= craft_IconsPerRow;
					if (Main.recStart < 0)
					{
						Main.recStart = 0;
						return;
					}
				}
				else if (next == -2)
				{
					Main.recStart += craft_IconsPerRow;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					if (Main.recStart > Main.numAvailableRecipes - craft_IconsPerRow)
					{
						Main.recStart = Main.numAvailableRecipes - craft_IconsPerRow;
					}
				}
			};
			cp12.EnterEvent += delegate()
			{
				Main.recBigList = true;
			};
			cp12.LeaveEvent += delegate()
			{
				Main.recBigList = false;
			};
			cp12.CanEnterEvent += (() => Main.playerInventory && Main.numAvailableRecipes > 0);
			cp12.IsValidEvent += (() => Main.playerInventory && Main.recBigList && Main.numAvailableRecipes > 0);
			cp12.PageOnLeft = 0;
			cp12.PageOnRight = 9;
			UILinkPointNavigator.RegisterPage(cp12, 10, true);
			UILinkPage cp13 = new UILinkPage();
			cp13.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			for (int num21 = 2605; num21 < 2620; num21++)
			{
				UILinkPoint uilinkPoint12 = new UILinkPoint(num21, true, num21, num21, num21, num21);
				uilinkPoint12.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[73].Value, true, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
				}));
				cp13.LinkMap.Add(num21, uilinkPoint12);
			}
			cp13.UpdateEvent += delegate()
			{
				int num32 = 5;
				int num33 = 3;
				int num34 = num32 * num33;
				int count = Main.Hairstyles.AvailableHairstyles.Count;
				for (int num35 = 0; num35 < num34; num35++)
				{
					cp13.LinkMap[2605 + num35].Left = ((num35 % num32 == 0) ? -3 : (2605 + num35 - 1));
					cp13.LinkMap[2605 + num35].Right = (((num35 + 1) % num32 == 0 || num35 == count - 1) ? -4 : (2605 + num35 + 1));
					cp13.LinkMap[2605 + num35].Down = ((num35 < num34 - num32) ? (2605 + num35 + num32) : -2);
					cp13.LinkMap[2605 + num35].Up = ((num35 < num32) ? -1 : (2605 + num35 - num32));
				}
			};
			cp13.ReachEndEvent += delegate(int current, int next)
			{
				int num32 = 5;
				if (next == -1)
				{
					Main.hairStart -= num32;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				if (next == -2)
				{
					Main.hairStart += num32;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
			};
			cp13.CanEnterEvent += (() => Main.hairWindow);
			cp13.IsValidEvent += (() => Main.hairWindow);
			cp13.PageOnLeft = 12;
			cp13.PageOnRight = 12;
			UILinkPointNavigator.RegisterPage(cp13, 11, true);
			UILinkPage uilinkPage6 = new UILinkPage();
			uilinkPage6.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			uilinkPage6.LinkMap.Add(2600, new UILinkPoint(2600, true, -3, -4, -1, 2601));
			uilinkPage6.LinkMap.Add(2601, new UILinkPoint(2601, true, -3, -4, 2600, 2602));
			uilinkPage6.LinkMap.Add(2602, new UILinkPoint(2602, true, -3, -4, 2601, 2603));
			uilinkPage6.LinkMap.Add(2603, new UILinkPoint(2603, true, -3, 2604, 2602, -2));
			uilinkPage6.LinkMap.Add(2604, new UILinkPoint(2604, true, 2603, -4, 2602, -2));
			uilinkPage6.UpdateEvent += delegate()
			{
				Vector3 value22 = Main.rgbToHsl(Main.selColor);
				float interfaceDeadzoneX = PlayerInput.CurrentProfile.InterfaceDeadzoneX;
				float num32 = PlayerInput.GamepadThumbstickLeft.X;
				if (num32 < -interfaceDeadzoneX || num32 > interfaceDeadzoneX)
				{
					num32 = MathHelper.Lerp(0f, 0.008333334f, (Math.Abs(num32) - interfaceDeadzoneX) / (1f - interfaceDeadzoneX)) * (float)Math.Sign(num32);
				}
				else
				{
					num32 = 0f;
				}
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				if (currentPoint == 2600)
				{
					Main.hBar = MathHelper.Clamp(Main.hBar + num32, 0f, 1f);
				}
				if (currentPoint == 2601)
				{
					Main.sBar = MathHelper.Clamp(Main.sBar + num32, 0f, 1f);
				}
				if (currentPoint == 2602)
				{
					Main.lBar = MathHelper.Clamp(Main.lBar + num32, 0.15f, 1f);
				}
				Vector3.Clamp(value22, Vector3.Zero, Vector3.One);
				if (num32 != 0f)
				{
					if (Main.hairWindow)
					{
						Main.player[Main.myPlayer].hairColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
			};
			uilinkPage6.CanEnterEvent += (() => Main.hairWindow);
			uilinkPage6.IsValidEvent += (() => Main.hairWindow);
			uilinkPage6.PageOnLeft = 11;
			uilinkPage6.PageOnRight = 11;
			UILinkPointNavigator.RegisterPage(uilinkPage6, 12, true);
			UILinkPage cp14 = new UILinkPage();
			for (int num22 = 0; num22 < 30; num22++)
			{
				cp14.LinkMap.Add(2900 + num22, new UILinkPoint(2900 + num22, true, -3, -4, -1, -2));
				cp14.LinkMap[2900 + num22].OnSpecialInteracts += value;
			}
			cp14.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			cp14.TravelEvent += delegate()
			{
				if (UILinkPointNavigator.CurrentPage == cp14.ID)
				{
					int num32 = cp14.CurrentPoint - 2900;
					if (num32 < 5)
					{
						IngameOptions.category = num32;
					}
				}
			};
			cp14.UpdateEvent += delegate()
			{
				int num32 = UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_LEFT;
				if (num32 == 0)
				{
					num32 = 5;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp14.CurrentPoint < 2930 && cp14.CurrentPoint > 2900 + num32 - 1)
				{
					UILinkPointNavigator.ChangePoint(2900);
				}
				for (int num33 = 2900; num33 < 2900 + num32; num33++)
				{
					cp14.LinkMap[num33].Up = num33 - 1;
					cp14.LinkMap[num33].Down = num33 + 1;
				}
				cp14.LinkMap[2900].Up = 2900 + num32 - 1;
				cp14.LinkMap[2900 + num32 - 1].Down = 2900;
				int num34 = cp14.CurrentPoint - 2900;
				if (num34 < 4 && UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.MouseLeft)
				{
					IngameOptions.category = num34;
					UILinkPointNavigator.ChangePage(1002);
				}
				int num35 = (SocialAPI.Network != null && SocialAPI.Network.CanInvite()) ? 1 : 0;
				if (num34 == 4 + num35 && UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.MouseLeft)
				{
					UILinkPointNavigator.ChangePage(1004);
				}
			};
			cp14.EnterEvent += delegate()
			{
				cp14.CurrentPoint = 2900 + IngameOptions.category;
			};
			cp14.PageOnLeft = (cp14.PageOnRight = 1002);
			cp14.IsValidEvent += (() => Main.ingameOptionsWindow && !Main.InGameUI.IsVisible);
			cp14.CanEnterEvent += (() => Main.ingameOptionsWindow && !Main.InGameUI.IsVisible);
			UILinkPointNavigator.RegisterPage(cp14, 1001, true);
			UILinkPage cp15 = new UILinkPage();
			for (int num23 = 0; num23 < 30; num23++)
			{
				cp15.LinkMap.Add(2930 + num23, new UILinkPoint(2930 + num23, true, -3, -4, -1, -2));
				cp15.LinkMap[2930 + num23].OnSpecialInteracts += value;
			}
			cp15.EnterEvent += delegate()
			{
				Main.mouseLeftRelease = false;
			};
			cp15.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			cp15.UpdateEvent += delegate()
			{
				int num32 = UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_RIGHT;
				if (num32 == 0)
				{
					num32 = 5;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp15.CurrentPoint >= 2930 && cp15.CurrentPoint > 2930 + num32 - 1)
				{
					UILinkPointNavigator.ChangePoint(2930);
				}
				for (int num33 = 2930; num33 < 2930 + num32; num33++)
				{
					cp15.LinkMap[num33].Up = num33 - 1;
					cp15.LinkMap[num33].Down = num33 + 1;
				}
				cp15.LinkMap[2930].Up = -1;
				cp15.LinkMap[2930 + num32 - 1].Down = -2;
				UILinksInitializer.HandleOptionsSpecials();
			};
			cp15.PageOnLeft = (cp15.PageOnRight = 1001);
			cp15.IsValidEvent += (() => Main.ingameOptionsWindow);
			cp15.CanEnterEvent += (() => Main.ingameOptionsWindow);
			UILinkPointNavigator.RegisterPage(cp15, 1002, true);
			UILinkPage cp16 = new UILinkPage();
			cp16.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			for (int num24 = 1550; num24 < 1558; num24++)
			{
				UILinkPoint uilinkPoint13 = new UILinkPoint(num24, true, -3, -4, -1, -2);
				switch (num24 - 1550)
				{
				case 1:
				case 3:
				case 5:
					uilinkPoint13.Up = uilinkPoint13.ID - 2;
					uilinkPoint13.Down = uilinkPoint13.ID + 2;
					uilinkPoint13.Right = uilinkPoint13.ID + 1;
					break;
				case 2:
				case 4:
				case 6:
					uilinkPoint13.Up = uilinkPoint13.ID - 2;
					uilinkPoint13.Down = uilinkPoint13.ID + 2;
					uilinkPoint13.Left = uilinkPoint13.ID - 1;
					break;
				}
				cp16.LinkMap.Add(num24, uilinkPoint13);
			}
			cp16.LinkMap[1550].Down = 1551;
			cp16.LinkMap[1550].Right = 120;
			cp16.LinkMap[1550].Up = 307;
			cp16.LinkMap[1551].Up = 1550;
			cp16.LinkMap[1552].Up = 1550;
			cp16.LinkMap[1552].Right = 121;
			cp16.LinkMap[1554].Right = 121;
			cp16.LinkMap[1555].Down = 1570;
			cp16.LinkMap[1556].Down = 1570;
			cp16.LinkMap[1556].Right = 122;
			cp16.LinkMap[1557].Up = 1570;
			cp16.LinkMap[1557].Down = 308;
			cp16.LinkMap[1557].Right = 127;
			cp16.LinkMap.Add(1570, new UILinkPoint(1570, true, -3, -4, -1, -2));
			cp16.LinkMap[1570].Up = 1555;
			cp16.LinkMap[1570].Down = 1557;
			cp16.LinkMap[1570].Right = 126;
			for (int num25 = 0; num25 < 7; num25++)
			{
				cp16.LinkMap[1550 + num25].OnSpecialInteracts += value;
			}
			cp16.UpdateEvent += delegate()
			{
				if (!Main.ShouldPVPDraw)
				{
					if (UILinkPointNavigator.OverridePoint == -1 && cp16.CurrentPoint != 1557 && cp16.CurrentPoint != 1570)
					{
						UILinkPointNavigator.ChangePoint(1557);
					}
					cp16.LinkMap[1570].Up = -1;
					cp16.LinkMap[1557].Down = 308;
					cp16.LinkMap[1557].Right = 127;
				}
				else
				{
					cp16.LinkMap[1570].Up = 1555;
					cp16.LinkMap[1557].Down = 308;
					cp16.LinkMap[1557].Right = 127;
				}
				int infoacccount = UILinkPointNavigator.Shortcuts.INFOACCCOUNT;
				if (infoacccount > 0)
				{
					cp16.LinkMap[1570].Up = 1558 + (infoacccount - 1) / 2 * 2;
				}
				if (Main.ShouldPVPDraw)
				{
					if (infoacccount >= 1)
					{
						cp16.LinkMap[1555].Down = 1558;
						cp16.LinkMap[1556].Down = 1558;
					}
					else
					{
						cp16.LinkMap[1555].Down = 1570;
						cp16.LinkMap[1556].Down = 1570;
					}
					if (infoacccount >= 2)
					{
						cp16.LinkMap[1556].Down = 1559;
						return;
					}
					cp16.LinkMap[1556].Down = 1570;
				}
			};
			cp16.IsValidEvent += (() => Main.playerInventory);
			cp16.PageOnLeft = 8;
			cp16.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp16, 16, true);
			UILinkPage cp17 = new UILinkPage();
			cp17.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			for (int num26 = 1558; num26 < 1570; num26++)
			{
				UILinkPoint uilinkPoint14 = new UILinkPoint(num26, true, -3, -4, -1, -2);
				uilinkPoint14.OnSpecialInteracts += value;
				switch (num26 - 1558)
				{
				case 1:
				case 3:
				case 5:
					uilinkPoint14.Up = uilinkPoint14.ID - 2;
					uilinkPoint14.Down = uilinkPoint14.ID + 2;
					uilinkPoint14.Right = uilinkPoint14.ID + 1;
					break;
				case 2:
				case 4:
				case 6:
					uilinkPoint14.Up = uilinkPoint14.ID - 2;
					uilinkPoint14.Down = uilinkPoint14.ID + 2;
					uilinkPoint14.Left = uilinkPoint14.ID - 1;
					break;
				}
				cp17.LinkMap.Add(num26, uilinkPoint14);
			}
			cp17.UpdateEvent += delegate()
			{
				int infoacccount = UILinkPointNavigator.Shortcuts.INFOACCCOUNT;
				if (UILinkPointNavigator.OverridePoint == -1 && cp17.CurrentPoint - 1558 >= infoacccount)
				{
					UILinkPointNavigator.ChangePoint(1558 + infoacccount - 1);
				}
				for (int num32 = 0; num32 < infoacccount; num32++)
				{
					bool flag = num32 % 2 == 0;
					int num33 = num32 + 1558;
					cp17.LinkMap[num33].Down = ((num32 < infoacccount - 2) ? (num33 + 2) : 1570);
					cp17.LinkMap[num33].Up = ((num32 > 1) ? (num33 - 2) : (Main.ShouldPVPDraw ? (flag ? 1555 : 1556) : -1));
					cp17.LinkMap[num33].Right = ((flag && num32 + 1 < infoacccount) ? (num33 + 1) : (123 + num32 / 4));
					cp17.LinkMap[num33].Left = (flag ? -3 : (num33 - 1));
				}
			};
			cp17.IsValidEvent += (() => Main.playerInventory && UILinkPointNavigator.Shortcuts.INFOACCCOUNT > 0);
			cp17.PageOnLeft = 8;
			cp17.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp17, 17, true);
			UILinkPage cp18 = new UILinkPage();
			cp18.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			int num27 = 6000;
			while (num27 < 6012)
			{
				UILinkPoint uilinkPoint15 = new UILinkPoint(num27, true, -3, -4, -1, -2);
				switch (num27)
				{
				case 6000:
					uilinkPoint15.Right = 0;
					break;
				case 6001:
				case 6002:
					uilinkPoint15.Right = 10;
					break;
				case 6003:
				case 6004:
					uilinkPoint15.Right = 20;
					break;
				case 6005:
				case 6006:
					uilinkPoint15.Right = 30;
					break;
				case 6007:
				case 6008:
				case 6009:
					goto IL_2C14;
				default:
					goto IL_2C14;
				}
				IL_2C1D:
				cp18.LinkMap.Add(num27, uilinkPoint15);
				num27++;
				continue;
				IL_2C14:
				uilinkPoint15.Right = 40;
				goto IL_2C1D;
			}
			cp18.UpdateEvent += delegate()
			{
				int builderacccount = UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT;
				if (UILinkPointNavigator.OverridePoint == -1 && cp18.CurrentPoint - 6000 >= builderacccount)
				{
					UILinkPointNavigator.ChangePoint(6000 + builderacccount - 1);
				}
				for (int num32 = 0; num32 < builderacccount; num32++)
				{
					int num33 = num32 % 2;
					int num34 = num32 + 6000;
					cp18.LinkMap[num34].Down = ((num32 < builderacccount - 1) ? (num34 + 1) : -2);
					cp18.LinkMap[num34].Up = ((num32 > 0) ? (num34 - 1) : -1);
				}
			};
			cp18.IsValidEvent += (() => Main.playerInventory && UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 0);
			cp18.PageOnLeft = 8;
			cp18.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp18, 18, true);
			UILinkPage uilinkPage7 = new UILinkPage();
			uilinkPage7.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			uilinkPage7.LinkMap.Add(2806, new UILinkPoint(2806, true, 2805, 2807, -1, 2808));
			uilinkPage7.LinkMap.Add(2807, new UILinkPoint(2807, true, 2806, 2810, -1, 2809));
			uilinkPage7.LinkMap.Add(2808, new UILinkPoint(2808, true, 2805, 2809, 2806, -2));
			uilinkPage7.LinkMap.Add(2809, new UILinkPoint(2809, true, 2808, 2811, 2807, -2));
			uilinkPage7.LinkMap.Add(2810, new UILinkPoint(2810, true, 2807, -4, -1, 2811));
			uilinkPage7.LinkMap.Add(2811, new UILinkPoint(2811, true, 2809, -4, 2810, -2));
			uilinkPage7.LinkMap.Add(2805, new UILinkPoint(2805, true, -3, 2806, -1, -2));
			uilinkPage7.LinkMap[2806].OnSpecialInteracts += value;
			uilinkPage7.LinkMap[2807].OnSpecialInteracts += value;
			uilinkPage7.LinkMap[2808].OnSpecialInteracts += value;
			uilinkPage7.LinkMap[2809].OnSpecialInteracts += value;
			uilinkPage7.LinkMap[2805].OnSpecialInteracts += value;
			uilinkPage7.CanEnterEvent += (() => Main.clothesWindow);
			uilinkPage7.IsValidEvent += (() => Main.clothesWindow);
			uilinkPage7.EnterEvent += delegate()
			{
				Main.player[Main.myPlayer].releaseInventory = false;
			};
			uilinkPage7.LeaveEvent += delegate()
			{
				Main.player[Main.myPlayer].LockGamepadTileInteractions();
			};
			uilinkPage7.PageOnLeft = 15;
			uilinkPage7.PageOnRight = 15;
			UILinkPointNavigator.RegisterPage(uilinkPage7, 14, true);
			UILinkPage uilinkPage8 = new UILinkPage();
			uilinkPage8.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			uilinkPage8.LinkMap.Add(2800, new UILinkPoint(2800, true, -3, -4, -1, 2801));
			uilinkPage8.LinkMap.Add(2801, new UILinkPoint(2801, true, -3, -4, 2800, 2802));
			uilinkPage8.LinkMap.Add(2802, new UILinkPoint(2802, true, -3, -4, 2801, 2803));
			uilinkPage8.LinkMap.Add(2803, new UILinkPoint(2803, true, -3, 2804, 2802, -2));
			uilinkPage8.LinkMap.Add(2804, new UILinkPoint(2804, true, 2803, -4, 2802, -2));
			uilinkPage8.LinkMap[2800].OnSpecialInteracts += value;
			uilinkPage8.LinkMap[2801].OnSpecialInteracts += value;
			uilinkPage8.LinkMap[2802].OnSpecialInteracts += value;
			uilinkPage8.LinkMap[2803].OnSpecialInteracts += value;
			uilinkPage8.LinkMap[2804].OnSpecialInteracts += value;
			uilinkPage8.UpdateEvent += delegate()
			{
				Vector3 value22 = Main.rgbToHsl(Main.selColor);
				float interfaceDeadzoneX = PlayerInput.CurrentProfile.InterfaceDeadzoneX;
				float num32 = PlayerInput.GamepadThumbstickLeft.X;
				if (num32 < -interfaceDeadzoneX || num32 > interfaceDeadzoneX)
				{
					num32 = MathHelper.Lerp(0f, 0.008333334f, (Math.Abs(num32) - interfaceDeadzoneX) / (1f - interfaceDeadzoneX)) * (float)Math.Sign(num32);
				}
				else
				{
					num32 = 0f;
				}
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				if (currentPoint == 2800)
				{
					Main.hBar = MathHelper.Clamp(Main.hBar + num32, 0f, 1f);
				}
				if (currentPoint == 2801)
				{
					Main.sBar = MathHelper.Clamp(Main.sBar + num32, 0f, 1f);
				}
				if (currentPoint == 2802)
				{
					Main.lBar = MathHelper.Clamp(Main.lBar + num32, 0.15f, 1f);
				}
				Vector3.Clamp(value22, Vector3.Zero, Vector3.One);
				if (num32 != 0f)
				{
					if (Main.clothesWindow)
					{
						Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue);
						switch (Main.selClothes)
						{
						case 0:
							Main.player[Main.myPlayer].shirtColor = Main.selColor;
							break;
						case 1:
							Main.player[Main.myPlayer].underShirtColor = Main.selColor;
							break;
						case 2:
							Main.player[Main.myPlayer].pantsColor = Main.selColor;
							break;
						case 3:
							Main.player[Main.myPlayer].shoeColor = Main.selColor;
							break;
						}
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
			};
			uilinkPage8.CanEnterEvent += (() => Main.clothesWindow);
			uilinkPage8.IsValidEvent += (() => Main.clothesWindow);
			uilinkPage8.EnterEvent += delegate()
			{
				Main.player[Main.myPlayer].releaseInventory = false;
			};
			uilinkPage8.LeaveEvent += delegate()
			{
				Main.player[Main.myPlayer].LockGamepadTileInteractions();
			};
			uilinkPage8.PageOnLeft = 14;
			uilinkPage8.PageOnRight = 14;
			UILinkPointNavigator.RegisterPage(uilinkPage8, 15, true);
			UILinkPage cp19 = new UILinkPage();
			cp19.UpdateEvent += delegate()
			{
				PlayerInput.GamepadAllowScrolling = true;
			};
			for (int num28 = 3000; num28 <= 4999; num28++)
			{
				cp19.LinkMap.Add(num28, new UILinkPoint(num28, true, -3, -4, -1, -2));
			}
			cp19.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[53].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
			}) + PlayerInput.BuildCommand(Lang.misc[82].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + UILinksInitializer.FancyUISpecialInstructions());
			cp19.UpdateEvent += delegate()
			{
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Inventory)
				{
					UILinksInitializer.FancyExit();
				}
				UILinkPointNavigator.Shortcuts.BackButtonInUse = false;
			};
			cp19.EnterEvent += delegate()
			{
				cp19.CurrentPoint = 3002;
			};
			cp19.CanEnterEvent += (() => Main.MenuUI.IsVisible || Main.InGameUI.IsVisible);
			cp19.IsValidEvent += (() => Main.MenuUI.IsVisible || Main.InGameUI.IsVisible);
			cp19.OnPageMoveAttempt += UILinksInitializer.OnFancyUIPageMoveAttempt;
			UILinkPointNavigator.RegisterPage(cp19, 1004, true);
			UILinkPage cp20 = new UILinkPage();
			cp20.UpdateEvent += delegate()
			{
				PlayerInput.GamepadAllowScrolling = true;
			};
			for (int num29 = 10000; num29 <= 11000; num29++)
			{
				cp20.LinkMap.Add(num29, new UILinkPoint(num29, true, -3, -4, -1, -2));
			}
			for (int num30 = 15000; num30 <= 15000; num30++)
			{
				cp20.LinkMap.Add(num30, new UILinkPoint(num30, true, -3, -4, -1, -2));
			}
			cp20.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[53].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
			}) + PlayerInput.BuildCommand(Lang.misc[82].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + UILinksInitializer.FancyUISpecialInstructions());
			cp20.UpdateEvent += delegate()
			{
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Inventory)
				{
					UILinksInitializer.FancyExit();
				}
				UILinkPointNavigator.Shortcuts.BackButtonInUse = false;
			};
			cp20.EnterEvent += delegate()
			{
				cp20.CurrentPoint = 10000;
			};
			cp20.CanEnterEvent += UILinksInitializer.CanEnterCreativeMenu;
			cp20.IsValidEvent += UILinksInitializer.CanEnterCreativeMenu;
			cp20.OnPageMoveAttempt += UILinksInitializer.OnFancyUIPageMoveAttempt;
			cp20.PageOnLeft = 8;
			cp20.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(cp20, 1005, true);
			UILinkPage cp = new UILinkPage();
			cp.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value21 = () => PlayerInput.BuildCommand(Lang.misc[94].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
			});
			for (int num31 = 9000; num31 <= 9050; num31++)
			{
				UILinkPoint uilinkPoint16 = new UILinkPoint(num31, true, num31 + 10, num31 - 10, num31 - 1, num31 + 1);
				cp.LinkMap.Add(num31, uilinkPoint16);
				uilinkPoint16.OnSpecialInteracts += value21;
			}
			cp.UpdateEvent += delegate()
			{
				int num32 = UILinkPointNavigator.Shortcuts.BUFFS_PER_COLUMN;
				if (num32 == 0)
				{
					num32 = 100;
				}
				for (int num33 = 0; num33 < 50; num33++)
				{
					cp.LinkMap[9000 + num33].Up = ((num33 % num32 == 0) ? -1 : (9000 + num33 - 1));
					if (cp.LinkMap[9000 + num33].Up == -1)
					{
						if (num33 >= num32)
						{
							cp.LinkMap[9000 + num33].Up = 184;
						}
						else
						{
							cp.LinkMap[9000 + num33].Up = 189;
						}
					}
					cp.LinkMap[9000 + num33].Down = (((num33 + 1) % num32 == 0 || num33 == UILinkPointNavigator.Shortcuts.BUFFS_DRAWN - 1) ? 308 : (9000 + num33 + 1));
					cp.LinkMap[9000 + num33].Left = ((num33 < UILinkPointNavigator.Shortcuts.BUFFS_DRAWN - num32) ? (9000 + num33 + num32) : -3);
					cp.LinkMap[9000 + num33].Right = ((num33 < num32) ? -4 : (9000 + num33 - num32));
				}
			};
			cp.IsValidEvent += (() => Main.playerInventory && Main.EquipPage == 2 && UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0);
			cp.PageOnLeft = 8;
			cp.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp, 19, true);
			UILinkPage uilinkPage9 = UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage];
			uilinkPage9.CurrentPoint = uilinkPage9.DefaultPoint;
			uilinkPage9.Enter();
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x004C0B8A File Offset: 0x004BED8A
		private static bool CanEnterCreativeMenu()
		{
			return Main.LocalPlayer.chest == -1 && Main.LocalPlayer.talkNPC == -1 && Main.playerInventory && Main.CreativeMenu.Enabled;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x004C0BBD File Offset: 0x004BEDBD
		private static int GetCornerWrapPageIdFromLeftToRight()
		{
			return 8;
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x004C0BC0 File Offset: 0x004BEDC0
		private static int GetCornerWrapPageIdFromRightToLeft()
		{
			if (Main.CreativeMenu.Enabled)
			{
				return 1005;
			}
			return 10;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x004C0BD8 File Offset: 0x004BEDD8
		private static void OnFancyUIPageMoveAttempt(int direction)
		{
			UICharacterCreation uicharacterCreation = Main.MenuUI.CurrentState as UICharacterCreation;
			if (uicharacterCreation != null)
			{
				uicharacterCreation.TryMovingCategory(direction);
			}
			UIBestiaryTest uibestiaryTest = UserInterface.ActiveInstance.CurrentState as UIBestiaryTest;
			if (uibestiaryTest != null)
			{
				uibestiaryTest.TryMovingPages(direction);
			}
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x004C0C1C File Offset: 0x004BEE1C
		public static void FancyExit()
		{
			switch (UILinkPointNavigator.Shortcuts.BackButtonCommand)
			{
			case 1:
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Main.menuMode = 0;
				return;
			case 2:
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Main.menuMode = (Main.menuMultiplayer ? 12 : 1);
				return;
			case 3:
				Main.menuMode = 0;
				IngameFancyUI.Close();
				return;
			case 4:
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Main.menuMode = 11;
				return;
			case 5:
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Main.menuMode = 11;
				return;
			case 6:
				UIVirtualKeyboard.Cancel();
				return;
			case 7:
			{
				IHaveBackButtonCommand haveBackButtonCommand = Main.MenuUI.CurrentState as IHaveBackButtonCommand;
				if (haveBackButtonCommand != null)
				{
					haveBackButtonCommand.HandleBackButtonUsage();
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x004C0CFC File Offset: 0x004BEEFC
		public static string FancyUISpecialInstructions()
		{
			string text = "";
			int fancyui_SPECIAL_INSTRUCTIONS = UILinkPointNavigator.Shortcuts.FANCYUI_SPECIAL_INSTRUCTIONS;
			if (fancyui_SPECIAL_INSTRUCTIONS == 1)
			{
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.HotbarMinus)
				{
					UIVirtualKeyboard.CycleSymbols();
					PlayerInput.LockGamepadButtons("HotbarMinus");
					PlayerInput.SettingsForUI.TryRevertingToMouseMode();
				}
				text += PlayerInput.BuildCommand(Lang.menu[235].Value, false, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"]
				});
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.MouseRight)
				{
					UIVirtualKeyboard.BackSpace();
					PlayerInput.LockGamepadButtons("MouseRight");
					PlayerInput.SettingsForUI.TryRevertingToMouseMode();
				}
				text += PlayerInput.BuildCommand(Lang.menu[236].Value, false, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
				});
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.SmartCursor)
				{
					UIVirtualKeyboard.Write(" ");
					PlayerInput.LockGamepadButtons("SmartCursor");
					PlayerInput.SettingsForUI.TryRevertingToMouseMode();
				}
				text += PlayerInput.BuildCommand(Lang.menu[238].Value, false, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["SmartCursor"]
				});
				if (UIVirtualKeyboard.CanSubmit)
				{
					if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.HotbarPlus)
					{
						UIVirtualKeyboard.Submit();
						PlayerInput.LockGamepadButtons("HotbarPlus");
						PlayerInput.SettingsForUI.TryRevertingToMouseMode();
					}
					text += PlayerInput.BuildCommand(Lang.menu[237].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
					});
				}
			}
			return text;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x004C0EC4 File Offset: 0x004BF0C4
		public static void HandleOptionsSpecials()
		{
			switch (UILinkPointNavigator.Shortcuts.OPTIONS_BUTTON_SPECIALFEATURE)
			{
			case 1:
				Main.bgScroll = (int)UILinksInitializer.HandleSliderHorizontalInput((float)Main.bgScroll, 0f, 100f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 1f);
				Main.caveParallax = 1f - (float)Main.bgScroll / 500f;
				return;
			case 2:
				Main.musicVolume = UILinksInitializer.HandleSliderHorizontalInput(Main.musicVolume, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				return;
			case 3:
				Main.soundVolume = UILinksInitializer.HandleSliderHorizontalInput(Main.soundVolume, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				return;
			case 4:
				Main.ambientVolume = UILinksInitializer.HandleSliderHorizontalInput(Main.ambientVolume, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				return;
			case 5:
			{
				float hBar = Main.hBar;
				float num = Main.hBar = UILinksInitializer.HandleSliderHorizontalInput(hBar, 0f, 1f, 0.2f, 0.5f);
				if (hBar != num)
				{
					int menuMode = Main.menuMode;
					switch (menuMode)
					{
					case 17:
						Main.player[Main.myPlayer].hairColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 18:
						Main.player[Main.myPlayer].eyeColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 19:
						Main.player[Main.myPlayer].skinColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 20:
						break;
					case 21:
						Main.player[Main.myPlayer].shirtColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 22:
						Main.player[Main.myPlayer].underShirtColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 23:
						Main.player[Main.myPlayer].pantsColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 24:
						Main.player[Main.myPlayer].shoeColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 25:
						Main.mouseColorSlider.Hue = num;
						break;
					default:
						if (menuMode == 252)
						{
							Main.mouseBorderColorSlider.Hue = num;
						}
						break;
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				break;
			}
			case 6:
			{
				float sBar = Main.sBar;
				float num2 = Main.sBar = UILinksInitializer.HandleSliderHorizontalInput(sBar, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f);
				if (sBar != num2)
				{
					int menuMode = Main.menuMode;
					switch (menuMode)
					{
					case 17:
						Main.player[Main.myPlayer].hairColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 18:
						Main.player[Main.myPlayer].eyeColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 19:
						Main.player[Main.myPlayer].skinColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 20:
						break;
					case 21:
						Main.player[Main.myPlayer].shirtColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 22:
						Main.player[Main.myPlayer].underShirtColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 23:
						Main.player[Main.myPlayer].pantsColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 24:
						Main.player[Main.myPlayer].shoeColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 25:
						Main.mouseColorSlider.Saturation = num2;
						break;
					default:
						if (menuMode == 252)
						{
							Main.mouseBorderColorSlider.Saturation = num2;
						}
						break;
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				break;
			}
			case 7:
			{
				float lBar = Main.lBar;
				float min = 0.15f;
				if (Main.menuMode == 252)
				{
					min = 0f;
				}
				Main.lBar = UILinksInitializer.HandleSliderHorizontalInput(lBar, min, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f);
				float lBar2 = Main.lBar;
				if (lBar != lBar2)
				{
					int menuMode = Main.menuMode;
					switch (menuMode)
					{
					case 17:
						Main.player[Main.myPlayer].hairColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 18:
						Main.player[Main.myPlayer].eyeColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 19:
						Main.player[Main.myPlayer].skinColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 20:
						break;
					case 21:
						Main.player[Main.myPlayer].shirtColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 22:
						Main.player[Main.myPlayer].underShirtColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 23:
						Main.player[Main.myPlayer].pantsColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 24:
						Main.player[Main.myPlayer].shoeColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
						break;
					case 25:
						Main.mouseColorSlider.Luminance = lBar2;
						break;
					default:
						if (menuMode == 252)
						{
							Main.mouseBorderColorSlider.Luminance = lBar2;
						}
						break;
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				break;
			}
			case 8:
			{
				float aBar = Main.aBar;
				float num3 = Main.aBar = UILinksInitializer.HandleSliderHorizontalInput(aBar, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f);
				if (aBar != num3)
				{
					int menuMode = Main.menuMode;
					if (menuMode == 252)
					{
						Main.mouseBorderColorSlider.Alpha = num3;
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					return;
				}
				break;
			}
			case 9:
			{
				bool left = PlayerInput.Triggers.Current.Left;
				bool right = PlayerInput.Triggers.Current.Right;
				if (PlayerInput.Triggers.JustPressed.Left || PlayerInput.Triggers.JustPressed.Right)
				{
					UILinksInitializer.SomeVarsForUILinkers.HairMoveCD = 0;
				}
				else if (UILinksInitializer.SomeVarsForUILinkers.HairMoveCD > 0)
				{
					UILinksInitializer.SomeVarsForUILinkers.HairMoveCD--;
				}
				if (UILinksInitializer.SomeVarsForUILinkers.HairMoveCD == 0 && (left || right))
				{
					if (left)
					{
						Main.PendingPlayer.hair--;
					}
					if (right)
					{
						Main.PendingPlayer.hair++;
					}
					UILinksInitializer.SomeVarsForUILinkers.HairMoveCD = 12;
				}
				int num4 = 51;
				if (Main.PendingPlayer.hair >= num4)
				{
					Main.PendingPlayer.hair = 0;
				}
				if (Main.PendingPlayer.hair < 0)
				{
					Main.PendingPlayer.hair = num4 - 1;
					return;
				}
				break;
			}
			case 10:
				Main.GameZoomTarget = UILinksInitializer.HandleSliderHorizontalInput(Main.GameZoomTarget, 1f, 2f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				return;
			case 11:
				Main.UIScale = UILinksInitializer.HandleSliderHorizontalInput(Main.UIScaleWanted, 0.5f, 2f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.35f);
				Main.temporaryGUIScaleSlider = Main.UIScaleWanted;
				return;
			case 12:
				Main.MapScale = UILinksInitializer.HandleSliderHorizontalInput(Main.MapScale, 0.5f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.7f);
				break;
			default:
				return;
			}
		}

		// Token: 0x02000570 RID: 1392
		public class SomeVarsForUILinkers
		{
			// Token: 0x0400593B RID: 22843
			public static Recipe SequencedCraftingCurrent;

			// Token: 0x0400593C RID: 22844
			public static int HairMoveCD;
		}
	}
}
