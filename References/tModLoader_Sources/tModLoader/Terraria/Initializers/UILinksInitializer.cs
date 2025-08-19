using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Terraria.Initializers
{
	// Token: 0x020003F2 RID: 1010
	public class UILinksInitializer
	{
		// Token: 0x060034F3 RID: 13555 RVA: 0x00567695 File Offset: 0x00565895
		public static bool NothingMoreImportantThanNPCChat()
		{
			return !Main.hairWindow && Main.npcShop == 0 && Main.player[Main.myPlayer].chest == -1;
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x005676BC File Offset: 0x005658BC
		public static float HandleSliderHorizontalInput(float currentValue, float min, float max, float deadZone = 0.2f, float sensitivity = 0.5f)
		{
			float x = PlayerInput.GamepadThumbstickLeft.X;
			x = ((x >= 0f - deadZone && x <= deadZone) ? 0f : (MathHelper.Lerp(0f, sensitivity / 60f, (Math.Abs(x) - deadZone) / (1f - deadZone)) * (float)Math.Sign(x)));
			return MathHelper.Clamp((currentValue - min) / (max - min) + x, 0f, 1f) * (max - min) + min;
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x00567734 File Offset: 0x00565934
		public static float HandleSliderVerticalInput(float currentValue, float min, float max, float deadZone = 0.2f, float sensitivity = 0.5f)
		{
			float num = 0f - PlayerInput.GamepadThumbstickLeft.Y;
			num = ((num >= 0f - deadZone && num <= deadZone) ? 0f : (MathHelper.Lerp(0f, sensitivity / 60f, (Math.Abs(num) - deadZone) / (1f - deadZone)) * (float)Math.Sign(num)));
			return MathHelper.Clamp((currentValue - min) / (max - min) + num, 0f, 1f) * (max - min) + min;
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x005677B0 File Offset: 0x005659B0
		public static bool CanExecuteInputCommand()
		{
			return PlayerInput.AllowExecutionOfGamepadInstructions;
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x005677B8 File Offset: 0x005659B8
		public static void Load()
		{
			Func<string> value = () => PlayerInput.BuildCommand(Lang.misc[53].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
			});
			UILinkPage uILinkPage = new UILinkPage();
			uILinkPage.UpdateEvent += delegate()
			{
				PlayerInput.GamepadAllowScrolling = true;
			};
			for (int i = 0; i < 20; i++)
			{
				uILinkPage.LinkMap.Add(2000 + i, new UILinkPoint(2000 + i, true, -3, -4, -1, -2));
			}
			uILinkPage.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[53].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
			}) + PlayerInput.BuildCommand(Lang.misc[82].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}));
			uILinkPage.UpdateEvent += delegate()
			{
				bool flag11 = PlayerInput.Triggers.JustPressed.Inventory;
				if (Main.inputTextEscape)
				{
					Main.inputTextEscape = false;
					flag11 = true;
				}
				if (UILinksInitializer.CanExecuteInputCommand() && flag11)
				{
					UILinksInitializer.FancyExit();
				}
				UILinkPointNavigator.Shortcuts.BackButtonInUse = flag11;
				UILinksInitializer.HandleOptionsSpecials();
			};
			uILinkPage.IsValidEvent += (() => Main.gameMenu && !Main.MenuUI.IsVisible);
			uILinkPage.CanEnterEvent += (() => Main.gameMenu && !Main.MenuUI.IsVisible);
			UILinkPointNavigator.RegisterPage(uILinkPage, 1000, true);
			UILinkPage cp20 = new UILinkPage();
			cp20.LinkMap.Add(2500, new UILinkPoint(2500, true, -3, 2501, -1, -2));
			cp20.LinkMap.Add(2501, new UILinkPoint(2501, true, 2500, 2502, -1, -2));
			cp20.LinkMap.Add(2502, new UILinkPoint(2502, true, 2501, 2503, -1, -2));
			cp20.LinkMap.Add(2503, new UILinkPoint(2503, true, 2502, -4, -1, -2));
			cp20.UpdateEvent += delegate()
			{
				cp20.LinkMap[2501].Right = (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight ? 2502 : -4);
				if (cp20.LinkMap[2501].Right == -4 && UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight2)
				{
					cp20.LinkMap[2501].Right = 2503;
				}
				cp20.LinkMap[2502].Right = (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight2 ? 2503 : -4);
				cp20.LinkMap[2503].Left = (UILinkPointNavigator.Shortcuts.NPCCHAT_ButtonsRight ? 2502 : 2501);
			};
			cp20.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[53].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
			}) + PlayerInput.BuildCommand(Lang.misc[56].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}));
			cp20.IsValidEvent += (() => (Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1) && UILinksInitializer.NothingMoreImportantThanNPCChat());
			cp20.CanEnterEvent += (() => (Main.player[Main.myPlayer].talkNPC != -1 || Main.player[Main.myPlayer].sign != -1) && UILinksInitializer.NothingMoreImportantThanNPCChat());
			cp20.EnterEvent += delegate()
			{
				Main.player[Main.myPlayer].releaseInventory = false;
			};
			cp20.LeaveEvent += delegate()
			{
				Main.npcChatRelease = false;
				Main.player[Main.myPlayer].LockGamepadTileInteractions();
			};
			UILinkPointNavigator.RegisterPage(cp20, 1003, true);
			UILinkPage cp19 = new UILinkPage();
			cp19.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value2 = delegate()
			{
				int currentPoint5 = UILinkPointNavigator.CurrentPoint;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].inventory, 0, currentPoint5);
			};
			Func<string> value3 = () => ItemSlot.GetGamepadInstructions(ref Main.player[Main.myPlayer].trashItem, 6);
			for (int j = 0; j <= 49; j++)
			{
				UILinkPoint uILinkPoint = new UILinkPoint(j, true, j - 1, j + 1, j - 10, j + 10);
				uILinkPoint.OnSpecialInteracts += value2;
				int num29 = j;
				if (num29 < 10)
				{
					uILinkPoint.Up = -1;
				}
				if (num29 >= 40)
				{
					uILinkPoint.Down = -2;
				}
				if (num29 % 10 == 9)
				{
					uILinkPoint.Right = -4;
				}
				if (num29 % 10 == 0)
				{
					uILinkPoint.Left = -3;
				}
				cp19.LinkMap.Add(j, uILinkPoint);
			}
			cp19.LinkMap[9].Right = 0;
			cp19.LinkMap[19].Right = 50;
			cp19.LinkMap[29].Right = 51;
			cp19.LinkMap[39].Right = 52;
			cp19.LinkMap[49].Right = 53;
			cp19.LinkMap[0].Left = 9;
			cp19.LinkMap[10].Left = 54;
			cp19.LinkMap[20].Left = 55;
			cp19.LinkMap[30].Left = 56;
			cp19.LinkMap[40].Left = 57;
			cp19.LinkMap.Add(300, new UILinkPoint(300, true, 309, 310, 49, -2));
			cp19.LinkMap.Add(309, new UILinkPoint(309, true, 310, 300, 302, 54));
			cp19.LinkMap.Add(310, new UILinkPoint(310, true, 300, 309, 301, 50));
			cp19.LinkMap.Add(301, new UILinkPoint(301, true, 300, 302, 53, 50));
			cp19.LinkMap.Add(302, new UILinkPoint(302, true, 301, 310, 57, 54));
			cp19.LinkMap.Add(311, new UILinkPoint(311, true, -3, -4, 40, -2));
			cp19.LinkMap[301].OnSpecialInteracts += value;
			cp19.LinkMap[302].OnSpecialInteracts += value;
			cp19.LinkMap[309].OnSpecialInteracts += value;
			cp19.LinkMap[310].OnSpecialInteracts += value;
			cp19.LinkMap[300].OnSpecialInteracts += value3;
			cp19.UpdateEvent += delegate()
			{
				bool inReforgeMenu = Main.InReforgeMenu;
				bool flag7 = Main.player[Main.myPlayer].chest != -1;
				bool flag8 = Main.npcShop != 0;
				TileEntity tileEntity = Main.LocalPlayer.tileEntityAnchor.GetTileEntity();
				bool flag9 = tileEntity is TEHatRack;
				bool flag10 = tileEntity is TEDisplayDoll;
				for (int num33 = 40; num33 <= 49; num33++)
				{
					if (inReforgeMenu)
					{
						cp19.LinkMap[num33].Down = ((num33 < 45) ? 303 : 304);
					}
					else if (flag7)
					{
						cp19.LinkMap[num33].Down = 400 + num33 - 40;
					}
					else if (flag8)
					{
						cp19.LinkMap[num33].Down = 2700 + num33 - 40;
					}
					else if (num33 == 40)
					{
						cp19.LinkMap[num33].Down = 311;
					}
					else
					{
						cp19.LinkMap[num33].Down = -2;
					}
				}
				if (flag10)
				{
					for (int num34 = 40; num34 <= 47; num34++)
					{
						cp19.LinkMap[num34].Down = 5100 + num34 - 40;
					}
				}
				if (flag9)
				{
					for (int num35 = 44; num35 <= 45; num35++)
					{
						cp19.LinkMap[num35].Down = 5000 + num35 - 44;
					}
				}
				if (flag7)
				{
					cp19.LinkMap[300].Up = 439;
					cp19.LinkMap[300].Right = -4;
					cp19.LinkMap[300].Left = 309;
					cp19.LinkMap[309].Up = 438;
					cp19.LinkMap[309].Right = 300;
					cp19.LinkMap[309].Left = 310;
					cp19.LinkMap[310].Up = 437;
					cp19.LinkMap[310].Right = 309;
					cp19.LinkMap[310].Left = -3;
				}
				else if (flag8)
				{
					cp19.LinkMap[300].Up = 2739;
					cp19.LinkMap[300].Right = -4;
					cp19.LinkMap[300].Left = 309;
					cp19.LinkMap[309].Up = 2738;
					cp19.LinkMap[309].Right = 300;
					cp19.LinkMap[309].Left = 310;
					cp19.LinkMap[310].Up = 2737;
					cp19.LinkMap[310].Right = 309;
					cp19.LinkMap[310].Left = -3;
				}
				else
				{
					cp19.LinkMap[49].Down = 300;
					cp19.LinkMap[48].Down = 309;
					cp19.LinkMap[47].Down = 310;
					cp19.LinkMap[300].Up = 49;
					cp19.LinkMap[300].Right = 301;
					cp19.LinkMap[300].Left = 309;
					cp19.LinkMap[309].Up = 48;
					cp19.LinkMap[309].Right = 300;
					cp19.LinkMap[309].Left = 310;
					cp19.LinkMap[310].Up = 47;
					cp19.LinkMap[310].Right = 309;
					cp19.LinkMap[310].Left = 302;
				}
				cp19.LinkMap[0].Left = 9;
				cp19.LinkMap[10].Left = 54;
				cp19.LinkMap[20].Left = 55;
				cp19.LinkMap[30].Left = 56;
				cp19.LinkMap[40].Left = 57;
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 0)
				{
					cp19.LinkMap[0].Left = 6000;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 2)
				{
					cp19.LinkMap[10].Left = 6002;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 4)
				{
					cp19.LinkMap[20].Left = 6004;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 6)
				{
					cp19.LinkMap[30].Left = 6006;
				}
				if (UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 8)
				{
					cp19.LinkMap[40].Left = 6008;
				}
				cp19.PageOnLeft = 9;
				if (Main.CreativeMenu.Enabled)
				{
					cp19.PageOnLeft = 1005;
				}
				if (Main.InReforgeMenu)
				{
					cp19.PageOnLeft = 5;
				}
			};
			cp19.IsValidEvent += (() => Main.playerInventory);
			cp19.PageOnLeft = 9;
			cp19.PageOnRight = 2;
			UILinkPointNavigator.RegisterPage(cp19, 0, true);
			UILinkPage cp18 = new UILinkPage();
			cp18.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value4 = delegate()
			{
				int currentPoint4 = UILinkPointNavigator.CurrentPoint;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].inventory, 1, currentPoint4);
			};
			for (int k = 50; k <= 53; k++)
			{
				UILinkPoint uILinkPoint2 = new UILinkPoint(k, true, -3, -4, k - 1, k + 1);
				uILinkPoint2.OnSpecialInteracts += value4;
				cp18.LinkMap.Add(k, uILinkPoint2);
			}
			cp18.LinkMap[50].Left = 19;
			cp18.LinkMap[51].Left = 29;
			cp18.LinkMap[52].Left = 39;
			cp18.LinkMap[53].Left = 49;
			cp18.LinkMap[50].Right = 54;
			cp18.LinkMap[51].Right = 55;
			cp18.LinkMap[52].Right = 56;
			cp18.LinkMap[53].Right = 57;
			cp18.LinkMap[50].Up = -1;
			cp18.LinkMap[53].Down = -2;
			cp18.UpdateEvent += delegate()
			{
				if (Main.player[Main.myPlayer].chest == -1 && Main.npcShop == 0)
				{
					cp18.LinkMap[50].Up = 301;
					cp18.LinkMap[53].Down = 301;
					return;
				}
				cp18.LinkMap[50].Up = 504;
				cp18.LinkMap[53].Down = 500;
			};
			cp18.IsValidEvent += (() => Main.playerInventory);
			cp18.PageOnLeft = 0;
			cp18.PageOnRight = 2;
			UILinkPointNavigator.RegisterPage(cp18, 1, true);
			UILinkPage cp17 = new UILinkPage();
			cp17.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value5 = delegate()
			{
				int currentPoint3 = UILinkPointNavigator.CurrentPoint;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].inventory, 2, currentPoint3);
			};
			for (int l = 54; l <= 57; l++)
			{
				UILinkPoint uILinkPoint3 = new UILinkPoint(l, true, -3, -4, l - 1, l + 1);
				uILinkPoint3.OnSpecialInteracts += value5;
				cp17.LinkMap.Add(l, uILinkPoint3);
			}
			cp17.LinkMap[54].Left = 50;
			cp17.LinkMap[55].Left = 51;
			cp17.LinkMap[56].Left = 52;
			cp17.LinkMap[57].Left = 53;
			cp17.LinkMap[54].Right = 10;
			cp17.LinkMap[55].Right = 20;
			cp17.LinkMap[56].Right = 30;
			cp17.LinkMap[57].Right = 40;
			cp17.LinkMap[54].Up = -1;
			cp17.LinkMap[57].Down = -2;
			cp17.UpdateEvent += delegate()
			{
				if (Main.player[Main.myPlayer].chest == -1 && Main.npcShop == 0)
				{
					cp17.LinkMap[54].Up = 302;
					cp17.LinkMap[57].Down = 302;
					return;
				}
				cp17.LinkMap[54].Up = 504;
				cp17.LinkMap[57].Down = 500;
			};
			cp17.PageOnLeft = 0;
			cp17.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp17, 2, true);
			UILinkPage cp16 = new UILinkPage();
			cp16.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value6 = delegate()
			{
				int num33 = UILinkPointNavigator.CurrentPoint - 100;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].armor, (num33 < 10) ? 8 : 9, num33);
			};
			Func<string> value7 = delegate()
			{
				int slot11 = UILinkPointNavigator.CurrentPoint - 120;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].dye, 12, slot11);
			};
			for (int m = 100; m <= 119; m++)
			{
				UILinkPoint uILinkPoint4 = new UILinkPoint(m, true, m + 10, m - 10, m - 1, m + 1);
				uILinkPoint4.OnSpecialInteracts += value6;
				int num2 = m - 100;
				if (num2 == 0)
				{
					uILinkPoint4.Up = 305;
				}
				if (num2 == 10)
				{
					uILinkPoint4.Up = 306;
				}
				if (num2 == 9 || num2 == 19)
				{
					uILinkPoint4.Down = -2;
				}
				if (num2 >= 10)
				{
					uILinkPoint4.Left = 120 + num2 % 10;
				}
				else if (num2 >= 3)
				{
					uILinkPoint4.Right = -4;
				}
				else
				{
					uILinkPoint4.Right = 312 + num2;
				}
				cp16.LinkMap.Add(m, uILinkPoint4);
			}
			for (int n = 120; n <= 129; n++)
			{
				UILinkPoint uILinkPoint5 = new UILinkPoint(n, true, -3, -10 + n, n - 1, n + 1);
				uILinkPoint5.OnSpecialInteracts += value7;
				int num30 = n - 120;
				if (num30 == 0)
				{
					uILinkPoint5.Up = 307;
				}
				if (num30 == 9)
				{
					uILinkPoint5.Down = 308;
					uILinkPoint5.Left = 1557;
				}
				if (num30 == 8)
				{
					uILinkPoint5.Left = 1570;
				}
				cp16.LinkMap.Add(n, uILinkPoint5);
			}
			for (int num3 = 312; num3 <= 314; num3++)
			{
				int num4 = num3 - 312;
				UILinkPoint uILinkPoint6 = new UILinkPoint(num3, true, 100 + num4, -4, num3 - 1, num3 + 1);
				if (num4 == 0)
				{
					uILinkPoint6.Up = -1;
				}
				if (num4 == 2)
				{
					uILinkPoint6.Down = -2;
				}
				uILinkPoint6.OnSpecialInteracts += value;
				cp16.LinkMap.Add(num3, uILinkPoint6);
			}
			cp16.IsValidEvent += (() => Main.playerInventory && Main.EquipPage == 0);
			cp16.UpdateEvent += delegate()
			{
				int num33 = 107;
				int amountOfExtraAccessorySlotsToShow = Main.player[Main.myPlayer].GetAmountOfExtraAccessorySlotsToShow();
				for (int num34 = 0; num34 < amountOfExtraAccessorySlotsToShow; num34++)
				{
					cp16.LinkMap[num33 + num34].Down = num33 + num34 + 1;
					cp16.LinkMap[num33 - 100 + 120 + num34].Down = num33 - 100 + 120 + num34 + 1;
					cp16.LinkMap[num33 + 10 + num34].Down = num33 + 10 + num34 + 1;
				}
				cp16.LinkMap[num33 + amountOfExtraAccessorySlotsToShow].Down = 308;
				cp16.LinkMap[num33 + 10 + amountOfExtraAccessorySlotsToShow].Down = 308;
				cp16.LinkMap[num33 - 100 + 120 + amountOfExtraAccessorySlotsToShow].Down = 308;
				bool shouldPVPDraw = Main.ShouldPVPDraw;
				for (int num35 = 120; num35 <= 129; num35++)
				{
					UILinkPoint uILinkPoint20 = cp16.LinkMap[num35];
					int num36 = num35 - 120;
					uILinkPoint20.Left = -3;
					if (num36 == 0)
					{
						uILinkPoint20.Left = (shouldPVPDraw ? 1550 : -3);
					}
					if (num36 == 1)
					{
						uILinkPoint20.Left = (shouldPVPDraw ? 1552 : -3);
					}
					if (num36 == 2)
					{
						uILinkPoint20.Left = (shouldPVPDraw ? 1556 : -3);
					}
					if (num36 == 3)
					{
						uILinkPoint20.Left = ((UILinkPointNavigator.Shortcuts.INFOACCCOUNT >= 1) ? 1558 : -3);
					}
					if (num36 == 4)
					{
						uILinkPoint20.Left = ((UILinkPointNavigator.Shortcuts.INFOACCCOUNT >= 5) ? 1562 : -3);
					}
					if (num36 == 5)
					{
						uILinkPoint20.Left = ((UILinkPointNavigator.Shortcuts.INFOACCCOUNT >= 9) ? 1566 : -3);
					}
				}
				cp16.LinkMap[num33 - 100 + 120 + amountOfExtraAccessorySlotsToShow].Left = 1557;
				cp16.LinkMap[num33 - 100 + 120 + amountOfExtraAccessorySlotsToShow - 1].Left = 1570;
			};
			cp16.PageOnLeft = 8;
			cp16.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp16, 3, true);
			UILinkPage uILinkPage2 = new UILinkPage();
			uILinkPage2.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value8 = delegate()
			{
				int slot10 = UILinkPointNavigator.CurrentPoint - 400;
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
				return ItemSlot.GetGamepadInstructions(item, context, slot10);
			};
			for (int num5 = 400; num5 <= 439; num5++)
			{
				UILinkPoint uILinkPoint7 = new UILinkPoint(num5, true, num5 - 1, num5 + 1, num5 - 10, num5 + 10);
				uILinkPoint7.OnSpecialInteracts += value8;
				int num6 = num5 - 400;
				if (num6 < 10)
				{
					uILinkPoint7.Up = 40 + num6;
				}
				if (num6 >= 30)
				{
					uILinkPoint7.Down = -2;
				}
				if (num6 % 10 == 9)
				{
					uILinkPoint7.Right = -4;
				}
				if (num6 % 10 == 0)
				{
					uILinkPoint7.Left = -3;
				}
				uILinkPage2.LinkMap.Add(num5, uILinkPoint7);
			}
			uILinkPage2.LinkMap.Add(500, new UILinkPoint(500, true, 409, -4, 53, 501));
			uILinkPage2.LinkMap.Add(501, new UILinkPoint(501, true, 419, -4, 500, 502));
			uILinkPage2.LinkMap.Add(502, new UILinkPoint(502, true, 429, -4, 501, 503));
			uILinkPage2.LinkMap.Add(503, new UILinkPoint(503, true, 439, -4, 502, 505));
			uILinkPage2.LinkMap.Add(505, new UILinkPoint(505, true, 439, -4, 503, 504));
			uILinkPage2.LinkMap.Add(504, new UILinkPoint(504, true, 439, -4, 505, 50));
			uILinkPage2.LinkMap.Add(506, new UILinkPoint(506, true, 439, -4, 505, 50));
			uILinkPage2.LinkMap[500].OnSpecialInteracts += value;
			uILinkPage2.LinkMap[501].OnSpecialInteracts += value;
			uILinkPage2.LinkMap[502].OnSpecialInteracts += value;
			uILinkPage2.LinkMap[503].OnSpecialInteracts += value;
			uILinkPage2.LinkMap[504].OnSpecialInteracts += value;
			uILinkPage2.LinkMap[505].OnSpecialInteracts += value;
			uILinkPage2.LinkMap[506].OnSpecialInteracts += value;
			uILinkPage2.LinkMap[409].Right = 500;
			uILinkPage2.LinkMap[419].Right = 501;
			uILinkPage2.LinkMap[429].Right = 502;
			uILinkPage2.LinkMap[439].Right = 503;
			uILinkPage2.LinkMap[439].Down = 300;
			uILinkPage2.LinkMap[438].Down = 309;
			uILinkPage2.LinkMap[437].Down = 310;
			uILinkPage2.PageOnLeft = 0;
			uILinkPage2.PageOnRight = 0;
			uILinkPage2.DefaultPoint = 400;
			UILinkPointNavigator.RegisterPage(uILinkPage2, 4, false);
			uILinkPage2.IsValidEvent += (() => Main.playerInventory && Main.player[Main.myPlayer].chest != -1);
			UILinkPage uILinkPage3 = new UILinkPage();
			uILinkPage3.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value9 = delegate()
			{
				int slot9 = UILinkPointNavigator.CurrentPoint - 5100;
				TEDisplayDoll tEDisplayDoll = Main.LocalPlayer.tileEntityAnchor.GetTileEntity() as TEDisplayDoll;
				if (tEDisplayDoll != null)
				{
					return tEDisplayDoll.GetItemGamepadInstructions(slot9);
				}
				return "";
			};
			for (int num7 = 5100; num7 <= 5115; num7++)
			{
				UILinkPoint uILinkPoint8 = new UILinkPoint(num7, true, num7 - 1, num7 + 1, num7 - 8, num7 + 8);
				uILinkPoint8.OnSpecialInteracts += value9;
				int num8 = num7 - 5100;
				if (num8 < 8)
				{
					uILinkPoint8.Up = 40 + num8;
				}
				if (num8 >= 8)
				{
					uILinkPoint8.Down = -2;
				}
				if (num8 % 8 == 7)
				{
					uILinkPoint8.Right = -4;
				}
				if (num8 % 8 == 0)
				{
					uILinkPoint8.Left = -3;
				}
				uILinkPage3.LinkMap.Add(num7, uILinkPoint8);
			}
			uILinkPage3.PageOnLeft = 0;
			uILinkPage3.PageOnRight = 0;
			uILinkPage3.DefaultPoint = 5100;
			UILinkPointNavigator.RegisterPage(uILinkPage3, 20, false);
			uILinkPage3.IsValidEvent += (() => Main.playerInventory && Main.LocalPlayer.tileEntityAnchor.GetTileEntity() is TEDisplayDoll);
			UILinkPage uILinkPage4 = new UILinkPage();
			uILinkPage4.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value10 = delegate()
			{
				int slot8 = UILinkPointNavigator.CurrentPoint - 5000;
				TEHatRack tEHatRack = Main.LocalPlayer.tileEntityAnchor.GetTileEntity() as TEHatRack;
				if (tEHatRack != null)
				{
					return tEHatRack.GetItemGamepadInstructions(slot8);
				}
				return "";
			};
			for (int num9 = 5000; num9 <= 5003; num9++)
			{
				UILinkPoint uILinkPoint9 = new UILinkPoint(num9, true, num9 - 1, num9 + 1, num9 - 2, num9 + 2);
				uILinkPoint9.OnSpecialInteracts += value10;
				int num10 = num9 - 5000;
				if (num10 < 2)
				{
					uILinkPoint9.Up = 44 + num10;
				}
				if (num10 >= 2)
				{
					uILinkPoint9.Down = -2;
				}
				if (num10 % 2 == 1)
				{
					uILinkPoint9.Right = -4;
				}
				if (num10 % 2 == 0)
				{
					uILinkPoint9.Left = -3;
				}
				uILinkPage4.LinkMap.Add(num9, uILinkPoint9);
			}
			uILinkPage4.PageOnLeft = 0;
			uILinkPage4.PageOnRight = 0;
			uILinkPage4.DefaultPoint = 5000;
			UILinkPointNavigator.RegisterPage(uILinkPage4, 21, false);
			uILinkPage4.IsValidEvent += (() => Main.playerInventory && Main.LocalPlayer.tileEntityAnchor.GetTileEntity() is TEHatRack);
			UILinkPage uILinkPage5 = new UILinkPage();
			uILinkPage5.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
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
				int slot7 = UILinkPointNavigator.CurrentPoint - 2700;
				return ItemSlot.GetGamepadInstructions(Main.instance.shop[Main.npcShop].item, 15, slot7);
			};
			for (int num11 = 2700; num11 <= 2739; num11++)
			{
				UILinkPoint uILinkPoint10 = new UILinkPoint(num11, true, num11 - 1, num11 + 1, num11 - 10, num11 + 10);
				uILinkPoint10.OnSpecialInteracts += value11;
				int num12 = num11 - 2700;
				if (num12 < 10)
				{
					uILinkPoint10.Up = 40 + num12;
				}
				if (num12 >= 30)
				{
					uILinkPoint10.Down = -2;
				}
				if (num12 % 10 == 9)
				{
					uILinkPoint10.Right = -4;
				}
				if (num12 % 10 == 0)
				{
					uILinkPoint10.Left = -3;
				}
				uILinkPage5.LinkMap.Add(num11, uILinkPoint10);
			}
			uILinkPage5.LinkMap[2739].Down = 300;
			uILinkPage5.LinkMap[2738].Down = 309;
			uILinkPage5.LinkMap[2737].Down = 310;
			uILinkPage5.PageOnLeft = 0;
			uILinkPage5.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(uILinkPage5, 13, true);
			uILinkPage5.IsValidEvent += (() => Main.playerInventory && Main.npcShop != 0);
			UILinkPage cp15 = new UILinkPage();
			cp15.LinkMap.Add(303, new UILinkPoint(303, true, 304, 304, 40, -2));
			cp15.LinkMap.Add(304, new UILinkPoint(304, true, 303, 303, 40, -2));
			cp15.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value12 = () => ItemSlot.GetGamepadInstructions(ref Main.reforgeItem, 5);
			cp15.LinkMap[303].OnSpecialInteracts += value12;
			cp15.LinkMap[304].OnSpecialInteracts += (() => Lang.misc[53].Value);
			cp15.UpdateEvent += delegate()
			{
				if (Main.reforgeItem.type > 0)
				{
					cp15.LinkMap[303].Left = (cp15.LinkMap[303].Right = 304);
					return;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp15.CurrentPoint == 304)
				{
					UILinkPointNavigator.ChangePoint(303);
				}
				cp15.LinkMap[303].Left = -3;
				cp15.LinkMap[303].Right = -4;
			};
			cp15.IsValidEvent += (() => Main.playerInventory && Main.InReforgeMenu);
			cp15.PageOnLeft = 0;
			cp15.PageOnRight = 0;
			cp15.EnterEvent += delegate()
			{
				PlayerInput.LockGamepadButtons("MouseLeft");
			};
			UILinkPointNavigator.RegisterPage(cp15, 5, true);
			UILinkPage cp14 = new UILinkPage();
			cp14.OnSpecialInteracts += delegate()
			{
				bool flag5 = UILinkPointNavigator.CurrentPoint == 600;
				bool flag6 = !flag5 && WorldGen.IsNPCEvictable(UILinkPointNavigator.Shortcuts.NPCS_LastHovered);
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Grapple)
				{
					Point point = Main.player[Main.myPlayer].Center.ToTileCoordinates();
					if (flag5)
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
				if (flag6 && UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.MouseRight)
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
					flag6 ? PlayerInput.BuildCommand("Evict", false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
					}) : ""
				});
			};
			for (int num13 = 600; num13 <= 698; num13++)
			{
				UILinkPoint value13 = new UILinkPoint(num13, true, num13 + 10, num13 - 10, num13 - 1, num13 + 1);
				cp14.LinkMap.Add(num13, value13);
			}
			cp14.UpdateEvent += delegate()
			{
				int num33 = UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn;
				if (num33 == 0)
				{
					num33 = 100;
				}
				for (int num34 = 0; num34 < 98; num34++)
				{
					cp14.LinkMap[600 + num34].Up = ((num34 % num33 == 0) ? -1 : (600 + num34 - 1));
					if (cp14.LinkMap[600 + num34].Up == -1)
					{
						if (num34 >= num33 * 2)
						{
							cp14.LinkMap[600 + num34].Up = 307;
						}
						else if (num34 >= num33)
						{
							cp14.LinkMap[600 + num34].Up = 306;
						}
						else
						{
							cp14.LinkMap[600 + num34].Up = 305;
						}
					}
					cp14.LinkMap[600 + num34].Down = (((num34 + 1) % num33 == 0 || num34 == UILinkPointNavigator.Shortcuts.NPCS_IconsTotal - 1) ? 308 : (600 + num34 + 1));
					cp14.LinkMap[600 + num34].Left = ((num34 < UILinkPointNavigator.Shortcuts.NPCS_IconsTotal - num33) ? (600 + num34 + num33) : -3);
					cp14.LinkMap[600 + num34].Right = ((num34 < num33) ? -4 : (600 + num34 - num33));
				}
			};
			cp14.IsValidEvent += (() => Main.playerInventory && Main.EquipPage == 1);
			cp14.PageOnLeft = 8;
			cp14.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp14, 6, true);
			UILinkPage cp13 = new UILinkPage();
			cp13.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			Func<string> value14 = delegate()
			{
				int slot6 = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 20, slot6);
			};
			Func<string> value15 = delegate()
			{
				int slot5 = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 19, slot5);
			};
			Func<string> value16 = delegate()
			{
				int slot4 = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 18, slot4);
			};
			Func<string> value17 = delegate()
			{
				int slot3 = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 17, slot3);
			};
			Func<string> value18 = delegate()
			{
				int slot2 = UILinkPointNavigator.CurrentPoint - 180;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscEquips, 16, slot2);
			};
			Func<string> value19 = delegate()
			{
				int slot = UILinkPointNavigator.CurrentPoint - 185;
				return ItemSlot.GetGamepadInstructions(Main.player[Main.myPlayer].miscDyes, 33, slot);
			};
			for (int num14 = 180; num14 <= 184; num14++)
			{
				UILinkPoint uILinkPoint11 = new UILinkPoint(num14, true, 185 + num14 - 180, -4, num14 - 1, num14 + 1);
				int num31 = num14 - 180;
				if (num31 == 0)
				{
					uILinkPoint11.Up = 305;
				}
				if (num31 == 4)
				{
					uILinkPoint11.Down = 308;
				}
				cp13.LinkMap.Add(num14, uILinkPoint11);
				switch (num14)
				{
				case 180:
					uILinkPoint11.OnSpecialInteracts += value15;
					break;
				case 181:
					uILinkPoint11.OnSpecialInteracts += value14;
					break;
				case 182:
					uILinkPoint11.OnSpecialInteracts += value16;
					break;
				case 183:
					uILinkPoint11.OnSpecialInteracts += value17;
					break;
				case 184:
					uILinkPoint11.OnSpecialInteracts += value18;
					break;
				}
			}
			for (int num15 = 185; num15 <= 189; num15++)
			{
				UILinkPoint uILinkPoint12 = new UILinkPoint(num15, true, -3, -5 + num15, num15 - 1, num15 + 1);
				uILinkPoint12.OnSpecialInteracts += value19;
				int num32 = num15 - 185;
				if (num32 == 0)
				{
					uILinkPoint12.Up = 306;
				}
				if (num32 == 4)
				{
					uILinkPoint12.Down = 308;
				}
				cp13.LinkMap.Add(num15, uILinkPoint12);
			}
			cp13.UpdateEvent += delegate()
			{
				cp13.LinkMap[184].Down = ((UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0) ? 9000 : 308);
				cp13.LinkMap[189].Down = ((UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0) ? 9000 : 308);
			};
			cp13.IsValidEvent += (() => Main.playerInventory && Main.EquipPage == 2);
			cp13.PageOnLeft = 8;
			cp13.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp13, 7, true);
			UILinkPage cp12 = new UILinkPage();
			cp12.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			cp12.LinkMap.Add(305, new UILinkPoint(305, true, 306, -4, 308, -2));
			cp12.LinkMap.Add(306, new UILinkPoint(306, true, 307, 305, 308, -2));
			cp12.LinkMap.Add(307, new UILinkPoint(307, true, -3, 306, 308, -2));
			cp12.LinkMap.Add(308, new UILinkPoint(308, true, -3, -4, -1, 305));
			cp12.LinkMap[305].OnSpecialInteracts += value;
			cp12.LinkMap[306].OnSpecialInteracts += value;
			cp12.LinkMap[307].OnSpecialInteracts += value;
			cp12.LinkMap[308].OnSpecialInteracts += value;
			cp12.UpdateEvent += delegate()
			{
				switch (Main.EquipPage)
				{
				case 0:
					cp12.LinkMap[305].Down = 100;
					cp12.LinkMap[306].Down = 110;
					cp12.LinkMap[307].Down = 120;
					cp12.LinkMap[308].Up = 108 + Main.player[Main.myPlayer].GetAmountOfExtraAccessorySlotsToShow() - 1;
					break;
				case 1:
				{
					cp12.LinkMap[305].Down = 600;
					cp12.LinkMap[306].Down = ((UILinkPointNavigator.Shortcuts.NPCS_IconsTotal / UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn > 0) ? (600 + UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn) : -2);
					cp12.LinkMap[307].Down = ((UILinkPointNavigator.Shortcuts.NPCS_IconsTotal / UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn > 1) ? (600 + UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn * 2) : -2);
					int num33 = UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn;
					if (num33 == 0)
					{
						num33 = 100;
					}
					if (num33 == 100)
					{
						num33 = UILinkPointNavigator.Shortcuts.NPCS_IconsTotal;
					}
					cp12.LinkMap[308].Up = 600 + num33 - 1;
					break;
				}
				case 2:
					cp12.LinkMap[305].Down = 180;
					cp12.LinkMap[306].Down = 185;
					cp12.LinkMap[307].Down = -2;
					cp12.LinkMap[308].Up = ((UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0) ? 9000 : 184);
					break;
				}
				cp12.PageOnRight = UILinksInitializer.GetCornerWrapPageIdFromRightToLeft();
			};
			cp12.IsValidEvent += (() => Main.playerInventory);
			cp12.PageOnLeft = 0;
			cp12.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(cp12, 8, true);
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
				if (Main.mouseItem.type >= 1)
				{
					return ItemSlot.GetGamepadInstructions(ref Main.mouseItem, 22);
				}
				return "";
			};
			for (int num16 = 1500; num16 < 1550; num16++)
			{
				UILinkPoint uILinkPoint13 = new UILinkPoint(num16, true, num16, num16, -1, -2);
				if (num16 != 1500)
				{
					uILinkPoint13.OnSpecialInteracts += HandleItem2;
				}
				cp11.LinkMap.Add(num16, uILinkPoint13);
			}
			cp11.LinkMap[1500].OnSpecialInteracts += value20;
			cp11.UpdateEvent += delegate()
			{
				int num33 = UILinkPointNavigator.Shortcuts.CRAFT_CurrentIngredientsCount;
				int num34 = num33;
				if (Main.numAvailableRecipes > 0)
				{
					num34 += 2;
				}
				if (num33 < num34)
				{
					num33 = num34;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp11.CurrentPoint > 1500 + num33)
				{
					UILinkPointNavigator.ChangePoint(1500);
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp11.CurrentPoint == 1500 && !Main.InGuideCraftMenu)
				{
					UILinkPointNavigator.ChangePoint(1501);
				}
				for (int num35 = 1; num35 < num33; num35++)
				{
					cp11.LinkMap[1500 + num35].Left = 1500 + num35 - 1;
					cp11.LinkMap[1500 + num35].Right = ((num35 == num33 - 2) ? -4 : (1500 + num35 + 1));
				}
				cp11.LinkMap[1501].Left = -3;
				if (num33 > 0)
				{
					cp11.LinkMap[1500 + num33 - 1].Right = -4;
				}
				cp11.LinkMap[1500].Down = ((num33 >= 2) ? 1502 : -2);
				cp11.LinkMap[1500].Left = ((num33 >= 1) ? 1501 : -3);
				cp11.LinkMap[1502].Up = (Main.InGuideCraftMenu ? 1500 : -1);
			};
			cp11.LinkMap[1501].OnSpecialInteracts += delegate()
			{
				if (Main.InGuideCraftMenu)
				{
					return "";
				}
				string text2 = "";
				Player player2 = Main.player[Main.myPlayer];
				bool flag3 = false;
				Item createItem = Main.recipe[Main.availableRecipe[Main.focusRecipe]].createItem;
				if (Main.mouseItem.type == 0 && createItem.maxStack > 1 && player2.ItemSpace(createItem).CanTakeItemToPersonalInventory && !player2.HasLockedInventory())
				{
					flag3 = true;
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
							Main.mouseItem = player2.GetItem(player2.whoAmI, Main.mouseItem, new GetItemSettings(false, true, false, null));
						}
					}
				}
				else if (Main.mouseItem.type > 0 && Main.mouseItem.maxStack == 1 && ItemSlot.Equippable(ref Main.mouseItem, 0))
				{
					text2 += PlayerInput.BuildCommand(Lang.misc[67].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
					});
					if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Grapple)
					{
						ItemSlot.SwapEquip(ref Main.mouseItem, 0);
						if (Main.player[Main.myPlayer].ItemSpace(Main.mouseItem).CanTakeItemToPersonalInventory)
						{
							Main.mouseItem = player2.GetItem(player2.whoAmI, Main.mouseItem, GetItemSettings.InventoryUIToInventorySettings);
						}
						PlayerInput.LockGamepadButtons("Grapple");
						PlayerInput.SettingsForUI.TryRevertingToMouseMode();
					}
				}
				bool flag4 = Main.mouseItem.stack <= 0;
				if (flag4 || (Main.mouseItem.type == createItem.type && Main.mouseItem.stack < Main.mouseItem.maxStack && ItemLoader.CanStack(Main.mouseItem, createItem)))
				{
					text2 = ((!flag4) ? (text2 + PlayerInput.BuildCommand(Lang.misc[72].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
					})) : (text2 + PlayerInput.BuildCommand(Lang.misc[72].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"],
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
					})));
				}
				if (!flag4 && Main.mouseItem.type == createItem.type && Main.mouseItem.stack < Main.mouseItem.maxStack && ItemLoader.CanStack(Main.mouseItem, createItem))
				{
					text2 += PlayerInput.BuildCommand(Lang.misc[93].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseRight"]
					});
				}
				if (flag3)
				{
					text2 += PlayerInput.BuildCommand(Lang.misc[71].Value, false, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
					});
				}
				return text2 + HandleItem2();
			};
			cp11.ReachEndEvent += delegate(int current, int next)
			{
				if (current != 1500)
				{
					if (current == 1501)
					{
						if (next != -2)
						{
							if (next == -1 && Main.focusRecipe > 0)
							{
								Main.focusRecipe--;
								return;
							}
						}
						else if (Main.focusRecipe < Main.numAvailableRecipes - 1)
						{
							Main.focusRecipe++;
							return;
						}
					}
					else if (next != -2)
					{
						if (next == -1 && Main.focusRecipe > 0)
						{
							UILinkPointNavigator.ChangePoint(1501);
							Main.focusRecipe--;
							return;
						}
					}
					else if (Main.focusRecipe < Main.numAvailableRecipes - 1)
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
			UILinkPage cp10 = new UILinkPage();
			cp10.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			for (int num17 = 700; num17 < 1500; num17++)
			{
				UILinkPoint uILinkPoint14 = new UILinkPoint(num17, true, num17, num17, num17, num17);
				int IHateLambda = num17;
				uILinkPoint14.OnSpecialInteracts += delegate()
				{
					string text = "";
					bool flag2 = false;
					Player player = Main.player[Main.myPlayer];
					if (IHateLambda + Main.recStart < Main.numAvailableRecipes)
					{
						int num33 = Main.recStart + IHateLambda - 700;
						if (Main.mouseItem.type == 0 && Main.recipe[Main.availableRecipe[num33]].createItem.maxStack > 1 && player.ItemSpace(Main.recipe[Main.availableRecipe[num33]].createItem).CanTakeItemToPersonalInventory && !player.HasLockedInventory())
						{
							flag2 = true;
							if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Grapple)
							{
								UILinksInitializer.SomeVarsForUILinkers.SequencedCraftingCurrent = Main.recipe[Main.availableRecipe[num33]];
							}
							if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.Current.Grapple && Main.stackSplit <= 1)
							{
								ItemSlot.RefreshStackSplitCooldown();
								if (UILinksInitializer.SomeVarsForUILinkers.SequencedCraftingCurrent == Main.recipe[Main.availableRecipe[num33]])
								{
									Main.CraftItem(Main.recipe[Main.availableRecipe[num33]]);
									Main.mouseItem = player.GetItem(player.whoAmI, Main.mouseItem, GetItemSettings.InventoryUIToInventorySettings);
								}
							}
						}
					}
					text += PlayerInput.BuildCommand(Lang.misc[73].Value, !flag2, new List<string>[]
					{
						PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
					});
					if (flag2)
					{
						text += PlayerInput.BuildCommand(Lang.misc[71].Value, true, new List<string>[]
						{
							PlayerInput.ProfileGamepadUI.KeyStatus["Grapple"]
						});
					}
					return text;
				};
				cp10.LinkMap.Add(num17, uILinkPoint14);
			}
			cp10.UpdateEvent += delegate()
			{
				int num33 = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerRow;
				int cRAFT_IconsPerColumn = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerColumn;
				if (num33 == 0)
				{
					num33 = 100;
				}
				int num34 = num33 * cRAFT_IconsPerColumn;
				if (num34 > 800)
				{
					num34 = 800;
				}
				if (num34 > Main.numAvailableRecipes)
				{
					num34 = Main.numAvailableRecipes;
				}
				for (int num35 = 0; num35 < num34; num35++)
				{
					cp10.LinkMap[700 + num35].Left = ((num35 % num33 == 0) ? -3 : (700 + num35 - 1));
					cp10.LinkMap[700 + num35].Right = (((num35 + 1) % num33 == 0 || num35 == Main.numAvailableRecipes - 1) ? -4 : (700 + num35 + 1));
					cp10.LinkMap[700 + num35].Down = ((num35 < num34 - num33) ? (700 + num35 + num33) : -2);
					cp10.LinkMap[700 + num35].Up = ((num35 < num33) ? -1 : (700 + num35 - num33));
				}
				cp10.PageOnLeft = UILinksInitializer.GetCornerWrapPageIdFromLeftToRight();
			};
			cp10.ReachEndEvent += delegate(int current, int next)
			{
				int cRAFT_IconsPerRow = UILinkPointNavigator.Shortcuts.CRAFT_IconsPerRow;
				if (next != -2)
				{
					if (next == -1)
					{
						Main.recStart -= cRAFT_IconsPerRow;
						if (Main.recStart < 0)
						{
							Main.recStart = 0;
							return;
						}
					}
				}
				else
				{
					Main.recStart += cRAFT_IconsPerRow;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
					if (Main.recStart > Main.numAvailableRecipes - cRAFT_IconsPerRow)
					{
						Main.recStart = Main.numAvailableRecipes - cRAFT_IconsPerRow;
					}
				}
			};
			cp10.EnterEvent += delegate()
			{
				Main.recBigList = true;
			};
			cp10.LeaveEvent += delegate()
			{
				Main.recBigList = false;
			};
			cp10.CanEnterEvent += (() => Main.playerInventory && Main.numAvailableRecipes > 0);
			cp10.IsValidEvent += (() => Main.playerInventory && Main.recBigList && Main.numAvailableRecipes > 0);
			cp10.PageOnLeft = 0;
			cp10.PageOnRight = 9;
			UILinkPointNavigator.RegisterPage(cp10, 10, true);
			UILinkPage cp9 = new UILinkPage();
			cp9.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			for (int num18 = 2605; num18 < 2620; num18++)
			{
				UILinkPoint uILinkPoint15 = new UILinkPoint(num18, true, num18, num18, num18, num18);
				uILinkPoint15.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[73].Value, true, new List<string>[]
				{
					PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
				}));
				cp9.LinkMap.Add(num18, uILinkPoint15);
			}
			cp9.UpdateEvent += delegate()
			{
				int num33 = 5;
				int num34 = 3;
				int num35 = num33 * num34;
				int count = Main.Hairstyles.AvailableHairstyles.Count;
				for (int num36 = 0; num36 < num35; num36++)
				{
					cp9.LinkMap[2605 + num36].Left = ((num36 % num33 == 0) ? -3 : (2605 + num36 - 1));
					cp9.LinkMap[2605 + num36].Right = (((num36 + 1) % num33 == 0 || num36 == count - 1) ? -4 : (2605 + num36 + 1));
					cp9.LinkMap[2605 + num36].Down = ((num36 < num35 - num33) ? (2605 + num36 + num33) : -2);
					cp9.LinkMap[2605 + num36].Up = ((num36 < num33) ? -1 : (2605 + num36 - num33));
				}
			};
			cp9.ReachEndEvent += delegate(int current, int next)
			{
				int num33 = 5;
				if (next != -2)
				{
					if (next == -1)
					{
						Main.hairStart -= num33;
						SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
						return;
					}
				}
				else
				{
					Main.hairStart += num33;
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
			};
			cp9.CanEnterEvent += (() => Main.hairWindow);
			cp9.IsValidEvent += (() => Main.hairWindow);
			cp9.PageOnLeft = 12;
			cp9.PageOnRight = 12;
			UILinkPointNavigator.RegisterPage(cp9, 11, true);
			UILinkPage uilinkPage = new UILinkPage();
			uilinkPage.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			uilinkPage.LinkMap.Add(2600, new UILinkPoint(2600, true, -3, -4, -1, 2601));
			uilinkPage.LinkMap.Add(2601, new UILinkPoint(2601, true, -3, -4, 2600, 2602));
			uilinkPage.LinkMap.Add(2602, new UILinkPoint(2602, true, -3, -4, 2601, 2603));
			uilinkPage.LinkMap.Add(2603, new UILinkPoint(2603, true, -3, 2604, 2602, -2));
			uilinkPage.LinkMap.Add(2604, new UILinkPoint(2604, true, 2603, -4, 2602, -2));
			uilinkPage.UpdateEvent += delegate()
			{
				Vector3 vector = Main.rgbToHsl(Main.selColor);
				float interfaceDeadzoneX2 = PlayerInput.CurrentProfile.InterfaceDeadzoneX;
				float x2 = PlayerInput.GamepadThumbstickLeft.X;
				x2 = ((x2 >= 0f - interfaceDeadzoneX2 && x2 <= interfaceDeadzoneX2) ? 0f : (MathHelper.Lerp(0f, 0.008333334f, (Math.Abs(x2) - interfaceDeadzoneX2) / (1f - interfaceDeadzoneX2)) * (float)Math.Sign(x2)));
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				if (currentPoint == 2600)
				{
					Main.hBar = MathHelper.Clamp(Main.hBar + x2, 0f, 1f);
				}
				if (currentPoint == 2601)
				{
					Main.sBar = MathHelper.Clamp(Main.sBar + x2, 0f, 1f);
				}
				if (currentPoint == 2602)
				{
					Main.lBar = MathHelper.Clamp(Main.lBar + x2, 0.15f, 1f);
				}
				Vector3.Clamp(vector, Vector3.Zero, Vector3.One);
				if (x2 != 0f)
				{
					if (Main.hairWindow)
					{
						Main.player[Main.myPlayer].hairColor = (Main.selColor = Main.hslToRgb(Main.hBar, Main.sBar, Main.lBar, byte.MaxValue));
					}
					SoundEngine.PlaySound(12, -1, -1, 1, 1f, 0f);
				}
			};
			uilinkPage.CanEnterEvent += (() => Main.hairWindow);
			uilinkPage.IsValidEvent += (() => Main.hairWindow);
			uilinkPage.PageOnLeft = 11;
			uilinkPage.PageOnRight = 11;
			UILinkPointNavigator.RegisterPage(uilinkPage, 12, true);
			UILinkPage cp8 = new UILinkPage();
			for (int num19 = 0; num19 < 30; num19++)
			{
				cp8.LinkMap.Add(2900 + num19, new UILinkPoint(2900 + num19, true, -3, -4, -1, -2));
				cp8.LinkMap[2900 + num19].OnSpecialInteracts += value;
			}
			cp8.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			cp8.TravelEvent += delegate()
			{
				if (UILinkPointNavigator.CurrentPage == cp8.ID)
				{
					int num33 = cp8.CurrentPoint - 2900;
					if (num33 < 5)
					{
						IngameOptions.category = num33;
					}
				}
			};
			cp8.UpdateEvent += delegate()
			{
				int num33 = UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_LEFT;
				if (num33 == 0)
				{
					num33 = 5;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp8.CurrentPoint < 2930 && cp8.CurrentPoint > 2900 + num33 - 1)
				{
					UILinkPointNavigator.ChangePoint(2900);
				}
				for (int num34 = 2900; num34 < 2900 + num33; num34++)
				{
					cp8.LinkMap[num34].Up = num34 - 1;
					cp8.LinkMap[num34].Down = num34 + 1;
				}
				cp8.LinkMap[2900].Up = 2900 + num33 - 1;
				cp8.LinkMap[2900 + num33 - 1].Down = 2900;
				int num35 = cp8.CurrentPoint - 2900;
				if (num35 < 4 && UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.MouseLeft)
				{
					IngameOptions.category = num35;
					UILinkPointNavigator.ChangePage(1002);
				}
				int num36 = (SocialAPI.Network != null && SocialAPI.Network.CanInvite()) ? 1 : 0;
				if (num35 == 4 + num36 && UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.MouseLeft)
				{
					UILinkPointNavigator.ChangePage(1004);
				}
			};
			cp8.EnterEvent += delegate()
			{
				cp8.CurrentPoint = 2900 + IngameOptions.category;
			};
			cp8.PageOnLeft = (cp8.PageOnRight = 1002);
			cp8.IsValidEvent += (() => Main.ingameOptionsWindow && !Main.InGameUI.IsVisible);
			cp8.CanEnterEvent += (() => Main.ingameOptionsWindow && !Main.InGameUI.IsVisible);
			UILinkPointNavigator.RegisterPage(cp8, 1001, true);
			UILinkPage cp7 = new UILinkPage();
			for (int num20 = 0; num20 < 30; num20++)
			{
				cp7.LinkMap.Add(2930 + num20, new UILinkPoint(2930 + num20, true, -3, -4, -1, -2));
				cp7.LinkMap[2930 + num20].OnSpecialInteracts += value;
			}
			cp7.EnterEvent += delegate()
			{
				Main.mouseLeftRelease = false;
			};
			cp7.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			cp7.UpdateEvent += delegate()
			{
				int num33 = UILinkPointNavigator.Shortcuts.INGAMEOPTIONS_BUTTONS_RIGHT;
				if (num33 == 0)
				{
					num33 = 5;
				}
				if (UILinkPointNavigator.OverridePoint == -1 && cp7.CurrentPoint >= 2930 && cp7.CurrentPoint > 2930 + num33 - 1)
				{
					UILinkPointNavigator.ChangePoint(2930);
				}
				for (int num34 = 2930; num34 < 2930 + num33; num34++)
				{
					cp7.LinkMap[num34].Up = num34 - 1;
					cp7.LinkMap[num34].Down = num34 + 1;
				}
				cp7.LinkMap[2930].Up = -1;
				cp7.LinkMap[2930 + num33 - 1].Down = -2;
				UILinksInitializer.HandleOptionsSpecials();
			};
			cp7.PageOnLeft = (cp7.PageOnRight = 1001);
			cp7.IsValidEvent += (() => Main.ingameOptionsWindow);
			cp7.CanEnterEvent += (() => Main.ingameOptionsWindow);
			UILinkPointNavigator.RegisterPage(cp7, 1002, true);
			UILinkPage cp6 = new UILinkPage();
			cp6.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			for (int num21 = 1550; num21 < 1558; num21++)
			{
				UILinkPoint uILinkPoint16 = new UILinkPoint(num21, true, -3, -4, -1, -2);
				switch (num21 - 1550)
				{
				case 1:
				case 3:
				case 5:
					uILinkPoint16.Up = uILinkPoint16.ID - 2;
					uILinkPoint16.Down = uILinkPoint16.ID + 2;
					uILinkPoint16.Right = uILinkPoint16.ID + 1;
					break;
				case 2:
				case 4:
				case 6:
					uILinkPoint16.Up = uILinkPoint16.ID - 2;
					uILinkPoint16.Down = uILinkPoint16.ID + 2;
					uILinkPoint16.Left = uILinkPoint16.ID - 1;
					break;
				}
				cp6.LinkMap.Add(num21, uILinkPoint16);
			}
			cp6.LinkMap[1550].Down = 1551;
			cp6.LinkMap[1550].Right = 120;
			cp6.LinkMap[1550].Up = 307;
			cp6.LinkMap[1551].Up = 1550;
			cp6.LinkMap[1552].Up = 1550;
			cp6.LinkMap[1552].Right = 121;
			cp6.LinkMap[1554].Right = 121;
			cp6.LinkMap[1555].Down = 1570;
			cp6.LinkMap[1556].Down = 1570;
			cp6.LinkMap[1556].Right = 122;
			cp6.LinkMap[1557].Up = 1570;
			cp6.LinkMap[1557].Down = 308;
			cp6.LinkMap[1557].Right = 127;
			cp6.LinkMap.Add(1570, new UILinkPoint(1570, true, -3, -4, -1, -2));
			cp6.LinkMap[1570].Up = 1555;
			cp6.LinkMap[1570].Down = 1557;
			cp6.LinkMap[1570].Right = 126;
			for (int num22 = 0; num22 < 7; num22++)
			{
				cp6.LinkMap[1550 + num22].OnSpecialInteracts += value;
			}
			cp6.UpdateEvent += delegate()
			{
				if (!Main.ShouldPVPDraw)
				{
					if (UILinkPointNavigator.OverridePoint == -1 && cp6.CurrentPoint != 1557 && cp6.CurrentPoint != 1570)
					{
						UILinkPointNavigator.ChangePoint(1557);
					}
					cp6.LinkMap[1570].Up = -1;
					cp6.LinkMap[1557].Down = 308;
					cp6.LinkMap[1557].Right = 127;
				}
				else
				{
					cp6.LinkMap[1570].Up = 1555;
					cp6.LinkMap[1557].Down = 308;
					cp6.LinkMap[1557].Right = 127;
				}
				int iNFOACCCOUNT2 = UILinkPointNavigator.Shortcuts.INFOACCCOUNT;
				if (iNFOACCCOUNT2 > 0)
				{
					cp6.LinkMap[1570].Up = 1558 + (iNFOACCCOUNT2 - 1) / 2 * 2;
				}
				if (Main.ShouldPVPDraw)
				{
					if (iNFOACCCOUNT2 >= 1)
					{
						cp6.LinkMap[1555].Down = 1558;
						cp6.LinkMap[1556].Down = 1558;
					}
					else
					{
						cp6.LinkMap[1555].Down = 1570;
						cp6.LinkMap[1556].Down = 1570;
					}
					if (iNFOACCCOUNT2 >= 2)
					{
						cp6.LinkMap[1556].Down = 1559;
						return;
					}
					cp6.LinkMap[1556].Down = 1570;
				}
			};
			cp6.IsValidEvent += (() => Main.playerInventory);
			cp6.PageOnLeft = 8;
			cp6.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp6, 16, true);
			UILinkPage cp5 = new UILinkPage();
			cp5.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			for (int num23 = 1558; num23 < 1570; num23++)
			{
				UILinkPoint uILinkPoint17 = new UILinkPoint(num23, true, -3, -4, -1, -2);
				uILinkPoint17.OnSpecialInteracts += value;
				switch (num23 - 1558)
				{
				case 1:
				case 3:
				case 5:
					uILinkPoint17.Up = uILinkPoint17.ID - 2;
					uILinkPoint17.Down = uILinkPoint17.ID + 2;
					uILinkPoint17.Right = uILinkPoint17.ID + 1;
					break;
				case 2:
				case 4:
				case 6:
					uILinkPoint17.Up = uILinkPoint17.ID - 2;
					uILinkPoint17.Down = uILinkPoint17.ID + 2;
					uILinkPoint17.Left = uILinkPoint17.ID - 1;
					break;
				}
				cp5.LinkMap.Add(num23, uILinkPoint17);
			}
			cp5.UpdateEvent += delegate()
			{
				int iNFOACCCOUNT = UILinkPointNavigator.Shortcuts.INFOACCCOUNT;
				if (UILinkPointNavigator.OverridePoint == -1 && cp5.CurrentPoint - 1558 >= iNFOACCCOUNT)
				{
					UILinkPointNavigator.ChangePoint(1558 + iNFOACCCOUNT - 1);
				}
				for (int num33 = 0; num33 < iNFOACCCOUNT; num33++)
				{
					bool flag = num33 % 2 == 0;
					int num34 = num33 + 1558;
					cp5.LinkMap[num34].Down = ((num33 < iNFOACCCOUNT - 2) ? (num34 + 2) : 1570);
					cp5.LinkMap[num34].Up = ((num33 > 1) ? (num34 - 2) : (Main.ShouldPVPDraw ? (flag ? 1555 : 1556) : -1));
					cp5.LinkMap[num34].Right = ((flag && num33 + 1 < iNFOACCCOUNT) ? (num34 + 1) : (123 + num33 / 4));
					cp5.LinkMap[num34].Left = (flag ? -3 : (num34 - 1));
				}
			};
			cp5.IsValidEvent += (() => Main.playerInventory && UILinkPointNavigator.Shortcuts.INFOACCCOUNT > 0);
			cp5.PageOnLeft = 8;
			cp5.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp5, 17, true);
			UILinkPage cp4 = new UILinkPage();
			cp4.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			for (int num24 = 6000; num24 < 6012; num24++)
			{
				UILinkPoint uILinkPoint18 = new UILinkPoint(num24, true, -3, -4, -1, -2);
				switch (num24)
				{
				case 6000:
					uILinkPoint18.Right = 0;
					break;
				case 6001:
				case 6002:
					uILinkPoint18.Right = 10;
					break;
				case 6003:
				case 6004:
					uILinkPoint18.Right = 20;
					break;
				case 6005:
				case 6006:
					uILinkPoint18.Right = 30;
					break;
				default:
					uILinkPoint18.Right = 40;
					break;
				}
				cp4.LinkMap.Add(num24, uILinkPoint18);
			}
			cp4.UpdateEvent += delegate()
			{
				int bUILDERACCCOUNT = UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT;
				if (UILinkPointNavigator.OverridePoint == -1 && cp4.CurrentPoint - 6000 >= bUILDERACCCOUNT)
				{
					UILinkPointNavigator.ChangePoint(6000 + bUILDERACCCOUNT - 1);
				}
				for (int num33 = 0; num33 < bUILDERACCCOUNT; num33++)
				{
					int num35 = num33 % 2;
					int num34 = num33 + 6000;
					cp4.LinkMap[num34].Down = ((num33 < bUILDERACCCOUNT - 1) ? (num34 + 1) : -2);
					cp4.LinkMap[num34].Up = ((num33 > 0) ? (num34 - 1) : -1);
				}
			};
			cp4.IsValidEvent += (() => Main.playerInventory && UILinkPointNavigator.Shortcuts.BUILDERACCCOUNT > 0);
			cp4.PageOnLeft = 8;
			cp4.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp4, 18, true);
			UILinkPage uilinkPage2 = new UILinkPage();
			uilinkPage2.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			uilinkPage2.LinkMap.Add(2806, new UILinkPoint(2806, true, 2805, 2807, -1, 2808));
			uilinkPage2.LinkMap.Add(2807, new UILinkPoint(2807, true, 2806, 2810, -1, 2809));
			uilinkPage2.LinkMap.Add(2808, new UILinkPoint(2808, true, 2805, 2809, 2806, -2));
			uilinkPage2.LinkMap.Add(2809, new UILinkPoint(2809, true, 2808, 2811, 2807, -2));
			uilinkPage2.LinkMap.Add(2810, new UILinkPoint(2810, true, 2807, -4, -1, 2811));
			uilinkPage2.LinkMap.Add(2811, new UILinkPoint(2811, true, 2809, -4, 2810, -2));
			uilinkPage2.LinkMap.Add(2805, new UILinkPoint(2805, true, -3, 2806, -1, -2));
			uilinkPage2.LinkMap[2806].OnSpecialInteracts += value;
			uilinkPage2.LinkMap[2807].OnSpecialInteracts += value;
			uilinkPage2.LinkMap[2808].OnSpecialInteracts += value;
			uilinkPage2.LinkMap[2809].OnSpecialInteracts += value;
			uilinkPage2.LinkMap[2805].OnSpecialInteracts += value;
			uilinkPage2.CanEnterEvent += (() => Main.clothesWindow);
			uilinkPage2.IsValidEvent += (() => Main.clothesWindow);
			uilinkPage2.EnterEvent += delegate()
			{
				Main.player[Main.myPlayer].releaseInventory = false;
			};
			uilinkPage2.LeaveEvent += delegate()
			{
				Main.player[Main.myPlayer].LockGamepadTileInteractions();
			};
			uilinkPage2.PageOnLeft = 15;
			uilinkPage2.PageOnRight = 15;
			UILinkPointNavigator.RegisterPage(uilinkPage2, 14, true);
			UILinkPage uilinkPage3 = new UILinkPage();
			uilinkPage3.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[56].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + PlayerInput.BuildCommand(Lang.misc[64].Value, true, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarMinus"],
				PlayerInput.ProfileGamepadUI.KeyStatus["HotbarPlus"]
			}));
			uilinkPage3.LinkMap.Add(2800, new UILinkPoint(2800, true, -3, -4, -1, 2801));
			uilinkPage3.LinkMap.Add(2801, new UILinkPoint(2801, true, -3, -4, 2800, 2802));
			uilinkPage3.LinkMap.Add(2802, new UILinkPoint(2802, true, -3, -4, 2801, 2803));
			uilinkPage3.LinkMap.Add(2803, new UILinkPoint(2803, true, -3, 2804, 2802, -2));
			uilinkPage3.LinkMap.Add(2804, new UILinkPoint(2804, true, 2803, -4, 2802, -2));
			uilinkPage3.LinkMap[2800].OnSpecialInteracts += value;
			uilinkPage3.LinkMap[2801].OnSpecialInteracts += value;
			uilinkPage3.LinkMap[2802].OnSpecialInteracts += value;
			uilinkPage3.LinkMap[2803].OnSpecialInteracts += value;
			uilinkPage3.LinkMap[2804].OnSpecialInteracts += value;
			uilinkPage3.UpdateEvent += delegate()
			{
				Vector3 vector = Main.rgbToHsl(Main.selColor);
				float interfaceDeadzoneX = PlayerInput.CurrentProfile.InterfaceDeadzoneX;
				float x = PlayerInput.GamepadThumbstickLeft.X;
				x = ((x >= 0f - interfaceDeadzoneX && x <= interfaceDeadzoneX) ? 0f : (MathHelper.Lerp(0f, 0.008333334f, (Math.Abs(x) - interfaceDeadzoneX) / (1f - interfaceDeadzoneX)) * (float)Math.Sign(x)));
				int currentPoint = UILinkPointNavigator.CurrentPoint;
				if (currentPoint == 2800)
				{
					Main.hBar = MathHelper.Clamp(Main.hBar + x, 0f, 1f);
				}
				if (currentPoint == 2801)
				{
					Main.sBar = MathHelper.Clamp(Main.sBar + x, 0f, 1f);
				}
				if (currentPoint == 2802)
				{
					Main.lBar = MathHelper.Clamp(Main.lBar + x, 0.15f, 1f);
				}
				Vector3.Clamp(vector, Vector3.Zero, Vector3.One);
				if (x != 0f)
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
			uilinkPage3.CanEnterEvent += (() => Main.clothesWindow);
			uilinkPage3.IsValidEvent += (() => Main.clothesWindow);
			uilinkPage3.EnterEvent += delegate()
			{
				Main.player[Main.myPlayer].releaseInventory = false;
			};
			uilinkPage3.LeaveEvent += delegate()
			{
				Main.player[Main.myPlayer].LockGamepadTileInteractions();
			};
			uilinkPage3.PageOnLeft = 14;
			uilinkPage3.PageOnRight = 14;
			UILinkPointNavigator.RegisterPage(uilinkPage3, 15, true);
			UILinkPage cp3 = new UILinkPage();
			cp3.UpdateEvent += delegate()
			{
				PlayerInput.GamepadAllowScrolling = true;
			};
			for (int num25 = 3000; num25 <= 4999; num25++)
			{
				cp3.LinkMap.Add(num25, new UILinkPoint(num25, true, -3, -4, -1, -2));
			}
			cp3.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[53].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
			}) + PlayerInput.BuildCommand(Lang.misc[82].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + UILinksInitializer.FancyUISpecialInstructions());
			cp3.UpdateEvent += delegate()
			{
				bool flag11 = PlayerInput.Triggers.JustPressed.Inventory;
				if (Main.inputTextEscape)
				{
					Main.inputTextEscape = false;
					flag11 = true;
				}
				if (UILinksInitializer.CanExecuteInputCommand() && flag11)
				{
					UILinksInitializer.FancyExit();
				}
				UILinkPointNavigator.Shortcuts.BackButtonInUse = false;
			};
			cp3.EnterEvent += delegate()
			{
				cp3.CurrentPoint = 3002;
			};
			cp3.CanEnterEvent += (() => Main.MenuUI.IsVisible || Main.InGameUI.IsVisible);
			cp3.IsValidEvent += (() => Main.MenuUI.IsVisible || Main.InGameUI.IsVisible);
			UILinkPage cp24 = cp3;
			Action<int> value22;
			if ((value22 = UILinksInitializer.<>O.<0>__OnFancyUIPageMoveAttempt) == null)
			{
				value22 = (UILinksInitializer.<>O.<0>__OnFancyUIPageMoveAttempt = new Action<int>(UILinksInitializer.OnFancyUIPageMoveAttempt));
			}
			cp24.OnPageMoveAttempt += value22;
			UILinkPointNavigator.RegisterPage(cp3, 1004, true);
			UILinkPage cp2 = new UILinkPage();
			cp2.UpdateEvent += delegate()
			{
				PlayerInput.GamepadAllowScrolling = true;
			};
			for (int num26 = 10000; num26 <= 11000; num26++)
			{
				cp2.LinkMap.Add(num26, new UILinkPoint(num26, true, -3, -4, -1, -2));
			}
			for (int num27 = 15000; num27 <= 15000; num27++)
			{
				cp2.LinkMap.Add(num27, new UILinkPoint(num27, true, -3, -4, -1, -2));
			}
			cp2.OnSpecialInteracts += (() => PlayerInput.BuildCommand(Lang.misc[53].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["MouseLeft"]
			}) + PlayerInput.BuildCommand(Lang.misc[82].Value, false, new List<string>[]
			{
				PlayerInput.ProfileGamepadUI.KeyStatus["Inventory"]
			}) + UILinksInitializer.FancyUISpecialInstructions());
			cp2.UpdateEvent += delegate()
			{
				if (UILinksInitializer.CanExecuteInputCommand() && PlayerInput.Triggers.JustPressed.Inventory)
				{
					UILinksInitializer.FancyExit();
				}
				UILinkPointNavigator.Shortcuts.BackButtonInUse = false;
			};
			cp2.EnterEvent += delegate()
			{
				cp2.CurrentPoint = 10000;
			};
			UILinkPage cp23 = cp2;
			Func<bool> value23;
			if ((value23 = UILinksInitializer.<>O.<1>__CanEnterCreativeMenu) == null)
			{
				value23 = (UILinksInitializer.<>O.<1>__CanEnterCreativeMenu = new Func<bool>(UILinksInitializer.CanEnterCreativeMenu));
			}
			cp23.CanEnterEvent += value23;
			UILinkPage cp22 = cp2;
			Func<bool> value24;
			if ((value24 = UILinksInitializer.<>O.<1>__CanEnterCreativeMenu) == null)
			{
				value24 = (UILinksInitializer.<>O.<1>__CanEnterCreativeMenu = new Func<bool>(UILinksInitializer.CanEnterCreativeMenu));
			}
			cp22.IsValidEvent += value24;
			UILinkPage cp21 = cp2;
			Action<int> value25;
			if ((value25 = UILinksInitializer.<>O.<0>__OnFancyUIPageMoveAttempt) == null)
			{
				value25 = (UILinksInitializer.<>O.<0>__OnFancyUIPageMoveAttempt = new Action<int>(UILinksInitializer.OnFancyUIPageMoveAttempt));
			}
			cp21.OnPageMoveAttempt += value25;
			cp2.PageOnLeft = 8;
			cp2.PageOnRight = 0;
			UILinkPointNavigator.RegisterPage(cp2, 1005, true);
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
			for (int num28 = 9000; num28 <= 9050; num28++)
			{
				UILinkPoint uILinkPoint19 = new UILinkPoint(num28, true, num28 + 10, num28 - 10, num28 - 1, num28 + 1);
				cp.LinkMap.Add(num28, uILinkPoint19);
				uILinkPoint19.OnSpecialInteracts += value21;
			}
			cp.UpdateEvent += delegate()
			{
				int num33 = UILinkPointNavigator.Shortcuts.BUFFS_PER_COLUMN;
				if (num33 == 0)
				{
					num33 = 100;
				}
				for (int num34 = 0; num34 < 50; num34++)
				{
					cp.LinkMap[9000 + num34].Up = ((num34 % num33 == 0) ? -1 : (9000 + num34 - 1));
					if (cp.LinkMap[9000 + num34].Up == -1)
					{
						if (num34 >= num33)
						{
							cp.LinkMap[9000 + num34].Up = 184;
						}
						else
						{
							cp.LinkMap[9000 + num34].Up = 189;
						}
					}
					cp.LinkMap[9000 + num34].Down = (((num34 + 1) % num33 == 0 || num34 == UILinkPointNavigator.Shortcuts.BUFFS_DRAWN - 1) ? 308 : (9000 + num34 + 1));
					cp.LinkMap[9000 + num34].Left = ((num34 < UILinkPointNavigator.Shortcuts.BUFFS_DRAWN - num33) ? (9000 + num34 + num33) : -3);
					cp.LinkMap[9000 + num34].Right = ((num34 < num33) ? -4 : (9000 + num34 - num33));
				}
			};
			cp.IsValidEvent += (() => Main.playerInventory && Main.EquipPage == 2 && UILinkPointNavigator.Shortcuts.BUFFS_DRAWN > 0);
			cp.PageOnLeft = 8;
			cp.PageOnRight = 8;
			UILinkPointNavigator.RegisterPage(cp, 19, true);
			UILinkPage uilinkPage4 = UILinkPointNavigator.Pages[UILinkPointNavigator.CurrentPage];
			uilinkPage4.CurrentPoint = uilinkPage4.DefaultPoint;
			uilinkPage4.Enter();
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x0056ABDD File Offset: 0x00568DDD
		private static bool CanEnterCreativeMenu()
		{
			return Main.LocalPlayer.chest == -1 && Main.LocalPlayer.talkNPC == -1 && Main.playerInventory && Main.CreativeMenu.Enabled;
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x0056AC10 File Offset: 0x00568E10
		private static int GetCornerWrapPageIdFromLeftToRight()
		{
			return 8;
		}

		// Token: 0x060034FA RID: 13562 RVA: 0x0056AC13 File Offset: 0x00568E13
		private static int GetCornerWrapPageIdFromRightToLeft()
		{
			if (Main.CreativeMenu.Enabled)
			{
				return 1005;
			}
			return 10;
		}

		// Token: 0x060034FB RID: 13563 RVA: 0x0056AC2C File Offset: 0x00568E2C
		private static void OnFancyUIPageMoveAttempt(int direction)
		{
			UICharacterCreation uICharacterCreation = Main.MenuUI.CurrentState as UICharacterCreation;
			if (uICharacterCreation != null)
			{
				uICharacterCreation.TryMovingCategory(direction);
			}
			UIBestiaryTest uIBestiaryTest = UserInterface.ActiveInstance.CurrentState as UIBestiaryTest;
			if (uIBestiaryTest != null)
			{
				uIBestiaryTest.TryMovingPages(direction);
			}
		}

		// Token: 0x060034FC RID: 13564 RVA: 0x0056AC70 File Offset: 0x00568E70
		public static void FancyExit()
		{
			int backButtonCommand = UILinkPointNavigator.Shortcuts.BackButtonCommand;
			switch (backButtonCommand)
			{
			case 1:
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Main.menuMode = 0;
				return;
			case 2:
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Main.menuMode = ((!Main.menuMultiplayer) ? 1 : 12);
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
					return;
				}
				break;
			}
			default:
				if (backButtonCommand != 100)
				{
					return;
				}
				SoundEngine.PlaySound(11, -1, -1, 1, 1f, 0f);
				Main.menuMode = UILinkPointNavigator.Shortcuts.BackButtonGoto;
				break;
			}
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x0056AD78 File Offset: 0x00568F78
		public static string FancyUISpecialInstructions()
		{
			string text = "";
			if (UILinkPointNavigator.Shortcuts.FANCYUI_SPECIAL_INSTRUCTIONS == 1)
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

		// Token: 0x060034FE RID: 13566 RVA: 0x0056AF3C File Offset: 0x0056913C
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
				float num3 = Main.hBar = UILinksInitializer.HandleSliderHorizontalInput(hBar, 0f, 1f, 0.2f, 0.5f);
				if (hBar != num3)
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
						Main.mouseColorSlider.Hue = num3;
						break;
					default:
						if (menuMode == 252)
						{
							Main.mouseBorderColorSlider.Hue = num3;
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
				float num4 = Main.sBar = UILinksInitializer.HandleSliderHorizontalInput(sBar, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f);
				if (sBar != num4)
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
						Main.mouseColorSlider.Saturation = num4;
						break;
					default:
						if (menuMode == 252)
						{
							Main.mouseBorderColorSlider.Saturation = num4;
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
				float lBar3 = Main.lBar;
				float min = 0.15f;
				if (Main.menuMode == 252)
				{
					min = 0f;
				}
				Main.lBar = UILinksInitializer.HandleSliderHorizontalInput(lBar3, min, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f);
				float lBar2 = Main.lBar;
				if (lBar3 != lBar2)
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
				float num5 = Main.aBar = UILinksInitializer.HandleSliderHorizontalInput(aBar, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.5f);
				if (aBar != num5)
				{
					if (Main.menuMode == 252)
					{
						Main.mouseBorderColorSlider.Alpha = num5;
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
				int num6 = 51;
				if (Main.PendingPlayer.hair >= num6)
				{
					Main.PendingPlayer.hair = 0;
				}
				if (Main.PendingPlayer.hair < 0)
				{
					Main.PendingPlayer.hair = num6 - 1;
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

		// Token: 0x02000B35 RID: 2869
		public class SomeVarsForUILinkers
		{
			// Token: 0x04006F79 RID: 28537
			public static Recipe SequencedCraftingCurrent;

			// Token: 0x04006F7A RID: 28538
			public static int HairMoveCD;
		}

		// Token: 0x02000B36 RID: 2870
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04006F7B RID: 28539
			public static Action<int> <0>__OnFancyUIPageMoveAttempt;

			// Token: 0x04006F7C RID: 28540
			public static Func<bool> <1>__CanEnterCreativeMenu;
		}
	}
}
